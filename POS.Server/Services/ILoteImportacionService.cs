using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public interface ILoteImportacionService
    {
        Task<List<LoteImportacionResponseDto>> GetAllAsync();
        Task<LoteImportacionResponseDto?> GetByIdAsync(int id);
        Task<LoteImportacionResponseDto> CreateAsync(LoteImportacionRequestDto request);
        Task<LoteImportacionResponseDto?> UpdateAsync(int id, LoteImportacionRequestDto request);

        Task<bool> DeleteAsync(int id);
    }
}