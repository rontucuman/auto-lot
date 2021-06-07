using System;
using System.Collections.Generic;
using AutoLot.Model.Entities.Base;

namespace AutoLot.Model.Entities
{
  public partial class Make : BaseEntity
  {
    public string Name { get; set; } = "Ford";

    public virtual IEnumerable<Car> Cars { get; set; } = new List<Car>();
  }
}
