using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public interface IUnidadFisicaService
    {
        Task<List<UnidadFisicaDto>> GetDisponiblesByProductoIdAsync(int idProducto);
    }
}