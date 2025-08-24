using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.GenericRepository
{
    internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
        where TKey : IEquatable<TKey> {
        private readonly StoreDbContext _storeDbContext;

        public GenericRepository(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
        }
        public async Task<IEnumerable<TEntity>> GetAllAsync(bool withTracking = false)
        {
   
           return  withTracking? 
                   await _storeDbContext.Set<TEntity>().ToListAsync()
                   :
                   await _storeDbContext.Set<TEntity>().AsNoTracking().ToListAsync();

            
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
          return await  _storeDbContext.Set<TEntity>().FindAsync(id);
        }

        public async Task AddAsync(TEntity entity)
        {
        await _storeDbContext.Set<TEntity>().AddAsync(entity);
        }

        public void Update(TEntity entity)
        {
              _storeDbContext.Set<TEntity>().Update(entity);
        }

        public void Delete(TEntity entity)
        {
             _storeDbContext.Set<TEntity>().Remove(entity);
        }

      
     
    }
}
