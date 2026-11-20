using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MarcaController : ControllerBase
    {
        private readonly IMarcaService _marcaService;
        public MarcaController(IMarcaService marcaService) => _marcaService = marcaService;

        [HttpGet]
        public async Task<ActionResult<List<MarcaResponse>>> GetAll() => Ok(await _marcaService.GetAllAsync());

        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<MarcaResponse>> Create(MarcaRequest request)
        {
            var created = await _marcaService.CreateAsync(request);
            if (created == null) return BadRequest("La marca ya existe.");
            return Ok(created);
        }

        // ... (Agrega Update y Delete igual que en Categoría)
    }
}