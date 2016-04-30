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
            

            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            return View();
        }

        [HttpGet]
        public ActionResult Check(string date)
        {
            

            var model = new Appointment();
            model.Jadwal.TanggalJadwal = DateTime.Parse(date);
            model.IsTimeShowed = true;

            //// Database operation
            //// contoh : cek slot time sesuai dgn tanggal.

            ////Dbcontext.Jadwals.Where(x => x.Tanggal.Date == selectedDate.Date).ToList()

            var cek = db.Jadwal.Where(x => x.TanggalJadwal == model.Jadwal.TanggalJadwal).ToList();

            //model.IsTimeAvailable == cek;

            //model.IsTime1Available = true;
            //model.IsTime2Available = false;
            //model.IsTime3Available = true;

            ModelState.Remove("Date");
            return View("Index", model);
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PasienId,JadwalId,PhoneNumber,Keluhan,CreatedAt")] Appointment appointment)
        {
            appointment.CreatedAt = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(appointment); // Return view balik (tambah error la)
            }

            //var date = appointment.Date;
            //var time = appointment.Time;
            // Save ke database?

            //if (ModelState.IsValid)
            //{

            //    ViewBag.Pesan = "Kamu telah berhasil melakukan reservasi dengan tanggal " + date + " dengan waktu sesi " + time;
            //}

            //return View(model); // Return view success.

            if (ModelState.IsValid)
            {
                db.Appointment.Add(appointment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
