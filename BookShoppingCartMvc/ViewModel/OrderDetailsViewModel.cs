namespace BookShoppingCartMvc.ViewModel
{
    public class OrderDetailsViewModel
    {
        public string DivId {  get; set; }

        public IEnumerable<OrderDetail> OrderDetail {  get; set; }
    }
}
