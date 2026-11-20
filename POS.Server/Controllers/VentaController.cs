using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;
using System.Security.Claims;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Solo vendedores/admins logueados pueden vender
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;

        public VentaController(IVentaService ventaService) => _ventaService = ventaService;

        [HttpPost]
        public async Task<IActionResult> RegistrarVenta(VentaRequest request)
        {
            // Obtenemos el ID del usuario logueado desde el Token JWT
            var idUsuarioClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (idUsuarioClaim == null) return Unauthorized();

            int idUsuario = int.Parse(idUsuarioClaim);

            try
            {
                var idVenta = await _ventaService.RegistrarVentaAsync(request, idUsuario);
                return Ok(new { Mensaje = "Venta registrada con éxito", IdVenta = idVenta });
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al registrar venta: {ex.Message}");
            }
        }
    }
}