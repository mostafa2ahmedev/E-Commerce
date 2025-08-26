using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.GenericRepository
{
    internal static class SpecificationEvaluator <TEntity,TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>

    {

        public static IQueryable<TEntity> GetQuery(IQueryable<TEntity> inputQuery , ISpecifications<TEntity,TKey> spec) {

            var query = inputQuery;

            // Set Criteria
            if (spec.Criteria is not null)
               query =  query.Where(spec.Criteria);

            // Order By
            if (spec.OrderByDesc is not null)
                query = query.OrderByDescending(spec.OrderByDesc);
            else
                query = query.OrderBy(spec.OrderBy!);



            if(spec.IsPaginationEnabled)
                query = query.Skip(spec.Skip).Take(spec.Take);
                query = spec.Includes.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}
