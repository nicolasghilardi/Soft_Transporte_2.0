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
    public partial class frmCamiones : Form
    {
        public frmCamiones()
        {
            InitializeComponent();
        }

        private void frmCamiones_Load(object sender, EventArgs e)
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
            List<CAMIONES> listaUsuario = new CN_CAMIONES().Listar();
            foreach (CAMIONES item in listaUsuario)
            {
                dgvData.Rows.Add(new object[]{
                    "", 
                    item.idCamion,
                    item.modelo, 
                    item.patente, 
                    item.capacidad, 
                    item.vencTecnica,
                    item.vencRuta,
                    item.vencSeguro,
                    item.vencTazaFiscal,
                    item.cambioAceite,
            });

            }
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            CAMIONES obj = new CAMIONES()
            {

                idCamion = Convert.ToInt32(txtId.Text),
                modelo = txtModelo.Text,
                patente = txtPatente.Text,
                capacidad = Convert.ToDecimal(txtCapacidad.Text),
                vencTecnica = cmbVencTecnica.Text,
                vencRuta = cmbVencRuta.Text,
                vencSeguro = cmbVencSeguro.Text,
                vencTazaFiscal = cmbVencTazaFiscal.Text,
                cambioAceite = cmbCambioAceite.Text,
            };

            if (obj.idCamion == 0)
            {
                int idCamionGenerado = new CN_CAMIONES().Registrar(obj, out mensaje);

                if (idCamionGenerado != 0)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        "",
                        idCamionGenerado,
                        txtModelo.Text,
                        txtPatente.Text,
                        txtCapacidad.Text,
                        cmbVencTecnica.Text,
                        cmbVencRuta.Text,
                        cmbVencSeguro.Text,
                        cmbVencTazaFiscal.Text,
                        cmbCambioAceite.Text,
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
                bool resultado = new CN_CAMIONES().Editar(obj, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtInidice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["modelo"].Value = txtModelo.Text;
                    row.Cells["patente"].Value = txtPatente.Text;
                    row.Cells["capacidad"].Value = txtCapacidad.Text;
                    row.Cells["vencTecnica"].Value = cmbVencTecnica.Text;
                    row.Cells["vencRuta"].Value = cmbVencRuta.Text;
                    row.Cells["vencSeguro"].Value = cmbVencSeguro.Text;
                    row.Cells["vencTazaFiscal"].Value = cmbVencTazaFiscal.Text;
                    row.Cells["cambioAceite"].Value = cmbCambioAceite.Text;
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
            txtModelo.Text = "";
            txtPatente.Text = "";
            txtCapacidad.Text = "0.00";

            txtModelo.Select();
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
                    txtModelo.Text = dgvData.Rows[indice].Cells["modelo"].Value.ToString();
                    txtPatente.Text = dgvData.Rows[indice].Cells["patente"].Value.ToString();
                    txtCapacidad.Text = dgvData.Rows[indice].Cells["capacidad"].Value.ToString();
                    cmbVencTecnica.Text = dgvData.Rows[indice].Cells["vencTecnica"].Value.ToString();
                    cmbVencRuta.Text = dgvData.Rows[indice].Cells["vencRuta"].Value.ToString();
                    cmbVencSeguro.Text = dgvData.Rows[indice].Cells["vencSeguro"].Value.ToString();
                    cmbVencTazaFiscal.Text = dgvData.Rows[indice].Cells["vencTazaFiscal"].Value.ToString();
                    cmbCambioAceite.Text = dgvData.Rows[indice].Cells["cambioAceite"].Value.ToString();
                    
                }
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(txtId.Text) != 0)
            {
                if (MessageBox.Show("Desea Eliminar el Camion?", "Mensaje", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string mensaje = string.Empty;
                    CAMIONES obj = new CAMIONES()
                    {
                        idCamion = Convert.ToInt32(txtId.Text),
                    };
                    bool respuesta = new CN_CAMIONES().Eliminar(obj, out mensaje);

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
