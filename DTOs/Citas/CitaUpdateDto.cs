using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApi.DTOs.Citas
{
    public class CitaUpdateDto
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

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;

        [MaxLength(250)]
        public string Observacion { get; set; } = string.Empty;
    }
}