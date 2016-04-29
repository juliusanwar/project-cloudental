using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel
{
    public class Pembelian
    {
        [Key]
        public int PembelianId { get; set; }

        [Required]
        public int BarangId { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.DateTime)]
        public DateTime TglBeli { get; set; }

        public int Qty { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Total { get; set; }

        public string namaUnik
        {
            get { return "PB" + BarangId.ToString("D2") + PembelianId.ToString("D3"); }
        }
        //public Pembelian()
        //{
        //    TglBeli = 
        //}

        public virtual Barang Barang { get; set; }
    }

    public class PembelianViewModel
    {
        public int PembelianId { get; set; }

        
        public int BarangId { get; set; }

        public int NamaBarang { get; set; }

        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime TglBeli { get; set; }

        public int Qty { get; set; }

        
        [DataType(DataType.Currency)]
        public decimal? Harga { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Total { get; set; }
    }
}