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
        public int ReservationId { get; set; }

        [Required]
        public int PasienId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? TglReservasi { get; set; }
        

        [Display(Name = "Jadwal")]
        public int JadwalId { get; set; }

        [Required]
        [Display(Name = "Dokter")]
        public int PenggunaId { get; set; }


        public virtual Jadwal Jadwal { get; set; }

        public virtual Pengguna Pengguna { get; set; }

        public virtual Pasien Pasien { get; set; }
    }
}