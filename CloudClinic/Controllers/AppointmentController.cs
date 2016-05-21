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
using System.Net.Mail;
using System.Web.Helpers;
using System.Net.Http;
using System.Globalization;
using System.Threading;

namespace CloudClinic.Controllers
{
    
    public class AppointmentController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Appointment
        public ActionResult Index()
        {
            var appointment = db.Appointment.OrderByDescending(a => a.Jadwal.TanggalJadwal).OrderBy(a => a.Jadwal.TanggalJadwal).OrderBy(a => a.Jadwal.Sesi).Include(a => a.Jadwal).Include(a => a.Pasien);
            return View(appointment.ToList());           
        }

        public ActionResult ShowTodayRecord()
        {
            DateTime today;
            today = DateTime.Today;

            var setNow = from s in db.Jadwal
                         where s.TanggalJadwal == today
                         select s.TanggalJadwal;

            var appointment = db.Appointment.Where(a => a.Jadwal.TanggalJadwal == setNow.FirstOrDefault()).OrderBy(a => a.Jadwal.Sesi).Include(a => a.Jadwal).Include(a => a.Pasien);
            return View(appointment.ToList());
        }
        //[HttpGet]
        public ActionResult ShowTommorowRecord()
        {
            DateTime today;
            today = DateTime.Today.AddDays(1);

            var setNow = from s in db.Jadwal
                         where s.TanggalJadwal == today
                         select s.TanggalJadwal;

            var appointment = db.Appointment.Where(a => a.Jadwal.TanggalJadwal == setNow.FirstOrDefault()).OrderBy(a => a.Jadwal.Sesi).Include(a => a.Jadwal).Include(a => a.Pasien);
            return View(appointment.ToList());
        }

        public ActionResult CloseSession(int? id)
        {
            Appointment appointment = db.Appointment.Find(id);

            //var model = new AppointmentViewModel();
            var emailAdd = from e in db.Appointment
                           where e.Jadwal.TanggalJadwal == DateTime.Today
                           select e.Email.ToList();

            var nextID = db.Appointment.OrderBy(i => i.Id)
                     .SkipWhile(i => i.Id != id)
                     .Skip(1)
                     .Select(i => i.Id);

            ViewBag.NextID = nextID;

            #region Convert Session To Time
            var sesi1 = "08:00 - 08:30";
            var sesi2 = "08:30 - 09:00";
            var sesi3 = "09:00 - 09:30";
            var sesi4 = "09:30 - 10:00";
            var sesi5 = "10:00 - 10:30";

            var sesi6 = "16:00 - 16:30";
            var sesi7 = "16:30 - 17:00";
            var sesi8 = "17:00 - 17:30";
            var sesi9 = "17:30 - 18:00";
            var sesi10 = "18:00 - 18:30";


            if (appointment.Jadwal.Sesi == "Time1")
            {
                appointment.Jadwal.Sesi = sesi1;
            }
            if (appointment.Jadwal.Sesi == "Time2")
            {
                appointment.Jadwal.Sesi = sesi2;
            }
            if (appointment.Jadwal.Sesi == "Time3")
            {
                appointment.Jadwal.Sesi = sesi3;
            }
            if (appointment.Jadwal.Sesi == "Time4")
            {
                appointment.Jadwal.Sesi = sesi4;
            }
            if (appointment.Jadwal.Sesi == "Time5")
            {
                appointment.Jadwal.Sesi = sesi5;
            }
            if (appointment.Jadwal.Sesi == "Time6")
            {
                appointment.Jadwal.Sesi = sesi6;
            }
            if (appointment.Jadwal.Sesi == "Time7")
            {
                appointment.Jadwal.Sesi = sesi7;
            }
            if (appointment.Jadwal.Sesi == "Time8")
            {
                appointment.Jadwal.Sesi = sesi8;
            }
            if (appointment.Jadwal.Sesi == "Time9")
            {
                appointment.Jadwal.Sesi = sesi9;
            }
            if (appointment.Jadwal.Sesi == "Time10")
            {
                appointment.Jadwal.Sesi = sesi10;
            }
            #endregion

            //Configuring webMail class to send emails  
            //gmail smtp server  
            WebMail.SmtpServer = "smtp.gmail.com";
            //gmail port to send emails  
            WebMail.SmtpPort = 587;
            WebMail.SmtpUseDefaultCredentials = true;
            //sending emails with secure protocol  
            WebMail.EnableSsl = true;
            //EmailId used to send emails from application  
            WebMail.UserName = "rovski77@gmail.com";
            WebMail.Password = "aqkerenz23";

            //Sender email address.  
            WebMail.From = "rovski77@gmail.com";

            var callbackUrl = Url.Action(
               "Edit", "Appointment",
               new { Id = appointment.Id },
               protocol: Request.Url.Scheme);

            //Send email  
            WebMail.Send(
            to: appointment.Email,
            subject: "Booking Appointment at Cloudental with ID number : " + appointment.namaUnik,
            body: "Halo Pelanggan Setia Kami di Cloudental.<br /> Sebelumnya Anda pernah mendaftar reservasi pada: <br/ > Tanggal : "
            + appointment.Jadwal.TanggalJadwal.ToShortDateString()
            + ", <br /> Sesi : " + appointment.Jadwal.Sesi
            + "<br /><br />"
            + "Apakah anda bersedia melakukan perubahaan jadwal ? <br />"
            + "Jika anda berniat untuk melakukan perubahaan jadwal maka anda perlu melakukan konfirmasi seperti dibawah ini: <br /><br />"
            + "Konfirmasi ketersediaan untuk melakukan perubahaan jadwal dengan klik link ini : <a href=\""
                                            + callbackUrl + "\">link</a>",
            isBodyHtml: true);            
            
            return RedirectToAction("ShowTodayRecord", appointment);
        }

