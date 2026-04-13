using CitasMedicasApi.DTOs.Citas;
using CitasMedicasApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CitasMedicasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CitasController : ControllerBase
    {
        private readonly ICitaService _citaService;

        public CitasController(ICitaService citaService)
        {
            _citaService = citaService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var citas = await _citaService.GetAllAsync();
            return Ok(citas);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var cita = await _citaService.GetByIdAsync(id);
            if (cita == null)
            {
                return NotFound(new { message = "Cita no encontrada." });
            }

            return Ok(cita);
        }

        [HttpGet("paciente/{pacienteId}")]
        public async Task<IActionResult> GetByPaciente(int pacienteId)
        {
            var citas = await _citaService.GetByPacienteAsync(pacienteId);
            return Ok(citas);
        }

        [HttpGet("doctor/{doctorId}")]
        public async Task<IActionResult> GetByDoctor(int doctorId)
        {
            var citas = await _citaService.GetByDoctorAsync(doctorId);
            return Ok(citas);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CitaCreateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var cita = await _citaService.CreateAsync(dto);
            if (cita == null)
            {
                return BadRequest(new { message = "No se pudo crear la cita. Verifica el paciente y el doctor." });
            }

            return CreatedAtAction(nameof(GetById), new { id = cita.Id }, cita);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] CitaUpdateDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _citaService.UpdateAsync(id, dto);
            if (!updated)
            {
                return BadRequest(new { message = "No se pudo actualizar la cita." });
            }

            return Ok(new { message = "Cita actualizada correctamente." });
        }

        [HttpPatch("{id}/estado")]
        public async Task<IActionResult> CambiarEstado(int id, [FromBody] CambiarEstadoCitaDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updated = await _citaService.CambiarEstadoAsync(id, dto);
            if (!updated)
            {
                return BadRequest(new { message = "No se pudo cambiar el estado de la cita." });
            }

            return Ok(new { message = "Estado de la cita actualizado correctamente." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _citaService.DeleteAsync(id);
            if (!deleted)
            {
                return NotFound(new { message = "Cita no encontrada." });
            }

            return Ok(new { message = "Cita eliminada correctamente." });
        }
    }
}