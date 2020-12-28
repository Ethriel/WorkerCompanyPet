using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace WorkerCompany.Authentication.Models
{
    public class AppIdentityContext : IdentityDbContext<AppUser, IdentityRole, string>
    {
        public AppIdentityContext(DbContextOptions<AppIdentityContext> options) : base(options)
        {

        }
    }
}
