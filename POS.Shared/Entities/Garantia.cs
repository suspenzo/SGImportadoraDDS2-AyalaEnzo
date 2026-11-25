using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POS.Shared.Enums;

namespace POS.Shared.Entities
{
    public class Garantia
    {
        public int Id { get; set; }
        public int IdUnidad { get; set; }
        public DateTime FechaInicio { get; set; } = DateTime.Now;
        public DateTime FechaFin { get; set; }
        public EstadoGarantia EstadoGarantia { get; set; } = EstadoGarantia.Vigente;

        // Navegación
        public UnidadFisica? UnidadFisica { get; set; }
    }
}