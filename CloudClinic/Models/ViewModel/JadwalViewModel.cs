
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CloudClinic.Models.ViewModel
{
    public class JadwalViewModel
    {
        public int JadwalId { get; set; }

        //[Required]
        public int PenggunaId { get; set; }

        
        public string UserName { get; set; }

        //[Required]
        public int AppointmentId { get; set; }

        //[Required]
        //public string Hari { get; set; }

        [Required]
        public DateTime TanggalJadwal { get; set; }

        [Required]
        public string Ruang { get; set; }

        [Required]
        public string Sesi { get; set; }


    }
}