        public ActionResult SendNotification(int? id)
        {
            Appointment appointment = db.Appointment.Find(id);

            //var model = new AppointmentViewModel();
            var emailAdd = from e in db.Pasien
                           where e.Email == appointment.Email
                           select e.Email;


            #region Convert Session To Time
            var sesi1 = "08:00 - 08:30";
            var sesi2 = "08:30 - 09:00";
            var sesi3 = "09:00 - 09:30";
            var sesi4 = "09:30 - 10:00";
            var sesi5 = "10:00 - 10:30";

            var sesi6 = "16:00 - 16:30";
            var sesi7 = "16:30 - 17:00";
            var sesi8 = "17:00 - 17:30";
            var sesi9 = "17:30 - 18:00";
            var sesi10 = "18:00 - 18:30";


            if (appointment.Jadwal.Sesi == "Time1")
            {
                appointment.Jadwal.Sesi = sesi1;
            }
            if (appointment.Jadwal.Sesi == "Time2")
            {
                appointment.Jadwal.Sesi = sesi2;
            }
            if (appointment.Jadwal.Sesi == "Time3")
            {
                appointment.Jadwal.Sesi = sesi3;
            }
            if (appointment.Jadwal.Sesi == "Time4")
            {
                appointment.Jadwal.Sesi = sesi4;
            }
            if (appointment.Jadwal.Sesi == "Time5")
            {
                appointment.Jadwal.Sesi = sesi5;
            }
            if (appointment.Jadwal.Sesi == "Time6")
            {
                appointment.Jadwal.Sesi = sesi6;
            }
            if (appointment.Jadwal.Sesi == "Time7")
            {
                appointment.Jadwal.Sesi = sesi7;
            }
            if (appointment.Jadwal.Sesi == "Time8")
            {
                appointment.Jadwal.Sesi = sesi8;
            }
            if (appointment.Jadwal.Sesi == "Time9")
            {
                appointment.Jadwal.Sesi = sesi9;
            }
            if (appointment.Jadwal.Sesi == "Time10")
            {
                appointment.Jadwal.Sesi = sesi10;
            }
            #endregion

            //Configuring webMail class to send emails  
            //gmail smtp server  
            WebMail.SmtpServer = "smtp.gmail.com";
            //gmail port to send emails  
            WebMail.SmtpPort = 587;
            WebMail.SmtpUseDefaultCredentials = true;
            //sending emails with secure protocol  
            WebMail.EnableSsl = true;
            //EmailId used to send emails from application  
            WebMail.UserName = "rovski77@gmail.com";
            WebMail.Password = "aqkerenz23";

            //Sender email address.  
            WebMail.From = "rovski77@gmail.com";

            var callbackUrl = Url.Action(
               "Edit", "Appointment",
               new { Id = appointment.Id },
               protocol: Request.Url.Scheme);

            //Send email  
            WebMail.Send(
                to: appointment.Email,
                subject: "Booking Appointment at Cloudental with ID number : " + appointment.namaUnik,
                body: "Halo Pelanggan Setia Kami di Cloudental.<br /> Sebelumnya Anda pernah mendaftar reservasi pada: <br/ > Tanggal : "
                + appointment.Jadwal.TanggalJadwal.ToShortDateString()
                + ", <br /> Sesi : " + appointment.Jadwal.Sesi
                + "<br /><br />"
                + "Apakah anda bersedia melakukan perubahaan jadwal ? <br />"
                + "Jika anda berniat untuk melakukan perubahaan jadwal maka anda perlu melakukan konfirmasi seperti dibawah ini: <br /><br />"
                + "Konfirmasi ketersediaan untuk melakukan perubahaan jadwal dengan klik link ini : <a href=\""
                                               + callbackUrl + "\">link</a>",
                isBodyHtml: true);
            return RedirectToAction("ShowTommorowRecord");
        }

        

