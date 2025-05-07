
using AppStore.Models.Domain;
using AppStore.Models.DTO;
namespace AppStore.Repositories.Absatract
{
    public interface ILibroService
    {
        bool Add(Libro libro);
        
        bool Update(Libro libro);
        Libro GetById(int Id);
        bool Delete(int id);
        LibroListVm List(string term="", bool paging = false, int currentPage = 0);
        List<int> GetCategoriaByLibroId(int libroId);
    }
}