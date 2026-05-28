namespace BookShoppingCartMvc.Ropository
{
    public interface IBookRepository
    {
        Task AddBook(Book book);

        Task DeleteBook(Book book);

        Task UpdateBook(Book book);

        Task<Book?> GetBookById(int id);

        Task<IEnumerable<Book>> GetBooks();

        Task<IEnumerable<Book>> DisplayBooks(string sterm = "");
    }
}