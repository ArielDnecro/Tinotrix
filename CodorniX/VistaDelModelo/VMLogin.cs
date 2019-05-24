using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.Modelo;
using System.Data;
using System.Data.SqlClient;
using CodorniX.ConexionDB;

namespace CodorniX.VistaDelModelo
{
    public class VMLogin
    {
        DBLogin login = new DBLogin();

        Permisos _CPermisos = new Permisos();
        public Permisos CPermisos
        {
            get { return _CPermisos; }
            set { _CPermisos = value; }
        }

       
        public Guid IniciarSesion(string usuario)//quite password 171118
        {
            Guid id = new Guid();
            foreach (DataRow item in login.obtenerUsuarioLogin(usuario).Rows)
            {
               id=new Guid( item["UidUsuario"].ToString());
            }
            return id;
        }
        public Guid Perfiles(Guid usuario)
        {
            Guid id = new Guid();
            foreach (DataRow item in login.obtenerperfiles(usuario).Rows)
            {
                id = new Guid(item["UidPerfil"].ToString());
            }
            return id;
        }


        //public Guid Permisos(Guid perfil)
        //{
        //    Guid id = new Guid();
        //    foreach (DataRow item in login.obtenerpermisos(perfil).Rows)
        //    {
        //        id = new Guid(item["MidModulo"].ToString());
        //    }
        //    return id;
        //}


        public string perfil(string idUsuario)
        {
            string NombrePerfil = "";
            foreach (DataRow item in login.obtenerPerfilLogin(idUsuario).Rows)
            {
                NombrePerfil = item["VchPerfil"].ToString();
            }
            return NombrePerfil;
        }

        public string empresa(string uidperfil)
        {
            string NombrePerfil = "";
            foreach (DataRow item in login.obtenerEmpresaPerfil(uidperfil).Rows)
            {
                NombrePerfil = item["VchNombreComercial"].ToString();
            }
            return NombrePerfil;
        }


        public Guid idperfil(string nombreperfil)
        {
            Guid NombrePerfil = Guid.Empty;
            foreach (DataRow item in login.obteneridperfil(nombreperfil).Rows)
            {
                NombrePerfil =new Guid( item["UidPerfil"].ToString());
            }
            return NombrePerfil;
        }

        public List<Permisos> LtsPermisos = new List<Permisos>();
        public void CargarListaDePermisos(Guid perfil)
        {
            foreach (DataRow item in login.obtenerpermisos(perfil).Rows)
            {
                CPermisos = new Permisos()
                {
                    UIDPERFIL = new Guid(item["UidPerfil"].ToString()),
                    UidModulo = new Guid(item["UidModulo"].ToString())

                };
                LtsPermisos.Add(CPermisos);
            }
        }


    }
}