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

        [Key]
        public int Id { get; set; }

        public int PasienId { get; set; }

        public string PhoneNumber { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Keluhan { get; set; }

        public bool IsSelectFix { get; set; }

        //public bool IsSelected { get; set; }

        public DateTime CreatedAt { get; set; }

        public string namaUnik
        {
            get { return "APK" + Id.ToString("D4"); }
        }

        public virtual Pasien Pasien { get; set; }

        public virtual Jadwal Jadwal { get; set; }
    }
}