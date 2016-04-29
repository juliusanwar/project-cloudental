using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

//namespace CloudClinic.Models.DataModel
//{
//    public class RekamMedis
//    {
//        [Key]
//        public int RekamId { get; set; }

//        public int PasienId { get; set; }

//        public DateTime TglDatang { get; set; }

//        [Required]
//        public string Amnanesa { get; set; }

//        //[Required]
//        //[Display(Name="Dokter")]
//        //public int PenggunaId { get; set; }
//        public RekamMedis()
//        {
//            TglDatang = DateTime.Now;
//        }

//        public virtual Pasien Pasien { get; set; }
//        public virtual ICollection<BillingJasa> BillingJasa { get; set; }
//        public virtual ICollection<BillingObat> BillingObat { get; set; }
//    }
//}