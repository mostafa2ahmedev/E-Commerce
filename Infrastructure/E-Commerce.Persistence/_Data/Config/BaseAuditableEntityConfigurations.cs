using E_Commerce.Domain.Common;
using E_Commerce.Persistence.Data.Config;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence._Data.Config
{
    internal class BaseAuditableEntityConfigurations<TEntity,TKey> : BaseEntityConfiguration<TEntity, TKey>
           where TEntity : BaseAuditableEntity<TKey> where TKey : IEquatable<TKey>
    {

        public override void Configure(EntityTypeBuilder<TEntity> builder)
        {
            base.Configure(builder);

            builder.Property(E => E.CreatedBy)
                .IsRequired();
            //.HasDefaultValueSql("GetUTCDate");
            builder.Property(E => E.CreatedOn)
             .IsRequired();

            builder.Property(E => E.LastModifiedBy)
             .IsRequired(); 

            builder.Property(E => E.LastModifiedOn)
             .IsRequired();

        }
    }
}
