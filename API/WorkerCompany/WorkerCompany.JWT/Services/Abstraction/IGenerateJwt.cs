using System.Threading.Tasks;
using WorkerCompany.Authentication.Models;

namespace WorkerCompany.JWT.Services.Abstraction
{
    public interface IGenerateJwt
    {
        Task<string> CreateToken(AppUser user);
    }
}
