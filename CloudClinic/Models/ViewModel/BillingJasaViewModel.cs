using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.ViewModel
{
    public class BillingJasaViewModel
    {
        public int BilJasaId { get; set; }

        //public IEnumerable<Diagnosis> Records { get; set; }

        public int PasienId { get; set; }

        public string UserName { get; set; }

        public string Anamnesa { get; set; }

        public string Gigi { get; set; }

        public int TindakanId { get; set; }

        public string NamaTindakan { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Total { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Bayar { get; set; }

        [DataType(DataType.Currency)]
        public decimal? Kembalian { get; set; }

        //public BillingJasa()
        //{
        //    TglDatang = DateTime.Now;
        //}
    }
}
