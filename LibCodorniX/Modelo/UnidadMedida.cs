using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodorniX.Modelo
{
    public class UnidadMedida
    {
        #region propiedades
        protected bool _ExistsInDatabase;
       

        protected Guid _UidMedida;
        public Guid UidMedida
        {
            get { return _UidMedida; }
            set { _UidMedida = value; }
        }
        protected string _VchMedida;

        public string VchMedida
        {
            get { return _VchMedida; }
            set { _VchMedida = value; }
        }
        #endregion propiedades

        public new class Repository
        {
            //Comienzo repositorio raiz tomado de suscursaltelefono -> telefono
            protected Connection _Conexion = new Connection();
           
            public List<UnidadMedida> FindAll()
            {
                DataTable table = new DataTable();
                List<UnidadMedida> Medidas = new List<UnidadMedida>();
                UnidadMedida Medida = null;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_UnidadMedida_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;

                    //comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    //comando.Parameters["@UidSucursal"].Value = uid;

                    table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {

                        Medida = new UnidadMedida()
                        {
                            
                            _UidMedida = (Guid)row["UidMedida"],
                            _VchMedida = row["VchMedida"].ToString(),
                            _ExistsInDatabase = true,
                        };
                        Medidas.Add(Medida);
                    }
                }
                catch (Exception e)
                {
                    throw new DatabaseException("No cargo las unidades de medida en finall", e);
                }

                return Medidas;
            }
            
        }
    }
}
