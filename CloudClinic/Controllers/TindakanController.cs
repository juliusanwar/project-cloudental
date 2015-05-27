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
    
    public class TindakanController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Tindakans
        public ActionResult Index()
        {
            var tindakan = db.Tindakan.Include(t => t.JenisTindakan);
            return View(tindakan.ToList());
        }

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Tindakans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tindakan tindakan = db.Tindakan.Find(id);
            if (tindakan == null)
            {
                return HttpNotFound();
            }
            return View(tindakan);
        }


        // GET: Tindakans/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakans, "JenisTindakanId", "NamaTindakan");
            return View();
        }

        // POST: Tindakans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TindakanId,Nama,JenisTindakanId,Harga,Diagnosa")] Tindakan tindakan)
        {
            if (ModelState.IsValid)
            {
                db.Tindakan.Add(tindakan);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakans, "JenisTindakanId", "NamaTindakan", tindakan.JenisTindakanId);
            return View(tindakan);
        }

        // GET: Tindakans/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tindakan tindakan = db.Tindakan.Find(id);
            if (tindakan == null)
            {
                return HttpNotFound();
            }
            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakans, "JenisTindakanId", "NamaTindakan", tindakan.JenisTindakanId);
            return View(tindakan);
        }

        // POST: Tindakans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TindakanId,Nama,JenisTindakanId,Harga,Diagnosa")] Tindakan tindakan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tindakan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakans, "JenisTindakanId", "NamaTindakan", tindakan.JenisTindakanId);
            return View(tindakan);
        }


        // GET: Tindakans/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            Tindakan tindakan = db.Tindakan.Find(id);
            db.Tindakan.Remove(tindakan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Tindakans/Delete/5
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
