namespace BookShoppingCartMvc.Models
{
    public class CartDetail
    {
        public int Id { get; set; }


        [Required]
        public double UnitPrice { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public int ShoppingCartId {  get; set; }
        public ShoppingCart shoppingCart { get; set; }

        [Required]
        public int BookId { get; set; }
        public Book book { get; set; }

      
    }
}
