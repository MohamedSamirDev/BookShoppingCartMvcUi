namespace BookShoppingCartMvc.Ropository
{
    public class StockRepository: IStockRepository
    {
        private readonly ApplicationDbContext _context;

        public StockRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Stock>? GetStockByBookId(int bookId)
        {
           var result= await  _context.Stocks.FirstOrDefaultAsync(a => a.BookId == bookId);
            return result;
        }

        public async Task ManageStock(StockViewModel stockViewModel)
        {
            //if there is no stock for given book id,then add new record
            //if there is already stock for given book id,update stocks quantity

            var existingStock = await GetStockByBookId(stockViewModel.BookId);
            if (existingStock == null)
            {
                var stock = new Stock()
                {
                    BookId = stockViewModel.BookId,
                    Quantity = stockViewModel.Quantity,
                    
                };
                _context.Stocks.Add(stock);
            }
            else
            {
                existingStock.Quantity = stockViewModel.Quantity;
            }
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StockDisplayModel>> GetStocks(string sterm = "")
        {
            var stocks=await (from book in _context.Books
                              join stock in _context.Stocks
                              on book.Id equals stock.BookId
                              into book_stock
                              from bookStock in book_stock.DefaultIfEmpty()
                              where string.IsNullOrWhiteSpace(sterm) || book.BookName.ToLower().Contains(sterm.ToLower())
                              select new StockDisplayModel
                              {
                                  BookId = book.Id,
                                  BookName = book.BookName,
                                  Quantity=bookStock==null ? 0 : bookStock.Quantity,
                              }


                              ).ToListAsync();
            return stocks;

        }
    }
}
