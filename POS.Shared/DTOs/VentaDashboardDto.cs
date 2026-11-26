using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.DTOs
{
    public class VentaDashboardDto
    {
        public int IdVenta { get; set; }
        public DateTime FechaVenta { get; set; }
        public string Vendedor { get; set; } = string.Empty;
        public string Cliente { get; set; } = string.Empty;
        public string Producto { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public string ImeiSerial { get; set; } = string.Empty;
        public string TiempoGarantiaRestante { get; set; } = string.Empty; 
        public bool GarantiaVigente { get; set; }
    }
}