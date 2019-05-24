using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    public class Tipo
    {
        private Guid _UidTipo;

        public Guid UidTipo
        {
            get { return _UidTipo; }
        }

        private string _StrTipo;

        public string StrTipo
        {
            get { return _StrTipo; }
            set { _StrTipo = value; }
        }

        public class Repository
        {
            Connection conn = new Connection();

            public IList<Tipo> FindAll()
            {
                List<Tipo> tipos = new List<Tipo>();

                SqlCommand command = new SqlCommand();
                try
                {
                    command.CommandText = "usp_Tipo_FindAll";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Tipo tipo = new Tipo()
                        {
                            _UidTipo = (Guid)row["UidTipo"],
                            _StrTipo = row["VchTipo"].ToString(),
                        };
                        tipos.Add(tipo);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot get Tipos", e);
                }

                return tipos;
            }
        }
    }
}