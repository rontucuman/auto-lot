using AutoLot.Model.Entities;
using AutoLot.Model.Entities.Owned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class CustomerEntityConfiguration : BaseEntityConfiguration<Customer>
  {
    public override void Configure(EntityTypeBuilder<Customer> builder)
    {
      base.Configure(builder);

      builder.ToTable("Customer", "dbo");

      builder.OwnsOne(e => e.PersonalInformation, ba =>
      {
        ba.Property(e => e.FirstName)
          .HasColumnName(nameof(Person.FirstName))
          .IsRequired()
          .HasMaxLength(50)
          .IsUnicode(false);

        ba.Property(e => e.LastName)
          .HasColumnName(nameof(Person.LastName))
          .IsRequired()
          .HasMaxLength(50)
          .IsUnicode(false);

        ba.Property(e => e.FullName)
          .HasColumnName(nameof(Person.FullName))
          .HasComputedColumnSql("[LastName] + ', ' + [FirstName]")
          .HasMaxLength(150)
          .IsUnicode(false);
      });
    }
  }
}
