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
using System.Windows.Media;
using CapaEntidades;
using CapaNegocios;
using System.Diagnostics.Eventing.Reader;

namespace CapaPresentacion
{
    public partial class frmMantenimiento : Form
    {
        public frmMantenimiento()
        {
            InitializeComponent();
        }

        private void frmMantenimiento_Load(object sender, EventArgs e)
        {
            cboEstado.Items.Add(new OpcionCombo() { Valor = 1, Texto = "Hecho" });
            cboEstado.Items.Add(new OpcionCombo() { Valor = 0, Texto = "Pendiente" });
            cboEstado.DisplayMember = "Texto";
            cboEstado.ValueMember = "Valor"; 
            cboEstado.SelectedIndex = 0;

            List<CAMIONES> listaCamiones = new CN_CAMIONES().Listar();
            foreach (CAMIONES item in listaCamiones)
            {
                cboCamion.Items.Add(new OpcionCombo() { Valor = item.idCamion, Texto = item.patente });
                cboCamion.DisplayMember = "Texto";
                cboCamion.ValueMember = "Valor";
                cboCamion.SelectedIndex = 0;
            }
            foreach (DataGridViewColumn columna in dgvData.Columns)
            {
                if (columna.Visible == true && columna.Name != "btnSeleccionar")
                {
                    cboBusqueda.Items.Add(new OpcionCombo() { Valor = columna.Name, Texto = columna.HeaderText });
                    cboBusqueda.DisplayMember = "Texto";
                    cboBusqueda.ValueMember = "Valor";
                    cboBusqueda.SelectedIndex = 0;
                }
            }

            List<MANTENIMIENTO> lista = new CN_MANTENIMIENTO().Listar();

            foreach (MANTENIMIENTO item in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    "",
                    item.idMantenimiento,
                    item.oCamion.idCamion,
                    item.oCamion.patente,
                    item.fecha,
                    item.tipoMantenimiento,
                    item.descripcion,
                    item.kilometrajes,
                    item.proxMantenimiento,
                    item.costo,
                    item.partesReemplazadas,
                    item.comentarios,
                    item.estado
                });

            }
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            MANTENIMIENTO objMantenimiento = new MANTENIMIENTO()
            {
                idMantenimiento = Convert.ToInt32(txtId.Text),
                oCamion = new CAMIONES() { idCamion = Convert.ToInt32(((OpcionCombo)cboCamion.SelectedItem).Valor) },
                fecha = txtFecha.Text,
                tipoMantenimiento = txtTipoMantenimiento.Text,
                descripcion = txtDescripcion.Text,
                kilometrajes = txtKilometraje.Text,
                proxMantenimiento = txtProxMantenimiento.Text,
                costo = Convert.ToDecimal(txtCosto.Text),
                partesReemplazadas = txtPartReemplazadas.Text,
                comentarios = txtComentarios.Text,
                estado = cboEstado.Text,
            };

            if (objMantenimiento.idMantenimiento == 0)
            {
                int idMantenimientoGenerado = new CN_MANTENIMIENTO().Registrar(objMantenimiento, out mensaje);

                if (idMantenimientoGenerado != 0)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        "",
                        idMantenimientoGenerado,
                        ((OpcionCombo)cboCamion.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboCamion.SelectedItem).Texto.ToString(),
                        txtFecha.Text,
                        txtTipoMantenimiento.Text,
                        txtDescripcion.Text,
                        txtKilometraje.Text,
                        txtProxMantenimiento.Text,
                        txtCosto.Text,
                        txtPartReemplazadas.Text,
                        txtComentarios.Text,
                        cboEstado.Text,
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
                bool resultado = new CN_MANTENIMIENTO().Editar(objMantenimiento, out mensaje);

                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["idCamion"].Value = ((OpcionCombo)cboCamion.SelectedItem).Valor.ToString();
                    row.Cells["Camion"].Value = ((OpcionCombo)cboCamion.SelectedItem).Texto.ToString();
                    row.Cells["fecha"].Value = txtFecha.Text;
                    row.Cells["tipoMantenimiento"].Value = txtTipoMantenimiento.Text;
                    row.Cells["descripcion"].Value = txtDescripcion.Text;
                    row.Cells["kilometrajes"].Value = txtKilometraje.Text;
                    row.Cells["proxMantenimiento"].Value = txtProxMantenimiento.Text;
                    row.Cells["costo"].Value = txtCosto.Text;
                    row.Cells["partesReemplazadas"].Value = txtPartReemplazadas.Text;
                    row.Cells["comentarios"].Value = txtComentarios.Text;
                    row.Cells["estado"].Value = cboEstado.Text;
                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
            }
            MessageBox.Show("Registro guardado correctamente.");
        }
        private void Limpiar()
        {
            txtIndice.Text = "-1";
            txtId.Text = "0";
            cboCamion.SelectedIndex = 0;
            txtTipoMantenimiento.Text = "";
            txtDescripcion.Text = "";
            txtKilometraje.Text = "";
            txtProxMantenimiento.Text = "";
            txtCosto.Text = "0.00";
            txtPartReemplazadas.Text = "";
            txtComentarios.Text = "";
            cboEstado.SelectedIndex = 0;

            cboCamion.Select();
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
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    foreach (OpcionCombo oc in cboCamion.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idCamion"].Value))
                        {
                            int indice_combo = cboCamion.Items.IndexOf(oc);
                            cboCamion.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    txtFecha.Text = dgvData.Rows[indice].Cells["fecha"].Value.ToString();
                    txtTipoMantenimiento.Text = dgvData.Rows[indice].Cells["tipoMantenimiento"].Value.ToString();
                    txtDescripcion.Text = dgvData.Rows[indice].Cells["descripcion"].Value.ToString();
                    txtKilometraje.Text = dgvData.Rows[indice].Cells["kilometrajes"].Value.ToString();
                    txtProxMantenimiento.Text = dgvData.Rows[indice].Cells["proxMantenimiento"].Value.ToString();
                    txtCosto.Text = dgvData.Rows[indice].Cells["costo"].Value.ToString();
                    txtPartReemplazadas.Text = dgvData.Rows[indice].Cells["partesReemplazadas"].Value.ToString();
                    txtComentarios.Text = dgvData.Rows[indice].Cells["comentarios"].Value.ToString();
                    cboEstado.Text = dgvData.Rows[indice].Cells["estado"].Value.ToString();
                }
            }
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
