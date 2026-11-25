using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;

namespace POS.Server.Services
{
    public class LoteImportacionService : ILoteImportacionService
    {
        private readonly ILoteImportacionRepository _repository;

        public LoteImportacionService(ILoteImportacionRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<LoteImportacionResponseDto>> GetAllAsync()
        {
            var lotes = await _repository.GetAllAsync();
            return lotes.Select(MapToResponseDto).ToList();
        }

        public async Task<LoteImportacionResponseDto?> GetByIdAsync(int id)
        {
            var lote = await _repository.GetByIdAsync(id);
            return lote == null ? null : MapToResponseDto(lote);
        }

        public async Task<LoteImportacionResponseDto> CreateAsync(LoteImportacionRequestDto request)
        {
            var nuevoLote = new LoteImportacion
            {
                IdProveedor = request.IdProveedor,
                NumeroLote = request.NumeroLote,
                FechaLlegada = request.FechaLlegada,
                CostosFlete = request.CostosFlete
            };

            var created = await _repository.CreateAsync(nuevoLote);

            // Al usar el repositorio, este ya nos devuelve el lote con el proveedor incluido
            return MapToResponseDto(created);
        }

        public async Task<LoteImportacionResponseDto?> UpdateAsync(int id, LoteImportacionRequestDto request)
        {
            var lote = await _repository.GetByIdAsync(id);
            if (lote == null) return null;

            lote.IdProveedor = request.IdProveedor;
            lote.NumeroLote = request.NumeroLote;
            lote.FechaLlegada = request.FechaLlegada;
            lote.CostosFlete = request.CostosFlete;

            var updated = await _repository.UpdateAsync(lote);

            // Volvemos a buscar para obtener el nombre del proveedor actualizado
            var result = await _repository.GetByIdAsync(updated.Id);
            return MapToResponseDto(result!);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        // Método auxiliar para centralizar el mapeo de Entidad a DTO
        private static LoteImportacionResponseDto MapToResponseDto(LoteImportacion lote)
        {
            return new LoteImportacionResponseDto
            {
                Id = lote.Id,
                NumeroLote = lote.NumeroLote,
                // Si el proveedor es nulo (por algún error en BD), mostramos un texto por defecto
                NombreProveedor = lote.Proveedor?.Nombre ?? "Sin Proveedor",
                FechaLlegada = lote.FechaLlegada,
                CostosFlete = lote.CostosFlete
            };
        }
    }
}