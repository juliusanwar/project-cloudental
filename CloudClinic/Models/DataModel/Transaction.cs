using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }

        public int PasienId { get; set; }

        public DateTime TanggalDatang { get; set; }

        [Required]
        public string Amnanesa { get; set; }

        [Required]
        [Display(Name="Dokter")]
        public int PenggunaId { get; set; }


        public virtual Pasien Pasien { get; set; }
        public virtual Pengguna Pengguna { get; set; }
        public virtual ICollection<BillingJasa> BillingJasa { get; set; }
        public virtual ICollection<BillingObat> BillingObat { get; set; }
    }
}