using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public interface IProductService
    {
        Task<List<ProductResponse>> GetAllAsync();
        Task<ProductResponse?> GetByIdAsync(int id);
        Task<ProductResponse?> CreateAsync(ProductRequest request);
        Task<ProductResponse?> UpdateAsync(int id, ProductRequest request);
        Task<bool> DeleteAsync(int id);
    }
}