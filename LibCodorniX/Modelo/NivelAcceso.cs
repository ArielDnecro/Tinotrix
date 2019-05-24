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
    [Serializable]
    public class NivelAcceso
    {
       
        private Guid _UidNivelAcceso;

        public Guid UidNivelAcceso
        {
            get { return _UidNivelAcceso; }
            set { _UidNivelAcceso = value; }
        }

        private string _StrNivelAcceso;

        public string StrNivelAcceso
        {
            get { return _StrNivelAcceso; }
            set { _StrNivelAcceso = value; }
        }

        public class Repositorio
        {
            public List<NivelAcceso> CargarNivel()
            {
                List<NivelAcceso> Niveles = new List<NivelAcceso>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "sp_ConsultarNivelAcceso";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        NivelAcceso nivel = new NivelAcceso()
                        {
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"],
                            StrNivelAcceso = (string)row["VchNivelAcceso"]
                        };
                        Niveles.Add(nivel);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return Niveles;
            }

            public List<NivelAcceso> FindAll()
            {
                List<NivelAcceso> Niveles = new List<NivelAcceso>();

                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "usp_NivelAcceso_FindAll";
                    command.CommandType = CommandType.StoredProcedure;

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        NivelAcceso nivel = new NivelAcceso()
                        {
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"],
                            StrNivelAcceso = (string)row["VchNivelAcceso"]
                        };
                        Niveles.Add(nivel);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching Modulos", e);
                }

                return Niveles;
            }

            public NivelAcceso Find(Guid uid)
            {
                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "usp_NivelAcceso_Find";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@UidNivelAcceso", uid, SqlDbType.UniqueIdentifier);

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        NivelAcceso nivel = new NivelAcceso()
                        {
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"],
                            StrNivelAcceso = (string)row["VchNivelAcceso"]
                        };
                        return nivel;
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching NivelAcceso", e);
                }

                return null;
            }

            public NivelAcceso FindByName(string name)
            {
                SqlCommand command = new SqlCommand();

                try
                {
                    command.CommandText = "usp_NivelAcceso_FindByName";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@VchNivelAcceso", name, SqlDbType.NVarChar, 50);

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        NivelAcceso nivel = new NivelAcceso()
                        {
                            UidNivelAcceso = (Guid)row["UidNivelAcceso"],
                            StrNivelAcceso = (string)row["VchNivelAcceso"]
                        };
                        return nivel;
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Error fetching NivelAcceso", e);
                }

                return null;
            }
        }
    }
}