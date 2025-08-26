using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Domain.Contracts.Persistence
{
    public interface IGenericRepository<TEntity,TKey> 
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey>
    {

        Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false);
        Task<IEnumerable<TEntity>> GetAllAsyncWithSpec(ISpecifications<TEntity, TKey> spec, bool withTracking = false);
        Task<TEntity?> GetAsync(TKey id);

        Task<TEntity?> GetAsyncWithSpec(ISpecifications<TEntity, TKey> spec);
        Task AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);

        Task<int> GetCountAsync(ISpecifications<TEntity, TKey> spec);



    }
}
