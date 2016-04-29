using CloudClinic.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CloudClinic.Models
{
    public class PenggunaService : IDisposable
    {
        private ClinicContext db;

        public PenggunaService()
        {
            db = new ClinicContext();
        }
        

        public IEnumerable<PenggunaViewModel> Read()
        {
            return db.Pengguna.Select(pengguna => new PenggunaViewModel
            {
                 PenggunaId = pengguna.PenggunaId,
                 UserName = pengguna.UserName,
                 Nama = pengguna.Nama,
                 Alamat = pengguna.Alamat,
                 Kota = pengguna.Kota,
                 Telp = pengguna.Telp,
                 Email = pengguna.Email
                 //CategoryID = pengguna.CategoryID,
                 //Category = new CategoryViewModel()
                 //{
                 //    CategoryID = pengguna.Category.CategoryID,
                 //    CategoryName = pengguna.Category.CategoryName
                 //},
                 //LastSupply = DateTime.Today
            });
        }

        public void Update(PenggunaViewModel pengguna)
        {
            var entity = new Pengguna();

            entity.PenggunaId = pengguna.PenggunaId;
            entity.Nama = pengguna.Nama;
            entity.Alamat = pengguna.Alamat;
            entity.Kota = pengguna.Kota;
            entity.Telp = pengguna.Telp;
            entity.Email = pengguna.Email;

            db.Pengguna.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Destroy(PenggunaViewModel pengguna)
        {
            var entity = new Pengguna();

            entity.PenggunaId = pengguna.PenggunaId;

            db.Pengguna.Attach(entity);

            db.Pengguna.Remove(entity);

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }

    }
}
