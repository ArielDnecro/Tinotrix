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
    public class Session
    {
        protected Guid _UidUsuario;
        public Guid UidUsusario
        {
            get { return _UidUsuario; }
            set { _UidUsuario = value; }
        }
        public new class Repository
        {
            protected Konection _Conexion = new Konection();
            public Session Find()
            {
                DataTable table = new DataTable();
                Session Sesion = new Session();
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_VerificarExistenciaUsuario";
                    table = _Conexion.ExecuteQuery(comando);
                    if (int.Parse(table.Rows[0]["IntNoUsuarios"].ToString()) == 1)
                    {
                        comando.CommandText = "Wpf_Usuario_Find";
                        table = _Conexion.ExecuteQuery(comando);
                        Sesion._UidUsuario = new Guid(table.Rows[0]["UidUsuario"].ToString());
                    }
                }
                catch (Exception e)
                {
                    throw new UsuarioLocalException("(Obtener El usuario local)" + e.Message);
                }

                return Sesion;
            }

            public bool Revocar()
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_RevocarUsuario";
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new UsuarioLocalException("(Revocar Usuario local) " + e.Message);
                }

            }
            public bool ActualizarUsuario(Guid UsuarioNuevo)
            {
                try
                {

                    SqlCommand comando = new SqlCommand();
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.CommandText = "Wpf_ActualizarUsuario";
                    comando.Parameters.Add("@UidUsuario", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@UidUsuario"].Value = UsuarioNuevo;
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (Exception e)
                {
                    throw new UsuarioLocalException("(Actualizar el usuario local) " + e.Message);
                }

            }
            #region Excepciones
            public class UsuarioLocalException : Exception
            {
                public UsuarioLocalException(string mensaje) : base("(UsuarioLocalException):  " + mensaje) { }
            }
            #endregion Excepciones
        }
    }
}
