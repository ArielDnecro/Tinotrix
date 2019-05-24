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
    public class Empresa
    {
#region propiedades
        private bool _ExistsInDatabase;

        private Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        private string _StrNombreComercial;

        public string StrNombreComercial
        {
            get { return _StrNombreComercial; }
            set { _StrNombreComercial = value; }
        }

        private string _StrRazonSocial;

        public string StrRazonSocial
        {
            get { return _StrRazonSocial; }
            set { _StrRazonSocial = value; }
        }

        private string _StrGiro;

        public string StrGiro
        {
            get { return _StrGiro; }
            set { _StrGiro = value; }
        }

        private string _StrRFC;

        public string StrRFC
        {
            get { return _StrRFC; }
            set { _StrRFC = value; }
        }

        private DateTime _DtFechaRegistro;

        public DateTime DtFechaRegistro
        {
            get { return _DtFechaRegistro; }
            set { _DtFechaRegistro = value; }
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

            private bool InternalUpdate(Empresa empresa)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Empresa_Update";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = empresa._UidEmpresa;

                    comando.Parameters.Add("@VchNombreComercial", SqlDbType.NVarChar, 50);
                    comando.Parameters["@VchNombreComercial"].Value = empresa._StrNombreComercial;

                    comando.Parameters.Add("@VchRazonSocial", SqlDbType.NVarChar, 60);
                    comando.Parameters["@VchRazonSocial"].Value = empresa._StrRazonSocial;

                    comando.Parameters.Add("@VchGiro", SqlDbType.NVarChar, 40);
                    comando.Parameters["@VchGiro"].Value = empresa._StrGiro;

                    comando.Parameters.Add("@ChRFC", SqlDbType.NVarChar, 13);
                    comando.Parameters["@ChRFC"].Value = empresa._StrRFC;

                    comando.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
                    comando.Parameters["@VchRutaImagen"].Value = empresa._RutaImagen;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update Empresa entry", e);
                }
            }

            private bool InternalSave(Empresa empresa)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Empresa_Add";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Direction = ParameterDirection.Output;

                    comando.Parameters.Add("@VchNombreComercial", SqlDbType.NVarChar, 50);
                    comando.Parameters["@VchNombreComercial"].Value = empresa._StrNombreComercial;

                    comando.Parameters.Add("@VchRazonSocial", SqlDbType.NVarChar, 60);
                    comando.Parameters["@VchRazonSocial"].Value = empresa._StrRazonSocial;

                    comando.Parameters.Add("@VchGiro", SqlDbType.NVarChar, 40);
                    comando.Parameters["@VchGiro"].Value = empresa._StrGiro;

                    comando.Parameters.Add("@ChRFC", SqlDbType.NVarChar, 13);
                    comando.Parameters["@ChRFC"].Value = empresa._StrRFC;

                    comando.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
                    comando.Parameters["@VchRutaImagen"].Value = empresa._StrRFC;

                    bool result = _Conexion.ExecuteCommand(comando, false);

                    empresa._UidEmpresa = new Guid(comando.Parameters["@UidEmpresa"].Value.ToString());

                    comando.Dispose();

                    return result;
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update Empresa entry", e);
                }
            }

            public bool Save(Empresa empresa)
            {
                if (empresa._ExistsInDatabase)
                    return InternalUpdate(empresa);
                else
                {
                    empresa._ExistsInDatabase = true;
                    return InternalSave(empresa);
                }
            }

            public Empresa Find(Guid uid)
            {
                Empresa empresa = null;

                DataTable table = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_Empresa_Find";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                comando.Parameters["@UidEmpresa"].Value = uid;

                table = _Conexion.ExecuteQuery(comando);

                foreach (DataRow row in table.Rows)
                {
                    empresa = new Empresa()
                    {
                        _ExistsInDatabase = true,
                        _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                        _StrNombreComercial = row["VchNombreComercial"].ToString(),
                        _StrRazonSocial = row["VchRazonSocial"].ToString(),
                        _StrGiro = row["VchGiro"].ToString(),
                        _StrRFC = row["ChRFC"].ToString(),
                        _RutaImagen = row["VchRutaImagen"].ToString(),
                        _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                    };
                }

                return empresa;
            }

            public List<Empresa> FindAll()
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresa = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Empresa_Search";
                    comando.CommandType = CommandType.StoredProcedure;

                    DataTable table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        empresa = new Empresa()
                        {
                            _ExistsInDatabase = true,
                            _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                            _StrNombreComercial = row["VchNombreComercial"].ToString(),
                            _StrRazonSocial = row["VchRazonSocial"].ToString(),
                            _StrGiro = row["VchGiro"].ToString(),
                            _StrRFC = row["ChRFC"].ToString(),
                            _RutaImagen = row["VchRutaImagen"].ToString(),
                            _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        };
                        empresas.Add(empresa);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load Empresas", e);
                }

                return empresas;
            }

            public List<Empresa> BuscarEmpresa (string nombrecomercial, string RFC, string Razonsocial)
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresa = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "sp_BuscarEmpresa";
                comando.CommandType = CommandType.StoredProcedure;

                if (nombrecomercial != string.Empty)
                {
                    comando.Parameters.Add("@VchNombreComercial", SqlDbType.NVarChar, 50);
                    comando.Parameters["@VchNombreComercial"].Value =nombrecomercial;
                }

                if (Razonsocial != string.Empty)
                {
                    comando.Parameters.Add("@VchRazonSocial", SqlDbType.NVarChar, 50);
                    comando.Parameters["@VchRazonSocial"].Value = Razonsocial;
                }
                if (RFC != string.Empty)
                {
                    comando.Parameters.Add("@ChRfc", SqlDbType.NVarChar, 18);
                    comando.Parameters["@ChRfc"].Value = RFC;
                }
                DataTable table = _Conexion.ExecuteQuery(comando);

                foreach (DataRow row in table.Rows)
                {
                    empresa = new Empresa()
                    {
                        _ExistsInDatabase = true,
                        _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                        _StrNombreComercial = row["VchNombreComercial"].ToString(),
                        _StrRazonSocial = row["VchRazonSocial"].ToString(),
                        _StrGiro = row["VchGiro"].ToString(),
                        _StrRFC = row["ChRFC"].ToString(),
                        _RutaImagen = row["VchRutaImagen"].ToString(),
                        _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                    };
                    empresas.Add(empresa);
                }

                return empresas;
            }


            public List<Empresa> FindBy(Criteria criteria)
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresa = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "usp_Empresa_Search";
                comando.CommandType = CommandType.StoredProcedure;

                InjectParameters(comando, criteria);

                DataTable table = _Conexion.ExecuteQuery(comando);

                foreach (DataRow row in table.Rows)
                {
                    empresa = new Empresa()
                    {
                        _ExistsInDatabase = true,
                        _UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                        _StrNombreComercial = row["VchNombreComercial"].ToString(),
                        _StrRazonSocial = row["VchRazonSocial"].ToString(),
                        _StrGiro = row["VchGiro"].ToString(),
                        _StrRFC = row["ChRFC"].ToString(),
                        _RutaImagen = row["VchRutaImagen"].ToString(),
                        _DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                    };
                    empresas.Add(empresa);
                }

                return empresas;
            }

           
            private void InjectParameters(SqlCommand command, Criteria criteria)
            {
                if (!string.IsNullOrWhiteSpace(criteria.NombreComercial))
                {
                    command.Parameters.Add("@VchNombreComercial", SqlDbType.NVarChar, 40);
                    command.Parameters["@VchNombreComercial"].Value = criteria.NombreComercial;
                }
                if (!string.IsNullOrWhiteSpace(criteria.RazonSocial))
                {
                    command.Parameters.Add("@VchRazonSocial", SqlDbType.NVarChar, 50);
                    command.Parameters["@VchRazonSocial"].Value = criteria.RazonSocial;
                }
                if (!string.IsNullOrWhiteSpace(criteria.Giro))
                {
                    command.Parameters.Add("@VchGiro", SqlDbType.NVarChar, 50);
                    command.Parameters["@VchGiro"].Value = criteria.Giro;
                }
                if (!string.IsNullOrWhiteSpace(criteria.RFC))
                {
                    command.Parameters.Add("@ChRFC", SqlDbType.NVarChar, 13);
                    command.Parameters["@ChRFC"].Value = criteria.RFC.Trim();
                }
                if (criteria.RutaImagen != null)
                {
                    command.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
                    command.Parameters["@VchRutaImagen"].Value = criteria.RutaImagen;
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
            }
        }

        public class Criteria
        {
            public string NombreComercial { get; set; }
            public string RazonSocial { get; set; }
            public string Giro { get; set; }
            public string RFC { get; set; }
            public DateTime? FechaRegistroDespues { get; set; }
            public DateTime? FechaRegistroAntes { get; set; }
            public string RutaImagen { get; set; }
        }
    }
}