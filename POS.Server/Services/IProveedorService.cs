using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;

namespace POS.Server.Services
{
    public interface IProveedorService
    {
        Task<List<ProveedorResponseDto>> GetAllAsync();
        Task<ProveedorResponseDto> CreateAsync(ProveedorRequestDto request);
    }
}