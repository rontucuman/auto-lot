using System.Collections.Generic;
using AutoLot.Model.Entities.Base;

namespace AutoLot.Model.Entities
{
  public partial class Car : BaseEntity
  {
    private bool? _isDrivable;

    public int MakeId { get; set; }
    public string Color { get; set; } = "Gold";
    public string PetName { get; set; } = "My Precious";

    public string MakeName => MakeNavigation?.Name ?? "Unknown";

    public bool IsDrivable
    {
      get => _isDrivable ?? false;
      set => _isDrivable = value;
    }

    public Make? MakeNavigation { get; set; }
    public IEnumerable<Order> Orders { get; set; } = new List<Order>();

    public override string ToString()
    {
      return $"{PetName ?? "**No Name**"} is a {Color} {MakeNavigation?.Name} with Id {Id}";
    }
  }
}
