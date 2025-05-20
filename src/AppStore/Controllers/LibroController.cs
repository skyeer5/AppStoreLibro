using AppStore.Models.Domain;
using AppStore.Repositories.Absatract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _libroService;
        private readonly IFileService _fileService;
        private readonly ICategoriaService _categoriaService;

        public LibroController(ILibroService libroService, IFileService fileService, ICategoriaService categoriaService)
        {
            _libroService = libroService;
            _fileService = fileService;
            _categoriaService = categoriaService;
        }
        [HttpPost]
        public IActionResult Add(Libro libro)
        {
            libro.CategoriaList = _categoriaService.List()
                .Select(a=> new SelectListItem{Text = a.Nombre, Value = a.Id.ToString()});
            if(!ModelState.IsValid)
            {
                return View(libro);
            }
            
            if(libro.ImageFile != null)
            {
            var resultado =  _fileService.SaveImage(libro.ImageFile);
            if(resultado.Item1 == 0)
            {
                TempData["msg"] = "La imagen no pudo guardarse exitosamente";
                return View(libro);
            }
            var imagenName = resultado.Item2;
            libro.Imagen = imagenName;

            }
        
            var resultadoLibro = _libroService.Add(libro);
            if(resultadoLibro)
            {
                TempData["msg"] = "Se agrego el libro exitosamente";
                return RedirectToAction(nameof(Add));
            }
            TempData["msg"] = "Error al subir la imagen";
            return View(libro);
        }
        public IActionResult Add()
        {
            var libro = new Libro();
            libro.CategoriaList = _categoriaService.List().Select(a=> new SelectListItem{Text=a.Nombre, Value=a.Id.ToString()});
            return View(libro);
        }
        public IActionResult Edit(int id)
        {
            return View();
        }
        public IActionResult LibroList()
        {
            var libros = _libroService.List();
            return View(libros);
        }
        public IActionResult Delete(int id)
        {
            return RedirectToAction(nameof(LibroList));
        }
    }
}