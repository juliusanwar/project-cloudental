using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class BillingObat
    {
        [Key]
        public int BilObatId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public int ObatId { get; set; }
        public int Qty { get; set; }
        public int TotalHarga { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public virtual Obat Obat { get; set; }
    }
}