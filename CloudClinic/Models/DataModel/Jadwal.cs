using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    //public enum Hari { Senin, Selasa, Rabu, Kamis, Jumat};
    public enum Ruang { Ruang1, Ruang2 };
    public enum Sesi { Sesi1, Sesi2, Sesi3, Sesi4, Sesi5 };

    public class Jadwal
    {
        [Key]
        public int JadwalId { get; set; }

        [Required]
        public int PenggunaId { get; set; }

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

        public virtual Pengguna Pengguna { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}