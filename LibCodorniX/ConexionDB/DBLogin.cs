using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.ConexionDB;
using System.Data;
using System.Data.SqlClient;

namespace CodorniX.ConexionDB
{
    public class DBLogin
    {
        Conexion conexion = new Conexion();
        public DataTable obtenerUsuarioLogin(string usuario)
        {
            string consulta = "select UidUsuario from Usuario where VchUsuario ='"+usuario+"'";
            return conexion.Consultas(consulta);
             
        }


        public DataTable obtenerperfiles(Guid usuario)
        {
            string consulta = "select UidPerfil from Usuario where UidUsuario ='" + usuario + "'";
            return conexion.Consultas(consulta);

        }

        //public DataTable obtenerpermisos(Guid Perfil)
        //{
        //    string consulta = "select MidModulo from Permisos where PidPerfil ='" + Perfil + "'";
        //    return conexion.Consultas(consulta);

        //}

        public DataTable obtenerpermisos(Guid Perfil)
        {
            string consulta = "select * from Permisos where UidPerfil ='" + Perfil + "'";
            return conexion.Consultas(consulta);

        }

        public DataTable obteneridperfil(string uidperfil)
        {
            string consultaperfil = "select UidPerfil from Perfil where VchPerfil= '" + uidperfil + "'";
            return conexion.Consultas(consultaperfil);

        }

        public DataTable obtenerPerfilLogin(string uiperfil)
        {
            string consultaperfil = "select VchPerfil from Perfil where UidPerfil in (select UidPerfil from UsuarioPerfilEmpresa where UidUsuario = '" + uiperfil+ "')";
            return conexion.Consultas(consultaperfil);

        }

        public DataTable obtenerEmpresaPerfil(string uidempresa)
        {
            string consultaperfil = "select VchNombreComercial from Empresa where UidEmpresa in (select UidEmpresa from UsuarioPerfilEmpresa where UidPerfil = '" + uidempresa + "')";
            return conexion.Consultas(consultaperfil);

        }
    }
}