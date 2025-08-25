using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Domain.Entities.Products;
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
            if(typeof(TEntity) == typeof(Product))
                return withTracking ?
                  (IEnumerable <TEntity>) await _storeDbContext.Set<Product>().Include(P => P.Brand).Include(P=>P.Category).ToListAsync()
                 :
                  (IEnumerable <TEntity>) await _storeDbContext.Set<Product>().Include(P => P.Brand).Include(P => P.Category).AsNoTracking().ToListAsync();


            return withTracking ? 
                   await _storeDbContext.Set<TEntity>().ToListAsync()
                   :
                   await _storeDbContext.Set<TEntity>().AsNoTracking().ToListAsync();

            
        }

        public async Task<TEntity?> GetAsync(TKey id)
        {
            if (typeof(TEntity) == typeof(Product))
                return (TEntity) await _storeDbContext.Set<Product>().Where(P=>P.Id.Equals(id)).Include(P => P.Brand).Include(P => P.Category);
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
