﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;

namespace CloudClinic.Controllers
{
    public class JadwalController : Controller
    {
        private ClinicContext db = new ClinicContext();

        // GET: Jadwal
        public ActionResult Index()
        {
            return View(db.Jadwal.ToList());
        }

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

        [Authorize(Users = "jul@jul.com")]
        // GET: Jadwal/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Jadwal/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "JadwalId,PilihanJadwal")] Jadwal jadwal)
        {
            if (ModelState.IsValid)
            {
                db.Jadwal.Add(jadwal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(jadwal);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Jadwal/Edit/5
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
        public ActionResult Edit([Bind(Include = "JadwalId,PilihanJadwal")] Jadwal jadwal)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jadwal).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(jadwal);
        }

        [Authorize(Users = "jul@jul.com")]
        // GET: Jadwal/Delete/5
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
