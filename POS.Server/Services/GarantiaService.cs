using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;
using POS.Shared.Enums;

namespace POS.Server.Services
{
    public class GarantiaService : IGarantiaService
    {
        private readonly IGarantiaRepository _repository;

        public GarantiaService(IGarantiaRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<GarantiaResponseDto>> GetAllAsync()
        {
            var garantias = await _repository.GetAllAsync();
            return garantias.Select(MapToResponseDto).ToList();
        }

        public async Task<GarantiaResponseDto?> CreateAsync(GarantiaRequestDto request)
        {
            // 1. Validar que la unidad no tenga ya una garantía activa
            var existeGarantia = await _repository.GetByUnidadIdAsync(request.IdUnidad);
            if (existeGarantia != null) return null; // Ya tiene garantía

            // 2. Crear la nueva garantía calculando la fecha de fin
            var nuevaGarantia = new Garantia
            {
                IdUnidad = request.IdUnidad,
                FechaInicio = DateTime.Now,
                FechaFin = DateTime.Now.AddMonths(request.MesesGarantia),
                EstadoGarantia = EstadoGarantia.Vigente
            };

            var created = await _repository.CreateAsync(nuevaGarantia);
            return MapToResponseDto(created);
        }

        public async Task<bool> UpdateEstadoAsync(int id, EstadoGarantia nuevoEstado)
        {
            var garantia = await _repository.GetByIdAsync(id);
            if (garantia == null) return false;

            garantia.EstadoGarantia = nuevoEstado;
            await _repository.UpdateAsync(garantia);
            return true;
        }

        private static GarantiaResponseDto MapToResponseDto(Garantia garantia)
        {
            return new GarantiaResponseDto
            {
                Id = garantia.Id,
                IdUnidad = garantia.IdUnidad,
                ImeiSerial = garantia.UnidadFisica?.ImeiSerial ?? "Sin IMEI",
                FechaInicio = garantia.FechaInicio,
                FechaFin = garantia.FechaFin,
                EstadoGarantia = garantia.EstadoGarantia
            };
        }
    }
}