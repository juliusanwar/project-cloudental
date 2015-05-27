using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.ViewModel
{
    public class TransactionViewModel
    {
        [Key]
        public int TransactionId { get; set; }

        public int PasienId { get; set; }

        public DateTime TanggalDatang { get; set; }

        [Required]
        public string Amnanesa { get; set; }

        [Required]
        [Display(Name = "Dokter")]
        public string Nama { get; set; }
    }
}