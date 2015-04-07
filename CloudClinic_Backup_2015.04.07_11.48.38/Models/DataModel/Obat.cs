using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Obat
    {
        [Key]
        public int ObatId { get; set; }
        public string Nama { get; set; }
        public string JenisObat { get; set; }
        public string Kategori { get; set; }
        public int Harga { get; set; }
        public int Stok { get; set; }

        public virtual ICollection<BillingObat> BillingObats { get; set; }

    }
}