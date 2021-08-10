using CodorniX.Modelo;
using CodorniX.Util;
using CodorniX.VistaDelModelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using CodorniX.Common;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Globalization;
using System.Windows;
using System.Threading;

namespace CodorniX.Vista
{
    public partial class FotografiasComercial : System.Web.UI.Page
    {
        #region Variables Sucursales generales

        VMSucursales VM = new VMSucursales();
        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }
        private List<SucursalFotoC> FotoCRemoved
        {
            get
            {
                if (ViewState["FotoCRemoved"] == null)
                    ViewState["FotoCRemoved"] = new List<SucursalFotoC>();

                return (List<SucursalFotoC>)ViewState["FotoCRemoved"];
            }
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

        #endregion Variables Sucursales generales

        #region Private methods

        private void ActivarCamposDatos(bool enable)
        {
            if (enable)
            {
               

                btnEditarSucursal.AddCssClass("disabled");
                btnEditarSucursal.Enabled = false;

                btnOkSucursal.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnOkSucursal.Enabled = true;

                btnCancelarSucursal.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnCancelarSucursal.Enabled = true;

                dgvFotosC.Enabled = true;

                EditingMode = true;
            }
            else
            {
                btnEditarSucursal.RemoveCssClass("disabled");
                btnEditarSucursal.Enabled = true;

                btnOkSucursal.AddCssClass("disabled").AddCssClass("hidden");
                btnOkSucursal.Enabled = false;

                btnCancelarSucursal.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarSucursal.Enabled = false;

                dgvFotosC.Enabled = false;

                EditingMode = false;
            }
        }

        #endregion Private methods

        #region Constructor
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (SesionActual == null)
                return;

            if (!Acceso.TieneAccesoAModulo("Sucursales", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }

            if (!IsPostBack)
            {
                Site1 master = (Site1)Master;
                master.ActivarFotografiasComercial();

                ActivarCamposDatos(false);

                #region Botones de aceptar eliminar 

                btnAceptarEliminarFotoC.Visible = false;
                btnCancelarEliminarFotoC.Visible = false;
                lblAceptarEliminarFotoC.Visible = false;

                #endregion Botones de aceptar eliminar 

                #region btns
                btnEncargados.Enabled = false;
                btnEncargados.CssClass = "btn btn-default disabled btn-sm";

                btnMostrarBusqueda.Text = "Ocultar";
                btnBorrarBusqueda.Visible = true;
                btnBorrarBusqueda.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnBuscar.Visible = true;
                btnBuscar.RemoveCssClass("hidden").RemoveCssClass("disabled");

                btnEditarSucursal.Enabled = false;
                btnEditarSucursal.AddCssClass("disabled");
                #endregion btns

                #region Dgv (Grid views)
                dgvSucursales.Visible = false;
                dgvSucursales.AddCssClass("hidden");
                dgvSucursales.DataSource = null;
                dgvSucursales.DataBind();

                dgvFotosC.DataSource = null;
                dgvFotosC.DataBind();

                dvgFotosPapelC.DataSource = null;
                dvgFotosPapelC.DataBind();

                #endregion Dgv (Grid views)

                #region Obtener listas de DropDownList (dd)
                //-----------------------------------------------------------------------------
                VM.ObtenerMedidas();

                ddMedidaFotoC.DataSource = VM.Medidas;
                ddMedidaFotoC.DataValueField = "UidMedida";
                ddMedidaFotoC.DataTextField = "VchMedida";
                ddMedidaFotoC.DataBind();
                
                //---------------------------------------------------------------------------------
                VM.ObtenerStatus();

                ddActivoFotoC.DataSource = VM.ListaStatus;
                ddActivoFotoC.DataValueField = "UidStatus";
                ddActivoFotoC.DataTextField = "StrStatus";
                ddActivoFotoC.DataBind();

                lbStatus.DataSource = VM.ListaStatus;
                lbStatus.DataValueField = "UidStatus";
                lbStatus.DataTextField = "StrStatus";
                lbStatus.DataBind();
                //---------------------------------------------------------------------------------

                VM.ObtenerTipoSucursales();

                lbTipoSucursal.DataSource = VM.TipoSucursales;
                lbTipoSucursal.DataValueField = "UidTipoSucursal";
                lbTipoSucursal.DataTextField = "StrTipoSucursal";
                lbTipoSucursal.DataBind();
                //---------------------------------------------------------------------------------

                #endregion Obtener listas de DropDownList (dd)

            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), "initializeDatapicker", "enableDatapicker()", true);
        }
        #endregion Constructor

