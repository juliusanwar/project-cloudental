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

        public DateTime TglDatang { get; set; }

        [Required]
        public string Amnanesa { get; set; }

        public Diagnosis()
        {
            TglDatang = DateTime.Now;
        }

        
        public virtual ICollection<BillingJasa> BillingJasas { get; set; }
        public virtual ICollection<BillingObat> BillingObat { get; set; }
        public virtual Pasien Pasien { get; set; }
    }
}
