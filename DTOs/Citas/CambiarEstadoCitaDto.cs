using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApi.DTOs.Citas
{
    public class CambiarEstadoCitaDto
    {
        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = string.Empty;
    }
}