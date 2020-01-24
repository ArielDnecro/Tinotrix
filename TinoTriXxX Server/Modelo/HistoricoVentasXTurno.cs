using CodorniX.ConexionDB;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.Modelo
{
    public class HistoricoImpresionesXTurno
    {
        #region Propiedades
       
        protected int _IntFolio;
        public int IntFolio
        {
            get { return _IntFolio; }
            set { _IntFolio = value; }
        }

        protected String _StrFechaHora;
        public String StrFechaHora
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

        public new class Repository
        {
            Conexion Conexionhost = new Conexion();

            public List<HistoricoImpresionesXTurno> CargarVentas(Guid uidturno)
            {
                List<HistoricoImpresionesXTurno> ventas = new List<HistoricoImpresionesXTurno>();
                HistoricoImpresionesXTurno venta = null;

                try
                {

                    System.Data.SqlClient.SqlCommand comando = new System.Data.SqlClient.SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ImpresionesXturno_Server";
                    comando.AddParameter("@UidTurno", uidturno, SqlDbType.UniqueIdentifier);

                    DataTable table = Conexionhost.Busquedas(comando);

                    //NumberFormatInfo ni = new NumberFormatInfo();
                    //ni.NumberDecimalSeparator = ".";

                    foreach (DataRow item in Conexionhost.Busquedas(comando).Rows)
                    {
                        venta = new HistoricoImpresionesXTurno()
                        {
                            IntFolio = int.Parse( item["IntFolio"].ToString()),
                            StrFechaHora = item["DtTmVenta"].ToString(),
                            IntMaquina = int.Parse(item["IntMaquina"].ToString()),
                            StrFotoDesc= item["IntCopiasXImpresion"].ToString()+"x"+ item["VchDescripcion"].ToString(),
                            StrFotosXCopiasXImpresion= item["IntFotosXCopiasXImpresion"].ToString(),
                            StrCostoTicket = item["VchCostoTicket"].ToString(),
                            StrCosto = item["VchCosto"].ToString()
                        };
                        ventas.Add(venta);
                    }
                }
                catch (SqlException e)
                {
                    throw new HistoricoVentasXTurnoException("(No se pudo cargar la lista de fotos ) \n\n" + e.Message);
                }

                return ventas;
            }

            public class HistoricoVentasXTurnoException : Exception
            {
                public HistoricoVentasXTurnoException(string mensaje) : base("(HistoricoVentasXTurno error):  " + mensaje) { }
            }
        }

        public override string ToString()
        {
            return IntFolio + StrFechaHora + IntMaquina +StrFotosXCopiasXImpresion+ StrFotoDesc  + StrCostoTicket + StrCosto;
        }
    }
}
