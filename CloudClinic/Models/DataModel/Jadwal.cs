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
    public enum Sesi { Time1, Time2, Time3, Time4, Time5, Time6, Time7, Time8, Time9, Time10 };

    public class Jadwal
    {
        [Key]
        public int JadwalId { get; set; }

        [Required]
        public int PenggunaId { get; set; }

        //[Required]
        //public string Hari { get; set; }

        [Required]
        public DateTime TanggalJadwal { get; set; }

        [Required]
        public string Ruang { get; set; }

        [Required]
        public string Sesi { get; set; }

        //public bool isSelected { get; set; }

        public string namaUnik
        {
            get { return "JK" + JadwalId.ToString("D4"); }
        }

        public virtual Pengguna Pengguna { get; set; }
        public virtual Appointment Appointment { get; set; }
    }
}