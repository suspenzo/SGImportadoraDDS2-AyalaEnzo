using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.DTOs
{
    public class ProductoInventarioDto
    {
        public int ProductID { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public int StockDisponible { get; set; }
        public List<UnidadFisicaDto> Unidades { get; set; } = new();
    }
}