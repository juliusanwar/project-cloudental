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
        
    public class JenisBarangController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter")]
        // GET: JenisObat
        public ActionResult Index()
        {
            var jenis = db.JenisBarang.Include(j => j.Barang);
            return View(jenis.ToList());
        }

        [Authorize(Roles = "Admin,Dokter")]
        // GET: JenisObat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisBarang jenisBarang = db.JenisBarang.Find(id);
            if (jenisBarang == null)
            {
                return HttpNotFound();
            }
            return View(jenisBarang);
        }


        // GET: JenisObat/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: JenisObat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisBrgId,NamaJenis")] JenisBarang jenisBarang)
        {
            if (ModelState.IsValid)
            {
                db.JenisBarang.Add(jenisBarang);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jenisBarang);
        }


        // GET: JenisObat/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisBarang jenisBarang = db.JenisBarang.Find(id);
            if (jenisBarang == null)
            {
                return HttpNotFound();
            }
            return View(jenisBarang);
        }

        // POST: JenisObat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisBrgId,NamaJenis")] JenisBarang jenisBarang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisBarang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jenisBarang);
        }


        // GET: JenisObat/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            JenisBarang jenisBarang = db.JenisBarang.Find(id);
            db.JenisBarang.Remove(jenisBarang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: JenisObat/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
