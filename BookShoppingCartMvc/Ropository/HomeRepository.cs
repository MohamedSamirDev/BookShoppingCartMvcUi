using AspNetCoreGeneratedDocument;
using Microsoft.EntityFrameworkCore;

namespace BookShoppingCartMvc.Ropository
{
    public class HomeRepository:IHomeRepository
    {
        private readonly ApplicationDbContext _context;

        public HomeRepository(ApplicationDbContext context )
        {
            this._context = context;
        }


        public async Task<IEnumerable<Genre>> Genres()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<IEnumerable<Book>>  DisplayBooks(string sterm="",int genreId = 0)
        {
            sterm = sterm.ToLower();

            IEnumerable<Book> books= await(from book in _context.Books
                       join genre in _context.Genres
                       on book.GenreId equals genre.Id
                       join stock in _context.Stocks
                       on book.Id equals stock.BookId
                       into book_stocks
                       from bookWithStock in book_stocks.DefaultIfEmpty()
                       where string.IsNullOrWhiteSpace(sterm) || (book!=null && book.BookName.ToLower().Contains(sterm))
                      // &&(genreId==0 || book.GenreId==genreId)
                       select new Book
                       {
                          
                           Id= book.Id,
                           Price= book.Price,
                           Images= book.Images,
                           GenreName=genre.GenreName,
                           AuthorName=book.AuthorName,
                           GenreId=book.GenreId,
                           BookName=book.BookName,
                           Quantity=bookWithStock==null?0:bookWithStock.Quantity
                           
                           
                       }
                       
                       ).ToListAsync();

            if (genreId > 0)
            {
              books=  books.Where(a=>a.GenreId == genreId).ToList();
           }

            return books;
        }

    }

    
}
