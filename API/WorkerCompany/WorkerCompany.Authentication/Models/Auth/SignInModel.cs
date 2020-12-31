using System.ComponentModel.DataAnnotations;

namespace WorkerCompany.Authentication.Models.Auth
{
    public class SignInModel
    {
        [Required(ErrorMessage = "Username is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }
    }
}
