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
    
    public class BillingJasaController : Controller
    {
        private ClinicContext db = new ClinicContext();

        
        // GET: BillingJasa
        [Authorize(Roles = "Admin,Dokter,Pasien")]
        public ActionResult Index()
        {
            var billingJasa = db.BillingJasa.Include(b => b.Tindakan).Include(b => b.Transaction);
            return View(billingJasa.ToList());
        }

        
        // GET: BillingJasa/Details/5
        [Authorize(Roles = "Admin,Dokter,Pasien")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingJasa billingJasa = db.BillingJasa.Find(id);
            if (billingJasa == null)
            {
                return HttpNotFound();
            }
            return View(billingJasa);
        }


        // GET: BillingJasa/Create
        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult Create()
        {
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "Nama");
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa");
            return View();
        }

        // POST: BillingJasa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BilJasaId,TransactionId,TindakanId")] BillingJasa billingJasa)
        {
            if (ModelState.IsValid)
            {
                db.BillingJasa.Add(billingJasa);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "Nama", billingJasa.TindakanId);
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa", billingJasa.TransactionId);
            return View(billingJasa);
        }


        // GET: BillingJasa/Edit/5
        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BillingJasa billingJasa = db.BillingJasa.Find(id);
            if (billingJasa == null)
            {
                return HttpNotFound();
            }
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "Nama", billingJasa.TindakanId);
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa", billingJasa.TransactionId);
            return View(billingJasa);
        }

        // POST: BillingJasa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BilJasaId,TransactionId,TindakanId")] BillingJasa billingJasa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billingJasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "Nama", billingJasa.TindakanId);
            ViewBag.TransactionId = new SelectList(db.Transaction, "TransactionId", "Amnanesa", billingJasa.TransactionId);
            return View(billingJasa);
        }


        // GET: BillingJasa/Delete/5
        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult Delete(int? id)
        {
            BillingJasa billingJasa = db.BillingJasa.Find(id);
            db.BillingJasa.Remove(billingJasa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: BillingJasa/Delete/5
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
