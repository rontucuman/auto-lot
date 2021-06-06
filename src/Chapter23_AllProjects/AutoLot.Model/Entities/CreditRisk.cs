using System;
using System.Collections.Generic;

#nullable disable

namespace AutoLot.Model.Entities
{
    public partial class CreditRisk
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual Customer Customer { get; set; }
    }
}
