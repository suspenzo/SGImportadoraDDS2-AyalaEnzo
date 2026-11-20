using POS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class Venta
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public int IdUsuario { get; set; } 
        public DateTime Fecha { get; set; } = DateTime.Now;
        public decimal Total { get; set; }
        public MetodoPago MetodoPago { get; set; }

        // Propiedades de navegación (Para Entity Framework)
        public Client? Cliente { get; set; }
        public User? Usuario { get; set; }
        public ICollection<DetalleVenta> Detalles { get; set; } = new List<DetalleVenta>();
    }
}