using CapaEntidades;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CapaDatos
{
    public class CD_CHOFERES
    {
        public List<CHOFERES> Listar()
        {
            List<CHOFERES> Lista = new List<CHOFERES>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idChofer,nombreChofer,documento,vencLicencia,vencLinti,telefono,antiguedad from CHOFERES");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new CHOFERES()
                            {
                                idChofer = Convert.ToInt32(dr["idChofer"]),
                                nombreChofer = dr["nombreChofer"].ToString(),
                                documento = dr["documento"].ToString(),
                                vencLicencia = dr["vencLicencia"].ToString(),
                                vencLinti = dr["vencLinti"].ToString(),
                                telefono = dr["telefono"].ToString(),
                                antiguedad = Convert.ToInt32(dr["antiguedad"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<CHOFERES>();
                }

            }
            return Lista;
        }

        public int Registrar(CHOFERES obj, out string Mensaje)
        {
            int idChoferGenerado = 0;
            Mensaje = string.Empty;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))

                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarChofer".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("nombreChofer", obj.nombreChofer);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("vencLicencia", obj.vencLicencia);
                    cmd.Parameters.AddWithValue("vencLinti", obj.vencLinti);
                    cmd.Parameters.AddWithValue("telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("antiguedad", obj.antiguedad);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idChoferGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idChoferGenerado = 0;
                Mensaje = ex.Message;
            }

            return idChoferGenerado;
        }

        public bool Editar(CHOFERES obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))

                {
                    SqlCommand cmd = new SqlCommand("SP_EditarChofer".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idChofer", obj.idChofer);
                    cmd.Parameters.AddWithValue("nombreChofer", obj.nombreChofer);
                    cmd.Parameters.AddWithValue("documento", obj.documento);
                    cmd.Parameters.AddWithValue("vencLicencia", obj.vencLicencia);
                    cmd.Parameters.AddWithValue("vencLinti", obj.vencLinti);
                    cmd.Parameters.AddWithValue("telefono", obj.telefono);
                    cmd.Parameters.AddWithValue("antiguedad", obj.antiguedad);
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

        public bool Eliminar(CHOFERES obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))

                {
                    SqlCommand cmd = new SqlCommand("delete from CHOFERES where idChofer = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.idChofer);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();
                    respuesta = cmd.ExecuteNonQuery() > 0 ? true : false;
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
