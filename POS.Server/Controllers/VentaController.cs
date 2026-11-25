using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Server.Services;
using POS.Shared.DTOs;
using System.Security.Claims;
using POS.Server.Data;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Solo vendedores/admins logueados pueden vender
    public class VentaController : ControllerBase
    {
        private readonly IVentaService _ventaService;
        private readonly AppDbContext _context;

        public VentaController(IVentaService ventaService, AppDbContext context)
        {
            _ventaService = ventaService;
            _context = context;
        }

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
        [HttpGet("{id}")]
        public async Task<ActionResult<VentaResponse>> GetComprobante(int id)
        {
            // Necesitas inyectar AppDbContext en este controlador para esta consulta
            var venta = await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles).ThenInclude(d => d.Producto)
                .Include(v => v.Detalles).ThenInclude(d => d.UnidadesFisicas)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (venta == null) return NotFound("Venta no encontrada");

            var response = new VentaResponse
            {
                IdVenta = venta.Id,
                Fecha = venta.Fecha,
                ClienteNombre = $"{venta.Cliente?.Nombre} {venta.Cliente?.APaterno}",
                ClienteDocumento = venta.Cliente?.NumDocumento ?? "",
                Total = venta.Total,
                MetodoPago = venta.MetodoPago.ToString(),
                Detalles = venta.Detalles.Select(d => new DetalleVentaResponse
                {
                    ProductoNombre = d.Producto != null ? d.Producto.Nombre : "Producto no encontrado",
                    PrecioUnitario = d.PrecioUnitario,
                    Seriales = d.UnidadesFisicas.Select(u => u.ImeiSerial).ToList(),
                    GarantiaMeses = "12 Meses" // Si tienes lógica de garantías, aquí la consultas
                }).ToList()
            };

            return Ok(response);
        }
    }
}