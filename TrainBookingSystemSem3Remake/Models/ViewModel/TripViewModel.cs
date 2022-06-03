using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models.ViewModel
{
    public class TripViewModel
    {
        public int Id { get; set; }
        public int FromStationId { get; set; }
        public int ToStationId { get; set; }
        [DataType(DataType.Date)]
        public DateTime StartDateTime { get; set; } // Thời gian từ ngày nào đến ngày nào
        [DataType(DataType.Date)]
        public DateTime EndDateTime { get; set; } // Thời gian từ ngày nào đến ngày nào

        public string StartTime { get; set; } // VD: 7:00
        public string EndTime { get; set; } // VD: 14:00
        public double Price { get; set; } // giá vé
        public int Status { get; set; }

        public int TrainTypeId { get; set; }

        public int TicketId { get; set; }
    }
}