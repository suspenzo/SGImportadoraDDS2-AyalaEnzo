using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bodeguero")]
    public class UnidadFisicaController : ControllerBase
    {
        private readonly IUnidadFisicaService _service;
        public UnidadFisicaController(IUnidadFisicaService service) => _service = service;

        [HttpGet("disponibles/{idProducto}")]
        public async Task<IActionResult> GetDisponibles(int idProducto)
        {
            return Ok(await _service.GetDisponiblesByProductoIdAsync(idProducto));
        }
    }
}