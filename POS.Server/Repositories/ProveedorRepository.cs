using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public class ProveedorRepository : IProveedorRepository
    {
        private readonly AppDbContext _context;
        public ProveedorRepository(AppDbContext context) => _context = context;

        public async Task<List<Proveedor>> GetAllAsync() => await _context.Proveedores.ToListAsync();
        public async Task<Proveedor?> GetByIdAsync(int id) => await _context.Proveedores.FindAsync(id);

        public async Task<Proveedor> CreateAsync(Proveedor proveedor)
        {
            _context.Proveedores.Add(proveedor);
            await _context.SaveChangesAsync();
            return proveedor;
        }
    }
}
