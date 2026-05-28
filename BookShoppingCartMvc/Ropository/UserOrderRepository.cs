using Microsoft.AspNetCore.Identity;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookShoppingCartMvc.Ropository
{
    public class UserOrderRepository: IUserOrderRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public UserOrderRepository(ApplicationDbContext context
            , IHttpContextAccessor httpContextAccessor
            , UserManager<IdentityUser> userManager)
        {
            this._context = context;
            this._httpContextAccessor = httpContextAccessor;
            this._userManager = userManager;
        }

        public async Task ChangeOrderStatus(UpdateOrderStatusViewModel data)
        {
            var order=await _context.Order.FindAsync(data.OrderId);
            if (order == null)
            {
                throw new InvalidOperationException($"Order with id {data.OrderId} dose not found");
            }
            order.OrderStatusId = data.OrderStatusId;
            await _context.SaveChangesAsync();
        } 

        public async Task<Order?> GetOrderById(int id)
        {
            return await _context.Order.FindAsync(id);
        }

        public async Task<IEnumerable<OrderStatus>> GetOrderStatuses()
        {
             return await _context.OrderStatus.ToListAsync();            
        }

        public string? GetUserId()
        {
            var user = _httpContextAccessor.HttpContext?.User;

            if (user?.Identity?.IsAuthenticated != true)
                return null;

            return _userManager.GetUserId(user);
        }

        public async Task TogglePaymentStatus(int orderId)
        {
            var order=await _context.Order.FindAsync(orderId);
            if(order == null)
            {
                throw new InvalidOperationException($"Order with id {orderId} dose not found");
            }
            order.IsPaid= !order.IsPaid;
            await _context.SaveChangesAsync();

        }

        public async Task<IEnumerable<Order>> UserOrders(bool getAll = false)
        {
            var orders =  _context.Order

                                  .Include(x => x.OrderStatus)
                                  .Include(x => x.OrderDetails)
                                  .ThenInclude(x => x.Book)
                                  .ThenInclude(x => x.Genre).AsQueryable();
            if (!getAll)
            {
                var userId = GetUserId();
                if (string.IsNullOrEmpty(userId))
                  throw new Exception("User is not Logged-in");
                orders=orders.Where(a=>a.UserId == userId);
                return await orders.ToListAsync();
            }
            return await orders.ToListAsync();

          
        }
    
    }
}
