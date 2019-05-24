using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.ConexionDB;
using System.Data.SqlClient;
using System.Data;

namespace CodorniX.Modelo
{
    public class Status
    {
        private Guid _UidStatus;

        public Guid UidStatus
        {
            get { return _UidStatus; }
            set { _UidStatus = value; }
        }
        private string _strStatus;

        public string strStatus
        {
            get { return _strStatus; }
            set { _strStatus = value; }
        }

        public class Repository
        {
            Connection conn = new Connection();

            public List<Status> FindAll()
            {
                List<Status> status = new List<Status>();

                SqlCommand command = new SqlCommand("select * from Estatus");

                DataTable table = conn.ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    Status s = new Status()
                    {
                        _UidStatus = (Guid)row["UidStatus"],
                        _strStatus = (string)row["VchStatus"],
                    };
                    status.Add(s);
                }

                return status;
            }

            public Status Find(Guid uid)
            {
                Status status = null;

                SqlCommand command = new SqlCommand("select * from Estatus WHERE UidStatus = '" + uid.ToString() + "'");

                DataTable table = conn.ExecuteQuery(command);

                foreach (DataRow row in table.Rows)
                {
                    Status s = new Status()
                    {
                        _UidStatus = (Guid)row["UidStatus"],
                        _strStatus = (string)row["VchStatus"],
                    };
                    status = s;
                }

                return status;
            }
        }
    }
}