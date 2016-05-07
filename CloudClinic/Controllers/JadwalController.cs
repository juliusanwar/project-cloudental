﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;
using CloudClinic.Models.ViewModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;

namespace CloudClinic.Controllers
{
    
    public class JadwalController : Controller
    {
        private ClinicContext db = new ClinicContext();

        //[Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Jadwal
        public ActionResult Index()
        {
            return View(db.Jadwal.ToList());
        }

        //[Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Jadwal/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadwal jadwal = db.Jadwal.Find(id);
            if (jadwal == null)
            {
                return HttpNotFound();
            }
            return View(jadwal);
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
        private UserManager<ApplicationUser> manager;
        public JadwalController()
        {
            manager = new UserManager<ApplicationUser>(
                new UserStore<ApplicationUser>(db));
        }


        // GET: Jadwal/Create
        //[Authorize(Roles = "Admin,Dokter")]
        public ActionResult Create()
        {
            var user = from p in db.Pengguna
                       where p.UserName == User.Identity.Name
                       select p.PenggunaId;

            var model = new Jadwal
            {
                PenggunaId = user.First()
            };


            //ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "Nama");
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Jadwal jadwal)
        {
                if (ModelState.IsValid)
                {
                    //insert new pasien
                    var newJadwal = new Jadwal
                    {
                        JadwalId = jadwal.JadwalId,
                        PenggunaId = jadwal.PenggunaId,
                        TanggalJadwal = jadwal.TanggalJadwal,
                        Sesi = jadwal.Sesi,
                        Ruang = jadwal.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();

                    //RedirectToAction("Index");
                    ViewBag.Pesan = "Berhasil menambahkan jadwal baru!";

                }
                else
                {
                    ViewBag.Error = "Jadwal tidak berhasil dimasukkan!!!";
                }
            ModelState.Clear();
            return View(jadwal);
        }

        // POST: Jadwal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "JadwalId,PenggunaId,TanggalJadwal,Ruang,Sesi")] Jadwal jadwal)
        //{
            
        //    if (ModelState.IsValid)
        //    {
        //        string currentUserId = User.Identity.Name;
        //        var currentUser = from c in db.Pengguna
        //                          where c.PenggunaId == User.Identity.GetUserId<int>()
        //                          select c;
        //        //jadwal.Pengguna.UserName = currentUser;
        //        var newJadwal = new Jadwal
        //        {
        //            JadwalId = jadwal.JadwalId,
        //            PenggunaId = jadwal.PenggunaId,
        //            TanggalJadwal = jadwal.TanggalJadwal,
        //            Sesi = jadwal.Sesi,
        //            Ruang = jadwal.Ruang
        //        };


        //        db.Jadwal.Add(jadwal);
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
        //    //ViewBag.PenggunaId = new SelectList(db.Pengguna, "PenggunaId", "Nama", jadwal.PenggunaId);

        //    return View(jadwal);
        //}


        // GET: Jadwal/Edit/5
        //[Authorize(Roles = "Admin,Dokter")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Jadwal jadwal = db.Jadwal.Find(id);
            if (jadwal == null)
            {
                return HttpNotFound();
            }
            return View(jadwal);
        }

        

        // POST: Jadwal/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JadwalId,PenggunaId,TanggalJadwal,Ruang,Sesi")] Jadwal jadwal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jadwal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jadwal);
        }


        // GET: Jadwal/Delete/5
        //[Authorize(Roles = "Admin,Dokter")]
        public ActionResult Delete(int? id)
        {
            Jadwal jadwal = db.Jadwal.Find(id);
            db.Jadwal.Remove(jadwal);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Jadwal/Delete/5
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
