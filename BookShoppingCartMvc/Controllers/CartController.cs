


using System.Threading.Tasks;

namespace BookShoppingCartMvc.Controllers
{
    [Authorize]
    public class CartController:Controller
    {
        private readonly ICartRepository _cartRepository;

        public CartController(ICartRepository cartRepository)
        {
            this._cartRepository = cartRepository;
        }
        //C
        
        public async Task<IActionResult> AddItem(int BookId,int Quantity=1,int redirect=0)
        {
            var CartCount= await _cartRepository.AddItem(BookId, Quantity);
            if (redirect == 0)
                return Ok(CartCount);

          return RedirectToAction("GetUserCart");

            
        }

        //R
       public async Task<IActionResult> RemoveItem(int BookId)
       {
            var CartCount = await _cartRepository.RemoveItem(BookId);
            return RedirectToAction("GetUserCart");

       }
        public async Task<IActionResult> GetUserCart()
        {
            var cart=await _cartRepository.GetUserCart();
            return View(cart);
        }
      public async Task<IActionResult> GetTotalItemInCart()
      {
            int CartItem=await _cartRepository.GetCartItemCount();
            return Ok(CartItem);
      }

        //CheckOut

        public IActionResult Checkout()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Checkout(CheckoutViewModel viewModel)
        {
            if(!ModelState.IsValid) 
                return View(viewModel);
            bool IsCheckedout = await _cartRepository.DoCheckout(viewModel);
            if (!IsCheckedout)
                return RedirectToAction(nameof(OrderFailed));
            return RedirectToAction(nameof(OrderSuccess));
        }
        public IActionResult OrderSuccess()
        {
            return View();
        }
        public IActionResult OrderFailed()
        {
            return View();
        }
    }
}
