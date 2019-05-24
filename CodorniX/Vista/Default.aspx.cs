using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodorniX.Vista
{
    public partial class Default : System.Web.UI.Page
    {
        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SesionActual == null)
                return;
        }
    }
}