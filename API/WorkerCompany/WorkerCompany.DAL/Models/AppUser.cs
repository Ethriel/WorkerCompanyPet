using Microsoft.AspNetCore.Identity;

namespace WorkerCompany.DAL.Models
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public int? WorkerId { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
