using E_Commerce.Domain.Common;
using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Persistence._Data.Config;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence._Data.Config.Order
{
    internal class DeliveryMethodConfigurations :BaseEntityConfiguration<DeliveryMethod,int>
    {

        public override void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            base.Configure(builder);

            builder.Property(D => D.Cost).HasColumnType("decimal(8,2)");
        }
    }
}
