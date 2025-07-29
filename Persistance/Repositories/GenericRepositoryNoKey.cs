using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Persistance.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Repositories
{
    class GenericRepositoryNoKey<TEntity>(AppDbContext context) : IGenericRepositoryNoKey<TEntity>
  where TEntity : class
    {


            public async Task<IEnumerable<TEntity>> GetAllAsync()
            {
                return await context.Set<TEntity>().ToListAsync();
            }




            public async Task AddAsync(TEntity entity)
            {
                await context.Set<TEntity>().AddAsync(entity);

            }

            public  Task UpdateAsync(TEntity entity)
            {
                context.Set<TEntity>().Update(entity);
            return Task.CompletedTask;
        }

            public  Task DeleteAsync(TEntity entity)
            {
                context.Set<TEntity>().Remove(entity);
                return Task.CompletedTask;
            }

            public async Task<IEnumerable<TEntity>> GetWhereAsync(Expression<Func<TEntity, bool>> predicate)
            {
                return await context.Set<TEntity>().Where(predicate).ToListAsync();
            }

    }
}

