using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CloudClinic.Models.DataModel
{
    public class BillingJasa
    {
        [Key]
        public int BilJasaId { get; set; }

        //[Required]
        //public int PasienId { get; set; }

        //public int PenggunaId { get; set; }

        [Required]
        public int DiagnosisId { get; set; }

        [Required]
        public string Gigi { get; set; }

        [Required]
        public int TindakanId { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TglDatang { get; set; }

        public string namaUnik
        {
            get { return "BJ" + BilJasaId.ToString("D4"); }
        }

        public BillingJasa()
        {
            TglDatang = DateTime.Now;
        }

        
        public virtual Tindakan Tindakan { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
        //public virtual Pasien Pasien { get; set; }
        //public virtual Pengguna Pengguna { get; set; }
    }
}