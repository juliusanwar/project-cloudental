using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;
using CloudClinic.Models.ViewModel;
using System.Threading.Tasks;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace CloudClinic.Controllers
{
    public class DoctorsController : Controller
    {
        /*private ClinicContext db = new ClinicContext();

        // GET: Doctorsm 
        public ActionResult Index()
        {
            //IEnumerable<DoctorViewModel> model = from d in db.Doctors
            //                                     select new DoctorViewModel
            //                                     {
            //                                         DoctorId = d.DoctorId,
            //                                         UserName = d.UserName,
            //                                         Password = "",
            //                                         Repassword = "",
            //                                         Nama = d.Nama,
            //                                         Alamat = d.Alamat,
            //                                         Telp = d.Telp,
            //                                         Email = d.Email
            //                                     };
            //return Json(model,JsonRequestBehavior.AllowGet);
            return View();
        }

        // GET: Doctors/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // GET: Doctors/Create
        public ActionResult Create()
        {
            return View();
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // POST: Doctors/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(DoctorViewModel doctor)
        {
            if (ModelState.IsValid)
            {
                //create new doctor
                var user = new ApplicationUser { UserName = doctor.UserName, Email = doctor.Email };
                var result = await UserManager.CreateAsync(user, doctor.Password);
                if(result.Succeeded)
                {
                    //insert new dokter
                    var newDokter = new Doctor
                    {
                        DoctorId = doctor.DoctorId,
                        UserName = user.UserName,
                        Nama = doctor.Nama,
                        Alamat = doctor.Alamat,
                        Telp = doctor.Telp,
                        Email=doctor.Email
                    };

                    db.Doctors.Add(newDokter);
                    db.SaveChanges();

                    ViewBag.Pesan = "Berhasil menambahkan Dokter baru";
                }
                else
                {
                    ViewBag.Error = result.Errors;
                }
            }

            return View(doctor);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DoctorId,UserName,Nama,Alamat,Telp,Email")] Doctor doctor)
        {
            if (ModelState.IsValid)
            {
                db.Entry(doctor).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(doctor);
        }

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<DoctorViewModel> model = from d in db.Doctors
                                                 select new DoctorViewModel
                                                 {
                                                     DoctorId = d.DoctorId,
                                                     UserName = d.UserName,
                                                     Password = "default",
                                                     Repassword = "default",
                                                     Nama = d.Nama,
                                                     Alamat = d.Alamat,
                                                     Telp = d.Telp,
                                                     Email = d.Email
                                                 };


            return Json(model.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, DoctorViewModel doctor)
        {
            if (doctor != null && ModelState.IsValid)
            {
                //create new doctor
                var user = new ApplicationUser { UserName = doctor.UserName, Email = doctor.Email };
                var result = UserManager.Create(user, doctor.Password);
                if (result.Succeeded)
                {
                    //insert new dokter
                    var newDokter = new Doctor
                    {
                        DoctorId = doctor.DoctorId,
                        UserName = user.UserName,
                        Nama = doctor.Nama,
                        Alamat = doctor.Alamat,
                        Telp = doctor.Telp,
                        Email = doctor.Email
                    };

                    db.Doctors.Add(newDokter);
                    db.SaveChanges();

                    ViewBag.Pesan = "Berhasil menambahkan Dokter baru";
                }
                else
                {
                    ViewBag.Error = result.Errors;
                }
            }
            return Json(new[] { doctor }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, DoctorViewModel doctor)
        {
            if (ModelState.IsValid)
            {
               
                var editDoctor = db.Doctors.Where(d => d.DoctorId == doctor.DoctorId).SingleOrDefault();
                if(editDoctor!=null)
                {
                    editDoctor.UserName = doctor.UserName;
                    editDoctor.Nama = doctor.Nama;
                    editDoctor.Alamat = doctor.Alamat;
                    editDoctor.Telp = doctor.Telp;
                    editDoctor.Email = doctor.Email;

                    db.SaveChanges();
                }
            }

            return Json(new[] { doctor }.ToDataSourceResult(request, ModelState));
        }

        //[AcceptVerbs(HttpVerbs.Post)]
        //public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, DoctorViewModel dokter)
        //{
        //    if (dokter != null)
        //    {
                
        //    }

        //    return Json(new[] { dokter }.ToDataSourceResult(request, ModelState));
        //}


        // GET: Doctors/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Doctor doctor = db.Doctors.Find(id);
            if (doctor == null)
            {
                return HttpNotFound();
            }
            return View(doctor);
        }

        // POST: Doctors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Doctor doctor = db.Doctors.Find(id);
            db.Doctors.Remove(doctor);
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
    } */
    }
}
