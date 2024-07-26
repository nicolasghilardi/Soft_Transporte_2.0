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
    public partial class frmDetalleViaje : Form
    {
        private int viajeId;
        private int nroRendicion;
        public frmDetalleViaje(int idViaje, int nroRendicion)
        {
            InitializeComponent();
            this.viajeId = idViaje;
            this.nroRendicion = nroRendicion;
            txtID.Text = idViaje.ToString();
            txtNroRendicion.Text = nroRendicion.ToString();

            this.Load += new EventHandler(frmDetalleViaje_Load);
            
        }
        private void LoadDetalleViaje()
        {
            CN_DETALLE_VIAJE objNegocioDetalleViaje = new CN_DETALLE_VIAJE();
            List<DETALLE_VIAJE> listaDetalles = objNegocioDetalleViaje.ListarPorViaje(viajeId);

            dgvData.Rows.Clear();



            foreach (DETALLE_VIAJE item in listaDetalles)
            {
                dgvData.Rows.Add(new object[]
               {
                   "",
                    item.idDetalleViaje,
                    item.oViaje.idViaje,
                    item.fecha,
                    item.destino,
                    item.vuelta,
                    item.rtoNumero,
                    item.micNro,
                    item.observacion,
                    item.demoras,
                });
            }
            txtFecha.Text = DateTime.Now.ToString("dd/MM/yyyy");
        }

        private void frmDetalleViaje_Load(object sender, EventArgs e)
        {
            LoadDetalleViaje();
            
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            string mensaje = string.Empty;
            DETALLE_VIAJE objDetalleViaje = new DETALLE_VIAJE()
            {
                idDetalleViaje = Convert.ToInt32(txtIdDetalleViaje.Text),
                oViaje = new VIAJES() { idViaje = Convert.ToInt32(txtID.Text) },
                fecha = txtFecha.Text,
                destino = txtDestino.Text,
                vuelta = txtVuelta.Text,
                rtoNumero = txtRemito.Text,
                micNro = txtMic.Text,
                observacion = txtObservacion.Text,
                demoras = Convert.ToInt32(txtDemoras.Text),
            };

            if (objDetalleViaje.idDetalleViaje == 0 )
            {
                int idDetalleViajeGenerado =  new CN_DETALLE_VIAJE().Registrar(objDetalleViaje, out mensaje);

                if (idDetalleViajeGenerado != 0 ) 
                {
                    dgvData.Rows.Add(new object[]
                    {
                        "",
                        idDetalleViajeGenerado,
                        txtID.Text,
                        txtFecha.Text,
                        txtDestino.Text,
                        txtVuelta.Text,
                        txtRemito.Text,
                        txtMic.Text,
                        txtObservacion,
                        txtDemoras, 
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
                bool resultado = new CN_DETALLE_VIAJE().Editar(objDetalleViaje, out mensaje);
                if (resultado)
                {
                    DataGridViewRow row = dgvData.Rows[Convert.ToInt32(txtIndice.Text)];
                    row.Cells["idDetalleViaje"].Value = txtIdDetalleViaje.Text;
                    row.Cells["idViajes"].Value = txtID.Text;
                    row.Cells["fecha"].Value = txtFecha.Text;
                    row.Cells["destino"].Value = txtDestino.Text;
                    row.Cells["vuelta"].Value = txtVuelta.Text;
                    row.Cells["rtoNumero"].Value = txtRemito.Text;
                    row.Cells["micNro"].Value = txtMic.Text;
                    row.Cells["observacion"].Value = txtObservacion.Text;
                    row.Cells["demoras"].Value = txtDemoras.Text;

                    Limpiar();
                }
                else
                {
                    MessageBox.Show(mensaje);
                }
                MessageBox.Show("Detalle del Viaje guardado correctamente");
            }
            
        }

        private void Limpiar()
        {
            txtDestino.Text = "";
            txtVuelta.Text = "";
            txtRemito.Text = "";
            txtMic.Text = "";
            txtObservacion.Text = "";
            txtDemoras.Text = "0";
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
                    txtIdDetalleViaje.Text = dgvData.Rows[indice].Cells["idDetalleViaje"].Value.ToString();
                    txtID.Text = dgvData.Rows[indice].Cells["idViajes"].Value.ToString();
                    txtFecha.Text = dgvData.Rows[indice].Cells["fecha"].Value.ToString();
                    txtDestino.Text = dgvData.Rows[indice].Cells["destino"].Value.ToString();
                    txtVuelta.Text = dgvData.Rows[indice].Cells["vuelta"].Value.ToString();
                    txtRemito.Text = dgvData.Rows[indice].Cells["rtoNumero"].Value.ToString();
                    txtMic.Text = dgvData.Rows[indice].Cells["micNro"].Value.ToString();
                    txtObservacion.Text = dgvData.Rows[indice].Cells["observacion"].Value.ToString();
                    txtDemoras.Text = dgvData.Rows[indice].Cells["demoras"].Value.ToString();
                }
            }
        }
    }
}
