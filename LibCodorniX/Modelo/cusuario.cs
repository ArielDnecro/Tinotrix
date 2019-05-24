using CodorniX.Modelo;
using CodorniX.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Citas.modelo
{
    public class cusuario
    {

        #region propiedades
        private bool _ExistsInDatabase;

        private string _Vchnombre;
        public string Vchnombre
        {
            get { return _Vchnombre; }
            set { _Vchnombre = value; }
        }

        private int _intidusuario;
        public int intidusuario
        {
            get { return _intidusuario; }
            set { _intidusuario = value; }
        }



        #endregion propiedades
        public class Repository
        {

            Connection _Conexion = new Connection();

           
            private bool InternalUpdate(cusuario usuario)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Usuario_Update";
                    comando.CommandType = CommandType.StoredProcedure;

                    comando.Parameters.Add("@Intidusuario", SqlDbType.Int);
                    comando.Parameters["@Intidusuario"].Value = usuario.intidusuario;

                    comando.Parameters.Add("@VchNombre", SqlDbType.VarChar, 50);
                    comando.Parameters["@VchNombre"].Value = usuario.Vchnombre;

                   
                    return _Conexion.ExecuteCommand(comando);
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update Sucursal entry", e);
                }
            }

         
            private bool InternalSave(cusuario usuario)
            {
                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_usuario_Add";
                    comando.CommandType = CommandType.StoredProcedure;

                    

                    comando.Parameters.Add("@Vchnombre", SqlDbType.VarChar, 50);
                    comando.Parameters["@Vchnombre"].Value = usuario._Vchnombre;

                    comando.Parameters.Add("@Intidusuario", SqlDbType.UniqueIdentifier);
                    comando.Parameters["@Intidusuario"].Value = usuario._intidusuario;
                    

                    bool result = _Conexion.ExecuteCommand(comando, false);

                    usuario.intidusuario = int.Parse(comando.Parameters["@Intidusuario"].Value.ToString());

                    comando.Dispose();

                    return result;
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot update usuario", e);
                }
            }

            //public bool Save(cusuario usuario)
            //{
            //    if (cusuario._ExistsInDatabase)
            //        return InternalUpdate(usuario);
            //    else
            //    {
            //        Sucursal._ExistsInDatabase = true;
            //        return InternalSave(usuario);
            //    }
            //}
            

            public List<cusuario> FindAll()
            {
                List<cusuario> usuarios = new List<cusuario>();
                cusuario usuario = null;

                try
                {
                    SqlCommand comando = new SqlCommand();
                    comando.CommandText = "usp_Usuarios_FindAll";
                    comando.CommandType = CommandType.StoredProcedure;
                   

                    DataTable table = _Conexion.ExecuteQuery(comando);

                    foreach (DataRow row in table.Rows)
                    {
                        usuario = new cusuario()
                        {
                            _ExistsInDatabase = true,
                            _intidusuario = int.Parse(row["UidSucursal"].ToString()),
                           Vchnombre = row["VchNombre"].ToString()
                        };
                        usuarios.Add(usuario);
                    }
                }
                catch (SqlException e)
                {
                    throw new DatabaseException("Cannot load usuarios", e);
                }

                return usuarios;
            }

          
        }
    }
}