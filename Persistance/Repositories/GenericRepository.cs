using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    class GenericRepository<TEntity, Tkey>(AppDbContext context) : IGenericRepository<TEntity, Tkey>
    where TEntity : ModelBase<Tkey>
    {


        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await context.Set<TEntity>().ToListAsync();
        }

        public async Task<TEntity> GetByIdAsync(Tkey id)
        {
            return await context.Set<TEntity>().FindAsync(id);
        }



        public async Task<IEnumerable<TEntity>> GetAllAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), spec).ToListAsync();
        }



        public async Task<TEntity> GetByIdAsync(ISpecifications<TEntity, Tkey> spec)
        {
            return await SpecificationEvaluator.CreateQuery(context.Set<TEntity>(), spec).FirstOrDefaultAsync();
        }

        public async Task AddAsync(TEntity entity)
        {
           await context.Set<TEntity>().AddAsync(entity);
           
        }

        public async Task UpdateAsync(TEntity entity)
        {
            context.Set<TEntity>().Update(entity);
          
        }

        public  async Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
           
        }




    }
    class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity>where TEntity:class

    {
        public async Task AddAsync(TEntity entity)
        {
          await  context.Set<TEntity>().AddAsync(entity);
            
        }

        public async Task UpdateAsync(TEntity entity)
        {
             context.Set<TEntity>().Update(entity);
           
        }

        public async Task DeleteAsync(TEntity entity)
        {
            context.Set<TEntity>().Remove(entity);
           
        }
    }
}
