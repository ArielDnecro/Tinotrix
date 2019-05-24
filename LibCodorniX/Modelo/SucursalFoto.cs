using System;
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
   
    public class SucursalFoto
    {
        #region propiedades
        protected bool _ExistsInDatabase;

        protected Guid _UidFoto;
        public Guid UidFoto
        {
            get { return _UidFoto; }
            set { _UidFoto = value; }
        }


        private Guid _UidSucursal;
        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }


        protected string _StrDescripcion;
        public string StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }


        protected string _StrPrecio;
        public string StrPrecio
        {
            get { return _StrPrecio; }
            set { _StrPrecio = value; }
        }


        protected Guid _UidStatus;
        public Guid UidStatus
        {
            get { return _UidStatus; }
            set { _UidStatus = value; }
        }

        protected Guid _UidMedida;
        public Guid UidMedida
        {
            get { return _UidMedida; }
            set { _UidMedida = value; }
        }

        protected string _StrStatus;
        public string StrStatus
        {
            get { return _StrStatus; }
            set { _StrStatus = value; }
        }


        protected string _VchAlto;
        public string VchAlto
        {
            get { return _VchAlto; }
            set { _VchAlto = value; }
        }


        protected string _VchAncho;
        public string VchAncho
        {
            get { return _VchAncho; }
            set { _VchAncho = value; }
        }

        protected string _VchMedida;
        
        public string VchMedida
        {
            get { return _VchMedida; }
            set { _VchMedida = value; }
        }

        protected Guid _UidImpresora;
        public Guid UidImpresora
        {
            get { return _UidImpresora; }
            set { _UidImpresora = value; }
        }

        protected string _VchFila;

        public string VchFila
        {
            get { return _VchFila; }
            set { _VchFila = value; }
        }

        protected string _VchColumna;

        public string VchColumna
        {
            get { return _VchColumna; }
            set { _VchColumna = value; }
        }

        protected bool _BooRotarEnPapel;

        public bool BooRotarEnPapel
        {
            get { return _BooRotarEnPapel; }
            set { _BooRotarEnPapel = value; }
        }
        #endregion propiedades
        public new class Repository
        {
            //Comienzo repositorio raiz tomado de suscursaltelefono -> telefono
            protected Connection _Conexion = new Connection();

            //protected bool InternalUpdate(SucursalFoto foto)//pendiente
            //{
            //    try
            //    {
            //        SqlCommand comando = new SqlCommand();
            //        comando.CommandText = "usp_Foto_Update";
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

            //public SucursalFoto Find(Guid uid)
            //{
            //    SucursalFoto foto = null;

            //    try
            //    {
            //        SqlCommand command = new SqlCommand();
            //        command.CommandText = "usp_Foto_Find";
            //        command.CommandType = CommandType.StoredProcedure;

            //        command.Parameters.Add("@UidFoto", SqlDbType.UniqueIdentifier);
            //        command.Parameters["@UidFoto"].Value = uid;

            //        DataTable table = _Conexion.ExecuteQuery(command);

            //        foreach (DataRow row in table.Rows)
            //        {
            //            foto = new SucursalFoto()
            //            {
            //                _ExistsInDatabase = true,
            //                _UidFoto = uid,

            //                _UidSucursal = (Guid)row["UidSucursal"],
            //                _StrDescripcion= row["VchDescripcion"].ToString(),
            //                _StrPrecio = row["VchPrecio"].ToString(),
            //                _UidStatus = (Guid)row["UidStatus"],
            //                _StrStatus = row["VchStatus"].ToString(),

            //                _IntAlto= int.Parse(row["IntAlto"].ToString()),
            //                _IntAncho = int.Parse(row["IntAncho"].ToString())
            //            };
            //        }
            //    }
            //    catch (SqlException e)
            //    {
            //        throw new DatabaseException("Error finding a Impresora", e);
            //    }

            //    return foto;
            //}

            //Final repositorio raiz tomado de suscursaltelefono -> telefono
            //private bool InternalSave(SucursalFoto SucursalFoto)//Lista 04/10/2017
            //{
            //    try
            //    {
            //        SqlCommand command = new SqlCommand();
            //        command.CommandText = "usp_SucursalFoto_Add";
            //        command.CommandType = CommandType.StoredProcedure;

            //        //command.AddParameter("@UidFoto", SucursalFoto._UidFoto, SqlDbType.UniqueIdentifier);
            //        command.AddParameter("@UidSucursal", SucursalFoto._UidSucursal, SqlDbType.UniqueIdentifier);
            //        command.AddParameter("@UidStatus", SucursalFoto._UidStatus, SqlDbType.UniqueIdentifier);
            //        //No se le agrega vchstatus porque ya lo tiene en un catalogo en la bd solo se crea la referencia uidStatus

            //        command.AddParameter("@VchDescripcion", SucursalFoto._StrDescripcion, SqlDbType.VarChar, 50);
            //        command.AddParameter("@VchPrecio", SucursalFoto._StrPrecio, SqlDbType.VarChar, 50);
            //        command.AddParameter("@IntAlto", SucursalFoto._IntAlto, SqlDbType.Int);
            //        command.AddParameter("@IntAncho", SucursalFoto._IntAncho, SqlDbType.Int);
                     
            //        return _Conexion.ExecuteCommand(command);
            //    }
            //    catch (SqlException e)
            //    {
            //        throw new DatabaseException("Cannot save a Impresora from Sucursal", e);
            //    }
            //}

            public bool Save(SucursalFoto SucursalFoto)//Lista 18/10/2017
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                if (SucursalFoto._ExistsInDatabase)
                {
                    //return InternalUpdate(SucursalFoto);
                    comando.CommandText = "usp_Foto_Update";
                    comando.AddParameter("@UidFoto", SucursalFoto._UidFoto, SqlDbType.UniqueIdentifier);
                }
                else
                {
                    SucursalFoto._ExistsInDatabase = true;
                    //return InternalSave(SucursalFoto);
                    comando.CommandText = "usp_SucursalFoto_Add";
                }
                comando.CommandType = CommandType.StoredProcedure;
                comando.AddParameter("@UidSucursal", SucursalFoto._UidSucursal, SqlDbType.UniqueIdentifier);
                comando.AddParameter("@UidStatus", SucursalFoto._UidStatus, SqlDbType.UniqueIdentifier);
                //No se le agrega vchstatus porque ya lo tiene en un catalogo en la bd solo se crea la referencia uidStatus
                comando.AddParameter("@UidImpresora", SucursalFoto._UidImpresora, SqlDbType.UniqueIdentifier);
                comando.AddParameter("@UidMedida", SucursalFoto._UidMedida, SqlDbType.UniqueIdentifier);

               comando.AddParameter("@VchDescripcion", SucursalFoto._StrDescripcion, SqlDbType.VarChar, 50);
                comando.AddParameter("@VchPrecio", SucursalFoto._StrPrecio, SqlDbType.VarChar, 50);
                comando.AddParameter("@VchAlto", SucursalFoto._VchAlto, SqlDbType.VarChar, 50);
                comando.AddParameter("@VchAncho", SucursalFoto._VchAncho, SqlDbType.VarChar, 50);
                comando.AddParameter("@VchColumna", SucursalFoto._VchColumna, SqlDbType.VarChar, 50);
                comando.AddParameter("@VchFila", SucursalFoto._VchFila, SqlDbType.VarChar, 50);
                comando.Parameters.Add("@BitRotarEnPapel", SqlDbType.Bit).Value = SucursalFoto._BooRotarEnPapel;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot save a Impresora from Sucursal", e);
                }
            }

            public List<SucursalFoto> FindAll(Guid uid)//Lista 18/10/2017
            {
                DataTable table = new DataTable();
                List<SucursalFoto> Fotos = new List<SucursalFoto>();
                SucursalFoto SucursalFoto = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalFoto_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        SucursalFoto = new SucursalFoto()
                        {
                            //_UidSucursal = uid,
                            //_UidImpresora = new Guid(row["UidImpresora"].ToString()),
                            //_UidTipoImpresora = (Guid)row["UidTipoImpresora"],
                            //_UidStatus = (Guid)row["UidStatus"],
                            //_StrMarca = row["VchMarca"].ToString(),
                            //_StrModelo = row["VchModelo"].ToString(),
                            //_StrStatus = row["VchStatus"].ToString(),
                            //_ExistsInDatabase = true,
                            _UidSucursal = uid,
                            _UidFoto = (Guid)row["UidFoto"],
                            _StrDescripcion = row["VchDescripcion"].ToString(),
                            _StrPrecio = row["VchPrecio"].ToString(),
                            _UidStatus = (Guid)row["UidStatus"],
                            _UidImpresora = (Guid)row["UidImpresora"],
                            _StrStatus = row["VchStatus"].ToString(),
                            _VchAlto = row["VchAlto"].ToString(),
                            _VchAncho = row["VchAncho"].ToString(),
                            _UidMedida = (Guid)row["UidMedida"],
                            _VchMedida = row["VchMedida"].ToString(),
                            _VchColumna = row["VchColumna"].ToString(),
                            _VchFila = row["VchFila"].ToString(),
                            _BooRotarEnPapel = Convert.ToBoolean(row["BitRotarEnPapel"]),
                            _ExistsInDatabase = true,
                        };
                        ////if ( int.Parse( row["BitRotarEnPapel"].ToString())==1) {
                        ////    SucursalFoto._BooRotarEnPapel = true;
                        ////} else {
                        ////    SucursalFoto._BooRotarEnPapel = false;
                        ////}
                        Fotos.Add(SucursalFoto);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return Fotos;
            }

            public bool Remove(SucursalFoto SucursalFoto)//Lista 04/10/2017
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalFoto_Remove";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidFoto", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidFoto"].Value = SucursalFoto._UidFoto;

                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error removing a Foto", e);
                }
            }
        }
    }
}
