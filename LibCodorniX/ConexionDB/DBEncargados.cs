using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace CodorniX.ConexionDB
{
    public class DBEncargados
    {

        Conexion con = new Conexion();
        public DataTable obtenerUsuarios()
        {
            string query = "select u.*, s.VchStatus, p.VchPerfil from Usuario" +
                " u JOIN Estatus s ON u.UidStatus = s.UidStatus JOIN UsuarioPerfilSucursal " +
                "upe on u.UidUsuario = upe.UidUsuario JOIN Perfil p on upe.UidPerfil = p.UidPerfil " +
                "where p.VchPerfil not like 'Administrador' and p.VchPerfil not like 'SuperAdministrador' ";
            return con.Consultas(query);
        }


        public DataTable obtenerTelefono()
        {
            string query = "select t.*, tipo.VchTipoTelefono from Telefono t JOIN TipoTelefono tipo ON t.UidTipoTelefono=tipo.UidTipoTelefono";
            return con.Consultas(query);
        }


        public DataTable obtenerUsuario(Guid Uidusuario)
        {
            string query = "select VchNombre, VchApellidoPaterno, VchUsuario from Usuario where UidUsuario ='" + Uidusuario + "'";
            return con.Consultas(query);
        }
        public DataTable obtenerUsuarioseleccionado(Guid Uidusuario)
        {
            string query = "select * from Usuario where UidUsuario ='" + Uidusuario + "'";
            return con.Consultas(query);
        }

        public DataTable obtenerEncargadoPerfilSucursalseleccionado(Guid Uidusuario)
        {
            string query = "select * from UsuarioPerfilSucursal where UidUsuario ='" + Uidusuario + "'";
            return con.Consultas(query);
        }


        public DataTable Busquedas(SqlCommand Comando)
        {
            return con.Busquedas(Comando);
        }




        //Perfil

        public DataTable obtenerPerfiles()
        {
            string query = "select * from Perfil where VchPerfil not like'%Administrador%' order by IntJerarquia";
            return con.Consultas(query);
        }

        
        public DataTable obtenerTipoTelefono()
        {
            string query = "select * from TipoTelefono";
            return con.Consultas(query);
        }

        public DataTable obtenerPerfileSuperAdministrador()
        {
            string query = "select * from Perfil where VchPerfil='" + "Superadministrador" + "'";
            return con.Consultas(query);
        }


        public DataTable obtenerPerfiles(Guid PidUsuario)
        {
            string query = "select * from Perfil where UidPerfil ='" + PidUsuario + "'";
            return con.Consultas(query);
        }
        public DataTable obtenerPerfilSeleccionado(Guid UidPerfil)
        {
            string query = "select * from Perfil where UidUsuario ='" + UidPerfil + "'";
            return con.Consultas(query);
        }

        //Status

        public DataTable obtenerStatus()
        {
            string query = "select * from Estatus";
            return con.Consultas(query);
        }

        

        public DataTable obtenerStatus(Guid UidStatus)
        {
            string query = "select * from Estatus where UidStatus ='" + UidStatus + "'";
            return con.Consultas(query);
        }
        public DataTable obtenerStatusSeleccionado(Guid UidStatus)
        {
            string query = "select * from Estatus where UidUsuario ='" + UidStatus + "'";
            return con.Consultas(query);
        }
        public void EliminaImagenEmpresa(string UidEncargado)
        {
            string Query = "delete VchRutaImagen from Usuario where UidUsuario='"+UidEncargado+"'";
            con.Consultas(Query);
        }
    }
}