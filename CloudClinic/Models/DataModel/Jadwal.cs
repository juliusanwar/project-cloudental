using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Jadwal
    {
        [Key]
        public int JadwalId { get; set; }

        [Required]
        public string PilihanJadwal { get; set; }

        public virtual ICollection<Reservation> Reservation { get; set; }
    }
}