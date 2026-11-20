using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;

namespace POS.Server.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;

        public ClientService(IClientRepository repository) => _repository = repository;

        public async Task<List<ClientResponse>> GetAllAsync()
        {
            var clients = await _repository.GetAllAsync();
            return clients.Select(MapToResponseDto).ToList();
        }

        public async Task<ClientResponse?> GetByIdAsync(int id)
        {
            var client = await _repository.GetByIdAsync(id);
            return client == null ? null : MapToResponseDto(client);
        }

        public async Task<ClientResponse?> CreateAsync(ClientRequest request)
        {
            // Validar que el documento no exista
            var existingClient = await _repository.GetByDocumentAsync(request.NumDocumento);
            if (existingClient != null) return null; // Ya existe

            var newClient = new Client
            {
                Nombre = request.Nombre,
                APaterno = request.APaterno,
                AMaterno = request.AMaterno,
                NumDocumento = request.NumDocumento,
                Email = request.Email,
                Telefono = request.Telefono,
                Estado = request.Estado
            };

            var created = await _repository.CreateAsync(newClient);
            return MapToResponseDto(created);
        }

        public async Task<ClientResponse?> UpdateAsync(int id, ClientRequest request)
        {
            var client = await _repository.GetByIdAsync(id);
            if (client == null) return null;

            client.Nombre = request.Nombre;
            client.APaterno = request.APaterno;
            client.AMaterno = request.AMaterno;
            client.NumDocumento = request.NumDocumento;
            client.Email = request.Email;
            client.Telefono = request.Telefono;
            client.Estado = request.Estado;

            var updated = await _repository.UpdateAsync(client);
            return MapToResponseDto(updated);
        }

        public async Task<bool> DeleteAsync(int id) => await _repository.DeleteAsync(id);

        // Método auxiliar para mapear Entidad a DTO
        private static ClientResponse MapToResponseDto(Client client)
        {
            return new ClientResponse
            {
                Id = client.Id,
                NombreCompleto = $"{client.Nombre} {client.APaterno} {client.AMaterno}".Trim(),
                NumDocumento = client.NumDocumento,
                Email = client.Email,
                Telefono = client.Telefono,
                Estado = client.Estado
            };
        }
    }
}