using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoLot.Model.Entities.Owned
{
  public class Person
  {
    public string FirstName { get; set; } = "New";
    public string LastName { get; set; } = "Customer";
    public string? FullName { get; set; }
  }
}
