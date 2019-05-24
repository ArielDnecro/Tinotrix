using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodorniX.VistaDelModelo;
using CodorniX.Modelo;
using CodorniX.Util;

namespace CodorniX.Vista
{
    public partial class Site1 : System.Web.UI.MasterPage
    {
        VMLogin _CVMLogin = new VMLogin();
        Login lo = new Login();
        string uidinicioturno;
        string horainicio;
        string horafin;
        string numero;
        string fechainicio;
        Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            ScriptManager.ScriptResourceMapping.AddDefinition("jquery", new ScriptResourceDefinition()
            {
                Path = "~/Scripts/jquery-3.1.1.min.js",
                DebugPath = "~/Scripts/jquery-3.1.1.js",
            });

            if (Session["Sesion"] == null)
            {
                Response.Redirect("Login.aspx", false);
                return;
            }

            string strperfil = "";
            strperfil = Acceso.ObtenerAppWeb(SesionActual.uidPerfilActual.Value);

            Guid idPerfil = Guid.Empty;
            idPerfil = SesionActual.uidPerfilActual.Value;

            Usuario usuario = new Usuario.Repository().Find(new Guid(SesionActual.uidUsuario.ToString()));
            lblUsuario.Text = usuario.STRNOMBRE + " " + usuario.STRAPELLIDOPATERNO;

            UpdateNavbar();
        }

        protected void btnCerrarSession_Click(object sender, EventArgs e)
        {
            Session["Sesion"] = null;
            Response.Redirect("Login.aspx", false);
        }

        public void ActivarAdministrador()
        {
            activoAdmin.Attributes["class"] = "active";
        }

        public void ActivarEmpresa()
        {
            activoEmpresa.Attributes["class"] = "active";
        }
        public void ActivarSucursal()
        {
            activoSucursales.Attributes["class"] = "active";
        }
        public void ActivarDatosTarea()
        {
            //ActivoDatosTareas.Attributes["class"] = "active";
        }
        public void ActivarPerfiles()
        {
            activoPerfiles.Attributes["class"] = "active";
        }

        public void Backsite()
        {
            // Show all
        }

        public void Backend()
        {
            activoEmpresa.Visible = false;
            activoAdmin.Visible = false;
            menuEmpresas.Visible = false;
        }

        public void Frontend()
        {
            activoEmpresa.Visible = false;
            activoAdmin.Visible = false;
            menuEmpresas.Visible = false;
            activoSucursales.Visible = false;
            activoEncargados.Visible = false;
            menuSucursales.Visible = false;
        }

        public void UpdateNavbar()
        {
            string strperfil = Acceso.ObtenerAppWeb(SesionActual.uidPerfilActual.Value);
            Empresa empresa = new Empresa.Repository().Find(SesionActual.uidEmpresaActual.GetValueOrDefault(Guid.Empty));
            if (empresa != null)
            {
                activoSucursales.Visible = true;
                empresaActual.Visible = true;
                lblEmpresa.Text = empresa.StrNombreComercial;
            }
            else
            {
                menuSucursales.Visible = false;
                activoSucursales.Visible = false;
                empresaActual.Visible = false;
            }

            Sucursal sucursal = new Sucursal.Repository().Find(SesionActual.uidSucursalActual.GetValueOrDefault(Guid.Empty));
            if (sucursal != null)
            {
                sucursalActual.Visible = true;
                lblSucursal.Text = sucursal.StrNombre;
            }
            else
            {
                sucursalActual.Visible = false;
            }

            if (strperfil == "Backsite")
            {
                Backsite();
            }
            else if (strperfil == "Backend")
            {
                Backend();
            }
            else if (strperfil == "Frontend")
            {
                Frontend();
               
            }
        }

       
        protected void btnHome_Click(object sender, EventArgs e)
        {
            Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
        }
       
    }
}