using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.ConexionDB;
using CodorniX.Modelo;
using System.Data;
using System.Data.SqlClient;

namespace CodorniX.VistaDelModelo
{
    public class VMPermisos
    {
        DBPermisos conexion = new DBPermisos();
        private Permisos _Permisos;
        public Permisos Permisos
        {
            get { return _Permisos; }
            set { _Permisos = value; }
        }

        private Perfil _Perfil;
        public Perfil Perfil
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }

        private Modulo _Modulo;

        public Modulo Modulo
        {
            get { return _Modulo; }
            set { _Modulo = value; }
        }

        public List<Permisos> lstPermisos = new List<Permisos>();
        public List<Perfil> lstPerfil = new List<Perfil>();
        public List<Modulo> lstModulo = new List<Modulo>();

        public void CargarListaDePermisos(Guid idperfil)
        {
            foreach (DataRow item in conexion.obtenerPerfilPermisos(idperfil).Rows)
            {
                Permisos = new Permisos()
                {
                    UIDPERFIL = new Guid(item["UidUsuario"].ToString())
                    
                };
                lstPermisos.Add(Permisos);
            }
        }

    }
        
}