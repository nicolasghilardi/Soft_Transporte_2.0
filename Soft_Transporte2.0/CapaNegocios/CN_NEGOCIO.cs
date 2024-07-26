using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_NEGOCIO
    {
        private CD_NEGOCIO objcd_Negocio = new CD_NEGOCIO();

        public Negocio ObtenerDatos()
        {
            return objcd_Negocio.ObtenerDatos();
        }

        public bool GuardarDatos(Negocio obj, out string mensaje)
        {
            mensaje = string.Empty;

            if (obj.Nombre == "")
            {
                mensaje += "Es necesario el Nombre del negocio\n";
            }
            if (obj.CUIT == "")
            {
                mensaje += "Es necesario el numero de CUIT\n";
            }
            if (obj.Direccion == "")
            {
                mensaje += "Es necesario la direccion\n";
            }
            if (mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Negocio.GuardarDatos(obj, out mensaje);
            }

        }
        public byte[] ObtenerLogo(out bool obtenido)
        {
            return objcd_Negocio.ObtenerLogo(out obtenido);
        }

        public bool ActualizarLogo(byte[] imagen, out string mensaje)
        {
            return objcd_Negocio.ActualizarLogo(imagen, out mensaje);
        }
    }
}
