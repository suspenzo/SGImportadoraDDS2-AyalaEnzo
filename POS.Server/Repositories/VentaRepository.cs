using Microsoft.EntityFrameworkCore;
using POS.Server.Data;
using POS.Shared.Entities;
using POS.Shared.Enums;

namespace POS.Server.Repositories
{
    public class VentaRepository : IVentaRepository
    {
        private readonly AppDbContext _context;

        public VentaRepository(AppDbContext context) => _context = context;

        public async Task<Venta> CreateVentaAsync(Venta venta)
        {
            using var transaction = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. Guardar la cabecera de la venta
                _context.Ventas.Add(venta);
                await _context.SaveChangesAsync();

                // 2. Actualizar el estado de las unidades físicas a "Vendido"
                foreach (var detalle in venta.Detalles)
                {
                    foreach (var idUnidad in detalle.UnidadesFisicas.Select(u => u.Id))
                    {
                        var unidad = await _context.UnidadesFisicas.FindAsync(idUnidad);
                        if (unidad != null)
                        {
                            unidad.EstadoUnidad = EstadoUnidad.Vendido;
                            unidad.IdDetalleVenta = detalle.Id; // Vinculamos el IMEI al detalle
                        }
                    }
                }

                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return venta;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<List<Venta>> GetAllVentasAsync() =>
            await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles)
                .ThenInclude(d => d.Producto)
                .ToListAsync();

        public async Task<Venta?> GetVentaByIdAsync(int id) =>
            await _context.Ventas
                .Include(v => v.Cliente)
                .Include(v => v.Detalles).ThenInclude(d => d.Producto)
                .Include(v => v.Detalles).ThenInclude(d => d.UnidadesFisicas)
                .FirstOrDefaultAsync(v => v.Id == id);
    }
}