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
    public class Modulo
    {
        private Guid _UidModulo;

        public Guid UidModulo
        {
            get { return _UidModulo; }
            set { _UidModulo = value; }
        }

        private string _StrModulo;

        public string StrModulo
        {
            get { return _StrModulo; }
            set { _StrModulo = value; }
        }
        private int myVar;

        public int MyProperty
        {
            get { return myVar; }
            set { myVar = value; }
        }

        private string _StrURL;

        public string StrURL
        {
            get { return _StrURL; }
            set { _StrURL = value; }
        }

        private Guid _UidNivelAcceso;

        public Guid UidNivelAcceso
        {
            get { return _UidNivelAcceso; }
            set { _UidNivelAcceso = value; }
        }

        private string _StrUrl;

        public string StrUrl
        {
            get { return _StrUrl; }
            set { _StrUrl = value; }
        }

        private string _StrNivelAcceso;

        public string StrNivelAcceso
        {
            get { return _StrNivelAcceso; }
            set { _StrNivelAcceso = value; }
        }


        public static bool ActualizarModulos(Guid UidPerfil, List<Guid> modulos)
        {
            Connection conn = new Connection();

            conn.StartTransaction();

            try
            {
                SqlCommand removeAll = new SqlCommand();
                removeAll.CommandText = "sp_PerfilModulos";
                removeAll.CommandType = CommandType.StoredProcedure;
                removeAll.AddParameter("@UidPerfil", UidPerfil, SqlDbType.UniqueIdentifier);
                conn.ExecuteCommand(removeAll);
                foreach (Guid modulo in modulos)
                {
                    SqlCommand addEntry = new SqlCommand();
                    addEntry.CommandText = "sp_AgregarNivelAccesoModulo";
                    addEntry.CommandType = CommandType.StoredProcedure;
                    addEntry.AddParameter("@UidPerfil", UidPerfil, SqlDbType.UniqueIdentifier);
                    addEntry.AddParameter("@UidModulo", modulo, SqlDbType.UniqueIdentifier);
                    conn.ExecuteCommand(addEntry);
                }
            }
            catch (SqlException e)
            {
                conn.CancelTransaction();
                throw new DatabaseException("Error changing Acceso", e);
            }

            conn.FinishTransaction();

            return true;
        }
        public class Repositorio
        {
            Conexion Conexion = new Conexion();

            public IList<Modulo> ConsultarModulos()
            {
                List<Modulo> Modulos = new List<Modulo>();
                SqlCommand comando = new SqlCommand();
                comando.CommandText = "sp_ConsultarModulo";
                comando.CommandType = CommandType.StoredProcedure;
                DataTable table = null;
                try
                {
                    table = Conexion.Busquedas(comando);
                }
                catch (Exception)
                {

                    throw;
                }
                if (table == null)
                {
                    return null;
                }
                foreach (DataRow row in table.Rows)
                {
                    Modulo modulo = new Modulo()
                    {
                        _UidModulo = (Guid)row["UidModulo"],
                        _StrModulo = (string)row["VchModulo"],
                        _StrURL = (string)row["VchURL"]
                    };
                    Modulos.Add(modulo);
                }
                return Modulos;
            }

            public List<Modulo> CargarTodosLosModulos(string Acceso)
            {
                List<Modulo> modulos = new List<Modulo>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_ModuloPorAcceso";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@VchNivelAcceso", Acceso, SqlDbType.NVarChar, 50);

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Modulo modulo = new Modulo()
                        {
                            UidModulo = (Guid)row["UidModulo"],
                            StrModulo = (string)row["VchModulo"],
                            StrURL = (string)row["VchUrl"],
                        };
                        modulos.Add(modulo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return modulos;


            }

            public List<Modulo> CargarHome()
            {
                List<Modulo> modulos = new List<Modulo>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_CargarHome";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Modulo modulo = new Modulo()
                        {
                            UidModulo = (Guid)row["UidModulo"],
                            StrModulo = (string)row["VchModulo"],
                            StrURL = (string)row["VchUrl"],
                        };
                        modulos.Add(modulo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return modulos;


            }

            public List<Modulo> CargarModulos(Guid uidperfil)
            {
                List<Modulo> modulos = new List<Modulo>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_ConsultarModulo";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                    command.Parameters["@UidPerfil"].Value = uidperfil;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Modulo modulo = new Modulo()
                        {
                            UidModulo = (Guid)row["UidModulo"],
                            StrModulo = (string)row["VchModulo"],
                            StrURL = (string)row["VchURL"],
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"]
                        };
                        modulos.Add(modulo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return modulos;
            }

            public List<Modulo> CargarTodoslosModulos()
            {
                List<Modulo> modulos = new List<Modulo>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_ConsultarTodoslosModulos";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Modulo modulo = new Modulo()
                        {
                            UidModulo = (Guid)row["UidModulo"],
                            StrModulo = (string)row["VchModulo"],
                            StrURL = (string)row["VchURL"],
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"]

                        };
                        modulos.Add(modulo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return modulos;
            }

            public List<Modulo> CargarTodoslosModulosBackend()
            {
                List<Modulo> modulos = new List<Modulo>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_CargarTodoslosModulosBackend";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Modulo modulo = new Modulo()
                        {
                            UidModulo = (Guid)row["UidModulo"],
                            StrModulo = (string)row["VchModulo"],
                            StrURL = (string)row["VchURL"],
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"]

                        };
                        modulos.Add(modulo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return modulos;
            }

            public List<Modulo> CargarModulosBackend(Guid uidperfil)
            {
                List<Modulo> modulos = new List<Modulo>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_ConsultarModuloBackend";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UidPerfil", SqlDbType.UniqueIdentifier);
                    command.Parameters["@UidPerfil"].Value = uidperfil;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Modulo modulo = new Modulo()
                        {
                            UidModulo = (Guid)row["UidModulo"],
                            StrModulo = (string)row["VchModulo"],
                            StrURL = (string)row["VchURL"],
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"]
                        };
                        modulos.Add(modulo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return modulos;
            }
        }
    }
}