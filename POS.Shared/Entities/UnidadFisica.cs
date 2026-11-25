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
        public int? IdLote { get; set; }
        public int? IdDetalleVenta { get; set; }
        public string ImeiSerial { get; set; } = string.Empty;
        public EstadoUnidad EstadoUnidad { get; set; } = EstadoUnidad.Disponible;

        // Navegación
        public Product? Producto { get; set; }
        public DetalleVenta? DetalleVenta { get; set; }
        public LoteImportacion? Lote { get; set; } // 
        public Garantia? Garantia { get; set; }    //
    }
}