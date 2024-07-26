using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_MANTENIMIENTO
    {
        private CD_MANTENIMIENTO objcd_mantenimiento = new CD_MANTENIMIENTO();

        public List<MANTENIMIENTO> Listar()
        {
            return objcd_mantenimiento.Listar();
        }

        public int Registrar(MANTENIMIENTO obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.fecha == "")
            {
                Mensaje += "Es necesario la fecha del mantenimiento\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_mantenimiento.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(MANTENIMIENTO obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.fecha == "")
            {
                Mensaje += "Es necesario la fecha del mantenimiento\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_mantenimiento.Editar(obj, out Mensaje);
            }
        }
    }
}
