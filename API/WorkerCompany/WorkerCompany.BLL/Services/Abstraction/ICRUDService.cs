using System.Collections.Generic;
using System.Threading.Tasks;
using WorkerCompany.BLL.Responses.ApiResponses;

namespace WorkerCompany.BLL.Services.Abstraction
{
    public interface ICRUDService<TEntity, TModel>
        where TEntity: class
        where TModel: class
    {
        Task<ApiResponse> ReadAllAsync();
        Task<ApiResponse> GetByIdAsync(object id);
        Task<ApiResponse> DeleteAsync(object id);
        Task<ApiResponse> UpdateAsync(object id, TModel @new);
    }
}
