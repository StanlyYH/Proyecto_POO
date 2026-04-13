using CitasMedicasApi.DTOs.Doctores;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DoctoresController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        public DoctoresController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var doctores = await _doctorService.GetAllAsync();
            return Ok(doctores);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var doctor = await _doctorService.GetByIdAsync(id);
            if (doctor == null)
            {
                return NotFound(new { message = "Doctor no encontrado." });
            }

            return Ok(doctor);
        }

        [HttpGet("centro/{centroSaludId}")]
        public async Task<IActionResult> GetByCentroSalud(int centroSaludId)
        {
            var doctores = await _doctorService.GetByCentroSaludAsync(centroSaludId);
            return Ok(doctores);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] DoctorCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var doctor = await _doctorService.CreateAsync(dto);
            if (doctor == null)
            {
                return BadRequest(new { message = "No se pudo crear el doctor. Verifica el centro de salud o el correo." });
            }

            return CreatedAtAction(nameof(GetById), new { id = doctor.Id }, doctor);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] DoctorUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _doctorService.UpdateAsync(id, dto);
            if (!updated)
            {
                return BadRequest(new { message = "No se pudo actualizar el doctor." });
            }

            return Ok(new { message = "Doctor actualizado correctamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _doctorService.DeleteAsync(id);
            if (!deleted)
            {
                return BadRequest(new { message = "No se pudo eliminar el doctor. Puede que no exista o tenga citas asociadas." });
            }

            return Ok(new { message = "Doctor eliminado correctamente." });
        }
    }
}