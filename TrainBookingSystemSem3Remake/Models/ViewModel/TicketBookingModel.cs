using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models.ViewModel
{
    public class TicketBookingModel
    {
        public int OrderId { get; set; }
        public int TicketId { get; set; }
        public int UnitPrice { get; set; }
        public string TrainStationFrom { get; set; }
        public string TrainStationTo { get; set; }
        public string TrainCarriagesName { get; set; }
        public string Booking { get; set; }
        public DateTime BookingDate { get; set; }
    }
}