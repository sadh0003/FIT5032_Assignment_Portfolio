using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_Portfolio.Models;

namespace FIT5032_Assignment_Portfolio.Controllers
{
    public class HotelFacilitiesController : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        // GET: HotelFacilities
        public ActionResult Index()
        {
            var hotelFacilities = db.HotelFacilities.Include(h => h.Hotel);
            return View(hotelFacilities.ToList());
        }

        // GET: HotelFacilities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelFacility hotelFacility = db.HotelFacilities.Find(id);
            if (hotelFacility == null)
            {
                return HttpNotFound();
            }
            return View(hotelFacility);
        }

        // GET: HotelFacilities/Create
        public ActionResult Create()
        {
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName");
            return View();
        }

        // POST: HotelFacilities/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Facility,HotelId")] HotelFacility hotelFacility)
        {
            if (ModelState.IsValid)
            {
                db.HotelFacilities.Add(hotelFacility);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", hotelFacility.HotelId);
            return View(hotelFacility);
        }

        // GET: HotelFacilities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelFacility hotelFacility = db.HotelFacilities.Find(id);
            if (hotelFacility == null)
            {
                return HttpNotFound();
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", hotelFacility.HotelId);
            return View(hotelFacility);
        }

        // POST: HotelFacilities/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Facility,HotelId")] HotelFacility hotelFacility)
        {
            if (ModelState.IsValid)
            {
                db.Entry(hotelFacility).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.HotelId = new SelectList(db.Hotels, "Id", "HotelName", hotelFacility.HotelId);
            return View(hotelFacility);
        }

        // GET: HotelFacilities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            HotelFacility hotelFacility = db.HotelFacilities.Find(id);
            if (hotelFacility == null)
            {
                return HttpNotFound();
            }
            return View(hotelFacility);
        }

        // POST: HotelFacilities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            HotelFacility hotelFacility = db.HotelFacilities.Find(id);
            db.HotelFacilities.Remove(hotelFacility);
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
