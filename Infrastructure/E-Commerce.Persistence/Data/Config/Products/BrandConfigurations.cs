using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Config.Products
{

    internal class BrandConfigurations : BaseEntityConfiguration<ProductBrand, int>
    {

        public override void Configure(EntityTypeBuilder<ProductBrand> builder)
        {
            base.Configure(builder);

            builder.Property(B => B.Name).IsRequired();
        }
    }
}
