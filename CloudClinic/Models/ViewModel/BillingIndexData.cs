using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models.ViewModel
{
    public class BillingIndexData
    {
        public IEnumerable<BillingJasa> BillingJasa { get; set; }
        public IEnumerable<BillingObat> BillingObat { get; set; }
        public IEnumerable<Diagnosis> Diagnosis { get; set; }
    }
}
