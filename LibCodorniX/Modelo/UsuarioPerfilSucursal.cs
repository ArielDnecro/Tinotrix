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
    public class UsuarioPerfilSucursal
    {
        [NonSerialized]
        Conexion conexion =new  Conexion();
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

        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }

        private string _StrPerfil;

        public string StrPerfil
        {
            get { return _StrPerfil; }
            set { _StrPerfil = value; }
        }

        private string _StrSucursal;

        public string StrSucursal
        {
            get { return _StrSucursal; }
            set { _StrSucursal = value; }
        }

        [NonSerialized]
        private Perfil _Perfil;

        public Perfil Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }
        [NonSerialized]
        private Sucursal _Sucursal;

        public Sucursal Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }


        public bool GuardarUsuarioPerfilSucursal()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandText = "sp_AgregarUsuarioPerfilSucursal";

                Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidUsuario"].Value = UidUsuario;

                Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPerfil"].Value = UidPerfil;

                Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidSucursal"].Value = UidSucursal;

                Resultado = conexion.ManipilacionDeDatos(Comando);

                Comando.Dispose();


            }
            catch (Exception)
            {
                throw;
            }




            return Resultado;
        }


        public bool ModificarUsuarioPerfilSucursal()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "sp_ModificarUsuarioPerfilSucursal";

            Comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidUsuario"].Value = _UidUsuario;

            Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidPerfil"].Value = UidPerfil;

            Comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidSucursal"].Value = UidSucursal;

            Resultado = conexion.ManipilacionDeDatos(Comando);

            return Resultado;
        }
        public class Repository
        {
            Connection conn = new Connection();

            public List<UsuarioPerfilSucursal> FindAll(Guid uidUsuario)
            {
                List<UsuarioPerfilSucursal> SucursalPerfil = new List<UsuarioPerfilSucursal>();
                Sucursal sucursal = null;
                Perfil perfil = null;

                SqlCommand command = new SqlCommand();

                command.CommandText = "sp_UsuarioPerfilSucursal";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidUsuario", uidUsuario, SqlDbType.UniqueIdentifier);

                try
                {
                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        sucursal = new Sucursal()
                        {
                            UidSucursal = (Guid)row["UidSucursal"],
                            UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                            StrNombre = row["VchNombre"].ToString(),
                            RutaImagen = row["VchRutaImagen"].ToString(),
                        };
                        perfil = new Perfil()
                        {
                            strPerfil = row["VchPerfil"].ToString(),
                            UidPerfil = (Guid)row["UidPerfil"]
                        };
                        UsuarioPerfilSucursal ep = new UsuarioPerfilSucursal()
                        {
                            _UidUsuario = uidUsuario,
                            _UidSucursal = sucursal.UidSucursal,
                            _UidPerfil = perfil.UidPerfil,
                            _StrSucursal = sucursal.StrNombre,
                            _StrPerfil = perfil.strPerfil,
                            _Sucursal = sucursal,
                            _Perfil = perfil,
                        };
                        SucursalPerfil.Add(ep);
                    }

                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Empresas from Usuario", e);
                }

                return SucursalPerfil;
            }

            public List<Sucursal> SucursalPerfil(Guid uidPerfil)
            {
                List<Sucursal> sucursales = new List<Sucursal>();
                Sucursal sucursal = null;

                SqlCommand command = new SqlCommand();

                command.CommandText = "sp_PerfilSucursal";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidPerfil", uidPerfil, SqlDbType.UniqueIdentifier);

                try
                {
                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        sucursal = new Sucursal()
                        {

                            UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                            StrNombre = row["VchNombre"].ToString(),
                            StrTipoSucursal = (string)row["VchTipoSucursal"],
                            RutaImagen = row["VchRutaImagen"].ToString(),
                        };
                        sucursales.Add(sucursal);
                    }

                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Empresas from Usuario", e);
                }

                return sucursales;
            }
        }

    }
}