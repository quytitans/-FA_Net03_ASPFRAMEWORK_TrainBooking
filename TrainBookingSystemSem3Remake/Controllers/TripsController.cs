using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainBookingSystemSem3Remake.Data;
using TrainBookingSystemSem3Remake.Models;
using TrainBookingSystemSem3Remake.Models.ViewModel;

namespace TrainBookingSystemSem3Remake.Controllers
{
    public class TripsController : Controller
    {
        private TrainContext db;
        
        private UserManager<IdentityUser> userManager; //Bên database
        private RoleManager<IdentityRole> roleManager; //Bên database

        public ActionResult DashBoard()
        {
            ViewBag.thang01 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                 (p, e) => new
                 {
                     Id = p.TicketBookingId,
                     Id2 = p.TicketId,
                     Date = e.BookingDate
                 }).Count(s => s.Date.Year == 2022 && s.Date.Month == 1); ;
            ViewBag.thang02 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 2); ;
            ViewBag.thang03 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 3); ;
            ViewBag.thang04 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 4); ;
            ViewBag.thang05 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 5); ;
            ViewBag.thang06 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 6); ;
            ViewBag.thang07 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 7); ;
            ViewBag.thang08 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 8); ;
            ViewBag.thang09 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 9); ;
            ViewBag.thang10 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 10); ;
            ViewBag.thang11 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 11); ;
            ViewBag.thang12 = db.TicketBookingDetails.Join(db.TicketBookings, p => p.TicketBookingId, e => e.Id,
                (p, e) => new
                {
                    Id = p.TicketBookingId,
                    Id2 = p.TicketId,
                    Date = e.BookingDate
                }).Count(s => s.Date.Year == 2022 && s.Date.Month == 12); ;
            return View();
        }

        //Get All ticket
        public ActionResult GetAllTicket(int? page)
        {
            int pageSize = 10;
            int pageNumber = (page ?? 1);
            var tickets = db.Tickets.ToList();
            return View(tickets.ToPagedList(pageNumber, pageSize));
        }

        public TripsController()
        {
            db = new TrainContext();
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(db); // create, update, delete giống UserModel
            userManager = new UserManager<IdentityUser>(userStore); // giống Service, xử lý các vấn đề liên quan đến logic
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db); // create, update, delete giống UserModel
            roleManager = new RoleManager<IdentityRole>(roleStore); // giống Service, xử lý các vấn đề liên quan đến logic
        }

        [Authorize(Roles = "Admin")]
        // GET: Trips
        public ActionResult Index(int? fromStationId, int? toStationId, int? status, string startDate, string endDate, int? page)
        {
            var trips = db.Trips.Include(t => t.Train).Include(t => t.TrainStationFrom)
                .Include(t => t.TrainStationTo).OrderBy(s => s.TrainId).AsQueryable();

            if (fromStationId != null && fromStationId != -1)
            {
                trips = trips.Where(s => s.FromStationId == fromStationId);
            }

            if (toStationId != null && toStationId != -1)
            {
                trips = trips.Where(s => s.ToStationId == toStationId);
            }

            if (status != null && status != -2)
            {
                trips = trips.Where(s => s.Status == status);
            }

            if (startDate != null && startDate != "")
            {
                var startDateTime0000 = DateTime.Parse(startDate);
                var startDateTime2359 = DateTime.Parse(startDate).AddDays(1).AddTicks(-1);
                trips = trips.Where(s => s.StartDateTime >= startDateTime0000 && s.StartDateTime <= startDateTime2359);
            }

            if (endDate != null && endDate != "")
            {
                var endDateTime = DateTime.Parse(endDate);
                var startDateTime = DateTime.Parse(startDate);
                if (endDate == startDate)
                {

                }
                else
                {
                    trips = trips.Where(s => s.StartDateTime >= startDateTime && s.StartDateTime <= endDateTime);
                }
            }

            ViewBag.TrainStations = db.TrainStations.ToList();
            ViewBag.toStationId = toStationId;
            ViewBag.fromStationId = fromStationId;
            ViewBag.status = status;
            ViewBag.startDate = startDate;
            ViewBag.endDate = startDate;

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            return View(trips.ToPagedList(pageNumber, pageSize));
        }

        [Authorize(Roles = "Admin")]
        // GET: Trips/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.DetailTrip = trip;
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

            return View();
        }

        [Authorize(Roles = "Admin")]
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

        [Authorize(Roles = "Admin")]
        // GET: Trips/Create
        public ActionResult Create()
        {
            ViewBag.TrainTypeId = new SelectList(db.TrainTypes, "Id", "Name");
            ViewBag.FromStationId = new SelectList(db.TrainStations, "Id", "Name");
            ViewBag.ToStationId = new SelectList(db.TrainStations, "Id", "Name");
            return View();
        }

        /// <summary>
        ///     Tạo chuyến đi, tự động tạo vé
        /// </summary>
        /*
         1. Từ Form của view model, tạo các Trip trong 1 tuần theo các khung giờ định sẵn
            VD: 7h tối bắt đầu
            + Mỗi ngày tạo trip theo các khung giờ khác nhau
            + tạo vòng lặp các ngày trong tuần
            + lồng vòng lặp thứ nhất, chạy các khung giờ trong ngày
         2. Sau khi tạo mới được 1 Trip thì generate ra Ticket
         */

        // POST: Trips/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FromStationId,ToStationId,StartTime,EndTime,Price,StartDateTime,EndDateTime,Status,TrainTypeId,TicketId")] TripViewModel trip)
        {
            if (ModelState.IsValid)
            {
                var FromStationId = trip.FromStationId;
                var ToStationId = trip.ToStationId;

                var scheduleDay = trip.EndDateTime.Subtract(trip.StartDateTime).Days + 1;

                for (int i = 0; i < scheduleDay; i++)
                {
                    // Tạo Trip ở đây
                    // Save Trip vào database
                    // Tạo vé theo Trip


                    string formatDateStart = trip.StartDateTime.AddDays(i).ToShortDateString() + " " + trip.StartTime;
                    string formatDateEnd = trip.StartDateTime.AddDays(i).ToShortDateString() + " " + trip.EndTime;

                    DateTime myDateStart = DateTime.Parse(formatDateStart);
                    DateTime myDateEnd = DateTime.Parse(formatDateEnd);

                    var Train = db.Trains.Where(s => s.TrainTypeId == trip.TrainTypeId).ToList();
                    var random = new Random();
                    int TrainIndex = random.Next(0, Train.Count);

                    var generateTrip = new Trip()
                    {
                        Id = trip.Id,
                        FromStationId = FromStationId,
                        ToStationId = ToStationId,
                        StartDateTime = myDateStart,
                        EndDateTime = myDateEnd,
                        Status = trip.Status,
                        TrainId = Train[TrainIndex].Id,
                    };

                    db.Trips.Add(generateTrip);
                    db.SaveChanges();

                    int idGenerateTrip = generateTrip.Id;

                    //var trainCarriages = new TrainCarriages()
                    //{
                    //    NumRow = 3,
                    //    NumCol = 3,
                    //    TrainId = trip.TrainId
                    //};
                    //db.TrainCarriages.Add(trainCarriages);
                    //db.SaveChanges();

                    // Tìm kiếm Toa theo Id tàu
                    //int idTrainCarriages = trainCarriages.Id;
                    int TrainID = Train[TrainIndex].Id;
                    var findTrainCarriages = db.TrainCarriages.Where(s => s.TrainId == TrainID).ToList();

                    foreach (var trainCarriages in findTrainCarriages)
                    {
                        for (int j = 1; j < trainCarriages.NumRow + 1; j++)
                        {
                            for (int k = 1; k < trainCarriages.NumCol + 1; k++)
                            {
                                var Ticket = new Ticket()
                                {
                                    Row = j,
                                    Colunm = k,
                                    Price = trip.Price,
                                    Status = 1,
                                    TrainCarriagesId = trainCarriages.Id,
                                    TripId = idGenerateTrip
                                };
                                db.Tickets.Add(Ticket);
                                db.SaveChanges();

                                //int idTicket = Ticket.Id;

                                //generateTrip.TicketId = idTicket;
                                //db.Trips.AddOrUpdate(generateTrip);

                                //trainCarriages.TicketId = idTicket;
                                //db.TrainCarriages.AddOrUpdate(trainCarriages);
                                //db.SaveChanges();
                            }
                        }
                    }

                }

                return RedirectToAction("Index");
            }

            //ViewBag.TrainId = new SelectList(db.Trains, "Id", "Name", trip.TrainId);
            ViewBag.TrainTypeId = new SelectList(db.TrainTypes, "Id", "Name");
            ViewBag.FromStationId = new SelectList(db.TrainStations, "Id", "Name", trip.FromStationId);
            ViewBag.ToStationId = new SelectList(db.TrainStations, "Id", "Name", trip.ToStationId);
            return View(trip);
        }

        [Authorize(Roles = "Admin")]
        // GET: Trips/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            ViewBag.TrainId = new SelectList(db.Trains, "Id", "Name", trip.TrainId);
            ViewBag.FromStationId = new SelectList(db.TrainStations, "Id", "Name", trip.FromStationId);
            ViewBag.ToStationId = new SelectList(db.TrainStations, "Id", "Name", trip.ToStationId);
            return View(trip);
        }

        // POST: Trips/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FromStationId,ToStationId,StartDateTime,EndDateTime,Status,TrainId")] Trip trip)
        {
            if (ModelState.IsValid)
            {
                db.Entry(trip).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TrainId = new SelectList(db.Trains, "Id", "Name", trip.TrainId);
            ViewBag.FromStationId = new SelectList(db.TrainStations, "Id", "Name", trip.FromStationId);
            ViewBag.ToStationId = new SelectList(db.TrainStations, "Id", "Name", trip.ToStationId);
            return View(trip);
        }

        [Authorize(Roles = "Admin")]
        // GET: Trips/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        // POST: Trips/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Trip trip = db.Trips.Find(id);
            db.Trips.Remove(trip);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
