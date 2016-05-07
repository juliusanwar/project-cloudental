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
using Microsoft.Reporting.WebForms;
using System.IO;
using System.Collections;
using System.Web.Helpers;
using System.Web.UI;
using Rotativa;

namespace CloudClinic.Controllers
{
    
    public class BarangController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Barang
        [Authorize(Roles = "Admin,Dokter")]
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

            var barang = from b in db.Barang select b;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                barang = barang.Where(b => b.NamaBarang.ToUpper().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "NamaBarang":
                    barang = barang.OrderByDescending(b => b.NamaBarang);
                    break;
                default:
                    barang = barang.OrderBy(b => b.NamaBarang);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);



            return View(barang.ToPagedList(No_Of_Page, Size_Of_Page));

            //return View(pasien.ToList());
        }

        public ActionResult ReportAll(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "BarangReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<Barang> cm = new List<Barang>();
            //using (ClinicContext dc = new ClinicContext())
            {
                cm = db.Barang.OrderBy(b => b.NamaBarang).ToList();
            }
            ReportDataSource rd = new ReportDataSource("BarangDataset", cm);
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

        public ActionResult ReportMin(string id)
        {
            LocalReport lr = new LocalReport();
            string path = Path.Combine(Server.MapPath("~/Report"), "BarangReport.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Index");
            }
            List<Barang> cm = null;
            //using (ClinicContext dc = new ClinicContext())
            {
                cm = db.Barang.OrderBy(a => a.Stok).Where(a => a.Stok <= 5).ToList();
            }
            ReportDataSource rd = new ReportDataSource("BarangDataset", cm);
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

        // GET: Barang/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barang barang = db.Barang.Find(id);
            if (barang == null)
            {
                return HttpNotFound();
            }
            return View(barang);
        }

        [Authorize(Roles = "Admin")]
        // GET: Barang/Create
        public ActionResult Create()
        {
            ViewBag.JenisBrgId = new SelectList(db.JenisBarang, "JenisBrgId", "NamaJenis");
            return View();
        }

        // POST: Barang/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "BarangId,NamaBarang,JenisBrgId,Harga,Stok")] Barang barang)
        {
            if (ModelState.IsValid)
            {
                db.Barang.Add(barang);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.Pesan = "Berhasil menambahkan Barang baru";
            }
            ViewBag.JenisBrgId = new SelectList(db.JenisBarang, "JenisBrgId", "NamaJenis", barang.JenisBrgId);

            return View(barang);
        }

        [Authorize(Roles = "Admin")]
        // GET: Barang/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Barang barang = db.Barang.Find(id);
            if (barang == null)
            {
                return HttpNotFound();
            }
            ViewBag.JenisBrgId = new SelectList(db.JenisBarang, "JenisBrgId", "NamaJenis");
            return View(barang);
        }

        // POST: Barang/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BarangId,NamaBarang,JenisBrgId,Harga,Stok")] Barang barang)
        {
            if (ModelState.IsValid)
            {
                db.Entry(barang).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JenisBrgId = new SelectList(db.JenisBarang, "JenisBrgId", "NamaJenis", barang.JenisBrgId);
            return View(barang);
        }

        

        [Authorize(Roles = "Admin,Dokter")]
        public ActionResult DeleteBarang(int? id)
        {
            Barang barang = db.Barang.Find(id);
            db.Barang.Remove(barang);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Barang/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin,Dokter")]
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
