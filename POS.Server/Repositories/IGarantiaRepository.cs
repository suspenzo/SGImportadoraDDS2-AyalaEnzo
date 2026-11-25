using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public interface IGarantiaRepository
    {
        Task<List<Garantia>> GetAllAsync();
        Task<Garantia?> GetByIdAsync(int id);
        Task<Garantia?> GetByUnidadIdAsync(int idUnidad);
        Task<Garantia> CreateAsync(Garantia garantia);
        Task<Garantia> UpdateAsync(Garantia garantia);
        Task<bool> DeleteAsync(int id);
    }
}