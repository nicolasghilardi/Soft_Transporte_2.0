using CapaEntidades;
using CapaNegocios;
using CapaPresentacion.Utilidades;
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
    public partial class frmClientes : Form
    {
        public frmClientes()
        {
            InitializeComponent();
        }

        private void frmClientes_Load(object sender, EventArgs e)
        {
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                }
            }
            cboBusqueda.DisplayMember = "Texto";
            cboBusqueda.ValueMember = "Valor";
            cboBusqueda.SelectedIndex = 0;

            //Mostrar todos los Clientes
            List<CLIENTES> listaUsuario = new CN_CLIENTE().Listar();
            foreach (CLIENTES item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] {
                    "",
                    item.idCliente,
                    item.nombreCliente, 
                    item.telCliente, 
                    item.direccion, 
                    item.correo, 
                    item.observacion,
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            CLIENTES obj = new CLIENTES()
            {

                idCliente = Convert.ToInt32(txtId.Text),
                nombreCliente = txtNombre.Text,
                telCliente = txtTelefono.Text,
                direccion = txtDireccion.Text,
                correo = txtCorreo.Text,
                observacion = txtObservacion.Text
            };

            if (obj.idCliente == 0)
            {
                int idClienteGenerado = new CN_CLIENTE().Registrar(obj, out mensaje);

                if (idClienteGenerado != 0)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        "",
                        idClienteGenerado,
                        txtNombre.Text,
                        txtTelefono.Text,
                        txtDireccion.Text,
                        txtCorreo.Text,
                        txtObservacion.Text
                    });

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            else
            {
                bool resultado = new CN_CLIENTE().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtInidice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["nombreCliente"].Value = txtNombre.Text;
                    row.Cells["telCliente"].Value = txtTelefono.Text;
                    row.Cells["direccion"].Value = txtDireccion.Text;
                    row.Cells["correo"].Value = txtCorreo.Text;
                    row.Cells["observacion"].Value = txtObservacion.Text;
                    
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
        }
        private void Limpiar()
        {
            txtId.Text = "0";
            txtInidice.Text = "-1";
            txtNombre.Text = "";
            txtTelefono.Text = "";
            txtDireccion.Text = "";
            txtCorreo.Text = "";
            txtObservacion.Text = "";

            txtNombre.Select();
        }

        private void dgvData_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0)
                return;
            if (e.ColumnIndex == 0)
            {
                e.Paint(e.CellBounds, DataGridViewPaintParts.All);

                var w = Properties.Resources.check.Width;
                var h = Properties.Resources.check.Height;
                var x = e.CellBounds.Left + (e.CellBounds.Width - w) / 2;
                var y = e.CellBounds.Top + (e.CellBounds.Height - h) / 2;

                e.Graphics.DrawImage(Properties.Resources.check, new Rectangle(x, y, w, h));
                e.Handled = true;
            }
        }

        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtInidice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtNombre.Text = dgvData.Rows[indice].Cells["nombreCliente"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["telCliente"].Value.ToString();
                    txtDireccion.Text = dgvData.Rows[indice].Cells["direccion"].Value.ToString();
                    txtCorreo.Text = dgvData.Rows[indice].Cells["correo"].Value.ToString();
                    txtObservacion.Text = dgvData.Rows[indice].Cells["observacion"].Value.ToString();
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("Desea Eliminar el Cliente?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    CLIENTES obj = new CLIENTES()
                    {
                        idCliente = Convert.ToInt32(txtId.Text),
                    };
                    bool respuesta = new CN_CLIENTE().Eliminar(obj, out mensaje);

                    if (respuesta)
                    {
                        dgvData.Rows.RemoveAt(Convert.ToInt32(txtInidice.Text));
                    }
                    else
                    {
                        MessageBox.Show(mensaje, "Mensaje", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    }
                }
            }
            Limpiar();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            string columnaFilto = ((OpcionCombo)cboBusqueda.SelectedItem).Valor.ToString();

            if (dgvData.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvData.Rows)
                {
                    if (row.Cells[columnaFilto].Value.ToString().Trim().ToUpper().Contains(txtBusqueda.Text.Trim().ToUpper()))
                        row.Visible = true;
                    else
                        row.Visible = false;

                }
            }
        }

        private void btnLimpiarBuscador_Click(object sender, EventArgs e)
        {
            txtBusqueda.Text = "";
            foreach (DataGridViewRow row in dgvData.Rows)
            {
                row.Visible = true;
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }
    }
}
