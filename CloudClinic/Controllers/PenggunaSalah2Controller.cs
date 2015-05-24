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
    public class PenggunaSalah2Controller : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Doctorsm 
        public ActionResult Index()
        {
            //IEnumerable<UserViewModel> model = from p in db.Pengguna
            //                                   select new UserViewModel
            //                                   {
            //                                       PenggunaId = p.PenggunaId,
            //                                       UserName = p.UserName,
            //                                       Password = "",
            //                                       Repassword = "",
            //                                       Aturan = p.Aturan,
            //                                       Nama = p.Nama,
            //                                       Kota = p.Kota,
            //                                       Alamat = p.Alamat,
            //                                       Telp = p.Telp,
            //                                       Email = p.Email
            //                                   };
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
            Pengguna pengguna = db.Pengguna.Find(id);
            if (pengguna == null)
            {
                return HttpNotFound();
            }
            return View(pengguna);
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


        // POST: Pengguna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel pengguna)
        {
            if (ModelState.IsValid)
            {
                //create new pengguna
                var user = new ApplicationUser { UserName = pengguna.UserName, Email = pengguna.Email };
                var result = await UserManager.CreateAsync(user, pengguna.Password);
                if(result.Succeeded)
                {
                    //insert new pengguna
                    var newPengguna = new Pengguna
                    {
                        PenggunaId = pengguna.PenggunaId,
                        UserName = user.UserName,
                        Aturan = pengguna.Aturan,
                        Nama = pengguna.Nama,
                        Kota = pengguna.Kota,
                        Alamat = pengguna.Alamat,
                        Telp = pengguna.Telp,
                        Email=pengguna.Email
                    };

                    db.Pengguna.Add(newPengguna);
                    db.SaveChanges();

                    ViewBag.Pesan = "Berhasil menambahkan Dokter baru";
                }
                else
                {
                    ViewBag.Error = result.Errors;
                }
            }

            return View(pengguna);
        }

        // GET: Doctors/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengguna pengguna = db.Pengguna.Find(id);
            if (pengguna == null)
            {
                return HttpNotFound();
            }
            return View(pengguna);
        }

        // POST: Doctors/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenggunaId,UserName,Aturan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pengguna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pengguna);
        }

        public ActionResult EditingPopup_Read([DataSourceRequest] DataSourceRequest request)
        {
            IEnumerable<UserViewModel> model = from p in db.Pengguna
                                                 select new UserViewModel
                                                 {
                                                     PenggunaId = p.PenggunaId,
                                                     UserName = p.UserName,
                                                     Password = "default",
                                                     Repassword = "default",
                                                     Aturan = p.Aturan,
                                                     Nama = p.Nama,
                                                     Kota = p.Kota,
                                                     Alamat = p.Alamat,
                                                     Telp = p.Telp,
                                                     Email = p.Email
                                                 };


            return Json(model.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, UserViewModel pengguna)
        {
            if (pengguna != null && ModelState.IsValid)
            {
                //create new doctor
                var user = new ApplicationUser { UserName = pengguna.UserName, Email = pengguna.Email };
                var result = UserManager.Create(user, pengguna.Password);
                if (result.Succeeded)
                {
                    //insert new dokter
                    var newPengguna = new Pengguna
                    {
                        PenggunaId = pengguna.PenggunaId,
                        UserName = user.UserName,
                        Aturan = pengguna.Aturan,
                        Nama = pengguna.Nama,
                        Kota = pengguna.Kota,
                        Alamat = pengguna.Alamat,
                        Telp = pengguna.Telp,
                        Email = pengguna.Email
                    };

                    db.Pengguna.Add(newPengguna);
                    db.SaveChanges();

                    ViewBag.Pesan = "Berhasil menambahkan Dokter baru";
                }
                else
                {
                    ViewBag.Error = result.Errors;
                }
            }
            return Json(new[] { pengguna }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, UserViewModel pengguna)
        {
            if (ModelState.IsValid)
            {
               
                var editPengguna = db.Pengguna.Where(p => p.PenggunaId == pengguna.PenggunaId).SingleOrDefault();
                if(editPengguna!=null)
                {
                    editPengguna.UserName = pengguna.UserName;
                    editPengguna.Aturan = pengguna.Aturan;
                    editPengguna.Nama = pengguna.Nama;
                    editPengguna.Kota = pengguna.Kota;
                    editPengguna.Alamat = pengguna.Alamat;
                    editPengguna.Telp = pengguna.Telp;
                    editPengguna.Email = pengguna.Email;

                    db.SaveChanges();
                }
            }

            return Json(new[] { pengguna }.ToDataSourceResult(request, ModelState));
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
            Pengguna doctor = db.Pengguna.Find(id);
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
            Pengguna pengguna = db.Pengguna.Find(id);
            db.Pengguna.Remove(pengguna);
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
    


        /*private ClinicContext db = new ClinicContext();

        // GET: Pengguna
        public ActionResult Index()
        {
            return View(db.Pengguna.ToList());
        }

        // GET: Pengguna/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengguna pengguna = db.Pengguna.Find(id);
            if (pengguna == null)
            {
                return HttpNotFound();
            }
            return View(pengguna);
        }

        // GET: Pengguna/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pengguna/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PenggunaId,UserName,Aturan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
        {
            if (ModelState.IsValid)
            {
                db.Pengguna.Add(pengguna);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pengguna);
        }

        // GET: Pengguna/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pengguna pengguna = db.Pengguna.Find(id);
            if (pengguna == null)
            {
                return HttpNotFound();
            }
            return View(pengguna);
        }

        // POST: Pengguna/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenggunaId,UserName,Aturan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pengguna).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pengguna);
        }

        // GET: Pengguna/Delete/5
        public ActionResult Delete(int? id)
        {
            Pengguna pengguna = db.Pengguna.Find(id);
            db.Pengguna.Remove(pengguna);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Pengguna/Delete/5
        [HttpPost]
        public ActionResult Delete(int id)
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
        }*/
    }
}
