namespace BookShoppingCartMvc.ViewModel
{
    public class StockViewModel
    {
        public int BookId {  get; set; }

        [Range(0,int.MaxValue,ErrorMessage ="Quantity must be a non-negative Value")]
        public int Quantity {  get; set; }
    }
}
