using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApi.Entities
{
    public class CitaEntity
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }

        [ForeignKey(nameof(PacienteId))]
        public PacienteEntity? Paciente { get; set; }

        public int DoctorId { get; set; }

        [ForeignKey(nameof(DoctorId))]
        public DoctorEntity? Doctor { get; set; }

        public DateTime FechaCita { get; set; }

        [Required]
        [MaxLength(250)]
        public string Motivo { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Pendiente";

        [MaxLength(250)]
        public string Observacion { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;
    }
}