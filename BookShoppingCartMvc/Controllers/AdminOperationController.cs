using BookShoppingCartMvc.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BookShoppingCartMvc.Controllers
{
    [Authorize(Roles =nameof(Roles.Admin))]
    public class AdminOperationController:Controller
    {
        private readonly IUserOrderRepository _userOrderRepository;

        public AdminOperationController(IUserOrderRepository userOrderRepository)
        {
            this._userOrderRepository = userOrderRepository;
        }
        public async Task<IActionResult> AllOrders()
        {
            var orders=await _userOrderRepository.UserOrders(true);
            return View(orders);
        }

        public async Task<IActionResult> TogglePaymentStatus(int orderId)
        {
            try
            {

                await _userOrderRepository.TogglePaymentStatus(orderId);
            }
            catch (Exception ex) 
            {
                 //log exception here            
            }
            return RedirectToAction(nameof(AllOrders));


        }

        public async Task<IActionResult> UpdatePaymentStatus(int orderId) 
        { 
            var order=await _userOrderRepository.GetOrderById(orderId);
            if (order == null)
            {
                throw new InvalidOperationException
                    ($"Order with id {orderId} dose not found");
            }
            var OrderStatusList = (await _userOrderRepository.GetOrderStatuses()).
                Select(orderStatus =>
                {
                    return new SelectListItem
                    {
                        Value = orderStatus.Id.ToString(),
                        Text = orderStatus.StatusName,
                        Selected = order.OrderStatusId == orderStatus.Id
                    };


                });

            var data = new UpdateOrderStatusViewModel
            {
                OrderId = orderId,
                OrderStatusId = order.OrderStatusId,
                OrderStatusList = OrderStatusList,

            };
            return View(data);
        }

        [HttpPost]
        public async Task<IActionResult> UpdatePaymentStatus(UpdateOrderStatusViewModel data)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    data.OrderStatusList = (await
                        _userOrderRepository.GetOrderStatuses()).
                        Select(orderStatus =>
                        {
                            return new SelectListItem
                            {
                                Value = orderStatus.Id.ToString(),
                                Text = orderStatus.StatusName,
                                Selected = data.OrderStatusId == orderStatus.Id
                            };


                        });
                    return View(data);
                }
                await _userOrderRepository.ChangeOrderStatus(data);
                TempData["msg"] = "Update Successfully";
            }
            catch (Exception ex) 
            {
                TempData["msg"] = "Something went wrong";

            }

            return RedirectToAction(nameof(UpdatePaymentStatus), new { orderId = data.OrderId });

        }



    }
}
