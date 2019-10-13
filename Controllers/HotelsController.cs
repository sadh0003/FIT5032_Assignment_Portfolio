using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_Portfolio.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Assignment_Portfolio.Controllers
{
    public class HotelsController : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        ApplicationDbContext context;
        public HotelsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Map
        public ActionResult Map(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }

            ViewBag.Longitude = hotel.Longitude;
            ViewBag.Latitude = hotel.Latitude;
            ViewBag.Name = hotel.HotelName;
            ViewBag.Description = hotel.Description;
            ViewBag.Star = hotel.HotelStar;

            return View(hotel);
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
            Hotel hotel = db.Hotels.Find(id);
            if (hotel == null)
            {
                return HttpNotFound();
            }

            return View(hotel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage2([Bind(Include = "Id,HotelName,Description,HotelStar,Longitude,Latitude,UserId,Neighbourhood")] Hotel hotel, HttpPostedFileBase postedFile)
        {
            ModelState.Clear();
            Hotel hotel2 = db.Hotels.Find(hotel.Id);
            var myUniqueFileName = string.Format(@"{0}", Guid.NewGuid());
            hotel2.PicName = myUniqueFileName;

            if (ModelState.IsValid)
            {
                string serverPath = Server.MapPath("~/images/");
                string fileExtension = Path.GetExtension(postedFile.FileName);
                string filePath = hotel2.PicName + fileExtension;
                hotel2.PicName = filePath;
                postedFile.SaveAs(serverPath + filePath);

                try
                {
                    db.Entry(hotel2).State = EntityState.Modified;
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
    
    // GET: Find
    public ActionResult Find()
    {
        string dateParam = "";
        string date = Request.QueryString["date"];
        if (date != "" && date != null)
        {
            dateParam += "where Date ='" + date + "'";
            ViewBag.date = date;
        }

        string neighParam = "";
        string neighbourhood = Request.QueryString["neigh"];
        if (neighbourhood != "" && neighbourhood != null)
        {
            neighParam += "where UPPER(h.neighbourhood) like UPPER('%" + neighbourhood + "%')";
            ViewBag.neighbourhood = neighbourhood;
        }

        string query = "select distinct h.* from ( " +
            "               select r.Id, r.HotelId, r.Availability, count(b.Id) book " +
            "                   from Rooms r left join " +
            "                   (select * from Bookings " + dateParam + ") b on r.Id = b.RoomId " +
            "               group by r.Id, r.HotelId, r.Availability) avail " +
            "           join Hotels h on avail.HotelId = h.Id " + neighParam;

        IEnumerable<Hotel> data = db.Database.SqlQuery<Hotel>(query);
        return View(data.ToList());

        //return View(db.Hotels.ToList());
    }

    // GET: Hotels
    public ActionResult Index()
    {
        string userId = User.Identity.GetUserId();

        if (User.IsInRole("Vendor"))
        {
            return View(db.Hotels.Where(u => u.UserId.Equals(userId))
            .ToList());
        }
        else
        {
            return View(db.Hotels
            .ToList());
        }
    }

    // GET: Hotels/Details/5
    public ActionResult Details(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Hotel hotel = db.Hotels.Find(id);
        if (hotel == null)
        {
            return HttpNotFound();
        }
        return View(hotel);
    }

    // GET: Hotels/Create
    public ActionResult Create()
    {
        ViewBag.UserName = new SelectList(context.Users
                                        .ToList(), "Id", "UserName");

        return View();
    }

    // POST: Hotels/Create
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Create([Bind(Include = "Id,HotelName,Description,HotelStar,Longitude,Latitude,UserId,Neighbourhood")] Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            db.Hotels.Add(hotel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        return View(hotel);
    }

    // GET: Hotels/Edit/5
    public ActionResult Edit(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Hotel hotel = db.Hotels.Find(id);
        if (hotel == null)
        {
            return HttpNotFound();
        }

        ViewBag.UserName = new SelectList(context.Users
                            .ToList(), "Id", "UserName");

        return View(hotel);
    }

    // POST: Hotels/Edit/5
    // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
    // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public ActionResult Edit([Bind(Include = "Id,HotelName,Description,HotelStar,Longitude,Latitude,UserId,Neighbourhood")] Hotel hotel)
    {
        if (ModelState.IsValid)
        {
            db.Entry(hotel).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        return View(hotel);
    }

    // GET: Hotels/Delete/5
    public ActionResult Delete(int? id)
    {
        if (id == null)
        {
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        Hotel hotel = db.Hotels.Find(id);
        if (hotel == null)
        {
            return HttpNotFound();
        }
        return View(hotel);
    }

    // POST: Hotels/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public ActionResult DeleteConfirmed(int id)
    {
        Hotel hotel = db.Hotels.Find(id);
        db.Hotels.Remove(hotel);
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
