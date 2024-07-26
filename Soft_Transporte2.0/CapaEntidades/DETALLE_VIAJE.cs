using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class DETALLE_VIAJE
    {
        public int idDetalleViaje { get; set; }
        public VIAJES oViaje { get; set; }
        public string fecha { get; set; }
        public string destino { get; set; }
        public string vuelta { get; set; }
        public string rtoNumero { get; set; }
        public string micNro { get; set; }
        public string observacion { get; set; }
        public int demoras { get; set; }
        public string FechaRegistro { get; set; }
    }
}
