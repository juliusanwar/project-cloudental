using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using CloudClinic.Models;

namespace CloudClinic.Context
{
    public static class PasienRepository
    {
        public static IQueryable<Pasien> GetPeople()
        {
            return new List<Pasien>
            {
                new Pasien
                {
                    PasienId = 101,
                    UserName = "mike",
                    Nama = "Dea Novita",
                    TglLhr = new DateTime(1993, 02, 03),
                    Gender = "Wanita",
                    Alamat = "Jl. Ngasal",
                    Telp = "123123123213",
                    Email = "mike@novita.com"
                },
                new Pasien
                {
                    PasienId = 101,
                    UserName = "andrew",
                    Nama = "andrew Novita",
                    TglLhr = new DateTime(1993, 02, 03),
                    Gender = "Wanita",
                    Alamat = "Jl. Ngasal",
                    Telp = "123123123213",
                    Email = "andrew@novita.com"
                },
                new Pasien
                {
                    PasienId = 101,
                    UserName = "Billy",
                    Nama = "Billy Novita",
                    TglLhr = new DateTime(1993, 02, 03),
                    Gender = "Wanita",
                    Alamat = "Jl. Ngasal",
                    Telp = "123123123213",
                    Email = "billy@novita.com"
                },
                new Pasien
                {
                    PasienId = 101,
                    UserName = "Jedi",
                    Nama = "Jedi Novita",
                    TglLhr = new DateTime(1993, 02, 03),
                    Gender = "Wanita",
                    Alamat = "Jl. Ngasal",
                    Telp = "123123123213",
                    Email = "jedi@novita.com"
                },
                new Pasien
                {
                    PasienId = 105,
                    UserName = "Livia",
                    Nama = "Livia Eletra",
                    TglLhr = new DateTime(1993, 02, 03),
                    Gender = "Wanita",
                    Alamat = "Jl. Ngasal",
                    Telp = "123123123213",
                    Email = "livia@novita.com"
                }
            }.AsQueryable();
        }
    }
}
