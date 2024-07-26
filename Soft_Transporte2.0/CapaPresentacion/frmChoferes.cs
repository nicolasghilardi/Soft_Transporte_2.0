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
    public partial class frmChoferes : Form
    {
        public frmChoferes()
        {
            InitializeComponent();
        }

        private void frmChoferes_Load(object sender, EventArgs e)
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

            //Mostrar todos los usuarios
            List<CHOFERES> listaUsuario = new CN_CHOFERES().Listar();
            foreach (CHOFERES item in listaUsuario)
            {
                dgvData.Rows.Add(new object[] {
                    "", 
                    item.idChofer,
                    item.nombreChofer, 
                    item.documento, 
                    item.vencLicencia, 
                    item.vencLinti,
                    item.telefono,
                    item.antiguedad,
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            CHOFERES obj = new CHOFERES()
            {

                idChofer = Convert.ToInt32(txtId.Text),
                nombreChofer = txtNombre.Text,
                documento = txtDocumento.Text,
                vencLicencia = cmbVencLicencia.Text,
                vencLinti = cmbVencLinti.Text,
                telefono = txtTelefono.Text,
                antiguedad = Convert.ToInt32(txtAntiguedad.Text),
            };

            if (obj.idChofer == 0)
            {
                int idChoferGenerado = new CN_CHOFERES().Registrar(obj, out mensaje);

                if (idChoferGenerado != 0)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        "",
                        idChoferGenerado,
                        txtNombre.Text,
                        txtDocumento.Text,
                        cmbVencLicencia.Text,
                        cmbVencLinti.Text,
                        txtTelefono.Text,
                        txtAntiguedad.Text,
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
                bool resultado = new CN_CHOFERES().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtInidice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["nombreChofer"].Value = txtNombre.Text;
                    row.Cells["documento"].Value = txtDocumento.Text;
                    row.Cells["vencLicencia"].Value = cmbVencLicencia.Text;
                    row.Cells["vencLinti"].Value = cmbVencLinti.Text;
                    row.Cells["telefono"].Value = txtTelefono.Text;
                    row.Cells["antiguedad"].Value = txtAntiguedad.Text;
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
            txtDocumento.Text = "";
            txtTelefono.Text = "";
            txtAntiguedad.Text = "";

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
                    txtNombre.Text = dgvData.Rows[indice].Cells["nombreChofer"].Value.ToString();
                    txtDocumento.Text = dgvData.Rows[indice].Cells["documento"].Value.ToString();
                    cmbVencLicencia.Text = dgvData.Rows[indice].Cells["vencLicencia"].Value.ToString();
                    cmbVencLinti.Text = dgvData.Rows[indice].Cells["vencLinti"].Value.ToString();
                    txtTelefono.Text = dgvData.Rows[indice].Cells["telefono"].Value.ToString();
                    txtAntiguedad.Text = dgvData.Rows[indice].Cells["antiguedad"].Value.ToString();
                    
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("Desea Eliminar el Chofer?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    CHOFERES obj = new CHOFERES()
                    {
                        idChofer = Convert.ToInt32(txtId.Text),
                    };
                    bool respuesta = new CN_CHOFERES().Eliminar(obj, out mensaje);

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
