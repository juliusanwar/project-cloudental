using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using CloudClinic.Models.ViewModel;
using System.Threading.Tasks;

namespace CloudClinic.Controllers
{
    public class PenggunaSalahController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Penggunas
        public ActionResult Index()
        {
            IEnumerable<UserViewModel> model = from d in db.Pengguna 
                                               select new UserViewModel
                                                 {
                                                     PenggunaId = d.PenggunaId,
                                                     UserName = d.UserName,
                                                     Password = "",
                                                     Repassword = "",
                                                     Nama = d.Nama,
                                                     Alamat = d.Alamat,
                                                     Telp = d.Telp,
                                                     Email = d.Email
                                                 };
            return Json(model, JsonRequestBehavior.AllowGet);
            return View();
        }

        // GET: Penggunas/Details/5
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

        // GET: Penggunas/Create
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

        // POST: Penggunas/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                //create new doctor
                var pengguna = new ApplicationUser { UserName = user.UserName, Email = user.Email };
                var result = await UserManager.CreateAsync(pengguna, user.Password);
                if (result.Succeeded)
                {
                    //insert new dokter
                    var newPengguna = new Pengguna
                    {
                        PenggunaId = user.PenggunaId,
                        UserName = user.UserName,
                        Nama = user.Nama,
                        Alamat = user.Alamat,
                        Telp = user.Telp,
                        Email = user.Email
                    };

                    db.Pengguna.Add(newPengguna);
                    db.SaveChanges();

                    ViewBag.Pesan = "Berhasil menambahkan Dokter/Perawat baru";
                }
                else
                {
                    ViewBag.Error = result.Errors;
                }
            }

            return View(user);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "PenggunaId,UserName,Atruan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.Pengguna.Add(pengguna);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }

        //    return View(pengguna);
        //}

        // GET: Penggunas/Edit/5
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

        // POST: Penggunas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PenggunaId,UserName,Atruan,Nama,Kota,Alamat,Telp,Email")] Pengguna pengguna)
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
            IEnumerable<UserViewModel> model = from d in db.Pengguna
                                               select new UserViewModel
                                                 {
                                                     PenggunaId = d.PenggunaId,
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
        public ActionResult EditingPopup_Create([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (user != null && ModelState.IsValid)
            {
                //create new doctor
                var pengguna = new ApplicationUser { UserName = user.UserName, Email = user.Email };
                var result = UserManager.Create(pengguna, user.Password);
                if (result.Succeeded)
                {
                    //insert new dokter
                    var newPengguna = new Pengguna
                    {
                        PenggunaId = user.PenggunaId,
                        UserName = user.UserName,
                        Nama = user.Nama,
                        Alamat = user.Alamat,
                        Telp = user.Telp,
                        Email = user.Email
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
            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Update([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (ModelState.IsValid)
            {

                var editDoctor = db.Pengguna.Where(d => d.PenggunaId == user.PenggunaId).SingleOrDefault();
                if (editDoctor != null)
                {
                    editDoctor.UserName = user.UserName;
                    editDoctor.Nama = user.Nama;
                    editDoctor.Alamat = user.Alamat;
                    editDoctor.Telp = user.Telp;
                    editDoctor.Email = user.Email;

                    db.SaveChanges();
                }
            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingPopup_Destroy([DataSourceRequest] DataSourceRequest request, UserViewModel user)
        {
            if (user != null)
            {

            }

            return Json(new[] { user }.ToDataSourceResult(request, ModelState));
        }

        // GET: Penggunas/Delete/5
        public ActionResult Delete(int? id)
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

        // POST: Penggunas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
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
    }
}
