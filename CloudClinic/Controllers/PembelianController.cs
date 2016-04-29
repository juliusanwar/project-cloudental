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
using Microsoft.Reporting.WebForms;
using System.IO;

using PagedList;

namespace CloudClinic.Controllers
{
    public class PembelianController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Pembelian
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "NamaBarang" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var pembelian = from b in db.Pembelian select b;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                pembelian = pembelian.Where(b => b.Barang.NamaBarang.ToUpper().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "NamaBarang":
                    pembelian = pembelian.OrderByDescending(b => b.Barang.NamaBarang);
                    break;
                default:
                    pembelian = pembelian.OrderBy(b => b.Barang.NamaBarang);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);



            return View(pembelian.ToPagedList(No_Of_Page, Size_Of_Page));

            //return View(pasien.ToList());
        }
        //public ActionResult Index()
        //{
        //    var pembelian = db.Pembelian.Include(p => p.Barang);
        //    return View(pembelian.ToList());

        //    //using (PembelianModel dc = new PembelianModel())
        //    //{
        //    //    var v = dc.Pembelian.ToList();
        //    //    return View(v);
        //    //}
        //}

        [Authorize(Roles = "Admin")]
        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "PembelianReport.rdlc");
            if(System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<Pembelian> cm = new List<Pembelian>();
            using (ClinicContext dc = new ClinicContext())
            {
                cm = dc.Pembelian.ToList();
            }
            ReportDataSource rd = new ReportDataSource("MyDatase", cm);
            lr.DataSources.Add(rd);
            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

                "<DeviceInfo>" +
                "   <OutputFormat>" + id + "</OutputFormat>" +
                "   <PageWidth>8.15in</PageWidth>" +
                "   <PageHeight>11in</PageHeight>" +
                "   <MarginTop>0.5in</MarginTop>" +
                "   <MarginLeft>1in</MarginLeft>" +
                "   <MarginRight>1in</MarginRight>" +
                "   <MarginBottom>0.5in</MarginBottom>" +
                "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);

            return File(renderedBytes, mimeType);
        }

        // GET: Pembelian/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pembelian pembelian = db.Pembelian.Find(id);
            if (pembelian == null)
            {
                return HttpNotFound();
            }
            return View(pembelian);
        }

        // GET: Pembelian/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang");
            return View();
        }

        // POST: Pembelian/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PembelianId,BarangId,TglBeli,Qty,Harga,Total")] Pembelian pembelian)
        {
            if (ModelState.IsValid)
            {
                var beli = (from b in db.Barang
                            where b.BarangId == pembelian.BarangId
                            select b).SingleOrDefault();
                beli.Stok = beli.Stok + pembelian.Qty;

                var total = (from t in db.Barang
                             where t.BarangId == pembelian.BarangId
                             select t).SingleOrDefault();
                pembelian.Total = pembelian.Harga * pembelian.Qty;

                db.Pembelian.Add(pembelian);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.Pesan = "Berhasil menambahkan pembelian obat!";
            }

            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang", pembelian.BarangId);
            return View(pembelian);
        }

        // GET: Pembelian/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pembelian pembelian = db.Pembelian.Find(id);
            if (pembelian == null)
            {
                return HttpNotFound();
            }
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang", pembelian.BarangId);
            return View(pembelian);
        }

        // POST: Pembelian/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PembelianId,BarangId,TglBeli,Qty,Harga,Total")] Pembelian pembelian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pembelian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.BarangId = new SelectList(db.Barang, "BarangId", "NamaBarang", pembelian.BarangId);
            return View(pembelian);
        }

        // GET: Pembelian/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pembelian pembelian = db.Pembelian.Find(id);
            if (pembelian == null)
            {
                return HttpNotFound();
            }
            return View(pembelian);
        }

        // POST: Pembelian/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteBeli(int? id)
        {
            Pembelian pembelian = db.Pembelian.Find(id);
            db.Pembelian.Remove(pembelian);
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
