using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class TicketBookingDetail
    {
        [Key]
        [Column(Order = 0)]
        public int TicketBookingId { get; set; }
        [Key]
        [Column(Order = 1)]
        public int TicketId { get; set; }

        public int UnitPrice { get; set; }

        [ForeignKey("TicketBookingId")]
        public virtual TicketBooking TicketBooking { get; set; }
        [ForeignKey("TicketId")]
        public virtual Ticket Ticket { get; set; }
    }
}