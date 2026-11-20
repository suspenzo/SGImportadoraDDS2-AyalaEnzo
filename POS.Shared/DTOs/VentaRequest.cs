using POS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Shared.Enums;

namespace POS.Shared.DTOs
{
    public class VentaRequest
    {
        public int IdCliente { get; set; }
        public MetodoPago MetodoPago { get; set; }
        public List<DetalleVentaRequest> Detalles { get; set; } = new List<DetalleVentaRequest>();
    }

    public class DetalleVentaRequest
    {
        // Mantenemos el IdProducto para validar rápido contra las UnidadesFísicas
        public int IdProducto { get; set; }

        // Eliminamos 'Cantidad' porque se deduce de la lista de abajo

        // Eliminamos 'PrecioUnitario' porque el servidor lo sacará de la BD

        // El corazón de la venta de electrónicos: los IMEIs/Seriales exactos
        public List<int> IdsUnidadesFisicas { get; set; } = new List<int>();
    }
}