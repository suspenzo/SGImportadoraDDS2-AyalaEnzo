using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public interface ILoteImportacionRepository
    {
        Task<List<LoteImportacion>> GetAllAsync();
        Task<LoteImportacion?> GetByIdAsync(int id);
        Task<LoteImportacion> CreateAsync(LoteImportacion lote);
        Task<LoteImportacion> UpdateAsync(LoteImportacion lote);
        Task<bool> DeleteAsync(int id);
    }
}