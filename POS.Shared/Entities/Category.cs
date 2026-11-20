using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class Category
    {
        public int CategoryID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Descripcion { get; set; } = string.Empty;

        // Propiedad de navegación (opcional, pero útil para relaciones)
        public ICollection<Product> Productos { get; set; } = new List<Product>();
    }
}