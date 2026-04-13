using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApi.Entities
{
    public class PacienteEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Dni { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Nombres { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellidos { get; set; } = string.Empty;

        public DateTime FechaNacimiento { get; set; }

        [Required]
        [MaxLength(10)]
        public string Sexo { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [MaxLength(200)]
        public string Direccion { get; set; } = string.Empty;

        public DateTime FechaRegistro { get; set; } = DateTime.Now;

        public ICollection<CitaEntity> Citas { get; set; } = new List<CitaEntity>();
    }
}