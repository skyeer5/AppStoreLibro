using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain
{
    public class LoadDatabase
    {
        public static async Task InsertarData(  DatabaseContext context
                                                ,UserManager<ApplicationUser> usuarioManager
                                                ,RoleManager<IdentityRole> roleManager)
        {
            if(!roleManager.Roles.Any())
            {
                await roleManager.CreateAsync(new IdentityRole("ADMIN"));
            }
            if(!usuarioManager.Users.Any())
            {
                var usuario = new ApplicationUser{
                    Nombre = "Daniel Pineda",
                    Email = "danielpineda430@gmail.com",
                    UserName = "Daniel.P"
                };
                await usuarioManager.CreateAsync(usuario, "PasswordVaxidrez123$");
                await usuarioManager.AddToRoleAsync(usuario,"ADMIN");
            }
            if(!context.Categorias!.Any())
            {
                await context.Categorias!.AddRangeAsync(
                    new Categoria {Nombre = "Drama"},
                    new Categoria {Nombre = "Comedia"},
                    new Categoria {Nombre = "Accion"},
                    new Categoria {Nombre = "Terror"},
                    new Categoria {Nombre = "Aventura"}
                );

                context.SaveChanges();
            }
            if(!context.Libros!.Any())
            {
                await context.Libros!.AddRangeAsync(
                    new Libro { Titulo = "El Quijote de la mancha", CreateDate = "06/06/2020", Autor = "Miguel de Cervantes", Imagen = "quijote.jpg"},
                    new Libro {Titulo = "Harry Potter", CreateDate = "06/01/2021", Autor = "Juan de la Vega", Imagen ="harry.jpg"}
                );
                context.SaveChanges();
            }
            if(!context.LibroCategorias!.Any())
            {
                await context.LibroCategorias!.AddRangeAsync(
                    new LibroCategoria{CategoriaId = 1, LibroId = 1},
                    new LibroCategoria{CategoriaId = 1, LibroId = 2}
                );
                context.SaveChanges();
            }
            context.SaveChanges();
        }
    }
}