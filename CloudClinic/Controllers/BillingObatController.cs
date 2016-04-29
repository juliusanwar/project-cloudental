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

using PagedList;

namespace CloudClinic.Controllers
{
    public class BillingObatController : Controller
    {
        private ClinicContext db = new ClinicContext();


        //public decimal GetTotal()
        //{
        //    decimal? total = (from c in db.BillingObat.Include("Diagnosis")
        //                      where c.BilObatId == ShoppingCartId
        //                      select (int?)c.Count * c.Book.Price).Sum();

        //    return total ?? decimal.Zero;
        //}

        [Authorize(Roles = "Dokter")]
        // GET: BillingObat
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "UserName" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var bilObat = from b in db.BillingObat select b;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                bilObat = bilObat.Where(b => b.Diagnosis.Pasien.UserName.ToUpper().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "UserName":
                    bilObat = bilObat.OrderByDescending(b => b.Diagnosis.Pasien.UserName);
                    break;
                default:
                    bilObat = bilObat.OrderBy(b => b.Diagnosis.Pasien.UserName);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);



            return View(bilObat.ToPagedList(No_Of_Page, Size_Of_Page));

            //return View(pasien.ToList());
        }
        //public ActionResult Index()
        //{
        //    var billingObat = db.BillingObat.Include(b => b.Barang).Include(b => b.Diagnosis);
        //    return View(billingObat.ToList());
        //}

        [Authorize(Roles = "Dokter")]
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

        [Authorize(Roles = "Dokter")]
        // GET: BillingObat/Create
        public ActionResult Create()
        {
            ViewBag.DiagnosisId = new SelectList(db.Diagnosis, "DiagnosisId", "Amnanesa");
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang");
            //ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            return View();
        }

        // POST: BillingObat/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BilObatId,TglDatang,DiagnosisId,BarangId,Qty,Harga")] BillingObat billingObat)
        {
            if (ModelState.IsValid)
            {
                var obat = (from o in db.Barang
                            where o.BarangId == billingObat.BarangId
                            select o).SingleOrDefault();
                if (billingObat.Qty >= obat.Stok)
                {
                    ViewBag.Error = "Gagal menambahkan transaksi obat, stok tidak sesuai dengan kuantitas!!!";
                }
                else if (obat.Stok <= 5)
                {
                    ViewBag.Error = "Gagal melakukan transaksi obat! Silahkan Re-Order / Re-Stock Klinik terlebih dahulu!!!";
                }
                else
                {
                    obat.Stok = obat.Stok - billingObat.Qty;
                    var subTotal = (from t in db.Barang
                                    where t.BarangId == billingObat.BarangId
                                    select t).SingleOrDefault();
                    billingObat.SubTotal = billingObat.Harga * billingObat.Qty;


                    db.BillingObat.Add(billingObat);
                    db.SaveChanges();
                    //return RedirectToAction("Index");
                    ViewBag.Pesan = "Berhasil menambahkan transaksi obat!";
                }
                

                
            }
      

            ViewBag.DiagnosisId = new SelectList(db.Diagnosis, "DiagnosisId", "Amnanesa", billingObat.DiagnosisId);
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang", billingObat.BarangId);
            //ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", billingObat.PasienId);
            return View(billingObat);
        }

        [Authorize(Roles = "Dokter")]
        // GET: BillingObat/Edit/5
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
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang", billingObat.BarangId);
            //ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", billingObat.PasienId);
            return View(billingObat);
        }

        // POST: BillingObat/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BilObatId,TglDatang,DiagnosisId,BarangId,Qty,Harga,Total")] BillingObat billingObat)
        {
            if (ModelState.IsValid)
            {
                db.Entry(billingObat).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang", billingObat.BarangId);
            //ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", billingObat.PasienId);
            return View(billingObat);
        }

        // GET: BillingObat/Delete/5
        [Authorize(Roles = "Dokter")]
        public ActionResult DeleteBillObat(int? id)
        {
            BillingObat obat = db.BillingObat.Find(id);
            db.BillingObat.Remove(obat);
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
