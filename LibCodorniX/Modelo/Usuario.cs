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
    public class Usuario
    {
        #region Propiedades
        [NonSerialized]
        Conexion Conexion = new Conexion();

        protected Guid _UidUsuario;
        public Guid UIDUSUARIO
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }
        protected string _strNombre;
        public string STRNOMBRE
        {
            get { return _strNombre; }
            set { _strNombre = value; }
        }
        protected string _strApellidoPaterno;

        public string STRAPELLIDOPATERNO
        {
            get { return _strApellidoPaterno; }
            set { _strApellidoPaterno = value; }
        }
        protected string _strApellidoMaterno;

        public string STRAPELLIDOMATERNO
        {
            get { return _strApellidoMaterno; }
            set { _strApellidoMaterno = value; }
        }
        protected DateTime _DtFechaNacimiento;

        public DateTime DtFechaNacimiento
        {
            get { return _DtFechaNacimiento; }
            set { _DtFechaNacimiento = value; }
        }
        protected string _strCorreo;

        public string STRCORREO
        {
            get { return _strCorreo; }
            set { _strCorreo = value; }
        }
        
        protected DateTime _DtFechaInicio;

        public DateTime DtFechaInicio
        {
            get { return _DtFechaInicio; }
            set { _DtFechaInicio = value; }
        }
        protected DateTime? _DtFechaFin;

        public DateTime? DtFechaFin
        {
            get { return _DtFechaFin; }
            set { _DtFechaFin = value; }
        }

        protected string _strUsuario;

        public string STRUSUARIO
        {
            get { return _strUsuario; }
            set { _strUsuario = value; }
        }
        protected string _strPassword;

        public string STRPASSWORD
        {
            get { return _strPassword; }
            set { _strPassword = value; }
        }

        protected string _RutaImagen;

        public string RutaImagen
        {
            get { return _RutaImagen; }
            set { _RutaImagen = value; }
        }

        protected Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }


        protected Guid _UidPerfil;
        public Guid UidPerfil
        {
            get { return _UidPerfil; }
            set { _UidPerfil = value; }
        }
        protected Guid _UidStatus;

        public Guid UidStatus
        {
            get { return _UidStatus; }
            set { _UidStatus = value; }
        }

        protected Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        protected string _StrEmpresa;

        public string StrEmpresa
        {
            get { return _StrEmpresa; }
            set { _StrEmpresa = value; }
        }


        protected string _StrPerfil;

        public string StrPerfil
        {
            get { return _StrPerfil; }
            set { _StrPerfil = value; }
        }

        protected string _StrStatus;

        public string StrStatus
        {
            get { return _StrStatus; }
            set { _StrStatus = value; }
        }

        protected string _StrSucursal;

        public string StrSucursal
        {
            get { return _StrSucursal; }
            set { _StrSucursal = value; }
        }

        #endregion

        #region Metodos

        public bool GuardarDatos()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();
            
            try
            {
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandText = "sp_InsertarUsuario";

                Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchNombre"].Value = STRNOMBRE;

                Comando.Parameters.Add("@VchApellidoPaterno", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchApellidoPaterno"].Value = STRAPELLIDOPATERNO;

                Comando.Parameters.Add("@VchApellidoMaterno", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchApellidoMaterno"].Value = STRAPELLIDOMATERNO;

                Comando.Parameters.Add("@DtFechaNacimiento", SqlDbType.DateTime);
                Comando.Parameters["@DtFechaNacimiento"].Value = DtFechaNacimiento;

                Comando.Parameters.Add("@VchCorreo", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchCorreo"].Value = STRCORREO;

                Comando.Parameters.Add("@DtFechaInicio", SqlDbType.DateTime);
                Comando.Parameters["@DtFechaInicio"].Value = DtFechaInicio;

                Comando.Parameters.Add("@DtFechaFin", SqlDbType.DateTime);
                Comando.Parameters["@DtFechaFin"].Value = DtFechaFin;

                Comando.Parameters.Add("@VchUsuario", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchUsuario"].Value = STRUSUARIO;

                Comando.Parameters.Add("@VchPassword", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchPassword"].Value = STRPASSWORD;

                Comando.Parameters.Add("@UidStatus", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidStatus"].Value = UidStatus;

                Comando.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
                Comando.Parameters["@VchRutaImagen"].Value = RutaImagen;

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Direction = ParameterDirection.Output;

                Resultado = Conexion.ManipilacionDeDatos(Comando);

                _UidUsuario = (Guid)Comando.Parameters["@UidUsuario"].Value;
                Comando.Dispose();

               
            }
            catch (Exception)
            {
                throw;
            }

            


            return Resultado;
        }

        public bool ModificarDatos()
        {
            
            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "sp_ModificarUsuario";

            Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidUsuario"].Value = _UidUsuario;

            Comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchNombre"].Value = STRNOMBRE;

            Comando.Parameters.Add("@VchApellidoPaterno", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchApellidoPaterno"].Value = STRAPELLIDOPATERNO;

            Comando.Parameters.Add("@VchApellidoMaterno", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchApellidoMaterno"].Value = STRAPELLIDOMATERNO;

            Comando.Parameters.Add("@DtFechaNacimiento", SqlDbType.DateTime);
            Comando.Parameters["@DtFechaNacimiento"].Value = DtFechaNacimiento;

            Comando.Parameters.Add("@VchCorreo", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchCorreo"].Value = STRCORREO;

            Comando.Parameters.Add("@DtFechaInicio", SqlDbType.DateTime);
            Comando.Parameters["@DtFechaInicio"].Value = DtFechaInicio;

            Comando.Parameters.Add("@DtFechaFin", SqlDbType.DateTime);
            Comando.Parameters["@DtFechaFin"].Value = DtFechaFin;

            Comando.Parameters.Add("@VchUsuario", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchUsuario"].Value = STRUSUARIO;

            Comando.Parameters.Add("@VchPassword", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchPassword"].Value = STRPASSWORD;

            
            Comando.Parameters.Add("@UidStatus", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidStatus"].Value = UidStatus;

            Comando.Parameters.Add("@VchRutaImagen", SqlDbType.NVarChar, 200);
            Comando.Parameters["@VchRutaImagen"].Value = RutaImagen;


            Resultado = Conexion.ManipilacionDeDatos(Comando);

            return Resultado;
        }

        public class Repository
        {
            Conexion Conexion = new Conexion();

            public Usuario Find(Guid uid)
            {
                Usuario usuario = null;
                SqlCommand command = new SqlCommand();

                command.CommandText = "SELECT * FROM Usuario WHERE Usuario.UidUsuario = @uid";
                command.CommandType = CommandType.Text;

                command.AddParameter("@uid", uid, SqlDbType.UniqueIdentifier);

                DataTable table = new Connection().ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    usuario = new Usuario()
                    {
                        _UidUsuario = (Guid)row["UidUsuario"],
                        _strNombre = row["VchNombre"].ToString(),
                        _strApellidoPaterno = row["VchApellidoPaterno"].ToString(),
                        _strApellidoMaterno = row["VchApellidoMaterno"].ToString(),
                        _DtFechaNacimiento = Convert.ToDateTime(row["DtFechaNacimiento"].ToString()),
                        _strCorreo = row["VchCorreo"].ToString(),
                        _DtFechaInicio = Convert.ToDateTime(row["DtFechaInicio"].ToString()),
                        _DtFechaFin = row.IsNull("DtFechaFin") ? (DateTime?)null : Convert.ToDateTime(row["DtFechaFin"].ToString()),
                        _strUsuario = row["VchUsuario"].ToString(),
                        _strPassword= row["VchPassword"].ToString(),
                        // _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                        _UidStatus = new Guid(row["UidStatus"].ToString()),
                    };
                }

                return usuario;
            }

            public Usuario FindByName(string name)
            {
                Usuario usuario = null;
                SqlCommand command = new SqlCommand();

                command.CommandText = "usp_User_FindByName";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@VchUsuario", name, SqlDbType.NVarChar, 50);

                DataTable table = new Connection().ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    usuario = new Usuario()
                    {
                        _UidUsuario = (Guid)row["UidUsuario"],
                        _strNombre = row["VchNombre"].ToString(),
                        _strApellidoPaterno = row["VchApellidoPaterno"].ToString(),
                        _strApellidoMaterno = row["VchApellidoMaterno"].ToString(),
                        _DtFechaNacimiento = Convert.ToDateTime(row["DtFechaNacimiento"].ToString()),
                        _strCorreo = row["VchCorreo"].ToString(),
                        _DtFechaInicio = Convert.ToDateTime(row["DtFechaInicio"].ToString()),
                        _DtFechaFin = row.IsNull("DtFechaFin") ? (DateTime?)null : Convert.ToDateTime(row["DtFechaFin"].ToString()),
                        _strUsuario = row["VchUsuario"].ToString(),
                        _strPassword = row["VchPassword"].ToString(),
                        // _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                        _UidStatus = new Guid(row["UidStatus"].ToString()),
                    };
                }

                return usuario;
            }

            public List<Usuario> CargarUsuarios()
            {
                List<Usuario> Usuarios = new List<Usuario>();
                Usuario usuario = null;

                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "sp_BuscarUsuario";

                    DataTable table = Conexion.Busquedas(comando);

                    foreach (DataRow item in Conexion.Busquedas(comando).Rows)
                    {
                        usuario = new Usuario()
                        {
                            UIDUSUARIO = new Guid(item["UidUsuario"].ToString()),
                            STRNOMBRE = item["VchNombre"].ToString(),
                            STRAPELLIDOPATERNO = item["VchApellidoPaterno"].ToString(),
                            STRAPELLIDOMATERNO = item["VchApellidoMaterno"].ToString(),
                            DtFechaNacimiento = Convert.ToDateTime(item["DtFechaNacimiento"].ToString()),
                            STRCORREO = item["VchCorreo"].ToString(),
                            DtFechaInicio = Convert.ToDateTime(item["DtFechaInicio"].ToString()),
                            DtFechaFin = item.IsNull("DtFechaFin") ? (DateTime?)null : Convert.ToDateTime(item["DtFechaFin"].ToString()),
                            STRUSUARIO = item["VchUsuario"].ToString(),
                            STRPASSWORD = item["VchPassword"].ToString(),
                            UidPerfil = new Guid(item["UidPerfil"].ToString()),
                            UidStatus = new Guid(item["UidStatus"].ToString()),
                            StrPerfil = item["VchPerfil"].ToString(),
                            StrStatus = item["VchStatus"].ToString(),

                        };
                        Usuarios.Add(usuario);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load Empresas", e);
                }

                return Usuarios;
            }


            protected Connection _Conexion = new Connection();

            public bool wpffindsucursal(Guid iduser, Guid idsucursal) {
                bool i = false;
                SqlCommand comando = new SqlCommand();
                DataTable table = new DataTable();
                try
                {
                   
                    comando.CommandText = "Wpf_UsuarioSucursal_IfExists";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@UidUser", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidUser"].Value = iduser;
                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = idsucursal;
                    //return _Conexion.ExecuteCommand(comando);
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["cantUser"].ToString()) == 1)
                    {
                        i = true;
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Comprovar si existe el usuario en la sucursal", e);
                }
                return i;
            }

            public bool wpffindempresa(Guid iduser, Guid idempresa)
            {
                bool i = false;
                SqlCommand comando = new SqlCommand();
                DataTable table = new DataTable();
                try
                {

                    comando.CommandText = "Wpf_UsuarioEmpresa_IfExists";
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.Add("@UidUser", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidUser"].Value = iduser;
                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = idempresa;
                    //return _Conexion.ExecuteCommand(comando);
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["cantUser"].ToString()) == 1)
                    {
                        i = true;
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Comprovar si existe el usuario en la sucursal", e);
                }
                return i;
            }

        }

        #endregion
    }
}