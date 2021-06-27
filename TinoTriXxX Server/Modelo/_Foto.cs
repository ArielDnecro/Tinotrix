using CodorniX.ConexionDB;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.Modelo
{
   
    public class _Foto
    {
        #region Propiedades
        protected Guid _UidFoto;
        public Guid UidFoto
        {
            get { return _UidFoto; }
            set { _UidFoto = value; }
        }

        protected String _StrDescripcion;
        public String StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }

        protected String _StrCosto;
        public String StrCosto
        {
            get { return _StrCosto; }
            set { _StrCosto = value; }
        }

        protected String _StrCantidad;
        public String StrCantidad
        {
            get { return _StrCantidad; }
            set { _StrCantidad = value; }
        }
       
        #endregion Propiedades
        public new class Repository
        {
            Conexion Conexionhost = new Conexion();

            public List<_Foto> CargarFotos(Guid uidturno)
            {
                List<_Foto> fotos = new List<_Foto>();
                _Foto foto = null;

                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_Foto_find_Server";
                    comando.AddParameter("@UidTurno", uidturno, SqlDbType.UniqueIdentifier);

                    DataTable table = Conexionhost.Busquedas(comando);

                    //NumberFormatInfo ni = new NumberFormatInfo();
                    //ni.NumberDecimalSeparator = ".";

                    foreach (DataRow item in Conexionhost.Busquedas(comando).Rows)
                    {
                        foto = new _Foto()
                        {
                            UidFoto = new Guid(item["UidFoto"].ToString()),
                            StrDescripcion = item["VchDescripcion"].ToString(),
                            StrCosto = item["IntCosto"].ToString(),
                            StrCantidad = item["IntFotos"].ToString()
                        };
                        fotos.Add(foto);
                    }
                }
                catch (SqlException e)
                {
                    throw new FotoException("(No se pudo cargar la lista de fotos) " + e.Message);
                }

                return fotos;
            }


            public class FotoException : Exception
            {
                public FotoException(string mensaje) : base("(Foto error):  " + mensaje) { }
            }

        }
            
    }
}
