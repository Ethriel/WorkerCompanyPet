using System.ComponentModel.DataAnnotations;

namespace WorkerCompany.Authentication.Models.Auth
{
    public class SignUpModel
    {
        [Required(ErrorMessage = "Email is required!")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Display name is required!")]
        public string DisplayName { get; set; }
        [Required(ErrorMessage = "Password is required!")]
        public string Password { get; set; }
        public int? WorkerId { get; set; }
    }
}
