using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.DataModel
{
    public class Diagnosis
    {
        [Key]
        public int DiagnosisId { get; set; }

        public int PasienId { get; set; }

        public int PenggunaId { get; set; }

        public DateTime TglDatang { get; set; }

        [Required]
        public string Amnanesa { get; set; }

        public string namaUnik
        {
            get { return "DP" + DiagnosisId.ToString("D4"); }
        }

        public Diagnosis()
        {
            TglDatang = DateTime.Now;
        }

       


        public virtual ICollection<BillingJasa> BillingJasas { get; set; }
        public virtual ICollection<BillingObat> BillingObat { get; set; }
        public virtual Pasien Pasien { get; set; }
        public virtual Pengguna Pengguna { get; set; }
    }
}
