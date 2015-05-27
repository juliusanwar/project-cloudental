using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;

namespace CloudClinic.Controllers
{
    [Authorize(Roles = "Admin")]
    public class JenisTindakanController : Controller
    {
        private ClinicContext db = new ClinicContext();


        // GET: JenisTindakans
        public ActionResult Index()
        {
            return View(db.JenisTindakans.ToList());
        }

        // GET: JenisTindakans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisTindakan jenisTindakan = db.JenisTindakans.Find(id);
            if (jenisTindakan == null)
            {
                return HttpNotFound();
            }
            return View(jenisTindakan);
        }


        // GET: JenisTindakans/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JenisTindakans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisTindakanId,NamaTindakan")] JenisTindakan jenisTindakan)
        {
            if (ModelState.IsValid)
            {
                db.JenisTindakans.Add(jenisTindakan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jenisTindakan);
        }


        // GET: JenisTindakans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisTindakan jenisTindakan = db.JenisTindakans.Find(id);
            if (jenisTindakan == null)
            {
                return HttpNotFound();
            }
            return View(jenisTindakan);
        }

        // POST: JenisTindakans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisTindakanId,NamaTindakan")] JenisTindakan jenisTindakan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisTindakan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jenisTindakan);
        }


        // GET: JenisTindakans/Delete/5
        public ActionResult Delete(int? id)
        {
            JenisTindakan jenisTindakan = db.JenisTindakans.Find(id);
            db.JenisTindakans.Remove(jenisTindakan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: JenisTindakans/Delete/5
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
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
