using Domain.Models;
using Domain.Models.Instructors;
using Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepositoryNoKey<TEntity> where TEntity : class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate);
    }
    public interface IGenericRepository<TEntity, TKey> where TEntity : ModelBase<TKey>
        
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        Task<TEntity> GetByIdAsync(TKey id);
       
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(ISpecifications<TEntity,TKey> spec);
        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity,TKey> spec);
    }
}
