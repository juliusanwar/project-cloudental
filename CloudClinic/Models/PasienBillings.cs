using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class PasienBillings
    {
        public Pasien Pasien { get; set; }
        public IList<Diagnosis> Diagnosis { get; set; }
    }
}