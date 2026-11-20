using POS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Sku { get; set; } = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;
        public Category? Categoria { get; set; }
        public int CategoryID { get; set; }
        public Marca? Marca { get; set; }
        public int MarcaID { get; set; }
        public string Modelo { get; set; } = string.Empty;
        public decimal PrecioVenta { get; set; }
        public Status Estado { get; set; } = Status.Activo;
    }
}
