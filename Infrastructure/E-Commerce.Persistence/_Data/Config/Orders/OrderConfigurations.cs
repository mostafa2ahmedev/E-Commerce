using E_Commerce.Domain.Entities.Orders;
using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace E_Commerce.Persistence._Data.Config.Orders
{
    internal class OrderConfigurations : BaseAuditableEntityConfigurations<Domain.Entities.Orders.Order, int>
    {

        public override void Configure(EntityTypeBuilder<Domain.Entities.Orders.Order> builder)
        {
            base.Configure(builder);  

            builder.OwnsOne(O => O.ShippingAddress, shippingAddress => shippingAddress.WithOwner());

            builder.Property(O => O.Status).HasConversion(
               Status => Status.ToString(),
               returnStatus =>(OrderStatus) Enum.Parse(typeof(OrderStatus), returnStatus)
                );

            builder.Property(O => O.SubTotal).HasColumnType("decimal(8,2)");

            builder.HasOne(O => O.DeliveryMethod).WithMany()
                .HasForeignKey(O=>O.DeliveryMethodId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(O => O.Items).WithOne()
               .OnDelete(DeleteBehavior.Cascade);
          }
    }
}
