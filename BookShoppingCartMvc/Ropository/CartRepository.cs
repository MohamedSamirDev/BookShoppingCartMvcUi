namespace BookShoppingCartMvc.Ropository
{
    public class CartRepository:ICartRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public CartRepository(ApplicationDbContext context
            ,IHttpContextAccessor httpContextAccessor
            ,UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }

        public async Task<int> AddItem(int BookId,int Quantity)
        {
            string? UserId = GetUserId();
            using var transaction=_context.Database.BeginTransaction();
            try
            {              
                if (string.IsNullOrEmpty(UserId))
                    throw new UnauthorizedAccessException("User is not Logged-in");
                var Cart = await GetCart(UserId);

                if (Cart == null)
                {
                    Cart = new ShoppingCart
                    {
                        UserId = UserId,

                    };
                    _context.ShoppingCarts.Add(Cart);
                }

                _context.SaveChanges();

                var CartItem = _context.CartDetails.FirstOrDefault
                    (a => a.ShoppingCartId == Cart.Id && a.BookId == BookId);
                if (CartItem != null)
                {
                    CartItem.Quantity += Quantity;

                }
                else
                {
                    var book = _context.Books.Find(BookId);
                    CartItem = new CartDetail
                    {
                        BookId = BookId,
                        ShoppingCartId = Cart.Id,
                        Quantity = Quantity,
                        UnitPrice=book.Price
                        
                    }; 
                    _context.CartDetails.Add(CartItem);
                }
                 _context.SaveChanges();
                transaction.Commit();
            }catch (Exception ex)
            {

            }

            var CartItemCount =  await GetCartItemCount(UserId);
            return CartItemCount;

        } 
        
        public async Task<int> RemoveItem(int BookId)
        {
            string? UserId = GetUserId();

            try
            {
                if (string.IsNullOrEmpty(UserId))
                    throw new UnauthorizedAccessException("User is not Logged-in");

                var Cart = await GetCart(UserId);

                if (Cart == null)
                {
                    throw new InvalidOperationException("Invalid Cart");

                }

                _context.SaveChanges();
               var CartItem = _context.CartDetails.FirstOrDefault
                    (a => a.ShoppingCartId == Cart.Id && a.BookId == BookId);
                if(CartItem == null)
                    throw new Exception("Not items in Cart");
                else if (CartItem.Quantity==1)
                {
                   _context.CartDetails.Remove(CartItem);
                }
                else
                {
                   CartItem.Quantity=CartItem.Quantity-1;
                }
                 _context.SaveChanges();              
            }catch (Exception ex) { }                 
            var CartItemCount = await GetCartItemCount(UserId);
            return CartItemCount;
        }

        public async Task<ShoppingCart> GetUserCart()
        {
            var UserId= GetUserId();
            if (UserId == null)
                throw new InvalidOperationException("Invalid UserId");
            var ShoppingCart = await _context.ShoppingCarts
                                        .Include(a=>a.CartDetails)
                                        .ThenInclude(a=>a.book)
                                        .ThenInclude(a=>a.Stock)
                                       .Include(a => a.CartDetails)
                                       .ThenInclude(a => a.book)
                                       .ThenInclude(a => a.Genre)
                                       .Where(a => a.UserId == UserId).FirstOrDefaultAsync();
            return ShoppingCart;
                                       


        }


        public async Task<int> GetCartItemCount(string userId="")
        {
             userId = GetUserId();
            if (userId == null)
                return 0;

            var cart = await _context.ShoppingCarts
                .Include(c => c.CartDetails)
                .FirstOrDefaultAsync(c => c.UserId == userId && !c.IsDeleted);

            //var data = await (from cart in _context.ShoppingCarts
            //                  join cartDetails in _context.CartDetails
            //                  on cart.Id equals cartDetails.ShoppingCartId
            //                  select new { cartDetails.Id }


            //                 ).ToListAsync();             
            return cart?.CartDetails.Count ?? 0;
        }

        public async Task<ShoppingCart> GetCart(string userId)
        {
            ShoppingCart? cart= await _context.ShoppingCarts.FirstOrDefaultAsync(x => x.UserId == userId);
            return cart;
        }

        public bool IsAuthenticated()
        {
            return _httpContextAccessor.HttpContext?.User?.Identity?.IsAuthenticated ?? false;
        }
        public string? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                return null;

            return _userManager.GetUserId(user);
        }

        public async Task<bool> DoCheckout(CheckoutViewModel checkoutModel)
        {
            using var tranaction=_context.Database.BeginTransaction();
            try
            {
                //Logic
                //move data from CartDetail to order and orderDetail then we will remove cartdetail
                
                var userId=GetUserId();
                if (string.IsNullOrEmpty(userId))
                    throw new UnauthorizedAccessException("User is not Logged-in");
                var Cart= await GetCart(userId);
                if(Cart==null)
                    throw new InvalidOperationException("Invalid Cart");

                var CartDetail=_context.CartDetails
                                       .Where(a=>a.ShoppingCartId==Cart.Id).ToList();
                if (CartDetail.Count == 0)
                    throw new InvalidOperationException("Cart is Empty");
                var pendingRecord = _context.OrderStatus.FirstOrDefault
                    (a => a.StatusName == "Pending");
                if (pendingRecord == null)
                    throw new InvalidOperationException("Order Status dose not have pending ststus");

                var Order = new Order()
                {
                    UserId = userId,//Same User
                    CreateDate = DateTime.UtcNow,

                    Name = checkoutModel.Name,
                    Email = checkoutModel.Email,
                    MobilNumber = checkoutModel.MobilNumber,
                    PaymentMethod = checkoutModel.PaymentMethod,
                    Address = checkoutModel.Address,
                    IsPaid = false,

                    OrderStatusId = pendingRecord.Id //By Default =>pending
                };

                _context.Order.Add(Order);
                _context.SaveChanges();
                //Linking(ربط)Table(OrderDetail)==>Table(CartDetail) 
                //يعني كل عنصر في العربيه مبروط ب تفاصيل 
                foreach(var item in CartDetail)
                {
                    var orderDetail = new OrderDetail()
                    {
                        BookId = item.BookId,
                        OrderId = Order.Id,
                        Quantity = item.Quantity,
                        UnitPrice = item.UnitPrice,
                    };
                    _context.OrderDetail.Add(orderDetail);

                    //Update Stock here
                    var stock=await _context.Stocks.FirstOrDefaultAsync
                        (a=>a.BookId==item.BookId);

                    if (stock == null)
                        throw new InvalidOperationException("Stock is null");

                    if (item.Quantity > stock.Quantity)
                        throw new InvalidOperationException($"Only " +
                            $"{stock.Quantity} items(s) are available in the stock");

                    //decrease the number of quantity from the stock table
                    stock.Quantity=item.Quantity;
                }
                _context.SaveChanges();


                //Remove CartDetalis

                _context.CartDetails.RemoveRange(CartDetail);
                _context.SaveChanges();
                tranaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
