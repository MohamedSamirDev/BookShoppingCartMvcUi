namespace BookShoppingCartMvc.ViewModel
{
    public class Book_Genre_DisplayViewModel
    {

        public IEnumerable<Book> Books { get; set; } 
        public IEnumerable<Genre> Genres { get; set; }

        public string sterm { get; set; } = "";
        public int genreId { get; set; } = 0;
    }
}
