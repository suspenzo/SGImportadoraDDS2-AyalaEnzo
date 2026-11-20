using POS.Shared.DTOs;
namespace POS.Server.Services
{
    public interface IMarcaService
    {
        Task<List<MarcaResponse>> GetAllAsync();
        Task<MarcaResponse?> CreateAsync(MarcaRequest request);
        Task<MarcaResponse?> UpdateAsync(int id, MarcaRequest request);
        Task<bool> DeleteAsync(int id);
    }
}