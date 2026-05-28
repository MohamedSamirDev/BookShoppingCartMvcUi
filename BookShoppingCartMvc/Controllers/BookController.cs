using BookShoppingCartMvc.Models;
using BookShoppingCartMvc.ViewModel;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookShoppingCartMvc.Controllers
{
    [Authorize(Roles = nameof(Roles.Admin))]
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IFileService _fileService;
        private readonly IGenreRepository _genreRepository;

        public BookController(IBookRepository bookRepository, IFileService fileService, IGenreRepository genreRepository)
        {
            this._bookRepository = bookRepository;
            this._fileService = fileService;
            this._genreRepository = genreRepository;
        }
        public async Task<IActionResult> ListBook()
        {
            // جلب كل الكتب مباشرة من الريبو
            var books = await _bookRepository.GetBooks();
            return View(books);
        }


        public async Task<IActionResult> Index()
        {
            var Books = await _bookRepository.GetBooks();
            return View(Books);
        }

        public async Task<IActionResult> AddBook()
        {
            var genreSelectedList = (await _genreRepository.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            BookViewModel bookViewModel = new BookViewModel()
            {
                GenreList = genreSelectedList
            };
            return View(bookViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookViewModel bookViewModel)
        {
            var genreSelectedList = (await _genreRepository.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
            });
            bookViewModel.GenreList = genreSelectedList;

            if(!ModelState.IsValid)
            {
                return View (bookViewModel);
            }
            try
            {
                if (bookViewModel.ImageFile != null)
                {
                    if (bookViewModel.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }

                    string[] allowedExtenstions = [".jpg", ".jpeg", ".png"];
                    string imageName = await _fileService.SaveFile(bookViewModel.ImageFile, allowedExtenstions);

                    bookViewModel.Image = imageName;

                }
                Book book = new Book()
                {
                    Id = bookViewModel.Id,
                    BookName = bookViewModel.BookName,
                    AuthorName = bookViewModel.AuthorName,
                    Images = bookViewModel.Image,
                    GenreId = bookViewModel.GenreId,
                    Price = bookViewModel.Price,
                };
                await _bookRepository.AddBook(book);
                TempData["successMessage"] = "Book is Added successfully";
                return RedirectToAction(nameof(AddBook));
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(bookViewModel);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(bookViewModel);

            }
            catch (Exception ex) 
            {
                TempData["errorMessage"] = "Error on Saving data";

                return View(bookViewModel);

            }

        }
        public async Task<IActionResult> UpdateBook(int id)
        {
            var book=await _bookRepository.GetBookById(id);
            if (book == null)
            {
                TempData["errorMessage"] = $"Book with the id: {id} dose not found";
                return RedirectToAction(nameof(Index));
            }
            var genreSelectedList = (await _genreRepository.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
                Selected=genre.Id==book.Id
            });
            BookViewModel bookView = new BookViewModel()
            {
                GenreList = genreSelectedList,
                BookName = book.BookName,
                AuthorName = book.AuthorName,
                GenreId = book.GenreId,
                Price = book.Price,
                Image=book.Images
            };
            return View(bookView);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBook(BookViewModel bookView)
        {
            var genreSelectedList = (await _genreRepository.GetGenres()).Select(genre => new SelectListItem
            {
                Text = genre.GenreName,
                Value = genre.Id.ToString(),
                Selected = genre.Id == bookView.Id
            });
            bookView.GenreList = genreSelectedList;
            if(!ModelState.IsValid)
                return View(bookView);

            try
            {
                string oldImage = "";
                if(bookView.ImageFile!= null)
                {
                    if (bookView.ImageFile.Length > 1 * 1024 * 1024)
                    {
                        throw new InvalidOperationException("Image file can not exceed 1 MB");
                    }

                    string[] allowedExtenstions = [".jpg", ".jpeg", "png"];
                    string imageName = await _fileService.SaveFile(bookView.ImageFile, allowedExtenstions);
                    oldImage =bookView.Image;
                    bookView.Image = imageName;
                }
                Book book = new Book()
                {
                    Id = bookView.Id,
                    BookName = bookView.BookName,
                    AuthorName = bookView.AuthorName,
                    Images = bookView.Image,
                    GenreId = bookView.GenreId,
                    Price = bookView.Price,
                };

                await _bookRepository.UpdateBook(book);

                if (!string.IsNullOrEmpty(oldImage))
                {
                    _fileService.DeleteFile(oldImage);
                }
                TempData["successMessage"] = "Book is Added successfully";
                return RedirectToAction(nameof(Index));



            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(bookView);
            }
            catch (FileNotFoundException ex)
            {
                TempData["errorMessage"] = ex.Message;
                return View(bookView);

            }
            catch (Exception ex)
            {
                TempData["errorMessage"] = "Error on Saving data";

                return View(bookView);

            }




        }

        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _bookRepository.GetBookById(id);

                if (book == null)
                {
                    TempData["errorMessage"] = $"Book with the id: {id} does not exist";
                }
                else
                {
                    await _bookRepository.DeleteBook(book); // 👈 ملاحظة تحت
                    if (!string.IsNullOrWhiteSpace(book.Images))
                        _fileService.DeleteFile(book.Images);

                    TempData["successMessage"] = "Book deleted successfully";
                }
            }
            catch (InvalidOperationException ex)
            {
                TempData["errorMessage"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

    }
}