using CloudClinic.Models;
using CloudClinic.Models.DataModel;
using CloudClinic.Models.DataModel.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace CloudClinic.Controllers
{
    public class AppointmentsController : Controller
    {
        private readonly IAppointmentRepository _repository;

        public AppointmentsController() : this(new AppointmentRepository()) { }

        public AppointmentsController(IAppointmentRepository repository)
        {
            _repository = repository;
        }

        public SelectListItem[] Timezones
        {
            get
            {
                var systemTimeZones = TimeZoneInfo.GetSystemTimeZones();
                return systemTimeZones.Select(systemTimeZone => new SelectListItem
                {
                    Text = systemTimeZone.DisplayName,
                    Value = systemTimeZone.Id
                }).ToArray();
            }
        }

        // GET: Appointments
        public ActionResult Index()
        {
            Pasien Obj = new Pasien();

            Obj.UserName = User.Identity.Name;

            Session["Pasien"] = Obj;

            var appointments = _repository.FindAll();
            return View(appointments);
        }

        // GET: Appointments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointment = _repository.FindById(id.Value);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public ActionResult Create()
        {

            Pasien Obj = new Pasien();

            Obj.UserName = User.Identity.Name;

            Session["Pasien"] = Obj;

            //ViewBag.Timezones = Timezones;
            // Use an empty appointment to setup the default
            // values.
            //var appointment = new Appointment
            //{
            //    Timezone = "Pacific Standard Time",
            //    Time = DateTime.Now
            //};

            //ViewBag.PasienId = new SelectList(_repository.FindById.User, "PasienId", "UserName");

            return View();
        }

        [HttpPost]
        public ActionResult Create([Bind(Include = "ID,PasienId,PhoneNumber,Time,Keluhan")]Appointment appointment)
        {
            appointment.CreatedAt = DateTime.Now;

            if (ModelState.IsValid)
            {
                _repository.Create(appointment);
                //_repository.FindById(p => p.PasienId == p);
                return RedirectToAction("Details", new { id = appointment.Id });
            }

            //ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);

            return View("Create", appointment);
        }

        // GET: Appointments/Edit/5
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var appointment = _repository.FindById(id.Value);
            if (appointment == null)
            {
                return HttpNotFound();
            }

            ViewBag.Timezones = Timezones;
            return View(appointment);
        }

        // POST: /Appointments/Edit/5
        [HttpPost]
        public ActionResult Edit([Bind(Include = "ID,PasienId,PhoneNumber,Time,Timezone,Keluhan")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _repository.Update(appointment);
                return RedirectToAction("Details", new { id = appointment.Id });
            }
            return View(appointment);
        }

        // DELETE: Appointments/Delete/5
        [HttpDelete]
        public ActionResult Delete(int id)
        {
            _repository.Delete(id);
            return RedirectToAction("Index");
        }
    }
}