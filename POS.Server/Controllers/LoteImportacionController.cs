using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Administrador,Bodeguero")]
    public class LoteImportacionController : ControllerBase
    {
        private readonly ILoteImportacionService _service;
        public LoteImportacionController(ILoteImportacionService service) => _service = service;

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpPost]
        public async Task<IActionResult> Create(LoteImportacionRequestDto request)
        {
            return Ok(await _service.CreateAsync(request));
        }
    }
}
