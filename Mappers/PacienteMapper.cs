using System.ComponentModel.DataAnnotations;
using CitasMedicasApi.Entities;
using CitasMedicasApp.Dtos.Pacientes;

namespace CitasMedicasApp.Mappers
{
    public class PacienteMapper
    {
        public static PacienteEntity CreateDtoToEntity(PacienteCreateDto dto)
        {
            return new PacienteEntity
            {
                Id = Guid.NewGuid().ToString(),
                DNI = dto.DNI,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Gender = dto.Gender,
                Phone = dto.Phone,
                Address = dto.Address
            };
        }

        public static PacienteEntity EditDtoToEntity(PacienteEntity entity, PacienteEditDto dto)
        {
            entity.FirstName = dto.FirstName;
            entity.LastName = dto.LastName;
            entity.DNI = dto.DNI;
            entity.BirthDate = dto.BirthDate;
            entity.Gender = dto.Gender;
            entity.Phone = dto.Phone;
            entity.Address = dto.Address;
            return entity;
        }

        public static List<PacienteDto> ListEntityToListDto(List<PacienteEntity> entities)
        {
            return entities.Select(person => new PacienteDto
            {
                Id = person.Id,
                DNI = person.DNI,
                FirstName = person.FirstName,
                LastName = person.LastName,
                BirthDate = person.BirthDate,
                Gender = person.Gender,
                Phone = person.Phone,
                Address = person.Address,
            }).ToList();
        }
    }
}