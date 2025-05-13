
using AppStore.Models.Domain;

namespace AppStore.Repositories.Absatract
{
    public interface ICategoriaService
    {
        IQueryable<Categoria> List();
    }
}