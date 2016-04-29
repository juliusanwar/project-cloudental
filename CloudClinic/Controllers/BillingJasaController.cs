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
    public class BillingJasaController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Dokter")]
        // GET: BillingJasa
        public ActionResult Index()
        {
            var billingJasa = db.BillingJasa.Include(b => b.Diagnosis).Include(b => b.Tindakan);
            return View(billingJasa.ToList());
        }

        [Authorize(Roles = "Dokter")]
        // GET: BillingJasa/Details/5
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

        public JsonResult GetHarga(int TindakanId)
        {
            var harga = from r in db.Tindakan
                        where r.TindakanId == TindakanId
                        select new
                        {
                            id = r.TindakanId,
                            label = r.Harga,
                            value = r.Harga
                        };
            return Json(harga.Single(), JsonRequestBehavior.AllowGet);
        }

        [Authorize(Roles = "Dokter")]
        // GET: BillingJasa/Create
        public ActionResult Create()
        {
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            ViewBag.DiagnosisId = new SelectList(db.Diagnosis, "DiagnosisId", "Amnanesa");
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "NamaTindakan");
            return View();
        }

        // POST: BillingJasa/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BilJasaId,PasienId,DiagnosisId,Gigi,TindakanId,Harga,TglDatang")] BillingJasa billingJasa)
        {
            if (ModelState.IsValid)
            {
                db.BillingJasa.Add(billingJasa);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.Pesan = "Berhasil menambahkan pemeriksaan pasien!";
            }
            else
            {
                ViewBag.Pesan = "Pemeriksaan pasien berhasil dibatalkan!";
            }

            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", billingJasa.PasienId);
            ViewBag.DiagnosisId = new SelectList(db.Diagnosis, "DiagnosisId", "Amnanesa", billingJasa.DiagnosisId);
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "NamaTindakan", billingJasa.TindakanId);
            return View(billingJasa);
        }

        [Authorize(Roles = "Dokter")]
        // GET: BillingJasa/Edit/5
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
            ViewBag.DiagnosisId = new SelectList(db.Diagnosis, "DiagnosisId", "Amnanesa", billingJasa.DiagnosisId);
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "NamaTindakan", billingJasa.TindakanId);
            return View(billingJasa);
        }

        // POST: BillingJasa/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BilJasaId,PasienId,DiagnosisId,Gigi,TindakanId,Harga,TglDatang")] BillingJasa billingJasa)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billingJasa).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DiagnosisId = new SelectList(db.Diagnosis, "DiagnosisId", "Amnanesa", billingJasa.DiagnosisId);
            ViewBag.TindakanId = new SelectList(db.Tindakan, "TindakanId", "NamaTindakan", billingJasa.TindakanId);
            return View(billingJasa);
        }

        [Authorize(Roles = "Dokter")]
        // GET: BillingJasa/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: BillingJasa/Delete/5
        [Authorize(Roles = "Dokter")]
        public ActionResult DeleteJasa(int? id)
        {
            BillingJasa jasa = db.BillingJasa.Find(id);
            db.BillingJasa.Remove(jasa);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Barang/Delete/5
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
