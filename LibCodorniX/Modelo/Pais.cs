using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    public class Pais
    {
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

            public List<Pais> FindAll()
            {
                List<Pais> paises = new List<Pais>();
                
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_Pais_FindAll";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = connection.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        Pais pais = new Pais()
                        {
                            _UidPais = (Guid)row["UidPais"],
                            _StrNombre = row["VchNombre"].ToString(),
                        };

                        paises.Add(pais);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot fetch Pais", e);
                }

                return paises;
            }
        }
    }
}