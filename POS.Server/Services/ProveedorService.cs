using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;

namespace POS.Server.Services
{
    public class ProveedorService : IProveedorService
    {
        private readonly IProveedorRepository _repository;
        public ProveedorService(IProveedorRepository repository) => _repository = repository;

        public async Task<List<ProveedorResponseDto>> GetAllAsync()
        {
            var provs = await _repository.GetAllAsync();
            return provs.Select(p => new ProveedorResponseDto
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Nit = p.Nit,
                PaisOrigen = p.PaisOrigen,
                Telefono = p.Telefono
            }).ToList();
        }

        public async Task<ProveedorResponseDto> CreateAsync(ProveedorRequestDto request)
        {
            var newProv = new Proveedor
            {
                Nombre = request.Nombre,
                Nit = request.Nit,
                PaisOrigen = request.PaisOrigen,
                Telefono = request.Telefono
            };
            var created = await _repository.CreateAsync(newProv);
            return new ProveedorResponseDto { Id = created.Id, Nombre = created.Nombre, Nit = created.Nit };
        }
    }
}