using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere estar logueado para acceder a clientes
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService) => _clientService = clientService;

        [HttpGet]
        public async Task<ActionResult<List<ClientResponse>>> GetAll()
        {
            return Ok(await _clientService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClientResponse>> GetById(int id)
        {
            var client = await _clientService.GetByIdAsync(id);
            if (client == null) return NotFound("Cliente no encontrado");
            return Ok(client);
        }

        [HttpPost]
        public async Task<ActionResult<ClientResponse>> Create(ClientRequest request)
        {
            var created = await _clientService.CreateAsync(request);
            if (created == null) return BadRequest("El número de documento ya está registrado.");

            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClientResponse>> Update(int id, ClientRequest request)
        {
            var updated = await _clientService.UpdateAsync(id, request);
            if (updated == null) return NotFound("Cliente no encontrado");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _clientService.DeleteAsync(id);
            if (!deleted) return NotFound("Cliente no encontrado");
            return NoContent();
        }
    }
}