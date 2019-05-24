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
     public class EmpresaLocal
    {
        protected Guid _UidEmpresa;
        public Guid UidEmpresa
        {
            get { return _UidEmpresa; }
            set { _UidEmpresa = value; }
        }
        public new class Repository
        {
            protected Konection _Conexion = new Konection();
            public EmpresaLocal Find()
            {
                DataTable table = new DataTable();
                EmpresaLocal empresa = new EmpresaLocal();
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_VerificarExistenciaEmpresa";
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["IntNoEmpresas"].ToString()) == 1)
                    {
                        comando.CommandText = "Wpf_Empresa_Find";
                        table = _Conexion.ExecuteQuery(comando);
                        empresa._UidEmpresa = new Guid(table.Rows[0]["UidEmpresa"].ToString());
                    }
                }
                catch (Exception e)
                {
                    throw new EmpresaLocalException("(Obtener la Empresa local)" + e.Message);
                }

                return empresa;
            }
            public bool Revocar()
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_RevocarEmpresa";
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new EmpresaLocalException("(Revocar Empresa local) " + e.Message);
                }

            }
            public bool ActualizarEmpresa(Guid EmpresaNueva)
            {
                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ActualizarEmpresa";
                    comando.Parameters.Add("@UidEmpresa", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidEmpresa"].Value = EmpresaNueva;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new EmpresaLocalException("(Actualizar Empresa local) " + e.Message);
                }

            }
            #region Excepciones
            public class EmpresaLocalException : Exception
            {
                public EmpresaLocalException(string mensaje) : base("(EmpresaLocalException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
