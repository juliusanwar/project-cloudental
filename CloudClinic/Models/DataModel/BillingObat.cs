using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class BillingObat
    {
        [Key]
        public int BilObatId { get; set; }

        public int TransactionId { get; set; }

        public int ObatId { get; set; }

        public int Qty { get; set; }

        
        [DataType(DataType.Currency)]
        public decimal? Total { get; set; }  


        public virtual Transaction Transaction { get; set; }
        public virtual Obat Obat { get; set; }
    }
}