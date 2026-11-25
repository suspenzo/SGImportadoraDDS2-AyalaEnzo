using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class LoteImportacion
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public DateTime FechaLlegada { get; set; } = DateTime.Now;
        public decimal CostosFlete { get; set; }

        // Navegación
        public Proveedor? Proveedor { get; set; }
        public ICollection<UnidadFisica> Unidades { get; set; } = new List<UnidadFisica>();
    }
}