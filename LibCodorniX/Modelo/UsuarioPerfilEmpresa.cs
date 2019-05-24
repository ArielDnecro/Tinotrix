using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.ConexionDB;
using System.Data;
using System.Data.SqlClient;
using CodorniX.Util;

namespace CodorniX.Modelo
{
    [Serializable]
    public class UsuarioPerfilEmpresa
    {
        [NonSerialized]
        Conexion Conexion = new Conexion();
        private Guid _UidUsuario;

        public Guid UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }

        private Guid _UidPerfil;

        public Guid UidPerfil
        {
            get { return _UidPerfil; }
            set { _UidPerfil = value; }
        }

        private Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        private string _strEmpresa;

        public string strEmpresa
        {
            get { return _strEmpresa; }
            set { _strEmpresa = value; }
        }

        private string _StrPerfil;

        public string StrPerfil
        {
            get { return _StrPerfil; }
            set { _StrPerfil = value; }
        }

        [NonSerialized]
        private Empresa _Empresa;

        public Empresa Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }

        [NonSerialized]
        private Perfil _Perfil;

        public Perfil Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }


        public bool GuardarUsuarioPerfilEmpresa()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandText = "sp_AgregarUsuarioPerfilEmpresa";

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = UidUsuario;

                Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPerfil"].Value = UidPerfil;

                Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;

                Resultado = Conexion.ManipilacionDeDatos(Comando);

                Comando.Dispose();


            }
            catch (Exception)
            {
                throw;
            }




            return Resultado;
        }


        public bool ModificarUsuarioPerfilEmpresa()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "sp_ModificarUsuarioPerfilEmpresa";

            Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidUsuario"].Value = _UidUsuario;

            Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidPerfil"].Value = UidPerfil;

            Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;

            Resultado = Conexion.ManipilacionDeDatos(Comando);

            return Resultado;
        }
        public class Repository
        {
            Connection conn = new Connection();

            public List<UsuarioPerfilEmpresa> FindAll(Guid uidUsuario)
            {
                List<UsuarioPerfilEmpresa> empresaPerfil = new List<UsuarioPerfilEmpresa>();
                Empresa empresa = null;
                Perfil perfil = null;

                SqlCommand command = new SqlCommand();

                command.CommandText = "sp_UsuarioPerfilEmpresa";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidUsuario", uidUsuario, SqlDbType.UniqueIdentifier);

                try
                {
                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        empresa = new Empresa()
                        {
                            UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                            StrNombreComercial = row["VchNombreComercial"].ToString(),
                            StrRazonSocial = row["VchRazonSocial"].ToString(),
                            StrGiro = row["VchGiro"].ToString(),
                            StrRFC = row["ChRFC"].ToString(),
                            DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        };
                        perfil = new Perfil()
                        {
                            strPerfil = row["VchPerfil"].ToString(),
                            UidPerfil = (Guid)row["UidPerfil"]
                        };
                        UsuarioPerfilEmpresa ep = new UsuarioPerfilEmpresa()
                        {
                            _UidUsuario = uidUsuario,
                            _UidEmpresa = empresa.UidEmpresa,
                            _UidPerfil = perfil.UidPerfil,
                            _strEmpresa = empresa.StrNombreComercial,
                            _StrPerfil = perfil.strPerfil,
                            _Empresa = empresa,
                            _Perfil = perfil,
                        };
                        empresaPerfil.Add(ep);
                    }

                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Empresas from Usuario", e);
                }

                return empresaPerfil;
            }

            public List<Empresa> EmpresaPerfil(Guid uidPerfil)
            {
                List<Empresa> empresas = new List<Empresa>();
                Empresa empresa = null;

                SqlCommand command = new SqlCommand();

                command.CommandText = "sp_PerfilEmpresa";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidPerfil", uidPerfil, SqlDbType.UniqueIdentifier);

                try
                {
                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        empresa = new Empresa()
                        {
                            UidEmpresa = new Guid(row["UidEmpresa"].ToString()),
                            StrNombreComercial = row["VchNombreComercial"].ToString(),
                            StrRazonSocial = row["VchRazonSocial"].ToString(),
                            StrGiro = row["VchGiro"].ToString(),
                            StrRFC = row["ChRFC"].ToString(),
                            DtFechaRegistro = (DateTime)row["DtFechaRegistro"],
                        };
                        empresas.Add(empresa);
                    }

                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Empresas from Usuario", e);
                }

                return empresas;
            }
        }
    }
}