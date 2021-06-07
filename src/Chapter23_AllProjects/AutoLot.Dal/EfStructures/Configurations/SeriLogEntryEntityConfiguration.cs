using System;
using AutoLot.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class SeriLogEntryEntityConfiguration:IEntityTypeConfiguration<SeriLogEntry>
  {
    public void Configure(EntityTypeBuilder<SeriLogEntry> builder)
    {
      builder.HasKey(e => e.Id);

      builder.Property(e => e.Level)
        .HasMaxLength(128);

      builder.Property(e => e.Properties)
        .HasColumnType("Xml");

      builder.Property(e => e.TimeStamp)
        .HasColumnType(nameof(DateTime))
        .HasDefaultValueSql("GetDate()");

      builder.Ignore(e => e.PropertiesXml);
    }
  }
}
