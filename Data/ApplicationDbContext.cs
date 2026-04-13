using CitasMedicasApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<PacienteEntity> Pacientes { get; set; }
        public DbSet<DoctorEntity> Doctores { get; set; }
        public DbSet<CentroSaludEntity> CentrosSalud { get; set; }
        public DbSet<CitaEntity> Citas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<PacienteEntity>()
                .HasIndex(p => p.Dni)
                .IsUnique();

            modelBuilder.Entity<DoctorEntity>()
                .HasIndex(d => d.Correo)
                .IsUnique();

            modelBuilder.Entity<DoctorEntity>()
                .HasOne(d => d.CentroSalud)
                .WithMany(c => c.Doctores)
                .HasForeignKey(d => d.CentroSaludId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CitaEntity>()
                .HasOne(c => c.Paciente)
                .WithMany(p => p.Citas)
                .HasForeignKey(c => c.PacienteId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CitaEntity>()
                .HasOne(c => c.Doctor)
                .WithMany(d => d.Citas)
                .HasForeignKey(c => c.DoctorId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}