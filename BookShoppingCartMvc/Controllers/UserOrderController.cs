using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BookShoppingCartMvc.Controllers
{
    [Authorize]
    public class UserOrderController : Controller
    {
        private readonly IUserOrderRepository _orderRepository;

        public UserOrderController(IUserOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IActionResult> UserOrder()
        {
            var orders = await _orderRepository.UserOrders();
            return View(orders);
        }
    }
}
