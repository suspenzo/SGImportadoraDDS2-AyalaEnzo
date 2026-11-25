using POS.Shared.Entities;

namespace POS.Server.Repositories
{
    public interface IUnidadFisicaRepository
    {
        Task<List<UnidadFisica>> GetDisponiblesByProductoIdAsync(int idProducto);
        Task<UnidadFisica?> GetByIdAsync(int id);
        Task UpdateAsync(UnidadFisica unidad);
    }
}