using System;
using System.Collections.Generic;

#nullable disable

namespace AutoLot.Model.Entities
{
    public partial class Make
    {
        public Make()
        {
            Cars = new HashSet<Car>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] TimeStamp { get; set; }

        public virtual ICollection<Car> Cars { get; set; }
    }
}
