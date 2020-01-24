using CodorniX.ConexionDB;
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
    [Serializable]
    public class HTurno
    {
        #region Propiedades
        [NonSerialized]
        Conexion Conexion = new Conexion();

        protected string _StrSucursal;
        public string StrSucursal
        {
            get { return _StrSucursal; }
            set { _StrSucursal = value; }
        }
        protected Guid _UidSucursal;
        public Guid UidSucursal
        {
            get { return _UidSucursal; }
            set { _UidSucursal = value; }
        }
        protected string _StrEncargado;
        public string StrEncargado
        {
            get { return _StrEncargado; }
            set { _StrEncargado = value; }
        }
        protected Guid _UidEncargado;
        public Guid UidEncargado
        {
            get { return _UidEncargado; }
            set { _UidEncargado = value; }
        }
        protected Guid _UidTurno;
        public Guid UidTurno
        {
            get { return _UidTurno; }
            set { _UidTurno = value; }
        }
        protected int _IntFolio;
        public int IntFolio
        {
            get { return _IntFolio; }
            set { _IntFolio = value; }
        }

        protected int _IntNoFotos;
        public int IntNoFotos
        {
            get { return _IntNoFotos; }
            set { _IntNoFotos = value; }
        }

        protected double _DouImporte;
        public double DouImporte
        {
            get { return _DouImporte; }
            set { _DouImporte = value; }
        }
        protected DateTime _DtApertura;

        public DateTime DtApertura
        {
            get { return _DtApertura; }
            set { _DtApertura = value; }
        }
        protected DateTime _DtCierre;
        public DateTime DtCierre
        {
            get { return _DtCierre; }
            set { _DtCierre = value; }
        }

        protected List<HImpresiones> _LHImpresiones;
        public List<HImpresiones> LHImpresiones
        {
            get { return _LHImpresiones; }
            set { _LHImpresiones = value; }
        }

        #endregion Propiedades

        #region Metodos
        public class Repository
        {
            Conexion Conexion = new Conexion();

            public List<HTurno> FindHTurnos(string Rquery)
            {
                //try
                //{
                    Conexion = new Conexion();
                    List<HTurno> hturnos = new List<HTurno>();
                    HTurno hturno = null;
                    SqlCommand command = new SqlCommand();
                    //command.CommandText = "SELECT * FROM Usuario WHERE Usuario.UidUsuario = @uid";
                    command.CommandText = Rquery;
                    command.CommandType = CommandType.Text;
                    //command.AddParameter("@uid", uid, SqlDbType.UniqueIdentifier);

                    DataTable table = new Connection().ExecuteQuery(command);

                    foreach (DataRow row in table.Rows)
                    {
                        hturno = new HTurno()
                        {
                            _StrSucursal = row["VchSucursal"].ToString(),
                            _UidSucursal = new Guid(row["UidSucursal"].ToString()),
                            _StrEncargado = row["VchEncargado"].ToString(),
                            _UidEncargado = new Guid (row["UidEncargado"].ToString()),
                            _UidTurno = new Guid(row["UidTurno"].ToString()),
                            _IntFolio = int.Parse(row["IntFolio"].ToString()),
                            _IntNoFotos = int.Parse(row["IntNoFotos"].ToString()),
                            _DouImporte = double.Parse(row["IntImporte"].ToString()),
                            _DtCierre = Convert.ToDateTime(row["DtCierre"].ToString()),
                            _DtApertura = Convert.ToDateTime(row["DtApertura"].ToString())
                        };
                        hturnos.Add(hturno);
                    }

                    return hturnos;
                //}
                //catch (SqlException e)
                //{
                //    throw new HTurnoException("(No se pudo cargar la lista de HISTORICO )\n \n " + e.Message);
                //}
            }

            public class HTurnoException : Exception
            {
                public HTurnoException(string mensaje) : base("(HTurno error):  " + mensaje) { }
            }
        }
        #endregion Metodos
    }
}
