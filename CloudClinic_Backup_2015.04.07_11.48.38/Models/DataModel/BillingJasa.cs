using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class BillingJasa
    {
        [Key]
        public int BilJasaId { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string Diagnosa { get; set; }
        public int TindakanId { get; set; }
        public int HargaJasa { get; set; }

        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Tindakan Tindakan { get; set; }
        
    }
}