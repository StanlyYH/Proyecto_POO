namespace CitasMedicasApi.DTOs.Citas
{
    public class CitaResponseDto
    {
        public int Id { get; set; }
        public int PacienteId { get; set; }
        public string PacienteNombreCompleto { get; set; } = string.Empty;
        public int DoctorId { get; set; }
        public string DoctorNombreCompleto { get; set; } = string.Empty;
        public DateTime FechaCita { get; set; }
        public string Motivo { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Observacion { get; set; } = string.Empty;
        public DateTime FechaRegistro { get; set; }
    }
}