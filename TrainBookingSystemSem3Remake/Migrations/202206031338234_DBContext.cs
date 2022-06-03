namespace TrainBookingSystemSem3Remake.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DBContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketBookingDetails",
                c => new
                    {
                        TicketBookingId = c.Int(nullable: false),
                        TicketId = c.Int(nullable: false),
                        UnitPrice = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.TicketBookingId, t.TicketId })
                .ForeignKey("dbo.Tickets", t => t.TicketId, cascadeDelete: true)
                .ForeignKey("dbo.TicketBookings", t => t.TicketBookingId, cascadeDelete: true)
                .Index(t => t.TicketBookingId)
                .Index(t => t.TicketId);
            
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Row = c.Int(nullable: false),
                        Colunm = c.Int(nullable: false),
                        Price = c.Double(nullable: false),
                        BookingDate = c.DateTime(),
                        Status = c.Int(nullable: false),
                        TrainCarriagesId = c.Int(nullable: false),
                        TripId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainCarriages", t => t.TrainCarriagesId, cascadeDelete: true)
                .ForeignKey("dbo.Trips", t => t.TripId, cascadeDelete: true)
                .Index(t => t.TrainCarriagesId)
                .Index(t => t.TripId);
            
            CreateTable(
                "dbo.TrainCarriages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        NumRow = c.Int(nullable: false),
                        NumCol = c.Int(nullable: false),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trains", t => t.TrainId, cascadeDelete: true)
                .Index(t => t.TrainId);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Status = c.Int(nullable: false),
                        TrainTypeId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TrainTypes", t => t.TrainTypeId, cascadeDelete: true)
                .Index(t => t.TrainTypeId);
            
            CreateTable(
                "dbo.TrainTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Trips",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FromStationId = c.Int(nullable: false),
                        ToStationId = c.Int(nullable: false),
                        StartDateTime = c.DateTime(nullable: false),
                        EndDateTime = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        TrainId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Trains", t => t.TrainId)
                .ForeignKey("dbo.TrainStations", t => t.FromStationId)
                .ForeignKey("dbo.TrainStations", t => t.ToStationId)
                .Index(t => t.FromStationId)
                .Index(t => t.ToStationId)
                .Index(t => t.TrainId);
            
            CreateTable(
                "dbo.TrainStations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DetailAddress = c.String(),
                        City = c.String(),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.TicketBookings",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        TotalPrice = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketBookingDetails", "TicketBookingId", "dbo.TicketBookings");
            DropForeignKey("dbo.TicketBookingDetails", "TicketId", "dbo.Tickets");
            DropForeignKey("dbo.Tickets", "TripId", "dbo.Trips");
            DropForeignKey("dbo.Tickets", "TrainCarriagesId", "dbo.TrainCarriages");
            DropForeignKey("dbo.TrainCarriages", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Trips", "ToStationId", "dbo.TrainStations");
            DropForeignKey("dbo.Trips", "FromStationId", "dbo.TrainStations");
            DropForeignKey("dbo.Trips", "TrainId", "dbo.Trains");
            DropForeignKey("dbo.Trains", "TrainTypeId", "dbo.TrainTypes");
            DropIndex("dbo.Trips", new[] { "TrainId" });
            DropIndex("dbo.Trips", new[] { "ToStationId" });
            DropIndex("dbo.Trips", new[] { "FromStationId" });
            DropIndex("dbo.Trains", new[] { "TrainTypeId" });
            DropIndex("dbo.TrainCarriages", new[] { "TrainId" });
            DropIndex("dbo.Tickets", new[] { "TripId" });
            DropIndex("dbo.Tickets", new[] { "TrainCarriagesId" });
            DropIndex("dbo.TicketBookingDetails", new[] { "TicketId" });
            DropIndex("dbo.TicketBookingDetails", new[] { "TicketBookingId" });
            DropTable("dbo.TicketBookings");
            DropTable("dbo.TrainStations");
            DropTable("dbo.Trips");
            DropTable("dbo.TrainTypes");
            DropTable("dbo.Trains");
            DropTable("dbo.TrainCarriages");
            DropTable("dbo.Tickets");
            DropTable("dbo.TicketBookingDetails");
        }
    }
}
