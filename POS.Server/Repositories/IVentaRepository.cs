using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public interface IVentaRepository
    {
        Task<Venta> CreateVentaAsync(Venta venta);
        Task<List<Venta>> GetAllVentasAsync();
        Task<Venta?> GetVentaByIdAsync(int id);
    }
}