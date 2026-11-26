using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;

namespace POS.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository) => _repository = repository;

        public async Task<List<ProductResponse>> GetAllAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToResponseDto).ToList();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return product == null ? null : MapToResponseDto(product);
        }

        public async Task<ProductResponse?> CreateAsync(ProductRequest request)
        {
            // Validar que el SKU no exista
            var existingProduct = await _repository.GetBySkuAsync(request.Sku);
            if (existingProduct != null) return null; // El SKU ya está en uso

            var newProduct = new Product
            {
                Sku = request.Sku,
                Nombre = request.Nombre,
                Descripcion = request.Descripcion,
                CategoryID = request.CategoryID,
                MarcaID = request.MarcaID,
                Modelo = request.Modelo,
                PrecioVenta = request.PrecioVenta,
                Estado = request.Estado
            };

            var created = await _repository.CreateAsync(newProduct);

            // Volvemos a buscar el producto para traer el nombre de la categoría
            var result = await _repository.GetByIdAsync(created.ProductID);
            return MapToResponseDto(result!);
        }

        public async Task<ProductResponse?> UpdateAsync(int id, ProductRequest request)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            product.Sku = request.Sku;
            product.Nombre = request.Nombre;
            product.Descripcion = request.Descripcion;
            product.CategoryID = request.CategoryID;
            product.MarcaID = request.MarcaID;
            product.Modelo = request.Modelo;
            product.PrecioVenta = request.PrecioVenta;
            product.Estado = request.Estado;

            var updated = await _repository.UpdateAsync(product);

            var result = await _repository.GetByIdAsync(updated.ProductID);
            return MapToResponseDto(result!);
        }

        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        private static ProductResponse MapToResponseDto(Product product)
        {
            return new ProductResponse
            {
                ProductID = product.ProductID,
                Sku = product.Sku,
                Nombre = product.Nombre,
                Descripcion = product.Descripcion,
                NombreCategoria = product.Categoria?.Nombre ?? "Sin Categoría", // Manejo de nulos
                CategoryID = product.CategoryID, 
                MarcaID = product.MarcaID,
                NombreMarca = product.Marca?.Nombre ?? "Sin Marca",
                Modelo = product.Modelo,
                PrecioVenta = product.PrecioVenta,
                Estado = product.Estado
            };
        }
    }
}