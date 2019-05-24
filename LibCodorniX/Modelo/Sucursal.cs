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
    public class Sucursal
    {
#region propiedades
        private bool _ExistsInDatabase;

        private Guid _UidEmpresa;
        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }


        private Guid _UidSucursal;
        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }


        private string _StrNombre;
        public string StrNombre
        {
            get { return _StrNombre; }
            set { _StrNombre = value; }
        }

        
        private DateTime _DtFechaRegistro;
        public DateTime DtFechaRegistro
        {
            get { return _DtFechaRegistro; }
            set { _DtFechaRegistro = value; }
        }


        private Guid _UidTipoSucursal;
        public Guid UidTipoSucursal
        {
            get { return _UidTipoSucursal; }
            set { _UidTipoSucursal = value; }
        }


        private string _StrTipoSucursal;
        public string StrTipoSucursal
        {
            get { return _StrTipoSucursal; }
            set { _StrTipoSucursal = value; }
        }


        protected Guid _UidStatus;
        public Guid UidStatus
        {
            get { return _UidStatus; }
            set { _UidStatus = value; }
        }
        protected string _StrStatus;
        public string StrStatus
        {
            get { return _StrStatus; }
            set { _StrStatus = value; }
        }


        private string _RutaImagen;
        public string RutaImagen
        {
            get { return _RutaImagen; }
            set { _RutaImagen = value; }
        }

