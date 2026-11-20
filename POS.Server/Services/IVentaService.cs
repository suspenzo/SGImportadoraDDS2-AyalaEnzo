using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public interface IVentaService
    {
        Task<int> RegistrarVentaAsync(VentaRequest request, int idUsuarioLogueado);
    }
}