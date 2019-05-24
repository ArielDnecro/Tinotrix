using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodorniX.Vista
{
    public partial class Home : System.Web.UI.Page
    {
        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SesionActual == null)
                return;

            if (!Acceso.TieneAccesoAModulo("Home", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }
        }
    }
}