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
    public class PasienController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Pasiens
        public ActionResult Index()
        {
            return View(db.Pasien.ToList());
        }

        // GET: Pasiens/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasien pasien = db.Pasien.Find(id);
            if (pasien == null)
            {
                return HttpNotFound();
            }
            return View(pasien);
        }

        // GET: Pasiens/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pasiens/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PasienId,UserName,Nama,TglLhr,Gender,GolDarah,Alamat,Telp,TglRegistrasi,RiwayatSakit")] Pasien pasien)
        {
            if (ModelState.IsValid)
            {
                db.Pasien.Add(pasien);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pasien);
        }

        // GET: Pasiens/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasien pasien = db.Pasien.Find(id);
            if (pasien == null)
            {
                return HttpNotFound();
            }
            return View(pasien);
        }

        // POST: Pasiens/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PasienId,UserName,Nama,TglLhr,Gender,GolDarah,Alamat,Telp,TglRegistrasi,RiwayatSakit")] Pasien pasien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pasien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pasien);
        }

        // GET: Pasiens/Delete/5
        public ActionResult Delete(int? id)
        {
            Pasien pasien = db.Pasien.Find(id);
            db.Pasien.Remove(pasien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Pasiens/Delete/5
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
