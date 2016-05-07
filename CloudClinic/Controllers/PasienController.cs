using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;
using PagedList;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using CloudClinic.Models.ViewModel;
using System.Threading.Tasks;
using CloudClinic.Models.DataModel;

namespace CloudClinic.Controllers
{
    
    public class PasienController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter")]
        // GET: Pasien
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "UserName" : "";
            ViewBag.SortingDate = Sorting_Order == "TglRegistrasi" ? "TglDeskripsi" : "Date";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var pasien = from p in db.Pasien select p;

            if (!String.IsNullOrEmpty(Search_Data))
            {
                pasien = pasien.Where(p => p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
                    //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "UserName":
                    pasien = pasien.OrderByDescending(p => p.UserName);
                    break;
                case "TglRegistrasi":
                    pasien = pasien.OrderBy(p => p.TglRegistrasi);
                    break;
                case "TglDeskripsi":
                    pasien = pasien.OrderByDescending(p => p.TglRegistrasi);
                    break;
                default:
                    pasien = pasien.OrderBy(p => p.UserName);
                    break;
            }

            int Size_Of_Page = 4;
            int No_Of_Page = (Page_No ?? 1);



            return View(pasien.ToPagedList(No_Of_Page, Size_Of_Page));

            //return View(pasien.ToList());
        }

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Pasien/Details/5
        public ActionResult Details(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasien pasien = db.Pasien.Find(id);
            if (pasien == null)
            {
                return HttpNotFound();
            }
            return View(pasien);
        }

        // GET: Pasien/Create
        [Authorize(Roles = "Admin")]
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

        // POST: Pasien/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(PasienViewModel pasien)
        {
            if (ModelState.IsValid)
            {
                //create new pasien
                var user = new ApplicationUser { UserName = pasien.UserName, Email = pasien.Email };
                var result = await UserManager.CreateAsync(user, pasien.Password);
                if (result.Succeeded)
                {
                    //insert new pasien
                    var newPasien = new Pasien
                    {
                        PasienId = pasien.PasienId,
                        UserName = user.UserName,
                        Nama = pasien.Nama,
                        TglLhr = pasien.TglLhr,
                        Gender = pasien.Gender,
                        GolDarah = pasien.GolDarah,
                        Kota = pasien.Kota,
                        Alamat = pasien.Alamat,
                        Telp = pasien.Telp,
                        Email = pasien.Email,
                        TglRegistrasi = pasien.TglRegistrasi,
                        RiwayatSakit = pasien.RiwayatSakit
                        
                    };

                    db.Pasien.Add(newPasien);
                    db.SaveChanges();

                    //RedirectToAction("Index");
                    ViewBag.Pesan = "Berhasil menambahkan Pasien baru";
                    
                }
                else
                {
                    ViewBag.Error = result.Errors;
                }
            }
            ModelState.Clear();
            return View(pasien);
        }

        // GET: Pasien/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pasien pasien = db.Pasien.Find(id);
            if (pasien == null)
            {
                return HttpNotFound();
            }
            return View(pasien);
        }

        // POST: Pasien/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PasienId,UserName,Nama,TglLhr,Gender,GolDarah,Alamat,Kota,Telp,TglRegistrasi,RiwayatSakit,Email")] Pasien pasien)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pasien).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pasien);
        }

        // GET: Pasien/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            Pasien pasien = db.Pasien.Find(id);
            db.Pasien.Remove(pasien);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Pasien/Delete/5
        [HttpPost]
        [Authorize(Roles = "Admin")]
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
