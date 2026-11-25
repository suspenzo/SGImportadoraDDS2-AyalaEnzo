using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace POS.Shared.DTOs
{
    public class ProveedorRequestDto
    {
        [Required] public string Nombre { get; set; } = string.Empty;
        [Required] public string Nit { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }

    public class ProveedorResponseDto
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Nit { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
    }
}