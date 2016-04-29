using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel
{
    public class BillingObat
    {
        [Key]
        public int BilObatId { get; set; }

        //public int PasienId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TglDatang { get; set; }

        [Required]
        public int DiagnosisId { get; set; }

        public int BarangId { get; set; }

        public int Qty { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }

        [DataType(DataType.Currency)]
        public decimal? SubTotal { get; set; }

        public string namaUnik
        {
            get { return "BO" + BilObatId.ToString() + BarangId.ToString("D3"); }
        }

        public BillingObat()
        {
            TglDatang = DateTime.Now;
        }

        //public virtual Pasien Pasien { get; set; }
        public virtual Barang Barang { get; set; }
        public virtual Diagnosis Diagnosis { get; set; }
    }
}