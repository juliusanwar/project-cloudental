namespace CloudClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class version1_generate_database : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingJasa",
                c => new
                    {
                        BilJasaId = c.Int(nullable: false, identity: true),
                        TransactionId = c.Int(nullable: false),
                        TindakanId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BilJasaId)
                .ForeignKey("dbo.Tindakan", t => t.TindakanId, cascadeDelete: true)
                .ForeignKey("dbo.Transaction", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId)
                .Index(t => t.TindakanId);
            
            CreateTable(
                "dbo.Tindakan",
                c => new
                    {
                        TindakanId = c.Int(nullable: false, identity: true),
                        Nama = c.String(nullable: false),
                        JenisTindakanId = c.Int(nullable: false),
                        Harga = c.Int(nullable: false),
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
                "dbo.Transaction",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        PasienId = c.Int(nullable: false),
                        TanggalDatang = c.DateTime(nullable: false),
                        Amnanesa = c.String(nullable: false),
                        PenggunaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.Pengguna", t => t.PenggunaId, cascadeDelete: true)
                .ForeignKey("dbo.Pasien", t => t.PasienId, cascadeDelete: true)
                .Index(t => t.PasienId)
                .Index(t => t.PenggunaId);
            
            CreateTable(
                "dbo.BillingObat",
                c => new
                    {
                        BilObatId = c.Int(nullable: false, identity: true),
                        TransactionId = c.Int(nullable: false),
                        ObatId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BilObatId)
                .ForeignKey("dbo.Obat", t => t.ObatId, cascadeDelete: true)
                .ForeignKey("dbo.Transaction", t => t.TransactionId, cascadeDelete: true)
                .Index(t => t.TransactionId)
                .Index(t => t.ObatId);
            
            CreateTable(
                "dbo.Obat",
                c => new
                    {
                        ObatId = c.Int(nullable: false, identity: true),
                        Nama = c.String(),
                        JenisObatId = c.String(nullable: false),
                        Kategori = c.String(),
                        Harga = c.Int(nullable: false),
                        Stok = c.Int(nullable: false),
                        JenisObat_JenisObatId = c.Int(),
                    })
                .PrimaryKey(t => t.ObatId)
                .ForeignKey("dbo.JenisObat", t => t.JenisObat_JenisObatId)
                .Index(t => t.JenisObat_JenisObatId);
            
            CreateTable(
                "dbo.JenisObat",
                c => new
                    {
                        JenisObatId = c.Int(nullable: false, identity: true),
                        NamaJenis = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JenisObatId);
            
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
                        Telp = c.String(nullable: false),
                        TglRegistrasi = c.DateTime(nullable: false),
                        RiwayatSakit = c.String(),
                    })
                .PrimaryKey(t => t.PasienId);
            
            CreateTable(
                "dbo.Reservation",
                c => new
                    {
                        ReservationId = c.Int(nullable: false, identity: true),
                        PasienId = c.Int(nullable: false),
                        TglReservasi = c.DateTime(nullable: false),
                        PenggunaId = c.Int(nullable: false),
                        Jadwal_JadwalId = c.Int(),
                    })
                .PrimaryKey(t => t.ReservationId)
                .ForeignKey("dbo.Jadwal", t => t.Jadwal_JadwalId)
                .ForeignKey("dbo.Pasien", t => t.PasienId, cascadeDelete: true)
                .ForeignKey("dbo.Pengguna", t => t.PenggunaId, cascadeDelete: true)
                .Index(t => t.PasienId)
                .Index(t => t.PenggunaId)
                .Index(t => t.Jadwal_JadwalId);
            
            CreateTable(
                "dbo.Jadwal",
                c => new
                    {
                        JadwalId = c.Int(nullable: false, identity: true),
                        PilihanJadwal = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.JadwalId);
            
            CreateTable(
                "dbo.Pengguna",
                c => new
                    {
                        PenggunaId = c.Int(nullable: false, identity: true),
                        UserName = c.String(nullable: false),
                        Atruan = c.String(nullable: false),
                        Nama = c.String(nullable: false),
                        Kota = c.String(nullable: false),
                        Alamat = c.String(nullable: false),
                        Telp = c.String(nullable: false),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.PenggunaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Transaction", "PasienId", "dbo.Pasien");
            DropForeignKey("dbo.Transaction", "PenggunaId", "dbo.Pengguna");
            DropForeignKey("dbo.Reservation", "PenggunaId", "dbo.Pengguna");
            DropForeignKey("dbo.Reservation", "PasienId", "dbo.Pasien");
            DropForeignKey("dbo.Reservation", "Jadwal_JadwalId", "dbo.Jadwal");
            DropForeignKey("dbo.BillingObat", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.Obat", "JenisObat_JenisObatId", "dbo.JenisObat");
            DropForeignKey("dbo.BillingObat", "ObatId", "dbo.Obat");
            DropForeignKey("dbo.BillingJasa", "TransactionId", "dbo.Transaction");
            DropForeignKey("dbo.BillingJasa", "TindakanId", "dbo.Tindakan");
            DropForeignKey("dbo.Tindakan", "JenisTindakanId", "dbo.JenisTindakan");
            DropIndex("dbo.Reservation", new[] { "Jadwal_JadwalId" });
            DropIndex("dbo.Reservation", new[] { "PenggunaId" });
            DropIndex("dbo.Reservation", new[] { "PasienId" });
            DropIndex("dbo.Obat", new[] { "JenisObat_JenisObatId" });
            DropIndex("dbo.BillingObat", new[] { "ObatId" });
            DropIndex("dbo.BillingObat", new[] { "TransactionId" });
            DropIndex("dbo.Transaction", new[] { "PenggunaId" });
            DropIndex("dbo.Transaction", new[] { "PasienId" });
            DropIndex("dbo.Tindakan", new[] { "JenisTindakanId" });
            DropIndex("dbo.BillingJasa", new[] { "TindakanId" });
            DropIndex("dbo.BillingJasa", new[] { "TransactionId" });
            DropTable("dbo.Pengguna");
            DropTable("dbo.Jadwal");
            DropTable("dbo.Reservation");
            DropTable("dbo.Pasien");
            DropTable("dbo.JenisObat");
            DropTable("dbo.Obat");
            DropTable("dbo.BillingObat");
            DropTable("dbo.Transaction");
            DropTable("dbo.JenisTindakan");
            DropTable("dbo.Tindakan");
            DropTable("dbo.BillingJasa");
        }
    }
}
