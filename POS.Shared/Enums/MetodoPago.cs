using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Shared.Enums
{
    public enum MetodoPago
    {
        Efectivo = 1,
        QR = 2,
        TarjetaCredito = 3,
        TarjetaDebito = 4,
        Transferencia = 5
    }
}