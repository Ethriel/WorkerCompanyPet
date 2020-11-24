using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WorkerCompany.BLL.Helpers;
using WorkerCompany.BLL.Services.Abstraction;
using WorkerCompany.DAL.Models;

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

        public void Add(T entity)
        {
            set.Add(entity);
        }

        public async Task<int> CommitChangesAsync()
        {
            return await context.SaveChangesAsync();
        }

        public void Delete(T entity)
        {
            set.Remove(entity);
        }

        public async Task<T> GetByIdAsync(object id)
        {
            return await set.FindAsync(id);
        }

        public IQueryable<T> GetEntitiesByCondition(Expression<Func<T, bool>> expression)
        {
            return set.Where(expression);
        }

        public async Task<T> GetEntityByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await set.FirstOrDefaultAsync(expression);
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
