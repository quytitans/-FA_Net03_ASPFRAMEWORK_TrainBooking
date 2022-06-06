using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainBookingSystemSem3Remake.Data;
using TrainBookingSystemSem3Remake.Models;
using TrainBookingSystemSem3Remake.Models.ViewModel;

namespace TrainBookingSystemSem3Remake.Controllers
{
    public class ClientController : Controller
    {
        private TrainContext db = new TrainContext();
        private UserManager<IdentityUser> userManager; //Bên database
        private RoleManager<IdentityRole> roleManager; //Bên database

        public ClientController()
        {
            db = new TrainContext();
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(db); // create, update, delete giống UserModel
            userManager = new UserManager<IdentityUser>(userStore); // giống Service, xử lý các vấn đề liên quan đến logic
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db); // create, update, delete giống UserModel
            roleManager = new RoleManager<IdentityRole>(roleStore); // giống Service, xử lý các vấn đề liên quan đến logic
        }
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

        public async Task<ActionResult> AddRole(string RoleName)
        {
            IdentityRole role = new IdentityRole()
            {
                Name = RoleName
            };
            var result = await roleManager.CreateAsync(role);
            if (result.Succeeded)
            {
                return View("ViewSuccess");
            }
            else
            {
                return View("ViewError");
            }
        }

        public async Task<bool> AddUserToRoleAsync(string UserId, string RoleName)
        {
            var user = db.Users.Find(UserId);
            var role = db.Roles.AsQueryable().Where(roleFind => roleFind.Name.Contains(RoleName)).FirstOrDefault();
            if (user == null || role == null)
            {
                return false;
            }
            var result = await userManager.AddToRoleAsync(user.Id, role.Name);
            //string roleName1 = "Admin";
            //string roleName2 = "User";
            ////var result = await userManager.AddToRoleAsync(userId, roleName);
            //var result = await userManager.AddToRolesAsync(userId, roleName1, roleName2); // Thêm nhiều Role cho 1 User
            if (result.Succeeded)
            {
                return true;
            }
            else
            {
                ViewBag.Errors = result.Errors;
                System.Diagnostics.Debug.WriteLine("Lỗi tạo quyền có lỗi là ", result.Errors);
                return false;
            }
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(string Username, string Password)
        {
            IdentityUser user = new IdentityUser()
            {
                UserName = Username
            };

            var result = await userManager.CreateAsync(user, Password);
            if (result.Succeeded)
            {
                var queryUser = db.Users.AsQueryable().Where(userFind => userFind.UserName.Contains(Username)).FirstOrDefault();
                Debug.WriteLine("Tìm user có name là: ", Username);
                Debug.WriteLine("Tạo quyền User cho user có id là: ", queryUser.Id);
                if (queryUser == null)
                {
                    ViewBag.ErrorNull = "Không tìm thấy khi queryUser";
                    Debug.WriteLine("Tạo quyền User cho user có id là: ", queryUser.Id);
                    return View("ViewError");
                }
                var check = await AddUserToRoleAsync(queryUser.Id, "User");
                if (check)
                {
                    return View("ViewSuccess");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Lỗi tạo quyền");
                    return View("ViewError");
                }
            }
            else
            {
                ViewBag.Errors = result.Errors;
                System.Diagnostics.Debug.WriteLine("Lỗi đăng ký là ", result.Errors);
                return View("ViewError");
            }
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(string UserName, string Password)
        {
            var user = await userManager.FindAsync(UserName, Password);
            Debug.WriteLine("user đăng nhập là ", user);
            if (user == null)
            {
                ViewBag.Errors = new string[] { "Không tìm thấy user" };
                return View("ViewError");
            }
            else
            {
                SignInManager<IdentityUser, string> signInManager = new SignInManager<IdentityUser, string>(userManager, Request.GetOwinContext().Authentication);
                await signInManager.SignInAsync(user, false, false);
                Session["userId"] = user.Id;

                return Redirect("/Client/HomePage");
            }
        }

        public ActionResult Logout()
        {
            HttpContext.GetOwinContext().Authentication.SignOut();
            return Redirect("/Client/HomePage");
        }


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

            int pageSize = 4;
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

        [Authorize(Roles = "User,Admin")]
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

                //var userId = Session["userId"];
                //var ticketBooking = new TicketBooking()
                //{

                //}
                

                return Json("OK id la: " + id);
            
        }
        
    }
}