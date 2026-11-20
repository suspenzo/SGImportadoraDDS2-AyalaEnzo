using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using POS.Server.Services;
using POS.Shared.DTOs;

namespace POS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize] // Requiere estar logueado
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) => _categoryService = categoryService;

        [HttpGet]
        public async Task<ActionResult<List<CategoryResponse>>> GetAll()
        {
            return Ok(await _categoryService.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryResponse>> GetById(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) return NotFound("Categoría no encontrada");
            return Ok(category);
        }

        [HttpPost]
        [Authorize(Roles = "Administrador")] // Solo el Admin puede crear categorías
        public async Task<ActionResult<CategoryResponse>> Create(CategoryRequest request)
        {
            var created = await _categoryService.CreateAsync(request);
            if (created == null) return BadRequest("Ya existe una categoría con ese nombre.");

            return Ok(created);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult<CategoryResponse>> Update(int id, CategoryRequest request)
        {
            var updated = await _categoryService.UpdateAsync(id, request);
            if (updated == null) return NotFound("Categoría no encontrada");
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _categoryService.DeleteAsync(id);
            if (!deleted) return NotFound("Categoría no encontrada");
            return NoContent();
        }
    }
}