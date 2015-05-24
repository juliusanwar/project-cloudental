using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public enum Gender { pria, wanita};

    public enum GolDarah { A,B,AB,O};

    public class Pasien
    {
        [Key]
        public int PasienId { get; set; }

        //Inputan harus NOMOR BPJS. Jika tidak ada input sendiri sesuai email.
        [Required]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string UserName { get; set; }

        [Required]
        public string Nama { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime? TglLhr { get; set; }


        public string Gender { get; set; }

        public string GolDarah { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        public string Telp { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TglRegistrasi { get; set; }

        public string RiwayatSakit { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
        //public virtual ICollection<RekamMedis> RekamMedisis { get; set; }
        public virtual ICollection<Reservation> Reservations { get; set; }

    }
}