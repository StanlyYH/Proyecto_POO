namespace CitasMedicasApi.DTOs.Doctores
{
    public class DoctorResponseDto
    {
        public int Id { get; set; }
        public string Nombres { get; set; } = string.Empty;
        public string Apellidos { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public string Correo { get; set; } = string.Empty;
        public string Especialidad { get; set; } = string.Empty;
        public int CentroSaludId { get; set; }
        public string CentroSaludNombre { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
    }
}