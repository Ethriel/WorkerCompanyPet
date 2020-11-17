using System.Linq;
using System.Threading.Tasks;

namespace WorkerCompany.BLL.Services.Abstraction
{
    public interface IGenericEntityService<T> where T : class
    {
        IQueryable<T> ReadAll();
        Task<T> GetByIdAsync(object id);
        Task DeleteAsync(object id);
        T UpdateAsync(T old, T @new);
        Task<int> CommitChangesAsync();
    }
}
