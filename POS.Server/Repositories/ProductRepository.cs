using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<List<Product>> GetAllAsync() =>
            await _context.Products.Include(p => p.Categoria)
                                    .Include(p => p.Marca)
                                    .ToListAsync(); // Incluimos la categoría

        public async Task<Product?> GetByIdAsync(int id) =>
            await _context.Products.Include(p => p.Categoria)
                                    .Include(p => p.Marca)
                                    .FirstOrDefaultAsync(p => p.ProductID == id);

        public async Task<Product?> GetBySkuAsync(string sku) =>
            await _context.Products.FirstOrDefaultAsync(p => p.Sku == sku);

        public async Task<Product> CreateAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null) return false;

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}