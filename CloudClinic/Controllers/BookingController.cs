using CloudClinic.Models;
using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClinic.Controllers
{
    public class BookingController : Controller
    {

        private ClinicContext db = new ClinicContext();

        // GET: Booking
        public ActionResult Index()
        {
            var book = db.Appointment.ToList();
            return View(book);
        }

        // GET: Booking/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Booking/Create
        public ActionResult Create()
        {
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");

            var model = new Appointment();
            model.IsTimeShowed = false;
            return View(model);

            
        }

        [HttpGet]
        public ActionResult Check(string date, Appointment appointment)
        {
            var model = new Appointment();
            //model.Time = DateTime.Parse(date);
            model.IsTimeShowed = true;

            // Database operation
            // contoh : cek slot time sesuai dgn tanggal.

            //var book = (from a in db.Jadwal
            //            where a.JadwalId == appointment.JadwalId
            //            select a).SingleOrDefault();
            //model.IsTimeAvailable = book.Sesi;

            if (model.IsTimeAvailable == true)
            {
                //var book = (from a in db.Jadwal
                //            where a.JadwalId == appointment.JadwalId
                //            select a).SingleOrDefault();
                //model.IsTimeAvailable = book.Sesi;
                //book.Stok = book.Stok + appointment.Qty;

            }


            //model.IsTime1Available = true;
            //model.IsTime2Available = false;
            //model.IsTime3Available = true;

            ModelState.Remove("Time");
            return View("Create", model);
        }

        // POST: Booking/Create
        [HttpPost]
        public ActionResult Create(Appointment model)
        {
            model.CreatedAt = DateTime.Now;

            if (!ModelState.IsValid)
            {
                return View(model); // Return view balik (tambah error la)
            }

            //var date = model.Time;
            var time = model.Jadwal.Sesi;
            // Save ke database?

            if (ModelState.IsValid)
            {
                db.Appointment.Add(model);
                db.SaveChanges();
                //ViewBag.Pesan = "Kamu telah berhasil melakukan reservasi dengan tanggal " + date + " dengan waktu sesi " + time;
            }
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", model.PasienId);
            return View(model); // Return view success.
        }

        // GET: Booking/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Booking/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Booking/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Booking/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
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
    }
}
