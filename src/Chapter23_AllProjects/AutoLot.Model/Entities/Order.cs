using System;
using System.Collections.Generic;

#nullable disable

namespace AutoLot.Model.Entities
{
    public partial class Order
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public int CustomerId { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual Inventory Car { get; set; }
        public virtual Customer Customer { get; set; }
    }
}
