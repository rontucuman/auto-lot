using System;
using System.Collections.Generic;
using AutoLot.Model.Entities.Base;

namespace AutoLot.Model.Entities
{
    public partial class Order : BaseEntity
    {
        public int CarId { get; set; }
        public int CustomerId { get; set; }

        public Car? CarNavigation { get; set; }
        public Customer? CustomerNavigation { get; set; }
    }
}
