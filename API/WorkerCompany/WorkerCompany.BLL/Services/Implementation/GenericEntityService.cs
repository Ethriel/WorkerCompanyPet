using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WorkerCompany.BLL.Helpers;
using WorkerCompany.BLL.Services.Abstraction;

namespace WorkerCompany.BLL.Services.Implementation
{
    public class GenericEntityService<T> : IGenericEntityService<T> where T : class
    {
        private readonly DbContext context;
        private readonly DbSet<T> set;

        public GenericEntityService(DbContext context)
        {
            this.context = context;
            set = context.Set<T>();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(object id)
        {
            var entity = await GetByIdAsync(id);
            set.Remove(entity);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await set.FindAsync(id);
        }

        public IQueryable<T> ReadAll()
        {
            return set.AsQueryable();
        }

        public T UpdateAsync(T old, T @new)
        {
            old = UpdateEntity.Update<T>(context.Model, old, @new);
            return old;
        }
    }
}
