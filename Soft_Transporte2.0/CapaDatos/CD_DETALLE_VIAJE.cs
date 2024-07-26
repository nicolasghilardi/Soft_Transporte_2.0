using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_DETALLE_VIAJE
    {
        public List<DETALLE_VIAJE> ListarPorViaje(int idViaje)
        {
            List<DETALLE_VIAJE> Lista = new List<DETALLE_VIAJE>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("SELECT dv.idDetalleViaje, dv.idViaje, dv.fecha, dv.destino, dv.vuelta,");
                    query.AppendLine("dv.rtoNumero, dv.micNro, dv.observacion, dv.demoras, dv.FechaRegistro");
                    query.AppendLine("FROM DETALLE_VIAJE dv");
                    query.AppendLine("WHERE dv.idViaje = @idViaje");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.Parameters.AddWithValue("@idViaje", idViaje);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new DETALLE_VIAJE()
                            {
                                idDetalleViaje = Convert.ToInt32(dr["idDetalleViaje"]),
                                oViaje = new VIAJES() { idViaje = Convert.ToInt32(dr["idViaje"]) },
                                fecha = dr["fecha"].ToString(),
                                destino = dr["destino"].ToString(),
                                vuelta = dr["vuelta"].ToString(),
                                rtoNumero = dr["rtoNumero"].ToString(),
                                micNro = dr["micNro"].ToString(),
                                observacion = dr["observacion"].ToString(),
                                demoras = Convert.ToInt32(dr["demoras"]),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                            });
                        }
                    }
                }

                catch (Exception ex) 
                { 
                    Lista = new List<DETALLE_VIAJE>();
                }
            }
            return Lista;
        }

        public int Registrar(DETALLE_VIAJE obj, out string Mensaje)
        {
            int idDetalleViajeGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarDetalleViaje".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idViaje", obj.oViaje.idViaje);
                    cmd.Parameters.AddWithValue("fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("destino", obj.destino);
                    cmd.Parameters.AddWithValue("vuelta", obj.vuelta);
                    cmd.Parameters.AddWithValue("rtoNumero", obj.rtoNumero);
                    cmd.Parameters.AddWithValue("micNro", obj.micNro);
                    cmd.Parameters.AddWithValue("observacion", obj.observacion);
                    cmd.Parameters.AddWithValue("demoras", obj.demoras);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();
                    cmd.ExecuteNonQuery();
                    idDetalleViajeGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idDetalleViajeGenerado = 0;
                Mensaje = ex.Message;
            }

            return idDetalleViajeGenerado;
        }

        public bool Editar (DETALLE_VIAJE obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarDetalleViaje".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idDetalleViaje", obj.idDetalleViaje);
                    cmd.Parameters.AddWithValue("idViaje", obj.oViaje.idViaje);
                    cmd.Parameters.AddWithValue("fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("destino", obj.destino);
                    cmd.Parameters.AddWithValue("vuelta", obj.vuelta);
                    cmd.Parameters.AddWithValue("rtoNumero", obj.rtoNumero);
                    cmd.Parameters.AddWithValue("micNro", obj.micNro);
                    cmd.Parameters.AddWithValue("observacion", obj.observacion);
                    cmd.Parameters.AddWithValue("demoras", obj.demoras);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex) {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }
    }
}
 