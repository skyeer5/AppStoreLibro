
using AppStore.Models.DTO;
using AppStore.Repositories.Absatract;
using AppStore.Models.Domain;
using Microsoft.AspNetCore.Identity;


namespace AppStore.Repositories.Implementation
{
    public class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly SignInManager<ApplicationUser> signInManager;

        public UserAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public async Task<Status> LoginAsync(LoginModel login)
        {
            var status = new Status();
            var user = await userManager.FindByNameAsync(login.username!);

            if(user is null)
            {
                status.Message = "Usuario no encontrado, intente de nuevo";
                status.StatusCode = 0;
                return status;
            }

            if(!await userManager.CheckPasswordAsync(user, login.password!))
            {
                status.Message = "Contraseña incorrecta, intente de nuevo";
                status.StatusCode = 0;
                return status;
            }
            var resultado = await signInManager.PasswordSignInAsync(user,login.password!,true, false);
            if(!resultado.Succeeded)
            {
                status.Message = "Error al iniciar sesión";
                status.StatusCode = 0;
                return status;
            }

            status.Message = "Fue exitoso el inicio de sesión";
            status.StatusCode = 1;
            return status;
        }

        public async Task LogoutAsync()
        {
            
            await signInManager.SignOutAsync();
        }
    }
}