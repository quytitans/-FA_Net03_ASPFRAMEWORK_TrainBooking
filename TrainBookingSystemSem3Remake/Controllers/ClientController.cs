using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TrainBookingSystemSem3Remake.Data;
using TrainBookingSystemSem3Remake.Models.ViewModel;

namespace TrainBookingSystemSem3Remake.Controllers
{
    public class ClientController : Controller
    {
        private TrainContext db = new TrainContext();
        // GET: Client
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult HomePage()
        {
            var ListTrainStation = db.TrainStations.ToList();
            ViewBag.ListTrainStation = ListTrainStation;
            return View();
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult SearchTrips(int fromStation, int toStation , DateTime startDate, int? page)
        //{
        //    var listTrip = db.Trips.OrderBy(s => s.Id).AsQueryable().Where(s => s.FromStationId == fromStation && s.ToStationId == toStation);

        //    if (startDate != null)
        //    {
        //        var startDateTime0000 = startDate;
        //        var startDateTime2359 = startDate.AddDays(1).AddTicks(-1);
        //        listTrip = listTrip.Where(s => s.StartDateTime >= startDateTime0000 && s.StartDateTime <= startDateTime2359);
        //    }

        //    int pageSize = 3;
        //    int pageNumber = (page ?? 1);
        //    ViewBag.startDate = startDate;
        //    ViewBag.fromStation = fromStation;
        //    ViewBag.toStation = toStation;
        //    return View("ListTripsSelected",listTrip.ToPagedList(pageNumber, pageSize));
        //}

        public ActionResult SearchTrips(int? fromStation, int? toStation, String startDate, int? page)
        {
            var trips = db.Trips.OrderBy(s => s.Id).AsQueryable();

            if (fromStation != null && fromStation != -1)
            {
                trips = trips.Where(s => s.FromStationId == fromStation);
            }

            if (toStation != null && toStation != -1)
            {
                trips = trips.Where(s => s.ToStationId == toStation);
            }

            if (startDate != null && startDate != "")
            {
                //var startDateTime0000 = startDate;
                //var startDateTime2359 = startDate.AddDays(1).AddTicks(-1);
                //trips = trips.Where(s => s.StartDateTime >= startDateTime0000 && s.StartDateTime <= startDateTime2359);

                var startDateTime0000 = DateTime.Parse(startDate);
                var startDateTime2359 = DateTime.Parse(startDate).AddDays(1).AddTicks(-1);
                trips = trips.Where(s => s.StartDateTime >= startDateTime0000 && s.StartDateTime <= startDateTime2359);
            }

            ViewBag.TrainStations = db.TrainStations.ToList();
            ViewBag.toStationId = toStation;
            ViewBag.fromStationId = fromStation;
            ViewBag.startDate = startDate;

            int pageSize = 2;
            int pageNumber = (page ?? 1);
            return View("ListTripsSelected", trips.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult GetDetailTrip(int id)
        {
            var trip = db.Trips.Find(id);
            var listTrainCarriages = db.TrainCarriages.Where(s => s.TrainId == trip.TrainId).ToList();
            ViewBag.listTrainCarriages = listTrainCarriages;
            var TicketByTrip = db.Tickets.Where(s => s.TripId == trip.Id);

            var listTicketByTrip = new List<ListTicketByTrainCarriages>();
            foreach (var item in listTrainCarriages)
            {
                var listTicketByTrainCarriages = new ListTicketByTrainCarriages()
                {
                    TrainCarriagesId = item.Id,
                    TrainCarriagesName = item.Name,
                    Tickets = TicketByTrip.Where(s => s.TrainCarriagesId == item.Id).ToList()
                };
                listTicketByTrip.Add(listTicketByTrainCarriages);
            }
            ViewBag.listTicketByTrip = listTicketByTrip;
            //for (int i = 1; i <= listTrainCarriages.Count; i++)
            //{
            //    int TrainCarriagesId = listTrainCarriages[i].Id;
            //    ViewBag.Tickets[i] = db.Tickets.Where(s => s.TrainCarriagesId == TrainCarriagesId).ToList();
            //}

            return View();
        }

        [HttpPost]
        public JsonResult ChangeStatusTicket(int id)
        {
            var ticket = db.Tickets.Find(id);

            switch (ticket.Status)
            {
                case 1:
                    ticket.Status = 2;
                    ticket.BookingDate = DateTime.Now;
                    break;
                case 2:
                    ticket.Status = 1;
                    ticket.BookingDate = DateTime.Now;
                    break;
            }

            db.Tickets.AddOrUpdate(ticket);
            db.SaveChanges();
            return Json("OK id la: " + id);
        }
        
    }
}