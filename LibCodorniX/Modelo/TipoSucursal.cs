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
    public class TipoSucursal
    {
        private Guid _UidTipoSucursal;

        public Guid UidTipoSucursal
        {
            get { return _UidTipoSucursal; }
            set { _UidTipoSucursal = value; }
        }

        private string _StrTipoSucursal;

        public string StrTipoSucursal
        {
            get { return _StrTipoSucursal; }
            set { _StrTipoSucursal = value; }
        }

        public class Repository
        {
            Connection conn = new Connection();

            public List<TipoSucursal> FindAll()
            {
                List<TipoSucursal> tipos = new List<TipoSucursal>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "usp_TipoSucursal_FindAll";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = conn.ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        TipoSucursal tipoSucursal = new TipoSucursal()
                        {
                            _UidTipoSucursal = (Guid)row["UidTipoSucursal"],
                            _StrTipoSucursal = (string)row["VchTipoSucursal"],
                        };
                        tipos.Add(tipoSucursal);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot get TipoSucursal", e);
                }

                return tipos;
            }
        }
    }
}