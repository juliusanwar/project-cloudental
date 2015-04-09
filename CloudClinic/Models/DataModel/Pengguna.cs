using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Pengguna
    {
        [Key]
        public int PenggunaId { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string Atruan { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Kota { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        public string Telp { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual ICollection<RekamMedis> RekamMedisis { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }
    }
}