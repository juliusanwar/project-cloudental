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
    
    public class JenisTindakanController : Controller
    {
        private ClinicContext db = new ClinicContext();

        

        [Authorize(Roles = "Admin,Dokter")]
        // GET: JenisTindakans
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "NamaJenis" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var jenis = from j in db.JenisTindakan select j;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                jenis = jenis.Where(j => j.NamaTindakan.ToUpper().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "NamaJenis":
                    jenis = jenis.OrderByDescending(j => j.NamaTindakan);
                    break;
                default:
                    jenis = jenis.OrderBy(j => j.NamaTindakan);
                    break;
            }

            int Size_Of_Page = 5;
            int No_Of_Page = (Page_No ?? 1);



            return View(jenis.ToPagedList(No_Of_Page, Size_Of_Page));
        }

        [Authorize(Roles = "Admin,Dokter")]
        // GET: JenisTindakans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisTindakan jenisTindakan = db.JenisTindakan.Find(id);
            if (jenisTindakan == null)
            {
                return HttpNotFound();
            }
            return View(jenisTindakan);
        }


        // GET: JenisTindakans/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: JenisTindakans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JenisTindakanId,NamaTindakan")] JenisTindakan jenisTindakan)
        {
            if (ModelState.IsValid)
            {
                db.JenisTindakan.Add(jenisTindakan);
                db.SaveChanges();
                ViewBag.Pesan = "Berhasil menambahkan Jenis Kategori Tindakan baru!";
            }

            return View(jenisTindakan);
        }


        // GET: JenisTindakans/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            JenisTindakan jenisTindakan = db.JenisTindakan.Find(id);
            if (jenisTindakan == null)
            {
                return HttpNotFound();
            }
            return View(jenisTindakan);
        }

        // POST: JenisTindakans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JenisTindakanId,NamaTindakan")] JenisTindakan jenisTindakan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jenisTindakan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jenisTindakan);
        }


        // GET: JenisTindakans/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            JenisTindakan jenisTindakan = db.JenisTindakan.Find(id);
            db.JenisTindakan.Remove(jenisTindakan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: JenisTindakans/Delete/5
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
