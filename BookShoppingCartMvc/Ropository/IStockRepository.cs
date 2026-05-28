namespace BookShoppingCartMvc.Ropository
{
    public interface IStockRepository
    {
        Task<IEnumerable<StockDisplayModel>> GetStocks(string sterm = "");
        Task ManageStock(StockViewModel stockViewModel);
        Task<Stock>? GetStockByBookId(int bookId);

    }
}