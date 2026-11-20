using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace POS.Shared.DTOs
{
    public class MarcaRequest
    {
        [Required(ErrorMessage = "El nombre de la marca es obligatorio")]
        public string Nombre { get; set; } = string.Empty;
        public string PaisOrigen { get; set; } = string.Empty;
    }
}