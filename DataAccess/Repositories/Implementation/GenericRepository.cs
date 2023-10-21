using DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementation
{
    public abstract class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly DbContext dbContext;
        public GenericRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public virtual Task<IEnumerable<T>> GetAllAsync(Func<T, bool> predicate)
        {
            return Task.FromResult(dbContext.Set<T>().Where(predicate));
        }

        public virtual async Task<IList<T>> GetAllAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool? disableTracking = null)
        {
            IQueryable<T> query = dbContext.Set<T>().AsQueryable();

            if (disableTracking == true)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes(query).IgnoreAutoIncludes();
            }

            return await query.Where(filter).ToListAsync();
        }
        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbContext.Set<T>().ToListAsync();
        }
        public virtual async Task<T> GetDetailsAsync(object Id)
        {
            return await dbContext.Set<T>().FindAsync(Id);
        }
        public virtual async Task<T> GetDetailsAsync(Expression<Func<T, bool>> predicate)
        {
            return await dbContext.Set<T>().FirstOrDefaultAsync(predicate);
        }
        public virtual async Task<T> GetAsync(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null, bool? disableTracking = null)
        {
            IQueryable<T> query = dbContext.Set<T>().AsQueryable();

            if (disableTracking == true)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes(query).IgnoreAutoIncludes();
            }

            T result = await query.Where(filter).FirstOrDefaultAsync();
            return result;
        }
        public virtual async Task AddAsync(T entity)
        {
            _ = await dbContext.Set<T>().AddAsync(entity);
            _ = await dbContext.SaveChangesAsync();
        }
        public virtual async Task UpdateAsync(T entity)
        {
            _ = dbContext.Set<T>().Update(entity);
            _ = await dbContext.SaveChangesAsync();
        }
        public virtual async Task DeleteAsync(Expression<Func<T, bool>> predicate)
        {
            T entity = await GetAsync(predicate);

            _ = dbContext.Set<T>().Remove(entity);
            _ = dbContext.SaveChanges();
        }
        public virtual async void Delete(T entity)
        {
            _ = dbContext.Set<T>().Remove(entity);
            _ = await dbContext.SaveChangesAsync();
        }
    }
}
