namespace BookShoppingCartMvc.Controllers
{
    [Authorize(Roles =nameof(Roles.Admin))]
    public class StockController:Controller
    {
        private readonly IStockRepository _stockRepository;

        public StockController(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }
        public async Task<IActionResult> Index(string sterm = "")
        {
            var stocks=await _stockRepository.GetStocks(sterm);
            return View(stocks);
        }
    
        public async Task<IActionResult> ManagerStock(int bookId)
        {
            var existingStock = await _stockRepository.GetStockByBookId(bookId);
            var stock = new StockViewModel
            {
                BookId = bookId,
                Quantity = existingStock != null ? existingStock.Quantity : 0,
            };
            return View(stock);
        }

        [HttpPost]
        public async Task<IActionResult> ManagerStock(StockViewModel stock)
        {
            if (!ModelState.IsValid)
                return View(stock);

            try
            {
                await _stockRepository.ManageStock(stock);
                TempData["successMessage"] = "Stock is update Successfully";

            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "Something went wrong";

            }
            return RedirectToAction(nameof(Index));
        }

    }
}
