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

namespace CodorniX.Modelo
{
    [Serializable]
    public class HImpresiones
    {
        #region Propiedades

        protected int _IntFolio;
        public int IntFolio
        {
            get { return _IntFolio; }
            set { _IntFolio = value; }
        }

        protected DateTime _StrFechaHora;
        public DateTime StrFechaHora
        {
            get { return _StrFechaHora; }
            set { _StrFechaHora = value; }
        }

        protected int _IntMaquina;
        public int IntMaquina
        {
            get { return _IntMaquina; }
            set { _IntMaquina = value; }
        }

        protected String _StrFotoDesc;
        public String StrFotoDesc
        {
            get { return _StrFotoDesc; }
            set { _StrFotoDesc = value; }
        }
        protected String _StrCopiasXImpresion;
        public String StrCopiasXImpresion
        {
            get { return _StrCopiasXImpresion; }
            set { _StrCopiasXImpresion = value; }
        }
        protected String _StrFotosXCopiasXImpresion;
        public String StrFotosXCopiasXImpresion
        {
            get { return _StrFotosXCopiasXImpresion; }
            set { _StrFotosXCopiasXImpresion = value; }
        }
        protected String _StrCostoTicket;
        public String StrCostoTicket
        {
            get { return _StrCostoTicket; }
            set { _StrCostoTicket = value; }
        }
        protected String _StrCosto;
        public String StrCosto
        {
            get { return _StrCosto; }
            set { _StrCosto = value; }
        }

        #endregion Propiedades

        #region Metodos
        public class Repository
        {
            Conexion Conexionhost = new Conexion();

            public List<HImpresiones> CargarVentas(Guid uidturno)
            {
                List<HImpresiones> ventas = new List<HImpresiones>();
                HImpresiones venta = null;

                //try
                //{

                    System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ImpresionesXturno_Server";
                    comando.AddParameter("@UidTurno", uidturno, SqlDbType.UniqueIdentifier);

                    DataTable table = Conexionhost.Busquedas(comando);

                    //NumberFormatInfo ni = new NumberFormatInfo();
                    //ni.NumberDecimalSeparator = ".";

                    foreach (DataRow item in Conexionhost.Busquedas(comando).Rows)
                    {
                        venta = new HImpresiones();
                        //{
                        //    IntFolio = int.Parse(item["IntFolio"].ToString()),
                        //    StrFechaHora = item["DtTmVenta"].ToString(),
                        //    IntMaquina = int.Parse(item["IntMaquina"].ToString()),
                        //    StrFotoDesc =  item["VchDescripcion"].ToString(),
                        //    StrCopiasXImpresion = item["IntCopiasXImpresion"].ToString(),
                        //    StrFotosXCopiasXImpresion = item["IntFotosXCopiasXImpresion"].ToString(),
                        //    StrCostoTicket = item["VchCostoTicket"].ToString(),
                        //    StrCosto = item["VchCosto"].ToString()
                        //};
                        int tmp;
                        int IntSinCampo = -1;
                        if (!int.TryParse(item["IntFolio"].ToString().Trim(), out tmp))
                        {
                            venta.IntFolio = -1;
                        }
                        else {
                            venta.IntFolio = int.Parse(item["IntFolio"].ToString());
                        }
                    //string v = item["DtTmVenta"].ToString();
                    // DateTime FHVenta = DateTime.ParseExact(item["DtTmVenta"].ToString(), "d/M/yyyy H:mm:ss", CultureInfo.InvariantCulture);
                    // venta.StrFechaHora = FHVenta.ToString("dd/MM/yyyy HH:mm");
                    //venta.StrFechaHora = DateTime.ParseExact(item["DtTmVenta"].ToString(), "dd/MM/yyyy hh:mm:ss", CultureInfo.InvariantCulture);
                    //DateTime FHVenta = Convert.ToDateTime(item["DtTmVenta"].ToString());
                    //venta.StrFechaHora = FHVenta.ToString();
                       venta.StrFechaHora = Convert.ToDateTime(item["DtTmVenta"].ToString());
                    if (!int.TryParse(item["IntMaquina"].ToString().Trim(), out tmp))
                        {
                            venta.IntMaquina = -1;
                        }
                        else
                        {
                            venta.IntMaquina = int.Parse(item["IntMaquina"].ToString());
                        }
                        venta.StrFotoDesc = item["VchDescripcion"].ToString();
                        venta.StrCopiasXImpresion = item["IntCopiasXImpresion"].ToString();
                        if (item["IntFotosXCopiasXImpresion"].ToString().Trim()==null || item["IntFotosXCopiasXImpresion"].ToString().Trim() == "")
                        {
                            venta.StrFotosXCopiasXImpresion = IntSinCampo.ToString();
                        }
                        else
                        {
                            venta.StrFotosXCopiasXImpresion = item["IntFotosXCopiasXImpresion"].ToString().Trim();
                        }
                        if (item["VchCostoTicket"].ToString().Trim() == null || item["VchCostoTicket"].ToString().Trim() == "")
                        {
                            venta.StrCostoTicket = IntSinCampo.ToString();
                        }
                        else
                        {
                            venta.StrCostoTicket = item["VchCostoTicket"].ToString().Trim();
                        }
                        venta.StrCosto = item["VchCosto"].ToString();
                        ventas.Add(venta);
                    }
                //}
                //catch (SqlException e)
                //{
                //    throw new HImpresionesException("( No se pudo cargar la lista de Historicos de ventas ) \n\n" + e.Message);
                //}

                return ventas;
            }

            public class HImpresionesException : Exception
            {
                public HImpresionesException(string mensaje) : base("(M_HImpresionesException error):  " + mensaje) { }
            }
        }
        #endregion Metodos
    }
}
