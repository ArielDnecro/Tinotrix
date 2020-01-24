using CodorniX.Common;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    public class SucursalServidor
    {
        #region propiedades
        protected bool _ExistsInDatabase;
        
        private Guid _UidSucursal;
        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }

        private String _StrNombreIP;
        public String StrNombreIP
        {
            get { return _StrNombreIP; }
            set { _StrNombreIP = value; }
        }

        private String _StrPuerto;
        public String StrPuerto
        {
            get { return _StrPuerto; }
            set { _StrPuerto = value; }
        }
        #endregion propiedades
        public new class Repository
        {
            //Comienzo repositorio raiz tomado de suscursaltelefono -> telefono
            protected Connection _Conexion = new Connection();

            public bool Salvar(SucursalServidor ServerItem)//Lista 16/11/2017
            {

                try
                {
                    SqlCommand comando = new SqlCommand();
                    ServerItem._ExistsInDatabase = true;
                    comando.CommandText = "usp_SucursalServidor_AddUpdate";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@UidSucursal", ServerItem._UidSucursal, SqlDbType.UniqueIdentifier);
                    comando.Parameters.Add("@VchNombreIP", SqlDbType.VarChar, 150).Value = ServerItem.StrNombreIP;
                    comando.Parameters.Add("@VchPuerto", SqlDbType.VarChar, 150).Value = ServerItem.StrPuerto;
                    //sqlCommand.Parameters.Add("@HasPaid", SqlDbType.Bit).Value = hasPaid;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new ServidorException("(No se pudo salver el servidor de la sucursal)" + e.Message);
                }
            }
            public bool Eliminar(Guid UidSucursal)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalServidor_Remove";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@UidSucursal", UidSucursal, SqlDbType.UniqueIdentifier);
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new ServidorException("(No se pudo borrar el servidor de la sucursal)" + e.Message);
                }
            }
            //public SucursalLicencia IfExists(Guid uid)
            //{
            //    DataTable table = new DataTable();
            //    SucursalLicencia licencia = new SucursalLicencia();
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();

            //        comando.CommandType = CommandType.StoredProcedure;
            //        comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
            //        comando.Parameters["@UidLicencia"].Value = uid;
            //        comando.CommandText = "Wpf_SucursalLicencia_IfExists";
            //        table = _Conexion.ExecuteQuery(comando);
            //        if (int.Parse(table.Rows[0]["IntNoLicencia"].ToString()) == 1)
            //        {
            //            licencia._UidLicencia = uid;
            //        }
            //        //else {
            //        //    licencia._UidLicencia = Guid.Empty;
            //        //    //licencia._UidSucursal = 
            //        //}

            //    }
            //    catch (Exception e)
            //    {
            //        throw new DatabaseException("Error populating", e);
            //    }

            //    return licencia;
            //}
            public SucursalServidor Obtener(Guid UidSucursal)
            {
                DataTable table = new DataTable();
                SucursalServidor ServerItem = new SucursalServidor();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    comando.CommandText = "usp_SucursalServidor_Find";
                    comando.AddParameter("@UidSucursal",  UidSucursal, SqlDbType.UniqueIdentifier);
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows.Count.ToString()) == 1)
                    {

                        ServerItem._UidSucursal = UidSucursal;
                        ServerItem._StrNombreIP = table.Rows[0]["VchNombreIP"].ToString();
                        ServerItem._StrPuerto = table.Rows[0]["VchPuerto"].ToString();
                    }
                }
                catch (Exception e)
                {
                    throw new ServidorException("(No se pudo obtener el servidor de la sucursal)" + e.Message);
                }

                return ServerItem;
            }

            #region Excepciones
            public class ServidorException : Exception
            {
                public ServidorException(string mensaje) : base("(ServidorException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
