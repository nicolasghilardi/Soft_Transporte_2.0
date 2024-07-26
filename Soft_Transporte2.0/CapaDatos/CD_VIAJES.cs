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
    public class CD_VIAJES
    {
        public int ObtenerCorrelativo()
        {
            int idcorrelativo = 0;

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select count(*) + 1 from VIAJES");
                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;
                    oconexion.Open();

                    idcorrelativo = Convert.ToInt32(cmd.ExecuteScalar());

                }
                catch (Exception ex)
                {
                    idcorrelativo = 0;
                }

            }

            return idcorrelativo;
        }
        public List<VIAJES> Listar()
        {
            List<VIAJES> Lista = new List<VIAJES>();

            using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
            {
                try
                {
                    StringBuilder query = new StringBuilder();
                    query.AppendLine("select v.idViaje,");
                    query.AppendLine("v.FechaRegistro, v.nroRendicion,");
                    query.AppendLine("c.idCamion, c.patente,");
                    query.AppendLine("ch.idChofer, ch.nombreChofer,");
                    query.AppendLine("cl.idCliente, cl.nombreCliente,");
                    query.AppendLine("v.argEntregado,v.chilEntregado,v.gastosArg,v.gastosChil,v.kmSalida,v.kmVuelta,v.kmRecorrido,v.combLts,v.consumo,v.restoArg,v.restoChil,convert(char(10), v.FechaRegistro, 103) as FechaRegistro");
                    query.AppendLine("from VIAJES v");
                    query.AppendLine("inner join CAMIONES c on v.idCamion = c.idCamion");
                    query.AppendLine("inner join CHOFERES ch on v.idChofer = ch.idChofer");
                    query.AppendLine("inner join CLIENTES cl on v.idCliente = cl.idCliente");

                    SqlCommand cmd = new SqlCommand(query.ToString(), oconexion);
                    cmd.CommandType = CommandType.Text;

                    oconexion.Open();

                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            Lista.Add(new VIAJES()
                            {
                                idViaje = Convert.ToInt32(dr["idViaje"]),
                                FechaRegistro = dr["FechaRegistro"].ToString(),
                                nroRendicion = dr["nroRendicion"].ToString(),
                                oCamion = new CAMIONES() { patente = dr["patente"].ToString() },
                                oChofer = new CHOFERES() { nombreChofer = dr["nombreChofer"].ToString() },
                                oCliente = new CLIENTES() { nombreCliente = dr["nombreCliente"].ToString() },
                                argEntregado = Convert.ToDecimal(dr["argEntregado"]),
                                chilEntregado = Convert.ToDecimal(dr["chilEntregado"]),
                                gastosArg = Convert.ToDecimal(dr["gastosArg"]),
                                gastosChil = Convert.ToDecimal(dr["gastosChil"]),
                                kmSalida = Convert.ToDecimal(dr["kmSalida"]),
                                kmVuelta = Convert.ToDecimal(dr["kmVuelta"]),
                                kmRecorrido = Convert.ToDecimal(dr["kmRecorrido"]),
                                combLts = Convert.ToDecimal(dr["combLts"]),
                                consumo = Convert.ToDecimal(dr["consumo"]),
                                restoArg = Convert.ToDecimal(dr["restoArg"]),
                                restoChil = Convert.ToDecimal(dr["restoChil"]),
                            });
                        }
                    }
                }
                catch (Exception ex)
                {
                    Lista = new List<VIAJES>();
                }

            }
            return Lista;
        }

        public int Registrar(VIAJES obj, out string Mensaje)
        {
            int idViajeGenerado = 0;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {


                    SqlCommand cmd = new SqlCommand("SP_RegistrarViaje", oconexion);
                    cmd.Parameters.AddWithValue("nroRendicion", obj.nroRendicion);
                    cmd.Parameters.AddWithValue("idCamion", obj.oCamion.idCamion);
                    cmd.Parameters.AddWithValue("idChofer", obj.oChofer.idChofer);
                    cmd.Parameters.AddWithValue("idCliente", obj.oCliente.idCliente);
                    cmd.Parameters.AddWithValue("argEntregado", obj.argEntregado);
                    cmd.Parameters.AddWithValue("chilEntregado", obj.chilEntregado);
                    cmd.Parameters.AddWithValue("gastosArg", obj.gastosArg);
                    cmd.Parameters.AddWithValue("gastosChil", obj.gastosChil);
                    cmd.Parameters.AddWithValue("kmSalida", obj.kmSalida);
                    cmd.Parameters.AddWithValue("kmVuelta", obj.kmVuelta);
                    cmd.Parameters.AddWithValue("kmRecorrido", obj.kmRecorrido);
                    cmd.Parameters.AddWithValue("combLts", obj.combLts);
                    cmd.Parameters.AddWithValue("consumo", obj.consumo);
                    cmd.Parameters.AddWithValue("restoArg", obj.restoArg);
                    cmd.Parameters.AddWithValue("restoChil", obj.restoChil);
                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;
                    oconexion.Open();
                    cmd.ExecuteNonQuery();

                    idViajeGenerado = Convert.ToInt32(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();

                }
            }
            catch (Exception ex)
            {
                idViajeGenerado = 0;
                Mensaje = ex.Message;
            }
            return idViajeGenerado;
        }

        public bool Editar(VIAJES obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using(SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("SP_EditarViaje".ToString(), oconexion);
                    cmd.Parameters.AddWithValue("idViaje", obj.idViaje);
                    cmd.Parameters.AddWithValue("nroRendicion", obj.nroRendicion);
                    cmd.Parameters.AddWithValue("idCamion", obj.oCamion.idCamion);
                    cmd.Parameters.AddWithValue("idChofer", obj.oChofer.idChofer);
                    cmd.Parameters.AddWithValue("idCliente", obj.oCliente.idCliente);
                    cmd.Parameters.AddWithValue("argEntregado", obj.argEntregado);
                    cmd.Parameters.AddWithValue("chilEntregado", obj.chilEntregado);
                    cmd.Parameters.AddWithValue("gastosArg", obj.gastosArg);
                    cmd.Parameters.AddWithValue("gastosChil", obj.gastosChil);
                    cmd.Parameters.AddWithValue("kmSalida", obj.kmSalida);
                    cmd.Parameters.AddWithValue("kmVuelta", obj.kmVuelta);
                    cmd.Parameters.AddWithValue("kmRecorrido", obj.kmRecorrido);
                    cmd.Parameters.AddWithValue("combLts", obj.combLts);
                    cmd.Parameters.AddWithValue("consumo", obj.consumo);
                    cmd.Parameters.AddWithValue("restoArg", obj.restoArg);
                    cmd.Parameters.AddWithValue("restoChil", obj.restoChil);

                    cmd.Parameters.Add("Resultado", SqlDbType.Int).Direction = ParameterDirection.Output;
                    cmd.Parameters.Add("Mensaje", SqlDbType.VarChar, 500).Direction = ParameterDirection.Output;
                    cmd.CommandType = CommandType.StoredProcedure;

                    oconexion.Open();

                    cmd.ExecuteNonQuery();
                    respuesta = Convert.ToBoolean(cmd.Parameters["Resultado"].Value);
                    Mensaje = cmd.Parameters["Mensaje"].Value.ToString();
                }
            }
            catch(Exception ex)
            {
                respuesta = false;
                Mensaje = ex.Message;
            }
            return respuesta;
        }

        public bool Eliminar(VIAJES obj, out string Mensaje)
        {
            bool respuesta = false;
            Mensaje = string.Empty;

            try
            {
                using (SqlConnection oconexion = new SqlConnection(Conexion.cadena))
                {
                    SqlCommand cmd = new SqlCommand("delete from VIAJES where idViaje = @id", oconexion);
                    cmd.Parameters.AddWithValue("@id", obj.idViaje);
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