#endregion propiedades
        public class Repository
        {
            
            Connection _Conexion = new Connection();

            //ya  esta lista esta funcion 16/10/17
            private bool InternalUpdate(Sucursal sucursal)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Sucursal_Update";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = sucursal._UidSucursal;

                    comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchNombre"].Value = sucursal._StrNombre;

                    comando.Parameters.Add("@DtFechaRegistro", SqlDbType.DateTime);
                    comando.Parameters["@DtFechaRegistro"].Value = sucursal._DtFechaRegistro;

                    comando.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
                    comando.Parameters["@VchRutaImagen"].Value = sucursal._RutaImagen;

                    comando.Parameters.Add("@UidStatus", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidStatus"].Value = sucursal._UidStatus;

                    comando.AddParameter("@UidTipoSucursal", sucursal._UidTipoSucursal, SqlDbType.UniqueIdentifier);

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update Sucursal entry", e);
                }
            }

            //ya  esta lista esta funcion 16/10/17
            private bool InternalSave(Sucursal sucursal)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Sucursal_Add";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Direction = ParameterDirection.Output;

                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = sucursal._UidEmpresa;

                    comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchNombre"].Value = sucursal._StrNombre;

                    comando.Parameters.Add("@DtFechaRegistro", SqlDbType.DateTime);
                    comando.Parameters["@DtFechaRegistro"].Value = DateTime.Today;

                    comando.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
                    comando.Parameters["@VchRutaImagen"].Value = sucursal._RutaImagen;

                    comando.Parameters.Add("@UidStatus", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidStatus"].Value = sucursal._UidStatus;

                    comando.AddParameter("@UidTipoSucursal", sucursal._UidTipoSucursal, SqlDbType.UniqueIdentifier);

                    bool result = _Conexion.ExecuteCommand(comando, false);

                    sucursal._UidSucursal = new Guid(comando.Parameters["@UidSucursal"].Value.ToString());

                    comando.Dispose();

                    return result;
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update Empresa entry", e);
                }
            }

            public bool Save(Sucursal Sucursal)
            {
                if (Sucursal._ExistsInDatabase)
                    return InternalUpdate(Sucursal);
                else
                {
                    Sucursal._ExistsInDatabase = true;
                    return InternalSave(Sucursal);
                }
            }

            //ya  esta lista esta funcion 16/10/17
            public List<Sucursal> BuscarSucursal(Guid UidEmpresa, string Nombre)
            {
                List<Sucursal> sucursales = new List<Sucursal>();
                Sucursal sucursal = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_BuscarSucursal";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                comando.Parameters["@UidEmpresa"].Value = UidEmpresa ;

                if (Nombre != string.Empty)
                {
                    comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                    comando.Parameters["@VchNombre"].Value = Nombre;
                }

                 
                DataTable table = _Conexion.ExecuteQuery(comando);

                foreach (DataRow row in table.Rows)
                {
                    sucursal = new Sucursal()
                    {
                        _ExistsInDatabase = true,
                        _UidSucursal = new Guid(row["UidSucursal"].ToString()),
                        _StrNombre = row["VchNombre"].ToString(),
                        _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                        _UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                        _UidStatus = (Guid)row["UidStatus"],
                        _StrTipoSucursal = (string)row["VchTipoSucursal"],
                        _RutaImagen = row["VchRutaImagen"].ToString(),

                    };
                    sucursales.Add(sucursal);
                }
                return sucursales;
            }


            public Sucursal Find(Guid uid)
            {
                Sucursal Sucursal = null;

                DataTable table = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_Sucursal_Find";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                comando.Parameters["@UidSucursal"].Value = uid;

                table = _Conexion.ExecuteQuery(comando);

                foreach (DataRow row in table.Rows)
                {
                    Sucursal = new Sucursal()
                    {
                        _ExistsInDatabase = true,
                        _UidSucursal = new Guid(row["UidSucursal"].ToString()),
                        _StrNombre = row["VchNombre"].ToString(),
                        _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                        _UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                        _StrTipoSucursal = (string)row["VchTipoSucursal"],
                        _UidStatus = (Guid)row["UidStatus"],
                        
                        _RutaImagen = row["VchRutaImagen"].ToString(),
                    };
                }

                return Sucursal;
            }

            public List<Sucursal> FindAll(Guid uidEmpresa)
            {
                List<Sucursal> sucursales = new List<Sucursal>();
                Sucursal Sucursal = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Sucursal_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.AddParameter("@UidEmpresa", uidEmpresa, SqlDbType.UniqueIdentifier);

                    DataTable table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        Sucursal = new Sucursal()
                        {
                            _ExistsInDatabase = true,
                            _UidSucursal = new Guid(row["UidSucursal"].ToString()),
                            _StrNombre = row["VchNombre"].ToString(),
                            _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                            _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                            _UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                            _StrTipoSucursal = (string)row["VchTipoSucursal"],
                            _UidStatus = (Guid)row["UidStatus"],
                            _RutaImagen = row["VchRutaImagen"].ToString(),
                        };
                        sucursales.Add(Sucursal);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load Sucursales", e);
                }

                return sucursales;
            }

            //public Sucursal Find(Guid uid)
            //{
            //    Empresa empresa = null;

            //    DataTable table = null;

            //    SqlCommand comando = new SqlCommand();
            //    comando.CommandText = "usp_Empresa_Find";
            //    comando.CommandType = CommandType.StoredProcedure;

            //    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
            //    comando.Parameters["@UidEmpresa"].Value = uid;

            //    table = _Conexion.ExecuteQuery(comando);

            //    foreach (DataRow row in table.Rows)
            //    {
            //        empresa = new Empresa()
            //        {
            //            _ExistsInDatabase = true,
            //            _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
            //            _StrNombreComercial = row["VchNombreComercial"].ToString(),
            //            _StrRazonSocial = row["VchRazonSocial"].ToString(),
            //            _StrGiro = row["VchGiro"].ToString(),
            //            _StrRFC = row["ChRFC"].ToString(),
            //            _RutaImagen = row["VchRutaImagen"].ToString(),
            //            _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
            //        };
            //    }

            //    return empresa;
            //}
            public List<Sucursal> FindBy(Criteria criteria)
            {
                List<Sucursal> sucursales = new List<Sucursal>();
                Sucursal sucursal = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_Sucursal_Search";
                comando.CommandType = CommandType.StoredProcedure;

                InjectParameters(comando, criteria);

                DataTable table = _Conexion.ExecuteQuery(comando);

                foreach (DataRow row in table.Rows)
                {
                    sucursal = new Sucursal() 
                    {
                        _ExistsInDatabase = true,
                        _UidSucursal = new Guid(row["UidSucursal"].ToString()),
                        _StrNombre = row["VchNombre"].ToString(),
                        _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                        _UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                        _StrTipoSucursal = (string)row["VchTipoSucursal"],
                        _UidStatus=(Guid)row["UidStatus"],
                        _RutaImagen = row["VchRutaImagen"].ToString(),
                    };
                    sucursales.Add(sucursal);
                }

                return sucursales;
            }

            //listo 16/10/17
            private void InjectParameters(SqlCommand command, Criteria criteria)
            {
                if (!string.IsNullOrWhiteSpace(criteria.Nombre))
                {
                    command.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 40);
                    command.Parameters["@VchNombre"].Value = criteria.Nombre;
                }
                if (criteria.FechaRegistroDespues != null)
                {
                    command.Parameters.Add("@DtFechaRegistroInicio", SqlDbType.DateTime);
                    command.Parameters["@DtFechaRegistroInicio"].Value = criteria.FechaRegistroDespues;
                }
                if (criteria.FechaRegistroAntes != null)
                {
                    command.Parameters.Add("@DtFechaRegistroFin", SqlDbType.DateTime);
                    command.Parameters["@DtFechaRegistroFin"].Value = criteria.FechaRegistroAntes;
                }
                if (!string.IsNullOrWhiteSpace(criteria.Tipos))
                {
                    command.AddParameter("@UidTipoSucursal", criteria.Tipos, SqlDbType.NVarChar, 2000);
                }

                command.Parameters.Add("@UidStatus", SqlDbType.UniqueIdentifier);
                command.Parameters["@UidStatus"].Value = criteria.Status;

                command.AddParameter("@UidEmpresa", criteria.Empresa, SqlDbType.UniqueIdentifier);
            }
        }

        //listo 16/10/17
        public class Criteria
        {
            public string Nombre { get; set; }
            public DateTime? FechaRegistroDespues { get; set; }
            public DateTime? FechaRegistroAntes { get; set; }
            public string Tipos { get; set; }
            public Guid Empresa { get; set; }

            public Guid Status { get; set; }
        }
    }
}