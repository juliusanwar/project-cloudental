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
    
    public class PenggunaController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Pengguna
        public ActionResult Index()
        {
            return View(db.Pengguna.ToList());
        }

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Pengguna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengguna pengguna = db.Pengguna.Find(id);
            if (pengguna == null)
            {
                return HttpNotFound();
            }
            return View(pengguna);
        }

        // GET: Pengguna/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pengguna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PenggunaId,UserName,Aturan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
        {
            if (ModelState.IsValid)
            {
                db.Pengguna.Add(pengguna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pengguna);
        }

        // GET: Pengguna/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengguna pengguna = db.Pengguna.Find(id);
            if (pengguna == null)
            {
                return HttpNotFound();
            }
            return View(pengguna);
        }

        // POST: Pengguna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenggunaId,UserName,Aturan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pengguna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pengguna);
        }

        // GET: Pengguna/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            Pengguna pengguna = db.Pengguna.Find(id);
            db.Pengguna.Remove(pengguna);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Pengguna/Delete/5
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
