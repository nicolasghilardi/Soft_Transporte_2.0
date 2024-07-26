using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_CHOFERES
    {
        private CD_CHOFERES objcd_Chofer = new CD_CHOFERES();

        public List<CHOFERES> Listar()
        {
            return objcd_Chofer.Listar();
        }
        public int Registrar(CHOFERES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.nombreChofer == "")
            {
                Mensaje += "Es necesario el nombre del Chofer\n";
            }
            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del Chofer\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Chofer.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(CHOFERES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.nombreChofer == "")
            {
                Mensaje += "Es necesario el nombre del Chofer\n";
            }
            if (obj.documento == "")
            {
                Mensaje += "Es necesario el documento del Chofer\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Chofer.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(CHOFERES obj, out string Mensaje)
        {
            return objcd_Chofer.Eliminar(obj, out Mensaje);
        }
    }
}
