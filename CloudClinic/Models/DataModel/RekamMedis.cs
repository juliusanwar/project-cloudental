using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class RekamMedis
    {
        [Key]
        public int RekamId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public string Anamnesa { get; set; }
        public string Diagnosa { get; set; }
        public int BilJasaId { get; set; }
        public int BilObatId { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual BillingJasa BillingJasa { get; set; }
        public virtual BillingObat BillingObat { get; set; }
    }
}