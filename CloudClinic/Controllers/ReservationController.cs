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
    public class ReservationController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Reservation
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.Jadwal).Include(r => r.Pasien).Include(r => r.Pengguna);
            return View(reservation.ToList());
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }

        // GET: Reservation/Create
        public ActionResult Create()
        {
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal");
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,PasienId,TglReservasi,JadwalId,PenggunaId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName", reservation.PenggunaId);
            return View(reservation);
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName", reservation.PenggunaId);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,PasienId,TglReservasi,JadwalId,PenggunaId")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
            ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "UserName", reservation.PenggunaId);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        public ActionResult Delete(int? id)
        {
            Reservation reservasi = db.Reservation.Find(id);
            db.Reservation.Remove(reservasi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Reservation/Delete/5
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
