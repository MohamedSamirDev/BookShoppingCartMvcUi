
namespace BookShoppingCartMvc.Models
{
    public class Genre
    {
        public int Id { get; set; }


        [Required]
        [MaxLength(20)]
        public string GenreName { get; set; }

        public List<Book> Books { get; set; }
    }
}
