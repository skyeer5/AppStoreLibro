
using AppStore.Models.DTO;

namespace AppStore.Repositories.Absatract
{
    public interface IUserAuthenticationService
    {
        Task<Status>LoginAsync(LoginModel login);
        Task LogoutAsync();
    }
}