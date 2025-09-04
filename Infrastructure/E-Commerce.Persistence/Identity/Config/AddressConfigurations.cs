using E_Commerce.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce.Persistence.Identity.Config
{
    internal class AddressConfigurations : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Addresses");

            builder.Property(nameof(Address.Id)).ValueGeneratedOnAdd();
            builder.Property(nameof(Address.FirstName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.LastName)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.Street)).HasColumnType("nvarchar").HasMaxLength(50);
            builder.Property(nameof(Address.City)).HasColumnType("varchar").HasMaxLength(50);
            builder.Property(nameof(Address.Country)).HasColumnType("varchar").HasMaxLength(50);
        }
    }
}
