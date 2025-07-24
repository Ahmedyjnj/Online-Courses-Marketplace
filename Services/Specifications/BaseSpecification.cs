using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public abstract class BaseSpecification<TEntity, Tkey> : ISpecifications<TEntity, Tkey>
        where TEntity : ModelBase<Tkey>
    {
        public Expression<Func<TEntity, bool>>? Criteria {  get;   set; }

        public List<Expression<Func<TEntity, object>>> IncludeExpressions { get; private set; } = new();

        protected BaseSpecification()
        {
            
        }

        public BaseSpecification(Expression<Func<TEntity, bool>> PassedExpression)
        {
            Criteria = PassedExpression;

        }

        protected void AddInclude(Expression<Func<TEntity, object>> includeExpression)
        {
            IncludeExpressions.Add(includeExpression);
        }

      


    }
}
