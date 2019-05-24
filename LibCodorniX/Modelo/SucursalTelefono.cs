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
    public class SucursalTelefono : Telefono
    {
        #region propiedades
        public bool ExistsInDatabase { get { return _ExistsInDatabase; } }

        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }
        #endregion propiedades
        public new class Repository : Telefono.Repository
        {

            private bool InternalSave(SucursalTelefono SucursalTelefono)
            {
                try
                {
                    SqlCommand command = new SqlCommand();
                    command.CommandText = "usp_SucursalTelefono_Add";
                    command.CommandType = CommandType.StoredProcedure;

                    command.AddParameter("@UidSucursal", SucursalTelefono._UidSucursal, SqlDbType.UniqueIdentifier);

                    command.AddParameter("@VchTelefono", SucursalTelefono._StrTelefono, SqlDbType.NVarChar, 20);

                    command.AddParameter("@UidTipoTelefono", SucursalTelefono._UidTipoTelefono, SqlDbType.UniqueIdentifier);

                    return _Conexion.ExecuteCommand(command);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Telefono from Sucursal", e);
                }
            }

            public bool Save(SucursalTelefono SucursalTelefono)
            {
                if (SucursalTelefono._ExistsInDatabase)
                    return InternalUpdate(SucursalTelefono);
                else
                {
                    SucursalTelefono._ExistsInDatabase = true;
                    return InternalSave(SucursalTelefono);
                }
            }

            public List<SucursalTelefono> FindAll(Guid uid)
            {
                DataTable table = new DataTable();
                List<SucursalTelefono> telefonos = new List<SucursalTelefono>();
                SucursalTelefono SucursalTelefono = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalTelefono_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        SucursalTelefono = new SucursalTelefono()
                        {
                            _UidSucursal = uid,
                            _UidTelefono = new Guid(row["UidTelefono"].ToString()),
                            _StrTelefono = row["VchTelefono"].ToString(),
                            _UidTipoTelefono = (Guid)row["UidTipoTelefono"],
                            _StrTipoTelefono = (string)row["VchTipoTelefono"],
                            _ExistsInDatabase = true,
                        };

                        telefonos.Add(SucursalTelefono);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return telefonos;
            }

            public bool Remove(SucursalTelefono SucursalTelefono)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalTelefono_Remove";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidTelefono"].Value = SucursalTelefono._UidTelefono;

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