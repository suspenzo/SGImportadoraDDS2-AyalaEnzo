using POS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class UnidadFisica
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public int? IdLote { get; set; } // Opcional por ahora
        public int? IdDetalleVenta { get; set; } // Nulo si aún no se ha vendido
        public string ImeiSerial { get; set; } = string.Empty;
        public ItemStatus EstadoUnidad { get; set; } = ItemStatus.Disponible;

        // Propiedades de navegación
        public Product? Producto { get; set; }
        public DetalleVenta? DetalleVenta { get; set; }
    }
}