using System;
using System.Collections.Generic;
using System.Linq;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
   public class SucursalImpresoraTipo
    {
        #region propiedades
        protected bool _ExistsInDatabase;

        protected Guid _UidTipoImpresora;
        public Guid UidTipoImpresora
        {
            get { return _UidTipoImpresora; }
            set { _UidTipoImpresora = value; }
        }

        protected string _StrDescripcion;
        public string StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }

        protected int _IntOrden;
        public int IntOrden
        {
            get { return _IntOrden; }
            set { _IntOrden = value; }
        }
        protected Guid _UidStatus;
        public Guid UidStatus
        {
            get { return _UidStatus; }
            set { _UidStatus = value; }
        }

        //private Guid _UidSucursal;
        //public Guid UidSucursal
        //{
        //    get { return _UidSucursal; }
        //    set { _UidSucursal = value; }
        //}


        //protected Guid _UidTipoImpresora;
        //public Guid UidTipoImpresora
        //{
        //    get { return _UidTipoImpresora; }
        //    set { _UidTipoImpresora = value; }
        //}



        //protected string _StrMarca;
        //public string StrMarca
        //{
        //    get { return _StrMarca; }
        //    set { _StrMarca = value; }
        //}


        //protected string _StrModelo;
        //public string StrModelo
        //{
        //    get { return _StrModelo; }
        //    set { _StrModelo = value; }
        //}


        //protected string _StrTipoImpresora;
        //public string StrTipoImpresora
        //{
        //    get { return _StrTipoImpresora; }
        //    set { _StrTipoImpresora = value; }
        //}


        //protected string _StrStatus;
        //public string StrStatus
        //{
        //    get { return _StrStatus; }
        //    set { _StrStatus = value; }
        //}

        #endregion propiedades
        public new class Repository
        {
            //Comienzo repositorio raiz tomado de suscursaltelefono -> telefono
            protected Connection _Conexion = new Connection();
            
            public List<SucursalImpresoraTipo> FindAll()//Lista 18/10/2017
            {
                DataTable table = new DataTable();
                List<SucursalImpresoraTipo> TipoImpresoras = new List<SucursalImpresoraTipo>();
                SucursalImpresoraTipo SucursalImpresoraTipo = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_SucursalImpresoraTipo_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;


                    //comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    //comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        SucursalImpresoraTipo = new SucursalImpresoraTipo()
                        {
                            _UidTipoImpresora = (Guid)row["UidTipoImpresora"],
                            _UidStatus = (Guid)row["UidStatus"],
                            _StrDescripcion = row["VchDescripcion"].ToString(),
                            _IntOrden = (int)row["IntOrden"],
                            _ExistsInDatabase = true,
                        };
                        TipoImpresoras.Add(SucursalImpresoraTipo);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("Error populating", e);
                }

                return TipoImpresoras;
            }
            
        }
    }
}
