using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLot.Model.Entities;
using AutoLot.Model.Entities.Owned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class CreditRiskEntityConfiguration : BaseEntityConfiguration<CreditRisk>
  {
    public override void Configure(EntityTypeBuilder<CreditRisk> builder)
    {
      base.Configure(builder);

      builder.ToTable("CreditRisk");

      builder.HasIndex(e => e.CustomerId, "IX_FK_Customer_CreditRisk");

      builder.OwnsOne(e => e.PersonalInformation, ba =>
      {
        ba.Property<string>(nameof(Person.FirstName))
          .HasColumnName(nameof(Person.FirstName))
          .HasMaxLength(50)
          .IsUnicode(false)
          .IsRequired();

        ba.Property<string>(nameof(Person.LastName))
          .HasColumnName(nameof(Person.LastName))
          .HasMaxLength(50)
          .IsRequired()
          .IsUnicode(false);

        ba.Property<string>(p => p.FullName)
          .HasColumnName(nameof(Person.FullName))
          .HasComputedColumnSql("[LastName]+ ', ' + [FirstName]")
          .HasMaxLength(150);
      });

      builder.HasOne(d => d.CustomerNavigation)
        .WithMany(p => p!.CreditRisks)
        .HasForeignKey(d => d.CustomerId)
        .OnDelete(DeleteBehavior.ClientSetNull)
        .HasConstraintName("FK_Customer_CreditRisk");
    }
  }
}
