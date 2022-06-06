using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class Cart
    {
        public int Quantity { get; set; }
        public Ticket Ticket { get; set; }
    }
}