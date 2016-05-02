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

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "TanggalJadwal");
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");

            var model = new AppointmentViewModel();
            model.IsTimeShowed = false;
            return View(model);
        }

        [HttpGet]
        public ActionResult Check(string pasienId, string date, string phoneNumber, string keluhan)
        {
            ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "TanggalJadwal");
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");

            DateTime choosenDate;
            var model = new AppointmentViewModel();

            if (!String.IsNullOrEmpty(pasienId))
            {
                model.PasienId = pasienId;
            }

            if (DateTime.TryParse(date, out choosenDate))
            {
                model.Date = choosenDate;
                model.IsTimeShowed = true;

                using (var ctx = new ClinicContext())
                {
                    var availableJadwal = ctx.Jadwal.Where(x => x.TanggalJadwal.Date == choosenDate.Date).ToList();

                    if (availableJadwal.Any())
                    {
                        foreach (var jadwal in availableJadwal)
                        {
                            switch (jadwal.Sesi)
                            {
                                case "Time1":
                                    model.IsTime1Available = true;
                                    break;
                                case "Time2":
                                    model.IsTime2Available = true;
                                    break;
                                case "Time3":
                                    model.IsTime3Available = true;
                                    break;
                                case "Time4":
                                    model.IsTime4Available = true;
                                    break;
                                case "Time5":
                                    model.IsTime5Available = true;
                                    break;
                                case "Time6":
                                    model.IsTime6Available = true;
                                    break;
                                case "Time7":
                                    model.IsTime7Available = true;
                                    break;
                                case "Time8":
                                    model.IsTime8Available = true;
                                    break;
                                case "Time9":
                                    model.IsTime9Available = true;
                                    break;
                                case "Time10":
                                    model.IsTime10Available = true;
                                    break;
                                default:
                                    break;
                            }
                        }
                    }
                }
            }

            model.PhoneNumber = phoneNumber;
            model.Keluhan = keluhan;

            ModelState.Remove("Date");
            return View("Create", model);
        }

        // POST: Appointment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            return View(model);
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

    public class AppointmentViewModel
    {
        public string PasienId {get;set;}

        public DateTime Date { get; set; }

        public bool IsTimeShowed { get; set; }

        public bool IsTime1Available { get; set; }

        public bool IsTime2Available { get; set; }

        public bool IsTime3Available { get; set; }

        public bool IsTime4Available { get; set; }

        public bool IsTime5Available { get; set; }

        public bool IsTime6Available { get; set; }

        public bool IsTime7Available { get; set; }

        public bool IsTime8Available { get; set; }

        public bool IsTime9Available { get; set; }

        public bool IsTime10Available { get; set; }

        public string Session { get; set; }

        public string PhoneNumber { get; set; }

        public string Keluhan { get; set; }
    }
}
