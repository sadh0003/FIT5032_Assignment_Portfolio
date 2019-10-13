using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using FIT5032_Assignment_Portfolio.Models;
using FIT5032_Week08A.Utils;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace FIT5032_Assignment_Portfolio.Controllers
{
    public class MailsController : Controller
    {
        private FIT5032_Assignment_ModelContainer db = new FIT5032_Assignment_ModelContainer();

        ApplicationDbContext context;
        public MailsController()
        {
            context = new ApplicationDbContext();
        }

        // GET: Mails
        public ActionResult Index()
        {
            return View(db.Mails.ToList());
        }

        // GET: Mails/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // GET: Mails/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mails/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Content,Receiver,Subject")] Mail mail, HttpPostedFileBase postedFile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    String toEmail = mail.Receiver;
                    String subject = mail.Subject;
                    String contents = mail.Content;
                    
                    string serverPath = Server.MapPath("~/Upload/");
                    string fileExtension = Path.GetExtension(postedFile.FileName);
                    string filePath = postedFile.FileName;
                    string fileComplete = serverPath + filePath;
                    postedFile.SaveAs(fileComplete);

                    EmailSender es = new EmailSender();
                    es.SendAsync(toEmail, subject, contents, serverPath, postedFile.FileName);

                    ViewBag.Result = "Email has been send.";

                    ModelState.Clear();

                    db.Mails.Add(mail);
                    db.SaveChanges();
                    return RedirectToAction("Index");

                }
                catch
                {
                    return View();
                }
            }

            return View(mail);
        }

        // GET: Mails/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // POST: Mails/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Content,Receiver")] Mail mail)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mail);
        }

        // GET: Mails/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mail mail = db.Mails.Find(id);
            if (mail == null)
            {
                return HttpNotFound();
            }
            return View(mail);
        }

        // POST: Mails/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Mail mail = db.Mails.Find(id);
            db.Mails.Remove(mail);
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
