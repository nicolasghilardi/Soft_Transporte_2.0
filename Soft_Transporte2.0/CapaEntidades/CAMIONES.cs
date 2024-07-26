using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class CAMIONES
    {
        public int idCamion { get; set; }
        public string modelo { get; set; }
        public string patente { get; set; }
        public decimal capacidad { get; set; }
        public string vencTecnica { get; set; }
        public string vencRuta { get; set; }
        public string vencSeguro { get; set; }
        public string vencTazaFiscal { get; set; }
        public string cambioAceite { get; set; }
        public string FechaRegistro { get; set; }
    }
}
