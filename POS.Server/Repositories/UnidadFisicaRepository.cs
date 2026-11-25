using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;
using POS.Shared.Enums;

namespace POS.Server.Repositories
{
    public class UnidadFisicaRepository : IUnidadFisicaRepository
    {
        private readonly AppDbContext _context;
        public UnidadFisicaRepository(AppDbContext context) => _context = context;

        public async Task<List<UnidadFisica>> GetDisponiblesByProductoIdAsync(int idProducto)
        {
            return await _context.UnidadesFisicas
                .Where(u => u.IdProducto == idProducto && u.EstadoUnidad == EstadoUnidad.Disponible)
                .ToListAsync();
        }

        public async Task<UnidadFisica?> GetByIdAsync(int id)
        {
            return await _context.UnidadesFisicas.FindAsync(id);
        }

        public async Task UpdateAsync(UnidadFisica unidad)
        {
            _context.UnidadesFisicas.Update(unidad);
            await _context.SaveChangesAsync();
        }
    }
}