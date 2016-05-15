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
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using PagedList;

namespace CloudClinic.Controllers
{
    
    public class JadwalController : Controller
    {
        private ClinicContext db = new ClinicContext();

        [Authorize(Roles = "Admin,Dokter,Pasien")]
        // GET: Jadwal
        public ActionResult Index(string Sorting_Order, string Search_Data, string Filter_Value, int? Page_No)
        {
            ViewBag.CurrentSortOrder = Sorting_Order;
            ViewBag.SortingName = String.IsNullOrEmpty(Sorting_Order) ? "Ruang" : "";

            if (Search_Data != null)
            {
                Page_No = 1;
            }
            else
            {
                Search_Data = Filter_Value;
            }

            ViewBag.FilterValue = Search_Data;

            var jadwal = from b in db.Jadwal select b;


            if (!String.IsNullOrEmpty(Search_Data))
            {
                //DateTime dt = DateTime.Parse(Search_Data);
                jadwal = jadwal.Where(b => b.TanggalJadwal.ToShortDateString().Contains(Search_Data.ToUpper()));
                //|| p.Nama.ToUpper().Contains(Search_Data.ToUpper()));
            }

            switch (Sorting_Order)
            {
                case "Ruang":
                    jadwal = jadwal.OrderByDescending(b => b.Ruang);
                    break;
                default:
                    jadwal = jadwal.OrderBy(b => b.Ruang);
                    break;
            }

            int Size_Of_Page = 10;
            int No_Of_Page = (Page_No ?? 1);



            return View(jadwal.ToPagedList(No_Of_Page, Size_Of_Page));


            //return View(db.Jadwal.ToList());
        }

        [Authorize(Roles = "Admin,Dokter")]
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


        // GET: Jadwal/Create
        [Authorize(Roles = "Dokter")]
        public ActionResult Create()
        {
            

            var user = from p in db.Pengguna
                       where p.UserName == User.Identity.Name
                       select p.PenggunaId;

            var model = new JadwalViewModel
            {
                PenggunaId = user.First()
            };
                        
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(JadwalViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            for (DateTime x = model.TanggalJadwal; x <= model.Finish; x = x.AddDays(1))
            {
                if(model.IsTime1Check == true)
                {
                    string sesi = "Time1";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime2Check == true)
                {
                    string sesi = "Time2";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime3Check == true)
                {
                    string sesi = "Time3";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime4Check == true)
                {
                    string sesi = "Time4";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime5Check == true)
                {
                    string sesi = "Time5";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime6Check == true)
                {
                    string sesi = "Time6";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime6Check == true)
                {
                    string sesi = "Time6";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime7Check == true)
                {
                    string sesi = "Time7";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime8Check == true)
                {
                    string sesi = "Time8";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime9Check == true)
                {
                    string sesi = "Time9";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                if (model.IsTime10Check == true)
                {
                    string sesi = "Time10";
                    var newJadwal = new Jadwal
                    {

                        JadwalId = model.JadwalId,
                        PenggunaId = model.PenggunaId,
                        TanggalJadwal = x,
                        Sesi = sesi,
                        Ruang = model.Ruang
                    };

                    db.Jadwal.Add(newJadwal);
                    db.SaveChanges();
                }
                
                
                

            }           
            
            return RedirectToAction("Index");
        }

       

        [Authorize(Roles = "Dokter")]
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
        [Authorize(Roles = "Dokter")]
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
        [Authorize(Roles = "Dokter")]
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

    public class JadwalViewModel
    {
        [Required]
        public int JadwalId { get; set; }

        [Required]
        public int PenggunaId { get; set; }

        public int AppointmentId { get; set; }

        [Required]
        public DateTime Finish { get; set; }

        [Required]
        public DateTime TanggalJadwal { get; set; }

        [Required]
        public string Sesi { get; set; }

        public bool IsTimeShowed { get; set; }

        public bool IsTime1Check{ get; set; }

        public bool IsTime2Check { get; set; }

        public bool IsTime3Check { get; set; }

        public bool IsTime4Check { get; set; }

        public bool IsTime5Check { get; set; }

        public bool IsTime6Check { get; set; }

        public bool IsTime7Check { get; set; }

        public bool IsTime8Check { get; set; }

        public bool IsTime9Check { get; set; }

        public bool IsTime10Check { get; set; }

        [Required]
        public string Ruang { get; set; }

    }
}
