using AppStore.Models.Domain;
using AppStore.Repositories.Absatract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace AppStore.Controllers
{
    [Authorize]
    public class CategoriaController : Controller
    {
        private readonly ICategoriaService _categoriaService;

        public CategoriaController(ICategoriaService categoriaService)
        {
            _categoriaService = categoriaService;
        }
        [HttpPost]
        public IActionResult Add(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            var resultado = _categoriaService.Add(categoria);
            if (!resultado)
            {
                TempData["msg"] = "Error al subir la categoria";
                return View(categoria);
            }
            TempData["msg"] = "Se agrego la categoria con exito";
            return RedirectToAction(nameof(Add));
        }
        public IActionResult Add()
        {
            return View();
        }

        public IActionResult Update(int id)
        {
            var categoria = _categoriaService.GetCategoriaById(id);
            return View(categoria);
        }
        [HttpPost]
        public IActionResult Update(Categoria categoria)
        {
            if (!ModelState.IsValid)
            {
                return View(categoria);
            }
            var resultado = _categoriaService.Update(categoria);
            if (!resultado)
            {
                TempData["msg"] = "Error al subir la categoria";
                return View(categoria);
            }
            TempData["msg"] = "Se agrego la categoria con exito";
            return RedirectToAction(nameof(CategoriaList));
        }
        public IActionResult Delete(int id)
        {
            _categoriaService.Delete(id);
            return RedirectToAction(nameof(CategoriaList));
        }
        public IActionResult CategoriaList()
        {
            var categorias = _categoriaService.ListPaging();
            return View(categorias);
        }
    }
}