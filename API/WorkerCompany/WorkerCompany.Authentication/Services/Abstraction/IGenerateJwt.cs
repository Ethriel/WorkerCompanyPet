using System.Threading.Tasks;
using WorkerCompany.Authentication.Models;

namespace WorkerCompany.Authentication.Services.Abstraction
{
    public interface IGenerateJwt
    {
        Task<string> CreateToken(AppUser user);
    }
}
