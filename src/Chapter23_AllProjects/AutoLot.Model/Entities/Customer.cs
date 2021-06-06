using System.Collections.Generic;
using AutoLot.Model.Entities.Base;
using AutoLot.Model.Entities.Owned;

namespace AutoLot.Model.Entities
{
  public partial class Customer : BaseEntity
  {
    public Person PersonalInformation { get; set; } = new Person();

    public IEnumerable<CreditRisk> CreditRisks { get; set; } = new List<CreditRisk>();
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();
  }
}
