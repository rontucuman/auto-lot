using AutoLot.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class CarEntityConfiguration:BaseEntityConfiguration<Car>
  {
    public override void Configure(EntityTypeBuilder<Car> builder)
    {
      base.Configure(builder);

      builder.ToTable("Inventory", "dbo");

      builder.HasIndex(e => e.MakeId, "IX_FK_Make_Inventory");

      builder.Property(e => e.Color)
        .IsRequired()
        .HasMaxLength(50)
        .IsUnicode(false);

      builder.Property(e => e.PetName)
        .IsRequired()
        .HasMaxLength(50)
        .IsUnicode(false);

      builder.HasOne(d => d.MakeNavigation)
        .WithMany(p => p.Cars)
        .HasForeignKey(d => d.MakeId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Make_Inventory");
    }
  }
}
