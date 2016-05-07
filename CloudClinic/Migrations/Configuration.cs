namespace CloudClinic.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<CloudClinic.Models.ClinicContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(CloudClinic.Models.ClinicContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Pasien.AddOrUpdate(
                p => p.PasienId,
                new Pasien()
                {
                    PasienId = 5,
                    UserName = "hendry",
                    Nama = "Hendry Anwar",
                    TglLhr = DateTime.Parse("26-11-1990"),
                    Gender = "Pria",
                    GolDarah = "O",
                    Alamat = "Jalan Kopral Toya",
                    Kota = "Prabumulih",
                    Telp = "08981000277",
                    TglRegistrasi = DateTime.Now,
                    Email = "hendry.anwar@live.com",
                    RiwayatSakit = "sakit gigi geraham"
                },

                new Pasien()
                {
                    PasienId = 6,
                    UserName = "erick",
                    Nama = "Erick Kurniawan",
                    TglLhr = DateTime.Parse("12-11-1985"),
                    Gender = "Pria",
                    GolDarah = "AB",
                    Alamat = "Jalan Mangkubumi",
                    Kota = "Yogyakarta",
                    Telp = "08981000277",
                    TglRegistrasi = DateTime.Now,
                    Email = "erickkurniawan@live.com",
                    RiwayatSakit = "sakit gigi susu"
                }
            );

            context.Pengguna.AddOrUpdate(
                    d => d.PenggunaId,
                    new Pengguna()
                    {
                        PenggunaId = 4,
                        UserName = "siti",
                        Nama = "Siti Yoshi",
                        Alamat = "Jalan Condong Catur",
                        Kota = "Yogyakarta",
                        Telp = "08981000277",
                        Email = "siti.yoshi@gmail.com"
                    },

                    new Pengguna()
                    {
                        PenggunaId = 5,
                        UserName = "rahmat",
                        Nama = "Rahmat Wijaya",
                        Alamat = "Jalan Godean Barat",
                        Kota = "Yogyakarta",
                        Telp = "08981000277",
                        Email = "rahmat.wijaya@gmail.com"
                    }
                );

            context.Jadwal.AddOrUpdate(
                    j => j.JadwalId,
                    new Jadwal()
                    {
                        JadwalId = 11,
                        PenggunaId = 4,
                        TanggalJadwal = DateTime.Parse("05-05-2016"),
                        Ruang = "Ruang 1",
                        Sesi = "Sesi 1 : 08:00 - 08:30"
                    },

                    new Jadwal()
                    {
                        JadwalId = 12,
                        PenggunaId = 4,
                        TanggalJadwal = DateTime.Parse("05-05-2016"),
                        Ruang = "Ruang 1",
                        Sesi = "Sesi 2 : 08:30 - 09:00"
                    },

                    new Jadwal()
                    {
                        JadwalId = 13,
                        PenggunaId = 4,
                        TanggalJadwal = DateTime.Parse("05-05-2016"),
                        Ruang = "Ruang 1",
                        Sesi = "Sesi 3 : 09:00 - 09:30"
                    },

                    new Jadwal()
                    {
                        JadwalId = 14,
                        PenggunaId = 5,
                        TanggalJadwal = DateTime.Parse("06-05-2016"),
                        Ruang = "Ruang 2",
                        Sesi = "Sesi 1 : 08:00 - 08:30"
                    },

                    new Jadwal()
                    {
                        JadwalId = 15,
                        PenggunaId = 5,
                        TanggalJadwal = DateTime.Parse("06-05-2016"),
                        Ruang = "Ruang 2",
                        Sesi = "Sesi 2 : 08:30 - 09:00"
                    }
                );

        }
    }
}
