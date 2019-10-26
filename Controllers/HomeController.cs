using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using FIT5032_Assignment_Portfolio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace FIT5032_Assignment_Portfolio.Controllers
{
    [RequireHttps]
    public class HomeController : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        ApplicationDbContext context;
        public HomeController()
        {
            context = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            string userName = User.Identity.GetUserName();
            ViewBag.Message = userName;

            string userRole = "";
            if (User.IsInRole("Customer"))
            {
                userRole += "Customer";
            } else if (User.IsInRole("Vendor"))
            {
                userRole += "Vendor";
            } else if (User.IsInRole("Admin"))
            {
                userRole += "Admin";
            }

            ViewBag.roles = userRole;

           // int[] bookCount = { 1, 2, 3, 4, 5 };

            string query = "select count(b.Id) from Hotels h left join Rooms r on h.Id=r.HotelId " +
               " left join Bookings b on r.Id = b.RoomId " +
               " group by h.HotelName ";
            var bookCount = db.Database.SqlQuery<int>(query).ToArray();
            ViewBag.bc = bookCount;

            query = "select h.HotelName from Hotels h left join Rooms r on h.Id=r.HotelId " +
                " left join Bookings b on r.Id = b.RoomId " +
                " group by h.HotelName ";
            var hotelName = db.Database.SqlQuery<string>(query).ToArray();

            //string[] testt = {"aa","bb","cc" };
            ViewBag.name = hotelName;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "About GoBook";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Feel free to contact us.";

            return View();
        }
    }
}