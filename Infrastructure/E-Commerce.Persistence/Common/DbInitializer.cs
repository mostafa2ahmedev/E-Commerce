using E_Commerce.Domain.Contracts.Persistence.DbInitializer;
using E_Commerce.Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Common
{
    internal abstract class DbInitializer : IDbInitializer
    {
        private readonly DbContext _dbContext;

        protected DbInitializer(DbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public virtual async Task InitializeAsync()
        {
            var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
            if (pendingMigrations.Any())
                await _dbContext.Database.MigrateAsync();

        }

        public abstract Task SeedAsync();
    
    }
}
