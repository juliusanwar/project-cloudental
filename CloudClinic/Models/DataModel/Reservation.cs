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

        public DateTime TglReservasi { get; set; }

        [Required]
        [Display(Name = "Dokter")]
        public int PenggunaId { get; set; }


        public virtual Jadwal Jadwal { get; set; }

        public virtual Pengguna Pengguna { get; set; }

        public virtual Pasien Pasien { get; set; }
    }
}