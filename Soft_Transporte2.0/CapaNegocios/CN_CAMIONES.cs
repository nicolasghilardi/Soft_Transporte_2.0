using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_CAMIONES
    {
        private CD_CAMIONES objcd_Camiones = new CD_CAMIONES();

        public List<CAMIONES> Listar()
        {
            return objcd_Camiones.Listar();
        }

        public int Registrar(CAMIONES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.patente == "")
            {
                Mensaje += "Es necesario la patente del camion\n";
            }
            if (obj.modelo == "")
            {
                Mensaje += "Es necesario el modelo del camion\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Camiones.Registrar(obj, out Mensaje);
            }

        }
        public bool Editar(CAMIONES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.patente == "")
            {
                Mensaje += "Es necesario la patente del camion\n";
            }
            if (obj.modelo == "")
            {
                Mensaje += "Es necesario el modelo del camion\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Camiones.Editar(obj, out Mensaje);
            }

        }
        public bool Eliminar(CAMIONES obj, out string Mensaje)
        {
            return objcd_Camiones.Eliminar(obj, out Mensaje);
        }
    }
}
