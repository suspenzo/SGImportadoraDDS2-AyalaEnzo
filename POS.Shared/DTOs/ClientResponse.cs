using POS.Shared.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.DTOs
{
    public class ClientResponse
    {
        public int Id { get; set; }
        public string NombreCompleto { get; set; } = string.Empty; // Lo unimos para mostrarlo bonito
        public string NumDocumento { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefono { get; set; } = string.Empty;
        public Status Estado { get; set; }
    }
}
