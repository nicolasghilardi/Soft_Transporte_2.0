using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_CLIENTE
    {
        private CD_CLIENTE objcd_Cliente = new CD_CLIENTE();

        public List<CLIENTES> Listar()
        {
            return objcd_Cliente.Listar();
        }

        public int Registrar(CLIENTES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if(obj.nombreCliente == "")
            {
                Mensaje += "Es necesarios el nombre del Cliente\n";
            }
            if(Mensaje != string.Empty)
            {
                return 0;
            }
            else
            {
                return objcd_Cliente.Registrar(obj, out Mensaje);
            }
        }

        public bool Editar(CLIENTES obj, out string Mensaje)
        {
            Mensaje = string.Empty;

            if (obj.nombreCliente == "")
            {
                Mensaje += "Es necesarios el nombre del Cliente\n";
            }
            if (Mensaje != string.Empty)
            {
                return false;
            }
            else
            {
                return objcd_Cliente.Editar(obj, out Mensaje);
            }
        }

        public bool Eliminar(CLIENTES obj, out string Mensaje)
        {
            return objcd_Cliente.Eliminar(obj, out Mensaje);
        }
    }
}
