using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using msLicenca.Entity;
using msLicenca.Service;

namespace msLicenca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicencaController : ControllerBase
    {
        private readonly ILicencaService _licencaService;
        public LicencaController(ILicencaService licencaService)
        {
            _licencaService = licencaService;
            
        }

        [HttpGet]
        public async Task<ActionResult<List<Licenca>>> GetAll()
        {
            var licencas = await _licencaService.GetAllLicencasAsync();
            return Ok(licencas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Licenca>> GetById(int id)
        {
            var licenca = await _licencaService.GetLicencaByIdAsync(id);
            if (licenca == null)
                return NotFound();

            return Ok(licenca);
        }

        [HttpPost]
        public async Task<ActionResult<Licenca>> Create(Licenca licenca)
        {
            var createdLicenca = await _licencaService.CreateLicencaAsync(licenca);
            return CreatedAtAction(nameof(GetById), new { id = createdLicenca.Id }, createdLicenca);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Licenca>> Update(int id, Licenca licenca)
        {
            if (id != licenca.Id)
                return BadRequest("ID mismatch");

            var updatedLicenca = await _licencaService.UpdateLicencaAsync(id, licenca);

            if (updatedLicenca == null)
                return NotFound();

            return Ok(updatedLicenca);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _licencaService.DeleteLicencaAsync(id);
            if (!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
