using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;
using CloudClinic.Models.DataModel;

namespace CloudClinic.Models
{
    public class JenisBarang
    {
        [Key]
        public int JenisBrgId { get; set; }
        [Required]
        public string NamaJenis { get; set; }

        public string namaUnik
        {
            get { return "JO" + JenisBrgId.ToString("D2"); }
        }

        public virtual ICollection<Barang> Barang { get; set; }
    }
}