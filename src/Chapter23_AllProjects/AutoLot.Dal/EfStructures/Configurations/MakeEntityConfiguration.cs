using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLot.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class MakeEntityConfiguration : BaseEntityConfiguration<Make>
  {
    public override void Configure(EntityTypeBuilder<Make> builder)
    {
      base.Configure(builder);

      builder.ToTable("Make", "dbo");

      builder.Property(e => e.Name)
        .IsRequired()
        .HasMaxLength(50)
        .IsUnicode(false);
    }
  }
}
