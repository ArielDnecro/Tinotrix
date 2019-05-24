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
    public class LicenciaLocal
    {

        protected Guid _UidLicencia;
        public Guid UidLicencia
        {
            get { return _UidLicencia; }
            set { _UidLicencia = value; }
        }
        public new class Repository
        {
            protected Konection _Conexion = new Konection();
            public LicenciaLocal Find()
            {
                DataTable table = new DataTable();
                LicenciaLocal licencia = new LicenciaLocal();
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_VerificarExistenciaLicencia";
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["IntNoLicencias"].ToString()) == 1)
                    {
                        comando.CommandText = "Wpf_Licencia_Find";
                        table = _Conexion.ExecuteQuery(comando);
                        licencia._UidLicencia = new Guid(table.Rows[0]["UidLicencia"].ToString());
                    }
                    //else {
                    //    licencia._UidLicencia = new Guid("00000000-0000-0000-0000-000000000000");
                    //}
                }
                catch (Exception e)
                {
                    throw new LicenciaLocalException("(Obtener la licencia local)" + e.Message);
                }

                return licencia;
            }

            public bool Revocar()
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_RevocarLicencia";
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new LicenciaLocalException("(Revocar licencia local) " + e.Message);
                }

            }
            public bool ActualizarLicencia(Guid LicenciaNueva) {
                try
                {
                    
                     SqlCommand comando = new SqlCommand();
                     comando.CommandType = CommandType.StoredProcedure;
                     comando.CommandText = "Wpf_ActualizarLicencia";
                    //comando.AddParameter("@UidSucursal", LicenciaNueva, SqlDbType.UniqueIdentifier);
                    comando.Parameters.Add("@UidLicencia", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidLicencia"].Value = LicenciaNueva;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new LicenciaLocalException("(Actualizar licencia local) " + e.Message);
                }

            }

            #region Excepciones
            public class LicenciaLocalException : Exception
            {
                public LicenciaLocalException(string mensaje) : base("(LicenciaLocalException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
