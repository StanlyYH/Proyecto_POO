using System.ComponentModel.DataAnnotations;

namespace CitasMedicasApi.Entities
{
    public class CentroSaludEntity
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(200)]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Municipio { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Departamento { get; set; } = string.Empty;

        [MaxLength(20)]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Estado { get; set; } = "Activo";

        public ICollection<DoctorEntity> Doctores { get; set; } = new List<DoctorEntity>();
    }
}