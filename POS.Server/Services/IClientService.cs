using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public interface IClientService
    {
        Task<List<ClientResponse>> GetAllAsync();
        Task<ClientResponse?> GetByIdAsync(int id);
        Task<ClientResponse?> CreateAsync(ClientRequest request);
        Task<ClientResponse?> UpdateAsync(int id, ClientRequest request);
        Task<bool> DeleteAsync(int id);
    }
}