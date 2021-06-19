using AutoLot.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class OrderEntityConfiguration : BaseEntityConfiguration<Order>
  {
    public override void Configure(EntityTypeBuilder<Order> builder)
    {
      base.Configure(builder);
      
      builder.ToTable("Order");

      builder.HasIndex(e => new { e.CustomerId, e.CarId }, "IX_FK_Customer_Order");

      builder.HasIndex(e => e.CarId, "IX_FK_Inventory_Order");

      builder.HasQueryFilter(e => e.CarNavigation!.IsDrivable);

      builder.HasOne(d => d.CarNavigation)
        .WithMany(p => p!.Orders)
        .HasForeignKey(d => d.CarId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Inventory_Order");

      builder.HasQueryFilter(e => e.CarNavigation!.IsDrivable);

      builder.HasOne(d => d.CustomerNavigation)
        .WithMany(p => p!.Orders)
        .HasForeignKey(d => d.CustomerId)
        .OnDelete(DeleteBehavior.Cascade)
        .HasConstraintName("FK_Customer_Order");
    }
  }
}
