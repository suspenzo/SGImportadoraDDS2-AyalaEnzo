using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public class LoteImportacionRepository : ILoteImportacionRepository
    {
        private readonly AppDbContext _context;

        public LoteImportacionRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<LoteImportacion>> GetAllAsync()
        {
            // Incluimos el Proveedor para poder mostrar su nombre en el Frontend
            return await _context.LotesImportacion
                .Include(l => l.Proveedor)
                .ToListAsync();
        }

        public async Task<LoteImportacion?> GetByIdAsync(int id)
        {
            return await _context.LotesImportacion
                .Include(l => l.Proveedor)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<LoteImportacion> CreateAsync(LoteImportacion lote)
        {
            _context.LotesImportacion.Add(lote);
            await _context.SaveChangesAsync();

            // Volvemos a buscar el lote para traer la entidad Proveedor completa
            return await GetByIdAsync(lote.Id) ?? lote;
        }

        public async Task<LoteImportacion> UpdateAsync(LoteImportacion lote)
        {
            _context.LotesImportacion.Update(lote);
            await _context.SaveChangesAsync();
            return lote;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var lote = await _context.LotesImportacion.FindAsync(id);
            if (lote == null) return false;

            _context.LotesImportacion.Remove(lote);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}