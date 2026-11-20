using POS.Server.Repositories;
using POS.Shared.DTOs;
using POS.Shared.Entities;
using Microsoft.EntityFrameworkCore;
using POS.Server.Data;

namespace POS.Server.Services
{
    public class VentaService : IVentaService
    {
        private readonly IVentaRepository _repository;
        private readonly IProductRepository _productRepository;
        private readonly AppDbContext _context;

        public VentaService(IVentaRepository repository, IProductRepository productRepository, AppDbContext context)
        {
            _repository = repository;
            _productRepository = productRepository;
            _context = context;
        }


        public async Task<int> RegistrarVentaAsync(VentaRequest request, int idUsuarioLogueado)
        {
            var nuevaVenta = new Venta
            {
                IdCliente = request.IdCliente,
                IdUsuario = idUsuarioLogueado,
                Fecha = DateTime.Now,
                MetodoPago = request.MetodoPago,
                Total = 0 // Lo calcularemos en el bucle
            };

            foreach (var item in request.Detalles)
            {
                // 1. Validar que se envió al menos un equipo físico
                if (item.IdsUnidadesFisicas == null || item.IdsUnidadesFisicas.Count == 0)
                    throw new Exception("Debe seleccionar al menos un equipo físico (IMEI/Serial) para vender.");

                // 2. Obtener el producto real de la Base de Datos para sacar el precio
                var producto = await _productRepository.GetByIdAsync(item.IdProducto);
                if (producto == null) throw new Exception("Producto no encontrado.");

                var detalle = new DetalleVenta
                {
                    IdProducto = item.IdProducto,
                    Cantidad = item.IdsUnidadesFisicas.Count,
                    PrecioUnitario = producto.PrecioVenta
                };

                // 3. Asignar las unidades físicas a este detalle
                foreach (var idUnidad in item.IdsUnidadesFisicas)
                {
                    var unidadExistente = new UnidadFisica { Id = idUnidad };
                    _context.UnidadesFisicas.Attach(unidadExistente);

                    detalle.UnidadesFisicas.Add(unidadExistente);
                }

                // 4. Sumar al total de la venta (Cantidad * PrecioRealBD)
                nuevaVenta.Total += (detalle.Cantidad * detalle.PrecioUnitario);
                nuevaVenta.Detalles.Add(detalle);
            }

            var ventaGuardada = await _repository.CreateVentaAsync(nuevaVenta);
            return ventaGuardada.Id;
        }
    }
}