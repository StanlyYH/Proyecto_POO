using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApi.DTOs.Citas
{
    public class CitaCreateDto
    {
        [Required]
        public int PacienteId { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        public DateTime FechaCita { get; set; }

        [Required]
        [MaxLength(250)]
        public string Motivo { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Observacion { get; set; } = string.Empty;
    }
}