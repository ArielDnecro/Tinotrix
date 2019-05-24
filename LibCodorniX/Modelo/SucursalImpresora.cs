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
    public class SucursalImpresora 
    {
#region propiedades
        protected bool _ExistsInDatabase;

        protected Guid _UidImpresora;
        public Guid UidImpresora
        {
            get { return _UidImpresora; }
            set { _UidImpresora = value; }
        }


        private Guid _UidSucursal;
        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }


        protected Guid _UidTipoImpresora;
        public Guid UidTipoImpresora
        {
            get { return _UidTipoImpresora; }
            set { _UidTipoImpresora = value; }
        }


        protected Guid _UidStatus;
        public Guid UidStatus
        {
            get { return _UidStatus; }
            set { _UidStatus = value; }
        }


        protected string _StrDescripcion;
        public string StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }


        protected string _StrMarca;
        public string StrMarca
        {
            get { return _StrMarca; }
            set { _StrMarca = value; }
        }


        protected string _StrModelo;
        public string StrModelo
        {
            get { return _StrModelo; }
            set { _StrModelo = value; }
        }


        protected string _StrTipoImpresora;
        public string StrTipoImpresora
        {
            get { return _StrTipoImpresora; }
            set { _StrTipoImpresora = value; }
        }


        protected string _StrStatus;
        public string StrStatus
        {
            get { return _StrStatus; }
            set { _StrStatus = value; }
        }


    #endregion propiedades
        public new class Repository 
        {
            //Comienzo repositorio raiz tomado de suscursaltelefono -> telefono
            protected Connection _Conexion = new Connection();

            //protected bool InternalUpdate(SucursalImpresora impresora)//pendiente
            //{
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandText = "usp_Impresora_Update";
            //        comando.CommandType = CommandType.StoredProcedure;

            //        ////comando.Parameters.Add("@UidTelefono", SqlDbType.UniqueIdentifier); xq modificar uid impresora?
            //        ////comando.Parameters["@UidTelefono"].Value = telefono._UidTelefono;

            //        //comando.Parameters.Add("@UidTipoImpresora", SqlDbType.UniqueIdentifier); 
            //        ///comando.Parameters["@UidTipoImpresora"].Value = impresora._UidTipoImpresora;

            //        //comando.Parameters.Add("@VchTelefono", SqlDbType.NVarChar, 20);
            //        //comando.Parameters["@VchTelefono"].Value = telefono._StrTelefono;

            //        //comando.AddParameter("@UidTipoTelefono", telefono._UidTipoTelefono, SqlDbType.UniqueIdentifier);

            //        return _Conexion.ExecuteCommand(comando);
            //    }
            //    catch (SqlException e)
            //    {
            //        throw new DatabaseException("Cannot update a Telefono", e);
            //    }
            //}

            //public bool Save(SucursalImpresora impresora)
            //{
            //    if (!impresora._ExistsInDatabase)
            //        throw new DatabaseException("Cannot save a impresora in this Repository");

            //    return InternalUpdate(impresora);
            //}

            //public SucursalImpresora Find(Guid uid)
            //{
            //    SucursalImpresora impresora = null;

            //    try
            //    {
            //        SqlCommand command = new SqlCommand();
            //        command.CommandText = "usp_Impresora_Find";
            //        command.CommandType = CommandType.StoredProcedure;

            //        command.Parameters.Add("@UidImpresora", SqlDbType.UniqueIdentifier);
            //        command.Parameters["@UidImpresora"].Value = uid;

            //        DataTable table = _Conexion.ExecuteQuery(command);

            //        foreach (DataRow row in table.Rows)
            //        {
            //            impresora = new SucursalImpresora()
            //            {
            //                _ExistsInDatabase = true,
            //                _UidImpresora = uid,

            //                _UidSucursal = (Guid)row["UidSucursal"],
            //                _UidTipoImpresora = (Guid)row["UidTipoImpresora"],
            //                _UidStatus = (Guid)row["UidStatus"],
            //                _StrMarca = row["VchMarca"].ToString(),
            //                _StrModelo = row["VchModelo"].ToString(),
            //                _StrStatus = row["VchStatus"].ToString(),
            //            };
            //        }
            //    }
            //    catch (SqlException e)
            //    {
            //        throw new DatabaseException("Error finding a Impresora", e);
            //    }

            //    return impresora;
            //}

           

            
            //private bool InternalSave(SucursalImpresora SucursalImpresora)//Lista 27/09/2017
            //{
            //    try
            //    {
            //        SqlCommand command = new SqlCommand();
            //        command.CommandText = "usp_SucursalImpresora_Add";
            //        command.CommandType = CommandType.StoredProcedure;

            //        //command.AddParameter("@UidImpresora", SucursalImpresora._UidSucursal, SqlDbType.UniqueIdentifier);
            //        command.AddParameter("@UidSucursal", SucursalImpresora._UidImpresora, SqlDbType.UniqueIdentifier);
            //        command.AddParameter("@UidTipoImpresora", SucursalImpresora._UidTipoImpresora, SqlDbType.UniqueIdentifier);
            //        command.AddParameter("@UidStatus", SucursalImpresora._UidStatus, SqlDbType.UniqueIdentifier);

            //        command.AddParameter("@VchMarca", SucursalImpresora._StrMarca, SqlDbType.VarChar, 50);
            //        command.AddParameter("@VchModelo", SucursalImpresora._StrModelo, SqlDbType.VarChar, 50);


            //        return _Conexion.ExecuteCommand(command);
            //    }
            //    catch (SqlException e)
            //    {
            //        throw new DatabaseException("Cannot save a Impresora from Sucursal", e);
            //    }
            //}

            public bool Save(SucursalImpresora SucursalImpresora)//Lista 18/10/2017
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    if (SucursalImpresora._ExistsInDatabase)
                    {
                        //return InternalUpdate(SucursalImpresora);
                        comando.CommandText = "usp_Impresora_Update";
                        
                    }
                    else
                    {
                        SucursalImpresora._ExistsInDatabase = true;
                        //return InternalSave(SucursalImpresora);
                        comando.CommandText = "usp_SucursalImpresora_Add";
                    }
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@UidImpresora", SucursalImpresora._UidImpresora, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@UidSucursal", SucursalImpresora._UidSucursal, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@UidTipoImpresora", SucursalImpresora._UidTipoImpresora, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@UidStatus", SucursalImpresora._UidStatus, SqlDbType.UniqueIdentifier);

                    comando.AddParameter("@VchDescripcion", SucursalImpresora._StrDescripcion, SqlDbType.VarChar, 100);
                    comando.AddParameter("@VchMarca", SucursalImpresora._StrMarca, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchModelo", SucursalImpresora._StrModelo, SqlDbType.VarChar, 50);

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Impresora from Sucursal", e);
                }
            }

            public List<SucursalImpresora> FindAll(Guid uid)//Lista 18/10/2017
            {
                DataTable table = new DataTable();
                List<SucursalImpresora> Impresoras = new List<SucursalImpresora>();
                SucursalImpresora SucursalImpresora= null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalImpresora_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        SucursalImpresora = new SucursalImpresora()
                        {
                            _UidSucursal = uid,
                            _UidImpresora = new Guid(row["UidImpresora"].ToString()),
                            _UidTipoImpresora = (Guid)row["UidTipoImpresora"],
                            _UidStatus = (Guid)row["UidStatus"],
                            _StrDescripcion = row["VchDescripcion"].ToString(),
                            _StrMarca = row["VchMarca"].ToString(),
                            _StrModelo= row["VchModelo"].ToString(),
                            _StrStatus = row["VchStatus"].ToString(),
                            _StrTipoImpresora = row["VchTipoImpresora"].ToString(),
                            _ExistsInDatabase = true,
                        };
                        Impresoras.Add(SucursalImpresora);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return Impresoras;
            }
            
            public bool Remove(SucursalImpresora SucursalImpresora)//Lista 27/09/2017
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalIImpresora_Remove";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidImpresora", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidImpresora"].Value = SucursalImpresora._UidImpresora;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error removing a Impresora", e);
                }
            }
        }
    }
}