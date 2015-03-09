using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CloudClinic.Models.DataModel
{
    public class Patient
    {
        [Key]
        public string PatientId { get; set; }

        [Index(IsUnique=true)]
        public string UserName { get; set; }
    }
}