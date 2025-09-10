using E_Commerce.Application.Services;
using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence._Data.Interceptor
{
    internal class AuditInterceptor : SaveChangesInterceptor
    {
        private readonly ILoggedInUserService _loggedInUserService;

        public AuditInterceptor(ILoggedInUserService loggedInUserService)
        {
            _loggedInUserService = loggedInUserService;
        }

        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChanges(eventData, result);
        }
        public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {
            UpdateEntities(eventData.Context);

            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private void UpdateEntities(DbContext? dbContext) {
            foreach (var entry in dbContext!.ChangeTracker.Entries<IBaseAuditableEntity>()
                .Where(entry => entry.State is EntityState.Added or EntityState.Modified))
            {
                //if (entry.Entity is Order or OrderItem)
                //    _loggedInUserService.UserId = "";

                if (entry.State is EntityState.Added)
                {

                    entry.Entity.CreatedOn = DateTime.UtcNow;
                    entry.Entity.CreatedBy = _loggedInUserService?.UserId!;
                }
                entry.Entity.LastModifiedOn = DateTime.UtcNow;
                entry.Entity.LastModifiedBy = _loggedInUserService?.UserId!;
            }

        }
    }
}
