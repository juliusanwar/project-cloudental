using CloudClinic.Models.DataModel;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CloudClinic.Models
{
    public class ClinicContext : DbContext
    {
        public DbSet<Appointment> Appointment { get; set; }
        public DbSet<Pengguna> Pengguna { get; set; }
        public DbSet<Pasien> Pasien { get; set; }
        public DbSet<Barang> Barang { get; set; }
        public DbSet<JenisBarang> JenisBarang { get; set; }
        public DbSet<Jadwal> Jadwal { get; set; }
        public DbSet<Diagnosis> Diagnosis { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<Tindakan> Tindakan { get; set; }
        public DbSet<JenisTindakan> JenisTindakan { get; set; }
        public DbSet<BillingJasa> BillingJasa { get; set; }
        public DbSet<BillingObat> BillingObat { get; set; }
        public DbSet<Pembelian> Pembelian { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //pluralize off
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);

            // Disable FOREIGN KEY Constraints
            //---------------------------------

            //modelBuilder.Entity<PersonPhoto>()
            //    .HasRequired(p => p.PhotoOf)
            //    .WithOptional(p => p.Photo);

            //modelBuilder.Entity<Jadwal>()
            //    .HasRequired(a => a.Appointment)
            //    .WithMany()
            //    .HasForeignKey(a => a.AppointmentId);

            modelBuilder.Entity<Jadwal>() // Jadwal
                .HasOptional<Appointment>(x => x.Appointment) // Punya optional Appointment (Nullable)
                .WithOptionalPrincipal(x => x.Jadwal); // Dimana Jadwal yang jadi principal dan hold foreign key AppointmentId

            modelBuilder.Entity<Diagnosis>()
                    .HasRequired(d => d.Pasien)
                    .WithMany(w => w.Diagnosis)
                    .WillCascadeOnDelete(false);

            //modelBuilder.Entity<Diagnosis>()
            //    .HasMany(t => t.Pasien)
            //    .WithMany(t => t.Diagnosis);

            //modelBuilder.Entity<Diagnosis>()
            //    .HasRequired(c => c.Pasien)
                
            //    .WithMany()
                //.WillCascadeOnDelete(false);

            //modelBuilder.Entity<Pasien>()
            //    .HasRequired(s => s.Patient)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);
        }
    }
}