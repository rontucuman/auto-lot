using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoLot.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AutoLot.Dal.EfStructures.Configurations
{
  public class CustomerOrderViewModelConfiguration : IEntityTypeConfiguration<CustomerOrderViewModel>
  {
    public void Configure(EntityTypeBuilder<CustomerOrderViewModel> builder)
    {
      builder.HasNoKey().ToView("CustomerOrderView", "dbo");
    }
  }
}
