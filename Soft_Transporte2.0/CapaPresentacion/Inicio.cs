using CapaEntidades;
using CapaNegocios;
using FontAwesome.Sharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class Inicio : Form
    {
        private static USUARIO usuarioActual;
        private static IconMenuItem MenuActivo = null;
        private static Form FormularioActivo = null;
        public Inicio(USUARIO objusuario = null)
        {
            if (objusuario == null)
                usuarioActual = new USUARIO() { NombreCompleto = "Admin Predefinido", IdUsuario = 1 };
            else
                usuarioActual = objusuario;

            InitializeComponent();
        }

        private void Inicio_Load(object sender, EventArgs e)
        {
            List<PERMISO> ListaPermisos = new CN_PERMISO().Listar(usuarioActual.IdUsuario);
            foreach (IconMenuItem iconmenu in menuStrip1.Items)
            {
                bool encontrado = ListaPermisos.Any(m => m.NombreMenu == iconmenu.Name);

                if (!encontrado)
                {
                    iconmenu.Visible = false;
                }
                else
                {
                    iconmenu.IconChar = IconChar.User;
                }

            }

            lblUsuario.Text = usuarioActual.NombreCompleto;
        }
        
        private void AbrirFormulario(IconMenuItem menu, Form formulario)
        {
            if (MenuActivo != null)
            {
                MenuActivo.BackColor = Color.White;
            }
            menu.BackColor = Color.Silver;
            MenuActivo = menu;

            if (FormularioActivo != null)
            {
                FormularioActivo.Close();
            }
            FormularioActivo = formulario;
            formulario.TopLevel = false;
            formulario.FormBorderStyle = FormBorderStyle.None;
            formulario.Dock = DockStyle.Fill;
            formulario.BackColor = Color.DarkKhaki;

            contenedor.Controls.Add(formulario);
            formulario.Show();
        }
       

        private void menuUsuario_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmUsuarios());
        }

        private void menuClientes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmClientes());
        }

        private void menuChoferes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmChoferes());
        }

        private void menuCamiones_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmCamiones());
        }

        private void menuViajes_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmViajes());
        }

        private void menuAcercaDe_Click(object sender, EventArgs e)
        {
            AbrirFormulario((IconMenuItem)sender, new frmMantenimiento());
        }

        private void iconMenuItem1_Click(object sender, EventArgs e)
        {
            frmAcercaDe md = new frmAcercaDe();
            md.ShowDialog();
        }

        private void subMenuNegocio_Click(object sender, EventArgs e)
        {
            AbrirFormulario(menuAjustes, new frmNegocio());
        }
    }
}
