using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Reservation
    {
        [Key]
        public int ReservationId { get; set; }

        [Required(ErrorMessage="User Name harus diisi!")]
        public int PasienId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? TglReservasi { get; set; }

        [Required]
        public string PilihanJadwal { get; set; }

        //[Required]
        //[Display(Name = "Jadwal")]
        //public int JadwalId { get; set; }


        public string namaUnik 
        {
            get { return "RSVP" + PasienId.ToString() + ReservationId.ToString("D3");  }
        }

        public Reservation()
          {
            TglReservasi = DateTime.Now;

            //namaUnik = "RSVP" + ReservationId.ToString("D3");
          }

        //public virtual Jadwal Jadwal { get; set; }

        //[ForeignKey("Id")]
        //public virtual ApplicationUser User { get; set; }

        public virtual Pasien Pasien { get; set; }
    }
}