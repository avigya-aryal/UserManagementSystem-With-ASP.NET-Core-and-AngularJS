using DomainLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer.Repository
{
    public interface IRepository<T> where T:BaseEntity
    {
        Task InsertAsync(T entity);

        Task<IEnumerable<T>> FindAllAsync();

        Task<IEnumerable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task SaveChangesAsync();

        //IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        
    }
}
