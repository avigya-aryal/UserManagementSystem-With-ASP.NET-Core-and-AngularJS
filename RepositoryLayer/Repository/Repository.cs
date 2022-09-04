using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public class Repository<T> : IRepository<T> where T :  BaseEntity
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private DbSet<T> entities;

        public Repository(ApplicationDbContext applicationDbContext)
        {
            try
            {
                _applicationDbContext = applicationDbContext;
                entities = _applicationDbContext.Set<T>();
            }
            catch(Exception err)
            {
                var a = err;
            }
            
        }

        public async Task InsertAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Add(entity);
            try
            {
                await _applicationDbContext.SaveChangesAsync();
            }
            catch(Exception err)
            {
                var a = err;
            }

            entities.AsNoTracking();
        }

        public async Task<IEnumerable<T>> FindAllAsync()
        {
            return await this.entities.ToListAsync();
        }

        public async Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression)
        {
            return await this.entities.AsNoTracking().Where(expression).ToListAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Update(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            entities.Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _applicationDbContext.SaveChangesAsync();
        }

        //public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        //{
        //    return this.entities.AsNoTracking().Where(expression);
        //    //return this.entities.Where(expression).AsNoTracking();
        //}
    }
}
