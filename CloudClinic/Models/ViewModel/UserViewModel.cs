using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace CloudClinic.Models.ViewModel
{
    public class UserViewModel
    {
        [ScaffoldColumn(false)]
        [Required]
        public int PenggunaId { get; set; }

        [Required]
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
        public string Aturan { get; set; }

        [Required]
        public string Nama { get; set; }

        [Required]
        public string Kota { get; set; }

        [Required]
        public string Alamat { get; set; }
        [Required]
        public string Telp { get; set; }
        [Required]
        public string Email { get; set; }
    }
}