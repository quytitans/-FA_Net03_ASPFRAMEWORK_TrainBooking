using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class Train
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public int Status { get; set; }

        //public int TripId { get; set; }
        //[ForeignKey("TripId")]
        public virtual ICollection<Trip> Trips { get; set; }

        public int TrainTypeId { get; set; }
        [ForeignKey("TrainTypeId")]
        public virtual TrainType TrainType { get; set; }
    }
}