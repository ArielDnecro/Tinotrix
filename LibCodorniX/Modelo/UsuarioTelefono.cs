using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CodorniX.ConexionDB;
using CodorniX.Util;

namespace CodorniX.Modelo
{
    [Serializable]
    public class UsuarioTelefono : Telefono
    {

        public bool ExistsInDatabase { get { return _ExistsInDatabase; } }

        private Guid _UidUsuario;

        public Guid UidUsuario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }

        public new class Repository : Telefono.Repository
        {

            private bool InternalSave(UsuarioTelefono UsuarioTelefono)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_UsuarioTelefono_Add";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@UidUsuario", UsuarioTelefono._UidUsuario, SqlDbType.UniqueIdentifier);

                    command.AddParameter("@VchTelefono", UsuarioTelefono._StrTelefono, SqlDbType.NVarChar, 20);

                    command.AddParameter("@UidTipoTelefono", UsuarioTelefono._UidTipoTelefono, SqlDbType.UniqueIdentifier);

                    return _Conexion.ExecuteCommand(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Telefono from Sucursal", e);
                }
            }

            public bool Save(UsuarioTelefono UsuarioTelefono)
            {
                if (UsuarioTelefono._ExistsInDatabase)
                    return InternalUpdate(UsuarioTelefono);
                else
                {
                    UsuarioTelefono._ExistsInDatabase = true;
                    return InternalSave(UsuarioTelefono);
                }
            }

            public List<UsuarioTelefono> FindAll(Guid uid)
            {
                DataTable table = new DataTable();
                List<UsuarioTelefono> telefonos = new List<UsuarioTelefono>();
                UsuarioTelefono UsuarioTelefono = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_UsuarioTelefono_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidUsuario"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        UsuarioTelefono = new UsuarioTelefono()
                        {
                            _UidUsuario = uid,
                            _UidTelefono = new Guid(row["UidTelefono"].ToString()),
                            _StrTelefono = row["VchTelefono"].ToString(),
                            _UidTipoTelefono = (Guid)row["UidTipoTelefono"],
                            _StrTipoTelefono = (string)row["VchTipoTelefono"],
                            _ExistsInDatabase = true,
                        };

                        telefonos.Add(UsuarioTelefono);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return telefonos;
            }

            public bool Remove(UsuarioTelefono UsuarioTelefono)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_UsuarioTelefono_Remove";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidTelefono"].Value = UsuarioTelefono._UidTelefono;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error removing a Telefono", e);
                }
            }
        }
    }
}