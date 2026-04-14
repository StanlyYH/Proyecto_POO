using CitasMedicasApp.Dtos.Common;
using CitasMedicasApp.Dtos.Pacientes;

namespace CitasMedicasApp.Services.Pacientes
{
    public interface IPacienteService
    {
        Task<ResponseDto<PageDto<List<PacienteDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10);
        Task<ResponseDto<PacienteDto>> GetOneByIdAsync(string id);
        Task<ResponseDto<PacienteActionResponseDto>> CreateAsync(PacienteCreateDto dto);
        Task<ResponseDto<PacienteActionResponseDto>> EditAsync(string id, PacienteEditDto dto);
        Task<ResponseDto<PacienteActionResponseDto>> DeleteAsync(string id);
    }
}