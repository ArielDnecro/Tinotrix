using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CodorniX.ConexionDB
{
    public class DBPerfiles
    {
        Conexion conexion = new Conexion();
        public DataTable obtenerNivelAcceso(string NombreNivel)
        {
            string consulta = "select UidNivelAcceso from NivelAcceso where VchNivelAcceso ='" + NombreNivel + "'";
            return conexion.Consultas(consulta);

        }
        
    }
}