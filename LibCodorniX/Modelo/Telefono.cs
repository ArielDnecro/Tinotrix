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
    public class Telefono
    {
        #region propiedades
        protected bool _ExistsInDatabase;

        protected Guid _UidTelefono;

        public Guid UidTelefono
        {
            get { return _UidTelefono; }
            set { _UidTelefono = value; }
        }

        protected string _StrTelefono;

        public string StrTelefono
        {
            get { return _StrTelefono; }
            set { _StrTelefono = value; }
        }

        protected Guid _UidTipoTelefono;

        public Guid UidTipoTelefono
        {
            get { return _UidTipoTelefono; }
            set { _UidTipoTelefono = value; }
        }

        protected string _StrTipoTelefono;

        public string StrTipoTelefono
        {
            get { return _StrTipoTelefono; }
            set { _StrTipoTelefono = value; }
        }
        #endregion propiedades

        
        public class Repository
        {
            protected Connection _Conexion = new Connection();

            protected bool InternalUpdate(Telefono telefono)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Telefono_Update";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidTelefono"].Value = telefono._UidTelefono;

                    comando.Parameters.Add("@VchTelefono", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchTelefono"].Value = telefono._StrTelefono;

                    comando.AddParameter("@UidTipoTelefono", telefono._UidTipoTelefono, SqlDbType.UniqueIdentifier);

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update a Telefono", e);
                }
            }

            public bool Save(Telefono telefono)
            {
                if (!telefono._ExistsInDatabase)
                    throw new DatabaseException("Cannot save a Telefono in this Repository");

                return InternalUpdate(telefono);
            }

            public Telefono Find(Guid uid)
            {
                Telefono telefono = null;

                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_Telefono_Find";
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                    command.Parameters["@UidTelefono"].Value = uid;

                    DataTable table = _Conexion.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        telefono = new Telefono()
                        {
                            _ExistsInDatabase = true,
                            _UidTelefono = uid,
                            _StrTelefono = row["VchTelefono"].ToString(),
                            _UidTipoTelefono = (Guid)row["UidTipoTelefono"],
                            _StrTipoTelefono = (string)row["VchTipoTelefono"],
                        };
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error finding a Telefono", e);
                }

                return telefono;
            }
        }
        
    }
}