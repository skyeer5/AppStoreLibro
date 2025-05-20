
using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Absatract;


namespace AppStore.Repositories.Implementation
{
    public class CategoriaService : ICategoriaService
    {
        private readonly DatabaseContext ctx;
        public CategoriaService(DatabaseContext ctx)
        {
            this.ctx = ctx;
        }

        public IQueryable<Categoria> List()
        {
            return ctx.Categorias!.AsQueryable();
        }
        public bool Add(Categoria categoria)
        {
            try
            {
                ctx.Add(categoria);
                ctx.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Delete(int id)
        {
            try
            {
                var categoria = GetCategoriaById(id);
                if (categoria is null)
                {
                    return false;
                }
                ctx.Categorias!.Remove(categoria);
                var libroCategoriasParaEliminar = ctx.LibroCategorias!.Where(x => x.CategoriaId == id);
                ctx.RemoveRange(libroCategoriasParaEliminar);
                ctx.SaveChanges();

                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        public bool Update(Categoria categoria)
        {
            try
            {
                ctx.Categorias!.Update(categoria);
                ctx.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public Categoria GetCategoriaById(int id)
        {
            return ctx.Categorias!.Find(id)!;
        }

        public CategoriaListVm ListPaging(string term = "", bool paging = false, int currentPage = 0)
        {
            var data = new CategoriaListVm();
            var categorias = ctx.Categorias!.ToList();

            if (!String.IsNullOrEmpty(term))
            {
                term = term.ToLower();
                categorias = ctx.Categorias!.Where(x => x.Nombre!.ToLower().StartsWith(term)).ToList();
            }
            if (paging)
            {
                int pageSize = 5;
                int totalPages = (int)Math.Ceiling(categorias.Count / (float)pageSize);
                categorias = categorias.Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
                data.PageSize = pageSize;
                data.TotalPages = totalPages;
                data.CurrentPage = currentPage;
            }
            data.CategoriaList = categorias.AsQueryable();
            return data;
        }

        public List<string> LibrosCategoriaList(int id)
        {
            return(
                    from Libro in ctx.Libros
                    join lb in ctx.LibroCategorias!
                    on Libro.Id equals lb.LibroId
                    where lb.CategoriaId == id
                    select Libro.Titulo
                ).ToList();
        }

    }
}