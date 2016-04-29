using CloudClinic.Models.DataModel;
using PagedList;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudClinic.Models
{
    public class PasienSearchModel
    {
        public int? Page { get; set; }
        [Display(Name = "Pasien")]
        public string Nama { get; set; }
        public string Kota { get; set; }
        public IPagedList<Pasien> SearchResults { get; set; }
        public string SearchButton { get; set; }
    }
}
