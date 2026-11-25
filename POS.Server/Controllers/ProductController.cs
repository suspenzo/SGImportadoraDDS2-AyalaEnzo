using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;


namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere Token para acceder
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUnidadFisicaService _unidadFisicaService;

        public ProductController(IProductService productService, IUnidadFisicaService unidadFisicaService)
        {
            _productService = productService;
            _unidadFisicaService = unidadFisicaService; 
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
    }
}