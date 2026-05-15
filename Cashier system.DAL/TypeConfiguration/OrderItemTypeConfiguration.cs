using Cashier_system.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cashier_system.DAL.TypeConfiguration;
public class OrderItemTypeConfiguration
    : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UnitPrice)
               .HasColumnType("decimal(18,2)");

        builder.Property(x => x.TotalPrice)
               .HasColumnType("decimal(18,2)");

        builder.HasOne(x => x.Order)
               .WithMany(x => x.OrderItems)
               .HasForeignKey(x => x.OrderId);

        builder.HasOne(x => x.Product)
               .WithMany()
               .HasForeignKey(x => x.ProductId);
    }
}