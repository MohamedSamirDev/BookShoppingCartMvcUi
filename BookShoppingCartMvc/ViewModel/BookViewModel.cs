using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShoppingCartMvc.ViewModel
{
    public class BookViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(40)]
        public string? BookName { get; set; }

        [Required]
        [MaxLength(40)]
        public string? AuthorName { get; set; }

        public double Price {  get; set; }

        public string? Image {  get; set; }

        [Required]
        public int GenreId {  get; set; }

        public IFormFile? ImageFile { get; set; }

        public IEnumerable<SelectListItem>? GenreList { get; set; }
    }
}
