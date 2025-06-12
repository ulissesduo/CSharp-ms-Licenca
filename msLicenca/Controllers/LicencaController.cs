using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using msLicenca.Dto;
using msLicenca.Entity;
using msLicenca.Service;

namespace msLicenca.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicencaController : ControllerBase
    {
        private readonly ILicencaService _licencaService;
        private readonly IMapper _mapper;
        public LicencaController(ILicencaService licencaService, IMapper mapper)
        {
            _licencaService = licencaService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<ActionResult<List<LicencaResponseDTO>>> GetAll()
        {
            var licencas = await _licencaService.GetAllLicencasAsync();
            var response = _mapper.Map<List<LicencaResponseDTO>>(licencas);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LicencaResponseDTO>> GetById(int id)
        {
            var licenca = await _licencaService.GetLicencaByIdAsync(id);
            if (licenca == null)
                return NotFound();

            var response = _mapper.Map<LicencaResponseDTO>(licenca);
            return Ok(response);
        }


        [HttpPost]
        public async Task<ActionResult<LicencaResponseDTO>> Create([FromBody] LicencaRequestDTO dto)
        {
            if (dto == null) return BadRequest("Invalid licence");

            var licenca = _mapper.Map<Licenca>(dto);
            var createdLicenca = await _licencaService.CreateLicencaAsync(licenca);
            var response = _mapper.Map<LicencaResponseDTO>(createdLicenca);

            return CreatedAtAction(nameof(GetById), new { id = createdLicenca.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LicencaResponseDTO>> Update(int id, [FromBody] LicencaRequestDTO dto)
        {
            var existing = await _licencaService.GetLicencaByIdAsync(id);
            if (existing == null)
                return NotFound();

            var updatedEntity = _mapper.Map(dto, existing);
            var updatedLicenca = await _licencaService.UpdateLicencaAsync(id, updatedEntity);
            var response = _mapper.Map<LicencaResponseDTO>(updatedLicenca);

            return Ok(response);
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
