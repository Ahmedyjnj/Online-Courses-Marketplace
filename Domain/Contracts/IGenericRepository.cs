using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IGenericRepository<TEntity,Tkey> where TEntity : ModelBase<Tkey>
    {
        Task<IEnumerable<TEntity>> GetAllAsync();

        Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec);

        Task<TEntity> GetByIdAsync(Tkey id);

        Task<TEntity> GetByIdAsync(ISpecifications<TEntity, Tkey> spec);


        Task AddAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);


        Task DeleteAsync(TEntity entity);

    }
    public interface IGenericRepository<TEntity>
        where TEntity:class
    {
        Task AddAsync(TEntity entity);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
        
    }
}
