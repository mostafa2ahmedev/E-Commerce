using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Persistence.Data;
using E_Commerce.Persistence.Data.GenericRepository;
using System.Collections.Concurrent;

namespace E_Commerce.Persistence.Data.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly StoreDbContext _storeDbContext;

        public readonly ConcurrentDictionary<string ,object> _repositories;
    

  
        public UnitOfWork(StoreDbContext storeDbContext)
        {
            _storeDbContext = storeDbContext;
            _repositories = new ConcurrentDictionary<string ,object>();

        }
        public IGenericRepository<TEntity, TKey> GetRepository<TEntity, TKey>()
            where TEntity : BaseEntity<TKey>
            where TKey : IEquatable<TKey>
        {
            //var typeName = typeof(TEntity).Name;

            //if (_repositories.ContainsKey(typeName))
            //    return (IGenericRepository< TEntity, TKey>) _repositories[typeName];

            //var repository = new GenericRepository<TEntity, TKey>(_storeDbContext);
            //_repositories.Add(typeName,repository);
            //return repository;

            return (IGenericRepository<TEntity, TKey>)_repositories.GetOrAdd(typeof(TEntity).Name, new GenericRepository<TEntity, TKey>(_storeDbContext));

        }
        public Task<int> CompleteAsync()
        {
            return _storeDbContext.SaveChangesAsync();

        }

        public async ValueTask DisposeAsync()
        {
         await  _storeDbContext.DisposeAsync();
        }

     
    }
}
