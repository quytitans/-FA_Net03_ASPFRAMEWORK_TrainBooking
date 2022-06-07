using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TrainBookingSystemSem3Remake.Data;
using TrainBookingSystemSem3Remake.Models;
using TrainBookingSystemSem3Remake.Models.ViewModel;
using TrainBookingSystemSem3Remake.VnPay;

namespace TrainBookingSystemSem3Remake.Controllers
{
    public class ClientController : Controller
    {
        private TrainContext db = new TrainContext();
        private UserManager<IdentityUser> userManager; //Bên database
        private RoleManager<IdentityRole> roleManager; //Bên database
        private List<Cart> listTickets;
        private static double total = 0.0;

        public ClientController()
        {
            db = new TrainContext();
            UserStore<IdentityUser> userStore = new UserStore<IdentityUser>(db); // create, update, delete giống UserModel
            userManager = new UserManager<IdentityUser>(userStore); // giống Service, xử lý các vấn đề liên quan đến logic
            RoleStore<IdentityRole> roleStore = new RoleStore<IdentityRole>(db); // create, update, delete giống UserModel
            roleManager = new RoleManager<IdentityRole>(roleStore); // giống Service, xử lý các vấn đề liên quan đến logic
            listTickets = new List<Cart>();
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
        [HttpGet]
        public JsonResult AddToCart(int id)
        {
            Cart cart = new Cart();
            var ticket = db.Tickets.FirstOrDefault(model => model.Id == id);
            if (Session["CartItems"] != null)
            {
                listTickets = Session["CartItems"] as List<Cart>;
            }
            if (listTickets.Any(model => model.Ticket.Id == id))
            {
                return Json("Vé này đã có trong giỏ hàng", JsonRequestBehavior.AllowGet);
            }
            else
            {
                cart.Quantity = 1;
                cart.Ticket = ticket;
                listTickets.Add(cart);
                listTickets.ForEach(s =>
                {
                    s.Ticket.Status = 2;
                    db.Tickets.AddOrUpdate(s.Ticket);
                });
                Session["CartItems"] = listTickets;
                return Json("Thêm vé vào giỏ hàng thành công", JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult ShoppingCart()
        {
            listTickets = Session["CartItems"] as List<Cart>;
            return View(listTickets);
        }
        //[EnableCors(origins: "*", headers: "*",
        //methods: "*", SupportsCredentials = true)]
        public ActionResult Payment()
        {
            listTickets = Session["CartItems"] as List<Cart>;
            
            string url = ConfigurationManager.AppSettings["vnp_Url"];
            string returnUrl = ConfigurationManager.AppSettings["vnp_Returnurl"];
            string tmnCode = ConfigurationManager.AppSettings["vnp_TmnCode"];
            string hashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"];
            foreach (var item in listTickets)
            {
                total += item.Ticket.Price;
            }
            PayLib pay = new PayLib();

            pay.AddRequestData("vnp_Version", "2.1.0"); //Phiên bản api mà merchant kết nối. Phiên bản hiện tại là 2.0.0
            pay.AddRequestData("vnp_Command", "pay"); //Mã API sử dụng, mã cho giao dịch thanh toán là 'pay'
            pay.AddRequestData("vnp_TmnCode", tmnCode); //Mã website của merchant trên hệ thống của VNPAY (khi đăng ký tài khoản sẽ có trong mail VNPAY gửi về)
            pay.AddRequestData("vnp_Amount", Convert.ToString(total*100)); //số tiền cần thanh toán, công thức: số tiền * 100 - ví dụ 10.000 (mười nghìn đồng) --> 1000000
            pay.AddRequestData("vnp_BankCode", ""); //Mã Ngân hàng thanh toán (tham khảo: https://sandbox.vnpayment.vn/apis/danh-sach-ngan-hang/), có thể để trống, người dùng có thể chọn trên cổng thanh toán VNPAY
            pay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss")); //ngày thanh toán theo định dạng yyyyMMddHHmmss
            pay.AddRequestData("vnp_CurrCode", "VND"); //Đơn vị tiền tệ sử dụng thanh toán. Hiện tại chỉ hỗ trợ VND
            pay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress()); //Địa chỉ IP của khách hàng thực hiện giao dịch
            pay.AddRequestData("vnp_Locale", "vn"); //Ngôn ngữ giao diện hiển thị - Tiếng Việt (vn), Tiếng Anh (en)
            pay.AddRequestData("vnp_OrderInfo", "Thanh toan ve tau"); //Thông tin mô tả nội dung thanh toán
            pay.AddRequestData("vnp_OrderType", "Thanh toan ve tau"); //topup: Nạp tiền điện thoại - billpayment: Thanh toán hóa đơn - fashion: Thời trang - other: Thanh toán trực tuyến
            pay.AddRequestData("vnp_ReturnUrl", returnUrl); //URL thông báo kết quả giao dịch khi Khách hàng kết thúc thanh toán
            pay.AddRequestData("vnp_TxnRef", DateTime.Now.Ticks.ToString()); //mã hóa đơn

            string paymentUrl = pay.CreateRequestUrl(url, hashSecret);

            return Redirect(paymentUrl);
        }

        public ActionResult PaymentConfirm()
        {
            string query = ConfigurationManager.AppSettings["querydr"];
            listTickets = Session["CartItems"] as List<Cart>;
            if (Request.QueryString.Count > 0)
            {
                string hashSecret = ConfigurationManager.AppSettings["vnp_HashSecret"]; //Chuỗi bí mật
                var vnpayData = Request.QueryString;
                PayLib pay = new PayLib();

                //lấy toàn bộ dữ liệu được trả về
                foreach (string s in vnpayData)
                {
                    if (!string.IsNullOrEmpty(s) && s.StartsWith("vnp_"))
                    {
                        pay.AddResponseData(s, vnpayData[s]);
                    }
                }

                long orderId = Convert.ToInt64(pay.GetResponseData("vnp_TxnRef")); //mã hóa đơn
                long vnpayTranId = Convert.ToInt64(pay.GetResponseData("vnp_TransactionNo")); //mã giao dịch tại hệ thống VNPAY
                string vnp_ResponseCode = pay.GetResponseData("vnp_ResponseCode"); //response code: 00 - thành công, khác 00 - xem thêm https://sandbox.vnpayment.vn/apis/docs/bang-ma-loi/
                string vnp_SecureHash = Request.QueryString["vnp_SecureHash"]; //hash của dữ liệu trả về

                bool checkSignature = pay.ValidateSignature(vnp_SecureHash, hashSecret); //check chữ ký đúng hay không?

                if (checkSignature)
                {
                    if (vnp_ResponseCode == "00")
                    {
                        var userId = Session["userId"] as string;
                        var ticketBooking = new TicketBooking()
                        {
                            BookingDate = DateTime.Now,
                            IdentityUserId = userId,
                            TotalPrice = total * 100
                        };
                        db.TicketBookings.AddOrUpdate(ticketBooking);
                        db.SaveChanges();
                        foreach (var item in listTickets)
                        {
                            item.Ticket.Status = 3;
                            db.Tickets.AddOrUpdate(item.Ticket);

                            var ticketBookingDetail = new TicketBookingDetail()
                            {
                                TicketId = item.Ticket.Id,
                                TicketBookingId = ticketBooking.Id,
                                UnitPrice = (int) item.Ticket.Price
                            };
                            db.TicketBookingDetails.AddOrUpdate(ticketBookingDetail);
                            db.SaveChanges();
                        }
                        
                        db.TicketBookings.AddOrUpdate(ticketBooking);
                        //Thanh toán thành công
                        Session.Remove("CartItems");
                        ViewBag.Message = "Thanh toán thành công hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId;
                        string paymentUrl = pay.CreateRequestUrl(query, hashSecret);
                    }
                    else
                    {
                        //Thanh toán không thành công. Mã lỗi: vnp_ResponseCode
                        ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý hóa đơn " + orderId + " | Mã giao dịch: " + vnpayTranId + " | Mã lỗi: " + vnp_ResponseCode;
                    }
                }
                else
                {
                    ViewBag.Message = "Có lỗi xảy ra trong quá trình xử lý";
                }
            }

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