using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace CloudClinic.Models
{
    public class ClinicContext : DbContext
    {
        public DbSet<Pengguna> Pengguna { get; set; }
        public DbSet<Pasien> Pasien { get; set; }
        public DbSet<Obat> Obat { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        //public DbSet<RekamMedis> RekamMedis { get; set; }
        public DbSet<Reservation> Reservation { get; set; }
        public DbSet<JenisTindakan> JenisTindakans{ get; set; }
        public DbSet<Tindakan> Tindakan { get; set; }
        public DbSet<BillingJasa> BillingJasa { get; set; }
        public DbSet<BillingObat> BillingObat { get; set; }
        public DbSet<JenisObat> JenisObat { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //pluralize off
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            //base.OnModelCreating(modelBuilder);
        }
    }
}