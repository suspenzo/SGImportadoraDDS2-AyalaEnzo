using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public class GarantiaRepository : IGarantiaRepository
    {
        private readonly AppDbContext _context;

        public GarantiaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Garantia>> GetAllAsync()
        {
            // Incluimos la UnidadFisica para poder mostrar el IMEI en el frontend
            return await _context.Garantias
                .Include(g => g.UnidadFisica)
                .ToListAsync();
        }

        public async Task<Garantia?> GetByIdAsync(int id)
        {
            return await _context.Garantias
                .Include(g => g.UnidadFisica)
                .FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<Garantia?> GetByUnidadIdAsync(int idUnidad)
        {
            return await _context.Garantias
                .Include(g => g.UnidadFisica)
                .FirstOrDefaultAsync(g => g.IdUnidad == idUnidad);
        }

        public async Task<Garantia> CreateAsync(Garantia garantia)
        {
            _context.Garantias.Add(garantia);
            await _context.SaveChangesAsync();
            return await GetByIdAsync(garantia.Id) ?? garantia;
        }

        public async Task<Garantia> UpdateAsync(Garantia garantia)
        {
            _context.Garantias.Update(garantia);
            await _context.SaveChangesAsync();
            return garantia;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var garantia = await _context.Garantias.FindAsync(id);
            if (garantia == null) return false;

            _context.Garantias.Remove(garantia);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}