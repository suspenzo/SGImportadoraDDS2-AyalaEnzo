using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class Proveedor
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;

        // Navegación
        public ICollection<LoteImportacion> Lotes { get; set; } = new List<LoteImportacion>();
    }
}