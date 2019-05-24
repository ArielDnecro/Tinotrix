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
    public class Perfil
    {
        [NonSerialized]
        Conexion conexion = new Conexion();

        private Guid _UidPerfil;

        public Guid UidPerfil
        {
            get { return _UidPerfil; }
            set { _UidPerfil = value; }
        }


        private string _strPerfil;
        public string strPerfil
        {
            get { return _strPerfil; }
            set { _strPerfil = value; }
        }

        private Guid _UidNivelAcceso;

        public Guid UidNivelAcceso
        {
            get { return _UidNivelAcceso; }
            set { _UidNivelAcceso = value; }
        }

        private Guid _UidHome;

        public Guid UidHome
        {
            get { return _UidHome; }
            set { _UidHome = value; }
        }

        private Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }


        private string _StrNivelAcceso;

        public string StrNivelAcceso
        {
            get { return _StrNivelAcceso; }
            set { _StrNivelAcceso = value; }
        }

        private string _StrHome;

        public string strHome
        {
            get { return _StrHome; }
            set { _StrHome = value; }
        }

        public bool GuardarDatos()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandText = "sp_InsertarPerfil";

                Comando.Parameters.Add("@VchPerfil", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchPerfil"].Value = strPerfil;

                Comando.Parameters.Add("@UidNivelAcceso", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidNivelAcceso"].Value = UidNivelAcceso;

                Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;


                Comando.Parameters.Add("@UidHome", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidHome"].Value = UidHome;

                Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPerfil"].Direction = ParameterDirection.Output;

                Resultado = conexion.ManipilacionDeDatos(Comando);
                _UidPerfil = (Guid)Comando.Parameters["@UidPerfil"].Value;
                Comando.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return Resultado;
        }

        public bool GuardarDatossinempresa()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            try
            {
                Comando.CommandType = CommandType.StoredProcedure;

                Comando.CommandText = "sp_InsertarPerfilsinempresa";

                Comando.Parameters.Add("@VchPerfil", SqlDbType.NVarChar, 50);
                Comando.Parameters["@VchPerfil"].Value = strPerfil;

                Comando.Parameters.Add("@UidNivelAcceso", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidNivelAcceso"].Value = UidNivelAcceso;

                Comando.Parameters.Add("@UidHome", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidHome"].Value = UidHome;

                Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                Comando.Parameters["@UidPerfil"].Direction = ParameterDirection.Output;

                Resultado = conexion.ManipilacionDeDatos(Comando);
                _UidPerfil = (Guid)Comando.Parameters["@UidPerfil"].Value;
                Comando.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return Resultado;
        }

        public bool ModificarDatosconempresa()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "sp_ModificarPerfil";

            Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidPerfil"].Value = _UidPerfil;

            Comando.Parameters.Add("@VchPerfil", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchPerfil"].Value = strPerfil;

            Comando.Parameters.Add("@UidNivelAcceso", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidNivelAcceso"].Value = UidNivelAcceso;

            Comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidEmpresa"].Value = UidEmpresa;


            Comando.Parameters.Add("@UidHome", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidHome"].Value = UidHome;

            Resultado = conexion.ManipilacionDeDatos(Comando);

            return Resultado;
        }

        public bool ModificarDatos()
        {

            bool Resultado = false;
            SqlCommand Comando = new SqlCommand();

            Comando.CommandType = CommandType.StoredProcedure;
            Comando.CommandText = "sp_ModificarPerfiles";

            Comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidPerfil"].Value = _UidPerfil;

            Comando.Parameters.Add("@VchPerfil", SqlDbType.NVarChar, 50);
            Comando.Parameters["@VchPerfil"].Value = strPerfil;

            Comando.Parameters.Add("@UidNivelAcceso", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidNivelAcceso"].Value = UidNivelAcceso;

            Comando.Parameters.Add("@UidHome", SqlDbType.UniqueIdentifier);
            Comando.Parameters["@UidHome"].Value = UidHome;

            Resultado = conexion.ManipilacionDeDatos(Comando);

            return Resultado;
        }

        public class Repositorio
        {
            Conexion Conexion = new Conexion();

           

            public Perfil CargarDatos(Guid uidPerfil)
            {
                Perfil perfil = null;

                DataTable table = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "sp_ConsultarPerfil";
                comando.CommandType = CommandType.StoredProcedure;

                comando.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                comando.Parameters["@UidPerfil"].Value = uidPerfil;

                table = Conexion.Busquedas(comando);

                foreach (DataRow row in table.Rows)
                {
                    perfil = new Perfil()
                    {
                        _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                        _strPerfil = row["VchPerfil"].ToString(),
                        _UidNivelAcceso= new Guid(row["UidNivelAcceso"].ToString()),
                        _UidHome= new Guid(row["UidHome"].ToString()),
                        _StrHome= row["VchHome"].ToString(),
                        //_StrNivelAcceso= row["VchNivelAcceso"].ToString()
                    };
                }

                return perfil;
            }

            public List<Perfil> CargarTodosLosPerfiles()
            {
                List<Perfil> perfiles = new List<Perfil>();
                Perfil perfil = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "sp_Perfiles";
                    comando.CommandType = CommandType.StoredProcedure;

                    DataTable table = Conexion.Busquedas(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        perfil = new Perfil()
                        {
                            _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                            _strPerfil = row["VchPerfil"].ToString(),
                            _UidNivelAcceso= new Guid(row["UidNivelAcceso"].ToString()),
                            _StrNivelAcceso= row["VchNivelAcceso"].ToString()
                        };
                        perfiles.Add(perfil);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load Empresas", e);
                }

                return perfiles;
            }

            public List<Perfil> CargarTodosLosPerfilesporempresa(Guid UidEmpresa)
            {
                List<Perfil> perfiles = new List<Perfil>();
                Perfil perfil = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "PerfilesEmpresa";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = UidEmpresa;

                    DataTable table = Conexion.Busquedas(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        perfil = new Perfil()
                        {
                            _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                            _strPerfil = row["VchPerfil"].ToString(),
                        };
                        perfiles.Add(perfil);
                    }
                }
                catch (SqlException e)
                {

                    throw new DatabaseException("Cannot load Empresas", e);
                }

                return perfiles;
            }

            public List<Perfil> CargarPerfilPorNivel()
            {
                List<Perfil> perfiles = new List<Perfil>();
                Perfil perfil = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    //comando.CommandText = "sp_PerfilBackside";
                   comando.CommandText = "sp_Perfiles";
                    comando.CommandType = CommandType.StoredProcedure;

                    DataTable table = Conexion.Busquedas(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        perfil = new Perfil()
                        {
                            _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                            _strPerfil = row["VchPerfil"].ToString(),
                            _UidNivelAcceso= new Guid(row["UidNivelAcceso"].ToString()),
                            _StrNivelAcceso= row["VchNivelAcceso"].ToString()
                        };
                        perfiles.Add(perfil);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load Empresas", e);
                }

                return perfiles;
            }

            public List<Perfil> CargarPerfilPorEmpresa(Guid UidEmpresa)
            {
                List<Perfil> perfiles = new List<Perfil>();
                Perfil perfil = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "sp_PerfilPorEmpresa";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = UidEmpresa;
                    DataTable table = Conexion.Busquedas(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        perfil = new Perfil()
                        {
                            _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                            _strPerfil = row["VchPerfil"].ToString(),
                        };
                        perfiles.Add(perfil);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load Empresas", e);
                }

                return perfiles;
            }

            public List<Perfil> buscar(Criterio criterio)
            {
                List<Perfil> perfiles = new List<Perfil>();
                Perfil perfil = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "sp_Perfiles";
                comando.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrWhiteSpace(criterio.perfil))
                {
                    comando.Parameters.Add("@VchPerfil", SqlDbType.NVarChar, 40);
                    comando.Parameters["@VchPerfil"].Value = criterio.perfil;
                }
                if (criterio.nivelacceso != string.Empty)
                {
                    comando.Parameters.Add("@UidNivelAcceso", SqlDbType.NVarChar, 4000);
                    comando.Parameters["@UidNivelAcceso"].Value = criterio.nivelacceso;
                }
                DataTable table = Conexion.Busquedas(comando);

                foreach (DataRow row in table.Rows)
                {
                    perfil = new Perfil() 
                    {
                        _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                        _strPerfil = row["VchPerfil"].ToString(),
                        _UidNivelAcceso = new Guid(row["UidNivelAcceso"].ToString()),
                        _StrNivelAcceso = row["VchNivelAcceso"].ToString()

                    };
                    perfiles.Add(perfil);
                }

                return perfiles;
            }

            public List<Perfil> buscarporempresa(Criterio criterio)
            {
                List<Perfil> perfiles = new List<Perfil>();
                Perfil perfil = null;

                SqlCommand comando = new SqlCommand();
                comando.CommandText = "PerfilesEmpresa";
                comando.CommandType = CommandType.StoredProcedure;

                if (!string.IsNullOrWhiteSpace(criterio.perfil))
                {
                    comando.Parameters.Add("@VchPerfil", SqlDbType.NVarChar, 40);
                    comando.Parameters["@VchPerfil"].Value = criterio.perfil;
                }

                if (criterio.UidEmpesa!=null)
                {
                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = criterio.UidEmpesa;
                }

                DataTable table = Conexion.Busquedas(comando);

                foreach (DataRow row in table.Rows)
                {
                    perfil = new Perfil()
                    {
                        _UidPerfil = new Guid(row["UidPerfil"].ToString()),
                        _strPerfil = row["VchPerfil"].ToString(),
                    };
                    perfiles.Add(perfil);
                }

                return perfiles;
            }
        }
        
        public class Criterio
        {
            public string perfil { get; set; }
            public Guid UidEmpesa { get; set; }
            public string nivelacceso { get; set; }
        }
    }
}