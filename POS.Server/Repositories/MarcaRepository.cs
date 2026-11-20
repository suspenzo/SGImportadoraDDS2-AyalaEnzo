using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;
namespace POS.Server.Repositories
{
    public class MarcaRepository : IMarcaRepository
    {
        private readonly AppDbContext _context;
        public MarcaRepository(AppDbContext context) => _context = context;

        public async Task<List<Marca>> GetAllAsync() => await _context.Marcas.ToListAsync();
        public async Task<Marca?> GetByIdAsync(int id) => await _context.Marcas.FindAsync(id);
        public async Task<Marca?> GetByNameAsync(string nombre) => await _context.Marcas.FirstOrDefaultAsync(m => m.Nombre.ToLower() == nombre.ToLower());

        public async Task<Marca> CreateAsync(Marca marca)
        {
            _context.Marcas.Add(marca);
            await _context.SaveChangesAsync();
            return marca;
        }
        public async Task<Marca> UpdateAsync(Marca marca)
        {
            _context.Marcas.Update(marca);
            await _context.SaveChangesAsync();
            return marca;
        }
        public async Task<bool> DeleteAsync(int id)
        {
            var marca = await _context.Marcas.FindAsync(id);
            if (marca == null) return false;
            _context.Marcas.Remove(marca);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}