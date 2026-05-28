namespace BookShoppingCartMvc.Controllers
{
    [Authorize(Roles =nameof(Roles.Admin))]
    public class GenreController:Controller
    {
        private readonly IGenreRepository _genreRepository;

        public GenreController(IGenreRepository genreRepository)
        {
            this._genreRepository = genreRepository;
        }

        public async Task<IActionResult> Index()
        {
            var genres=await _genreRepository.GetGenres();
            return View(genres);
        }

        public IActionResult AddGenre()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddGenre(GenreViewModel genreViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(genreViewModel);
            }
            try
            {
                var genreToAdd = new Genre()
                {
                    Id = genreViewModel.Id,
                    GenreName = genreViewModel.GenreName
                };
                await _genreRepository.AddGenre(genreToAdd);
                TempData["successMessage"] = "Genre added successfully";
                return RedirectToAction(nameof(AddGenre));
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "Genre could not added";
                return View(genreViewModel);
            }
        }

        public async Task<IActionResult> UpdateGenre(int id)
        {
            var genre = await _genreRepository.GetGenreById(id);

            if (genre == null)
                throw new InvalidOperationException($"Genre with id:{id} dose not found");
            var genreToUpdate = new GenreViewModel()
            {
                Id = id,
                GenreName = genre.GenreName
            };
            return View(genreToUpdate);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateGenre(GenreViewModel genreViewModel)
        {
            if(!ModelState.IsValid)
            {
                return View(genreViewModel);
            }
            try
            {
                var genre = new Genre()
                {
                    Id = genreViewModel.Id,
                    GenreName = genreViewModel.GenreName
                };
                await _genreRepository.UpdateGenre(genre);
                TempData["successMessage"] = "Genre is update successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "Genre could not updated";
                return View(genreViewModel);
            }

        }
        public async Task<IActionResult> DeleteGenre(int id)
        {
            var genre = await _genreRepository.GetGenreById(id);
            if (genre == null)
                throw new InvalidOperationException($"Genre with id:{id} dose not found");

            await _genreRepository.DeleteGenre(genre);
            return RedirectToAction(nameof(Index));
        }

    }
}
