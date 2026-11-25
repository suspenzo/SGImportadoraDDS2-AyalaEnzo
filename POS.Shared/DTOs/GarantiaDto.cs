using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Shared.Enums;
namespace POS.Shared.DTOs
{
    public class GarantiaRequestDto
    {
        public int IdUnidad { get; set; }
        public int MesesGarantia { get; set; } 
    }

    public class GarantiaResponseDto
    {
        public int Id { get; set; }
        public int IdUnidad { get; set; }
        public string ImeiSerial { get; set; } = string.Empty;
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public EstadoGarantia EstadoGarantia { get; set; }
    }
}