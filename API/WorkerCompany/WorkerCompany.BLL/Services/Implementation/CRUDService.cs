using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using WorkerCompany.BLL.Responses.ApiResponses;
using WorkerCompany.BLL.Services.Abstraction;

namespace WorkerCompany.BLL.Services.Implementation
{
    public class CRUDService<TEntity, TModel> : ICRUDService<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        private readonly IGenericEntityService<TEntity> entityService;
        private readonly IMapperService<TEntity, TModel> mapperService;

        public CRUDService(IGenericEntityService<TEntity> entityService, IMapperService<TEntity, TModel> mapperService)
        {
            this.entityService = entityService;
            this.mapperService = mapperService;
        }

        public async Task<ApiResponse> AddAsync(TModel model)
        {
            var entity = mapperService.MapEntity(model);
            entityService.Add(entity);
            await entityService.CommitChangesAsync();

            var response = ApiResponse.GetOkResponse(message: "Item was added");
            return response;
        }

        public async Task<ApiResponse> DeleteAsync(object id)
        {
            var response = default(ApiResponse);

            var entity = await entityService.GetByIdAsync(id);

            if (entity == null)
            {
                var message = "Delete error";
                var loggerMessage = $"Entity id = {id} was not found";
                var errors = new string[] { "Item was not found" };
                response = ApiResponse.GetErrorResponse(ApiResultStatus.NotFound, loggerMessage, message, errors);
            }
            else
            {
                entityService.Delete(entity);
                await entityService.CommitChangesAsync();
                response = ApiResponse.GetOkResponse(message: "entity was deleted");
            }

            return response;
        }

        public async Task<ApiResponse> GetByIdAsync(object id)
        {
            var response = default(ApiResponse);

            var entity = await entityService.GetByIdAsync(id);

            if (entity == null)
            {
                var message = "Get error";
                var loggerMessage = $"Entity id = {id} was not found";
                var errors = new string[] { "Item was not found" };
                response = ApiResponse.GetErrorResponse(ApiResultStatus.NotFound, loggerMessage, message, errors);
            }
            else
            {
                var data = mapperService.MapModel(entity);
                response = ApiResponse.GetOkResponse(message: "Returning item", data);
            }

            return response;
        }

        public async Task<ApiResponse> ReadAllAsync()
        {
            var entities = await entityService.ReadAll()
                                              .ToArrayAsync();

            var data = mapperService.MapModels(entities);
            var response = ApiResponse.GetOkResponse("Returning items", data);
            return response;
        }

        public async Task<ApiResponse> UpdateAsync(object id, TModel @new)
        {
            var response = default(ApiResponse);

            var old = await entityService.GetByIdAsync(id);

            if (old == null)
            {
                var message = "Update error";
                var loggerMessage = $"Entity id = {id} was not found";
                var errors = new string[] { "Item was not found" };
                response = ApiResponse.GetErrorResponse(ApiResultStatus.NotFound, loggerMessage, message, errors);
            }
            else
            {
                var entity = mapperService.MapEntity(@new);
                old = entityService.UpdateAsync(old, entity);
                await entityService.CommitChangesAsync();
                var data = mapperService.MapModel(old);

                response = ApiResponse.GetOkResponse("Item was updated", data);
            }

            return response;
        }
    }
}
