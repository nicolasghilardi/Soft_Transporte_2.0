using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_VIAJES
    {
        private CD_VIAJES objcd_Viaje = new CD_VIAJES();

        public List<VIAJES> Listar()
        {
            return objcd_Viaje.Listar();
        }
        public int ObtenerCorrelativo()
        {
            return objcd_Viaje.ObtenerCorrelativo();
        }
        public int Registrar(VIAJES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oCamion.patente == "")
            {
                Mensaje += "Es necesario seleccionar un camion\n";
            }
            if (obj.oChofer.nombreChofer == "")
            {
                Mensaje += "Es necesario el nombre del Chofer\n";
            }
            if (obj.oCliente.nombreCliente == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Viaje.Registrar(obj, out Mensaje);
            }

        }
        public bool Editar(VIAJES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.oCamion.patente == "")
            {
                Mensaje += "Es necesario seleccionar un camion\n";
            }
            if (obj.oChofer.nombreChofer == "")
            {
                Mensaje += "Es necesario el nombre del Chofer\n";
            }
            if (obj.oCliente.nombreCliente == "")
            {
                Mensaje += "Es necesario el nombre del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Viaje.Editar(obj, out Mensaje);
            }
            
        }
        public bool Eliminar(VIAJES obj, out string Mensaje)
        {
            return objcd_Viaje.Eliminar(obj, out Mensaje);
        }
    }
}
