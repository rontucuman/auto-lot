using AutoLot.Model.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
  {
    public virtual void Configure(EntityTypeBuilder<TEntity> builder)
    {
      builder.HasKey(e => e.Id);

      builder.Property(e => e.TimeStamp)
        .IsRowVersion()
        .IsConcurrencyToken();
    }
  }
}
