using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TinoTriXxX.ConexionBaseDatos;

namespace TinoTriXxX.Modelo
{
    public class Impresora
    {
        protected string _StrDescripcion;
        public string StrDescripcion
        {
            get { return _StrDescripcion; }
            set { _StrDescripcion = value; }
        }

        public new class Repository
        {
            protected Konection _Conexion = new Konection();

            public Impresora Find()
            {
                DataTable table = new DataTable();
                Impresora impresora = new Impresora();
                impresora._StrDescripcion = "";
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_VerificarExistenciaImpresora";
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["IntNoImpresoras"].ToString()) == 1)
                    {
                        comando.CommandText = "Wpf_Impresora_Find";
                        table = _Conexion.ExecuteQuery(comando);
                        impresora._StrDescripcion = table.Rows[0]["VchDescripcion"].ToString();
                    }
                    //else {
                    //    licencia._UidLicencia = new Guid("00000000-0000-0000-0000-000000000000");
                    //}
                }
                catch (Exception e)
                {
                    throw new ImpresoraLocalException("(Obtener la impresora local)" + e.Message);
                }

                return impresora;
            }

            public bool ActualizarImpresora(string StrDesc)
            {
                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ActualizarImpresora";
                    comando.Parameters.Add("@VchDescripcion", SqlDbType.VarChar, 150);
                    comando.Parameters["@VchDescripcion"].Value = StrDesc;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new ImpresoraLocalException("(Actualizar licencia local) " + e.Message);
                }

            }
            #region Excepciones
            public class ImpresoraLocalException : Exception
            {
                public ImpresoraLocalException(string mensaje) : base("(ImpresoraLocalException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
