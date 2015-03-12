using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Reservation
    {
        [Key]
        public int NomorDaftar { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime TglRegistrasi { get; set; }
        public string Keluhan { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
    }
}