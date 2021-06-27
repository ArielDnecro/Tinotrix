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
using TinoTriXxX.ConexionBaseDatos;

namespace TinoTriXxX.Modelo
{
    //public class Foo
    //{
    //    public int Id { get; set; }
    //    public DateTime IAmSoSmall { get; set; }    // wants to be smalldatetime in SQL
    //}
    public class Foto
    {
        //public DbSet<Foo> Foos { get; set; }
        #region propiedades
        protected Guid _UidFoto;
        public Guid UidFoto
        {
            get { return _UidFoto; }
            set { _UidFoto = value; }
        }
        protected String _StrStatus;
        public String StrStatus
        {
            get { return _StrStatus; }
            set { _StrStatus = value; }
        }
        protected String _StrDescripcion;
        public String StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }
        protected String _StrDescripcionDetalle;
        public String StrDescripcionDetalle
        {
            get { return _StrDescripcionDetalle; }
            set { _StrDescripcionDetalle = value; }
        }
        protected String _StrPrecio;
        public String StrPrecio
        {
            get { return _StrPrecio; }
            set { _StrPrecio = value; }
        }
        protected double _IntAncho;
        public double IntAncho
        {
            get { return _IntAncho; }
            set { _IntAncho = value; }
        }
        protected double _IntAlto;
        public double IntAlto
        {
            get { return _IntAlto; }
            set { _IntAlto = value; }
        }
        protected String _StrMedida;
        public String StrMedida
        {
            get { return _StrMedida; }
            set { _StrMedida = value; }
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
            Conexion Conexionhost = new Conexion();
            //DataTable table = new DataTable();
            //Foto Turno = new Foto();
            public List<Foto> CargarFotos(Guid uidsucursal)
            {
                List<Foto> fotos = new List<Foto>();
                Foto foto = null;

                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_Foto_find";
                    comando.AddParameter("@UidSucursal", uidsucursal, SqlDbType.UniqueIdentifier);

                    DataTable table = Conexionhost.Busquedas(comando);

                    NumberFormatInfo ni = new NumberFormatInfo();
                    ni.NumberDecimalSeparator = ".";

                    foreach (DataRow item in Conexionhost.Busquedas(comando).Rows)
                    {
                        foto = new Foto()
                        {
                            UidFoto = new Guid(item["UidFoto"].ToString()),
                            StrStatus = item["VchStatus"].ToString(),
                            StrDescripcion = item["VchDescripcion"].ToString(),
                            StrPrecio = item["VchPrecio"].ToString(),
                            IntAlto =  double.Parse(item["VchAlto"].ToString(), ni),
                            IntAncho = double.Parse(item["VchAncho"].ToString(), ni),
                            StrMedida = item["VchMedida"].ToString(),
                            _VchColumna = item["VchColumna"].ToString(),
                            _VchFila = item["VchFila"].ToString(),
                            _BooRotarEnPapel = Convert.ToBoolean(item["BitRotarEnPapel"]),
                            StrDescripcionDetalle ="$ " + item["VchPrecio"].ToString()+ " c/u ( "+ item["VchAlto"].ToString()+" x "+ item["VchAncho"].ToString()+" " + item["VchMedida"].ToString()+ " ) "

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

            public bool NuevaImpresion( Guid UidSucursal, Guid UidFoto, string DtTmVenta, int IntFotos, int IntCosto)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_AddVenta";

                    comando.Parameters.Add("@UidSucursal", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidSucursal"].Value = UidSucursal;

                    comando.Parameters.Add("@UidFoto", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidFoto"].Value = UidFoto;

                    comando.Parameters.Add("@DtTmVenta", SqlDbType.SmallDateTime);
                    comando.Parameters["@DtTmVenta"].Value = DtTmVenta;

                    comando.Parameters.Add("@IntFotos", SqlDbType.Int);
                    comando.Parameters["@IntFotos"].Value = IntFotos;

                    comando.Parameters.Add("@IntCosto", SqlDbType.Int);
                    comando.Parameters["@IntCosto"].Value = IntCosto;
                     
                    return Conexionhost.ManipilacionDeDatos(comando);
                }
                catch (Exception e)
                {
                    throw new FotoException("(Nueva venta impresion fotos) " + e.Message);
                }
            }

            protected Konection _Conexion = new Konection();
            public double CargarMedidaLocal(string StrMedida) {

                DataTable table = new DataTable();
                //LicenciaLocal licencia = new LicenciaLocal();
                double IntMedida = 0.0;
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_UnidadMedida_Find";
                    comando.Parameters.Add("@VchAliasMedida", SqlDbType.VarChar, 150);
                    comando.Parameters["@VchAliasMedida"].Value = StrMedida;
                    NumberFormatInfo ni = new NumberFormatInfo();
                    ni.NumberDecimalSeparator = ".";
                    table = _Conexion.ExecuteQuery(comando);
                    if (table.Rows.Count== 1)
                    {
                        IntMedida = double.Parse(table.Rows[0]["IntMedidaConversion"].ToString(),ni);
                    }
                  
                }
                catch (Exception e)
                {
                    throw new FotoException("(Cargar medida local)" + e.Message);
                }

                return IntMedida;
            }
            public class FotoException : Exception
            {
                public FotoException(string mensaje) : base("(Foto error):  " + mensaje) { }
            }
        }
        public override string ToString()
        {
             
            return StrDescripcion + StrStatus +StrDescripcionDetalle + StrPrecio + IntAlto + IntAncho;
        }
    }
}
