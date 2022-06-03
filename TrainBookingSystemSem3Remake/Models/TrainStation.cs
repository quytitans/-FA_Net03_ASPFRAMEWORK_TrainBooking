using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class TrainStation
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string DetailAddress { get; set; }
        public string City { get; set; }
        public int Status { get; set; }


        //public int TripFromId { get; set; }
        [InverseProperty("TrainStationFrom")]
        public virtual List<Trip> TripFrom { get; set; }

        //public int TripToId { get; set; }
        [InverseProperty("TrainStationTo")]
        public virtual List<Trip> TripTo { get; set; }
    }
}