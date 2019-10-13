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
    public class HomeController : Controller
    {
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
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}