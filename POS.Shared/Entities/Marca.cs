using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class Marca
    {
        public int MarcaID { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty; // Ej: "EE.UU.", "Corea del Sur"

        // Propiedad de navegación
        public ICollection<Product> Productos { get; set; } = new List<Product>();
    }
}