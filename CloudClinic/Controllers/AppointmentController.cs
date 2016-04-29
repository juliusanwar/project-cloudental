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
    public class AppointmentController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Appointment
        public ActionResult Index()
        {
            var appointment = db.Appointment.Include(a => a.Jadwal).Include(a => a.Pasien);
            return View(appointment.ToList());
        }

        // GET: Appointment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // GET: Appointment/Create
        public ActionResult Create()
        {
            //var JadwalId = db.Appointment.Select(x => x.Jadwal.Hari).Distinct();
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Dokter").Distinct();
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Hari").Distinct();
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Sesi").Distinct();
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            return View();
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PasienId,JadwalId,PhoneNumber,Time,Keluhan,Timezone,CreatedAt")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Appointment.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Dokter").Distinct();
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Hari").Distinct();
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Sesi").Distinct();
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", appointment.PasienId);
            return View(appointment);
        }

        // GET: Appointment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Hari", appointment.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", appointment.PasienId);
            return View(appointment);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PasienId,JadwalId,PhoneNumber,Time,Keluhan,Timezone,CreatedAt")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(appointment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Hari", appointment.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", appointment.PasienId);
            return View(appointment);
        }

        // GET: Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Appointment appointment = db.Appointment.Find(id);
            if (appointment == null)
            {
                return HttpNotFound();
            }
            return View(appointment);
        }

        // POST: Appointment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Appointment appointment = db.Appointment.Find(id);
            db.Appointment.Remove(appointment);
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
