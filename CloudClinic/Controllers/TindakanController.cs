using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;
using PagedList;

namespace CloudClinic.Controllers
{
    
    public class TindakanController : Controller
    {
        private ClinicContext db = new ClinicContext();

        //public ActionResult TindakanByJenis(int id)
        //{
        //    IEnumerable<Tindakan> modelList = new List<Tindakan>();
        //    using (ClinicContext context = new ClinicContext())
        //    {
        //        var tindakan = context.Tindakan.Where(x => x.JenisTindakanId == id).ToList();
        //        modelList = tindakan.Select(x =>
        //                   new Tindakan()
        //                   {
        //                       TindakanId = x.TindakanId,
        //                       JenisTindakanId = x.JenisTindakanId,
        //                       NamaTindakan = x.NamaTindakan,
        //                       Harga = x.Harga,
        //                       Diagnosa = x.Diagnosa
        //                   });
        //    }
        //    return PartialView(modelList);
        //}

        [Authorize(Roles = "Admin,Dokter")]
        // GET: Tindakan
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "JenisTindakan" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var tindakan = from t in db.Tindakan select t;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                tindakan = tindakan.Where(t => t.NamaTindakan.ToUpper().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "JenisTindakan":
                    tindakan = tindakan.OrderByDescending(t => t.JenisTindakanId);
                    break;
                default:
                    tindakan = tindakan.OrderBy(t => t.JenisTindakanId);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);



            return View(tindakan.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        [Authorize(Roles = "Admin,Dokter")]
        // GET: Tindakans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tindakan tindakan = db.Tindakan.Find(id);
            if (tindakan == null)
            {
                return HttpNotFound();
            }
            return View(tindakan);
        }


        // GET: Tindakans/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakan, "JenisTindakanId", "NamaTindakan");
            return View();
        }

        // POST: Tindakans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TindakanId,NamaTindakan,JenisTindakanId,Harga,Diagnosa")] Tindakan tindakan)
        {
            if (ModelState.IsValid)
            {
                db.Tindakan.Add(tindakan);
                db.SaveChanges();
                //return RedirectToAction("Index");
                ViewBag.Pesan = "Berhasil menambahkan Tindakan baru!";
            }

            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakan, "JenisTindakanId", "NamaTindakan", tindakan.JenisTindakanId);
            return View(tindakan);
        }

        // GET: Tindakans/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tindakan tindakan = db.Tindakan.Find(id);
            if (tindakan == null)
            {
                return HttpNotFound();
            }
            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakan, "JenisTindakanId", "NamaTindakan", tindakan.JenisTindakanId);
            return View(tindakan);
        }

        // POST: Tindakans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TindakanId,NamaTindakan,JenisTindakanId,Harga,Diagnosa")] Tindakan tindakan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tindakan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JenisTindakanId = new SelectList(db.JenisTindakan, "JenisTindakanId", "NamaTindakan", tindakan.JenisTindakanId);
            return View(tindakan);
        }


        // GET: Tindakans/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            Tindakan tindakan = db.Tindakan.Find(id);
            db.Tindakan.Remove(tindakan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Tindakans/Delete/5
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