        #region Panel izquierdo
        protected void dgvSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvSucursales, "Select$" + e.Row.RowIndex);
            }


        }
        protected void dgvSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEncargados.Enabled = true;
            btnEncargados.CssClass = "btn btn-default btn-sm";
            Guid uid = new Guid(dgvSucursales.SelectedDataKey.Value.ToString());
            VM.ObtenerSucursal(uid);
            Guid? anterior = SesionActual.uidSucursalActual;
            if (anterior == null || !anterior.Equals(VM.Sucursal.UidSucursal))
            {
                SesionActual.uidSucursalActual = VM.Sucursal.UidSucursal;
                Site1 master = (Site1)Page.Master;
                master.UpdateNavbar();
            }

            #region Rellenar datos generales
            ActivarCamposDatos(false);
            btnEditarSucursal.RemoveCssClass("disabled");
            btnEditarSucursal.Enabled = true;

            uidSucursal.Text = VM.Sucursal.UidSucursal.ToString();

            int pos = -1;
            if (ViewState["SucursalPreviousRow"] != null)
            {
                pos = (int)ViewState["SucursalPreviousRow"];
                GridViewRow previousRow = dgvSucursales.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["SucursalPreviousRow"] = dgvSucursales.SelectedIndex;
            dgvSucursales.SelectedRow.AddCssClass("success");

            #endregion Rellenar datos generales

            #region Rellenar Impresoras
            VM.ObtenerImpresoras();
            ViewState["Impresoras"] = VM.Impresoras;
            #endregion Rellenar Impresoras

            #region Rellenar fotos Comerciales
            // VM.ObtenerImpresoras();
            if (VM.Impresoras.Count >= 1)
            {
                ddImpresoraFotoC.DataSource = ViewState["Impresoras"];
                ddImpresoraFotoC.DataValueField = "UidImpresora";
                ddImpresoraFotoC.DataTextField = "StrDescripcion";
                ddImpresoraFotoC.DataBind();
                ddImpresoraFotoC.SelectedIndex = 0;
            }
            else
            {
                ddImpresoraFotoC.DataSource = null;
                ddImpresoraFotoC.Items.Clear();
                ddImpresoraFotoC.DataBind();
            }
            //---------------------------------------------------------------------------------

            DesHabilitarFormularioFotografiasC();
            //---------------------------------------------------------------------------------
            VM.ObtenerfotosC();

            ViewState["FotosC"] = VM.FotosC;
            FotoCRemoved.Clear();
            DatabindFotografiasC();

            #endregion Rellenar fotos comerciales

            #region Rellenar Papel comerciales
            VM.ObtenerPapelC(VM.Sucursal.UidSucursal);

            if (VM.PapelC.UidPapel != Guid.Empty)
            {
                UidPapelC.Text = VM.PapelC.UidPapel.ToString();
                //ViewState["Papel"] = VM.Papel;
            }

            txtNombrePapelC.Text = VM.PapelC.StrDescripcion.ToString();
            txtAltoPapelC.Text = VM.PapelC.VchAlto.ToString();
            txtAnchoPapelC.Text = VM.PapelC.VchAncho.ToString();
            txtMargenSuperiorC.Text = VM.PapelC.VchSuperior.ToString();
            txtMargenInferiorC.Text = VM.PapelC.VchInferior.ToString();
            txtMargenDerechoC.Text = VM.PapelC.VchDerecho.ToString();
            txtMargenIzquierdoC.Text = VM.PapelC.VchIzquierdo.ToString();
            DataBindFotografiasPapelC();
            DesHabilitarFormularioFotoPapelC();
            DesHabilitarFormularioPapelC();
            LimpiarFormularioFotoPapelC();
            btnOKFotoPapelC.Visible = false;
            btnCancelarFotoPapelC.Visible = false;
            #endregion Rellenar Papel comerciales

            PnErrorSucursal.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorSucursal.Text = "";
        }
       
        private void SortSucursal(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            List<Sucursal> empresas = (List<Sucursal>)ViewState["Sucursales"];

            if (SortExpression == (string)ViewState["SortColumn"] && !same)
            {
                // We are resorting the same column, so flip the sort direction
                SortDirection =
                    ((SortDirection)ViewState["SortColumnDirection"] == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            if (SortExpression == "Nombre")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    empresas = empresas.OrderBy(x => x.StrNombre).ToList();
                }
                else
                {
                    empresas = empresas.OrderByDescending(x => x.StrNombre).ToList();
                }
            }
            else if (SortExpression == "TipoSucursal")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    empresas = empresas.OrderBy(x => x.StrTipoSucursal).ToList();
                }
                else
                {
                    empresas = empresas.OrderByDescending(x => x.StrTipoSucursal).ToList();
                }
            }
            else
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    empresas = empresas.OrderBy(x => x.DtFechaRegistro).ToList();
                }
                else
                {
                    empresas = empresas.OrderByDescending(x => x.DtFechaRegistro).ToList();
                }
            }
            dgvSucursales.DataSource = empresas;
            ViewState["SortColumn"] = SortExpression;
            ViewState["SortColumnDirection"] = SortDirection;
        }
        protected void dgvSucursales_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Invalidate Last position
            ViewState["SucursalPreviousRow"] = null;
            SortSucursal(e.SortExpression, e.SortDirection);

            dgvSucursales.DataBind();
        }
        protected void dgvSucursales_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Invalidate Last Position
            ViewState["SucursalPreviousRow"] = null;
            if (ViewState["SortColumn"] != null && ViewState["SortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["SortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["SortColumnDirection"];
                SortSucursal(SortExpression, SortDirection, true);
            }
            else
            {
                dgvSucursales.DataSource = ViewState["Sucursales"];
            }
            dgvSucursales.PageIndex = e.NewPageIndex;
            dgvSucursales.DataBind();
        }
        protected void btnBorrarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusquedaNombre.Text = string.Empty;
            lbTipoSucursal.ClearSelection();
            txtBusquedaRegistradoAntes.Text = string.Empty;
            txtBusquedaRegistradoDespues.Text = string.Empty;
            lbStatus.ClearSelection();
        }
        protected void btnBuscar_Click(object sender, EventArgs e)//19/10/17
        {  //variables
            String uids = "";
            seccionBusqueda.Visible = false;
            Guid status = new Guid();
            string nombreComercial = txtBusquedaNombre.Text;

            if (lbStatus.SelectedValue != "")
            {
                status = new Guid(lbStatus.SelectedValue);
            }
            //botones
            btnMostrarBusqueda.Text = "Mostrar";
            btnBorrarBusqueda.Visible = false;
            btnBuscar.Visible = false;


            foreach (int pos in lbTipoSucursal.GetSelectedIndices())
            {
                if (uids.Length > 0)
                    uids += "," + lbTipoSucursal.Items[pos].Value;
                else
                    uids += lbTipoSucursal.Items[pos].Value;
            }

            #region datetime
            DateTime? registradoDespues = null;
            try
            {
                registradoDespues = Convert.ToDateTime(txtBusquedaRegistradoDespues.Text);
            }
            catch (FormatException)
            {

            }
            DateTime? registradoAntes = null;
            try
            {
                registradoAntes = Convert.ToDateTime(txtBusquedaRegistradoAntes.Text);
            }

            catch (FormatException)
            {

            }
            #endregion datetime

            //busqueda de sucursales
            try
            {
                VM.BuscarSucursales(nombreComercial, registradoDespues, registradoAntes, uids, SesionActual.uidEmpresaActual.Value, status);
            }
            catch (Exception et)
            {
                LbErrorSucursales.Text = "Error de sesion \n cargando ....\n" + et;
                Response.Redirect("Sucursales.aspx");
            }
            #region Actualizar GridViews
            ViewState["Sucursales"] = VM.Sucursales;
            dgvSucursales.DataSource = VM.Sucursales;
            dgvSucursales.DataBind();
            dgvSucursales.Visible = true;
            dgvSucursales.RemoveCssClass("hidden");

            dgvFotosC.DataSource = null;
            dgvFotosC.DataBind();
            #endregion Actualizar GridViews

        }
        protected void btnMostrarBusqueda_Click(object sender, EventArgs e)
        {
            if (btnMostrarBusqueda.Text == "Mostrar")
            {
                dgvSucursales.Visible = false;
                dgvSucursales.AddCssClass("hidden");

                seccionBusqueda.Visible = true;

                btnBorrarBusqueda.Visible = true;
                btnBuscar.Visible = true;

                btnMostrarBusqueda.Text = "Ocultar";
            }
            else
            {
                dgvSucursales.Visible = true;
                dgvSucursales.RemoveCssClass("hidden");

                seccionBusqueda.Visible = false;

                btnBorrarBusqueda.Visible = false;
                btnBuscar.Visible = false;

                btnMostrarBusqueda.Text = "Mostrar";
            }
        }
        #endregion



        #region Panel derecho (edicion)
        protected void btnEditarSucursal_Click(object sender, EventArgs e)
        {
            try
            {
                ActivarCamposDatos(true);
               
                List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
                if (impresoras.Count >= 1)
                {
                    btnAgregarFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");//05/10/17
                    btnAgregarFotoC.Enabled = true;
                }
                else
                {
                    btnAgregarFotoC.AddCssClass("disabled");
                }
               
                if (uidFotoC.Text.Length > 0)
                {
                    btnEditarFotoC.Enable();
                }
                btnEditarPapelC.Enable();

                DataBindFotografiasPapelC();
                if (Guid.Empty != new Guid(DdlFotoC.SelectedValue.ToString()) && !String.IsNullOrWhiteSpace(DdlFotoC.SelectedValue.ToString()))
                { }
                else { UidFotoPapelC.Text = ""; }
                if (!String.IsNullOrWhiteSpace(UidFotoPapelC.Text) && Guid.Empty != new Guid(UidFotoPapelC.Text))
                {
                    btnEditarFotoPapelC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoPapelC.Enabled = true; btnEditarFotoPapelC.Visible = true;
                    btnOKFotoPapelC.Visible = false;
                    btnCancelarFotoPapelC.Visible = false;
                }

                PnErrorSucursal.Visible = false;
                lblErrorSucursal.Visible = false;
                lblErrorSucursal.Text = "";
            }
            catch (Exception x)
            {
                PnErrorSucursal.Visible = true;
                lblErrorSucursal.Visible = true;
                lblErrorSucursal.Text = "Error al editar la sucursal: \n detalle del error...\n" + x;
            }

        }
        protected void btnOkSucursal_Click(object sender, EventArgs e)
        {
            try
            {

                PnErrorSucursal.Visible = false;
                lblErrorSucursal.Visible = false;
                lblErrorSucursal.Text = "";

               
                //PnErrorSucursal.Visible = false;
                lblErrorSucursal.Visible = true;
                Sucursal empresa = null;
                if (!string.IsNullOrWhiteSpace(uidSucursal.Text))
                {
                    VM.ObtenerSucursal(new Guid(uidSucursal.Text));
                    empresa = VM.Sucursal;
                }
                else
                {
                    empresa = new Sucursal();
                }


                empresa.UidEmpresa = SesionActual.uidEmpresaActual.Value;



                if (ValidarCamposPapelC() == false)
                {
                    return;
                }

                #region Papel comercial
                SucursalPapelC PapelC = new SucursalPapelC();
                //SucursalPapel Papel = (SucursalPapel)ViewState["Papel"];

                //if (!string.IsNullOrWhiteSpace(UidPapelC.Text))
                //{
                    VM.ObtenerPapelC(new Guid(uidSucursal.Text));
                    PapelC = VM.PapelC;
                    PapelC.UidPapel = new Guid(UidPapelC.Text);
                //}
                //else
                //{

                //    PapelC.UidPapel = empresa.UidSucursal;
                //}


                PapelC.StrDescripcion = txtNombrePapelC.Text;
                PapelC.VchAlto = txtAltoPapelC.Text;
                PapelC.VchAncho = txtAnchoPapelC.Text;
                PapelC.VchSuperior = txtMargenSuperiorC.Text;
                PapelC.VchInferior = txtMargenInferiorC.Text;
                PapelC.VchDerecho = txtMargenDerechoC.Text;
                PapelC.VchIzquierdo = txtMargenIzquierdoC.Text;

                VM.GuardarPapelC(PapelC);

                LimpiarFormularioPapelC();
                DesHabilitarFormularioPapelC();
                LimpiarFormularioFotoPapelC();
                DesHabilitarFormularioFotoPapelC();
                dvgFotosPapelC.DataSource = null;
                dvgFotosPapelC.DataBind();
                #endregion Papel comercial

                #region Sucursales
                ActivarCamposDatos(false);
                VM.ObtenerSucursales(SesionActual.uidEmpresaActual.Value);
                dgvSucursales.DataSource = VM.Sucursales;
                dgvSucursales.DataBind();
                #endregion Sucursales

                #region Impresoras

                List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
                int NoImpresoras = impresoras.Count;

                #endregion impresoras

                #region Fotos comerciales


                if (NoImpresoras >= 1)
                {
                    //Begin Fotografias
                    List<SucursalFotoC> fotosC = (List<SucursalFotoC>)ViewState["FotosC"];
                    VM.GuardarFotosC(fotosC, empresa.UidSucursal);
                    VM.EliminarFotosC(FotoCRemoved);
                    //End Fotografias
                }

                dgvFotosC.DataSource = null;
                dgvFotosC.DataBind();
                ddImpresoraFotoC.DataSource = null;
                ddImpresoraFotoC.DataBind();
                ddImpresoraFotoC.Items.Clear();
                btnAgregarFotoC.AddCssClass("disabled");
                btnEditarFotoC.AddCssClass("disabled");
                //btnEliminarFoto.AddCssClass("disabled");
                btnOKFotoC.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarFotoC.AddCssClass("disabled").AddCssClass("hidden");
                LimpiarFormularioFotografiasC();
                #endregion Fotos

                //btnOkSucursal.AddCssClass("disabled").AddCssClass("hidden");
                //btnOkSucursal.Enabled = false;
                //btnCancelarSucursal.AddCssClass("disabled").AddCssClass("hidden");
                //btnCancelarSucursal.Enabled = false;
                ActivarCamposDatos(false);
                btnEditarSucursal.Disable();

                PnErrorSucursal.Visible = false;
                lblErrorSucursal.Visible = false;
                lblErrorSucursal.Text = "";
            }
            catch (Exception x)
            {
                PnErrorSucursal.Visible = true;
                lblErrorSucursal.Visible = true;
                lblErrorSucursal.Text = "Error al editar la sucursal: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarSucursal_Click(object sender, EventArgs e)
        {
            try
            {
                #region Sucursales
                ActivarCamposDatos(false);
                PnErrorSucursal.Visible = false;
                lblErrorSucursal.Visible = false;
                lblErrorSucursal.Text = "";

                #endregion Sucursales

                #region Papel comercial
                //ActivarCamposDatos(false);
                lblErrorPapelC.Visible = false;
                lblErrorPapelC.Text = "";
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
                //FUImagen.Enabled = false;
                //frmGrpNombre.RemoveCssClass("has-error");
                LimpiarFormularioPapelC();
                DesHabilitarFormularioPapelC();
                LimpiarFormularioFotoPapelC();
                DesHabilitarFormularioFotoPapelC();
                btnEditarPapelC.Disable();

                #endregion Papel comercial

                #region Fotos comerciales
                //DesActivarValidacionFotografias();
                DesHabilitarFormularioFotografiasC();

                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";

                btnAgregarFotoC.AddCssClass("disabled");
                btnEditarFotoC.AddCssClass("disabled");
                //btnEliminarFoto.AddCssClass("disabled");

                btnOKFotoC.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarFotoC.AddCssClass("disabled").AddCssClass("hidden");

                txtDescripcionFotoC.Text = string.Empty;
                txtPrecioFotoC.Text = string.Empty;
                txtPrecioFotoTicketC.Text = string.Empty;
                txtPrecioFotoServidorC.Text = string.Empty;
                txtAltoFotoC.Text = string.Empty;
                txtAnchoFotoC.Text = string.Empty;
                txtAltoFotoDescC.Text = string.Empty;
                txtAnchoFotoDescC.Text = string.Empty;
                //ddActivo.SelectedIndex = 0;
                //ddTipoImpresora.SelectedIndex = 0;


                btnCancelarEliminarFotoC_Click(sender, e);
                #endregion Fotos comerciales

                if (uidSucursal.Text.Length == 0)
                {
                    uidSucursal.Text = string.Empty;

                    dgvFotosC.DataSource = null;
                    dgvFotosC.DataBind();

                    dvgFotosPapelC.DataSource = null;
                    dvgFotosPapelC.DataBind();

                    btnEditarSucursal.Disable();

                }
                else
                {
                    VM.ObtenerSucursal(new Guid(uidSucursal.Text));
                    Session["SucursalActual"] = VM.Sucursal;
                    Label lblSucursal = (Label)Page.Master.FindControl("lblSucursal");
                    lblSucursal.Text = VM.Sucursal.StrNombre;
                    uidSucursal.Text = VM.Sucursal.UidSucursal.ToString();
                   
                    VM.ObtenerPapelC(VM.Sucursal.UidSucursal);

                    if (VM.PapelC.UidPapel != Guid.Empty)
                    {
                        UidPapelC.Text = VM.PapelC.UidPapel.ToString();
                        //ViewState["Papel"] = VM.Papel;
                    }

                    txtNombrePapelC.Text = VM.PapelC.StrDescripcion.ToString();
                    txtAltoPapelC.Text = VM.PapelC.VchAlto.ToString();
                    txtAnchoPapelC.Text = VM.PapelC.VchAncho.ToString();
                    txtMargenSuperiorC.Text = VM.PapelC.VchSuperior.ToString();
                    txtMargenInferiorC.Text = VM.PapelC.VchInferior.ToString();
                    txtMargenDerechoC.Text = VM.PapelC.VchDerecho.ToString();
                    txtMargenIzquierdoC.Text = VM.PapelC.VchIzquierdo.ToString();


                    VM.ObtenerfotosC();
                    ViewState["FotosC"] = VM.FotosC;
                    FotoCRemoved.Clear();
                    DatabindFotografiasC();

                }


                PnErrorSucursal.Visible = false;
                lblErrorSucursal.Visible = false;
                lblErrorSucursal.Text = "";
            }
            catch (Exception x)
            {
                PnErrorSucursal.Visible = true;
                lblErrorSucursal.Visible = true;
                lblErrorSucursal.Text = "Error al editar la sucursal: \n detalle del error...\n" + x;
            }

        }
        #endregion  Panel derecho (edicion)

        #region Panel derecho (tabs)

        protected void tabFotografiasC_Click(object sender, EventArgs e)
        {
            _tabFotoC();
        }
        void _tabFotoC()
        {
            panelFotosC.Visible = true;
            activeFotografiasC.Attributes["class"] = "active";

            panelPapelC.Visible = false;
            activePapelC.Attributes["class"] = "";

        }
        protected void tabPapelC_Click(object sender, EventArgs e)
        {
            _tabPapelC();
        }
        void _tabPapelC()
        {
            panelFotosC.Visible = false;
            activeFotografiasC.Attributes["class"] = "";

            panelPapelC.Visible = true;
            activePapelC.Attributes["class"] = "active";
        }

        #endregion Panel derecho (tabs)

        #region Panel derecho (Fotografias Comercial)
        public bool ValidarCamposFotoC()//
        {
            bool FotoBIen = true;

            #region vacios

            if (string.IsNullOrWhiteSpace(txtDescripcionFotoC.Text))
            {
                txtDescripcionFotoC.Focus();
                txtDescripcionFotoC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioFotoC.Text))
            {
                txtPrecioFotoC.Focus();
                txtPrecioFotoC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioFotoTicketC.Text))
            {
                txtPrecioFotoTicketC.Focus();
                txtPrecioFotoTicketC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioFotoServidorC.Text))
            {
                txtPrecioFotoServidorC.Focus();
                txtPrecioFotoServidorC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAltoFotoC.Text))
            {
                txtAltoFotoC.Focus();
                txtAltoFotoC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAnchoFotoC.Text))
            {
                txtAnchoFotoC.Focus();
                txtAnchoFotoC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAltoFotoDescC.Text))
            {
                txtAltoFotoDescC.Focus();
                txtAltoFotoDescC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAnchoFotoDescC.Text))
            {
                txtAnchoFotoDescC.Focus();
                txtAnchoFotoDescC.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }

            if (FotoBIen == false)
            {
                _tabFotoC();
                lblErrorFotoC.Text = "Ningun campo debe estar vacio";//va despues que tab papel
                lblErrorFotoC.Visible = true;
                PnErrorFotoCSucursal.Visible = true;
                return FotoBIen;
            }

            #endregion vacios
            lblErrorFotoC.Text = "";//va despues que tab papel
            lblErrorFotoC.Visible = false;
            PnErrorFotoCSucursal.Visible = false;
            #region Es numero

            //char[] charsRead = new char[txtAltoPapel.Text.Length];
            foreach (char c in txtPrecioFotoC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtPrecioFotoC.Focus();
                        txtPrecioFotoC.BorderColor = Color.FromName("#f00800");
                        ToolPrecioFotoC.HRef = "Solo debe contener 1 numero";
                        ToolPrecioFotoC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtPrecioFotoTicketC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtPrecioFotoTicketC.Focus();
                        txtPrecioFotoTicketC.BorderColor = Color.FromName("#f00800");
                        ToolPrecioTicketC.HRef = "Solo debe contener 1 numero";
                        ToolPrecioTicketC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtPrecioFotoServidorC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtPrecioFotoServidorC.Focus();
                        txtPrecioFotoServidorC.BorderColor = Color.FromName("#f00800");
                        ToolPrecioServidorC.HRef = "Solo debe contener 1 numero";
                        ToolPrecioServidorC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAltoFotoC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAltoFotoC.Focus();
                        txtAltoFotoC.BorderColor = Color.FromName("#f00800");
                        ToolAltoFotoC.HRef = "Solo debe contener 1 numero";
                        ToolAltoFotoC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAnchoFotoC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAnchoFotoC.Focus();
                        txtAnchoFotoC.BorderColor = Color.FromName("#f00800");
                        ToolAnchoFotoC.HRef = "Solo debe contener 1 numero";
                        ToolAnchoFotoC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAltoFotoDescC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAltoFotoDescC.Focus();
                        txtAltoFotoDescC.BorderColor = Color.FromName("#f00800");
                        ToolAltoFotoDescC.HRef = "Solo debe contener 1 numero";
                        ToolAltoFotoDescC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAnchoFotoDescC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAnchoFotoDescC.Focus();
                        txtAnchoFotoDescC.BorderColor = Color.FromName("#f00800");
                        ToolAnchoFotoDescC.HRef = "Solo debe contener 1 numero";
                        ToolAnchoFotoDescC.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            if (FotoBIen == false)
            {
                _tabFotoC();
                lblErrorFotoC.Text = "Todos los campos en formato correcto";
                lblErrorFotoC.Visible = true;
                PnErrorFotoCSucursal.Visible = true;
                return FotoBIen;
            }
            #endregion Es numero
            lblErrorFotoC.Text = "";//va despues que tab papel
            lblErrorFotoC.Visible = false;
            PnErrorFotoCSucursal.Visible = false;
           
            return FotoBIen;
        }
        protected void btnAgregarFotoC_Click(object sender, EventArgs e)//
        {
            try
            {
                //ActivarValidacionFotografias();
                uidFotoC.Text = string.Empty;

                LimpiarFormularioFotografiasC();
                HabilitarFormularioFotografiasC();

                btnOKFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnOKFotoC.Enabled = true;
                btnCancelarFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnCancelarFotoC.Enabled = true;

                btnAgregarFotoC.Disable();
                btnEditarFotoC.Disable();
                //btnEliminarFoto.Disable();

                int pos = -1;
                if (ViewState["FotoCPreviousRow"] != null)
                {
                    pos = (int)ViewState["FotoCPreviousRow"];
                    GridViewRow previousRow = dgvFotosC.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnEditarFotoC_Click(object sender, EventArgs e)//
        {
            try
            {
                HabilitarFormularioFotografiasC();

                btnAgregarFotoC.Enabled = false;
                btnAgregarFotoC.AddCssClass("disabled");

                btnEditarFotoC.Enabled = false;
                btnEditarFotoC.AddCssClass("disabled");

                //btnEliminarFoto.Enabled = false;
                //btnEliminarFoto.AddCssClass("disabled");

                btnOKFotoC.Enabled = true;
                btnOKFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");

                btnCancelarFotoC.Enabled = true;
                btnCancelarFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#VConfimacionEditarFotoC", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#VConfimacionEditarFoto').hide();", true);


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnEliminarFotoC_Click(object sender, EventArgs e)//
        {
            lblAceptarEliminarFotoC.Visible = true;
            lblAceptarEliminarFotoC.Text = "¿Desea eliminar La foto seleccionada?";
            btnAceptarEliminarFotoC.Visible = true;
            btnCancelarEliminarFotoC.Visible = true;
        }
        protected void btnOKFotoC_Click(object sender, EventArgs e)//
        {
            try
            {

                if (ValidarCamposFotoC() == false)
                {
                    return;
                }
                ToolPrecioFotoC.HRef = "";
                ToolPrecioFotoC.Visible = false;
                ToolPrecioTicketC.HRef = "";
                ToolPrecioTicketC.Visible = false;
                ToolPrecioServidorC.HRef = "";
                ToolPrecioServidorC.Visible = false;
                ToolAltoFotoC.HRef = "";
                ToolAltoFotoC.Visible = false;
                ToolAnchoFotoC.HRef = "";
                ToolAnchoFotoC.Visible = false;
                ToolAltoFotoDescC.HRef = "";
                ToolAltoFotoDescC.Visible = false;
                ToolAnchoFotoDescC.HRef = "";
                ToolAnchoFotoDescC.Visible = false;
                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                SucursalFotoC foto = null;
                int pos = -1;
                if (!string.IsNullOrWhiteSpace(uidFotoC.Text))
                {
                    IEnumerable<SucursalFotoC> dir = from i in fotos where i.UidFoto.ToString() == uidFotoC.Text select i;
                    foto = dir.First();
                    pos = fotos.IndexOf(foto);
                    fotos.Remove(foto);
                }
                else
                {
                    foto = new SucursalFotoC();
                    foto.UidFoto = Guid.NewGuid();
                }
                //a partir de aqui agrega los datos al objeto
                foto.UidImpresora = new Guid(ddImpresoraFotoC.SelectedValue);
                foto.StrDescripcion = txtDescripcionFotoC.Text;
                foto.StrPrecio = txtPrecioFotoC.Text;
                foto.StrPrecioTicket = txtPrecioFotoTicketC.Text;
                foto.StrPrecioServidor = txtPrecioFotoServidorC.Text;
                foto.VchAlto = txtAltoFotoC.Text;
                foto.VchAncho = txtAnchoFotoC.Text;
                foto.VchAltoDesc = txtAltoFotoDescC.Text;
                foto.VchAnchoDesc = txtAnchoFotoDescC.Text;
                foto.UidStatus = new Guid(ddActivoFotoC.SelectedValue);
                foto.StrStatus = ddActivoFotoC.SelectedItem.Text;
                foto.BooRotarEnPapel = false;
                foto.VchColumna = "";
                foto.VchFila = "";
                foto.UidMedida = new Guid(ddMedidaFotoC.SelectedValue);
                foto.VchMedida = ddMedidaFotoC.SelectedItem.Text;
                if (pos < 0)
                    fotos.Add(foto);
                else
                    fotos.Insert(pos, foto);


                ViewState["FotosC"] = fotos;
                DatabindFotografiasC();
                DataBindFotografiasPapelC();
                LimpiarFormularioFotografiasC();
                DesHabilitarFormularioFotografiasC();
                btnOKFotoC.AddCssClass("hidden").AddCssClass("disabled");
                btnOKFotoC.Enabled = false;
                btnCancelarFotoC.AddCssClass("hidden").AddCssClass("disabled");
                btnCancelarFotoC.Enabled = false;
                btnEditarFotoC.AddCssClass("disabled").AddCssClass("hidden");
                btnEditarFotoC.Enabled = false;
                btnAgregarFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnAgregarFotoC.Enabled = true;


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarFotoC_Click(object sender, EventArgs e)//
        {
            try
            {

                DesHabilitarFormularioFotografiasC();

                btnOKFotoC.AddCssClass("hidden").AddCssClass("disabled");
                btnOKFotoC.Enabled = false;
                btnCancelarFotoC.AddCssClass("hidden").AddCssClass("disabled");
                btnCancelarFotoC.Enabled = false;

                btnAgregarFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnAgregarFotoC.Enabled = true;


                if (uidFotoC.Text.Length == 0)
                {
                    btnEditarFotoC.Disable();
                    LimpiarFormularioFotografiasC();
                }
                else
                {
                    //btnEliminarFoto.Enable();
                    btnEditarFotoC.Enable();

                    List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                    SucursalFotoC foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dgvFotosC.SelectedDataKey.Value.ToString()).First();

                    uidFotoC.Text = foto.UidFoto.ToString();

                    txtDescripcionFotoC.Text = foto.StrDescripcion;
                    txtPrecioFotoC.Text = foto.StrPrecio;
                    txtPrecioFotoTicketC.Text = foto.StrPrecioTicket;
                    txtPrecioFotoServidorC.Text = foto.StrPrecioServidor;
                    txtAltoFotoC.Text = foto.VchAlto.ToString();
                    txtAnchoFotoC.Text = foto.VchAncho.ToString();
                    txtAltoFotoDescC.Text = foto.VchAltoDesc.ToString();
                    txtAnchoFotoDescC.Text = foto.VchAnchoDesc.ToString();
                    txtFxColumnaC.Text = foto.VchColumna;
                    txtFxFilaC.Text = foto.VchFila;
                    CbRotarImagenPapelC.Checked = foto.BooRotarEnPapel;

                    ddActivoFotoC.SelectedValue = foto.UidStatus.ToString();
                    ddMedidaFotoC.SelectedValue = foto.UidMedida.ToString();
                }


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnAceptarEliminarFotoC_Click(object sender, EventArgs e)//
        {
            try
            {

                btnAgregarFotoC.Enabled = true;
                btnAgregarFotoC.RemoveCssClass("disabled");

                btnOKFotoC.Enabled = false;
                btnOKFotoC.AddCssClass("hidden").AddCssClass("disabled");

                btnCancelarFotoC.Enabled = false;
                btnCancelarFotoC.AddCssClass("hidden").AddCssClass("disabled");

                Guid uid = new Guid(uidFotoC.Text);

                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                SucursalFotoC foto = fotos.Select(x => x).Where(x => x.UidFoto == uid).First();
                fotos.Remove(foto);
                FotoCRemoved.Add(foto);

                LimpiarFormularioFotografiasC();

                ViewState["FotosC"] = fotos;
                DatabindFotografiasC();

                btnCancelarEliminarFotoC.Visible = false;
                btnAceptarEliminarFotoC.Visible = false;
                lblAceptarEliminarFotoC.Visible = false;
                ViewState["FotoCPreviousRow"] = null;


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarEliminarFotoC_Click(object sender, EventArgs e)//
        {
            try
            {
                //esta funcion parece ser llamada por otras
                btnCancelarEliminarFotoC.Visible = false;
                btnAceptarEliminarFotoC.Visible = false;
                lblAceptarEliminarFotoC.Visible = false;


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void dgvFotosC_RowDataBound(object sender, GridViewRowEventArgs e)//
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvFotosC, "Select$" + e.Row.RowIndex);
                }


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void dgvFotosC_SelectedIndexChanged(object sender, EventArgs e)//
        {
            try
            {

                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                SucursalFotoC foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dgvFotosC.SelectedDataKey.Value.ToString()).First();

                uidFotoC.Text = foto.UidFoto.ToString();
                ddImpresoraFotoC.SelectedValue = foto.UidImpresora.ToString();
                txtDescripcionFotoC.Text = foto.StrDescripcion;
                txtPrecioFotoC.Text = foto.StrPrecio;
                txtPrecioFotoTicketC.Text = foto.StrPrecioTicket;
                txtPrecioFotoServidorC.Text = foto.StrPrecioServidor;
                txtAltoFotoC.Text = foto.VchAlto.ToString();
                txtAnchoFotoC.Text = foto.VchAncho.ToString();
                txtAltoFotoDescC.Text = foto.VchAltoDesc.ToString();
                txtAnchoFotoDescC.Text = foto.VchAnchoDesc.ToString();
                ddActivoFotoC.SelectedValue = foto.UidStatus.ToString();//no se si se necesite seccionar tambien la uid
                ddMedidaFotoC.SelectedValue = foto.UidMedida.ToString();


                if (EditingMode)
                {
                    btnEditarFotoC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoC.Enabled = true;
                    //btnEliminarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    //btnEliminarFoto.Enabled = true;
                    btnOKFotoC.AddCssClass("disabled").AddCssClass("hidden");
                    btnOKFotoC.Enabled = false;
                    btnCancelarFotoC.AddCssClass("disabled").AddCssClass("hidden");
                    btnCancelarFotoC.Enabled = false;
                }

                int pos = -1;
                if (ViewState["FotoCPreviousRow"] != null)
                {
                    pos = (int)ViewState["FotoCPreviousRow"];
                    GridViewRow previousRow = dgvFotosC.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }

                ViewState["FotoCPreviousRow"] = dgvFotosC.SelectedIndex;
                dgvFotosC.SelectedRow.AddCssClass("success");


                PnErrorFotoCSucursal.Visible = false;
                lblErrorFotoC.Visible = false;
                lblErrorFotoC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoCSucursal.Visible = true;
                lblErrorFotoC.Visible = true;
                lblErrorFotoC.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }
        }
        void LimpiarFormularioFotografiasC()//
        {
            //solo son texbox
            uidFotoC.Text = string.Empty;
            txtDescripcionFotoC.Text = string.Empty;
            txtPrecioFotoC.Text = string.Empty;
            txtPrecioFotoTicketC.Text = string.Empty;
            txtPrecioFotoServidorC.Text = string.Empty;
            txtAltoFotoC.Text = string.Empty;
            txtAnchoFotoC.Text = string.Empty;
            txtAltoFotoDescC.Text = string.Empty;
            txtAnchoFotoDescC.Text = string.Empty;
            ddActivoFotoC.SelectedIndex = 0;
            ddMedidaFotoC.SelectedIndex = 0;
        }
        void DesHabilitarFormularioFotografiasC()//
        {
            if (ddImpresoraFotoC.DataSource != null)
            {
                ddImpresoraFotoC.SelectedIndex = 0;
            }
            ddImpresoraFotoC.AddCssClass("disabled");
            ddImpresoraFotoC.Enabled = false;

            txtDescripcionFotoC.Enabled = false;
            txtDescripcionFotoC.AddCssClass("disabled");

            txtPrecioFotoC.Enabled = false;
            txtPrecioFotoC.AddCssClass("disabled");

            txtPrecioFotoTicketC.Enabled = false;
            txtPrecioFotoTicketC.AddCssClass("disabled");

            txtPrecioFotoServidorC.Enabled = false;
            txtPrecioFotoServidorC.AddCssClass("disabled");

            txtAltoFotoC.Enabled = false;
            txtAltoFotoC.AddCssClass("disabled");

            txtAnchoFotoC.Enabled = false;
            txtAnchoFotoC.AddCssClass("disabled");

            txtAltoFotoDescC.Enabled = false;
            txtAltoFotoDescC.AddCssClass("disabled");

            txtAnchoFotoDescC.Enabled = false;
            txtAnchoFotoDescC.AddCssClass("disabled");

            ddActivoFotoC.SelectedIndex = 0;
            ddActivoFotoC.AddCssClass("disabled");
            ddActivoFotoC.Enabled = false;

            ddMedidaFotoC.SelectedIndex = 0;
            ddMedidaFotoC.AddCssClass("disabled");
            ddMedidaFotoC.Enabled = false;


        }
        void HabilitarFormularioFotografiasC()//
        {
            ddImpresoraFotoC.SelectedIndex = 0;
            ddImpresoraFotoC.RemoveCssClass("disabled");
            ddImpresoraFotoC.Enabled = true;

            txtDescripcionFotoC.Enabled = true;
            txtDescripcionFotoC.RemoveCssClass("disabled");

            txtPrecioFotoC.Enabled = true;
            txtPrecioFotoC.RemoveCssClass("disabled");

            txtPrecioFotoTicketC.Enabled = true;
            txtPrecioFotoTicketC.RemoveCssClass("disabled");

            txtPrecioFotoServidorC.Enabled = true;
            txtPrecioFotoServidorC.RemoveCssClass("disabled");

            txtAltoFotoC.Enabled = true;
            txtAltoFotoC.RemoveCssClass("disabled");

            txtAnchoFotoC.Enabled = true;
            txtAnchoFotoC.RemoveCssClass("disabled");

            txtAltoFotoDescC.Enabled = true;
            txtAltoFotoDescC.RemoveCssClass("disabled");

            txtAnchoFotoDescC.Enabled = true;
            txtAnchoFotoDescC.RemoveCssClass("disabled");

            ddActivoFotoC.SelectedIndex = 0;
            ddActivoFotoC.RemoveCssClass("disabled");
            ddActivoFotoC.Enabled = true;

            ddMedidaFotoC.SelectedIndex = 0;
            ddMedidaFotoC.RemoveCssClass("disabled");
            ddMedidaFotoC.Enabled = true;

        }
        void DatabindFotografiasC()//
        {
            List<SucursalFotoC> Fotos = (List<SucursalFotoC>)ViewState["FotosC"];

            dgvFotosC.DataSource = Fotos;
            dgvFotosC.DataBind();
        }
        #endregion Panel derecho (Fotografias Comercial)

        #region Panel derecho (Papel Comercial)
        void DesHabilitarFormularioPapelC()//
        {
            txtNombrePapelC.Enabled = false;
            txtAltoPapelC.Enabled = false;
            txtAnchoPapelC.Enabled = false;
            txtMargenSuperiorC.Enabled = false;
            txtMargenInferiorC.Enabled = false;
            txtMargenDerechoC.Enabled = false;
            txtMargenIzquierdoC.Enabled = false;
        }
        void DesHabilitarFormularioFotoPapelC()//
        {

            CbRotarImagenPapelC.AddCssClass("disabled");
            CbRotarImagenPapelC.Enabled = false;

            txtFxFilaC.Enabled = false;
            txtFxFilaC.AddCssClass("disabled");

            txtFxColumnaC.Enabled = false;
            txtFxColumnaC.AddCssClass("disabled");
        }
        void HabilitarFormularioPapelC()//
        {
            txtNombrePapelC.Enabled = true;
            txtAltoPapelC.Enabled = true;
            txtAnchoPapelC.Enabled = true;
            txtMargenSuperiorC.Enabled = true;
            txtMargenInferiorC.Enabled = true;
            txtMargenDerechoC.Enabled = true;
            txtMargenIzquierdoC.Enabled = true;


        }
        void HabilitarFormularioFotoPapelC()//
        {

            CbRotarImagenPapelC.RemoveCssClass("disabled");
            CbRotarImagenPapelC.Enabled = true;
            txtFxFilaC.Enabled = true;
            txtFxFilaC.RemoveCssClass("disabled");
            txtFxColumnaC.Enabled = true;
            txtFxColumnaC.RemoveCssClass("disabled");
        }
        void LimpiarFormularioPapelC()//
        {
            txtNombrePapelC.Text = "";
            txtAltoPapelC.Text = "";
            txtAnchoPapelC.Text = "";
            txtMargenSuperiorC.Text = "";
            txtMargenInferiorC.Text = "";
            txtMargenDerechoC.Text = "";
            txtMargenIzquierdoC.Text = "";

            txtNombrePapelC.BorderColor = Color.FromName("#FF3580BF");
            txtAltoPapelC.BorderColor = Color.FromName("#FF3580BF");
            txtAnchoPapelC.BorderColor = Color.FromName("#FF3580BF");
            txtMargenSuperiorC.BorderColor = Color.FromName("#FF3580BF");
            txtMargenInferiorC.BorderColor = Color.FromName("#FF3580BF");
            txtMargenDerechoC.BorderColor = Color.FromName("#FF3580BF");
            txtMargenIzquierdoC.BorderColor = Color.FromName("#FF3580BF");

            lblErrorPapelC.Text = "";
            lblErrorPapelC.Visible = false;

            ToolAltoPapelC.Visible = false;
            ToolAnchoPapelC.Visible = false;
            ToolMSuperiorC.Visible = false;
            ToolMInferiorC.Visible = false;
            ToolMDerechoC.Visible = false;
            ToolMIzquierdoC.Visible = false;

            ToolAltoPapelC.HRef = "";
            ToolAnchoPapelC.HRef = "";
            ToolMSuperiorC.HRef = "";
            ToolMInferiorC.HRef = "";
            ToolMDerechoC.HRef = "";
            ToolMIzquierdoC.HRef = "";
        }
        void LimpiarFormularioFotoPapelC()//
        {

            txtFxColumnaC.Text = string.Empty;
            txtFxFilaC.Text = string.Empty;
            CbRotarImagenPapelC.Checked = false;

            lblErrorFotoPapelC.Text = "";
            lblErrorFotoPapelC.Visible = false;

        }
        public bool ValidarCamposPapelC()//
        {
            bool PapelBIen = true;

            #region vacios


            if (string.IsNullOrWhiteSpace(txtNombrePapelC.Text))
            {
                txtNombrePapelC.Focus();
                txtNombrePapelC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }

            if (string.IsNullOrWhiteSpace(txtAltoPapelC.Text))
            {
                txtAltoPapelC.Focus();
                txtAltoPapelC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAnchoPapelC.Text))
            {
                txtAnchoPapelC.Focus();
                txtAnchoPapelC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenSuperiorC.Text))
            {
                txtMargenSuperiorC.Focus();
                txtMargenSuperiorC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenInferiorC.Text))
            {
                txtMargenInferiorC.Focus();
                txtMargenInferiorC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenDerechoC.Text))
            {
                txtMargenDerechoC.Focus();
                txtMargenDerechoC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenIzquierdoC.Text))
            {
                txtMargenIzquierdoC.Focus();
                txtMargenIzquierdoC.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (PapelBIen == false)
            {
                _tabPapelC();
                lblErrorPapelC.Text = "Ningun campo debe estar vacio";//va despues que tab papel
                lblErrorPapelC.Visible = true;
                PnErrorPapelCSucursal.Visible = true;
                return PapelBIen;
            }

            #endregion vacios
            lblErrorPapelC.Text = "";//va despues que tab papel
            lblErrorPapelC.Visible = false;
            PnErrorPapelCSucursal.Visible = false;
            #region Es numero

            //char[] charsRead = new char[txtAltoPapel.Text.Length];
            foreach (char c in txtAltoPapelC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtAltoPapelC.Focus();
                    txtAltoPapelC.BorderColor = Color.FromName("#f00800");
                    ToolAltoPapelC.HRef = "Solo debe contener 1 numero";
                    ToolAltoPapelC.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtAnchoPapelC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtAnchoPapelC.Focus();
                    txtAnchoPapelC.BorderColor = Color.FromName("#f00800");
                    ToolAnchoPapelC.HRef = "Solo debe contener 1 numero";
                    ToolAnchoPapelC.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenSuperiorC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenSuperiorC.Focus();
                    txtMargenSuperiorC.BorderColor = Color.FromName("#f00800");
                    ToolMSuperiorC.HRef = "Solo debe contener 1 numero";
                    ToolMSuperiorC.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenInferiorC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenInferiorC.Focus();
                    txtMargenInferiorC.BorderColor = Color.FromName("#f00800");
                    ToolMInferiorC.HRef = "Solo debe contener 1 numero";
                    ToolMInferiorC.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenDerechoC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenDerechoC.Focus();
                    txtMargenDerechoC.BorderColor = Color.FromName("#f00800");
                    ToolMDerechoC.HRef = "Solo debe contener 1 numero";
                    ToolMDerechoC.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenIzquierdoC.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenIzquierdoC.Focus();
                    txtMargenIzquierdoC.BorderColor = Color.FromName("#f00800");
                    ToolMIzquierdoC.HRef = "Solo debe contener 1 numero";
                    ToolMIzquierdoC.Visible = true;
                    PapelBIen = false;
                }
            }
            if (PapelBIen == false)
            {
                _tabPapelC();
                lblErrorPapelC.Text = "Todos los campos en formato correcto";//va despues que tab papel
                lblErrorPapelC.Visible = true;
                PnErrorPapelCSucursal.Visible = true;
                return PapelBIen;
            }
            #endregion Es numero
            lblErrorPapelC.Text = "";//va despues que tab papel
            lblErrorPapelC.Visible = false;
            PnErrorPapelCSucursal.Visible = false;
            #region digitos
            if (txtAltoPapelC.Text.Length < 3)
            {
                txtAltoPapelC.Focus();
                txtAltoPapelC.BorderColor = Color.FromName("#f00800");
                ToolAltoPapelC.HRef = "Minimo debe ser 1 numero de 3 digitos";
                ToolAltoPapelC.Visible = true;
                PapelBIen = false;
            }
            if (txtAnchoPapelC.Text.Length < 3)
            {
                txtAnchoPapelC.Focus();
                txtAnchoPapelC.BorderColor = Color.FromName("#f00800");
                ToolAnchoPapelC.HRef = "Minimo debe ser 1 numero de 3 digitos";
                ToolAnchoPapelC.Visible = true;
                PapelBIen = false;
            }
            if (PapelBIen == false)
            {
                _tabPapelC();
                lblErrorPapelC.Text = "Alto y Ancho deben tener al menos 3 digitos";//va despues que tab papel
                lblErrorPapelC.Visible = true;
                PnErrorPapelCSucursal.Visible = true;
                return PapelBIen;
            }
            #endregion digitos
            lblErrorPapelC.Text = "";//va despues que tab papel
            lblErrorPapelC.Visible = false;
            PnErrorPapelCSucursal.Visible = false;
            return PapelBIen;
        }
        void DataBindFotografiasPapelC()//
        {

            List<SucursalFotoC> Fotos = (List<SucursalFotoC>)ViewState["FotosC"];
            List<SucursalFotoC> FotosX = new List<SucursalFotoC>();
            SucursalFotoC fotox = new SucursalFotoC(); fotox.StrDescripcion = "[Selecciona]"; FotosX.Add(fotox);

            // txtCantMaqLicencia.Text ="EnuOrden: "+ Licencias[0].EnuOrden.ToString() + " StrOrdenaPor: " + Licencias[0].StrOrdenaPor.ToString();
            dvgFotosPapelC.DataSource = Fotos;
            dvgFotosPapelC.DataBind();
            int cant = dvgFotosPapelC.Rows.Count - 1; //el menos 1 es debido porque en el gridview se maneja a partir del 0 y  Licencias.Count a partir del 1
            for (int i = 0; i <= cant; i++) // i comienza desde 0 porque recorre todo el gridview y el gridview comienza desde 0 igual que el Array aunque utilizando el count lo obtienes comenzando a partir del 1
            {
                if (Fotos[i].VchFila == "" && Fotos[i].VchColumna == "" && Fotos[i].BooRotarEnPapel == false)
                {
                    dvgFotosPapelC.Rows[i].Visible = false;
                    FotosX.Add(Fotos[i]);
                }
                else
                {
                    if (Fotos[i].BooRotarEnPapel == false)
                    {
                        ((Label)dvgFotosPapelC.Rows[i].FindControl("lbFotoRotadoPapel_icon")).Visible = false;
                        ((Label)dvgFotosPapelC.Rows[i].FindControl("lbFotoNoRotadoPapel_icon")).Visible = true;
                    }
                    else
                    {
                        ((Label)dvgFotosPapelC.Rows[i].FindControl("lbFotoRotadoPapel_icon")).Visible = true;
                        ((Label)dvgFotosPapelC.Rows[i].FindControl("lbFotoNoRotadoPapel_icon")).Visible = false;
                    }
                }
            }

            DdlFotoC.DataSource = FotosX;
            DdlFotoC.DataValueField = "UidFoto";
            DdlFotoC.DataTextField = "StrDescripcion";
            DdlFotoC.DataBind();
        }
        protected void dvgFotosPapelC_RowDataBound(object sender, GridViewRowEventArgs e)//
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dvgFotosPapelC, "Select$" + e.Row.RowIndex);
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {

                    switch (lbOrdenFPPorC.Text)
                    {
                        case "Descripcion":
                            if (lbOrdenFPC.Text == "ASC")
                            {
                                ((HtmlGenericControl)e.Row.FindControl("IcoDescripcionFP")).Attributes["class"] = Global.OrdenDescendente;
                            }
                            else
                            {
                                ((HtmlGenericControl)e.Row.FindControl("IcoDescripcionFP")).Attributes["class"] = Global.OrdenAscendente;
                            }
                            break;
                    }
                }

                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void dvgFotosPapelC_SelectedIndexChanged(object sender, EventArgs e)//
        {
            try
            {
                DataBindFotografiasPapelC();
                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                SucursalFotoC foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dvgFotosPapelC.SelectedDataKey.Value.ToString()).First();
                UidFotoPapelC.Text = foto.UidFoto.ToString();
                txtFxFilaC.Text = foto.VchFila.ToString();
                txtFxColumnaC.Text = foto.VchColumna.ToString();
                CbRotarImagenPapelC.Checked = foto.BooRotarEnPapel;
                DdlFotoC.Items.Insert(0, new ListItem(foto.StrDescripcion, foto.UidFoto.ToString()));
                btnEditarFotoPapelC.Text = "Editar";
                DesHabilitarFormularioFotoPapelC();
                if (EditingMode)
                {
                    btnEditarFotoPapelC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoPapelC.Enabled = true;
                    //btnEliminarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    //btnEliminarFoto.Enabled = true;
                    btnOKFotoPapelC.AddCssClass("disabled").AddCssClass("hidden");
                    btnOKFotoPapelC.Enabled = false;
                    btnCancelarFotoPapelC.AddCssClass("disabled").AddCssClass("hidden");
                    btnCancelarFotoPapelC.Enabled = false;
                }

                int pos = -1;
                if (ViewState["FotoCPreviousRow"] != null)
                {
                    pos = (int)ViewState["FotoCPreviousRow"];
                    GridViewRow previousRow = dvgFotosPapelC.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }

                ViewState["FotoCPreviousRow"] = dvgFotosPapelC.SelectedIndex;
                dvgFotosPapelC.SelectedRow.AddCssClass("success");


                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnEditarFotoPapelC_Click(object sender, EventArgs e)//
        {
            try
            {
                HabilitarFormularioFotoPapelC();
                btnEditarFotoPapelC.AddCssClass("disabled");
                btnCancelarFotoPapelC.Visible = true;
                btnOKFotoPapelC.Visible = true;
                btnCancelarFotoPapelC.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnOKFotoPapelC.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnOKFotoPapelC.Enabled = true;
                btnCancelarFotoPapelC.Enabled = true;

                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnOKFotoPapelC_Click(object sender, EventArgs e)//
        {
            try
            {
                if (ValidarCamposPapelC() == false)
                {
                    return;
                }
                ToolAltoPapelC.Visible = false;
                ToolAnchoPapelC.Visible = false;
                ToolMDerechoC.Visible = false;
                ToolMInferiorC.Visible = false;
                ToolMIzquierdoC.Visible = false;
                ToolMSuperiorC.Visible = false;
                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                SucursalFotoC foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == DdlFotoC.SelectedValue.ToString()).First();
                //dgvFotos.SelectedDataKey.Value.ToString()).First();
                double CEspdisponible = ConMMColumnaC(foto);
                double FEspdisponible = ConMMFilaC(foto);
                NumberFormatInfo punto = new NumberFormatInfo(); punto.NumberDecimalSeparator = ".";

                //tarea pendiente validar si estan vacios columna y fila
                if (CbRotarImagenPapelC.Checked == false)
                {
                    if (CEspdisponible - (double.Parse(txtFxColumnaC.Text, punto) * double.Parse(foto.VchAncho, punto)) <= 0)
                    {
                        txtFxColumnaC.BorderColor = Color.FromName("#f00800");
                        txtFxColumnaC.Focus();
                        ToolFxColumnaC.HRef = "Excede del espacio diponible";
                        ToolFxColumnaC.Visible = true;
                        return;
                    }
                    ToolFxColumnaC.HRef = "";
                    ToolFxColumnaC.Visible = false;
                    if (FEspdisponible - (double.Parse(txtFxFilaC.Text, punto) * double.Parse(foto.VchAlto, punto)) <= 0)
                    {
                        txtFxFilaC.BorderColor = Color.FromName("#f00800");
                        txtFxFilaC.Focus();
                        ToolFxFilaC.HRef = "Excede del espacio diponible";
                        ToolFxFilaC.Visible = true;
                        return;
                    }
                    ToolFxFilaC.HRef = "";
                    ToolFxFilaC.Visible = false;
                }
                else
                {
                    if (CEspdisponible - (double.Parse(txtFxColumnaC.Text, punto) * double.Parse(foto.VchAlto, punto)) <= 0)
                    {
                        txtFxColumnaC.BorderColor = Color.FromName("#f00800");
                        txtFxColumnaC.Focus();
                        ToolFxColumnaC.HRef = "Excede del espacio diponible";
                        ToolFxColumnaC.Visible = true;
                        return;
                    }
                    ToolFxColumnaC.HRef = "";
                    ToolFxColumnaC.Visible = false;
                    if (FEspdisponible - (double.Parse(txtFxFilaC.Text, punto) * double.Parse(foto.VchAncho, punto)) <= 0)
                    {
                        txtFxFilaC.BorderColor = Color.FromName("#f00800");
                        txtFxFilaC.Focus();
                        ToolFxFilaC.HRef = "Excede del espacio diponible";
                        ToolFxFilaC.Visible = true;
                        return;
                    }
                    ToolFxFilaC.HRef = "";
                    ToolFxFilaC.Visible = false;
                }
                lblErrorFotoC.Visible = true;
                //List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFotoC photo = null;
                int pos = -1;
                if (!string.IsNullOrWhiteSpace(UidFotoPapelC.Text))
                {
                    IEnumerable<SucursalFotoC> dir = from i in fotos where i.UidFoto.ToString() == DdlFotoC.SelectedValue.ToString() select i;
                    photo = dir.First();
                    pos = fotos.IndexOf(photo);
                    fotos.Remove(photo);
                }
                else
                {
                    photo = new SucursalFotoC();
                    photo.UidFoto = Guid.NewGuid();
                }
                photo.BooRotarEnPapel = CbRotarImagenPapelC.Checked;
                photo.VchColumna = txtFxColumnaC.Text;
                photo.VchFila = txtFxFilaC.Text;
                photo.UidFoto = new Guid(UidFotoPapelC.Text);
                photo.StrDescripcion = DdlFotoC.SelectedItem.Text;

                if (pos < 0)
                    fotos.Add(photo);
                else
                    fotos.Insert(pos, photo);

                ViewState["FotosC"] = fotos;
                DataBindFotografiasPapelC();
                LimpiarFormularioFotoPapelC();
                DesHabilitarFormularioFotoPapelC();
                btnOKFotoPapelC.AddCssClass("hidden").AddCssClass("disabled");
                btnOKFotoPapelC.Enabled = false;
                btnCancelarFotoPapelC.AddCssClass("hidden").AddCssClass("disabled");
                btnCancelarFotoPapelC.Enabled = false;
                btnEditarFotoPapelC.AddCssClass("disabled").AddCssClass("hidden");
                btnEditarFotoPapelC.Enabled = false;


                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarFotoPapelC_Click(object sender, EventArgs e)//
        {
            try
            {
                DesHabilitarFormularioFotoPapelC();
                btnEditarFotoPapelC.RemoveCssClass("disabled");
                btnCancelarFotoPapelC.Visible = false;
                btnOKFotoPapelC.Visible = false;

                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnEditarPapelC_Click(object sender, EventArgs e)//
        {
            try
            {
                btnEditarPapelC.AddCssClass("disabled");
                HabilitarFormularioPapelC();
                //HabilitarFormularioFotoPapel();
                LimpiarFormularioFotoPapelC();
                LimpiarFormularioPapelC();
                //btnOkPapel
                //btnCancelarPapel.
                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];


                foreach (SucursalFotoC f in fotos)
                {
                    f.VchColumna = "";
                    f.VchFila = "";
                    f.BooRotarEnPapel = false;
                }
                ViewState["FotosC"] = fotos;
                DataBindFotografiasPapelC();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#VConfimacionNuevoPapelC", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#VConfimacionNuevoPapelC').hide();", true);

                PnErrorPapelCSucursal.Visible = false;
                lblErrorPapelC.Visible = false;
                lblErrorPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorPapelCSucursal.Visible = true;
                lblErrorPapelC.Visible = true;
                lblErrorPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnOkPapelC_Click(object sender, EventArgs e)//
        {
            try
            {
                DesHabilitarFormularioPapelC();
                btnOkPapelC.Visible = false;
                btnCancelarPapelC.Visible = false;

                PnErrorPapelCSucursal.Visible = false;
                lblErrorPapelC.Visible = false;
                lblErrorPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorPapelCSucursal.Visible = true;
                lblErrorPapelC.Visible = true;
                lblErrorPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarPapelC_Click(object sender, EventArgs e)//NO HAY NADA DE PORSI
        {

        }
        protected void DdlFotoC_SelectedIndexChanged(object sender, EventArgs e)//
        {
            try
            {

                if (Guid.Empty != new Guid(DdlFotoC.SelectedValue.ToString()) && !String.IsNullOrWhiteSpace(DdlFotoC.SelectedValue.ToString()))
                {
                    if (EditingMode)
                    {
                        btnEditarFotoPapelC.RemoveCssClass("disabled").RemoveCssClass("hidden");
                        btnEditarFotoPapelC.Enabled = true;
                    }
                    UidFotoPapelC.Text = DdlFotoC.SelectedValue.ToString();
                }
                else
                {
                    btnEditarFotoPapelC.AddCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoPapelC.Enabled = false;
                    UidFotoPapelC.Text = "";
                }
                DesHabilitarFormularioFotoPapelC();
                btnEditarFotoPapelC.Text = "Nuevo";
                btnOKFotoPapelC.AddCssClass("disabled").AddCssClass("hidden");
                btnOKFotoPapelC.Enabled = false;
                btnCancelarFotoPapelC.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarFotoPapelC.Enabled = false;

                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        public double ConMMColumnaC(SucursalFotoC foto)//
        {
            double CEspDisponible;
            SucursalPapelC Papel1 = new SucursalPapelC();
            Papel1.VchAncho = txtAnchoPapelC.Text;
            Papel1.VchDerecho = txtMargenDerechoC.Text;
            Papel1.VchIzquierdo = txtMargenIzquierdoC.Text;

            double CEspDisponibleMilimetros = (double.Parse(Papel1.VchAncho) - (double.Parse(Papel1.VchDerecho) + double.Parse(Papel1.VchIzquierdo)));
            CEspDisponible = ConversionMedidaMilimetros(CEspDisponibleMilimetros, foto.VchMedida);

            return CEspDisponible;
        }
        public double ConMMFilaC(SucursalFotoC foto)
        {
            double FEspDisponible;
            SucursalPapelC Papel1 = new SucursalPapelC();
            Papel1.VchAlto = txtAltoPapelC.Text;
            Papel1.VchSuperior = txtMargenSuperiorC.Text;
            Papel1.VchInferior = txtMargenInferiorC.Text;
            double FEspDisponibleMilimetros = (double.Parse(Papel1.VchAlto) - (double.Parse(Papel1.VchInferior) + double.Parse(Papel1.VchSuperior)));
            FEspDisponible = ConversionMedidaMilimetros(FEspDisponibleMilimetros, foto.VchMedida);
            return FEspDisponible;
        }
        protected void dvgFotosPapelC_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                List<SucursalFotoC> fotos = (List<SucursalFotoC>)ViewState["FotosC"];
                if (e.SortExpression == lbOrdenFPPorC.Text)
                {
                    if (lbOrdenFPC.Text == Orden.ASC.ToString())
                    {
                        lbOrdenFPC.Text = Orden.DESC.ToString();
                    }
                    else
                    {
                        lbOrdenFPC.Text = Orden.ASC.ToString();
                    }
                }
                else
                {
                    lbOrdenFPPorC.Text = e.SortExpression;
                    lbOrdenFPC.Text = Orden.ASC.ToString();
                }
                Orden Ordenn = (Orden)Enum.Parse(typeof(Orden), lbOrdenFPC.Text, true);
                //var txt = (HtmlInputText)dvgFotosPapel.FindControl("txt");
                List<SucursalFotoC> fotosOrdenNueva = VM.OrdenarListaFPC(e.SortExpression, Ordenn, fotos);
                ViewState["FotosC"] = fotosOrdenNueva;
                DataBindFotografiasPapelC();

                PnErrorFotoPapelCSucursal.Visible = false;
                lblErrorFotoPapelC.Visible = false;
                lblErrorFotoPapelC.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelCSucursal.Visible = true;
                lblErrorFotoPapelC.Visible = true;
                lblErrorFotoPapelC.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }


        #endregion Panel derecho (Papel comercial)

        #region Otros
        public double ConversionMedidaMilimetros(double Dou, String StrMedida)
        {

            switch (StrMedida)
            {
                case "Centimetro":
                    Dou = Dou * 0.1;
                    break;
                case "Pulgada":
                    Dou = Dou * 0.0393701;
                    break;
                default:
                    Dou = Dou * 0.1;
                    break;
            }
            return Dou;
        }
        protected void btnEncargados_Click(object sender, EventArgs e)
        {
            Response.Redirect("Encargados.aspx");
        }
        #endregion Otros
    }
}