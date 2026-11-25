using POS.Server.Repositories;
using POS.Shared.DTOs;

namespace POS.Server.Services
{
    public class UnidadFisicaService : IUnidadFisicaService
    {
        private readonly IUnidadFisicaRepository _repository;
        public UnidadFisicaService(IUnidadFisicaRepository repository) => _repository = repository;

        public async Task<List<UnidadFisicaDto>> GetDisponiblesByProductoIdAsync(int idProducto)
        {
            var unidades = await _repository.GetDisponiblesByProductoIdAsync(idProducto);

            // Mapeamos a DTO
            return unidades.Select(u => new UnidadFisicaDto
            {
                Id = u.Id,
                ImeiSerial = u.ImeiSerial
            }).ToList();
        }
    }
}