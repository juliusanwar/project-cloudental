using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CloudClinic.Models
{
    public class AppointmentViewModel
    {

        public static int ReminderTime = 30;
        public int Id { get; set; }


        public int PasienId { get; set; }

        //[Required]
        //public string Name { get; set; }

        public string PhoneNumber { get; set; }

        public DateTime Time { get; set; }

        public string PilihanWaktu { get; set; }

        public string Keluhan { get; set; }

        //[Required]
        public string Timezone { get; set; }

        public DateTime CreatedAt { get; set; }

        public int JadwalId { get; set; }

        public int PenggunaId { get; set; }

        public string Hari { get; set; }

        public string Ruang { get; set; }

        public string Sesi { get; set; }

    }
}