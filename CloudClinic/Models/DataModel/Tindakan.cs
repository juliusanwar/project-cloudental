using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Tindakan
    {
        public int TindakanId { get; set; }
        public string Nama { get; set; }
        public int JenisTindakanId { get; set; }
        public string Alat { get; set; }
        public int Harga { get; set; }
        public string Diagnosis { get; set; }
        public int TotalHarga { get; set; }

        public virtual JenisTindakan JenisTindakan { get; set; }

    }
}