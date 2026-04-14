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
    }
}