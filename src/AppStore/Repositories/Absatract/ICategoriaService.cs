
using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Absatract
{
    public interface ICategoriaService
    {
        IQueryable<Categoria> List();
        bool Add(Categoria categoria);
        bool Delete(int id);
        bool Update(Categoria categoria);
        Categoria GetCategoriaById(int id);
        CategoriaListVm ListPaging(string term = "", bool paging = false, int currentPage = 0);
        List<string> LibrosCategoriaList(int id);
    }
}