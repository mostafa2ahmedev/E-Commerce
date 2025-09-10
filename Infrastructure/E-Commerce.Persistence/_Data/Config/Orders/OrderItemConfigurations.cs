using E_Commerce.Domain.Entities.Orders;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence._Data.Config.Order
{
    internal class OrderItemConfigurations : BaseAuditableEntityConfigurations<OrderItem,int>
    {
        public override void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            base.Configure(builder);
            builder.OwnsOne(orderItem => orderItem.Product , orderItem => orderItem.WithOwner());
            builder.Property(orderItem => orderItem.Price).HasColumnType("decimal(8,2)");


        }
    }
}
