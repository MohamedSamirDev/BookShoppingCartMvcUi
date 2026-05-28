namespace BookShoppingCartMvc.Models
{
    public class OrderDetail
    {

        public int Id { get; set; }

        [Required]
       public  double UnitPrice {  get; set; }

        [Required]
        public int Quantity {  get; set; }

        [Required]
        public int OrderId {  get; set; }
        public Order Order { get; set; }

        public int BookId {  get; set; }
        public Book Book { get; set; }

    }
}
