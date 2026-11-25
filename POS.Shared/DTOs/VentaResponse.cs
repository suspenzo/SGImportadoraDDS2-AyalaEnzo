using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.DTOs
{
    public class VentaResponse
    {
        public int IdVenta { get; set; }
        public DateTime Fecha { get; set; }
        public string ClienteNombre { get; set; } = string.Empty;
        public string ClienteDocumento { get; set; } = string.Empty;
        public decimal Total { get; set; }
        public string MetodoPago { get; set; } = string.Empty;
        public List<DetalleVentaResponse> Detalles { get; set; } = new();
    }

    public class DetalleVentaResponse
    {
        public string ProductoNombre { get; set; } = string.Empty;
        public decimal PrecioUnitario { get; set; }
        public List<string> Seriales { get; set; } = new();
        public string GarantiaMeses { get; set; } = "12 Meses"; // Garantía estándar
    }
}
