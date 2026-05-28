using System.Threading.Tasks;

namespace BookShoppingCartMvc.Ropository
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Book>> DisplayBooks(string sterm = "", int genreId = 0);
        Task<IEnumerable<Genre>> Genres();
    }
}