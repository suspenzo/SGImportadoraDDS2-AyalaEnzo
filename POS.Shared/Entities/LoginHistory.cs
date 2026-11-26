using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Entities
{
    public class LoginHistory
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

        public DateTime LoginTime { get; set; } = DateTime.Now;
        public DateTime? LogoutTime { get; set; } // Nulo hasta que cierre sesión
    }
}