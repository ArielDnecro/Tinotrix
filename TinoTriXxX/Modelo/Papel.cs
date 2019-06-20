using CodorniX.ConexionDB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.Modelo
{
    public class Papel
    {
        #region Propiedades
        protected bool _ExistsInDatabase = false;
        protected Guid _UidPapel;
        public Guid UidPapel
        {
            get { return _UidPapel; }
            set { _UidPapel = value; }
        }
        protected String _StrDescripcion;
        public String StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }
        protected String _StrAlto;
        public String StrAlto
        {
            get { return _StrAlto; }
            set { _StrAlto = value; }
        }
        protected String _StrAncho;
        public String StrAncho
        {
            get { return _StrAncho; }
            set { _StrAncho = value; }
        }
        protected String _StrMSuperior;
        public String StrMSuperior
        {
            get { return _StrMSuperior; }
            set { _StrMSuperior = value; }
        }
        protected String _StrMInferior;
        public String StrMInferior
        {
            get { return _StrMInferior; }
            set { _StrMInferior = value; }
        }
        protected String _StrMDerecho;
        public String StrMDerecho
        {
            get { return _StrMDerecho; }
            set { _StrMDerecho = value; }
        }
        protected String _StrMIzquierdo;
        public String StrMIzquierdo
        {
            get { return _StrMIzquierdo; }
            set { _StrMIzquierdo = value; }
        }
        #endregion Propiedades
        public new class Repository
        {
            Conexion Conexionhost = new Conexion();
            public Papel Find(Guid uid)
            {
                DataTable table = new DataTable();
                Papel Papel = new Papel();
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "usp_SucursalPapel_Find";
                    comando.Parameters.Add("@UidPapel", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidPapel"].Value = uid;
                    table = Conexionhost.Busquedas(comando);
                    if (table.Rows.Count == 1)
                    {
                        Papel._UidPapel =uid ;
                        Papel._StrDescripcion = table.Rows[0]["VchDescripcion"].ToString();
                        Papel._StrAlto = table.Rows[0]["VchAlto"].ToString();
                        Papel._StrAncho = table.Rows[0]["VchAncho"].ToString();
                        Papel._StrMSuperior = table.Rows[0]["VchMSuperior"].ToString();
                        Papel._StrMInferior = table.Rows[0]["VchMInferior"].ToString();
                        Papel._StrMDerecho = table.Rows[0]["VchMDerecho"].ToString();
                        Papel._StrMIzquierdo = table.Rows[0]["VchMIzquierdo"].ToString();
                        Papel._ExistsInDatabase = true;
                    }
                    
                }
                catch (Exception e)
                {
                    throw new PapelLocalException("(Obtener la licencia local)" + e.Message);
                }

                return Papel;
            }

            #region Excepciones
            public class PapelLocalException : Exception
            {
                public PapelLocalException(string mensaje) : base("(PapelLocalException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
