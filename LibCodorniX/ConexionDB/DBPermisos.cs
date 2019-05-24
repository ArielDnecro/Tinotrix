using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CodorniX.ConexionDB
{
    public class DBPermisos
    {
        Conexion conexion = new Conexion();
        public DataTable obtenerPermisos()
        {
            string query = "select * from Permisos";
            return conexion.Consultas(query);
        }

        public DataTable obtenerPerfilPermisos(Guid idPermisos)
        {
            string query = "select * from Permisos where PidPerfil='" + idPermisos + "'";
            return conexion.Consultas(query);
        }
    }
}