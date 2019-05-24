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
    public class EmpresaTelefono : Telefono
    {
        public bool ExistsInDatabase { get { return _ExistsInDatabase; } }

        private Guid _UidEmpresa;

        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }

        public new class Repository : Telefono.Repository
        {

            private bool InternalSave(EmpresaTelefono empresaTelefono)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_EmpresaTelefono_Add";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@UidEmpresa", empresaTelefono._UidEmpresa, SqlDbType.UniqueIdentifier);

                    command.AddParameter("@VchTelefono", empresaTelefono._StrTelefono, SqlDbType.NVarChar, 20);

                    command.AddParameter("@UidTipoTelefono", empresaTelefono._UidTipoTelefono, SqlDbType.UniqueIdentifier);

                    return _Conexion.ExecuteCommand(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Telefono from Empresa", e);
                }
            }

            public bool Save(EmpresaTelefono empresaTelefono)
            {
                if (empresaTelefono._ExistsInDatabase)
                    return InternalUpdate(empresaTelefono);
                else
                {
                    empresaTelefono._ExistsInDatabase = true;
                    return InternalSave(empresaTelefono);
                }
            }

            public List<EmpresaTelefono> FindAll(Guid uid)
            {
                DataTable table = new DataTable();
                List<EmpresaTelefono> telefonos = new List<EmpresaTelefono>();
                EmpresaTelefono empresaTelefono = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_EmpresaTelefono_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        empresaTelefono = new EmpresaTelefono()
                        {
                            _UidEmpresa = uid,
                            _UidTelefono = new Guid(row["UidTelefono"].ToString()),
                            _StrTelefono = row["VchTelefono"].ToString(),
                            _UidTipoTelefono = (Guid)row["UidTipoTelefono"],
                            _StrTipoTelefono = (string)row["VchTipoTelefono"],
                            _ExistsInDatabase = true,
                        };

                        telefonos.Add(empresaTelefono);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return telefonos;
            }

            public bool Remove(EmpresaTelefono empresaTelefono)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_EmpresaTelefono_Remove";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidTelefono"].Value = empresaTelefono._UidTelefono;

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