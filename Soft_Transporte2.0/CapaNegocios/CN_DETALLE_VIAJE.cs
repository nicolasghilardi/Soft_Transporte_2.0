using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_DETALLE_VIAJE
    {
        private CD_DETALLE_VIAJE objcd_DetalleViaje = new CD_DETALLE_VIAJE();

        public List<DETALLE_VIAJE> ListarPorViaje(int idViaje)
        {
            return objcd_DetalleViaje.ListarPorViaje(Convert.ToInt32(idViaje));
        }
        public int Registrar(DETALLE_VIAJE obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.fecha == "")
            {
                Mensaje += "Es necesario la fecha\n";
            }
            if (obj.destino == "")
            {
                Mensaje += "Es necesario el destino del viaje\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_DetalleViaje.Registrar(obj, out Mensaje);
            }

        }

        public bool Editar(DETALLE_VIAJE obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(obj.fecha == "")
            {
                Mensaje += "Es necesario la fecha\n";
            }
            if (obj.destino == "")
            {
                Mensaje += "Es necesario el destino del viaje\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_DetalleViaje.Editar(obj, out Mensaje);
            }
        }


    }
}
