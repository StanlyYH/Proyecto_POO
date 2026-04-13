using CitasMedicasApi.DTOs.Doctores;

namespace CitasMedicasApi.Services.Interfaces
{
    public interface IDoctorService
    {
        Task<IEnumerable<DoctorResponseDto>> GetAllAsync();
        Task<DoctorResponseDto?> GetByIdAsync(int id);
        Task<IEnumerable<DoctorResponseDto>> GetByCentroSaludAsync(int centroSaludId);
        Task<DoctorResponseDto?> CreateAsync(DoctorCreateDto dto);
        Task<bool> UpdateAsync(int id, DoctorUpdateDto dto);
        Task<bool> DeleteAsync(int id);
    }
}