using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TrainBookingSystemSem3Remake.Models
{
    public class TicketBooking
    {
        [Key]
        public int Id { get; set; }

        public DateTime BookingDate { get; set; }
        public double TotalPrice { get; set; }

        public string IdentityUserId { get; set; }
        [ForeignKey("IdentityUserId")]
        public virtual IdentityUser IdentityUsers { get; set; }
    }
}