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
    public class SucursalDireccion : Direccion
    {
        public bool ExistsInDatabase { get { return !_IsUserCreated; } }

        private Guid _UidSucursal;

        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }

        public new class Repository : Direccion.Repository
        {
            Direccion.Repository _RepositorioDireccion = new Direccion.Repository();

            public bool InternalSave(SucursalDireccion SucursalDireccion)
            {
                Direccion direccion = SucursalDireccion;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalDireccion_Add";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.AddParameter("@UidSucursal", SucursalDireccion._UidSucursal, SqlDbType.UniqueIdentifier);

                    comando.Parameters.Add("@UidPais", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidPais"].Value = direccion.UidPais;

                    comando.Parameters.Add("@UidEstado", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEstado"].Value = direccion.UidEstado;

                    comando.Parameters.Add("@VchMunicipio", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchMunicipio"].Value = direccion.StrMunicipio;

                    comando.Parameters.Add("@VchCiudad", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchCiudad"].Value = direccion.StrCiudad;

                    comando.Parameters.Add("@VchColonia", SqlDbType.NVarChar, 30);
                    comando.Parameters["@VchColonia"].Value = direccion.StrColonia;

                    comando.Parameters.Add("@VchCalle", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchCalle"].Value = direccion.StrCalle;

                    comando.Parameters.Add("@VchConCalle", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchConCalle"].Value = direccion.StrConCalle;

                    comando.Parameters.Add("@VchYCalle", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchYCalle"].Value = direccion.StrYCalle;

                    comando.Parameters.Add("@VchNoExt", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchNoExt"].Value = direccion.StrNoExt;

                    comando.Parameters.Add("@VchNoInt", SqlDbType.NVarChar, 20);
                    comando.Parameters["@VchNoInt"].Value = direccion.StrNoInt;

                    comando.Parameters.Add("@VchReferencia", SqlDbType.NVarChar, 200);
                    comando.Parameters["@VchReferencia"].Value = direccion.StrReferencia;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot Update a Direccion", e);
                }

            }

            public bool Save(SucursalDireccion SucursalDireccion)
            {
                if (SucursalDireccion._IsUserCreated)
                {
                    SucursalDireccion._IsUserCreated = false;
                    return InternalSave(SucursalDireccion);
                }

                return InternalUpdate(SucursalDireccion);
            }

            public List<SucursalDireccion> FindAll(Guid uid)
            {
                DataTable table = new DataTable();
                List<SucursalDireccion> direcciones = new List<SucursalDireccion>();
                SucursalDireccion direccion = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalDireccion_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        direccion = new SucursalDireccion()
                        {
                            _UidSucursal = uid,
                            _UidDireccion = new Guid(row["UidDireccion"].ToString()),
                            _UidPais = new Guid(row["UidPais"].ToString()),
                            _UidEstado = new Guid(row["UidEstado"].ToString()),
                            _StrMunicipio = row["VchMunicipio"].ToString(),
                            _StrCiudad = row["VchCiudad"].ToString(),
                            _StrColonia = row["VchColonia"].ToString(),
                            _StrCalle = row["VchCalle"].ToString(),
                            _StrConCalle = row["VchConCalle"].ToString(),
                            _StrYCalle = row["VchYCalle"].ToString(),
                            _StrNoExt = row["VchNoExt"].ToString(),
                            _StrNoInt = row["VchNoInt"].ToString(),
                            _StrReferencia = row["VchReferencia"].ToString(),
                            _IsUserCreated = false,
                        };

                        direcciones.Add(direccion);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return direcciones;
            }

            public bool Remove(SucursalDireccion SucursalDireccion)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalDireccion_Remove";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidDireccion", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidDireccion"].Value = SucursalDireccion._UidDireccion;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error removing a Direccion", e);
                }
            }
        }
    }
}