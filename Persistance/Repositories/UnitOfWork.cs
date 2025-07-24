using Domain.Contracts;
using Domain.Models;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    public class UnitOfWork(AppDbContext context) : IUnitOfWork
    {
        private readonly Dictionary<string, object> _Repositories = new Dictionary<string, object>();


        public IGenericRepository<TEntity,Tkey> GetRepository<TEntity, Tkey>() where TEntity : ModelBase<Tkey>
        {
            var TypeName = typeof(TEntity).Name;

            if (_Repositories.ContainsKey(TypeName))
                return (IGenericRepository<TEntity, Tkey>)_Repositories[TypeName];

            var Repo = new GenericRepository<TEntity, Tkey>(context);
            _Repositories.Add(TypeName, Repo);
            return Repo;
        }

        public IGenericRepositoryNoKey<TEntity> GetRepositoryWithNoid<TEntity>() where TEntity:class
        {
            var typeName = typeof(TEntity).Name;
            if (_Repositories.ContainsKey(typeName))
                return (IGenericRepositoryNoKey<TEntity>)_Repositories[typeName];

            var repo = new GenericRepositoryNoKey<TEntity>(context);

            _Repositories.Add(typeName, repo);

            return repo;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await context.SaveChangesAsync();
        }
    }

}
