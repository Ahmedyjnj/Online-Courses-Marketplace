using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Contracts
{
    public interface ISpecifications<TEntity,Tkey> where TEntity : ModelBase<Tkey>
    {
        // Filter condition
        public Expression<Func<TEntity, bool>>? Criteria { get; }



        // Eager loading includes
        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; }










        //// Sorting
        //Expression<Func<TEntity, object>>? OrderBy { get; }
        //Expression<Func<TEntity, object>>? OrderByDescending { get; }


        //// Pagination
        //int? Skip { get; }
        //int? Take { get; }
        //protected void AddOrderBy(Expression<Func<TEntity, object>> orderByExpression)
        //{
        //    OrderBy = orderByExpression;
        //}

        //protected void AddOrderByDescending(Expression<Func<TEntity, object>> orderByDescExpression)
        //{
        //    OrderByDescending = orderByDescExpression;
        //}

        //protected void ApplyPaging(int skip, int take)
        //{
        //    Skip = skip;
        //    Take = take;
        //}

        //protected void AsNoTracking()
        //{
        //    IsTracking = false;
        //}
    }
}
