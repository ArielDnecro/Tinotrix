using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace CodorniX.Util
{
    public class Global : System.Web.HttpApplication
    {
        #region Clases bootstrap

        public static string BotonHabilitadoPequeño = "btn btn-default btn-sm";
        public static string BotonDeshabilitadoPequeño = "btn btn-default btn-sm disabled";
        public static string BotonActivoPequeño = "btn btn-default btn-sm active";
        public static string OrdenAscendente = "glyphicon glyphicon-circle-arrow-down";
        public static string OrdenDescendente = "glyphicon glyphicon-circle-arrow-up";

        #endregion Clases bootstrap
        protected void Application_Start(object sender, EventArgs e)
        {

        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}