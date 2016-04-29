using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Tindakan
    {
        [Key]
        public int TindakanId { get; set; }
        [Required]
        public string NamaTindakan { get; set; }
        [Required]
        public int JenisTindakanId { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }

        public string Diagnosa { get; set; }

        public string namaUnik
        {
            get { return "T" + JenisTindakanId.ToString("D2") + TindakanId.ToString("D3"); }
        }

        public virtual JenisTindakan JenisTindakan { get; set; }

        public virtual ICollection<BillingJasa> BillingJasas { get; set; }

    }
}