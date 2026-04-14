using System.Xml;
using CitasMedicasApp.Dtos.Pacientes;
using CitasMedicasApp.Services.Pacientes;
using Microsoft.AspNetCore.Mvc;


namespace CitasMedicasApp.Controllers
{
    [Route("api/pacientes")]
    [ApiController]
    public class PacientesController : ControllerBase
    {
        private readonly IPacienteService _pacienteService;

        public PacientesController(IPacienteService pacienteService)
        {
            _pacienteService = pacienteService;
        }

        [HttpGet]
        public async Task<ActionResult> GetPage(string searchTerm = "", int page = 1, int pageSize = 10)
        {
            var response = await _pacienteService.GetPageAsync(searchTerm, page, pageSize);
            return StatusCode(response.StatusCode, response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetOne(string id)
        {
            var result = await _pacienteService.GetOneByIdAsync(id);
            return StatusCode(result.StatusCode, result);
            
        }

        [HttpPost]
        public async Task<ActionResult> Create(PacienteCreateDto dto)
        {
            
            var result = await _pacienteService.CreateAsync(dto);
            return StatusCode(result.StatusCode, result);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, PacienteEditDto dto)
        {
            var result = await  _pacienteService.EditAsync(id, dto);
            return StatusCode(result.StatusCode, result);
        }
    
         [HttpDelete("{id}")]
         public async Task<ActionResult> Delete(string id)
        {
            
            var result = await _pacienteService.DeleteAsync(id    );
            return StatusCode(result.StatusCode, result);
        }

    }
}