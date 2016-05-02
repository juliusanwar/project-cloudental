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

        public int JadwalId { get; set; }

        public string PhoneNumber { get; set; }

        public string Keluhan { get; set; }

        public DateTime CreatedAt { get; set; }

        public virtual Pasien Pasien { get; set; }

        public virtual Jadwal Jadwal { get; set; }
    }
}