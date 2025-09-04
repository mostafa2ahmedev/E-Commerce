using E_Commerce.Domain.Entities.Identity;
using E_Commerce.Persistence.Identity.Config;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Identity
{
    public class StoreIdentityDbContext : IdentityDbContext<ApplicationUser>
    {


    
        public StoreIdentityDbContext(DbContextOptions<StoreIdentityDbContext> dbContextOptions)  :base(dbContextOptions)
        {
            
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            builder.ApplyConfiguration(new ApplicationUserConfigurations());
            builder.ApplyConfiguration(new AddressConfigurations());

            builder.ApplyConfigurationsFromAssembly(typeof(AssemblyInformation).Assembly,type=>
            type.Namespace!.Contains( "E_Commerce.Persistence.Identity.Config")
            );
        }
    }
}
