using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;
namespace POS.Server.Services
{
    public class MarcaService : IMarcaService
    {
        private readonly IMarcaRepository _repository;
        public MarcaService(IMarcaRepository repository) => _repository = repository;

        public async Task<List<MarcaResponse>> GetAllAsync()
        {
            var marcas = await _repository.GetAllAsync();
            return marcas.Select(m => new MarcaResponse { MarcaID = m.MarcaID, Nombre = m.Nombre, PaisOrigen = m.PaisOrigen }).ToList();
        }

        public async Task<MarcaResponse?> CreateAsync(MarcaRequest request)
        {
            if (await _repository.GetByNameAsync(request.Nombre) != null) return null; // Ya existe

            var nuevaMarca = new Marca { Nombre = request.Nombre, PaisOrigen = request.PaisOrigen };
            var created = await _repository.CreateAsync(nuevaMarca);
            return new MarcaResponse { MarcaID = created.MarcaID, Nombre = created.Nombre, PaisOrigen = created.PaisOrigen };
        }

        public async Task<MarcaResponse?> UpdateAsync(int id, MarcaRequest request)
        {
            var marca = await _repository.GetByIdAsync(id);
            if (marca == null) return null;

            marca.Nombre = request.Nombre;
            marca.PaisOrigen = request.PaisOrigen;
            var updated = await _repository.UpdateAsync(marca);
            return new MarcaResponse { MarcaID = updated.MarcaID, Nombre = updated.Nombre, PaisOrigen = updated.PaisOrigen };
        }

        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);
    }
}