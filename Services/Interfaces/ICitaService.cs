using CitasMedicasApi.DTOs.Citas;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface ICitaService
    {
        Task<IEnumerable<CitaResponseDto>> GetAllAsync();
        Task<CitaResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<CitaResponseDto>> GetByPacienteAsync(int pacienteId);
        Task<IEnumerable<CitaResponseDto>> GetByDoctorAsync(int doctorId);
        Task<CitaResponseDto?> CreateAsync(CitaCreateDto dto);
        Task<bool> UpdateAsync(int id, CitaUpdateDto dto);
        Task<bool> CambiarEstadoAsync(int id, CambiarEstadoCitaDto dto);
        Task<bool> DeleteAsync(int id);
    }
}