using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaEntidades
{
    public class MANTENIMIENTO
    {
        public int idMantenimiento { get; set; }
        public CAMIONES oCamion { get; set; }
        public string fecha { get; set; }
        public string tipoMantenimiento { get; set; }
        public string descripcion { get; set; }
        public string kilometrajes { get; set; }
        public string proxMantenimiento { get; set; }
        public decimal costo { get; set; }
        public string partesReemplazadas { get; set; }
        public string comentarios { get; set; }
        public string estado { get; set; }
        public string FechaRegistro { get; set; }
    }
}
