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
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Data.Entity.Core.Objects;

namespace CloudClinic.Controllers
{
    [Authorize]
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
            var model = new AppointmentViewModel();
            model.IsTimeShowed = false;
            model.Date = DateTime.Now;
            return View(model);
        }

        [HttpGet]
        public async Task<ActionResult> Check(string date, string phoneNumber, string keluhan)
        {
            DateTime choosenDate;
            var model = new AppointmentViewModel();

            if (DateTime.TryParse(date, out choosenDate))
            {
                model.Date = choosenDate;
                model.IsTimeShowed = true;

                using (var ctx = new ClinicContext())
                {
                    var availableJadwal = await ctx.Jadwal.Where(x => DbFunctions.TruncateTime(x.TanggalJadwal) == DbFunctions.TruncateTime(choosenDate))
                                                    .ToListAsync();

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
        public async Task<ActionResult> Create(AppointmentViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            #region Sanity Check

            if (String.IsNullOrEmpty(model.Session))
            {
                ModelState.AddModelError("Session", "Silahkan pilih sesi.");
                return View(model);
            }

            if (String.IsNullOrEmpty(model.PhoneNumber))
            {
                ModelState.AddModelError("PhoneNumber", "Silahkan masukkan phone number.");
                return View(model);
            }

            #endregion

            var appointment = this.PopulateAppointmentDataModel(model);
            
            using (ClinicContext ctx = new ClinicContext())
            {
                var pasien = ctx.Pasien.FirstOrDefault(x => x.UserName.Equals(this.User.Identity.Name, StringComparison.OrdinalIgnoreCase));

                if (pasien != null)
                {
                    appointment.PasienId = pasien.PasienId;
                    ctx.Appointment.Add(appointment);
                    await ctx.SaveChangesAsync();
                }
            }

            return RedirectToAction("Index");
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

        private Appointment PopulateAppointmentDataModel(AppointmentViewModel model)
        {
            var appointment = new Appointment();
            appointment.PasienId = model.PasienId;
            appointment.PhoneNumber = model.PhoneNumber;
            appointment.Keluhan = model.Keluhan;
            appointment.CreatedAt = DateTime.Now;

            using (ClinicContext ctx = new ClinicContext())
            {
                var jadwal = ctx.Jadwal.FirstOrDefault(x => DbFunctions.TruncateTime(x.TanggalJadwal) == DbFunctions.TruncateTime(model.Date)
                                            && x.Sesi.Equals(model.Session, StringComparison.OrdinalIgnoreCase));
                appointment.JadwalId = jadwal.JadwalId;
                appointment.Jadwal = jadwal;
            }

            return appointment;
        }
    }

    public class AppointmentViewModel
    {
        [Required]
        public int PasienId {get;set;}

        [Required]
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

        [Required]
        public string Session { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Keluhan { get; set; }
    }
}
