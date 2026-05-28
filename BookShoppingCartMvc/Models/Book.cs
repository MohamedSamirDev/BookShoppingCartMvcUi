
namespace BookShoppingCartMvc.Models
{
   
    public class Book
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string BookName { get; set; }

        [Required]
        [MaxLength(20)]
        public string AuthorName { get; set; }

        public string? Images { get; set; }

        [Required]  
        public double Price {  get; set; }
        
        public int GenreId {  get; set; }
        public Genre Genre { get; set; }

        public List<CartDetail> CartDetails { get; set; }

        public List<OrderDetail> OrderDetails { get; set; }

        public Stock Stock {  get; set; }
     
        [NotMapped]
        public string GenreName {  get; set; }
       [NotMapped]
    public int Quantity {  get; set; }

    }
}
