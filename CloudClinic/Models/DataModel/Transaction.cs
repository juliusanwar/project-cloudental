using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Transaction
    {
        [Key]
        public int TransactionId { get; set; }
        public int PatienId { get; set; }
        public int BilJasaId { get; set; }
        public int BilObatId { get; set; }
        public int TotalBayar { get; set; }
        public int Bayar { get; set; }
        public int Kembalian { get; set; }
        public DateTime TglBayar { get; set; }

        public virtual Patient Patient { get; set; }
        public virtual BillingJasa BillingJasa { get; set; }
        public virtual BillingObat BillingObat { get; set; }
    }
}