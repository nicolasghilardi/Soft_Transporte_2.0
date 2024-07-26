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
    public partial class frmViajes : Form
    {
        public frmViajes()
        {
            InitializeComponent();
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void frmViajes_Load(object sender, EventArgs e)
        {
            
            List<CAMIONES> listaCamiones = new CN_CAMIONES().Listar();
            foreach (CAMIONES item in listaCamiones)
            {
                cboCamion.Items.Add(new OpcionCombo() { Valor = item.idCamion, Texto = item.patente });
                cboCamion.DisplayMember = "Texto";
                cboCamion.ValueMember = "Valor";
                cboCamion.SelectedIndex = 0;
            }
            List<CHOFERES> listaChoferes = new CN_CHOFERES().Listar();
            foreach (CHOFERES item in listaChoferes)
            {
                cboChofer.Items.Add(new OpcionCombo() { Valor = item.idChofer, Texto = item.nombreChofer });
                cboChofer.DisplayMember = "Texto";
                cboChofer.ValueMember = "Valor";
                cboChofer.SelectedIndex = 0;
            }
            List<CLIENTES> listaClientes = new CN_CLIENTE().Listar();
            foreach (CLIENTES item in listaClientes)
            {
                cboCliente.Items.Add(new OpcionCombo() { Valor = item.idCliente, Texto = item.nombreCliente });
                cboCliente.DisplayMember = "Texto";
                cboCliente.ValueMember = "Valor";
                cboCliente.SelectedIndex = 0;
            }

            List<VIAJES> lista = new CN_VIAJES().Listar();

            foreach (VIAJES item in lista)
            {
                dgvData.Rows.Add(new object[]
                {
                    "",
                    item.idViaje,
                    item.FechaRegistro,
                    item.nroRendicion,
                    item.oCamion.idCamion,
                    item.oCamion.patente,
                    item.oChofer.idChofer,
                    item.oChofer.nombreChofer,
                    item.oCliente.idCliente,
                    item.oCliente.nombreCliente,
                    item.argEntregado,
                    item.chilEntregado,
                    item.gastosArg,
                    item.gastosChil,
                    item.kmSalida,
                    item.kmVuelta,
                    item.kmRecorrido,
                    item.combLts,
                    item.consumo,
                    item.restoArg,
                    item.restoChil
                });

            }
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            int idcorrelativo = new CN_VIAJES().ObtenerCorrelativo();
            string numerorendimiento = string.Format("{0:00000}", idcorrelativo);

            string mensaje = string.Empty;
            VIAJES objViaje = new VIAJES()
            {
                idViaje = Convert.ToInt32(txtId.Text),
                nroRendicion = numerorendimiento,
                oCamion = new CAMIONES() { idCamion = Convert.ToInt32(((OpcionCombo)cboCamion.SelectedItem).Valor)},
                oChofer = new CHOFERES() { idChofer = Convert.ToInt32(((OpcionCombo)cboChofer.SelectedItem).Valor)},
                oCliente = new CLIENTES() { idCliente = Convert.ToInt32(((OpcionCombo)cboCliente.SelectedItem).Valor) },
                argEntregado = Convert.ToDecimal(txtArgEntregado.Text),
                chilEntregado = Convert.ToDecimal(txtChilEntregado.Text),
                gastosArg = Convert.ToDecimal(txtGastosArg.Text),
                gastosChil = Convert.ToDecimal(txtGastosChil.Text),
                kmSalida = Convert.ToDecimal(txtKmSalida.Text),
                kmVuelta = Convert.ToDecimal(txtKmLlegada.Text),
                kmRecorrido = Convert.ToDecimal(txtKmRecorrido.Text),
                combLts = Convert.ToDecimal(txtCombustible.Text),
                consumo = Convert.ToDecimal(txtConsumo.Text),
                restoArg = Convert.ToDecimal(txtRestoArg.Text),
                restoChil = Convert.ToDecimal(txtRestoChil.Text),
            };

            if(objViaje.idViaje == 0)
            {
                int idViajeGenerado = new CN_VIAJES().Registrar(objViaje, out mensaje);
                if (idViajeGenerado != 0)
                {
                    dgvData.Rows.Add(new object[]
                    {
                        "",
                        idViajeGenerado,
                        ((OpcionCombo)cboCamion.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboCamion.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cboChofer.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboChofer.SelectedItem).Texto.ToString(),
                        ((OpcionCombo)cboCliente.SelectedItem).Valor.ToString(),
                        ((OpcionCombo)cboCliente.SelectedItem).Texto.ToString(),
                        txtArgEntregado.Text,
                        txtChilEntregado.Text,
                        txtGastosArg.Text,
                        txtGastosChil.Text,
                        txtKmSalida.Text,
                        txtKmLlegada.Text,
                        txtKmRecorrido.Text,
                        txtCombustible.Text,
                        txtConsumo.Text,
                        txtRestoArg.Text,
                        txtRestoChil.Text
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
                bool resultado = new CN_VIAJES().Editar(objViaje, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["Id"].Value = txtId.Text;
                    row.Cells["FechaRegistro"].Value = txtFecha.Text;
                    row.Cells["idCamion"].Value = ((OpcionCombo)cboCamion.SelectedItem).Valor.ToString();
                    row.Cells["patente"].Value = ((OpcionCombo)cboCamion.SelectedItem).Texto.ToString();
                    row.Cells["idChofer"].Value = ((OpcionCombo)cboChofer.SelectedItem).Valor.ToString();
                    row.Cells["nombreChofer"].Value = ((OpcionCombo)cboChofer.SelectedItem).Texto.ToString();
                    row.Cells["idCliente"].Value = ((OpcionCombo)cboCliente.SelectedItem).Valor.ToString();
                    row.Cells["nombreCliente"].Value = ((OpcionCombo)cboCliente.SelectedItem).Texto.ToString();
                    row.Cells["argEntregado"].Value = txtArgEntregado.Text;
                    row.Cells["chilEntregado"].Value = txtChilEntregado.Text;
                    row.Cells["gastosArg"].Value = txtGastosArg.Text;
                    row.Cells["gastosChil"].Value = txtGastosChil.Text;
                    row.Cells["kmSalida"].Value = txtKmSalida.Text;
                    row.Cells["kmVuelta"].Value = txtKmLlegada.Text;
                    row.Cells["kmRecorrido"].Value = txtKmRecorrido.Text;
                    row.Cells["combLts"].Value = txtCombustible.Text;
                    row.Cells["consumo"].Value = txtConsumo.Text;
                    row.Cells["restoArg"].Value = txtRestoArg.Text;
                    row.Cells["restoChil"].Value = txtRestoChil.Text;

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
            txtArgEntregado.Text = "0.00";
            txtChilEntregado.Text = "0.00";
            txtGastosArg.Text = "0.00";
            txtGastosChil.Text = "0.00";
            txtKmSalida.Text = "0.00";
            txtKmLlegada.Text = "0.00";
            txtKmRecorrido.Text = "0.00";
            txtCombustible.Text = "0.00";
            txtConsumo.Text = "0.00";
            txtRestoArg.Text = "0.00";
            txtRestoChil.Text = "0.00";
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            Limpiar();
        }

        

        private void dgvData_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvData.Columns[e.ColumnIndex].Name == "btnSeleccionar")
            {
                int indice = e.RowIndex;
                if (indice >= 0)
                {
                    txtIndice.Text = indice.ToString();
                    txtId.Text = dgvData.Rows[indice].Cells["Id"].Value.ToString();
                    txtFecha.Text = dgvData.Rows[indice].Cells["FechaRegistro"].Value.ToString();
                    foreach (OpcionCombo oc in cboCamion.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idCamion"].Value))
                        {
                            int indice_combo = cboCamion.Items.IndexOf(oc);
                            cboCamion.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    foreach (OpcionCombo oc in cboChofer.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idChofer"].Value))
                        {
                            int indice_combo = cboChofer.Items.IndexOf(oc);
                            cboChofer.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    foreach (OpcionCombo oc in cboCliente.Items)
                    {
                        if (Convert.ToInt32(oc.Valor) == Convert.ToInt32(dgvData.Rows[indice].Cells["idCliente"].Value))
                        {
                            int indice_combo = cboCliente.Items.IndexOf(oc);
                            cboCliente.SelectedIndex = indice_combo;
                            break;
                        }
                    }
                    txtArgEntregado.Text = dgvData.Rows[indice].Cells["argEntregado"].Value.ToString();
                    txtChilEntregado.Text = dgvData.Rows[indice].Cells["chilEntregado"].Value.ToString();
                    txtGastosArg.Text = dgvData.Rows[indice].Cells["gastosArg"].Value.ToString();
                    txtGastosChil.Text = dgvData.Rows[indice].Cells["gastosChil"].Value.ToString();
                    txtKmSalida.Text = dgvData.Rows[indice].Cells["kmSalida"].Value.ToString();
                    txtKmLlegada.Text = dgvData.Rows[indice].Cells["kmVuelta"].Value.ToString();
                    txtKmRecorrido.Text = dgvData.Rows[indice].Cells["kmRecorrido"].Value.ToString();
                    txtCombustible.Text = dgvData.Rows[indice].Cells["combLts"].Value.ToString();
                    txtConsumo.Text = dgvData.Rows[indice].Cells["consumo"].Value.ToString();
                    txtRestoArg.Text = dgvData.Rows[indice].Cells["restoArg"].Value.ToString();
                    txtRestoChil.Text = dgvData.Rows[indice].Cells["restoChil"].Value.ToString();
                }
            }
        }

        private void dgvData_CellPainting_1(object sender, DataGridViewCellPaintingEventArgs e)
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

        private void dgvData_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) { 
                if (dgvData.Columns.Contains("nroRendicion") && dgvData.Columns.Contains("Id")) 
                { 
                    int nroRendicion = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["nroRendicion"].Value);
                    int idViajes = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["Id"].Value);
                    MessageBox.Show("Nro. de Rendición seleccionado: " + nroRendicion.ToString() + "\nID del Viaje seleccionado: " + idViajes.ToString());

                    frmDetalleViaje detalleviaje = new frmDetalleViaje(idViajes, nroRendicion);

                    detalleviaje.Show();
                }
                else
                {
                    MessageBox.Show("La columna 'nroRendicion' o 'idViaje' no existe en el DataGridView.");
                }

            }
        }
    }
}
