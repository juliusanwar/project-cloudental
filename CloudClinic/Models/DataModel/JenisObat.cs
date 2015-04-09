using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using System.ComponentModel.DataAnnotations;

namespace CloudClinic.Models
{
    public class JenisObat
    {
        [Key]
        public int JenisObatId { get; set; }
        [Required]
        public string NamaJenis { get; set; }

        public virtual ICollection<Obat> Obat { get; set; }

    }
}