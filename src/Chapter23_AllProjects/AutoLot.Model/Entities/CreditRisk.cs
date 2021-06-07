using System;
using System.Collections.Generic;
using AutoLot.Model.Entities.Base;
using AutoLot.Model.Entities.Owned;

namespace AutoLot.Model.Entities
{
  public partial class CreditRisk : BaseEntity
  {
    public Person PersonalInformation { get; set; } = new Person();
    public int CustomerId { get; set; }

    public Customer? CustomerNavigation { get; set; }
  }
}
