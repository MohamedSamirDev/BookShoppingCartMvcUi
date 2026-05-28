
using BookShoppingCartMvc.Models;

namespace BookShoppingCartMvc.Ropository
{
    public class BookRepository : IBookRepository
    {
        private readonly ApplicationDbContext _context;

        public BookRepository(ApplicationDbContext context)
        {
            this._context = context;
        }

        public  async Task AddBook(Book book)
        {
           _context.Books.Add(book);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteBook(Book book)
        {
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
        }

        public async Task<Book?> GetBookById(int id)
        {
            return await _context.Books.FindAsync(id);
        }

        public async Task<IEnumerable<Book>> GetBooks()
        {
           return await _context.Books.Include(a=>a.Genre).ToListAsync();
        }

        public async Task UpdateBook(Book book)
        {
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Book>> DisplayBooks(string sterm = "" )
        {
            // تأكد من أن البحث يكون غير حساس لحالة الحروف
            sterm = sterm?.Trim().ToLower() ?? "";

            var books = await (
                from book in _context.Books
                join genre in _context.Genres
                    on book.GenreId equals genre.Id
                where (string.IsNullOrWhiteSpace(sterm) || book.BookName.ToLower().Contains(sterm))              
                select new Book
                {
                    Id = book.Id,
                    BookName = book.BookName,
                    AuthorName = book.AuthorName,
                    Price = book.Price,
                    Images = book.Images,
                    GenreId = book.GenreId,
                    GenreName = genre.GenreName
                }
            ).ToListAsync();

            return books;
        }

    }
}