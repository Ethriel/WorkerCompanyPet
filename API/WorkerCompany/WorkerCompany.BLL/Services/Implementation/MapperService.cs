using AutoMapper;
using System.Collections.Generic;
using WorkerCompany.BLL.Services.Abstraction;

namespace WorkerCompany.BLL.Services.Implementation
{
    public class MapperService<TEntity, TModel> : IMapperService<TEntity, TModel>
        where TEntity : class
        where TModel : class
    {
        private readonly IMapper mapper;

        public MapperService(IMapper mapper)
        {
            this.mapper = mapper;
        }
        public IEnumerable<TEntity> MapEntities(IEnumerable<TModel> models)
        {
            return mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);
        }

        public TEntity MapEntity(TModel model)
        {
            return mapper.Map<TModel, TEntity>(model);
        }

        public TModel MapModel(TEntity entity)
        {
            return mapper.Map<TEntity, TModel>(entity);
        }

        public IEnumerable<TModel> MapModels(IEnumerable<TEntity> entities)
        {
            return mapper.Map<IEnumerable<TEntity>, IEnumerable<TModel>>(entities);
        }
    }
}
