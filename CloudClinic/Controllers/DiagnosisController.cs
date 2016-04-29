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
using Kendo.Mvc.UI;
using PagedList;

namespace CloudClinic.Controllers
{
    public class DiagnosisController : Controller
    {
        private ClinicContext db = new ClinicContext();

        //[HttpGet]
        //public JsonResult GetPasien(string term = "")
        //{
        //    var returner = (from a in db.Pasien
        //                    where a.UserName.ToUpper().Contains(term.ToUpper().Trim())
        //                    select new { id = a.PasienId, label = a.UserName.Trim(), value = a.UserName.Trim() })
        //           .Take(20);
        //    return Json(returner, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetPasien()
        {
            return Json(db.Pasien, JsonRequestBehavior.AllowGet);
        }

        //public MultiSelectList GetUserName()
        //{
        //    return new MultiSelectList(db.Pasien.ToList(), "PasienId", "UserName");
        //}

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Diagnosis
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            //ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "UserName" : "";
            ViewBag.DateSortParm = Sorting_Order == "Date" ? "TglDatang" : "Date";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var medis = from m in db.Diagnosis select m;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                medis = medis.Where(p => p.Pasien.UserName.ToUpper().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "TglDatang":
                    medis = medis.OrderBy(m => m.TglDatang);
                    break;
                default:
                    medis = medis.OrderByDescending(m => m.TglDatang);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);



            return View(medis.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Diagnosis/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnosis.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        [Authorize(Roles = "Dokter")]
        // GET: Diagnosis/Create
        public ActionResult Create()
        {
            //ViewBag.PasienId = GetUserName();
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            return View();
        }

        // POST: Diagnosis/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiagnosisId,PasienId,TglDatang,Amnanesa")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Diagnosis.Add(diagnosis);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.PasienId = GetUserName();
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", diagnosis.PasienId);
            return View(diagnosis);
        }

        [Authorize(Roles = "Dokter")]
        // GET: Diagnosis/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnosis.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", diagnosis.PasienId);
            return View(diagnosis);
        }

        // POST: Diagnosis/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiagnosisId,PasienId,TglDatang,Amnanesa")] Diagnosis diagnosis)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diagnosis).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", diagnosis.PasienId);
            return View(diagnosis);
        }

        [Authorize(Roles = "Dokter")]
        // GET: Diagnosis/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diagnosis diagnosis = db.Diagnosis.Find(id);
            if (diagnosis == null)
            {
                return HttpNotFound();
            }
            return View(diagnosis);
        }

        // POST: Diagnosis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diagnosis diagnosis = db.Diagnosis.Find(id);
            db.Diagnosis.Remove(diagnosis);
            db.SaveChanges();
            return RedirectToAction("Index");
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
