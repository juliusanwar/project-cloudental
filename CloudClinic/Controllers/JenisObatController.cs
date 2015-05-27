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
    public class JenisObatController : Controller
    {
        private ClinicContext db = new ClinicContext();


        // GET: JenisObat
        public ActionResult Index()
        {
            var jenis = db.JenisObat.Include(j => j.Obat);
            return View(jenis.ToList());
        }

        // GET: JenisObat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisObat jenisObat = db.JenisObat.Find(id);
            if (jenisObat == null)
            {
                return HttpNotFound();
            }
            return View(jenisObat);
        }


        // GET: JenisObat/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JenisObat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisObatId,NamaJenis")] JenisObat jenisObat)
        {
            if (ModelState.IsValid)
            {
                db.JenisObat.Add(jenisObat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jenisObat);
        }


        // GET: JenisObat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisObat jenisObat = db.JenisObat.Find(id);
            if (jenisObat == null)
            {
                return HttpNotFound();
            }
            return View(jenisObat);
        }

        // POST: JenisObat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisObatId,NamaJenis")] JenisObat jenisObat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisObat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jenisObat);
        }


        // GET: JenisObat/Delete/5
        public ActionResult Delete(int? id)
        {
            JenisObat jenisObat = db.JenisObat.Find(id);
            db.JenisObat.Remove(jenisObat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: JenisObat/Delete/5
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
