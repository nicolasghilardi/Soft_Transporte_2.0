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
    public class CD_CAMIONES
    {
        public List<CAMIONES> Listar()
        {
            List<CAMIONES> Lista = new List<CAMIONES>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select idCamion,modelo,patente,capacidad,vencTecnica,vencRuta,vencSeguro,vencTazaFiscal,cambioAceite from CAMIONES");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new CAMIONES()
                            {
                                idCamion = Convert.ToInt32(dr["idCamion"]),
                                modelo = dr["modelo"].ToString(),
                                patente = dr["patente"].ToString(),
                                capacidad = Convert.ToDecimal(dr["capacidad"]),
                                vencTecnica = dr["vencTecnica"].ToString(),
                                vencRuta = dr["vencRuta"].ToString(),
                                vencSeguro = dr["vencSeguro"].ToString(),
                                vencTazaFiscal = dr["vencTazaFiscal"].ToString(),
                                cambioAceite = dr["cambioAceite"].ToString(),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<CAMIONES>();
                }

            }
            return Lista;
        }

        public int Registrar(CAMIONES obj, out string Mensaje)
        {
            int idCamionGenerado = 0;
            Mensaje = string.Empty;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))

                {
                    SqlCommand cmd = new SqlCommand("SP_RegistrarCamion".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("modelo", obj.modelo);
                    cmd.Parameters.AddWithValue("patente", obj.patente);
                    cmd.Parameters.AddWithValue("capacidad", obj.capacidad);
                    cmd.Parameters.AddWithValue("vencTecnica", obj.vencTecnica);
                    cmd.Parameters.AddWithValue("vencRuta", obj.vencRuta);
                    cmd.Parameters.AddWithValue("vencSeguro", obj.vencSeguro);
                    cmd.Parameters.AddWithValue("vencTazaFiscal", obj.vencTazaFiscal);
                    cmd.Parameters.AddWithValue("cambioAceite", obj.cambioAceite);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    idCamionGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch (Exception ex)
            {
                idCamionGenerado = 0;
                Mensaje = ex.Message;
            }

            return idCamionGenerado;
        }

        public bool Editar(CAMIONES obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))

                {
                    SqlCommand cmd = new SqlCommand("SP_EditarCamion".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idCamion", obj.idCamion);
                    cmd.Parameters.AddWithValue("modelo", obj.modelo);
                    cmd.Parameters.AddWithValue("patente", obj.patente);
                    cmd.Parameters.AddWithValue("capacidad", obj.capacidad);
                    cmd.Parameters.AddWithValue("vencTecnica", obj.vencTecnica);
                    cmd.Parameters.AddWithValue("vencRuta", obj.vencRuta);
                    cmd.Parameters.AddWithValue("vencSeguro", obj.vencSeguro);
                    cmd.Parameters.AddWithValue("vencTazaFiscal", obj.vencTazaFiscal);
                    cmd.Parameters.AddWithValue("cambioAceite", obj.cambioAceite);
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

        public bool Eliminar(CAMIONES obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;


            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))

                {
                    SqlCommand cmd = new SqlCommand("delete from CAMIONES where idCamion = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.idCamion);
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
