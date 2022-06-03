using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TrainBookingSystemSem3Remake.Models;

namespace TrainBookingSystemSem3Remake.Data
{
    public class TrainContext : DbContext
    {
        public TrainContext() : base("name=TrainDB")
        {
        }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketBooking> TicketBookings { get; set; }
        public DbSet<TicketBookingDetail> TicketBookingDetails { get; set; }
        public DbSet<Train> Trains { get; set; }
        public DbSet<TrainCarriages> TrainCarriages { get; set; }
        public DbSet<TrainStation> TrainStations { get; set; }
        public DbSet<TrainType> TrainTypes { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // configures one-to-many relationship
            modelBuilder.Entity<Trip>()
                .HasRequired<Train>(s => s.Train)
                .WithMany(g => g.Trips)
                .HasForeignKey<int>(s => s.TrainId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                 .HasRequired<TrainStation>(s => s.TrainStationFrom)
                 .WithMany(g => g.TripFrom)
                 .HasForeignKey<int>(s => s.FromStationId).WillCascadeOnDelete(false);

            modelBuilder.Entity<Trip>()
                 .HasRequired<TrainStation>(s => s.TrainStationTo)
                 .WithMany(g => g.TripTo)
                 .HasForeignKey<int>(s => s.ToStationId).WillCascadeOnDelete(false);
        }
    }
}