        public ActionResult Sent()
        {            
            return View();
        }



        [Authorize(Roles = "Pasien")]
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

        [Authorize(Roles = "Pasien")]
        [HttpGet]
        public ActionResult Create()
        {
            var model = new AppointmentViewModel();
            model.IsTimeShowed = false;
            model.Date = DateTime.Now;
            return View(model);
        }        

        [Authorize(Roles = "Pasien")]
        [HttpGet]
        public async Task<ActionResult> Check(string date, string phoneNumber, string keluhan, string email)
        {
            DateTime choosenDate;
            
            
            var model = new AppointmentViewModel();

            if (DateTime.TryParse(date, out choosenDate))
            {
                model.Date = choosenDate;
                model.IsTimeShowed = true;

                using (var ctx = new ClinicContext())
                {                   
                    var availableJadwal = await ctx.Jadwal.Where(
                        x => DbFunctions.TruncateTime
                        (x.TanggalJadwal) == DbFunctions.TruncateTime(choosenDate)
                        && x.Appointment == null)
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
            model.Email = email;
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

            if (model.Date.Date < DateTime.Today)
            {
                ModelState.AddModelError("Date", "Silahkan pilih tanggal hari ini atau besok.");
                return View(model);
            }            

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

            if (String.IsNullOrEmpty(model.Email))
            {
                ModelState.AddModelError("Email", "Silahkan masukkan alamat email anda.");
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

                    var jadwal = ctx.Jadwal.FirstOrDefault(x => DbFunctions.TruncateTime(x.TanggalJadwal) == DbFunctions.TruncateTime(model.Date)
                                            && x.Sesi.Equals(model.Session, StringComparison.OrdinalIgnoreCase));
                    appointment.Jadwal = jadwal;

                    ctx.Appointment.Add(appointment);
                    await ctx.SaveChangesAsync();
                        
                    //return RedirectToAction("Sent");
                    
                }
            }

            #region Send Email
            var emailAdd = from e in db.Pasien
                           where e.UserName == User.Identity.Name
                           select e.Email;

            //Configuring webMail class to send emails  
            //gmail smtp server  
            WebMail.SmtpServer = "smtp.gmail.com";
            //gmail port to send emails  
            WebMail.SmtpPort = 587;
            WebMail.SmtpUseDefaultCredentials = true;
            //sending emails with secure protocol  
            WebMail.EnableSsl = true;
            //EmailId used to send emails from application  
            WebMail.UserName = "rovski77@gmail.com";
            WebMail.Password = "aqkerenz23";

            //Sender email address.  
            WebMail.From = "rovski77@gmail.com";

            //Send email  
            WebMail.Send(
                to: model.Email, 
                subject: "Booking Appointment at Cloudental with ID number : " + model.Id, 
                body: "Halo Pelanggan Setia Kami di Cloudental, <br /> Anda baru saja melakukan reservasi klinik kami pada: "
                + "<br /><br /> Tanggal: " + model.Date.ToShortDateString()
                + ", <br /> Sesi : " + model.Session
                + "<br /> Dimohon untuk pasien dateng pada jadwal sesi yang telah dipilih. <br /> Atas kerjasamanya kami ucapkan terima kasih."
                + "<br /><br /> Hormat kami,"
                + "<br /><br /><br /><br />"
                + "Cloudental",
                isBodyHtml: true);
            //ViewBag.Status = "Email Sent Successfully.";
        

            return RedirectToAction("Sent");
            #endregion
        }

        // GET: Appointment/Edit/5
        [HttpGet]
        public ActionResult Edit(int id)
        {
            #region Sanity Get
            var getId = from g in db.Appointment
                        where g.Id == id
                        select g.Id;

            var getPasien = from p in db.Appointment
                            where p.Pasien.UserName == User.Identity.Name
                            select p.PasienId;

            var getPhone = from h in db.Appointment
                           where h.Id == id
                           select h.PhoneNumber;

            var getKeluhan = from h in db.Appointment
                           where h.Id == id
                           select h.Keluhan;

            var getEmail = from h in db.Appointment
                             where h.Id == id
                             select h.Email;

            var getDate = from h in db.Appointment
                           where h.Id == id
                           select h.Jadwal.TanggalJadwal;
            #endregion

            var model = new AppointmentViewModel();
            model.Id = getId.FirstOrDefault();
            model.PasienId = getPasien.FirstOrDefault();
            model.PhoneNumber = getPhone.FirstOrDefault();
            model.Keluhan = getKeluhan.FirstOrDefault();
            model.Email = getEmail.FirstOrDefault();
            model.IsTimeShowed = false;
            model.Date = getDate.FirstOrDefault();
            return View(model);
        }

        //[Authorize(Roles = "Pasien")]
        [HttpGet]
        public async Task<ActionResult> Checked(int id,int pid,string date, string phoneNumber, string keluhan, string email)
        {
            DateTime choosenDate;


            var model = new AppointmentViewModel();

            if (DateTime.TryParse(date, out choosenDate))
            {
                model.Date = choosenDate;
                model.IsTimeShowed = true;

                using (var ctx = new ClinicContext())
                {
                    var availableJadwal = await ctx.Jadwal.Where(
                        x => DbFunctions.TruncateTime
                        (x.TanggalJadwal) == DbFunctions.TruncateTime(choosenDate)
                        && x.Appointment == null)
                        .ToListAsync();

                    var getSesi = from g in db.Appointment
                                  where g.Id == id
                                  select g.Jadwal.Sesi;

                    if (availableJadwal.Any())
                    {
                        foreach (var jadwal in availableJadwal)
                        {
                            switch (jadwal.Sesi)
                            {
                                #region Sanity IsTimeAvailable
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
                                    #endregion
                            }

                        }
                    }
                }
            }

            model.Id = id;
            model.PasienId = pid;
            model.PhoneNumber = phoneNumber;
            model.Email = email;
            model.Keluhan = keluhan;

            ModelState.Remove("Date");

            return View("Edit", model);
        }

        // POST: Appointment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(AppointmentViewModel model)
        {

            var appointment = this.PopulateEditAppointmentDataModel(model);

            using (ClinicContext ctx = new ClinicContext())
            {
                var pasien = ctx.Pasien.FirstOrDefault(
                    x => x.UserName.Equals(
                        this.User.Identity.Name,
                        StringComparison.OrdinalIgnoreCase));

                if (pasien != null)
                {
                    model.PasienId = pasien.PasienId;

                    var jadwal = ctx.Jadwal.FirstOrDefault(
                        x => DbFunctions.TruncateTime(
                            x.TanggalJadwal) == DbFunctions.TruncateTime(model.Date)
                            && x.Sesi.Equals(model.Session,
                            StringComparison.OrdinalIgnoreCase));

                    appointment.Jadwal = jadwal;

                    ctx.Entry(appointment).State = EntityState.Modified;
                    //ctx.Appointment.Add(appointment);
                    await ctx.SaveChangesAsync();

                }

            }
            return RedirectToAction("Index");

            //if (ModelState.IsValid)
            //{
            //    db.Entry(appointment).State = EntityState.Modified;
            //    db.SaveChanges();
            //    return RedirectToAction("Index");
            //}
            //ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "Hari", appointment.JadwalId);
            //ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", appointment.PasienId);
            //return View(appointment);
        }

        //GET: Appointment/Delete/5
        public ActionResult Delete(int? id)
        {
            Appointment app = db.Appointment.Find(id);
            db.Appointment.Remove(app);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Appointment/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Appointment appointment = db.Appointment.Find(id);
        //    db.Appointment.Remove(appointment);
        //    db.SaveChanges();
        //    return RedirectToAction("Index");
        //}

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
            appointment.Email = model.Email;
            appointment.Keluhan = model.Keluhan;
            appointment.CreatedAt = DateTime.Now;
            return appointment;
        }

        private Appointment PopulateEditAppointmentDataModel(AppointmentViewModel model)
        {
            var appointment = new Appointment();
            appointment.Id = model.Id;
            appointment.PasienId = model.PasienId;
            appointment.Email = model.Email;
            appointment.PhoneNumber = model.PhoneNumber;
            appointment.Keluhan = model.Keluhan;
            appointment.CreatedAt = DateTime.Now;
            return appointment;
        }
    }

    public class AppointmentViewModel
    {
        public int Id { get; set; } 

        [Required]
        public int PasienId {get;set;}

        [Required]
        [DisplayFormatAttribute(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
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

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Keluhan { get; set; }
    }
}
