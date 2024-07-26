using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CapaEntidades;

namespace CapaDatos
{
    public class CD_MANTENIMIENTO
    {
        public List<MANTENIMIENTO> Listar()
        {
            List<MANTENIMIENTO> Lista = new List<MANTENIMIENTO>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select m.idMantenimiento,");
                    query.AppendLine("c.idCamion,c.patente,");
                    query.AppendLine("m.fecha, m.tipoMantenimiento,m.descripcion,");
                    query.AppendLine("m.kilometrajes, m.proxMantenimiento, m.costo,");
                    query.AppendLine("m.partesReemplazadas,m.comentarios,m.estado");
                    query.AppendLine("from MANTENIMIENTO m");
                    query.AppendLine("inner join CAMIONES c on m.idCamion = c.idCamion");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = System.Data.CommandType.Text;

                    oconexion.Open();

                    using(SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new MANTENIMIENTO()
                            {
                                idMantenimiento = Convert.ToInt32(dr["idMantenimiento"]),
                                oCamion = new CAMIONES() { idCamion = Convert.ToInt32(dr["idCamion"]), patente = dr["patente"].ToString() },
                                fecha = dr["fecha"].ToString(),
                                tipoMantenimiento = dr["tipoMantenimiento"].ToString(),
                                descripcion = dr["descripcion"].ToString(),
                                kilometrajes = dr["kilometrajes"].ToString(),
                                proxMantenimiento = dr["proxMantenimiento"].ToString(),
                                costo = Convert.ToDecimal(dr["costo"]),
                                partesReemplazadas = dr["partesReemplazadas"].ToString(),
                                comentarios = dr["comentarios"].ToString(),
                                estado = dr["estado"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex) 
                {
                    Lista = new List<MANTENIMIENTO>();
                }
            }
            return Lista;
        }

        public int Registrar(MANTENIMIENTO obj, out string Mensaje)
        {
            int idMantenimientoGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarMantenimiento".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idCamion", obj.oCamion.idCamion);
                    cmd.Parameters.AddWithValue("fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("tipoMantenimiento", obj.tipoMantenimiento);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("kilometrajes", obj.kilometrajes);
                    cmd.Parameters.AddWithValue("proxMantenimiento", obj.proxMantenimiento);
                    cmd.Parameters.AddWithValue("costo", obj.costo);
                    cmd.Parameters.AddWithValue("partesReemplazadas", obj.partesReemplazadas);
                    cmd.Parameters.AddWithValue("comentarios", obj.comentarios);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idMantenimientoGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex) 
            {
                idMantenimientoGenerado = 0;
                Mensaje = ex.Message;
            }

            return idMantenimientoGenerado;
        }

        public bool Editar(MANTENIMIENTO obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarMantenimiento".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idMantenimiento", obj.idMantenimiento);
                    cmd.Parameters.AddWithValue("idCamion", obj.oCamion.idCamion);
                    cmd.Parameters.AddWithValue("fecha", obj.fecha);
                    cmd.Parameters.AddWithValue("tipoMantenimiento", obj.tipoMantenimiento);
                    cmd.Parameters.AddWithValue("descripcion", obj.descripcion);
                    cmd.Parameters.AddWithValue("kilometrajes", obj.kilometrajes);
                    cmd.Parameters.AddWithValue("proxMantenimiento", obj.proxMantenimiento);
                    cmd.Parameters.AddWithValue("costo", obj.costo);
                    cmd.Parameters.AddWithValue("partesReemplazadas", obj.partesReemplazadas);
                    cmd.Parameters.AddWithValue("comentarios", obj.comentarios);
                    cmd.Parameters.AddWithValue("estado", obj.estado);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex) 
            {
                respuesta = false;
                Mensaje = ex.Message;
            }

            return respuesta;
        }
    }
}
