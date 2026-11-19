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
        public int productID { get; set; }
        public string sku { get; set; } = string.Empty;
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public Category categoria { get; set; } = new Category(); //revisar = new Category();
        public int id_categoria { get; set; }
        public int marca { get; set; }
        public string modelo { get; set; } = string.Empty;
        public decimal precio_venta { get; set; }
    }
}
