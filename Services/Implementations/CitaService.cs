using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Citas;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class CitaService : ICitaService
    {
        private readonly ApplicationDbContext _context;

        public CitaService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<CitaResponseDto>> GetAllAsync()
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Select(c => new CitaResponseDto
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    PacienteNombreCompleto = c.Paciente != null ? $"{c.Paciente.Nombres} {c.Paciente.Apellidos}" : string.Empty,
                    DoctorId = c.DoctorId,
                    DoctorNombreCompleto = c.Doctor != null ? $"{c.Doctor.Nombres} {c.Doctor.Apellidos}" : string.Empty,
                    FechaCita = c.FechaCita,
                    Motivo = c.Motivo,
                    Estado = c.Estado,
                    Observacion = c.Observacion,
                    FechaRegistro = c.FechaRegistro
                })
                .ToListAsync();
        }

        public async Task<CitaResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.Id == id)
                .Select(c => new CitaResponseDto
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    PacienteNombreCompleto = c.Paciente != null ? $"{c.Paciente.Nombres} {c.Paciente.Apellidos}" : string.Empty,
                    DoctorId = c.DoctorId,
                    DoctorNombreCompleto = c.Doctor != null ? $"{c.Doctor.Nombres} {c.Doctor.Apellidos}" : string.Empty,
                    FechaCita = c.FechaCita,
                    Motivo = c.Motivo,
                    Estado = c.Estado,
                    Observacion = c.Observacion,
                    FechaRegistro = c.FechaRegistro
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<CitaResponseDto>> GetByPacienteAsync(int pacienteId)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.PacienteId == pacienteId)
                .Select(c => new CitaResponseDto
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    PacienteNombreCompleto = c.Paciente != null ? $"{c.Paciente.Nombres} {c.Paciente.Apellidos}" : string.Empty,
                    DoctorId = c.DoctorId,
                    DoctorNombreCompleto = c.Doctor != null ? $"{c.Doctor.Nombres} {c.Doctor.Apellidos}" : string.Empty,
                    FechaCita = c.FechaCita,
                    Motivo = c.Motivo,
                    Estado = c.Estado,
                    Observacion = c.Observacion,
                    FechaRegistro = c.FechaRegistro
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<CitaResponseDto>> GetByDoctorAsync(int doctorId)
        {
            return await _context.Citas
                .Include(c => c.Paciente)
                .Include(c => c.Doctor)
                .Where(c => c.DoctorId == doctorId)
                .Select(c => new CitaResponseDto
                {
                    Id = c.Id,
                    PacienteId = c.PacienteId,
                    PacienteNombreCompleto = c.Paciente != null ? $"{c.Paciente.Nombres} {c.Paciente.Apellidos}" : string.Empty,
                    DoctorId = c.DoctorId,
                    DoctorNombreCompleto = c.Doctor != null ? $"{c.Doctor.Nombres} {c.Doctor.Apellidos}" : string.Empty,
                    FechaCita = c.FechaCita,
                    Motivo = c.Motivo,
                    Estado = c.Estado,
                    Observacion = c.Observacion,
                    FechaRegistro = c.FechaRegistro
                })
                .ToListAsync();
        }

        public async Task<CitaResponseDto?> CreateAsync(CitaCreateDto dto)
        {
            var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == dto.PacienteId);
            if (!pacienteExiste)
            {
                return null;
            }

            var doctorExiste = await _context.Doctores.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExiste)
            {
                return null;
            }

            var cita = new CitaEntity
            {
                PacienteId = dto.PacienteId,
                DoctorId = dto.DoctorId,
                FechaCita = dto.FechaCita,
                Motivo = dto.Motivo,
                Estado = "Pendiente",
                Observacion = dto.Observacion,
                FechaRegistro = DateTime.Now
            };

            _context.Citas.Add(cita);
            await _context.SaveChangesAsync();

            var paciente = await _context.Pacientes.FindAsync(dto.PacienteId);
            var doctor = await _context.Doctores.FindAsync(dto.DoctorId);

            return new CitaResponseDto
            {
                Id = cita.Id,
                PacienteId = cita.PacienteId,
                PacienteNombreCompleto = paciente != null ? $"{paciente.Nombres} {paciente.Apellidos}" : string.Empty,
                DoctorId = cita.DoctorId,
                DoctorNombreCompleto = doctor != null ? $"{doctor.Nombres} {doctor.Apellidos}" : string.Empty,
                FechaCita = cita.FechaCita,
                Motivo = cita.Motivo,
                Estado = cita.Estado,
                Observacion = cita.Observacion,
                FechaRegistro = cita.FechaRegistro
            };
        }

        public async Task<bool> UpdateAsync(int id, CitaUpdateDto dto)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            var pacienteExiste = await _context.Pacientes.AnyAsync(p => p.Id == dto.PacienteId);
            if (!pacienteExiste)
            {
                return false;
            }

            var doctorExiste = await _context.Doctores.AnyAsync(d => d.Id == dto.DoctorId);
            if (!doctorExiste)
            {
                return false;
            }

            cita.PacienteId = dto.PacienteId;
            cita.DoctorId = dto.DoctorId;
            cita.FechaCita = dto.FechaCita;
            cita.Motivo = dto.Motivo;
            cita.Estado = dto.Estado;
            cita.Observacion = dto.Observacion;

            _context.Citas.Update(cita);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> CambiarEstadoAsync(int id, CambiarEstadoCitaDto dto)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            cita.Estado = dto.Estado;
            _context.Citas.Update(cita);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var cita = await _context.Citas.FindAsync(id);
            if (cita == null)
            {
                return false;
            }

            _context.Citas.Remove(cita);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}