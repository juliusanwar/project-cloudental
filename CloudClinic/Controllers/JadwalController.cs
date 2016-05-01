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
    
    public class JadwalController : Controller
    {
        private ClinicContext db = new ClinicContext();

        //[Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Jadwal
        public ActionResult Index()
        {
            return View(db.Jadwal.ToList());
        }

        //[Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Jadwal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadwal jadwal = db.Jadwal.Find(id);
            if (jadwal == null)
            {
                return HttpNotFound();
            }
            return View(jadwal);
        }


        // GET: Jadwal/Create
        //[Authorize(Roles = "Admin,Dokter")]
        public ActionResult Create()
        {
            //Jadwal jadwal = new Jadwal();
            ////appointment.Jadwal.TanggalJadwal = DateTime.Now;
            ////appointment.Id = Guid.NewGuid();
            ////appointment.CreatedAt = DateTime.Now;
            //jadwal.Pengguna.UserName = User.Identity.Name;
            //return View(jadwal);


            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "Nama");
            return View();
        }

        // POST: Jadwal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JadwalId,PenggunaId,TanggalJadwal,Ruang,Sesi")] Jadwal jadwal)
        {
            if (ModelState.IsValid)
            {
                db.Jadwal.Add(jadwal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "Nama", jadwal.PenggunaId);

            return View(jadwal);
        }


        // GET: Jadwal/Edit/5
        //[Authorize(Roles = "Admin,Dokter")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadwal jadwal = db.Jadwal.Find(id);
            if (jadwal == null)
            {
                return HttpNotFound();
            }
            return View(jadwal);
        }

        // POST: Jadwal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JadwalId,PenggunaId,TanggalJadwal,Ruang,Sesi")] Jadwal jadwal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jadwal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jadwal);
        }


        // GET: Jadwal/Delete/5
        //[Authorize(Roles = "Admin,Dokter")]
        public ActionResult Delete(int? id)
        {
            Jadwal jadwal = db.Jadwal.Find(id);
            db.Jadwal.Remove(jadwal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Jadwal/Delete/5
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
