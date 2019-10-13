using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_Portfolio.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment_Portfolio.Controllers
{
    public class Bookings1Controller : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        // GET: Bookings1
        public ActionResult Index()
        {
            string userId = User.Identity.GetUserId();
            string userRole = "";
            if (User.IsInRole("Customer"))
            {
                userRole += "Customer";
            }
            else if (User.IsInRole("Vendor"))
            {
                userRole += "Vendor";
            }
            else if (User.IsInRole("Admin"))
            {
                userRole += "Admin";
            }

            ViewBag.roles = userRole;
            var bookings = db.Bookings.Include(b => b.Room);
            if (User.IsInRole("Customer"))
            {
                return View(db.Bookings.Where(u => u.UserId.Equals(userId))
                .ToList());
            }
            else
            {
                return View(bookings.ToList());
            }
        }

        public ActionResult Book()
        {
            int hotelId = 0;
            if (Request["hotelId"] != null)
            {
                hotelId = Int32.Parse(Request["hotelId"]);
            }

            ViewBag.Hotel = db.Database.SqlQuery<string>("Select HotelName from Hotels where id = {0}"
                , hotelId).FirstOrDefault<string>();
            ViewBag.RoomId = new SelectList(db.Rooms.Where(u => u.HotelId.Equals(hotelId))
                .ToList(), "Id", "RoomName", "Rate");
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Book([Bind(Include = "Id,Date,Value,RoomId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                if (userId != null)
                {
                    booking.UserId = userId;
                }

                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(booking);
        }

        // GET: Bookings1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings1/Create
        public ActionResult Create()
        {
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();
            return View();
        }

        // POST: Bookings1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Value,Review,Rating,RoomId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Bookings.Add(booking);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", booking.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", booking.UserId);
            return View(booking);
        }

        // GET: Bookings1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", booking.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", booking.UserId);
            return View(booking);
        }

        // POST: Bookings1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Value,Review,Rating,RoomId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", booking.RoomId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "UserName", booking.UserId);
            return View(booking);
        }

        // GET: Bookings1/Review/5
        public ActionResult Review(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            int tes = booking.RoomId;
            int hotelId = db.Database.SqlQuery<int>("Select distinct h.Id from Hotels h join Rooms r on h.Id = r.HotelId " +
                            "           where r.Id = {0}"
                            , booking.RoomId).FirstOrDefault<int>();

            ViewBag.Hotel = db.Database.SqlQuery<string>("Select HotelName from Hotels where id = {0}"
                , hotelId).FirstOrDefault<string>();
            ViewBag.RoomId = new SelectList(db.Rooms.Where(u => u.HotelId.Equals(hotelId))
                .ToList(), "Id", "RoomName", "Rate");
            ViewBag.UserId = User.Identity.GetUserId();
            ViewBag.UserName = User.Identity.GetUserName();

            return View(booking);
        }

        // POST: Bookings1/Review/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Review([Bind(Include = "Id,Value,Review,Rating,RoomId,UserId")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                string userId = User.Identity.GetUserId();
                booking.UserId = userId;
                db.Entry(booking).State = EntityState.Modified;
                try
                {
                    db.SaveChanges();
                }
                catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
                {
                    Exception raise = dbEx;
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            string message = string.Format("{0}:{1}",
                                validationErrors.Entry.Entity.ToString(),
                                validationError.ErrorMessage);
                            // raise a new exception nesting  
                            // the current instance as InnerException  
                            raise = new InvalidOperationException(message, raise);
                        }
                    }
                    throw raise;
                }

                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Bookings.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Bookings.Find(id);
            db.Bookings.Remove(booking);
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
