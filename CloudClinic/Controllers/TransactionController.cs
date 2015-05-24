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
    public class TransactionController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Transaction
        public ActionResult Index()
        {
            //IEnumerable<TransactionViewModel> model = from t in db.Transaction
            //                                          select new TransactionViewModel
            //                                   {
            //                                       TransactionId = t.TransactionId,
            //                                       PasienId = t.PasienId,
            //                                       TanggalDatang = t.TanggalDatang,
            //                                       Amnanesa = t.Amnanesa,
            //                                       Nama = t.Pengguna.Nama
            //                                   };
            //return Json(model, JsonRequestBehavior.AllowGet);
            //return View();

            var transaction = db.Transaction.Include(t => t.Pasien).Include(t => t.Pengguna);
            return View(transaction.ToList());
        }

        // GET: Transaction/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Transaction/Create
        public ActionResult Create()
        {
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName");
            return View();
        }

        // POST: Transaction/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TransactionId,PasienId,TanggalDatang,Amnanesa,PenggunaId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Transaction.Add(transaction);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", transaction.PasienId);
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName", transaction.PenggunaId);
            return View(transaction);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Transaction/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transaction.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", transaction.PasienId);
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName", transaction.PenggunaId);
            return View(transaction);
        }

        // POST: Transaction/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TransactionId,PasienId,TanggalDatang,Amnanesa,PenggunaId")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", transaction.PasienId);
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName", transaction.PenggunaId);
            return View(transaction);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Transaction/Delete/5
        public ActionResult Delete(int? id)
        {
            Transaction transaksi = db.Transaction.Find(id);
            db.Transaction.Remove(transaksi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Transaction/Delete/5
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
