using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;
using POS.Shared.Enums;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Vendedor")] // Vendedores pueden consultar y crear garantías
    public class GarantiaController : ControllerBase
    {
        private readonly IGarantiaService _garantiaService;

        public GarantiaController(IGarantiaService garantiaService)
        {
            _garantiaService = garantiaService;
        }

        [HttpGet]
        public async Task<ActionResult<List<GarantiaResponseDto>>> GetAll()
        {
            return Ok(await _garantiaService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult<GarantiaResponseDto>> Create(GarantiaRequestDto request)
        {
            var created = await _garantiaService.CreateAsync(request);
            if (created == null)
                return BadRequest("La unidad física ya cuenta con una garantía registrada.");

            return Ok(created);
        }

        [HttpPut("{id}/estado")]
        [Authorize(Roles = "Administrador")] // Solo el admin puede anular o expirar garantías
        public async Task<IActionResult> UpdateEstado(int id, [FromBody] EstadoGarantia nuevoEstado)
        {
            var updated = await _garantiaService.UpdateEstadoAsync(id, nuevoEstado);
            if (!updated) return NotFound("Garantía no encontrada");

            return NoContent();
        }
    }
}