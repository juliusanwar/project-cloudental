using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class JenisTindakan
    {
        [Key]
        public int JenisTindakanId { get; set; }

        [Required]
        [Display(Name="Nama Tindakan")]
        public string NamaTindakan { get; set; }

        public virtual ICollection<Tindakan> Tindakan { get; set; }
    }
}