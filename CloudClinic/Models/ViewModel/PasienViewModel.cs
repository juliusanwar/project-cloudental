using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.ViewModel
{
    public enum Gender { pria, wanita };

    public enum GolDarah { A, B, AB, O };

    public class PasienViewModel
    {
        [ScaffoldColumn(false)]
        [Required]
        public int PasienId { get; set; }

        //Inputan harus NOMOR BPJS. Jika tidak ada input sendiri sesuai email.
        [Required]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        [DataType(DataType.Password)]
        [System.Web.Mvc.HiddenInput(DisplayValue = false)]
        public string Repassword { get; set; }

        [Required]
        public string Nama { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TglLhr { get; set; }

        [Required]
        public string Gender { get; set; }

        [Required]
        public string GolDarah { get; set; }

        [Required]
        public string Alamat { get; set; }

        [Required]
        public string Kota { get; set; }

        [Required]
        public string Telp { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TglRegistrasi { get; set; }

        public string RiwayatSakit { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public PasienViewModel()
          {
            TglRegistrasi = DateTime.Now;
          }
    }
}
