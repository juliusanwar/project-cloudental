using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;
using CloudClinic.Models.ViewModel;

namespace CloudClinic.Controllers
{
    
    public class BillingObatController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: BillingObat
        public ActionResult Index()
        {
            var billingObat = db.BillingObat.Include(b => b.Obat).Include(b => b.Transaction);
            return View(billingObat.ToList());
        }
        
        [Authorize(Roles = "Admin,Dokter")]
        // GET: BillingObat/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingObat billingObat = db.BillingObat.Find(id);
            if (billingObat == null)
            {
                return HttpNotFound();
            }
            return View(billingObat);
        }

        
        // GET: BillingObat/Create
        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult Create()
        {
            ViewBag.ObatId = new SelectList(db.Obat, "ObatId", "Nama");
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa");
            return View();
        }

        // POST: BillingObat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BilObatId,TransactionId,ObatId,Qty")] BillingObat billingObat)
        {
            if (ModelState.IsValid)
            {
                var obat = (from o in db.Obat
                            where o.ObatId == billingObat.ObatId
                            select o).SingleOrDefault();
                            obat.Stok = obat.Stok - billingObat.Qty;

                var total = (from t in db.Obat
                             where t.ObatId == billingObat.ObatId
                             select t).SingleOrDefault();
                             billingObat.Total = obat.Harga * billingObat.Qty;

                db.BillingObat.Add(billingObat);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ObatId = new SelectList(db.Obat, "ObatId", "Nama", billingObat.ObatId);
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa", billingObat.TransactionId);
            return View(billingObat);
        }

        
        // GET: BillingObat/Edit/5
        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingObat billingObat = db.BillingObat.Find(id);
            if (billingObat == null)
            {
                return HttpNotFound();
            }
            ViewBag.ObatId = new SelectList(db.Obat, "ObatId", "Nama", billingObat.ObatId);
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa", billingObat.TransactionId);
            return View(billingObat);
        }

        // POST: BillingObat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BilObatId,TransactionId,ObatId,Qty,Total")] BillingObat billingObat)
        {
            if (ModelState.IsValid)
            {
                var obat = (from o in db.Obat
                            where o.ObatId == billingObat.ObatId
                            select o).SingleOrDefault();
                obat.Stok = obat.Stok - billingObat.Qty;

                var total = (from t in db.Obat
                             where t.ObatId == billingObat.ObatId
                             select t).SingleOrDefault();

                billingObat.Total = obat.Harga * billingObat.Qty;
                db.Entry(billingObat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ObatId = new SelectList(db.Obat, "ObatId", "Nama", billingObat.ObatId);
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa", billingObat.TransactionId);
            return View(billingObat);
        }

        
        // GET: BillingObat/Delete/5
        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult Delete(int? id)
        {
            BillingObat billingObat = db.BillingObat.Find(id);
            db.BillingObat.Remove(billingObat);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: BillingObat/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
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
