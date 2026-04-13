using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApi.DTOs.Doctores
{
    public class DoctorCreateDto
    {
        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [EmailAddress]
        public string Correo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Especialidad { get; set; } = string.Empty;

        [Required]
        public int CentroSaludId { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Activo";
    }
}
