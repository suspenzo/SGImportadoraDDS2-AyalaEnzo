using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class DetalleVenta
    {
        public int Id { get; set; }
        public int IdVenta { get; set; }
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Propiedades de navegación
        public Venta? Venta { get; set; }
        public Product? Producto { get; set; }
        public ICollection<UnidadFisica> UnidadesFisicas { get; set; } = new List<UnidadFisica>();
    }
}