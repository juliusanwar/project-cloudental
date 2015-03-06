using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Doctor
    {
        [Key]
        public string DoctorId { get; set; }
        [Index(IsUnique = true)]
        public string UserName { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Telp { get; set; }
    }
}