using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;


namespace CodorniX.ConexionDB
{
    public class Conexion
    {
        #region MyRegion

        
        private SqlConnection conexion = new SqlConnection(Config.ConnectionString);
        #endregion
        public Conexion()
        {

        }

        #region Metodos
        public DataTable Consultas(string Query)
        {
            DataTable Tabla = new DataTable();
            conexion.Open();


            SqlCommand comando = new SqlCommand(Query, conexion);
            SqlDataAdapter Adaptador = new SqlDataAdapter(comando);
            Adaptador.Fill(Tabla);

            conexion.Close();

            return Tabla;
        }
        public bool ManipilacionDeDatos(SqlCommand comando, bool disponse = true)
        {
            bool Resultado = false;
            conexion.Open();
            try
            {
                comando.Connection = conexion;
                comando.ExecuteNonQuery();
                if (disponse)
                    comando.Dispose();
                Resultado = true;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
            return Resultado;
        }




        public DataTable Busquedas(SqlCommand comando)
        {
            DataTable Tabla = new DataTable();
            conexion.Open();
            try
            {
                comando.Connection = conexion;
                SqlDataAdapter Adaptador = new SqlDataAdapter(comando);
                Adaptador.Fill(Tabla);
            }
            catch (Exception)
            {

              throw;
            }
            finally
            {
                conexion.Close();
            }
            return Tabla;
        }



        #endregion

    }
}