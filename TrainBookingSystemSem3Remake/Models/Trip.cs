using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class Trip
    {
        [Key]
        [DisplayName("No")]
        public int Id { get; set; }
        [DisplayName("From ... Station")]
        public int FromStationId { get; set; }
        [DisplayName("To ... Station")]
        public int ToStationId { get; set; }
        [DisplayName("Start Date in schedule")]
        public DateTime StartDateTime { get; set; } // thời gian bắt đầu, VD: 28/05/2022 7:00
        [DisplayName("End Date in schedule")]
        public DateTime EndDateTime { get; set; } // thời gian kết thúc, VD: 29/05/2022 6:00
        [DisplayName("Status")]
        public int Status { get; set; }

        [ForeignKey("FromStationId")]
        public virtual TrainStation TrainStationFrom { get; set; }

        [ForeignKey("ToStationId")]
        public virtual TrainStation TrainStationTo { get; set; }

        public int TrainId { get; set; }
        [ForeignKey("TrainId")]
        public virtual Train Train { get; set; }

        //public int? TicketId { get; set; }
        //[ForeignKey("TicketId")]
        //public virtual List<Ticket> Ticket { get; set; }

        public Trip()
        {

        }
    }
}