using CloudClinic.Models.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.ViewModel
{
    public class BillingObatViewModel
    {
        private ClinicContext db;

        public IEnumerable<BillingObat> BillingObatItems { get; set; }
        public decimal? BillingObatTotal { get; set; }

        public decimal GetTotal()
        {
            decimal? total += BillingObatTotal;

            return total ?? decimal.Zero;
        }

    }
}