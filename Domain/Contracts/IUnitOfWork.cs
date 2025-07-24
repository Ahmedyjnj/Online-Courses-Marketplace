using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface IUnitOfWork
    {
        // we will handle any opject you need to make 


        IGenericRepository<TEntity, Tkey> GetRepository<TEntity, Tkey>()
           where TEntity :ModelBase<Tkey>;


        IGenericRepositoryNoKey<TEntity> GetRepositoryWithNoid<TEntity>()
            where TEntity : class;


        Task<int> SaveChangesAsync();

    }
}
