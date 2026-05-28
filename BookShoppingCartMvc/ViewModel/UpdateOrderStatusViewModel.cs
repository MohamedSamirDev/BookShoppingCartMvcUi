using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShoppingCartMvc.ViewModel
{
    public class UpdateOrderStatusViewModel
    {
        public int OrderId {  get; set; }

        [Required]
        public int OrderStatusId { get; set; }

        public IEnumerable<SelectListItem>? OrderStatusList { get; set; }
    }
}
