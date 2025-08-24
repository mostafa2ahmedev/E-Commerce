using E_Commerce.Domain.Entities.Products;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Data.Config.Products
{
    internal class CategoryConfigurations :BaseEntityConfiguration<ProductCategory,int>
    {

        public override void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            base.Configure(builder);

            builder.Property(C => C.Name).IsRequired();
        }
    }
}
