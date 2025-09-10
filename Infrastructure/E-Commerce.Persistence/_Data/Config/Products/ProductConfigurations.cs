﻿using E_Commerce.Domain.Entities.Products;
using E_Commerce.Persistence._Data.Config;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence._Data.Config.Products
{
    internal class ProductConfigurations : BaseAuditableEntityConfigurations<Product,int>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);


            builder.Property(P => P.Name).IsRequired().HasMaxLength(100);
            builder.Property(P => P.NormalizedName).IsRequired().HasMaxLength(100);
            builder.Property(P => P.Description).IsRequired();
            builder.Property(P => P.Price).HasColumnType("decimal(9,2)");


            builder.HasOne(P=>P.Brand).WithMany().HasForeignKey(P=>P.BrandId).OnDelete(DeleteBehavior.SetNull);
            builder.HasOne(P => P.Category).WithMany().HasForeignKey(P => P.CategoryId).OnDelete(DeleteBehavior.SetNull);
        }
    }
}
