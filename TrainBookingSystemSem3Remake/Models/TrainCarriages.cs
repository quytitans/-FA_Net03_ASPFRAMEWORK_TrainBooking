using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class TrainCarriages
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumRow { get; set; }
        public int NumCol { get; set; }

        //public int TicketId { get; set; }
        //[ForeignKey("TicketId")]
        //public virtual List<Ticket> Tickets { get; set; }

        public int TrainId { get; set; }
        [ForeignKey("TrainId")]
        public virtual Train Train { get; set; }
    }
}