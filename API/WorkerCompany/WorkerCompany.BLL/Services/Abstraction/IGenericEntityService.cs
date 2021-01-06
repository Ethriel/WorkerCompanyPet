using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace WorkerCompany.BLL.Services.Abstraction
{
    public interface IGenericEntityService<T>
        where T : class
    {
        IQueryable<T> ReadAll();
        void Add(T entity);
        IQueryable<T> GetEntitiesByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetEntityByConditionAsync(Expression<Func<T, bool>> expression);
        Task<T> GetByIdAsync(object id);
        void Delete(T entity);
        T UpdateAsync(T old, T @new);
        Task<int> CommitChangesAsync();
    }
}
