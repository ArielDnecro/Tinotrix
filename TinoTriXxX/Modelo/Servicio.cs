using CodorniX.Modelo;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoTriXxX.ConexionBaseDatos;

namespace TinoTriXxX.Modelo
{
    public class Servicio
    {
        public new class Repository
        {
            protected Konection _Conexion = new Konection();

            #region Funciones
            public bool VerificarExistenciaPuerto()
            {
                try
                {
                    DataTable table = new DataTable();
                    bool Existe = false;
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_VerificarExistencia_Puerto";
                    table = _Conexion.ExecuteQuery(comando);
                    Existe = Convert.ToBoolean(table.Rows[0]["IsBTech"]);
                    return Existe;
                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(Actualizar Puerto) " + e.Message);
                }

            }
            public String FindPuerto()
            {
                DataTable table = new DataTable();
                string Puerto = "";
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_Puerto_Find";
                    table = _Conexion.ExecuteQuery(comando);
                    Puerto = table.Rows[0]["VchPuerto"].ToString();


                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(Obtener puerto de conexion local conf)" + e.Message);
                }

                return Puerto;
            }
            public bool ActualizarPuerto(string Strpuerto)
            {
                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ActualizarPuerto";
                    comando.Parameters.Add("@VchPuerto", SqlDbType.VarChar, 150);
                    comando.Parameters["@VchPuerto"].Value = Strpuerto;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(Actualizar Puerto) " + e.Message);
                }

            }
            public bool VerificarExistenciaIPServidor()
            {
                try
                {
                    DataTable table = new DataTable();
                    bool Existe = false;
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_VerificarExistencia_IPServidor";
                    table = _Conexion.ExecuteQuery(comando);
                    Existe = Convert.ToBoolean(table.Rows[0]["IsBTech"]);
                    return Existe;
                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(Actualizar Puerto) " + e.Message);
                }

            }
            public String FindIPServidor()
            {
                DataTable table = new DataTable();
                string ip = "";
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_IPServidor_Find";
                    table = _Conexion.ExecuteQuery(comando);
                    ip = table.Rows[0]["VchIpServidor"].ToString();


                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(Obtener ip servidor)" + e.Message);
                }

                return ip;
            }
            public bool ActualizarIPServidor(string StrIpServer)
            {
                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ActualizarIpServidor";
                    comando.Parameters.Add("@VchIpServidor", SqlDbType.VarChar, 150);
                    comando.Parameters["@VchIpServidor"].Value = StrIpServer;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(Actualizar IpServidor) " + e.Message);
                }

            }

            protected Connection _ConexionHost = new Connection();

            public SucursalServidor ObtenerConfServerHost(Guid UidSucursal)
            {
                DataTable table = new DataTable();
                SucursalServidor ServerItem = new SucursalServidor();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    comando.CommandText = "usp_SucursalServidor_Find";
                    comando.AddParameter("@UidSucursal", UidSucursal, SqlDbType.UniqueIdentifier);
                    table = _ConexionHost.ExecuteQuery(comando);
                    if (int.Parse(table.Rows.Count.ToString()) == 1)
                    {

                        ServerItem.UidSucursal = UidSucursal;
                        ServerItem.StrNombreIP = table.Rows[0]["VchNombreIP"].ToString();
                        ServerItem.StrPuerto = table.Rows[0]["VchPuerto"].ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new ServicioConexionException("(No se pudo obtener el servidor de la sucursal)" + e.Message);
                }

                return ServerItem;
            }
            #endregion Funciones

            #region Excepciones
            public class ServicioConexionException : Exception
            {
                public ServicioConexionException(string mensaje) : base("(ServicioConexionException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
