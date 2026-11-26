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

        [HttpGet("dashboard")]
        [Authorize(Roles = "Administrador,Vendedor")]
        public async Task<ActionResult<List<VentaDashboardDto>>> GetDashboard()
        {
            // Traemos todas las ventas con sus relaciones
            var ventas = await _context.Ventas
                .Include(v => v.Usuario)
                .Include(v => v.Cliente)
                .Include(v => v.Detalles).ThenInclude(d => d.Producto).ThenInclude(p => p.Categoria)
                .Include(v => v.Detalles).ThenInclude(d => d.UnidadesFisicas)
                .OrderByDescending(v => v.Fecha)
                .ToListAsync();

            var dashboard = new List<VentaDashboardDto>();

            foreach (var venta in ventas)
            {
                foreach (var detalle in venta.Detalles)
                {
                    foreach (var unidad in detalle.UnidadesFisicas)
                    {
                        // 1. Lógica de Garantía por Categoría
                        var categoria = detalle.Producto?.Categoria?.Nombre?.ToLower() ?? "";
                        DateTime fechaVencimiento = venta.Fecha;

                        if (categoria.Contains("laptop"))
                            fechaVencimiento = venta.Fecha.AddMonths(12);
                        else if (categoria.Contains("tablet"))
                            fechaVencimiento = venta.Fecha.AddMonths(6);
                        else if (categoria.Contains("celular") || categoria.Contains("telefono") || categoria.Contains("smartphone"))
                            fechaVencimiento = venta.Fecha.AddMonths(3);
                        else
                            fechaVencimiento = venta.Fecha.AddMonths(3); // Por defecto 3 meses

                        // 2. Calcular tiempo restante exacto (Meses y Días)
                        var tiempoRestante = "Vencida";
                        bool vigente = false;
                        DateTime ahora = DateTime.Now;

                        if (fechaVencimiento > ahora)
                        {
                            vigente = true;

                            // Lógica para calcular años, meses y días exactos
                            int years = fechaVencimiento.Year - ahora.Year;
                            int months = fechaVencimiento.Month - ahora.Month;
                            int days = fechaVencimiento.Day - ahora.Day;

                            // Si los días son negativos, pedimos prestado un mes
                            if (days < 0)
                            {
                                months--;
                                // Obtenemos cuántos días tiene el mes anterior a la fecha de vencimiento
                                DateTime mesAnterior = fechaVencimiento.AddMonths(-1);
                                days += DateTime.DaysInMonth(mesAnterior.Year, mesAnterior.Month);
                            }

                            // Si los meses son negativos, pedimos prestado un año
                            if (months < 0)
                            {
                                years--;
                                months += 12;
                            }

                            // Convertimos los años a meses para el total
                            int totalMeses = (years * 12) + months;

                            // Armamos el texto final (Manejando singular y plural)
                            var partes = new List<string>();
                            if (totalMeses > 0)
                                partes.Add($"{totalMeses} {(totalMeses == 1 ? "mes" : "meses")}");
                            if (days > 0)
                                partes.Add($"{days} {(days == 1 ? "día" : "días")}");

                            if (partes.Count == 0)
                                tiempoRestante = "Menos de 1 día";
                            else
                                tiempoRestante = string.Join(" y ", partes);
                        }

                        // 3. Agregar al Dashboard
                        dashboard.Add(new VentaDashboardDto
                        {
                            IdVenta = venta.Id,
                            FechaVenta = venta.Fecha,
                            Vendedor = venta.Usuario?.Name ?? "Desconocido",
                            Cliente = $"{venta.Cliente?.Nombre} {venta.Cliente?.APaterno}",
                            Producto = detalle.Producto?.Nombre ?? "Desconocido",
                            Categoria = detalle.Producto?.Categoria?.Nombre ?? "Sin Categoría",
                            ImeiSerial = unidad.ImeiSerial,
                            TiempoGarantiaRestante = tiempoRestante,
                            GarantiaVigente = vigente
                        });
                    }
                }
            }

            return Ok(dashboard);
        }
    }
}