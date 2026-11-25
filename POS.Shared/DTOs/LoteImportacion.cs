using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.DTOs
{
    public class LoteImportacionRequestDto
    {
        public int IdProveedor { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public DateTime FechaLlegada { get; set; }
        public decimal CostosFlete { get; set; }
    }

    public class LoteImportacionResponseDto
    {
        public int Id { get; set; }
        public string NumeroLote { get; set; } = string.Empty;
        public string NombreProveedor { get; set; } = string.Empty;
        public DateTime FechaLlegada { get; set; }
        public decimal CostosFlete { get; set; }
    }
}