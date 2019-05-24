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
    [Serializable]
    public class SucursalLicencia
    {
        #region propiedades
        protected bool _ExistsInDatabase;

        protected int _IntNo;
        public int IntNo
        {
            get { return _IntNo; }
            set { _IntNo = value; }
        }

        protected int _IntNoTotal;
        public int IntNoTotal
        {
            get { return _IntNoTotal; }
            set { _IntNoTotal = value; }
        }


        protected Guid _UidLicencia;
        public Guid UidLicencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; }
        }
        private Guid _UidSucursal;
        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }

        protected bool _BooStatus;
        public bool BooStatus
        {
            get { return _BooStatus; }
            set { _BooStatus = value; }
        }

        protected bool _BooStatusLicencia;
        public bool BooStatusLicencia
        {
            get { return _BooStatusLicencia; }
            set { _BooStatusLicencia = value; }
        }

        string _StrOrdenaPor = string.Empty;
        public string StrOrdenaPor
        {
            get { return _StrOrdenaPor; }
            set { _StrOrdenaPor = value; }
        }
        Orden _EnuOrden = Orden.ASC;
        public Orden EnuOrden
        {
            get { return _EnuOrden; }
            set { _EnuOrden = value; }
        }

        #endregion propiedades
        public new class Repository
        {
            //Comienzo repositorio raiz tomado de suscursaltelefono -> telefono
            protected Connection _Conexion = new Connection();

            public bool Save(SucursalLicencia SucursalLicencia)//Lista 16/11/2017
            {

                try
                {
                    SqlCommand comando = new SqlCommand();
                    //if (SucursalLicencia._ExistsInDatabase)
                    //{
                    //    //return InternalUpdate(SucursalImpresora);
                    //    comando.CommandText = "usp_Licencia_Update";

                    //}
                    //else
                    //{
                    //    SucursalLicencia._ExistsInDatabase = true;
                    //    //return InternalSave(SucursalImpresora);
                    //    comando.CommandText = "usp_SucursalLicencia_Add";
                    //}
                    SucursalLicencia._ExistsInDatabase = true;
                    comando.CommandText = "usp_SucursalLicencia_Add";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@IntNo", SucursalLicencia._IntNo, SqlDbType.Int);
                    comando.AddParameter("@UidLicencia", SucursalLicencia._UidLicencia, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@UidSucursal", SucursalLicencia._UidSucursal, SqlDbType.UniqueIdentifier);

                    comando.Parameters.Add("@BitStatus", SqlDbType.Bit).Value = SucursalLicencia._BooStatus;
                    comando.Parameters.Add("@BitStatusLicencia", SqlDbType.Bit).Value = SucursalLicencia._BooStatusLicencia;
                    //sqlCommand.Parameters.Add("@HasPaid", SqlDbType.Bit).Value = hasPaid;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Licencia from Sucursal", e);
                }
            }

            public bool EliminarAllSucursal(Guid UidSucursal){
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalLicencia_Eliminar_Todas_Sucursal";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@UidSucursal", UidSucursal, SqlDbType.UniqueIdentifier);
                    
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Licencia from Sucursal", e);
                }
            }
            public List<SucursalLicencia> FindAll(Guid uid)//Lista 16/11/2017
            {
                DataTable table = new DataTable();
                List<SucursalLicencia> Licencias = new List<SucursalLicencia>();
                SucursalLicencia licencia = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalLicencia_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        licencia = new SucursalLicencia()
                        {
                            _IntNo = int.Parse(row["IntNo"].ToString()),
                            _UidLicencia = new Guid(row["UidLicencia"].ToString()),
                            _UidSucursal = uid,
                            _BooStatus = Convert.ToBoolean(row["BitStatus"]),
                            _BooStatusLicencia = Convert.ToBoolean(row["BitStatusLicencia"]),

                            _ExistsInDatabase = true,
                        };
                        Licencias.Add(licencia);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return Licencias;
            }

            public SucursalLicencia IfExists(Guid uid)
            {
                DataTable table = new DataTable();
                SucursalLicencia licencia = new SucursalLicencia();
                try
                {
                    SqlCommand comando = new SqlCommand();
                   
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidLicencia"].Value = uid;
                    comando.CommandText = "Wpf_SucursalLicencia_IfExists";
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["IntNoLicencia"].ToString()) == 1)
                    {
                        licencia._UidLicencia = uid;
                    }
                        //else {
                        //    licencia._UidLicencia = Guid.Empty;
                        //    //licencia._UidSucursal = 
                        //}

                    }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return licencia;
            }

            public SucursalLicencia IfExistsServer(Guid uid) //15/feb/19//realizado otra vez
            {
                DataTable table = new DataTable();
                SucursalLicencia licencia = new SucursalLicencia();
                try
                {
                    SqlCommand comando = new SqlCommand();

                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidLicencia"].Value = uid;
                    comando.CommandText = "Wpf_SucursalLicencia_IfExists_Server";
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["IntNoLicencia"].ToString()) == 1)
                    {
                        licencia._UidLicencia = uid;
                    }
                    //else {
                    //    licencia._UidLicencia = Guid.Empty;
                    //    //licencia._UidSucursal = 
                    //}

                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return licencia;
            } 
            public SucursalLicencia Find(Guid uid, int IntPrimeraVez)
            {
                DataTable table = new DataTable();
                SucursalLicencia licencia = new SucursalLicencia();
                SqlCommand comando = new SqlCommand();
                comando.CommandType = CommandType.StoredProcedure;
                try
                {
                    comando.CommandText = "Wpf_SucursalLicencia_Find";
                    //comando.Parameters.Add("@UidLicenciaVieja", SqlDbType.UniqueIdentifier);
                    //comando.Parameters["@UidLicenciaVieja"].Value = uid;
                    comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidLicencia"].Value = uid;
                    comando.Parameters.Add("@IntPrimeraVez", SqlDbType.Int);
                    comando.Parameters["@IntPrimeraVez"].Value = IntPrimeraVez;
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows.Count.ToString()) == 1)
                    {
                        licencia._IntNo = int.Parse(table.Rows[0]["IntNo"].ToString());
                        licencia._UidLicencia = uid;
                        licencia._UidSucursal = new Guid(table.Rows[0]["UidSucursal"].ToString());
                        licencia._BooStatus = Convert.ToBoolean(table.Rows[0]["BitStatus"]);
                        licencia._ExistsInDatabase = true;
                        //obtener el total de maquinas
                        SqlCommand comando2 = new SqlCommand();
                        //DataTable table2 = new DataTable();
                        comando2.CommandType = CommandType.StoredProcedure;
                        comando2.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                        comando2.Parameters["@UidSucursal"].Value = licencia._UidSucursal;
                        comando2.CommandText = "Wpf_SucursalLicencia_NoTotal";
                        table = _Conexion.ExecuteQuery(comando2);
                        licencia._IntNoTotal = int.Parse(table.Rows[0]["IntNoTotal"].ToString());
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return licencia;
            }

            public bool HabilitarLicenciaAnterior(Guid LicenciaAnterior)
            {

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "Wpf_SucursalLicencia_HabilitarLicencia";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@UidLicenciaAnterior", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidLicenciaAnterior"].Value = LicenciaAnterior;
                    //sqlCommand.Parameters.Add("@HasPaid", SqlDbType.Bit).Value = hasPaid;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Habilitar licencia anterior", e);
                }
            }
        }
    }
}
    

