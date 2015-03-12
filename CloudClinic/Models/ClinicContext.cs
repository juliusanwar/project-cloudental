using System.Data.Entity;

namespace CloudClinic.Models
{
    public class ClinicContext : DbContext
    {
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Obat> Obats { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<RekamMedis> RekamMedisis { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Tindakan> Tindakans { get; set; }
        public DbSet<JenisTindakan> JenisTindakans { get; set; }
        public DbSet<BillingJasa> BillingJasas { get; set; }
        public DbSet<BillingObat> BillingObats { get; set; }
    }
}