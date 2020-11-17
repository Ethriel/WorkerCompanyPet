using System.Collections.Generic;

namespace WorkerCompany.BLL.Services.Abstraction
{
    public interface IMapperService<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        public TEntity MapEntity(TModel model);
        public TModel MapModel(TEntity entity);
        public IEnumerable<TEntity> MapEntities(IEnumerable<TModel> models);
        public IEnumerable<TModel> MapModels(IEnumerable<TEntity> entities);
    }
}
