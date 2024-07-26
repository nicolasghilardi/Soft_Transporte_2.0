using CapaDatos;
using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaNegocios
{
    public class CN_ROL
    {
        private CD_ROL objcd_rol = new CD_ROL();

        public List<ROL> Listar()
        {
            return objcd_rol.Listar();
        }
    }
}
