using System.Threading.Tasks;
using WorkerCompany.DAL.Models;

namespace WorkerCompany.Authentication.Services.Abstraction
{
    public interface IGenerateJwt
    {
        Task<string> CreateToken(AppUser user);
    }
}
