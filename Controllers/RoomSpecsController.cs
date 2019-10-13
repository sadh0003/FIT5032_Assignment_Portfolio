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
    public class RoomSpecsController : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        // GET: RoomSpecs
        public ActionResult Index()
        {
            var roomSpecs = db.RoomSpecs.Include(r => r.Room);
            return View(roomSpecs.ToList());
        }

        // GET: RoomSpecs/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomSpec roomSpec = db.RoomSpecs.Find(id);
            if (roomSpec == null)
            {
                return HttpNotFound();
            }
            return View(roomSpec);
        }

        // GET: RoomSpecs/Create
        public ActionResult Create()
        {
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName");
            return View();
        }

        // POST: RoomSpecs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Specification,RoomId")] RoomSpec roomSpec)
        {
            if (ModelState.IsValid)
            {
                db.RoomSpecs.Add(roomSpec);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", roomSpec.RoomId);
            return View(roomSpec);
        }

        // GET: RoomSpecs/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomSpec roomSpec = db.RoomSpecs.Find(id);
            if (roomSpec == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", roomSpec.RoomId);
            return View(roomSpec);
        }

        // POST: RoomSpecs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Specification,RoomId")] RoomSpec roomSpec)
        {
            if (ModelState.IsValid)
            {
                db.Entry(roomSpec).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.RoomId = new SelectList(db.Rooms, "Id", "RoomName", roomSpec.RoomId);
            return View(roomSpec);
        }

        // GET: RoomSpecs/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoomSpec roomSpec = db.RoomSpecs.Find(id);
            if (roomSpec == null)
            {
                return HttpNotFound();
            }
            return View(roomSpec);
        }

        // POST: RoomSpecs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RoomSpec roomSpec = db.RoomSpecs.Find(id);
            db.RoomSpecs.Remove(roomSpec);
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
