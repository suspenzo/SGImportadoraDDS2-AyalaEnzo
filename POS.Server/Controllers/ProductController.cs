using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using POS.Server.Services;
using POS.Shared.DTOs;
using POS.Server.Data;


namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere Token para acceder
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUnidadFisicaService _unidadFisicaService;
        private readonly AppDbContext _context;

        public ProductController(
            IProductService productService,
            IUnidadFisicaService unidadFisicaService,
            AppDbContext context)
            {
                _productService = productService;
                _unidadFisicaService = unidadFisicaService;
                _context = context; 
            }


        [HttpGet]
        public async Task<ActionResult<List<ProductResponse>>> GetAll()
        {
            return Ok(await _productService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductResponse>> GetById(int id)
        {
            var product = await _productService.GetByIdAsync(id);
            if (product == null) return NotFound("Producto no encontrado");
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductResponse>> Create(ProductRequest request)
        {
            var created = await _productService.CreateAsync(request);
            if (created == null) return BadRequest("El SKU ya está registrado.");

            return Ok(created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductResponse>> Update(int id, ProductRequest request)
        {
            var updated = await _productService.UpdateAsync(id, request);
            if (updated == null) return NotFound("Producto no encontrado");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")] // Solo el admin puede borrar productos
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _productService.DeleteAsync(id);
            if (!deleted) return NotFound("Producto no encontrado");
            return NoContent();
        }

        [HttpGet("{id}/unidades-disponibles")]
        [Authorize(Roles = "Administrador,Vendedor")] // Vendedores también pueden ver stock
        public async Task<ActionResult<List<UnidadFisicaDto>>> GetUnidadesDisponibles(int id)
        {
            // Usamos el servicio de UnidadFisica para buscar los IMEIs disponibles
            var unidades = await _unidadFisicaService.GetDisponiblesByProductoIdAsync(id);
            return Ok(unidades);
        }

        [HttpGet("inventario")]
        [Authorize(Roles = "Administrador,Almacenero")]
        public async Task<ActionResult<List<ProductoInventarioDto>>> GetInventario()
        {
            // Buscamos todas las unidades disponibles y las agrupamos por producto
            var unidades = await _context.UnidadesFisicas
                .Include(u => u.Producto)
                .Where(u => u.EstadoUnidad == POS.Shared.Enums.EstadoUnidad.Disponible)
                .ToListAsync();

            var inventario = unidades
                .GroupBy(u => u.IdProducto)
                .Select(g => new ProductoInventarioDto
                {
                    ProductID = g.Key,
                    Nombre = g.First().Producto?.Nombre ?? "Desconocido",
                    Sku = g.First().Producto?.Sku ?? "N/A",
                    StockDisponible = g.Count(),
                    Unidades = g.Select(u => new UnidadFisicaDto { Id = u.Id, ImeiSerial = u.ImeiSerial }).ToList()
                }).ToList();

            return Ok(inventario);
        }
    }
}