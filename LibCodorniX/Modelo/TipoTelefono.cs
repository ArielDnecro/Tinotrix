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
    public class TipoTelefono
    {
        private Guid _UidTipoTelefono;

        public Guid UidTipoTelefono
        {
            get { return _UidTipoTelefono; }
            set { _UidTipoTelefono = value; }
        }

        private string _StrTipoTelefono;

        public string StrTipoTelefono
        {
            get { return _StrTipoTelefono; }
            set { _StrTipoTelefono = value; }
        }

        public class Repository
        {
            Connection conn = new Connection();

            public IList<TipoTelefono> FindAll()
            {
                List<TipoTelefono> tipoTelefonos = new List<TipoTelefono>();

                SqlCommand command = new SqlCommand();

                command.CommandText = "usp_TipoTelefono_FindAll";
                command.CommandType = CommandType.StoredProcedure;

                DataTable table = null;

                try
                {
                    table = conn.ExecuteQuery(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching all TipoTelefono", e);
                }

                if (table == null)
                    return null;

                foreach (DataRow row in table.Rows)
                {
                    TipoTelefono tt = new TipoTelefono()
                    {
                        _UidTipoTelefono = (Guid)row["UidTipoTelefono"],
                        _StrTipoTelefono = (string)row["VchTipoTelefono"],
                    };
                    tipoTelefonos.Add(tt);
                }

                return tipoTelefonos;
            }
        }
    }
}