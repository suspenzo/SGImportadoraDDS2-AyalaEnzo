using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Shared.Enums;

namespace POS.Shared.DTOs
{
    public class ProductResponse
    {
        public int ProductID { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public int CategoryID { get; set; }
        public int MarcaID { get; set; }
        public string NombreCategoria { get; set; } = string.Empty; // Mostramos el nombre, no el ID
        
        public string NombreMarca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public Status Estado { get; set; }
    }
}