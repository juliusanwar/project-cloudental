namespace CloudClinic.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change1db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BillingJasas",
                c => new
                    {
                        BilJasaId = c.Int(nullable: false, identity: true),
                        DoctorId = c.Int(nullable: false),
                        PatientId = c.Int(nullable: false),
                        Diagnosa = c.String(),
                        TindakanId = c.Int(nullable: false),
                        HargaJasa = c.Int(nullable: false),
                        Doctor_DoctorId = c.String(maxLength: 128),
                        Patient_PatientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BilJasaId)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId)
                .ForeignKey("dbo.Tindakans", t => t.TindakanId, cascadeDelete: true)
                .Index(t => t.TindakanId)
                .Index(t => t.Doctor_DoctorId)
                .Index(t => t.Patient_PatientId);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        DoctorId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(nullable: false),
                        Nama = c.String(),
                        Alamat = c.String(),
                        Telp = c.String(),
                        Email = c.String(),
                    })
                .PrimaryKey(t => t.DoctorId);
            
            CreateTable(
                "dbo.BillingObats",
                c => new
                    {
                        BilObatId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        ObatId = c.Int(nullable: false),
                        Qty = c.Int(nullable: false),
                        TotalHarga = c.Int(nullable: false),
                        Doctor_DoctorId = c.String(maxLength: 128),
                        Patient_PatientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.BilObatId)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.Obats", t => t.ObatId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId)
                .Index(t => t.ObatId)
                .Index(t => t.Doctor_DoctorId)
                .Index(t => t.Patient_PatientId);
            
            CreateTable(
                "dbo.Obats",
                c => new
                    {
                        ObatId = c.Int(nullable: false, identity: true),
                        Nama = c.String(),
                        JenisObat = c.String(),
                        Kategori = c.String(),
                        Harga = c.Int(nullable: false),
                        Stok = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ObatId);
            
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        PatientId = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        Nama = c.String(),
                        TglLhr = c.DateTime(nullable: false),
                        Gender = c.String(),
                        GolDarah = c.String(),
                        Alamat = c.String(),
                        Telp = c.String(),
                        TglRegistrasi = c.DateTime(nullable: false),
                        RiwayatSakit = c.String(),
                    })
                .PrimaryKey(t => t.PatientId);
            
            CreateTable(
                "dbo.RekamMedis",
                c => new
                    {
                        RekamId = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        Anamnesa = c.String(),
                        Diagnosa = c.String(),
                        BilJasaId = c.Int(nullable: false),
                        BilObatId = c.Int(nullable: false),
                        Doctor_DoctorId = c.String(maxLength: 128),
                        Patient_PatientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.RekamId)
                .ForeignKey("dbo.BillingJasas", t => t.BilJasaId, cascadeDelete: true)
                .ForeignKey("dbo.BillingObats", t => t.BilObatId, cascadeDelete: true)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId)
                .Index(t => t.BilJasaId)
                .Index(t => t.BilObatId)
                .Index(t => t.Doctor_DoctorId)
                .Index(t => t.Patient_PatientId);
            
            CreateTable(
                "dbo.Reservations",
                c => new
                    {
                        NomorDaftar = c.Int(nullable: false, identity: true),
                        PatientId = c.Int(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        TglRegistrasi = c.DateTime(nullable: false),
                        Keluhan = c.String(),
                        Doctor_DoctorId = c.String(maxLength: 128),
                        Patient_PatientId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NomorDaftar)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId)
                .Index(t => t.Doctor_DoctorId)
                .Index(t => t.Patient_PatientId);
            
            CreateTable(
                "dbo.Transactions",
                c => new
                    {
                        TransactionId = c.Int(nullable: false, identity: true),
                        PatienId = c.Int(nullable: false),
                        BilJasaId = c.Int(nullable: false),
                        BilObatId = c.Int(nullable: false),
                        TotalBayar = c.Int(nullable: false),
                        Bayar = c.Int(nullable: false),
                        Kembalian = c.Int(nullable: false),
                        TglBayar = c.DateTime(nullable: false),
                        Patient_PatientId = c.String(maxLength: 128),
                        Doctor_DoctorId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TransactionId)
                .ForeignKey("dbo.BillingJasas", t => t.BilJasaId, cascadeDelete: true)
                .ForeignKey("dbo.BillingObats", t => t.BilObatId, cascadeDelete: true)
                .ForeignKey("dbo.Patients", t => t.Patient_PatientId)
                .ForeignKey("dbo.Doctors", t => t.Doctor_DoctorId)
                .Index(t => t.BilJasaId)
                .Index(t => t.BilObatId)
                .Index(t => t.Patient_PatientId)
                .Index(t => t.Doctor_DoctorId);
            
            CreateTable(
                "dbo.Tindakans",
                c => new
                    {
                        TindakanId = c.Int(nullable: false, identity: true),
                        Nama = c.String(),
                        JenisTindakanId = c.Int(nullable: false),
                        Alat = c.String(),
                        Harga = c.Int(nullable: false),
                        Diagnosis = c.String(),
                        TotalHarga = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.TindakanId)
                .ForeignKey("dbo.JenisTindakans", t => t.JenisTindakanId, cascadeDelete: true)
                .Index(t => t.JenisTindakanId);
            
            CreateTable(
                "dbo.JenisTindakans",
                c => new
                    {
                        JenisTindakanId = c.Int(nullable: false, identity: true),
                        NamaTindakan = c.String(),
                    })
                .PrimaryKey(t => t.JenisTindakanId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BillingJasas", "TindakanId", "dbo.Tindakans");
            DropForeignKey("dbo.Tindakans", "JenisTindakanId", "dbo.JenisTindakans");
            DropForeignKey("dbo.Transactions", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.Transactions", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.Transactions", "BilObatId", "dbo.BillingObats");
            DropForeignKey("dbo.Transactions", "BilJasaId", "dbo.BillingJasas");
            DropForeignKey("dbo.Reservations", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.Reservations", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.RekamMedis", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.RekamMedis", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.RekamMedis", "BilObatId", "dbo.BillingObats");
            DropForeignKey("dbo.RekamMedis", "BilJasaId", "dbo.BillingJasas");
            DropForeignKey("dbo.BillingObats", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.BillingJasas", "Patient_PatientId", "dbo.Patients");
            DropForeignKey("dbo.BillingObats", "ObatId", "dbo.Obats");
            DropForeignKey("dbo.BillingObats", "Doctor_DoctorId", "dbo.Doctors");
            DropForeignKey("dbo.BillingJasas", "Doctor_DoctorId", "dbo.Doctors");
            DropIndex("dbo.Tindakans", new[] { "JenisTindakanId" });
            DropIndex("dbo.Transactions", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.Transactions", new[] { "Patient_PatientId" });
            DropIndex("dbo.Transactions", new[] { "BilObatId" });
            DropIndex("dbo.Transactions", new[] { "BilJasaId" });
            DropIndex("dbo.Reservations", new[] { "Patient_PatientId" });
            DropIndex("dbo.Reservations", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.RekamMedis", new[] { "Patient_PatientId" });
            DropIndex("dbo.RekamMedis", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.RekamMedis", new[] { "BilObatId" });
            DropIndex("dbo.RekamMedis", new[] { "BilJasaId" });
            DropIndex("dbo.BillingObats", new[] { "Patient_PatientId" });
            DropIndex("dbo.BillingObats", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.BillingObats", new[] { "ObatId" });
            DropIndex("dbo.BillingJasas", new[] { "Patient_PatientId" });
            DropIndex("dbo.BillingJasas", new[] { "Doctor_DoctorId" });
            DropIndex("dbo.BillingJasas", new[] { "TindakanId" });
            DropTable("dbo.JenisTindakans");
            DropTable("dbo.Tindakans");
            DropTable("dbo.Transactions");
            DropTable("dbo.Reservations");
            DropTable("dbo.RekamMedis");
            DropTable("dbo.Patients");
            DropTable("dbo.Obats");
            DropTable("dbo.BillingObats");
            DropTable("dbo.Doctors");
            DropTable("dbo.BillingJasas");
        }
    }
}
