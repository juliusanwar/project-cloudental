using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

using Microsoft.Ajax.Utilities;

namespace CloudClinic.Models.DataModel
{
    public enum PilihanWaktu { A, B, C, D };

    public class Appointment
    {
        public static int ReminderTime = 30;
        public int Id { get; set; }

        [Required]
        public int PasienId { get; set; }

        
        public int JadwalId { get; set; }


        [Required, Phone, Display(Name = "Phone number")]
        public string PhoneNumber { get; set; }

        //[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd-MM-yyyy}")]
        //[DataType(DataType.DateTime)]
        //public DateTime Time { get; set; }

        public bool IsTimeShowed { get; set; }

        public bool IsTimeAvailable { get; set; }

        public string Keluhan { get; set; }

        [Display(Name = "Created at")]
        public DateTime CreatedAt { get; set; }

        public virtual Pasien Pasien { get; set; }
        public virtual Jadwal Jadwal { get; set; }
    }
}