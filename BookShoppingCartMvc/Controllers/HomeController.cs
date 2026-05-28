
namespace BookShoppingCartMvc.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHomeRepository _repository;

        public HomeController(ILogger<HomeController> logger,IHomeRepository repository)
        {
            _logger = logger;
            this._repository = repository;
        }

        public async  Task<IActionResult> Index(string sterm="",int genreId=0)
        {
            IEnumerable<Book> books= await _repository.DisplayBooks(sterm, genreId);
            IEnumerable<Genre> genres = await _repository.Genres();

            Book_Genre_DisplayViewModel BookModel = new Book_Genre_DisplayViewModel()
            {
                Books = books,
                Genres = genres,
                sterm=sterm,
                genreId=genreId
                
            };

            return View(BookModel);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
