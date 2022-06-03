using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class Ticket
    {
        [Key]
        public int Id { get; set; }
        public int Row { get; set; }
        public int Colunm { get; set; }
        public double Price { get; set; }
        public DateTime? BookingDate { get; set; }
        public int Status { get; set; }

        public int TrainCarriagesId { get; set; }
        [ForeignKey("TrainCarriagesId")]
        public virtual TrainCarriages TrainCarriages { get; set; }

        public int TripId { get; set; }
        [ForeignKey("TripId")]
        public virtual Trip Trip { get; set; }
    }
}