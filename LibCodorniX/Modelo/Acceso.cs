using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    /// <summary>
    /// Clase de control de acceso y permisos del usuario. Permite obtener información necesaria del usuario
    /// para iniciar sesión y acceso granular por módulos.
    /// </summary>
    /// <seealso cref="Usuario"/>
    /// <seealso cref="Modulo"/>
    public class Acceso
    {
        private Guid _UidPerfil;

        /// <summary>
        /// Identificador único de un perfil, sea nivel Backend o Frontend.
        /// </summary>
        public Guid UidPerfil
        {
            get { return _UidPerfil; }
            set { _UidPerfil = value; }
        }

        private Guid _UidModulo;

        /// <summary>
        /// Identificador único de un módulo.
        /// </summary>
        public Guid UidModulo
        {
            get { return _UidModulo; }
            set { _UidModulo = value; }
        }

        /// <summary>
        /// Revisa si el usuario tiene permiso para acceder a un módulo específico.
        /// </summary>
        /// <param name="modulo">Nombre corto del Módulo</param>
        /// <param name="usuario">Identificador único del usuario</param>
        /// <param name="perfil">Identificador único del perfil</param>
        /// <returns>true si el <see cref="Usuario"/> tiene acceso al <see cref="Modulo"/>, en caso contrario retornará false.</returns>
        public static bool TieneAccesoAModulo(string modulo, Guid usuario, Guid perfil)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "usp_Acceso";
            command.CommandType = CommandType.StoredProcedure;

            command.AddParameter("@modulo", modulo, SqlDbType.NVarChar, 100);
            command.AddParameter("@uidPerfil", perfil, SqlDbType.UniqueIdentifier);

            Connection conn = new Connection();

            DataTable table = conn.ExecuteQuery(command);

            if (table.Rows.Count > 0)
                return false;

            command = new SqlCommand();
            command.CommandText = "usp_AccesoUsuario";
            command.CommandType = CommandType.StoredProcedure;

            command.AddParameter("@modulo", modulo, SqlDbType.NVarChar, 100);
            command.AddParameter("@uidUsuario", usuario, SqlDbType.UniqueIdentifier);

            conn = new Connection();

            table = conn.ExecuteQuery(command);

            if (table.Rows.Count > 0)
                return false;

            return true;
        }

        /// <summary>
        /// Obtiene el nivel de acceso (WebApp) que posee un usuario, basado en su perfil activo.
        /// </summary>
        /// <param name="perfil">Identificador único del perfil.</param>
        /// <returns>Alguno de los tres niveles de acceso: "Backsite", "Backend" o "Frontend".</returns>
        public static string ObtenerAppWeb(Guid perfil)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "usp_AppWeb";
            command.CommandType = CommandType.StoredProcedure;

            command.AddParameter("@UidPerfil", perfil, SqlDbType.UniqueIdentifier);

            Connection conn = new Connection();

            DataTable table = conn.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                return row["VchNivelAcceso"].ToString();
            }

            return null;
        }

        /// <summary>
        /// Obtiene el Home (módulo o página de inicio) de un usuario, basado en el perfil.
        /// </summary>
        /// <param name="perfil">Identificador único del perfil.</param>
        /// <returns>URL del módulo o página correspondiente.</returns>
        public static string ObtenerHomePerfil(Guid perfil)
        {
            SqlCommand command = new SqlCommand();
            command.CommandText = "usp_ObtenerHomePerfil";
            command.CommandType = CommandType.StoredProcedure;

            command.AddParameter("@UidPerfil", perfil, SqlDbType.UniqueIdentifier);

            Connection conn = new Connection();

            DataTable table = conn.ExecuteQuery(command);

            foreach (DataRow row in table.Rows)
            {
                if (row.IsNull("VchURL"))
                    break;

                return row["VchURL"].ToString();
            }

            return "Default.aspx";
        }

        /// <summary>
        /// Obtiene los módulos que tiene acceso un perfil en específico. Solo debe utilizarse para el
        /// control de permisos. En caso de comprobar acceso a un módulo, debe usar <see cref="TieneAccesoAModulo(string, Guid, Guid)"/>.
        /// </summary>
        /// <param name="perfil">Identificador único del perfil.</param>
        /// <param name="nivelAcceso"></param>
        /// <returns>Una lista de objetos <see cref="Modulo"/> accesibles por el perfil especificado.</returns>
        public static List<Modulo> ObtenerModulosPorPerfil(Guid perfil, Guid? nivelAcceso = null)
        {
            List<Modulo> modulos = new List<Modulo>();

            SqlCommand command = new SqlCommand();
            
            try
            {
                command.CommandText = "usp_Modulo_FindByPerfil";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidPerfil", perfil, SqlDbType.UniqueIdentifier);

                if (nivelAcceso.HasValue)
                    command.AddParameter("@UidNivelAcceso", nivelAcceso.Value, SqlDbType.UniqueIdentifier);

                DataTable table = new Connection().ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    Modulo modulo = new Modulo()
                    {
                        UidModulo = (Guid)row["UidModulo"],
                        StrModulo = (string)row["VchModulo"],
                        StrURL = (string)row["VchURL"],
                        UidNivelAcceso = (Guid)row["UidNivelAcceso"],
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

        /// <summary>
        /// Obtiene los módulos que tiene acceso un nivel de acceso en específico. Solo debe utilizarse para el
        /// control de permisos. En caso de comprobar acceso a un módulo, debe usar <see cref="TieneAccesoAModulo(string, Guid, Guid)"/>.
        /// </summary>
        /// <param name="nivelAcceso">Identificador único del nivel de acceso (WebApp).</param>
        /// <returns>Una lista de objetos <see cref="Modulo"/> accesibles por el nivel especificado.</returns>
        public static List<Modulo> ObtenerModulosPorNivel(Guid nivelAcceso)
        {
            List<Modulo> modulos = new List<Modulo>();

            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = "usp_Modulo_FindByNivel";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidNivelAcceso", nivelAcceso, SqlDbType.UniqueIdentifier);

                DataTable table = new Connection().ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    Modulo modulo = new Modulo()
                    {
                        UidModulo = (Guid)row["UidModulo"],
                        StrModulo = (string)row["VchModulo"],
                        StrURL = (string)row["VchURL"],
                        UidNivelAcceso = (Guid)row["UidNivelAcceso"],
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

        /// <summary>
        /// Obtiene los módulos que tiene acceso un usuario en específico. Solo debe utilizarse para el
        /// control de permisos. En caso de comprobar acceso a un módulo, debe usar <see cref="TieneAccesoAModulo(string, Guid, Guid)"/>.
        /// </summary>
        /// <param name="nivelAcceso">Identificador único del usuario.</param>
        /// <returns>Una lista de objetos <see cref="Modulo"/> accesibles por el usuario especificado.</returns>
        public static List<Modulo> ObtenerModulosPorUsuario(Guid usuario)
        {
            List<Modulo> modulos = new List<Modulo>();

            SqlCommand command = new SqlCommand();

            try
            {
                command.CommandText = "usp_AccesoUsuario_FindByUsuario";
                command.CommandType = CommandType.StoredProcedure;

                command.AddParameter("@UidUsuario", usuario, SqlDbType.UniqueIdentifier);

                DataTable table = new Connection().ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    Modulo modulo = new Modulo()
                    {
                        UidModulo = (Guid)row["UidModulo"],
                        StrModulo = (string)row["VchModulo"],
                        StrURL = (string)row["VchURL"],
                        UidNivelAcceso = (Guid)row["UidNivelAcceso"],
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

        /// <summary>
        /// Realiza una actualización de los módulos autorizados para cierto usuario. Esta operación
        /// debe realizarse con mucho cuidado, debio a que puede denegar totalmente el acceso a todos los
        /// módulos si la lista de módulos esta vacía.
        /// </summary>
        /// <param name="uidUsuario">Identificador único del usuario.</param>
        /// <param name="modulos">Lista de objetos <see cref="Modulo"/> autorizados.</param>
        /// <returns>true en caso de una actualización completa, en caso de error retorna false.</returns>
        public static bool ActualizarModulosDelUsuario(Guid uidUsuario, List<Guid> modulos)
        {
            Connection conn = new Connection();
            
            conn.StartTransaction();

            try
            {
                // Remove all entries
                SqlCommand removeAll = new SqlCommand();
                removeAll.CommandText = "usp_AccesoUsuario_RemoveAll";
                removeAll.CommandType = CommandType.StoredProcedure;
                removeAll.AddParameter("@UidUsuario", uidUsuario, SqlDbType.UniqueIdentifier);
                conn.ExecuteCommand(removeAll);

                // Add the new deny entries
                foreach (Guid modulo in modulos)
                {
                    SqlCommand addEntry = new SqlCommand();
                    addEntry.CommandText = "usp_AccesoUsuario_AddEntry";
                    addEntry.CommandType = CommandType.StoredProcedure;
                    addEntry.AddParameter("@UidUsuario", uidUsuario, SqlDbType.UniqueIdentifier);
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
    }
}