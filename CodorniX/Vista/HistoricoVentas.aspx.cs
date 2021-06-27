using CodorniX.Modelo;
using CodorniX.Util;
using CodorniX.VistaDelModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodorniX.Vista
{
    public partial class HistoricoVentas : System.Web.UI.Page
    {
        #region Variables Historico generales
         VMHistoricoVentas VM = new VMHistoricoVentas();

        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        private bool EditingMode
        {
            get
            {
                if (ViewState["EditingMode"] == null)
                    return false;

                return (bool)ViewState["EditingMode"];
            }
            set
            {
                ViewState["EditingMode"] = value;
            }
        }
       
        #endregion Variables Historico generales
        protected void Page_Load(object sender, EventArgs e)
        {
            //FUImagen.Attributes["onchange"] = "upload(this)";
            if (SesionActual == null)
                return;

            if (!Acceso.TieneAccesoAModulo("HistoricoVentas", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }

            if (!IsPostBack)
            {
                Site1 master = (Site1)Master;
                master.ActivarHistoricoVentas();
                //lblErrorTelefono.Visible = false;
                //lblErrorDireccion.Visible = false;
                //lblErrorSucursal.Visible = false;

                //ActivarCamposDatos(false);
                //ActivarCamposDireccion(false);
                #region Botones de aceptar eliminar 
                //lblAceptarEliminarDireccion.Visible = false;
                //btnCancelarEliminarDireccion.Visible = false;
                //btnAceptarEliminarDireccion.Visible = false;

                //btnAceptarEliminarTelefono.Visible = false;
                //btnCancelarEliminarTelefono.Visible = false;
                //lblAceptarEliminarTelefono.Visible = false;

                //btnAceptarEliminarImpresora.Visible = false;
                //btnCancelarEliminarImpresora.Visible = false;
                //lblAceptarEliminarImpresora.Visible = false;

                //btnAceptarEliminarFoto.Visible = false;
                //btnCancelarEliminarFoto.Visible = false;
                //lblAceptarEliminarFoto.Visible = false;
                #endregion Botones de aceptar eliminar 

                #region btns
                //btnEncargados.Enabled = false;
                //btnEncargados.CssClass = "btn btn-default disabled btn-sm";

                btnMostrarBusqueda.Text = "Ocultar";
                btnBorrarBusqueda.Visible = true;
                btnBorrarBusqueda.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnBuscar.Visible = true;
                btnBuscar.RemoveCssClass("hidden").RemoveCssClass("disabled");

                //btnAgregarDireccion.AddCssClass("disabled");
                //btnAgregarTelefono.AddCssClass("disabled");
                //btnEditarSucursal.Enabled = false;
                //btnEditarSucursal.AddCssClass("disabled");
                #endregion btns

                #region Dgv (Grid views)
                //dgvSucursales.Visible = false;
                //dgvSucursales.AddCssClass("hidden");
                //dgvSucursales.DataSource = null;
                //dgvSucursales.DataBind();

                //dgvDirecciones.DataSource = null;
                //dgvDirecciones.DataBind();

                //dgvTelefonos.DataSource = null;
                //dgvTelefonos.DataBind();

                //dgvImpresoras.DataSource = null;
                //dgvImpresoras.DataBind();

                //dgvFotos.DataSource = null;
                //dgvFotos.DataBind();

                //dvgFotosPapel.DataSource = null;
                //dvgFotosPapel.DataBind();

                //dgvLicencias.DataSource = null;
                //dgvLicencias.DataBind();

                #endregion Dgv (Grid views)
            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "initializeDatapicker", "enableDatapicker()", true);
        }

        protected void dgvHistoricos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void dgvHistoricos_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvHistoricos_Sorting(object sender, GridViewSortEventArgs e)
        {

        }

        protected void dgvHistoricos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }
    }
}