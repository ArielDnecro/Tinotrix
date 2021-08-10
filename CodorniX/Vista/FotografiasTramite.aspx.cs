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
    public partial class FotografiasTramite : System.Web.UI.Page
    {

        #region Variables Sucursales generales

        VMSucursales VM = new VMSucursales();

        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        private List<SucursalFoto> FotoRemoved
        {
            get
            {
                if (ViewState["FotoRemoved"] == null)
                    ViewState["FotoRemoved"] = new List<SucursalFoto>();

                return (List<SucursalFoto>)ViewState["FotoRemoved"];
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

                dgvFotos.Enabled = true;

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

                dgvFotos.Enabled = false;

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
                master.ActivarFotografiasTramite();

                ActivarCamposDatos(false);

                #region Botones de aceptar eliminar 
                
                btnAceptarEliminarFoto.Visible = false;
                btnCancelarEliminarFoto.Visible = false;
                lblAceptarEliminarFoto.Visible = false;

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

                dgvFotos.DataSource = null;
                dgvFotos.DataBind();

                dvgFotosPapel.DataSource = null;
                dvgFotosPapel.DataBind();


                #endregion Dgv (Grid views)

                #region Obtener listas de DropDownList (dd)
                //-----------------------------------------------------------------------------
                VM.ObtenerMedidas();

                ddMedidaFoto.DataSource = VM.Medidas;
                ddMedidaFoto.DataValueField = "UidMedida";
                ddMedidaFoto.DataTextField = "VchMedida";
                ddMedidaFoto.DataBind();

                //-----------------------------------------------------------------------------

                VM.ObtenerStatus();


                ddActivoFoto.DataSource = VM.ListaStatus;
                ddActivoFoto.DataValueField = "UidStatus";
                ddActivoFoto.DataTextField = "StrStatus";
                ddActivoFoto.DataBind();

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

            #region Rellenar fotos
            VM.ObtenerImpresoras();
            if (VM.Impresoras.Count >= 1)
            {
                ddImpresoraFoto.DataSource = ViewState["Impresoras"];
                ddImpresoraFoto.DataValueField = "UidImpresora";
                ddImpresoraFoto.DataTextField = "StrDescripcion";
                ddImpresoraFoto.DataBind();
                ddImpresoraFoto.SelectedIndex = 0;
            }
            else
            {
                ddImpresoraFoto.DataSource = null;
                ddImpresoraFoto.Items.Clear();
                ddImpresoraFoto.DataBind();
            }
            //---------------------------------------------------------------------------------
            //btnAgregarFoto.AddCssClass("disabled");
            //btnAgregarFoto.Enabled = false;
            //txtDescripcionFoto.Enabled = false;
            //txtPrecioFoto.Enabled = false;
            //txtAltoFoto.Enabled = false;
            //txtAnchoFoto.Enabled = false;
            DesHabilitarFormularioFotografias();
            //---------------------------------------------------------------------------------

            //ddActivoFoto.SelectedIndex = 0;
            //---------------------------------------------------------------------------------
            VM.Obtenerfotos();

            ViewState["Fotos"] = VM.Fotos;
            FotoRemoved.Clear();
            //dgvFotos.DataSource = ViewState["Fotos"];
            //dgvFotos.DataBind();
            DatabindFotografias();

            #endregion Rellenar fotos

            #region Rellenar Papel
            VM.ObtenerPapel(VM.Sucursal.UidSucursal);

            if (VM.Papel.UidPapel != Guid.Empty)
            {
                UidPapel.Text = VM.Papel.UidPapel.ToString();
                //ViewState["Papel"] = VM.Papel;
            }

            txtNombrePapel.Text = VM.Papel.StrDescripcion.ToString();
            txtAltoPapel.Text = VM.Papel.VchAlto.ToString();
            txtAnchoPapel.Text = VM.Papel.VchAncho.ToString();
            txtMargenSuperior.Text = VM.Papel.VchSuperior.ToString();
            txtMargenInferior.Text = VM.Papel.VchInferior.ToString();
            txtMargenDerecho.Text = VM.Papel.VchDerecho.ToString();
            txtMargenIzquierdo.Text = VM.Papel.VchIzquierdo.ToString();
            DataBindFotografiasPapel();
            DesHabilitarFormularioFotoPapel();
            DesHabilitarFormularioPapel();
            LimpiarFormularioFotoPapel();
            btnOKFotoPapel.Visible = false;
            btnCancelarFotoPapel.Visible = false;
            #endregion Rellenar Papel

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

            dgvFotos.DataSource = null;
            dgvFotos.DataBind();
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
                    btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");//05/10/17
                    btnAgregarFoto.Enabled = true;
                }
                else
                {
                    btnAgregarFoto.AddCssClass("disabled");
                }

                if (uidFoto.Text.Length > 0)
                {
                    btnEditarFoto.Enable();
                }
                btnEditarPapel.Enable();

                DataBindFotografiasPapel();
                if (Guid.Empty != new Guid(DdlFoto.SelectedValue.ToString()) && !String.IsNullOrWhiteSpace(DdlFoto.SelectedValue.ToString()))
                { }
                else { UidFotoPapel.Text = ""; }
                if (!String.IsNullOrWhiteSpace(UidFotoPapel.Text) && Guid.Empty != new Guid(UidFotoPapel.Text))
                {
                    btnEditarFotoPapel.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoPapel.Enabled = true; btnEditarFotoPapel.Visible = true;
                    btnOKFotoPapel.Visible = false;
                    btnCancelarFotoPapel.Visible = false;
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



                if (ValidarCamposPapel() == false)
                {
                    return;
                }

                #region Papel 
                SucursalPapel Papel = new SucursalPapel();
                //SucursalPapel Papel = (SucursalPapel)ViewState["Papel"];

                //if (!string.IsNullOrWhiteSpace(UidPapel.Text))
                //{
                    VM.ObtenerPapel(new Guid(uidSucursal.Text));
                    Papel = VM.Papel;
                    Papel.UidPapel = new Guid(UidPapel.Text);
                //}
                //else
                //{

                //    Papel.UidPapel = empresa.UidSucursal;
                //}


                Papel.StrDescripcion = txtNombrePapel.Text;
                Papel.VchAlto = txtAltoPapel.Text;
                Papel.VchAncho = txtAnchoPapel.Text;
                Papel.VchSuperior = txtMargenSuperior.Text;
                Papel.VchInferior = txtMargenInferior.Text;
                Papel.VchDerecho = txtMargenDerecho.Text;
                Papel.VchIzquierdo = txtMargenIzquierdo.Text;

                VM.GuardarPapel(Papel);

                LimpiarFormularioPapel();
                DesHabilitarFormularioPapel();
                LimpiarFormularioFotoPapel();
                DesHabilitarFormularioFotoPapel();
                dvgFotosPapel.DataSource = null;
                dvgFotosPapel.DataBind();

                #endregion Papel

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
                    List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                    VM.GuardarFotos(fotos, empresa.UidSucursal);
                    VM.EliminarFotos(FotoRemoved);
                    //End Fotografias
                }

                dgvFotos.DataSource = null;
                dgvFotos.DataBind();
                ddImpresoraFoto.DataSource = null;
                ddImpresoraFoto.DataBind();
                ddImpresoraFoto.Items.Clear();
                btnAgregarFoto.AddCssClass("disabled");
                btnEditarFoto.AddCssClass("disabled");
                //btnEliminarFoto.AddCssClass("disabled");
                btnOKFoto.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarFoto.AddCssClass("disabled").AddCssClass("hidden");
                LimpiarFormularioFotografias();
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

                #region Papel 
                //ActivarCamposDatos(false);
                lblErrorPapel.Visible = false;
                lblErrorPapel.Text = "";
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
                //FUImagen.Enabled = false;
                //frmGrpNombre.RemoveCssClass("has-error");
                LimpiarFormularioPapel();
                DesHabilitarFormularioPapel();
                LimpiarFormularioFotoPapel();
                DesHabilitarFormularioFotoPapel();
                btnEditarPapel.Disable();

                #endregion Papel 

                #region Fotos 
                //DesActivarValidacionFotografias();
                DesHabilitarFormularioFotografias();

                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";

                btnAgregarFoto.AddCssClass("disabled");
                btnEditarFoto.AddCssClass("disabled");
                //btnEliminarFoto.AddCssClass("disabled");

                btnOKFoto.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarFoto.AddCssClass("disabled").AddCssClass("hidden");

                txtDescripcionFoto.Text = string.Empty;
                txtPrecioFoto.Text = string.Empty;
                txtPrecioFotoTicket.Text = string.Empty;
                txtPrecioFotoServidor.Text = string.Empty;
                txtAltoFoto.Text = string.Empty;
                txtAnchoFoto.Text = string.Empty;
                txtAltoFotoDesc.Text = string.Empty;
                txtAnchoFotoDesc.Text = string.Empty;
                //ddActivo.SelectedIndex = 0;
                //ddTipoImpresora.SelectedIndex = 0;


                btnCancelarEliminarFoto_Click(sender, e);
                #endregion Fotos 

                if (uidSucursal.Text.Length == 0)
                {
                    uidSucursal.Text = string.Empty;

                    dgvFotos.DataSource = null;
                    dgvFotos.DataBind();

                    dvgFotosPapel.DataSource = null;
                    dvgFotosPapel.DataBind();

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

                    if (VM.Papel.UidPapel != Guid.Empty)
                    {
                        UidPapel.Text = VM.Papel.UidPapel.ToString();
                        //ViewState["Papel"] = VM.Papel;
                    }

                    txtNombrePapel.Text = VM.Papel.StrDescripcion.ToString();
                    txtAltoPapel.Text = VM.Papel.VchAlto.ToString();
                    txtAnchoPapel.Text = VM.Papel.VchAncho.ToString();
                    txtMargenSuperior.Text = VM.Papel.VchSuperior.ToString();
                    txtMargenInferior.Text = VM.Papel.VchInferior.ToString();
                    txtMargenDerecho.Text = VM.Papel.VchDerecho.ToString();
                    txtMargenIzquierdo.Text = VM.Papel.VchIzquierdo.ToString();


                    VM.Obtenerfotos();
                    ViewState["Fotos"] = VM.Fotos;
                    FotoRemoved.Clear();
                    DatabindFotografias();

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

        protected void tabFotografias_Click(object sender, EventArgs e)
        {
            _tabFoto();
        }
        void _tabFoto()
        {
            panelFotos.Visible = true;
            activeFotografias.Attributes["class"] = "active";

            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";

        }
        protected void tabPapel_Click(object sender, EventArgs e)
        {
            _tabPapel();
        }
        void _tabPapel()
        {
            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelPapel.Visible = true;
            activePapel.Attributes["class"] = "active";
        }

        #endregion Panel derecho (tabs)

        #region Panel derecho (Fotografias)
        public bool ValidarCamposFoto()//
        {
            bool FotoBIen = true;

            #region vacios

            if (string.IsNullOrWhiteSpace(txtDescripcionFoto.Text))
            {
                txtDescripcionFoto.Focus();
                txtDescripcionFoto.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioFoto.Text))
            {
                txtPrecioFoto.Focus();
                txtPrecioFoto.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioFotoTicket.Text))
            {
                txtPrecioFotoTicket.Focus();
                txtPrecioFotoTicket.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtPrecioFotoServidor.Text))
            {
                txtPrecioFotoServidor.Focus();
                txtPrecioFotoServidor.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAltoFoto.Text))
            {
                txtAltoFoto.Focus();
                txtAltoFoto.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAnchoFoto.Text))
            {
                txtAnchoFoto.Focus();
                txtAnchoFoto.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAltoFotoDesc.Text))
            {
                txtAltoFotoDesc.Focus();
                txtAltoFotoDesc.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAnchoFotoDesc.Text))
            {
                txtAnchoFotoDesc.Focus();
                txtAnchoFotoDesc.BorderColor = Color.FromName("#f00800");
                FotoBIen = false;
            }

            if (FotoBIen == false)
            {
                _tabFoto();
                lblErrorFoto.Text = "Ningun campo debe estar vacio";//va despues que tab papel
                lblErrorFoto.Visible = true;
                PnErrorFotoSucursal.Visible = true;
                return FotoBIen;
            }

            #endregion vacios
            lblErrorFoto.Text = "";//va despues que tab papel
            lblErrorFoto.Visible = false;
            PnErrorFotoSucursal.Visible = false;
            #region Es numero

            //char[] charsRead = new char[txtAltoPapel.Text.Length];
            foreach (char c in txtPrecioFoto.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtPrecioFoto.Focus();
                        txtPrecioFoto.BorderColor = Color.FromName("#f00800");
                        ToolPrecioFoto.HRef = "Solo debe contener 1 numero";
                        ToolPrecioFoto.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtPrecioFotoTicket.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtPrecioFotoTicket.Focus();
                        txtPrecioFotoTicket.BorderColor = Color.FromName("#f00800");
                        ToolPrecioTicket.HRef = "Solo debe contener 1 numero";
                        ToolPrecioTicket.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtPrecioFotoServidor.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtPrecioFotoServidor.Focus();
                        txtPrecioFotoServidor.BorderColor = Color.FromName("#f00800");
                        ToolPrecioServidor.HRef = "Solo debe contener 1 numero";
                        ToolPrecioServidor.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAltoFoto.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAltoFoto.Focus();
                        txtAltoFoto.BorderColor = Color.FromName("#f00800");
                        ToolAltoFoto.HRef = "Solo debe contener 1 numero";
                        ToolAltoFoto.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAnchoFoto.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAnchoFoto.Focus();
                        txtAnchoFoto.BorderColor = Color.FromName("#f00800");
                        ToolAnchoFoto.HRef = "Solo debe contener 1 numero";
                        ToolAnchoFoto.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAltoFotoDesc.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAltoFotoDesc.Focus();
                        txtAltoFotoDesc.BorderColor = Color.FromName("#f00800");
                        ToolAltoFotoDesc.HRef = "Solo debe contener 1 numero";
                        ToolAltoFotoDesc.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            foreach (char c in txtAnchoFotoDesc.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {
                    if (c.ToString() != ".")
                    {
                        txtAnchoFotoDesc.Focus();
                        txtAnchoFotoDesc.BorderColor = Color.FromName("#f00800");
                        ToolAnchoFotoDesc.HRef = "Solo debe contener 1 numero";
                        ToolAnchoFotoDesc.Visible = true;
                        FotoBIen = false;
                    }
                }
            }
            if (FotoBIen == false)
            {
                _tabFoto();
                lblErrorFoto.Text = "Todos los campos en formato correcto";
                lblErrorFoto.Visible = true;
                PnErrorFotoSucursal.Visible = true;
                return FotoBIen;
            }
            #endregion Es numero
            lblErrorFoto.Text = "";//va despues que tab papel
            lblErrorFoto.Visible = false;
            PnErrorFotoSucursal.Visible = false;

            return FotoBIen;
        }
        protected void btnAgregarFoto_Click(object sender, EventArgs e)//
        {
            try
            {
                //ActivarValidacionFotografias();
                uidFoto.Text = string.Empty;

                LimpiarFormularioFotografias();
                HabilitarFormularioFotografias();

                btnOKFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnOKFoto.Enabled = true;
                btnCancelarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnCancelarFoto.Enabled = true;

                btnAgregarFoto.Disable();
                btnEditarFoto.Disable();
                //btnEliminarFoto.Disable();

                int pos = -1;
                if (ViewState["FotoPreviousRow"] != null)
                {
                    pos = (int)ViewState["FotoPreviousRow"];
                    GridViewRow previousRow = dgvFotos.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnEditarFoto_Click(object sender, EventArgs e)//
        {
            try
            {
                HabilitarFormularioFotografias();

                btnAgregarFoto.Enabled = false;
                btnAgregarFoto.AddCssClass("disabled");

                btnEditarFoto.Enabled = false;
                btnEditarFoto.AddCssClass("disabled");

                //btnEliminarFoto.Enabled = false;
                //btnEliminarFoto.AddCssClass("disabled");

                btnOKFoto.Enabled = true;
                btnOKFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");

                btnCancelarFoto.Enabled = true;
                btnCancelarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#VConfimacionEditarFoto", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#VConfimacionEditarFoto').hide();", true);


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnEliminarFoto_Click(object sender, EventArgs e)//
        {
            lblAceptarEliminarFoto.Visible = true;
            lblAceptarEliminarFoto.Text = "¿Desea eliminar La foto seleccionada?";
            btnAceptarEliminarFoto.Visible = true;
            btnCancelarEliminarFoto.Visible = true;
        }
        protected void btnOKFoto_Click(object sender, EventArgs e)//
        {
            try
            {

                if (ValidarCamposFoto() == false)
                {
                    return;
                }
                ToolPrecioFoto.HRef = "";
                ToolPrecioFoto.Visible = false;
                ToolPrecioTicket.HRef = "";
                ToolPrecioTicket.Visible = false;
                ToolPrecioServidor.HRef = "";
                ToolPrecioServidor.Visible = false;
                ToolAltoFoto.HRef = "";
                ToolAltoFoto.Visible = false;
                ToolAnchoFoto.HRef = "";
                ToolAnchoFoto.Visible = false;
                ToolAltoFotoDesc.HRef = "";
                ToolAltoFotoDesc.Visible = false;
                ToolAnchoFotoDesc.HRef = "";
                ToolAnchoFotoDesc.Visible = false;
                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFoto foto = null;
                int pos = -1;
                if (!string.IsNullOrWhiteSpace(uidFoto.Text))
                {
                    IEnumerable<SucursalFoto> dir = from i in fotos where i.UidFoto.ToString() == uidFoto.Text select i;
                    foto = dir.First();
                    pos = fotos.IndexOf(foto);
                    fotos.Remove(foto);
                }
                else
                {
                    foto = new SucursalFoto();
                    foto.UidFoto = Guid.NewGuid();
                }
                //a partir de aqui agrega los datos al objeto
                foto.UidImpresora = new Guid(ddImpresoraFoto.SelectedValue);
                foto.StrDescripcion = txtDescripcionFoto.Text;
                foto.StrPrecio = txtPrecioFoto.Text;
                foto.StrPrecioTicket = txtPrecioFotoTicket.Text;
                foto.StrPrecioServidor = txtPrecioFotoServidor.Text;
                foto.VchAlto = txtAltoFoto.Text;
                foto.VchAncho = txtAnchoFoto.Text;
                foto.VchAltoDesc = txtAltoFotoDesc.Text;
                foto.VchAnchoDesc = txtAnchoFotoDesc.Text;
                foto.UidStatus = new Guid(ddActivoFoto.SelectedValue);
                foto.StrStatus = ddActivoFoto.SelectedItem.Text;
                foto.BooRotarEnPapel = false;
                foto.VchColumna = "";
                foto.VchFila = "";
                foto.UidMedida = new Guid(ddMedidaFoto.SelectedValue);
                foto.VchMedida = ddMedidaFoto.SelectedItem.Text;
                if (pos < 0)
                    fotos.Add(foto);
                else
                    fotos.Insert(pos, foto);


                ViewState["Fotos"] = fotos;
                DatabindFotografias();
                DataBindFotografiasPapel();
                LimpiarFormularioFotografias();
                DesHabilitarFormularioFotografias();
                btnOKFoto.AddCssClass("hidden").AddCssClass("disabled");
                btnOKFoto.Enabled = false;
                btnCancelarFoto.AddCssClass("hidden").AddCssClass("disabled");
                btnCancelarFoto.Enabled = false;
                btnEditarFoto.AddCssClass("disabled").AddCssClass("hidden");
                btnEditarFoto.Enabled = false;
                btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnAgregarFoto.Enabled = true;


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarFoto_Click(object sender, EventArgs e)//
        {
            try
            {

                DesHabilitarFormularioFotografias();

                btnOKFoto.AddCssClass("hidden").AddCssClass("disabled");
                btnOKFoto.Enabled = false;
                btnCancelarFoto.AddCssClass("hidden").AddCssClass("disabled");
                btnCancelarFoto.Enabled = false;

                btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnAgregarFoto.Enabled = true;


                if (uidFoto.Text.Length == 0)
                {
                    btnEditarFoto.Disable();
                    LimpiarFormularioFotografias();
                }
                else
                {
                    //btnEliminarFoto.Enable();
                    btnEditarFoto.Enable();

                    List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                    SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dgvFotos.SelectedDataKey.Value.ToString()).First();

                    uidFoto.Text = foto.UidFoto.ToString();

                    txtDescripcionFoto.Text = foto.StrDescripcion;
                    txtPrecioFoto.Text = foto.StrPrecio;
                    txtPrecioFotoTicket.Text = foto.StrPrecioTicket;
                    txtPrecioFotoServidor.Text = foto.StrPrecioServidor;
                    txtAltoFoto.Text = foto.VchAlto.ToString();
                    txtAnchoFoto.Text = foto.VchAncho.ToString();
                    txtAltoFotoDesc.Text = foto.VchAltoDesc.ToString();
                    txtAnchoFotoDesc.Text = foto.VchAnchoDesc.ToString();
                    txtFxColumna.Text = foto.VchColumna;
                    txtFxFila.Text = foto.VchFila;
                    CbRotarImagenPapel.Checked = foto.BooRotarEnPapel;

                    ddActivoFoto.SelectedValue = foto.UidStatus.ToString();
                    ddMedidaFoto.SelectedValue = foto.UidMedida.ToString();
                }


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnAceptarEliminarFoto_Click(object sender, EventArgs e)//
        {
            try
            {

                btnAgregarFoto.Enabled = true;
                btnAgregarFoto.RemoveCssClass("disabled");

                btnOKFoto.Enabled = false;
                btnOKFoto.AddCssClass("hidden").AddCssClass("disabled");

                btnCancelarFoto.Enabled = false;
                btnCancelarFoto.AddCssClass("hidden").AddCssClass("disabled");

                Guid uid = new Guid(uidFoto.Text);

                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto == uid).First();
                fotos.Remove(foto);
                FotoRemoved.Add(foto);

                LimpiarFormularioFotografias();

                ViewState["Fotos"] = fotos;
                DatabindFotografias();

                btnCancelarEliminarFoto.Visible = false;
                btnAceptarEliminarFoto.Visible = false;
                lblAceptarEliminarFoto.Visible = false;
                ViewState["FotoPreviousRow"] = null;


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarEliminarFoto_Click(object sender, EventArgs e)//
        {
            try
            {
                //esta funcion parece ser llamada por otras
                btnCancelarEliminarFoto.Visible = false;
                btnAceptarEliminarFoto.Visible = false;
                lblAceptarEliminarFoto.Visible = false;


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void dgvFotos_RowDataBound(object sender, GridViewRowEventArgs e)//
        {
            try
            {

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvFotos, "Select$" + e.Row.RowIndex);
                }


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }

        }
        protected void dgvFotos_SelectedIndexChanged(object sender, EventArgs e)//
        {
            try
            {

                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dgvFotos.SelectedDataKey.Value.ToString()).First();

                uidFoto.Text = foto.UidFoto.ToString();
                ddImpresoraFoto.SelectedValue = foto.UidImpresora.ToString();
                txtDescripcionFoto.Text = foto.StrDescripcion;
                txtPrecioFoto.Text = foto.StrPrecio;
                txtPrecioFotoTicket.Text = foto.StrPrecioTicket;
                txtPrecioFotoServidor.Text = foto.StrPrecioServidor;
                txtAltoFoto.Text = foto.VchAlto.ToString();
                txtAnchoFoto.Text = foto.VchAncho.ToString();
                txtAltoFotoDesc.Text = foto.VchAltoDesc.ToString();
                txtAnchoFotoDesc.Text = foto.VchAnchoDesc.ToString();
                ddActivoFoto.SelectedValue = foto.UidStatus.ToString();//no se si se necesite seccionar tambien la uid
                ddMedidaFoto.SelectedValue = foto.UidMedida.ToString();


                if (EditingMode)
                {
                    btnEditarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFoto.Enabled = true;
                    //btnEliminarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    //btnEliminarFoto.Enabled = true;
                    btnOKFoto.AddCssClass("disabled").AddCssClass("hidden");
                    btnOKFoto.Enabled = false;
                    btnCancelarFoto.AddCssClass("disabled").AddCssClass("hidden");
                    btnCancelarFoto.Enabled = false;
                }

                int pos = -1;
                if (ViewState["FotoPreviousRow"] != null)
                {
                    pos = (int)ViewState["FotoPreviousRow"];
                    GridViewRow previousRow = dgvFotos.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }

                ViewState["FotoPreviousRow"] = dgvFotos.SelectedIndex;
                dgvFotos.SelectedRow.AddCssClass("success");


                PnErrorFotoSucursal.Visible = false;
                lblErrorFoto.Visible = false;
                lblErrorFoto.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoSucursal.Visible = true;
                lblErrorFoto.Visible = true;
                lblErrorFoto.Text = "Error al editar fotografias: \n detalle del error...\n" + x;
            }
        }
        void LimpiarFormularioFotografias()//
        {
            //solo son texbox
            uidFoto.Text = string.Empty;
            txtDescripcionFoto.Text = string.Empty;
            txtPrecioFoto.Text = string.Empty;
            txtPrecioFotoTicket.Text = string.Empty;
            txtPrecioFotoServidor.Text = string.Empty;
            txtAltoFoto.Text = string.Empty;
            txtAnchoFoto.Text = string.Empty;
            txtAltoFotoDesc.Text = string.Empty;
            txtAnchoFotoDesc.Text = string.Empty;
            ddActivoFoto.SelectedIndex = 0;
            ddMedidaFoto.SelectedIndex = 0;
        }
        void DesHabilitarFormularioFotografias()//
        {
            if (ddImpresoraFoto.DataSource != null)
            {
                ddImpresoraFoto.SelectedIndex = 0;
            }
            ddImpresoraFoto.AddCssClass("disabled");
            ddImpresoraFoto.Enabled = false;

            txtDescripcionFoto.Enabled = false;
            txtDescripcionFoto.AddCssClass("disabled");

            txtPrecioFoto.Enabled = false;
            txtPrecioFoto.AddCssClass("disabled");

            txtPrecioFotoTicket.Enabled = false;
            txtPrecioFotoTicket.AddCssClass("disabled");

            txtPrecioFotoServidor.Enabled = false;
            txtPrecioFotoServidor.AddCssClass("disabled");

            txtAltoFoto.Enabled = false;
            txtAltoFoto.AddCssClass("disabled");

            txtAnchoFoto.Enabled = false;
            txtAnchoFoto.AddCssClass("disabled");

            txtAltoFotoDesc.Enabled = false;
            txtAltoFotoDesc.AddCssClass("disabled");

            txtAnchoFotoDesc.Enabled = false;
            txtAnchoFotoDesc.AddCssClass("disabled");

            ddActivoFoto.SelectedIndex = 0;
            ddActivoFoto.AddCssClass("disabled");
            ddActivoFoto.Enabled = false;

            ddMedidaFoto.SelectedIndex = 0;
            ddMedidaFoto.AddCssClass("disabled");
            ddMedidaFoto.Enabled = false;


        }
        void HabilitarFormularioFotografias()//
        {
            ddImpresoraFoto.SelectedIndex = 0;
            ddImpresoraFoto.RemoveCssClass("disabled");
            ddImpresoraFoto.Enabled = true;

            txtDescripcionFoto.Enabled = true;
            txtDescripcionFoto.RemoveCssClass("disabled");

            txtPrecioFoto.Enabled = true;
            txtPrecioFoto.RemoveCssClass("disabled");

            txtPrecioFotoTicket.Enabled = true;
            txtPrecioFotoTicket.RemoveCssClass("disabled");

            txtPrecioFotoServidor.Enabled = true;
            txtPrecioFotoServidor.RemoveCssClass("disabled");

            txtAltoFoto.Enabled = true;
            txtAltoFoto.RemoveCssClass("disabled");

            txtAnchoFoto.Enabled = true;
            txtAnchoFoto.RemoveCssClass("disabled");

            txtAltoFotoDesc.Enabled = true;
            txtAltoFotoDesc.RemoveCssClass("disabled");

            txtAnchoFotoDesc.Enabled = true;
            txtAnchoFotoDesc.RemoveCssClass("disabled");

            ddActivoFoto.SelectedIndex = 0;
            ddActivoFoto.RemoveCssClass("disabled");
            ddActivoFoto.Enabled = true;

            ddMedidaFoto.SelectedIndex = 0;
            ddMedidaFoto.RemoveCssClass("disabled");
            ddMedidaFoto.Enabled = true;

        }
        void DatabindFotografias()//
        {
            List<SucursalFoto> Fotos = (List<SucursalFoto>)ViewState["Fotos"];

            dgvFotos.DataSource = Fotos;
            dgvFotos.DataBind();
        }
        #endregion Panel derecho (Fotografias)

        #region Panel derecho (Papel)
        void DesHabilitarFormularioPapel()//
        {
            txtNombrePapel.Enabled = false;
            txtAltoPapel.Enabled = false;
            txtAnchoPapel.Enabled = false;
            txtMargenSuperior.Enabled = false;
            txtMargenInferior.Enabled = false;
            txtMargenDerecho.Enabled = false;
            txtMargenIzquierdo.Enabled = false;
        }
        void DesHabilitarFormularioFotoPapel()//
        {

            CbRotarImagenPapel.AddCssClass("disabled");
            CbRotarImagenPapel.Enabled = false;

            txtFxFila.Enabled = false;
            txtFxFila.AddCssClass("disabled");

            txtFxColumna.Enabled = false;
            txtFxColumna.AddCssClass("disabled");
        }
        void HabilitarFormularioPapel()//
        {
            txtNombrePapel.Enabled = true;
            txtAltoPapel.Enabled = true;
            txtAnchoPapel.Enabled = true;
            txtMargenSuperior.Enabled = true;
            txtMargenInferior.Enabled = true;
            txtMargenDerecho.Enabled = true;
            txtMargenIzquierdo.Enabled = true;


        }
        void HabilitarFormularioFotoPapel()//
        {

            CbRotarImagenPapel.RemoveCssClass("disabled");
            CbRotarImagenPapel.Enabled = true;
            txtFxFila.Enabled = true;
            txtFxFila.RemoveCssClass("disabled");
            txtFxColumna.Enabled = true;
            txtFxColumna.RemoveCssClass("disabled");
        }
        void LimpiarFormularioPapel()//
        {
            txtNombrePapel.Text = "";
            txtAltoPapel.Text = "";
            txtAnchoPapel.Text = "";
            txtMargenSuperior.Text = "";
            txtMargenInferior.Text = "";
            txtMargenDerecho.Text = "";
            txtMargenIzquierdo.Text = "";

            txtNombrePapel.BorderColor = Color.FromName("#FF3580BF");
            txtAltoPapel.BorderColor = Color.FromName("#FF3580BF");
            txtAnchoPapel.BorderColor = Color.FromName("#FF3580BF");
            txtMargenSuperior.BorderColor = Color.FromName("#FF3580BF");
            txtMargenInferior.BorderColor = Color.FromName("#FF3580BF");
            txtMargenDerecho.BorderColor = Color.FromName("#FF3580BF");
            txtMargenIzquierdo.BorderColor = Color.FromName("#FF3580BF");

            lblErrorPapel.Text = "";
            lblErrorPapel.Visible = false;

            ToolAltoPapel.Visible = false;
            ToolAnchoPapel.Visible = false;
            ToolMSuperior.Visible = false;
            ToolMInferior.Visible = false;
            ToolMDerecho.Visible = false;
            ToolMIzquierdo.Visible = false;

            ToolAltoPapel.HRef = "";
            ToolAnchoPapel.HRef = "";
            ToolMSuperior.HRef = "";
            ToolMInferior.HRef = "";
            ToolMDerecho.HRef = "";
            ToolMIzquierdo.HRef = "";
        }
        void LimpiarFormularioFotoPapel()//
        {

            txtFxColumna.Text = string.Empty;
            txtFxFila.Text = string.Empty;
            CbRotarImagenPapel.Checked = false;

            lblErrorFotoPapel.Text = "";
            lblErrorFotoPapel.Visible = false;

        }
        public bool ValidarCamposPapel()//
        {
            bool PapelBIen = true;

            #region vacios


            if (string.IsNullOrWhiteSpace(txtNombrePapel.Text))
            {
                txtNombrePapel.Focus();
                txtNombrePapel.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }

            if (string.IsNullOrWhiteSpace(txtAltoPapel.Text))
            {
                txtAltoPapel.Focus();
                txtAltoPapel.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }
            if (string.IsNullOrWhiteSpace(txtAnchoPapel.Text))
            {
                txtAnchoPapel.Focus();
                txtAnchoPapel.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenSuperior.Text))
            {
                txtMargenSuperior.Focus();
                txtMargenSuperior.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenInferior.Text))
            {
                txtMargenInferior.Focus();
                txtMargenInferior.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenDerecho.Text))
            {
                txtMargenDerecho.Focus();
                txtMargenDerecho.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (string.IsNullOrWhiteSpace(txtMargenIzquierdo.Text))
            {
                txtMargenIzquierdo.Focus();
                txtMargenIzquierdo.BorderColor = Color.FromName("#f00800");
                PapelBIen = false;
            }


            if (PapelBIen == false)
            {
                _tabPapel();
                lblErrorPapel.Text = "Ningun campo debe estar vacio";//va despues que tab papel
                lblErrorPapel.Visible = true;
                PnErrorPapelSucursal.Visible = true;
                return PapelBIen;
            }

            #endregion vacios
            lblErrorPapel.Text = "";//va despues que tab papel
            lblErrorPapel.Visible = false;
            PnErrorPapelSucursal.Visible = false;
            #region Es numero

            //char[] charsRead = new char[txtAltoPapel.Text.Length];
            foreach (char c in txtAltoPapel.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtAltoPapel.Focus();
                    txtAltoPapel.BorderColor = Color.FromName("#f00800");
                    ToolAltoPapel.HRef = "Solo debe contener 1 numero";
                    ToolAltoPapel.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtAnchoPapel.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtAnchoPapel.Focus();
                    txtAnchoPapel.BorderColor = Color.FromName("#f00800");
                    ToolAnchoPapel.HRef = "Solo debe contener 1 numero";
                    ToolAnchoPapel.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenSuperior.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenSuperior.Focus();
                    txtMargenSuperior.BorderColor = Color.FromName("#f00800");
                    ToolMSuperior.HRef = "Solo debe contener 1 numero";
                    ToolMSuperior.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenInferior.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenInferior.Focus();
                    txtMargenInferior.BorderColor = Color.FromName("#f00800");
                    ToolMInferior.HRef = "Solo debe contener 1 numero";
                    ToolMInferior.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenDerecho.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenDerecho.Focus();
                    txtMargenDerecho.BorderColor = Color.FromName("#f00800");
                    ToolMDerecho.HRef = "Solo debe contener 1 numero";
                    ToolMDerecho.Visible = true;
                    PapelBIen = false;
                }
            }
            foreach (char c in txtMargenIzquierdo.Text)
            {
                if (char.IsLetter(c) || char.IsWhiteSpace(c))
                {

                    txtMargenIzquierdo.Focus();
                    txtMargenIzquierdo.BorderColor = Color.FromName("#f00800");
                    ToolMIzquierdo.HRef = "Solo debe contener 1 numero";
                    ToolMIzquierdo.Visible = true;
                    PapelBIen = false;
                }
            }
            if (PapelBIen == false)
            {
                _tabPapel();
                lblErrorPapel.Text = "Todos los campos en formato correcto";//va despues que tab papel
                lblErrorPapel.Visible = true;
                PnErrorPapelSucursal.Visible = true;
                return PapelBIen;
            }
            #endregion Es numero
            lblErrorPapel.Text = "";//va despues que tab papel
            lblErrorPapel.Visible = false;
            PnErrorPapelSucursal.Visible = false;
            #region digitos
            if (txtAltoPapel.Text.Length < 3)
            {
                txtAltoPapel.Focus();
                txtAltoPapel.BorderColor = Color.FromName("#f00800");
                ToolAltoPapel.HRef = "Minimo debe ser 1 numero de 3 digitos";
                ToolAltoPapel.Visible = true;
                PapelBIen = false;
            }
            if (txtAnchoPapel.Text.Length < 3)
            {
                txtAnchoPapel.Focus();
                txtAnchoPapel.BorderColor = Color.FromName("#f00800");
                ToolAnchoPapel.HRef = "Minimo debe ser 1 numero de 3 digitos";
                ToolAnchoPapel.Visible = true;
                PapelBIen = false;
            }
            if (PapelBIen == false)
            {
                _tabPapel();
                lblErrorPapel.Text = "Alto y Ancho deben tener al menos 3 digitos";//va despues que tab papel
                lblErrorPapel.Visible = true;
                PnErrorPapelSucursal.Visible = true;
                return PapelBIen;
            }
            #endregion digitos
            lblErrorPapel.Text = "";//va despues que tab papel
            lblErrorPapel.Visible = false;
            PnErrorPapelSucursal.Visible = false;
            return PapelBIen;
        }
        void DataBindFotografiasPapel()//
        {

            List<SucursalFoto> Fotos = (List<SucursalFoto>)ViewState["Fotos"];
            List<SucursalFoto> FotosX = new List<SucursalFoto>();
            SucursalFoto fotox = new SucursalFoto(); fotox.StrDescripcion = "[Selecciona]"; FotosX.Add(fotox);

            // txtCantMaqLicencia.Text ="EnuOrden: "+ Licencias[0].EnuOrden.ToString() + " StrOrdenaPor: " + Licencias[0].StrOrdenaPor.ToString();
            dvgFotosPapel.DataSource = Fotos;
            dvgFotosPapel.DataBind();
            int cant = dvgFotosPapel.Rows.Count - 1; //el menos 1 es debido porque en el gridview se maneja a partir del 0 y  Licencias.Count a partir del 1
            for (int i = 0; i <= cant; i++) // i comienza desde 0 porque recorre todo el gridview y el gridview comienza desde 0 igual que el Array aunque utilizando el count lo obtienes comenzando a partir del 1
            {
                if (Fotos[i].VchFila == "" && Fotos[i].VchColumna == "" && Fotos[i].BooRotarEnPapel == false)
                {
                    dvgFotosPapel.Rows[i].Visible = false;
                    FotosX.Add(Fotos[i]);
                }
                else
                {
                    if (Fotos[i].BooRotarEnPapel == false)
                    {
                        ((Label)dvgFotosPapel.Rows[i].FindControl("lbFotoRotadoPapel_icon")).Visible = false;
                        ((Label)dvgFotosPapel.Rows[i].FindControl("lbFotoNoRotadoPapel_icon")).Visible = true;
                    }
                    else
                    {
                        ((Label)dvgFotosPapel.Rows[i].FindControl("lbFotoRotadoPapel_icon")).Visible = true;
                        ((Label)dvgFotosPapel.Rows[i].FindControl("lbFotoNoRotadoPapel_icon")).Visible = false;
                    }
                }
            }

            DdlFoto.DataSource = FotosX;
            DdlFoto.DataValueField = "UidFoto";
            DdlFoto.DataTextField = "StrDescripcion";
            DdlFoto.DataBind();
        }
        protected void dvgFotosPapel_RowDataBound(object sender, GridViewRowEventArgs e)//
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dvgFotosPapel, "Select$" + e.Row.RowIndex);
                }
                if (e.Row.RowType == DataControlRowType.Header)
                {

                    switch (lbOrdenFPPor.Text)
                    {
                        case "Descripcion":
                            if (lbOrdenFP.Text == "ASC")
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

                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void dvgFotosPapel_SelectedIndexChanged(object sender, EventArgs e)//
        {
            try
            {
                DataBindFotografiasPapel();
                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dvgFotosPapel.SelectedDataKey.Value.ToString()).First();
                UidFotoPapel.Text = foto.UidFoto.ToString();
                txtFxFila.Text = foto.VchFila.ToString();
                txtFxColumna.Text = foto.VchColumna.ToString();
                CbRotarImagenPapel.Checked = foto.BooRotarEnPapel;
                DdlFoto.Items.Insert(0, new ListItem(foto.StrDescripcion, foto.UidFoto.ToString()));
                btnEditarFotoPapel.Text = "Editar";
                DesHabilitarFormularioFotoPapel();
                if (EditingMode)
                {
                    btnEditarFotoPapel.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoPapel.Enabled = true;
                    //btnEliminarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
                    //btnEliminarFoto.Enabled = true;
                    btnOKFotoPapel.AddCssClass("disabled").AddCssClass("hidden");
                    btnOKFotoPapel.Enabled = false;
                    btnCancelarFotoPapel.AddCssClass("disabled").AddCssClass("hidden");
                    btnCancelarFotoPapel.Enabled = false;
                }

                int pos = -1;
                if (ViewState["FotoPreviousRow"] != null)
                {
                    pos = (int)ViewState["FotoPreviousRow"];
                    GridViewRow previousRow = dvgFotosPapel.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }

                ViewState["FotoPreviousRow"] = dvgFotosPapel.SelectedIndex;
                dvgFotosPapel.SelectedRow.AddCssClass("success");


                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnEditarFotoPapel_Click(object sender, EventArgs e)//
        {
            try
            {
                HabilitarFormularioFotoPapel();
                btnEditarFotoPapel.AddCssClass("disabled");
                btnCancelarFotoPapel.Visible = true;
                btnOKFotoPapel.Visible = true;
                btnCancelarFotoPapel.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnOKFotoPapel.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnOKFotoPapel.Enabled = true;
                btnCancelarFotoPapel.Enabled = true;

                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnOKFotoPapel_Click(object sender, EventArgs e)//
        {
            try
            {
                if (ValidarCamposPapel() == false)
                {
                    return;
                }
                ToolAltoPapel.Visible = false;
                ToolAnchoPapel.Visible = false;
                ToolMDerecho.Visible = false;
                ToolMInferior.Visible = false;
                ToolMIzquierdo.Visible = false;
                ToolMSuperior.Visible = false;
                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == DdlFoto.SelectedValue.ToString()).First();
                //dgvFotos.SelectedDataKey.Value.ToString()).First();
                double CEspdisponible = ConMMColumnaC(foto);
                double FEspdisponible = ConMMFilaC(foto);
                NumberFormatInfo punto = new NumberFormatInfo(); punto.NumberDecimalSeparator = ".";

                //tarea pendiente validar si estan vacios columna y fila
                if (CbRotarImagenPapel.Checked == false)
                {
                    if (CEspdisponible - (double.Parse(txtFxColumna.Text, punto) * double.Parse(foto.VchAncho, punto)) <= 0)
                    {
                        txtFxColumna.BorderColor = Color.FromName("#f00800");
                        txtFxColumna.Focus();
                        ToolFxColumna.HRef = "Excede del espacio diponible";
                        ToolFxColumna.Visible = true;
                        return;
                    }
                    ToolFxColumna.HRef = "";
                    ToolFxColumna.Visible = false;
                    if (FEspdisponible - (double.Parse(txtFxFila.Text, punto) * double.Parse(foto.VchAlto, punto)) <= 0)
                    {
                        txtFxFila.BorderColor = Color.FromName("#f00800");
                        txtFxFila.Focus();
                        ToolFxFila.HRef = "Excede del espacio diponible";
                        ToolFxFila.Visible = true;
                        return;
                    }
                    ToolFxFila.HRef = "";
                    ToolFxFila.Visible = false;
                }
                else
                {
                    if (CEspdisponible - (double.Parse(txtFxColumna.Text, punto) * double.Parse(foto.VchAlto, punto)) <= 0)
                    {
                        txtFxColumna.BorderColor = Color.FromName("#f00800");
                        txtFxColumna.Focus();
                        ToolFxColumna.HRef = "Excede del espacio diponible";
                        ToolFxColumna.Visible = true;
                        return;
                    }
                    ToolFxColumna.HRef = "";
                    ToolFxColumna.Visible = false;
                    if (FEspdisponible - (double.Parse(txtFxFila.Text, punto) * double.Parse(foto.VchAncho, punto)) <= 0)
                    {
                        txtFxFila.BorderColor = Color.FromName("#f00800");
                        txtFxFila.Focus();
                        ToolFxFila.HRef = "Excede del espacio diponible";
                        ToolFxFila.Visible = true;
                        return;
                    }
                    ToolFxFila.HRef = "";
                    ToolFxFila.Visible = false;
                }
                lblErrorFoto.Visible = true;
                //List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                SucursalFoto photo = null;
                int pos = -1;
                if (!string.IsNullOrWhiteSpace(UidFotoPapel.Text))
                {
                    IEnumerable<SucursalFoto> dir = from i in fotos where i.UidFoto.ToString() == DdlFoto.SelectedValue.ToString() select i;
                    photo = dir.First();
                    pos = fotos.IndexOf(photo);
                    fotos.Remove(photo);
                }
                else
                {
                    photo = new SucursalFoto();
                    photo.UidFoto = Guid.NewGuid();
                }
                photo.BooRotarEnPapel = CbRotarImagenPapel.Checked;
                photo.VchColumna = txtFxColumna.Text;
                photo.VchFila = txtFxFila.Text;
                photo.UidFoto = new Guid(UidFotoPapel.Text);
                photo.StrDescripcion = DdlFoto.SelectedItem.Text;

                if (pos < 0)
                    fotos.Add(photo);
                else
                    fotos.Insert(pos, photo);

                ViewState["Fotos"] = fotos;
                DataBindFotografiasPapel();
                LimpiarFormularioFotoPapel();
                DesHabilitarFormularioFotoPapel();
                btnOKFotoPapel.AddCssClass("hidden").AddCssClass("disabled");
                btnOKFotoPapel.Enabled = false;
                btnCancelarFotoPapel.AddCssClass("hidden").AddCssClass("disabled");
                btnCancelarFotoPapel.Enabled = false;
                btnEditarFotoPapel.AddCssClass("disabled").AddCssClass("hidden");
                btnEditarFotoPapel.Enabled = false;


                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarFotoPapel_Click(object sender, EventArgs e)//
        {
            try
            {
                DesHabilitarFormularioFotoPapel();
                btnEditarFotoPapel.RemoveCssClass("disabled");
                btnCancelarFotoPapel.Visible = false;
                btnOKFotoPapel.Visible = false;

                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnEditarPapel_Click(object sender, EventArgs e)//
        {
            try
            {
                btnEditarPapel.AddCssClass("disabled");
                HabilitarFormularioPapel();
                //HabilitarFormularioFotoPapel();
                LimpiarFormularioFotoPapel();
                LimpiarFormularioPapel();
                //btnOkPapel
                //btnCancelarPapel.
                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];


                foreach (SucursalFoto f in fotos)
                {
                    f.VchColumna = "";
                    f.VchFila = "";
                    f.BooRotarEnPapel = false;
                }
                ViewState["Fotos"] = fotos;
                DataBindFotografiasPapel();

                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#VConfimacionNuevoPapel", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#VConfimacionNuevoPapel').hide();", true);

                PnErrorPapelSucursal.Visible = false;
                lblErrorPapel.Visible = false;
                lblErrorPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorPapelSucursal.Visible = true;
                lblErrorPapel.Visible = true;
                lblErrorPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnOkPapel_Click(object sender, EventArgs e)//
        {
            try
            {
                DesHabilitarFormularioPapel();
                btnOkPapel.Visible = false;
                btnCancelarPapel.Visible = false;

                PnErrorPapelSucursal.Visible = false;
                lblErrorPapel.Visible = false;
                lblErrorPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorPapelSucursal.Visible = true;
                lblErrorPapel.Visible = true;
                lblErrorPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        protected void btnCancelarPapel_Click(object sender, EventArgs e)//NO HAY NADA DE PORSI
        {

        }
        protected void DdlFoto_SelectedIndexChanged(object sender, EventArgs e)//
        {
            try
            {

                if (Guid.Empty != new Guid(DdlFoto.SelectedValue.ToString()) && !String.IsNullOrWhiteSpace(DdlFoto.SelectedValue.ToString()))
                {
                    if (EditingMode)
                    {
                        btnEditarFotoPapel.RemoveCssClass("disabled").RemoveCssClass("hidden");
                        btnEditarFotoPapel.Enabled = true;
                    }
                    UidFotoPapel.Text = DdlFoto.SelectedValue.ToString();
                }
                else
                {
                    btnEditarFotoPapel.AddCssClass("disabled").RemoveCssClass("hidden");
                    btnEditarFotoPapel.Enabled = false;
                    UidFotoPapel.Text = "";
                }
                DesHabilitarFormularioFotoPapel();
                btnEditarFotoPapel.Text = "Nuevo";
                btnOKFotoPapel.AddCssClass("disabled").AddCssClass("hidden");
                btnOKFotoPapel.Enabled = false;
                btnCancelarFotoPapel.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarFotoPapel.Enabled = false;

                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }
        public double ConMMColumnaC(SucursalFoto foto)//
        {
            double CEspDisponible;
            SucursalPapel Papel1 = new SucursalPapel();
            Papel1.VchAncho = txtAnchoPapel.Text;
            Papel1.VchDerecho = txtMargenDerecho.Text;
            Papel1.VchIzquierdo = txtMargenIzquierdo.Text;

            double CEspDisponibleMilimetros = (double.Parse(Papel1.VchAncho) - (double.Parse(Papel1.VchDerecho) + double.Parse(Papel1.VchIzquierdo)));
            CEspDisponible = ConversionMedidaMilimetros(CEspDisponibleMilimetros, foto.VchMedida);

            return CEspDisponible;
        }
        public double ConMMFilaC(SucursalFoto foto)
        {
            double FEspDisponible;
            SucursalPapel Papel1 = new SucursalPapel();
            Papel1.VchAlto = txtAltoPapel.Text;
            Papel1.VchSuperior = txtMargenSuperior.Text;
            Papel1.VchInferior = txtMargenInferior.Text;
            double FEspDisponibleMilimetros = (double.Parse(Papel1.VchAlto) - (double.Parse(Papel1.VchInferior) + double.Parse(Papel1.VchSuperior)));
            FEspDisponible = ConversionMedidaMilimetros(FEspDisponibleMilimetros, foto.VchMedida);
            return FEspDisponible;
        }
        protected void dvgFotosPapel_Sorting(object sender, GridViewSortEventArgs e)
        {
            try
            {
                List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
                if (e.SortExpression == lbOrdenFPPor.Text)
                {
                    if (lbOrdenFP.Text == Orden.ASC.ToString())
                    {
                        lbOrdenFP.Text = Orden.DESC.ToString();
                    }
                    else
                    {
                        lbOrdenFP.Text = Orden.ASC.ToString();
                    }
                }
                else
                {
                    lbOrdenFPPor.Text = e.SortExpression;
                    lbOrdenFP.Text = Orden.ASC.ToString();
                }
                Orden Ordenn = (Orden)Enum.Parse(typeof(Orden), lbOrdenFP.Text, true);
                //var txt = (HtmlInputText)dvgFotosPapel.FindControl("txt");
                List<SucursalFoto> fotosOrdenNueva = VM.OrdenarListaFP(e.SortExpression, Ordenn, fotos);
                ViewState["Fotos"] = fotosOrdenNueva;
                DataBindFotografiasPapel();

                PnErrorFotoPapelSucursal.Visible = false;
                lblErrorFotoPapel.Visible = false;
                lblErrorFotoPapel.Text = "";
            }
            catch (Exception x)
            {
                PnErrorFotoPapelSucursal.Visible = true;
                lblErrorFotoPapel.Visible = true;
                lblErrorFotoPapel.Text = "Error al editar papel: \n detalle del error...\n" + x;
            }

        }


        #endregion Panel derecho (Papel)

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