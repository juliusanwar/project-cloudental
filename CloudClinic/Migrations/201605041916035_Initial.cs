namespace CloudClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointment",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PasienId = c.Int(nullable: false),
                        JadwalId = c.Int(nullable: false),
                        PhoneNumber = c.String(),
                        Keluhan = c.String(),
                        CreatedAt = c.DateTime(nullable: false),
                        Jadwal_JadwalId = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Jadwal", t => t.Jadwal_JadwalId)
                .ForeignKey("dbo.Pasien", t => t.PasienId, cascadeDelete: true)
                .Index(t => t.PasienId)
                .Index(t => t.Jadwal_JadwalId);
            
            CreateTable(
                "dbo.Jadwal",
                c => new
                    {
                        JadwalId = c.Int(nullable: false, identity: true),
                        PenggunaId = c.Int(nullable: false),
                        AppointmentId = c.Int(nullable: false),
                        TanggalJadwal = c.DateTime(nullable: false),
                        Ruang = c.String(nullable: false),
                        Sesi = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JadwalId)
                .ForeignKey("dbo.Pengguna", t => t.PenggunaId, cascadeDelete: true)
                .Index(t => t.PenggunaId);
            
            CreateTable(
                "dbo.Pengguna",
                c => new
                    {
                        PenggunaId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Aturan = c.String(),
                        Nama = c.String(nullable: false),
                        Alamat = c.String(nullable: false),
                        Kota = c.String(nullable: false),
                        Telp = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PenggunaId);
            
            CreateTable(
                "dbo.Pasien",
                c => new
                    {
                        PasienId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Nama = c.String(nullable: false),
                        TglLhr = c.DateTime(nullable: false),
                        Gender = c.String(),
                        GolDarah = c.String(),
                        Alamat = c.String(nullable: false),
                        Kota = c.String(nullable: false),
                        Telp = c.String(nullable: false),
                        TglRegistrasi = c.DateTime(nullable: false),
                        RiwayatSakit = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PasienId);
            
            CreateTable(
                "dbo.BillingJasa",
                c => new
                    {
                        BilJasaId = c.Int(nullable: false, identity: true),
                        PasienId = c.Int(nullable: false),
                        DiagnosisId = c.Int(nullable: false),
                        Gigi = c.String(nullable: false),
                        TindakanId = c.Int(nullable: false),
                        Harga = c.Decimal(precision: 18, scale: 2),
                        TglDatang = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.BilJasaId)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId, cascadeDelete: true)
                .ForeignKey("dbo.Pasien", t => t.PasienId, cascadeDelete: true)
                .ForeignKey("dbo.Tindakan", t => t.TindakanId, cascadeDelete: true)
                .Index(t => t.PasienId)
                .Index(t => t.DiagnosisId)
                .Index(t => t.TindakanId);
            
            CreateTable(
                "dbo.Diagnosis",
                c => new
                    {
                        DiagnosisId = c.Int(nullable: false, identity: true),
                        PasienId = c.Int(nullable: false),
                        TglDatang = c.DateTime(nullable: false),
                        Amnanesa = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.DiagnosisId)
                .ForeignKey("dbo.Pasien", t => t.PasienId)
                .Index(t => t.PasienId);
            
            CreateTable(
                "dbo.BillingObat",
                c => new
                    {
                        BilObatId = c.Int(nullable: false, identity: true),
                        TglDatang = c.DateTime(nullable: false),
                        DiagnosisId = c.Int(nullable: false),
                        BarangId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        Harga = c.Decimal(precision: 18, scale: 2),
                        SubTotal = c.Decimal(precision: 18, scale: 2),
                        Pasien_PasienId = c.Int(),
                    })
                .PrimaryKey(t => t.BilObatId)
                .ForeignKey("dbo.Barang", t => t.BarangId, cascadeDelete: true)
                .ForeignKey("dbo.Diagnosis", t => t.DiagnosisId, cascadeDelete: true)
                .ForeignKey("dbo.Pasien", t => t.Pasien_PasienId)
                .Index(t => t.DiagnosisId)
                .Index(t => t.BarangId)
                .Index(t => t.Pasien_PasienId);
            
            CreateTable(
                "dbo.Barang",
                c => new
                    {
                        BarangId = c.Int(nullable: false, identity: true),
                        JenisBrgId = c.Int(nullable: false),
                        NamaBarang = c.String(nullable: false),
                        Harga = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Stok = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BarangId)
                .ForeignKey("dbo.JenisBarang", t => t.JenisBrgId, cascadeDelete: true)
                .Index(t => t.JenisBrgId);
            
            CreateTable(
                "dbo.JenisBarang",
                c => new
                    {
                        JenisBrgId = c.Int(nullable: false, identity: true),
                        NamaJenis = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JenisBrgId);
            
            CreateTable(
                "dbo.Pembelian",
                c => new
                    {
                        PembelianId = c.Int(nullable: false, identity: true),
                        BarangId = c.Int(nullable: false),
                        TglBeli = c.DateTime(nullable: false),
                        Qty = c.Int(nullable: false),
                        Harga = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Total = c.Decimal(precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.PembelianId)
                .ForeignKey("dbo.Barang", t => t.BarangId, cascadeDelete: true)
                .Index(t => t.BarangId);
            
            CreateTable(
                "dbo.Tindakan",
                c => new
                    {
                        TindakanId = c.Int(nullable: false, identity: true),
                        NamaTindakan = c.String(nullable: false),
                        JenisTindakanId = c.Int(nullable: false),
                        Harga = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Diagnosa = c.String(),
                    })
                .PrimaryKey(t => t.TindakanId)
                .ForeignKey("dbo.JenisTindakan", t => t.JenisTindakanId, cascadeDelete: true)
                .Index(t => t.JenisTindakanId);
            
            CreateTable(
                "dbo.JenisTindakan",
                c => new
                    {
                        JenisTindakanId = c.Int(nullable: false, identity: true),
                        NamaTindakan = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JenisTindakanId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        PasienId = c.Int(nullable: false),
                        TglReservasi = c.DateTime(),
                        PilihanJadwal = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Pasien", t => t.PasienId, cascadeDelete: true)
                .Index(t => t.PasienId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Reservation", "PasienId", "dbo.Pasien");
            DropForeignKey("dbo.BillingObat", "Pasien_PasienId", "dbo.Pasien");
            DropForeignKey("dbo.Tindakan", "JenisTindakanId", "dbo.JenisTindakan");
            DropForeignKey("dbo.BillingJasa", "TindakanId", "dbo.Tindakan");
            DropForeignKey("dbo.BillingJasa", "PasienId", "dbo.Pasien");
            DropForeignKey("dbo.Diagnosis", "PasienId", "dbo.Pasien");
            DropForeignKey("dbo.BillingObat", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Pembelian", "BarangId", "dbo.Barang");
            DropForeignKey("dbo.Barang", "JenisBrgId", "dbo.JenisBarang");
            DropForeignKey("dbo.BillingObat", "BarangId", "dbo.Barang");
            DropForeignKey("dbo.BillingJasa", "DiagnosisId", "dbo.Diagnosis");
            DropForeignKey("dbo.Appointment", "PasienId", "dbo.Pasien");
            DropForeignKey("dbo.Jadwal", "PenggunaId", "dbo.Pengguna");
            DropForeignKey("dbo.Appointment", "Jadwal_JadwalId", "dbo.Jadwal");
            DropIndex("dbo.Reservation", new[] { "PasienId" });
            DropIndex("dbo.Tindakan", new[] { "JenisTindakanId" });
            DropIndex("dbo.Pembelian", new[] { "BarangId" });
            DropIndex("dbo.Barang", new[] { "JenisBrgId" });
            DropIndex("dbo.BillingObat", new[] { "Pasien_PasienId" });
            DropIndex("dbo.BillingObat", new[] { "BarangId" });
            DropIndex("dbo.BillingObat", new[] { "DiagnosisId" });
            DropIndex("dbo.Diagnosis", new[] { "PasienId" });
            DropIndex("dbo.BillingJasa", new[] { "TindakanId" });
            DropIndex("dbo.BillingJasa", new[] { "DiagnosisId" });
            DropIndex("dbo.BillingJasa", new[] { "PasienId" });
            DropIndex("dbo.Jadwal", new[] { "PenggunaId" });
            DropIndex("dbo.Appointment", new[] { "Jadwal_JadwalId" });
            DropIndex("dbo.Appointment", new[] { "PasienId" });
            DropTable("dbo.Reservation");
            DropTable("dbo.JenisTindakan");
            DropTable("dbo.Tindakan");
            DropTable("dbo.Pembelian");
            DropTable("dbo.JenisBarang");
            DropTable("dbo.Barang");
            DropTable("dbo.BillingObat");
            DropTable("dbo.Diagnosis");
            DropTable("dbo.BillingJasa");
            DropTable("dbo.Pasien");
            DropTable("dbo.Pengguna");
            DropTable("dbo.Jadwal");
            DropTable("dbo.Appointment");
        }
    }
}
