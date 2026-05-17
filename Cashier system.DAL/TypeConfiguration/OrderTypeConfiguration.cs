using Cashier_system.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.TypeConfiguration;

public class OrderTypeConfiguration
    : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.TotalPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.Discount)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.FinalPrice)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.AppUser)
               .WithMany(x => x.Orders)
               .HasForeignKey(x => x.AppUserId);

        builder.HasMany(x => x.OrderItems)
               .WithOne(x => x.Order)
               .HasForeignKey(x => x.OrderId);
    }
}