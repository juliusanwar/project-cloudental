using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel
{
    public class Obat
    {
        [Key]
        public int ObatId { get; set; }
        public string Nama { get; set; }
        [Required]
        public string JenisObatId { get; set; }
        public string Kategori { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }
        public int Stok { get; set; }

        public virtual JenisObat JenisObat { get; set; }

        public virtual ICollection<BillingObat> BillingObats { get; set; }
    }
}