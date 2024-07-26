using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class VIAJES
    {
        public int idViaje { get; set; }
        public string nroRendicion { get; set; }
        public CAMIONES oCamion  { get; set; }
        public CHOFERES oChofer { get; set; }
        public CLIENTES oCliente { get; set; }
        public decimal argEntregado { get; set; }
        public decimal chilEntregado { get; set; }
        public decimal gastosArg { get; set; }
        public decimal gastosChil { get; set; }
        public decimal kmSalida { get; set; }
        public decimal kmVuelta { get; set; }
        public decimal kmRecorrido { get; set; }
        public decimal combLts { get; set; }
        public decimal consumo { get; set; }
        public decimal restoArg { get; set; }
        public decimal restoChil { get; set; }
        public string FechaRegistro { get; set; }
    }
}
