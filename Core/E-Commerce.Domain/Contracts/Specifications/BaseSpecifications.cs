using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Specifications
{
    public abstract class BaseSpecifications<TEntity, TKey>: ISpecifications<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {
        public Expression<Func<TEntity,bool>>? Criteria { get; set; } = null!;
        public List<Expression<Func<TEntity, object>>> Includes { get; set; } = new List<Expression<Func<TEntity, object>>>();
        public Expression<Func<TEntity, object>>? OrderBy { get; set; } = null!;
        public Expression<Func<TEntity, object>>? OrderByDesc { get ; set; } = null!;
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 0;
        public bool IsPaginationEnabled { get; set; } = false;

        public BaseSpecifications(Expression<Func<TEntity, bool>> CriteriaExpression)
        {
            Criteria = CriteriaExpression;


        }
        public BaseSpecifications(TKey key)
        {
            Criteria = E => E.Id.Equals(key);
    
        }

        private protected void AddOrderBy(Expression<Func<TEntity, object>>? orderByExpression) {
            OrderBy = orderByExpression;
        }
        private protected void AddOrderByDesc(Expression<Func<TEntity, object>>? orderByExpressionDesc)
        {
            OrderByDesc = orderByExpressionDesc;
        }
        private protected virtual void AddIncludes()
        {
          
        }

        private protected void ApplyPagination(int skip,int take) {
        
            IsPaginationEnabled = true;
            Skip = skip;
            Take = take;
        }
    }
}
