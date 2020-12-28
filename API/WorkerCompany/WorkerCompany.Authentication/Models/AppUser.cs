using Microsoft.AspNetCore.Identity;

namespace WorkerCompany.Authentication.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
