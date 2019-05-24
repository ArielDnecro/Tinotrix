using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    public class Estado
    {
        private Guid _UidEstado;

        public Guid UidEstado
        {
            get { return _UidEstado; }
            set { _UidEstado = value; }
        }


        private Guid _UidPais;

        public Guid UidPais
        {
            get { return _UidPais; }
            set { _UidPais = value; }
        }

        private string _StrNombre;

        public string StrNombre
        {
            get { return _StrNombre; }
            set { _StrNombre = value; }
        }

        public class Repository
        {
            Connection connection = new Connection();

            public List<Estado> FindAll(Guid uidPais)
            {
                List<Estado> estados = new List<Estado>();

                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_Estado_FindAll";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@UidPais", uidPais, SqlDbType.UniqueIdentifier);

                    DataTable table = connection.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Estado estado = new Estado()
                        {
                            _UidEstado = (Guid)row["UidEstado"],
                            _UidPais = (Guid)row["UidPais"],
                            _StrNombre = row["VchNombre"].ToString(),
                        };

                        estados.Add(estado);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot fetch Pais", e);
                }

                return estados;
            }
        }
    }
}