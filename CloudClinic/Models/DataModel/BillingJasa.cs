using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class BillingJasa
    {
        [Key]
        public int BilJasaId { get; set; }

        [Required]
        public int TransactionId { get; set; }

        public int TindakanId { get; set; }

        public virtual Tindakan Tindakan { get; set; }

        public virtual Transaction Transaction { get; set; }
        
    }
}