using CitasMedicasApi.Data;
using CitasMedicasApi.Entities;
using CitasMedicasApp.Constants;
using CitasMedicasApp.Dtos.Common;
using CitasMedicasApp.Dtos.Pacientes;
using CitasMedicasApp.Mappers;
using Microsoft.EntityFrameworkCore;

namespace CitasMedicasApp.Services.Pacientes
{
    public class PacienteService : IPacienteService
    {
        private readonly ApplicationDbContext _context;
        private readonly int PAGE_SIZE;
        private readonly int PAGE_SIZE_LIMIT;

        public PacienteService(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            PAGE_SIZE = configuration.GetValue<int>("PageSize");
            PAGE_SIZE_LIMIT = configuration.GetValue<int>("PageSizeLimit");
        }

         public async Task<ResponseDto<PageDto<List<PacienteDto>>>> GetPageAsync(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            page = Math.Abs(page);
            pageSize = Math.Abs(pageSize);

            pageSize = pageSize <= 0 ? PAGE_SIZE : pageSize;
            pageSize = pageSize > PAGE_SIZE_LIMIT ? PAGE_SIZE_LIMIT : pageSize;

            int startIndex = (page - 1) * pageSize;

            IQueryable<PacienteEntity> pacientesQuery = _context.Pacientes;//.Include(p => p.citas);

            if (!string.IsNullOrEmpty(searchTerm))
            {
                pacientesQuery = pacientesQuery.Where( x => (x.DNI + " " + x.FirstName + " " + x.LastName).Contains(searchTerm));
            }

            int totalRows = await pacientesQuery.CountAsync();

            var pacienteEntity = await pacientesQuery.OrderBy(x => x.FirstName).Skip(startIndex).Take(pageSize).ToListAsync();

            var personsDto = PacienteMapper.ListEntityToListDto(pacienteEntity);

            return new ResponseDto<PageDto<List<PacienteDto>>>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTERS_fOUND,
                Data = new PageDto<List<PacienteDto>>
                {
                    CurrentPage = page == 0 ? 1 : page,
                    PageSize = pageSize,
                    TotalItems = totalRows,
                    TotalPages = (int)Math.Ceiling((double)totalRows / pageSize),
                    Items = personsDto,
                    HasNextPage = startIndex + pageSize < PAGE_SIZE_LIMIT && 
                        page < (int)Math.Ceiling((double)totalRows / pageSize), 
                    HasPreviousPage = page > 1
                }
            };
        }

        public async Task<ResponseDto<PacienteDto>> GetOneByIdAsync(string id)
        {
            var pacienteEntity = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);

            //*ToDo Agregar el include de p.Citas al hacer el merge en Testing
            // var pacienteEntity = await _context.Pacientes.Include(p =>p.Citas)
            //     .FirstOrDefaultAsync(p => p.Id == id);

            if(pacienteEntity is null)
            {
                return new ResponseDto<PacienteDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,
                    Status = false,                    
                };
            }

            return new ResponseDto<PacienteDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_FOUND,
                Status = true,
                Data = new PacienteDto
                {
                    Id = pacienteEntity.Id,
                    DNI = pacienteEntity.DNI,
                    FirstName = pacienteEntity.FirstName,
                    LastName = pacienteEntity.LastName,
                    BirthDate = pacienteEntity.BirthDate,
                    Gender = pacienteEntity.Gender,
                    Phone = pacienteEntity.Phone,
                    Address = pacienteEntity.Address
                }
            };
        }


        public async Task<ResponseDto<PacienteActionResponseDto>> CreateAsync(PacienteCreateDto dto)
        {
            //*Valida si el paciente ya existe
            bool pacienteExiste = await _context.Pacientes.AnyAsync(p=> p.DNI == dto.DNI);

            if (pacienteExiste)
            {
                return new ResponseDto<PacienteActionResponseDto>
                {
                    StatusCode = HttpStatusCode.CONFLICT,
                    Message = "Ya existe un paciente registrado con ese DNI",
                    Status = false
                };
            }

            //*Si el paciente no existe procede a crear 
            PacienteEntity pacienteEntity = PacienteMapper.CreateDtoToEntity(dto);

            _context.Pacientes.Add(pacienteEntity);
            await _context.SaveChangesAsync();
            return new ResponseDto<PacienteActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Message = HttpMessageResponse.REGISTER_CREATED,
                Status = true,
                Data = new PacienteActionResponseDto
                {
                    Id = pacienteEntity.Id
                }
            };
        }

         public async Task<ResponseDto<PacienteActionResponseDto>> EditAsync(string id, PacienteEditDto dto)
        {
            var pacienteEntity = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);

            if(pacienteEntity is null)
            {
                return new ResponseDto<PacienteActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,

                };
            }

            var pacienteEntityUpdated = PacienteMapper.EditDtoToEntity(pacienteEntity, dto);

            _context.Pacientes.Update(pacienteEntityUpdated);
            await _context.SaveChangesAsync();

            return new ResponseDto<PacienteActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_UPDATED,
                Data = new PacienteActionResponseDto
                {
                    Id = id
                }

            };
        }


        public async Task<ResponseDto<PacienteActionResponseDto>> DeleteAsync(string id)
        {
            var pacienteEntity = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id);

            if(pacienteEntity is null)
            {
                return new ResponseDto<PacienteActionResponseDto>
                {
                    StatusCode = HttpStatusCode.NOT_FOUND,
                    Status = false,
                    Message = HttpMessageResponse.REGISTER_NOT_FOUND,

                };
            }

            _context.Pacientes.Remove(pacienteEntity);
            await _context.SaveChangesAsync();

            return new ResponseDto<PacienteActionResponseDto>
            {
                StatusCode = HttpStatusCode.OK,
                Status = true,
                Message = HttpMessageResponse.REGISTER_DELETED,
                Data = new PacienteActionResponseDto
                {
                    Id = id
                }
            };
           
        }




    }
}