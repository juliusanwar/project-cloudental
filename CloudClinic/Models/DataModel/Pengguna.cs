using CloudClinic.Models.DataModel;
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
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string UserName { get; set; }

        public string Aturan { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        public string Kota { get; set; }

        [Required]
        public string Telp { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string namaUnik
        {
            get { return "KGW" + PenggunaId.ToString("D4"); }
        }

        public virtual ICollection<Diagnosis> Diagnosis { get; set; }
        //public virtual ICollection<BillingJasa> BillingJasa { get; set; }
        public virtual ICollection<Jadwal> Jadwal { get; set; }
    }
}