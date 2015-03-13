using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public string Nama { get; set; }
        public DateTime TglLhr { get; set; }
        public string Gender { get; set; }
        public string GolDarah { get; set; }
        public string Alamat { get; set; }
        public string Telp { get; set; }
        public DateTime TglRegistrasi { get; set; }
        public string RiwayatSakit { get; set; }

        public virtual ICollection<BillingJasa> BillingJasas { get; set; }
        public virtual ICollection<BillingObat> BillingObats { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
        public virtual ICollection<RekamMedis> RekamMedisis { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}