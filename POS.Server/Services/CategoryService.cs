using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;

namespace POS.Server.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository) => _repository = repository;

        public async Task<List<CategoryResponse>> GetAllAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Select(MapToResponseDto).ToList();
        }

        public async Task<CategoryResponse?> GetByIdAsync(int id)
        {
            var category = await _repository.GetByIdAsync(id);
            return category == null ? null : MapToResponseDto(category);
        }

        public async Task<CategoryResponse?> CreateAsync(CategoryRequest request)
        {
            // Validar que no exista una categoría con el mismo nombre
            var existingCategory = await _repository.GetByNameAsync(request.Nombre);
            if (existingCategory != null) return null;

            var newCategory = new Category
            {
                Nombre = request.Nombre,
                Descripcion = request.Descripcion
            };

            var created = await _repository.CreateAsync(newCategory);
            return MapToResponseDto(created);
        }

        public async Task<CategoryResponse?> UpdateAsync(int id, CategoryRequest request)
        {
            var category = await _repository.GetByIdAsync(id);
            if (category == null) return null;

            category.Nombre = request.Nombre;
            category.Descripcion = request.Descripcion;

            var updated = await _repository.UpdateAsync(category);
            return MapToResponseDto(updated);
        }

        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        private static CategoryResponse MapToResponseDto(Category category)
        {
            return new CategoryResponse
            {
                CategoryID = category.CategoryID,
                Nombre = category.Nombre,
                Descripcion = category.Descripcion
            };
        }
    }
}