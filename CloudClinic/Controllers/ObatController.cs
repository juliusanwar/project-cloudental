using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;
using CloudClinic.Models.DataModel;

namespace CloudClinic.Controllers
{
    public class ObatController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Obat
        public ActionResult Index()
        {
            var jenis = db.Obat.Include(o => o.JenisObat).Include(o => o.BillingObats);
            //var obats = db.Obat.Include("JenisObat").Include("BillingObat")
            //    .OrderBy(o => o.Nama).Select(o => o);
            return View(jenis.ToList());
        }

        // GET: Obat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obat obat = db.Obat.Find(id);
            if (obat == null)
            {
                return HttpNotFound();
            }
            return View(obat);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Obat/Create
        public ActionResult Create()
        {
            ViewBag.JenisObatId = new SelectList(db.JenisObat, "JenisObatId", "NamaJenis");
            return View();
        }

        // POST: Obat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ObatId,Nama,JenisObatId,Kategori,Harga,Stok")] Obat obat)
        {
            if (ModelState.IsValid)
            {
                db.Obat.Add(obat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JenisObatId = new SelectList(db.JenisObat, "JenisObatId", "NamaJenis", obat.JenisObatId);
            return View(obat);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Obat/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Obat obat = db.Obat.Find(id);
            if (obat == null)
            {
                return HttpNotFound();
            }
            return View(obat);
        }

        // POST: Obat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ObatId,Nama,JenisObatId,Kategori,Harga,Stok")] Obat obat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(obat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(obat);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Obat/Delete/5
        public ActionResult Delete(int? id)
        {
            Obat obat = db.Obat.Find(id);
            db.Obat.Remove(obat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Obat/Delete/5
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
