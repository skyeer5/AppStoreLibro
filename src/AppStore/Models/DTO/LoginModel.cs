
using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.DTO
{
    public class LoginModel
    {
        [Required]
        public string? username {get;set;}
        [Required]
        public string? password {get;set;}
    }
}