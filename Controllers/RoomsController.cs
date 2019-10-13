using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_Portfolio.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment_Portfolio.Controllers
{
    public class RoomsController : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        // GET: Rooms
        public ActionResult Index()
        {
            string query = "select * from Rooms r join Hotels h on r.HotelId = h.Id " +
                "               where h.UserId=@p0";

            string userId = User.Identity.GetUserId();
            IEnumerable<Room> data = db.Database.SqlQuery<Room>(query, userId);
            return View(data.ToList());

            // var rooms = db.Rooms.Include(r => r.Hotel);
            // return View(rooms.ToList());
        }

        // GET: Rooms/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // GET: Rooms/Create
        public ActionResult Create()
        {
            string query = "select * from Hotels where UserId=@p0";

            string userId = User.Identity.GetUserId();
            IEnumerable<Hotel> data = db.Database.SqlQuery<Hotel>(query, userId);
            
            ViewBag.HotelId = new SelectList(data.ToList(), "Id", "HotelName");
            return View();
        }

        // POST: Rooms/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,RoomName,Description,Availability,Rate,HotelId")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Rooms.Add(room);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", room.HotelId);
            return View(room);
        }

        // GET: UploadImage
        public ActionResult UploadImage()
        {
            return View();
        }

        // GET: UploadImage
        public ActionResult UploadImage2(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }

            return View(room);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage2([Bind(Include = "Id,RoomName,Description,Availability,Rate,HotelId")] Room room, HttpPostedFileBase postedFile)
        {
            ModelState.Clear();
            Room room2 = db.Rooms.Find(room.Id);
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            room2.PicName = "picRoom_" + room2.Id;

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/images/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = room2.PicName + fileExtension;
                room2.PicName = filePath;
                postedFile.SaveAs(serverPath + filePath);

                try
                {
                    db.Entry(room2).State = EntityState.Modified;
                    db.SaveChanges();
                }
                catch (DbEntityValidationException dbEx)
                {
                    foreach (var validationErrors in dbEx.EntityValidationErrors)
                    {
                        foreach (var validationError in validationErrors.ValidationErrors)
                        {
                            System.Console.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }

        // GET: Rooms/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", room.HotelId);
            return View(room);
        }

        // POST: Rooms/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,RoomName,Description,Availability,Rate,HotelId")] Room room)
        {
            if (ModelState.IsValid)
            {
                db.Entry(room).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", room.HotelId);
            return View(room);
        }

        // GET: Rooms/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Room room = db.Rooms.Find(id);
            if (room == null)
            {
                return HttpNotFound();
            }
            return View(room);
        }

        // POST: Rooms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Room room = db.Rooms.Find(id);
            db.Rooms.Remove(room);
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
