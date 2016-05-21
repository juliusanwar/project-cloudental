namespace CloudClinic.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models;
    using Models.ViewModel;
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

            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new ApplicationRoleManager(roleStore);
            var adminRole = new IdentityRole()
            {
                Name = "Admin"
            };

            roleManager.CreateAsync(adminRole);

            var userStore = new UserStore<ApplicationUser>(new ApplicationDbContext());
            var userManager = new ApplicationUserManager(userStore);
            var adminUser = new ApplicationUser()
            {
                Email = "admin.admin.com",
                EmailConfirmed = true,
                UserName = "admin"
            };

            userManager.Create(adminUser, "password");


            context.Pasien.AddOrUpdate(
                p => p.PasienId,
                new Pasien()
                {
                    PasienId = 5,
                    UserName = "hendry",

                    Nama = "Hendry Anwar",
                    TglLhr = new DateTime(1990, 11, 26),
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
                    TglLhr = new DateTime(2000, 11, 20),
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
                        TanggalJadwal = DateTime.Parse("05-22-2016"),
                        Ruang = "Ruang 1",
                        Sesi = "Time1",
                    },

                    new Jadwal()
                    {
                        JadwalId = 12,
                        PenggunaId = 4,
                        TanggalJadwal = DateTime.Parse("05-22-2016"),
                        Ruang = "Ruang 1",
                        Sesi = "Time2"
                    },

                    new Jadwal()
                    {
                        JadwalId = 13,
                        PenggunaId = 4,
                        TanggalJadwal = new DateTime(2016, 05, 22),
                        Ruang = "Ruang 1",
                        Sesi = "Time3"
                    },

                    new Jadwal()
                    {
                        JadwalId = 14,
                        PenggunaId = 5,
                        TanggalJadwal = DateTime.Parse("06-22-2016"),
                        Ruang = "Ruang 2",
                        Sesi = "Time5"
                    },

                    new Jadwal()
                    {
                        JadwalId = 15,
                        PenggunaId = 5,
                        TanggalJadwal = DateTime.Parse("06-22-2016"),
                        Ruang = "Ruang 2",
                        Sesi = "Time7"
                    }
                );

            context.SaveChanges();
        }
    }
}
