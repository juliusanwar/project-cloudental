using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class Dokter
    {
        public int IdDokter { get; set; }
        public string Nama { get; set; }
        public string Alamat { get; set; }
        public string Telp { get; set; }
    }
}