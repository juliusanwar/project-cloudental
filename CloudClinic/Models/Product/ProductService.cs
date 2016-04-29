using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.Product
{
    public class ProductService : IDisposable
    {

        private ClinicContext db;

        public ProductService()
        {
            db = new ClinicContext();
        }

        public IEnumerable<Barang> Read()
        {
            return db.Barang.Select(brg => new Barang
            {
                 BarangId = brg.BarangId,
                 JenisBrgId = brg.JenisBrgId,
                 NamaBarang = brg.NamaBarang,
                 Harga = brg.Harga.HasValue ? brg.Harga.Value : default(decimal),
                 Stok = brg.Stok,

                 
                 //LastSupply = DateTime.Today
            });
        }

        public void Create(Barang brg)
        {
            var entity = new Barang();

            entity.NamaBarang = brg.NamaBarang;
            entity.Harga = brg.Harga;
            entity.Stok = (short)brg.Stok;
            
            entity.JenisBrgId = brg.JenisBrgId;

            //if (entity.JenisBrgId == null)
            //{
            //    entity.JenisBrgId = 1;
            //}

            if (brg.JenisBarang != null)
            {
                entity.JenisBrgId = brg.JenisBarang.JenisBrgId;
            }

            db.Barang.Add(entity);
            db.SaveChanges();

            brg.BarangId = entity.BarangId;
        }

        public void Update(Barang brg)
        {
            var entity = new Barang();

            entity.BarangId = brg.BarangId;
            entity.NamaBarang = brg.NamaBarang;
            entity.Harga = brg.Harga;
            entity.Stok = (short)brg.Stok;
            
            entity.JenisBrgId = brg.JenisBrgId;

            if (brg.JenisBarang != null)
            {
                entity.JenisBrgId = brg.JenisBarang.JenisBrgId;
            }

            db.Barang.Attach(entity);
            db.Entry(entity).State = EntityState.Modified;
            db.SaveChanges();
        }

        public void Destroy(Barang brg)
        {
            var entity = new Barang();

            entity.BarangId = brg.BarangId;

            db.Barang.Attach(entity);

            db.Barang.Remove(entity);

            //var orderDetails = entities.Order_Details.Where(pd => pd.ProductID == entity.ProductID);

            //foreach (var orderDetail in orderDetails)
            //{
            //    entities.Order_Details.Remove(orderDetail);
            //}

            db.SaveChanges();
        }

        public void Dispose()
        {
            db.Dispose();
        }
    }
}
