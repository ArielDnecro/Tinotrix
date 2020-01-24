using System;
using CodorniX.Util;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

 
namespace CodorniX.Modelo
{ 
    [Serializable]
    public class SucursalFotoC
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

        protected string _StrPrecioTicket;
        public string StrPrecioTicket
        {
            get { return _StrPrecioTicket; }
            set { _StrPrecioTicket = value; }
        }
        protected string _StrPrecioServidor;
        public string StrPrecioServidor
        {
            get { return _StrPrecioServidor; }
            set { _StrPrecioServidor = value; }
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
        protected string _VchAltoDesc;
        public string VchAltoDesc
        {
            get { return _VchAltoDesc; }
            set { _VchAltoDesc = value; }
        }

        protected string _VchAncho;
        public string VchAncho
        {
            get { return _VchAncho; }
            set { _VchAncho = value; }
        }

        protected string _VchAnchoDesc;
        public string VchAnchoDesc
        {
            get { return _VchAnchoDesc; }
            set { _VchAnchoDesc = value; }
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

           

            public bool Save(SucursalFotoC SucursalFoto)//Lista 18/10/2017
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    if (SucursalFoto._ExistsInDatabase)
                    {
                        //return InternalUpdate(SucursalFoto);
                        comando.CommandText = "usp_FotoC_Update";
                        comando.AddParameter("@UidFoto", SucursalFoto._UidFoto, SqlDbType.UniqueIdentifier);
                    }
                    else
                    {
                        SucursalFoto._ExistsInDatabase = true;
                        //return InternalSave(SucursalFoto);
                        comando.CommandText = "usp_SucursalFotoC_Add";
                    }
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.AddParameter("@UidSucursal", SucursalFoto._UidSucursal, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@UidStatus", SucursalFoto._UidStatus, SqlDbType.UniqueIdentifier);
                    //No se le agrega vchstatus porque ya lo tiene en un catalogo en la bd solo se crea la referencia uidStatus
                    comando.AddParameter("@UidImpresora", SucursalFoto._UidImpresora, SqlDbType.UniqueIdentifier);
                    comando.AddParameter("@UidMedida", SucursalFoto._UidMedida, SqlDbType.UniqueIdentifier);

                    comando.AddParameter("@VchDescripcion", SucursalFoto._StrDescripcion, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchPrecio", SucursalFoto._StrPrecio, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchPrecioTicket", SucursalFoto._StrPrecioTicket, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchPrecioServidor", SucursalFoto._StrPrecioServidor, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchAlto", SucursalFoto._VchAlto, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchAncho", SucursalFoto._VchAncho, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchAltoDesc", SucursalFoto._VchAltoDesc, SqlDbType.VarChar, 50);
                    comando.AddParameter("@VchAnchoDesc", SucursalFoto._VchAnchoDesc, SqlDbType.VarChar, 50);
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

            public List<SucursalFotoC> FindAll(Guid uid)//Lista 18/10/2017
            {
                DataTable table = new DataTable();
                List<SucursalFotoC> Fotos = new List<SucursalFotoC>();
                SucursalFotoC SucursalFoto = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalFotoC_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        SucursalFoto = new SucursalFotoC()
                        {
                           
                            _UidSucursal = uid,
                            _UidFoto = (Guid)row["UidFoto"],
                            _StrDescripcion = row["VchDescripcion"].ToString(),
                            _StrPrecio = row["VchPrecio"].ToString(),
                            _StrPrecioTicket = row["VchPrecioTicket"].ToString(),
                            _StrPrecioServidor = row["VchPrecioServidor"].ToString(),
                            _UidStatus = (Guid)row["UidStatus"],
                            _UidImpresora = (Guid)row["UidImpresora"],
                            _StrStatus = row["VchStatus"].ToString(),
                            _VchAlto = row["VchAlto"].ToString(),
                            _VchAncho = row["VchAncho"].ToString(),
                            _VchAltoDesc = row["VchAltoDesc"].ToString(),
                            _VchAnchoDesc = row["VchAnchoDesc"].ToString(),
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

            public bool Remove(SucursalFotoC SucursalFoto)//Lista 04/10/2017
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalFotoC_Remove";
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
