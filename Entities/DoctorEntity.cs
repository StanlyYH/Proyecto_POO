using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CitasMedicasApi.Entities
{
    public class DoctorEntity
    {
        public int Id { get; set; }

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
        public string Correo { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Especialidad { get; set; } = string.Empty;

        public int CentroSaludId { get; set; }

        [ForeignKey(nameof(CentroSaludId))]
        public CentroSaludEntity? CentroSalud { get; set; }

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Activo";

        public ICollection<CitaEntity> Citas { get; set; } = new List<CitaEntity>();
    }
}