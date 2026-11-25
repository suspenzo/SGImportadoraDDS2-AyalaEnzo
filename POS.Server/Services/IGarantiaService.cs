using POS.Shared.DTOs;
using POS.Shared.Enums;

namespace POS.Server.Services
{
    public interface IGarantiaService
    {
        Task<List<GarantiaResponseDto>> GetAllAsync();
        Task<GarantiaResponseDto?> CreateAsync(GarantiaRequestDto request);
        Task<bool> UpdateEstadoAsync(int id, EstadoGarantia nuevoEstado);
    }
}