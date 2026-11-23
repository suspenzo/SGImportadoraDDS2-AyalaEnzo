using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using POS.Shared.Enums;

namespace POS.Shared.DTOs
{
    public class UserResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        // Enviamos el rol como string para que sea más fácil mostrarlo en la tabla de Blazor
        public string Role { get; set; } = string.Empty;

        // Los campos que agregaste recientemente
        public Status Estado { get; set; }

        public DateTime FechaCreacion { get; set; }
    }
}