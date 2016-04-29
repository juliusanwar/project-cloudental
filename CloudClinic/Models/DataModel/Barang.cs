using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel
{
    public class Barang
    {
        [Key]
        public int BarangId { get; set; }
        [Required]
        public int JenisBrgId { get; set; }
        [Required]
        public string NamaBarang { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }
        public int Stok { get; set; }

        public string namaUnik
        {
            get { return "O"+ JenisBrgId.ToString() + BarangId.ToString("D3"); }
        }

        public virtual JenisBarang JenisBarang { get; set; }

        public virtual ICollection<Pembelian> Pembelians { get; set; }
        public virtual ICollection<BillingObat> BillingObats { get; set; }
    }
}