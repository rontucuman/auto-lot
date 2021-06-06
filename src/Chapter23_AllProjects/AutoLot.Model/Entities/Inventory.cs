using System;
using System.Collections.Generic;

#nullable disable

namespace AutoLot.Model.Entities
{
    public partial class Inventory
    {
        public Inventory()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int MakeId { get; set; }
        public string Color { get; set; }
        public string PetName { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual Make Make { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
