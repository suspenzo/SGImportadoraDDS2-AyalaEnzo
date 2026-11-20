using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Shared.Enums;
using System.ComponentModel.DataAnnotations;

namespace POS.Shared.DTOs
{
    public class ProductRequest
    {
        [Required(ErrorMessage = "El SKU es obligatorio")]
        public string Sku { get; set; } = string.Empty;

        [Required(ErrorMessage = "El nombre es obligatorio")]
        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "Debe seleccionar una categoría")]
        public int CategoryID { get; set; }

        [Required]
        public int MarcaID { get; set; }

        public string Modelo { get; set; } = string.Empty;

        [Range(0.01, double.MaxValue, ErrorMessage = "El precio debe ser mayor a 0")]
        public decimal PrecioVenta { get; set; }

        public Status Estado { get; set; } = Status.Activo;
    }
}