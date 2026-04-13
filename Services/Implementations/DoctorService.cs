using CitasMedicasApi.Data;
using CitasMedicasApi.DTOs.Doctores;
using CitasMedicasApi.Entities;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApi.Services.Implementations
{
    public class DoctorService : IDoctorService
    {
        private readonly ApplicationDbContext _context;

        public DoctorService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetAllAsync()
        {
            return await _context.Doctores
                .Include(d => d.CentroSalud)
                .Select(d => new DoctorResponseDto
                {
                    Id = d.Id,
                    Nombres = d.Nombres,
                    Apellidos = d.Apellidos,
                    Telefono = d.Telefono,
                    Correo = d.Correo,
                    Especialidad = d.Especialidad,
                    CentroSaludId = d.CentroSaludId,
                    CentroSaludNombre = d.CentroSalud != null ? d.CentroSalud.Nombre : string.Empty,
                    Estado = d.Estado
                })
                .ToListAsync();
        }

        public async Task<DoctorResponseDto?> GetByIdAsync(int id)
        {
            return await _context.Doctores
                .Include(d => d.CentroSalud)
                .Where(d => d.Id == id)
                .Select(d => new DoctorResponseDto
                {
                    Id = d.Id,
                    Nombres = d.Nombres,
                    Apellidos = d.Apellidos,
                    Telefono = d.Telefono,
                    Correo = d.Correo,
                    Especialidad = d.Especialidad,
                    CentroSaludId = d.CentroSaludId,
                    CentroSaludNombre = d.CentroSalud != null ? d.CentroSalud.Nombre : string.Empty,
                    Estado = d.Estado
                })
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<DoctorResponseDto>> GetByCentroSaludAsync(int centroSaludId)
        {
            return await _context.Doctores
                .Include(d => d.CentroSalud)
                .Where(d => d.CentroSaludId == centroSaludId)
                .Select(d => new DoctorResponseDto
                {
                    Id = d.Id,
                    Nombres = d.Nombres,
                    Apellidos = d.Apellidos,
                    Telefono = d.Telefono,
                    Correo = d.Correo,
                    Especialidad = d.Especialidad,
                    CentroSaludId = d.CentroSaludId,
                    CentroSaludNombre = d.CentroSalud != null ? d.CentroSalud.Nombre : string.Empty,
                    Estado = d.Estado
                })
                .ToListAsync();
        }

        public async Task<DoctorResponseDto?> CreateAsync(DoctorCreateDto dto)
        {
            var centroExiste = await _context.CentrosSalud.AnyAsync(c => c.Id == dto.CentroSaludId);
            if (!centroExiste)
            {
                return null;
            }

            var correoExiste = await _context.Doctores.AnyAsync(d => d.Correo == dto.Correo);
            if (correoExiste)
            {
                return null;
            }

            var doctor = new DoctorEntity
            {
                Nombres = dto.Nombres,
                Apellidos = dto.Apellidos,
                Telefono = dto.Telefono,
                Correo = dto.Correo,
                Especialidad = dto.Especialidad,
                CentroSaludId = dto.CentroSaludId,
                Estado = dto.Estado
            };

            _context.Doctores.Add(doctor);
            await _context.SaveChangesAsync();

            var centro = await _context.CentrosSalud.FindAsync(dto.CentroSaludId);

            return new DoctorResponseDto
            {
                Id = doctor.Id,
                Nombres = doctor.Nombres,
                Apellidos = doctor.Apellidos,
                Telefono = doctor.Telefono,
                Correo = doctor.Correo,
                Especialidad = doctor.Especialidad,
                CentroSaludId = doctor.CentroSaludId,
                CentroSaludNombre = centro?.Nombre ?? string.Empty,
                Estado = doctor.Estado
            };
        }

        public async Task<bool> UpdateAsync(int id, DoctorUpdateDto dto)
        {
            var doctor = await _context.Doctores.FindAsync(id);
            if (doctor == null)
            {
                return false;
            }

            var centroExiste = await _context.CentrosSalud.AnyAsync(c => c.Id == dto.CentroSaludId);
            if (!centroExiste)
            {
                return false;
            }

            var correoExiste = await _context.Doctores.AnyAsync(d => d.Correo == dto.Correo && d.Id != id);
            if (correoExiste)
            {
                return false;
            }

            doctor.Nombres = dto.Nombres;
            doctor.Apellidos = dto.Apellidos;
            doctor.Telefono = dto.Telefono;
            doctor.Correo = dto.Correo;
            doctor.Especialidad = dto.Especialidad;
            doctor.CentroSaludId = dto.CentroSaludId;
            doctor.Estado = dto.Estado;

            _context.Doctores.Update(doctor);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var doctor = await _context.Doctores.FindAsync(id);
            if (doctor == null)
            {
                return false;
            }

            var tieneCitas = await _context.Citas.AnyAsync(c => c.DoctorId == id);
            if (tieneCitas)
            {
                return false;
            }

            _context.Doctores.Remove(doctor);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}