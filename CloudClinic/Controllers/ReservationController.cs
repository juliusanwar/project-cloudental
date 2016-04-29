using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CloudClinic.Models;

using Omu.AwesomeMvc;

using System.Data.Odbc;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;

namespace CloudClinic.Controllers
{
    public class ReservationController : Controller
    {
        private ClinicContext db = new ClinicContext();

        //public ActionResult GetPasien(int? v)
        //{
        //    var o = db.Pasien.SingleOrDefault(f => f.PasienId == v) ?? new Pasien();

        //    return Json(new KeyContent(o.PasienId, o.UserName));
        //}

        //public ActionResult Search(string search, int page)
        //{
        //    const int PageSize = 7;
        //    search = (search ?? "").ToLower().Trim();

        //    var list = db.Pasien.Where(o => o.UserName.ToLower().Contains(search));

        //    return Json(new AjaxListResult
        //    {
        //        Items = list.Skip((page - 1) * PageSize).Take(PageSize).Select(o => new KeyContent(o.PasienId, o.UserName)),
        //        More = list.Count() > page * PageSize
        //    });
        //}

        //public JsonResult GetReservation(string sidx, string sord, int page, int rows)
        //{
        //    int pageIndex = Convert.ToInt32(page) - 1;
        //    int pageSize = rows;
        //    var reservationResults = db.Reservation.Select(
        //            a => new
        //                {
        //                    a.ReservationId,
        //                    a.PasienId,
        //                    a.TglReservasi,
        //                    a.JadwalId
        //                    //a.namaUnik
        //                });
        //    int totalRecords = reservationResults.Count();
        //    var totalPages = (int)Math.Ceiling((float)totalRecords / (float)rows);
        //    if (sord.ToUpper() == "DESC")
        //    {
        //        reservationResults = reservationResults.OrderByDescending(s => s.JadwalId);
        //        reservationResults = reservationResults.Skip(pageIndex * pageSize).Take(pageSize);
        //    }
        //    else
        //    {
        //        reservationResults = reservationResults.OrderBy(s => s.JadwalId);
        //        reservationResults = reservationResults.Skip(pageIndex * pageSize).Take(pageSize);
        //    }
        //    var jsonData = new
        //    {
        //        total = totalPages,
        //        page,
        //        records = totalRecords,
        //        rows = reservationResults
        //    };
        //    return Json(jsonData, JsonRequestBehavior.AllowGet);
        //}

        // GET: Reservation
        public ActionResult Index()
        {
            var reservation = db.Reservation.Include(r => r.Pasien);
            return View(reservation.ToList());
        }

        // GET: Reservation/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            return View(reservation);
        }


        // GET: Reservation/Create
        public ActionResult Create()
        {
            //string currentUserId = User.Identity.GetUserId();
            //ApplicationUser currentUser = db.Pasien.FirstOrDefault(x => x.PasienId == currentUserId);

            //ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal");
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName");
            return View();
        }

        // POST: Reservation/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ReservationId,PasienId,TglReservasi,PilihanJadwal,namaUnik")] Reservation reservation)
        {
            

            if (ModelState.IsValid)
            {
                db.Reservation.Add(reservation);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
            return View(reservation);
            //return msg;
        }

        // GET: Reservation/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Reservation reservation = db.Reservation.Find(id);
            if (reservation == null)
            {
                return HttpNotFound();
            }
            //ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
            return View(reservation);
        }

        // POST: Reservation/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ReservationId,PasienId,TglReservasi,PilihanJadwal")] Reservation reservation)
        {
            if (ModelState.IsValid)
            {
                db.Entry(reservation).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
            ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
            return View(reservation);
        }

        // GET: Reservation/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            Reservation reservasi = db.Reservation.Find(id);
            db.Reservation.Remove(reservasi);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        // POST: Reservation/Delete/5
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

        //[HttpPost]
        //public string Create([Bind(Exclude = "ReservationId")] Reservation reservation)
        //{
        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Reservation.Add(reservation);
        //            db.SaveChanges();
        //            msg = "Saved Successfully";
        //        }
        //        else
        //        {
        //            msg = "Validation data not successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Error occured: " + ex.Message;
        //    }
        //    ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
        //    ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
        //    return msg;
        //}

        //public string Edit(Reservation reservation)
        //{
        //    string msg;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            db.Entry(reservation).State = EntityState.Modified;
        //            db.SaveChanges();
        //            msg = "Saved Successfully";
        //        }
        //        else
        //        {
        //            msg = "Validation data not successfully";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        msg = "Error occured: " + ex.Message;
        //    }
        //    ViewBag.JadwalId = new SelectList(db.Jadwal, "JadwalId", "PilihanJadwal", reservation.JadwalId);
        //    ViewBag.PasienId = new SelectList(db.Pasien, "PasienId", "UserName", reservation.PasienId);
        //    return msg;
        //}

        //public string Delete(int id)
        //{
        //    Reservation reservation = db.Reservation.Find(id);
        //    db.Reservation.Remove(reservation);
        //    db.SaveChanges();
        //    return "Delete Successfully";
        //}
    }
}
