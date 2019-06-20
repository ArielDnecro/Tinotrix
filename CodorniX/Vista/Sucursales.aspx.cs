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

namespace CodorniX.Vista
{
    public partial class Sucursales : System.Web.UI.Page
    {
        #region Variables Sucursales generales
        VMSucursales VM = new VMSucursales();

       // System.Windows.Media.Color TemaRojoEstandar = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#f00800");
        SucursalLicencia ObjLicencia { get; set; }
        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        private List<SucursalDireccion> DireccionRemoved
        {
            get
            {
                if (ViewState["DireccionRemoved"] == null)
                    ViewState["DireccionRemoved"] = new List<SucursalDireccion>();

                return (List<SucursalDireccion>)ViewState["DireccionRemoved"];
            }
        }

        private List<SucursalTelefono> TelefonoRemoved
        {
            get
            {
                if (ViewState["TelefonoRemoved"] == null)
                    ViewState["TelefonoRemoved"] = new List<SucursalTelefono>();

                return (List<SucursalTelefono>)ViewState["TelefonoRemoved"];
            }
        }
        private List<SucursalImpresora> ImpresoraRemoved
        {
            get
            {
                if (ViewState["ImpresoraRemoved"] == null)
                    ViewState["ImpresoraRemoved"] = new List<SucursalImpresora>();

                return (List<SucursalImpresora>)ViewState["ImpresoraRemoved"];
            }
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

        private List<SucursalLicencia> LicenciaRemoved
        {
            get
            {
                if (ViewState["LicenciaRemoved"] == null)
                    ViewState["LicenciaRemoved"] = new List<SucursalLicencia>();

                return (List<SucursalLicencia>)ViewState["LicenciaRemoved"];
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

        private bool EditingModeDireccion
        {
            get
            {
                if (ViewState["EditingModeDireccion"] == null)
                    return false;

                return (bool)ViewState["EditingModeDireccion"];
            }
            set
            {
                ViewState["EditingModeDireccion"] = value;
            }
        }
        #endregion Variables Sucursales generales

        #region Private methods

        private void ActivarCamposDatos(bool enable)//lista 18/10/17
        {
            if (enable)
            {
                btnNuevaSucursal.AddCssClass("disabled");
                btnNuevaSucursal.Enabled = false;

                btnEditarSucursal.AddCssClass("disabled");
                btnEditarSucursal.Enabled = false;

                txtNombre.RemoveCssClass("disabled");
                txtNombre.Enabled = true;

                ddTipoSucursal.Enable();
                ddActivoSucursal.Enable();

                btnOkSucursal.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnOkSucursal.Enabled = true;

                btnCancelarSucursal.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnCancelarSucursal.Enabled = true;

                dgvDirecciones.Enabled = true;

                dgvTelefonos.Enabled = true;

                dgvImpresoras.Enabled = true;

                dgvFotos.Enabled = true;

                dgvLicencias.Enabled = true;

                EditingMode = true;
            }
            else
            {
                btnNuevaSucursal.RemoveCssClass("disabled");
                btnNuevaSucursal.Enabled = true;

                btnEditarSucursal.RemoveCssClass("disabled");
                btnEditarSucursal.Enabled = true;

                txtNombre.AddCssClass("disabled");
                txtNombre.Enabled = false;

                ddTipoSucursal.Disable();
                ddActivoSucursal.Disable();

                txtFechaRegistro.AddCssClass("disabled");
                txtFechaRegistro.Enabled = false;

                btnOkSucursal.AddCssClass("disabled").AddCssClass("hidden");
                btnOkSucursal.Enabled = false;

                btnCancelarSucursal.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarSucursal.Enabled = false;

                dgvDirecciones.Enabled = false;

                dgvTelefonos.Enabled = false;

                dgvImpresoras.Enabled = false;

                dgvFotos.Enabled = false;

                dgvLicencias.Enabled = false;
                EditingMode = false;
            }
        }

        private void ActivarCamposDireccion(bool enable)
        {
            if (enable)
            {
                btnOkDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnOkDireccion.Enabled = true;

                btnCancelarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnCancelarDireccion.Enabled = true;

                ddPais.RemoveCssClass("disabled");
                ddPais.Enabled = true;

                ddEstado.RemoveCssClass("disabled");
                ddEstado.Enabled = true;

                txtMunicipio.RemoveCssClass("disabled");
                txtMunicipio.Enabled = true;

                txtCiudad.RemoveCssClass("disabled");
                txtCiudad.Enabled = true;

                txtColonia.RemoveCssClass("disabled");
                txtColonia.Enabled = true;

                txtCalle.RemoveCssClass("disabled");
                txtCalle.Enabled = true;

                txtConCalle.RemoveCssClass("disabled");
                txtConCalle.Enabled = true;

                txtYCalle.RemoveCssClass("disabled");
                txtYCalle.Enabled = true;

                txtNoExt.RemoveCssClass("disabled");
                txtNoExt.Enabled = true;

                txtNoInt.RemoveCssClass("disabled");
                txtNoInt.Enabled = true;

                txtReferencia.RemoveCssClass("disabled");
                txtReferencia.Enabled = true;

            }
            else
            {
                btnOkDireccion.AddCssClass("disabled");
                btnOkDireccion.Enabled = false;

                btnCancelarDireccion.AddCssClass("disabled");
                btnCancelarDireccion.Enabled = false;

                ddPais.AddCssClass("disabled");
                ddPais.Enabled = false;

                ddEstado.AddCssClass("disabled");
                ddEstado.Enabled = false;

                txtMunicipio.AddCssClass("disabled");
                txtMunicipio.Enabled = false;

                txtCiudad.AddCssClass("disabled");
                txtCiudad.Enabled = false;

                txtColonia.AddCssClass("disabled");
                txtColonia.Enabled = false;

                txtCalle.AddCssClass("disabled");
                txtCalle.Enabled = false;

                txtConCalle.AddCssClass("disabled");
                txtConCalle.Enabled = false;

                txtYCalle.AddCssClass("disabled");
                txtYCalle.Enabled = false;

                txtNoExt.AddCssClass("disabled");
                txtNoExt.Enabled = false;

                txtNoInt.AddCssClass("disabled");
                txtNoInt.Enabled = false;

                txtReferencia.AddCssClass("disabled");
                txtReferencia.Enabled = false;
            }
        }

        #endregion Private methods

        protected void Page_Load(object sender, EventArgs e) //19/10/17
        {
            FUImagen.Attributes["onchange"] = "upload(this)";
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
                master.ActivarSucursal();
                lblErrorTelefono.Visible = false;
                lblErrorDireccion.Visible = false;
                lblErrorSucursal.Visible = false;

                ActivarCamposDatos(false);
                ActivarCamposDireccion(false);

                #region Botones de aceptar eliminar 
                lblAceptarEliminarDireccion.Visible = false;
                btnCancelarEliminarDireccion.Visible = false;
                btnAceptarEliminarDireccion.Visible = false;

                btnAceptarEliminarTelefono.Visible = false;
                btnCancelarEliminarTelefono.Visible = false;
                lblAceptarEliminarTelefono.Visible = false;

                btnAceptarEliminarImpresora.Visible = false;
                btnCancelarEliminarImpresora.Visible = false;
                lblAceptarEliminarImpresora.Visible = false;

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

                btnAgregarDireccion.AddCssClass("disabled");
                btnAgregarTelefono.AddCssClass("disabled");
                btnEditarSucursal.Enabled = false;
                btnEditarSucursal.AddCssClass("disabled");
                #endregion btns

                #region Dgv (Grid views)
                dgvSucursales.Visible = false;
                dgvSucursales.AddCssClass("hidden");
                dgvSucursales.DataSource = null;
                dgvSucursales.DataBind();

                dgvDirecciones.DataSource = null;
                dgvDirecciones.DataBind();

                dgvTelefonos.DataSource = null;
                dgvTelefonos.DataBind();

                dgvImpresoras.DataSource = null;
                dgvImpresoras.DataBind();

                dgvFotos.DataSource = null;
                dgvFotos.DataBind();

                dvgFotosPapel.DataSource = null;
                dvgFotosPapel.DataBind();

                dgvLicencias.DataSource = null;
                dgvLicencias.DataBind();

                #endregion Dgv (Grid views)

                #region Obtener listas de DropDownList (dd)
                //-----------------------------------------------------------------------------
                VM.ObtenerMedidas();

                ddMedidaFoto.DataSource = VM.Medidas;
                ddMedidaFoto.DataValueField = "UidMedida";
                ddMedidaFoto.DataTextField = "VchMedida";
                ddMedidaFoto.DataBind();
                //-----------------------------------------------------------------------------

                VM.ObtenerPaises();

                ddPais.DataSource = VM.Paises;
                ddPais.DataValueField = "UidPais";
                ddPais.DataTextField = "StrNombre";
                ddPais.DataBind();
                ddPais_SelectedIndexChanged(null, null);
                //---------------------------------------------------------------------------------
                VM.ObtenerTipoTelefonos();

                ddTipoTelefono.DataSource = VM.TipoTelefonos;
                ddTipoTelefono.DataValueField = "UidTipoTelefono";
                ddTipoTelefono.DataTextField = "StrTipoTelefono";
                ddTipoTelefono.DataBind();

                lbTipoTelefono.DataSource = VM.TipoTelefonos;
                lbTipoTelefono.DataValueField = "UidTipoTelefono";
                lbTipoTelefono.DataTextField = "StrTipoTelefono";
                lbTipoTelefono.DataBind();


                //---------------------------------------------------------------------------------
                VM.ObtenerStatus();

                ddActivo.DataSource = VM.ListaStatus;
                ddActivo.DataValueField = "UidStatus";
                ddActivo.DataTextField = "StrStatus";
                ddActivo.DataBind();

                ddActivoFoto.DataSource = VM.ListaStatus;
                ddActivoFoto.DataValueField = "UidStatus";
                ddActivoFoto.DataTextField = "StrStatus";
                ddActivoFoto.DataBind();

                ddActivoSucursal.DataSource = VM.ListaStatus;
                ddActivoSucursal.DataValueField = "UidStatus";
                ddActivoSucursal.DataTextField = "StrStatus";
                ddActivoSucursal.DataBind();

                lbStatus.DataSource = VM.ListaStatus;
                lbStatus.DataValueField = "UidStatus";
                lbStatus.DataTextField = "StrStatus";
                lbStatus.DataBind();


                //---------------------------------------------------------------------------------

                VM.ObtenerTipoSucursales();

                ddTipoSucursal.DataSource = VM.TipoSucursales;
                ddTipoSucursal.DataValueField = "UidTipoSucursal";
                ddTipoSucursal.DataTextField = "StrTipoSucursal";
                ddTipoSucursal.DataBind();

                lbTipoSucursal.DataSource = VM.TipoSucursales;
                lbTipoSucursal.DataValueField = "UidTipoSucursal";
                lbTipoSucursal.DataTextField = "StrTipoSucursal";
                lbTipoSucursal.DataBind();
                //---------------------------------------------------------------------------------
                VM.ObtenerTipoImpresoras();

                ddTipoImpresora.DataSource = VM.TipoImpresoras;
                ddTipoImpresora.DataValueField = "UidTipoImpresora";
                ddTipoImpresora.DataTextField = "StrDescripcion";
                ddTipoImpresora.DataBind();
                //---------------------------------------------------------------------------------

                #endregion Obtener listas de DropDownList (dd)

                
                FUImagen.Enabled = false;
                string todos = "";
                int[] i = lbTipoTelefono.GetSelectedIndices();
                foreach (int j in i)
                {
                    string value = lbTipoTelefono.Items[j].Value;
                    if (todos.Count() == 0)
                        todos += value;
                    else
                        todos += "," + value;
                }
                ImgSucursales.ImageUrl = "Imgenes/Default.jpg";
                ImgSucursales.DataBind();
            }

            ScriptManager.RegisterStartupScript(this, typeof(Page), "initializeDatapicker", "enableDatapicker()", true);
        }

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
            ActivarCamposDireccion(false);
            ActivarCamposDatos(false);
            btnEditarSucursal.RemoveCssClass("disabled");
            btnEditarSucursal.Enabled = true;

            uidSucursal.Text = VM.Sucursal.UidSucursal.ToString();
            ddTipoSucursal.SelectedValue = VM.Sucursal.UidTipoSucursal.ToString();
            ddActivoSucursal.SelectedValue = VM.Sucursal.UidStatus.ToString();
            txtNombre.Text = VM.Sucursal.StrNombre;
            txtFechaRegistro.Text = VM.Sucursal.DtFechaRegistro.ToString("dd/MM/yyyy");
            ImgSucursales.ImageUrl = Page.ResolveUrl(VM.Sucursal.RutaImagen);

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

            #region Rellenar Direccion
            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");
            btnObtenerDireccionEmpresa.Disable();
            VM.ObtenerDirecciones();
            ViewState["Direcciones"] = VM.Direcciones;
            DireccionRemoved.Clear();
            dgvDirecciones.DataSource = ViewState["Direcciones"];
            dgvDirecciones.DataBind();


            if (VM.Direcciones.Count == 0)
            {
                btnAgregarDireccion.Visible = true;
                btnEditarDireccion.Visible = false;
                btnEliminarDireccion.Visible = false;
            }
            else
            {
                btnAgregarDireccion.Visible = false;
                btnEditarDireccion.Visible = true;
                btnEliminarDireccion.Visible = true;
            }

            ViewState["DireccionPreviousRow"] = null;
            #endregion Rellenar Direccion

            #region Rellenar telefonos
            btnAgregarTelefono.AddCssClass("disabled");
            btnAgregarTelefono.Enabled = false;
            txtTelefono.Enabled = false;

            VM.ObtenerTelefonos();
            ViewState["Telefonos"] = VM.Telefonos;
            TelefonoRemoved.Clear();
            dgvTelefonos.DataSource = ViewState["Telefonos"];
            dgvTelefonos.DataBind();
            #endregion Rellenar telefonos

            #region Rellenar Impresoras
            btnAgregarImpresora.AddCssClass("disabled");
            btnAgregarImpresora.Enabled = false;
            txtDescripcionImpresora.Enabled = false;
            txtMarca.Enabled = false;
            txtModelo.Enabled = false;
            ddTipoImpresora.SelectedIndex = 0;
            ddActivo.SelectedIndex = 0;
            VM.ObtenerImpresoras();
            ViewState["Impresoras"] = VM.Impresoras;
            ImpresoraRemoved.Clear();
            dgvImpresoras.DataSource = ViewState["Impresoras"];
            dgvImpresoras.DataBind();
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

            #region Rellenar Licencias
           // dgvLicencias.Columns[6].Visible = false; //17 de agosto del 18
            dgvLicencias.Columns[5].Visible = false;
            btnGenerarLicencia.AddCssClass("disabled");
            btnGenerarLicencia.Enabled = false;
            btnAgregarLicencia.AddCssClass("disabled");
            btnAgregarLicencia.Enabled = false;
            txtCantMaqLicencia.Enabled = false;
            VM.ObtenerLicencias();
            Session["Licencias"] = VM.Licencias;
            LicenciaRemoved.Clear();
            DatabindLicencias();
            #endregion Rellenar Licencias

            #region Rellenar Papel
            VM.ObtenerPapel(VM.Sucursal.UidSucursal);
            
            if (VM.Papel.UidPapel != Guid.Empty) {
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
        }
        protected void ddPais_SelectedIndexChanged(object sender, EventArgs e)//19/10/17
        {
            Guid uid = new Guid(ddPais.SelectedValue.ToString());
            VM.ObtenerEstados(uid);
            ddEstado.DataSource = VM.Estados;
            ddEstado.DataValueField = "UidEstado";
            ddEstado.DataTextField = "StrNombre";
            ddEstado.DataBind();
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

            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataBind();

            dgvTelefonos.DataSource = null;
            dgvTelefonos.DataBind();

            dgvImpresoras.DataSource = null;
            dgvImpresoras.DataBind();

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
        protected void btnNuevaSucursal_Click(object sender, EventArgs e)
        {
            ActivarCamposDatos(true);
            uidSucursal.Text = string.Empty;
            txtNombre.Text = string.Empty;
            FUImagen.Enabled = true;
            ddTipoSucursal.SelectedIndex = 0;
            ddActivoSucursal.SelectedIndex = 0;
            txtFechaRegistro.Text = DateTime.Now.ToString("yyyy/MM/dd");

            ViewState["Direcciones"] = new List<SucursalDireccion>();
            DireccionRemoved.Clear();
            dgvDirecciones.DataSource = ViewState["Direcciones"];
            dgvDirecciones.DataBind();

            ViewState["Telefonos"] = new List<SucursalTelefono>();
            TelefonoRemoved.Clear();
            dgvTelefonos.DataSource = ViewState["Telefonos"];
            dgvTelefonos.DataBind();

            ViewState["Impresoras"] = new List<SucursalImpresora>();
            ImpresoraRemoved.Clear();
            dgvImpresoras.DataSource = ViewState["Impresoras"];
            dgvImpresoras.DataBind();

            ViewState["Fotos"] = new List<SucursalFoto>();
            FotoRemoved.Clear();
            //dgvFotos.DataSource = ViewState["Fotos"];
            //dgvFotos.DataBind();
            DatabindFotografias();

            Session["Licencias"] = new List<SucursalLicencia>();
            LicenciaRemoved.Clear();
            dgvLicencias.DataSource = Session["Licencias"];
            dgvLicencias.DataBind();

            btnAgregarDireccion.Visible = true;
            btnEditarDireccion.Visible = false;
            btnEliminarDireccion.Visible = false;
            btnObtenerDireccionEmpresa.Enable();
            btnAgregarDireccion.RemoveCssClass("disabled");

            btnAgregarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.Enabled = true;

            btnAgregarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");//27/09/17
            btnAgregarImpresora.Enabled = true;


            btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarFoto.Enabled = true;

            btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarFoto.Enabled = true;

            btnGenerarLicencia.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnGenerarLicencia.Enabled = true;
            btnAgregarLicencia.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarLicencia.Enabled = true;
            txtCantMaqLicencia.Enabled = true;
            txtCantMaqLicencia.RemoveCssClass("disabled");

            btnOkPapel.Visible = true;
            btnCancelarPapel.Visible = true;
            btnEditarPapel.AddCssClass("disabled");
            //btnNuevoPapel.AddCssClass("disabled");
            LimpiarFormularioPapel();
            LimpiarFormularioFotoPapel();
            HabilitarFormularioPapel();
            DataBindFotografiasPapel();

            int pos = -1;
            if (ViewState["SucursalPreviousRow"] != null)
            {
                pos = (int)ViewState["SucursalPreviousRow"];
                GridViewRow previousRow = dgvSucursales.Rows[pos];
                previousRow.RemoveCssClass("success");
            }
            ViewState["SucursalPreviousRow"] = null;
        }
        protected void btnEditarSucursal_Click(object sender, EventArgs e)
        {
           // dgvLicencias.Columns[6].Visible = true;
            dgvLicencias.Columns[5].Visible = true;

            ActivarCamposDatos(true);
            btnAgregarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarDireccion.Enabled = true;

            List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
            if (impresoras.Count >= 1)
            {
                btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");//05/10/17
                btnAgregarFoto.Enabled = true;
            }
            else
            {
                //btnAgregarFoto.AddCssClass("disabled").AddCssClass("hidden");//05/10/17
                //btnAgregarFoto.Enabled = false;
                btnAgregarFoto.AddCssClass("disabled");
            }
            btnAgregarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarImpresora.Enabled = true;

            btnGenerarLicencia.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnGenerarLicencia.Enabled = true;
            btnAgregarLicencia.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarLicencia.Enabled = true;
            txtCantMaqLicencia.Enabled = true;
            txtCantMaqLicencia.RemoveCssClass("disabled");

            FUImagen.Enabled = true;
            btnObtenerDireccionEmpresa.Enable();
            if (uidDireccion.Text.Length > 0)
            {
                btnEditarDireccion.Enable();
                btnEliminarDireccion.Enable();
            }
            if (uidTelefono.Text.Length > 0)
            {
                btnEditarTelefono.Enable();
                btnEliminarTelefono.Enable();
            }
            if (uidImpresora.Text.Length > 0)
            {
                btnEditarImpresora.Enable();
                btnEliminarImpresora.Enable();
            }
            if (uidFoto.Text.Length > 0)
            {
                btnEditarFoto.Enable();
                //btnEliminarFoto.Enable();
            }
            if (UidPapel.Text.Length > 0)
            {
                btnEditarPapel.Enable();
            }
            //btnOkPapel.Visible = true;
            //btnCancelarPapel.Visible = true;
            //btnEditarPapel.AddCssClass("disabled");
            //btnNuevoPapel.RemoveCssClass("disabled");
            DataBindFotografiasPapel();
            if (Guid.Empty != new Guid(DdlFoto.SelectedValue.ToString()) && !String.IsNullOrWhiteSpace(DdlFoto.SelectedValue.ToString()))
            { }
            else { UidFotoPapel.Text = ""; }
            if ( !String.IsNullOrWhiteSpace(UidFotoPapel.Text) && Guid.Empty!=new Guid(UidFotoPapel.Text) )
            {
                btnEditarFotoPapel.RemoveCssClass("disabled").RemoveCssClass("hidden") ;
                btnEditarFotoPapel.Enabled = true; btnEditarFotoPapel.Visible = true;
                btnOKFotoPapel.Visible = false;
                btnCancelarFotoPapel.Visible = false;
            }
            
           // HabilitarFormularioPapel();
        }
        protected void btnOkSucursal_Click(object sender, EventArgs e)
        {
            if (ValidarCamposPapel() == false)
            {
                return;
            }

            #region proceso de actualizacion de una sucursal
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

            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                lblErrorSucursal.Text = "El campo Nombre no debe estar vacío";
                txtNombre.Focus();
                frmGrpNombre.AddCssClass("has-error");
                return;
            }

            empresa.StrNombre = txtNombre.Text;

            empresa.UidEmpresa = SesionActual.uidEmpresaActual.Value;

            empresa.UidTipoSucursal = new Guid(ddTipoSucursal.SelectedValue);

            empresa.UidStatus = new Guid(ddActivoSucursal.SelectedValue);

            if (uidSucursal.Text != string.Empty)
            {
                string UidSucursal = dgvSucursales.SelectedDataKey.Value.ToString();
                VM.ObtenerSucursal(new Guid(UidSucursal));
                string Ruta = VM.Sucursal.RutaImagen;

                if (File.Exists(Server.MapPath(Ruta)))
                {
                    File.Delete(Server.MapPath(Ruta));

                }
            }

            if (ViewState["rutaimg"] != null)
                empresa.RutaImagen = ViewState["rutaimg"].ToString();

            VM.GuardarSucursal(empresa);

            #region Papel
            SucursalPapel Papel = new SucursalPapel();
            //SucursalPapel Papel = (SucursalPapel)ViewState["Papel"];

            if (!string.IsNullOrWhiteSpace(UidPapel.Text))
            {
                VM.ObtenerPapel(new Guid(uidSucursal.Text));
                Papel = VM.Papel;
                Papel.UidPapel = new Guid(UidPapel.Text);
            }
            else
            {

                Papel.UidPapel = empresa.UidSucursal;
            }

            
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
            #endregion proceso de actualizacion de una sucursal
            //-----------------------------------------------------------------------------------
            // AQUI YA GUARDO Y EMPIEZA LA LIMPIA DE LA VISTA

            #region Sucursales
            ActivarCamposDatos(false);
            frmGrpNombre.RemoveCssClass("has-error");
            txtNombre.Text = string.Empty;
            VM.ObtenerSucursales(SesionActual.uidEmpresaActual.Value);
            dgvSucursales.DataSource = VM.Sucursales;
            dgvSucursales.DataBind();
            #endregion Sucursales

            #region Direcciones
            ActivarCamposDireccion(false);

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");
            //Begin Direcciones
            List<SucursalDireccion> direcciones = (List<SucursalDireccion>)ViewState["Direcciones"];
            VM.GuardarDirecciones(direcciones, empresa.UidSucursal);
            VM.EliminarDirecciones(DireccionRemoved);
            //End Direcciones
            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataBind();

            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");

            btnOkDireccion.AddCssClass("disabled");
            btnCancelarDireccion.AddCssClass("disabled");
            btnObtenerDireccionEmpresa.Disable();

            ActivarCamposDireccion(false);
            #endregion direcciones

            #region telefonos

            frmGrpTelefono.RemoveCssClass("has-error");

            //Begin Telefonos
            List<SucursalTelefono> telefonos = (List<SucursalTelefono>)ViewState["Telefonos"];
            VM.GuardarTelefonos(telefonos, empresa.UidSucursal);
            VM.EliminarTelefonos(TelefonoRemoved);
            //End Telefono

            dgvTelefonos.DataSource = null;
            dgvTelefonos.DataBind();

            btnAgregarTelefono.AddCssClass("disabled");
            btnEditarTelefono.AddCssClass("disabled");
            btnEliminarTelefono.AddCssClass("disabled");
            btnOKTelefono.AddCssClass("disabled").AddCssClass("hidden");
            btnCancelarTelefono.AddCssClass("disabled").AddCssClass("hidden");

            txtTelefono.Text = string.Empty;
            uidTelefono.Text = string.Empty;
            ddTipoTelefono.SelectedIndex = 0;



            if (uidTelefono.Text.Length == 0)
            {
                btnEditarSucursal.Disable();
            }
            #endregion telefonos

            #region Impresoras


            frmGrpDescripcionImpresora.RemoveCssClass("has-error");
            frmGrpMarca.RemoveCssClass("has-error");
            frmGrpModelo.RemoveCssClass("has-error");
            List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
            int NoImpresoras = impresoras.Count;
            VM.GuardarImpresoras(impresoras, empresa.UidSucursal);
            VM.EliminarImpresoras(ImpresoraRemoved);

            dgvImpresoras.DataSource = null;
            dgvImpresoras.DataBind();

            btnAgregarImpresora.AddCssClass("disabled");
            btnEditarImpresora.AddCssClass("disabled");
            btnEliminarImpresora.AddCssClass("disabled");
            btnOKImpresora.AddCssClass("disabled").AddCssClass("hidden");
            btnCancelarImpresora.AddCssClass("disabled").AddCssClass("hidden");

            uidImpresora.Text = string.Empty;

            txtDescripcionImpresora.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            ddActivo.SelectedIndex = 0;
            ddTipoImpresora.SelectedIndex = 0;
            #endregion impresoras

            #region Fotos

            //frmGrpDescripcionFoto.RemoveCssClass("has-error");
            //frmGrpPrecioFoto.RemoveCssClass("has-error");
            //frmGrpAltoFoto.RemoveCssClass("has-error");
            //frmGrpAnchoFoto.RemoveCssClass("has-error");

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

            #region Licencias
           // dgvLicencias.Columns[6].Visible = false;
            dgvLicencias.Columns[5].Visible = false;
            frmGrpGenerarLicencias.RemoveCssClass("has-error");
            frmGrpNoLicencias.RemoveCssClass("has-error");
            List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
            VM.GuardarLicencias(Licencias, empresa.UidSucursal);
            dgvLicencias.DataSource = null;
            dgvLicencias.DataBind();
            btnGenerarLicencia.AddCssClass("disabled");
            btnAgregarLicencia.AddCssClass("disabled");
            txtCantMaqLicencia.Text = string.Empty;

            #endregion Licencias

            

            FUImagen.Enabled = false;
        }
        protected void btnCancelarSucursal_Click(object sender, EventArgs e)
        {


            #region Sucursales
            ActivarCamposDatos(false);
            lblErrorSucursal.Visible = false;
            lblErrorSucursal.Text = "";
            FUImagen.Enabled = false;
            frmGrpNombre.RemoveCssClass("has-error");

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
            #endregion Papel



            #region Direcciones
            lblErrorDireccion.Visible = false;
            lblErrorDireccion.Text = "";
            if (EditingModeDireccion)
            {
                btnCancelarDireccion_Click(null, null);
            }

            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");
            btnOkDireccion.AddCssClass("disabled");
            btnCancelarDireccion.AddCssClass("disabled");
            btnObtenerDireccionEmpresa.Disable();

            ActivarCamposDireccion(false);

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            btnCancelarEliminarDireccion_Click(sender, e);
            #endregion Direcciones

            #region Telefonos
            lblErrorTelefono.Visible = false;
            lblErrorTelefono.Text = "";

            btnAgregarTelefono.AddCssClass("disabled");
            btnEditarTelefono.AddCssClass("disabled");
            btnEliminarTelefono.AddCssClass("disabled");

            btnOKTelefono.AddCssClass("disabled").AddCssClass("hidden");
            btnCancelarTelefono.AddCssClass("disabled").AddCssClass("hidden");

            txtTelefono.Text = string.Empty;
            uidTelefono.Text = string.Empty;
            ddTipoTelefono.SelectedIndex = 0;

            frmGrpTelefono.RemoveCssClass("has-error");

            btnCancelarEliminarTelefono_Click(sender, e);
            #endregion Telefonos

            #region Impresoras
            lblErrorImpresora.Visible = false;
            lblErrorImpresora.Text = "";


            btnAgregarImpresora.AddCssClass("disabled");
            btnEditarImpresora.AddCssClass("disabled");
            btnEliminarImpresora.AddCssClass("disabled");

            btnOKImpresora.AddCssClass("disabled").AddCssClass("hidden");
            btnCancelarImpresora.AddCssClass("disabled").AddCssClass("hidden");

            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            ddActivo.SelectedIndex = 0;
            ddTipoImpresora.SelectedIndex = 0;

            frmGrpMarca.RemoveCssClass("has-error");
            frmGrpModelo.RemoveCssClass("has-error");

            btnCancelarEliminarImpresora_Click(sender, e);
            #endregion Impresoras

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
            txtAltoFoto.Text = string.Empty;
            txtAnchoFoto.Text = string.Empty;
            ddActivo.SelectedIndex = 0;
            ddTipoImpresora.SelectedIndex = 0;

            //frmGrpDescripcionFoto.RemoveCssClass("has-error");
            //frmGrpPrecioFoto.RemoveCssClass("has-error");
            //frmGrpAltoFoto.RemoveCssClass("has-error");
            //frmGrpAnchoFoto.RemoveCssClass("has-error");

            btnCancelarEliminarFoto_Click(sender, e);
            #endregion Fotos

            #region Licencias
           // dgvLicencias.Columns[6].Visible = false;
            dgvLicencias.Columns[5].Visible = false;
            btnGenerarLicencia.AddCssClass("disabled");
            btnAgregarLicencia.AddCssClass("disabled");
            #endregion Licencias

            if (uidSucursal.Text.Length == 0)
            {
                uidSucursal.Text = string.Empty;
                txtNombre.Text = string.Empty;
                ddTipoSucursal.SelectedIndex = 0;
                ddActivoSucursal.SelectedIndex = 0;
                txtFechaRegistro.Text = string.Empty;

                dgvDirecciones.DataSource = null;
                dgvDirecciones.DataBind();

                dgvTelefonos.DataSource = null;
                dgvTelefonos.DataBind();

                dgvImpresoras.DataSource = null;
                dgvImpresoras.DataBind();

                dgvFotos.DataSource = null;
                dgvFotos.DataBind();

                btnEditarSucursal.Disable();

                btnAgregarDireccion.Visible = true;
                btnEditarDireccion.Visible = false;
                btnEliminarDireccion.Visible = false;

                if (Session["RutaImagen"] != null)
                {
                    string Ruta = Session["RutaImagen"].ToString();

                    //Borra la imagen de la empresa
                    if (File.Exists(Server.MapPath(Ruta)))
                    {
                        File.Delete(Server.MapPath(Ruta));
                    }
                    //Recarga el controlador de la imagen con una imagen default
                    ImgSucursales.ImageUrl = "Img/Default.jpg";
                    ImgSucursales.DataBind();
                }
            }
            else
            {
                VM.ObtenerSucursal(new Guid(uidSucursal.Text));
                Session["SucursalActual"] = VM.Sucursal;
                Label lblSucursal = (Label)Page.Master.FindControl("lblSucursal");
                lblSucursal.Text = VM.Sucursal.StrNombre;
                uidSucursal.Text = VM.Sucursal.UidSucursal.ToString();
                txtNombre.Text = VM.Sucursal.StrNombre;
                ddTipoSucursal.SelectedValue = VM.Sucursal.UidTipoSucursal.ToString();
                ddActivoSucursal.SelectedValue = VM.Sucursal.UidStatus.ToString();
                txtFechaRegistro.Text = VM.Sucursal.DtFechaRegistro.ToString("dd/MM/yyyy");
                ImgSucursales.ImageUrl = Page.ResolveUrl(VM.Sucursal.RutaImagen);


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

                VM.ObtenerDirecciones();
                ViewState["Direcciones"] = VM.Direcciones;
                DireccionRemoved.Clear();
                dgvDirecciones.DataSource = ViewState["Direcciones"];
                dgvDirecciones.DataBind();

                VM.ObtenerTelefonos();
                ViewState["Telefonos"] = VM.Telefonos;
                TelefonoRemoved.Clear();
                dgvTelefonos.DataSource = ViewState["Telefonos"];
                dgvTelefonos.DataBind();

                VM.ObtenerImpresoras();
                ViewState["Impresoras"] = VM.Impresoras;
                ImpresoraRemoved.Clear();
                dgvImpresoras.DataSource = ViewState["Impresoras"];
                dgvImpresoras.DataBind();

                VM.Obtenerfotos();
                ViewState["Fotos"] = VM.Fotos;
                FotoRemoved.Clear();
                //dgvFotos.DataSource = ViewState["Fotos"];
                //dgvFotos.DataBind();
                DatabindFotografias();


                if (VM.Direcciones.Count == 0)
                {
                    btnAgregarDireccion.Visible = true;
                    btnEditarDireccion.Visible = false;
                    btnEliminarDireccion.Visible = false;
                }
                else
                {
                    btnAgregarDireccion.Visible = false;
                    btnEditarDireccion.Visible = true;
                    btnEliminarDireccion.Visible = true;
                }

            }
        }
        #endregion  Panel derecho (edicion)

        #region Panel derecho (tabs)

        protected void tabDatos_Click(object sender, EventArgs e)
        {
            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;
            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;


            panelDatosSucursal.Visible = true;
            activeDatos.Attributes["class"] = "active";

            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";

            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            panelImpresoras.Visible = false;
            activeImpresoras.Attributes["class"] = "";

            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelLicencias.Visible = false;
            activeLicencias.Attributes["class"] = "";

            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";


            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelSucursal.Visible = true;
            }
        }
        protected void tabDirecciones_Click(object sender, EventArgs e)
        {

            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;

            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;

            panelDatosSucursal.Visible = false;
            activeDatos.Attributes["class"] = "";

            panelDirecciones.Visible = true;
            activeDirecciones.Attributes["class"] = "active";

            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            panelImpresoras.Visible = false;
            activeImpresoras.Attributes["class"] = "";

            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelLicencias.Visible = false;
            activeLicencias.Attributes["class"] = "";

            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";
        }
        protected void tabTelefonos_Click(object sender, EventArgs e) // modificado 28/09/17
        {
            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;
            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;

            panelDatosSucursal.Visible = false;
            activeDatos.Attributes["class"] = "";

            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";

            panelTelefonos.Visible = true;
            activeTelefonos.Attributes["class"] = "active";

            panelImpresoras.Visible = false;
            activeImpresoras.Attributes["class"] = "";

            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelLicencias.Visible = false;
            activeLicencias.Attributes["class"] = "";

            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelSucursal.Visible = true;
            }
        }
        protected void tabImpresoras_Click(object sender, EventArgs e)
        {// pendiente el if editing direcciones
            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;
            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;

            panelDatosSucursal.Visible = false;
            activeDatos.Attributes["class"] = "";

            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";

            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            panelImpresoras.Visible = true;
            activeImpresoras.Attributes["class"] = "active";

            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelLicencias.Visible = false;
            activeLicencias.Attributes["class"] = "";

            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelSucursal.Visible = true;

            }
        }
        protected void tabFotografias_Click(object sender, EventArgs e)
        {
            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;
            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;

            panelDatosSucursal.Visible = false;
            activeDatos.Attributes["class"] = "";

            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";

            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            panelImpresoras.Visible = false;
            activeImpresoras.Attributes["class"] = "";

            panelFotos.Visible = true;
            activeFotografias.Attributes["class"] = "active";

            panelLicencias.Visible = false;
            activeLicencias.Attributes["class"] = "";
            
            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelSucursal.Visible = true;

            }
        }
        protected void tabLicencias_Click(object sender, EventArgs e)
        {
            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;
            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;

            panelDatosSucursal.Visible = false;
            activeDatos.Attributes["class"] = "";

            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";

            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            panelImpresoras.Visible = false;
            activeImpresoras.Attributes["class"] = "";

            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelLicencias.Visible = true;
            activeLicencias.Attributes["class"] = "active";

            panelPapel.Visible = false;
            activePapel.Attributes["class"] = "";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelSucursal.Visible = true;

            }
        }
        protected void tabPapel_Click(object sender, EventArgs e)
        {
            _tabPapel();
        }
       void  _tabPapel()
        {
            lblErrorTelefono.Visible = false;
            lblErrorSucursal.Visible = false;
            lblErrorDireccion.Visible = false;
            lblErrorImpresora.Visible = false;
            lblErrorFoto.Visible = false;
            lblErrorLicencia.Visible = false;
            lblErrorPapel.Visible = false;
            lblErrorFotoPapel.Visible = false;

            panelDatosSucursal.Visible = false;
            activeDatos.Attributes["class"] = "";

            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";

            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            panelImpresoras.Visible = false;
            activeImpresoras.Attributes["class"] = "";

            panelFotos.Visible = false;
            activeFotografias.Attributes["class"] = "";

            panelLicencias.Visible = false;
            activeLicencias.Attributes["class"] = "";

            panelPapel.Visible = true;
            activePapel.Attributes["class"] = "active";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelSucursal.Visible = true;

            }
        }
        #endregion

        #region Panel derecho (Direcciones)

        protected void dgvDirecciones_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            VM.ObtenerDireccion(new Guid(dgvDirecciones.SelectedDataKey.Value.ToString()));
            uidDireccion.Text = VM.Direccion.UidDireccion.ToString();
            ddPais.SelectedValue = VM.Direccion.UidPais.ToString();
            ddPais_SelectedIndexChanged(sender, e);
            ddEstado.SelectedValue = VM.Direccion.UidPais.ToString();
            txtMunicipio.Text = VM.Direccion.StrMunicipio;
            txtCiudad.Text = VM.Direccion.StrCiudad;
            txtColonia.Text = VM.Direccion.StrColonia;
            txtCalle.Text = VM.Direccion.StrCalle;
            txtConCalle.Text = VM.Direccion.StrConCalle;
            txtYCalle.Text = VM.Direccion.StrYCalle;
            txtReferencia.Text = VM.Direccion.StrReferencia;
            txtNoExt.Text = VM.Direccion.StrNoExt;
            txtNoInt.Text = VM.Direccion.StrNoInt;

            ActivarCamposDireccion(true);
        }

        protected void btnOkDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = false;
            lblErrorDireccion.Visible = true;
            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            if (string.IsNullOrWhiteSpace(txtMunicipio.Text))
            {
                lblErrorDireccion.Text = "El campo Municipio no debe estar vacío";
                txtMunicipio.Focus();
                frmGrpMunicipio.AddCssClass("has-error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                lblErrorDireccion.Text = "El campo Ciudad no debe estar vacío";
                txtCiudad.Focus();
                frmGrpCiudad.AddCssClass("has-error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtColonia.Text))
            {
                lblErrorDireccion.Text = "El campo Colonia no debe estar vacío";
                txtColonia.Focus();
                frmGrpColonia.AddCssClass("has-error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtCalle.Text))
            {
                lblErrorDireccion.Text = "El campo Calle no debe estar vacío";
                txtCalle.Focus();
                frmGrpCalle.AddCssClass("has-error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtConCalle.Text))
            {
                lblErrorDireccion.Text = "El campo Con Calle no debe estar vacío";
                txtConCalle.Focus();
                frmGrpConCalle.AddCssClass("has-error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtYCalle.Text))
            {
                lblErrorDireccion.Text = "El campo Y Calle no debe estar vacío";
                txtYCalle.Focus();
                frmGrpYCalle.AddCssClass("has-error");
                return;
            }

            if (string.IsNullOrWhiteSpace(txtNoExt.Text))
            {
                lblErrorDireccion.Text = "El campo No. Exterior no debe estar vacío";
                txtNoExt.Focus();
                frmGrpNoExt.AddCssClass("has-error");
                return;
            }

            List<SucursalDireccion> direcciones = (List<SucursalDireccion>)ViewState["Direcciones"];
            SucursalDireccion direccion = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidDireccion.Text))
            {
                IEnumerable<SucursalDireccion> dir = from d in direcciones where d.UidDireccion.ToString() == uidDireccion.Text select d;
                direccion = dir.First();
                pos = direcciones.IndexOf(direccion);
                direcciones.Remove(direccion);
            }
            else
            {
                direccion = new SucursalDireccion();
                direccion.UidDireccion = Guid.NewGuid();
            }
            direccion.UidPais = new Guid(ddPais.SelectedValue);
            direccion.UidEstado = new Guid(ddEstado.SelectedValue);
            direccion.StrMunicipio = txtMunicipio.Text;
            direccion.StrCiudad = txtCiudad.Text;
            direccion.StrColonia = txtColonia.Text;
            direccion.StrCalle = txtCalle.Text;
            direccion.StrConCalle = txtConCalle.Text;
            direccion.StrYCalle = txtYCalle.Text;
            direccion.StrNoExt = txtNoExt.Text;
            direccion.StrNoInt = txtNoInt.Text;
            direccion.StrReferencia = txtReferencia.Text;

            ActivarCamposDireccion(false);
            if (pos < 0)
                direcciones.Add(direccion);
            else
                direcciones.Insert(pos, direccion);

            dgvDirecciones.DataSource = direcciones;
            dgvDirecciones.DataBind();

            if (direcciones.Count == 0)
            {
                btnAgregarDireccion.Visible = true;
                btnEditarDireccion.Visible = false;
                btnEliminarDireccion.Visible = false;
            }
            else
            {
                btnAgregarDireccion.Visible = false;
                btnEditarDireccion.Visible = true;
                btnEliminarDireccion.Visible = true;
            }


            btnAgregarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");

            panelSeleccionDireccion.Visible = false;
            panelSucursal.Visible = true;
            panelDireccion.Visible = false;
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = false;
            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            ActivarCamposDireccion(false);

            uidDireccion.Text = string.Empty;
            ddPais.SelectedIndex = 0;
            ddEstado.SelectedIndex = 0;
            txtMunicipio.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtConCalle.Text = string.Empty;
            txtYCalle.Text = string.Empty;
            txtNoExt.Text = string.Empty;
            txtNoInt.Text = string.Empty;
            txtReferencia.Text = string.Empty;

            btnAgregarDireccion.Enabled = true;
            btnAgregarDireccion.RemoveCssClass("disabled");

            panelSeleccionDireccion.Visible = false;


            if (uidDireccion.Text.Length == 0)
            {
                btnEditarDireccion.Enabled = true;
                btnEditarDireccion.RemoveCssClass("disabled");

                btnEliminarDireccion.Enabled = true;
                btnEliminarDireccion.RemoveCssClass("disabled");
            }
            else
            {
                btnEditarDireccion.Enabled = false;
                btnEditarDireccion.AddCssClass("disabled");
                btnEliminarDireccion.Enabled = false;
                btnEliminarDireccion.AddCssClass("disabled");


            }

            panelSucursal.Visible = true;
            panelDireccion.Visible = false;
        }

        protected void dgvDirecciones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvDirecciones, "Select$" + e.Row.RowIndex);
            }
        }

        protected void dgvDirecciones_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SucursalDireccion> direcciones = (List<SucursalDireccion>)ViewState["Direcciones"];
            SucursalDireccion empresaDireccion = direcciones.Select(x => x).Where(x => x.UidDireccion.ToString() == dgvDirecciones.SelectedDataKey.Value.ToString()).First();
            uidDireccion.Text = empresaDireccion.UidDireccion.ToString();
            ddPais.SelectedValue = empresaDireccion.UidPais.ToString();
            ddPais_SelectedIndexChanged(sender, e);
            ddEstado.SelectedValue = empresaDireccion.UidEstado.ToString();
            txtMunicipio.Text = empresaDireccion.StrMunicipio;
            txtCiudad.Text = empresaDireccion.StrCiudad;
            txtColonia.Text = empresaDireccion.StrColonia;
            txtCalle.Text = empresaDireccion.StrCalle;
            txtConCalle.Text = empresaDireccion.StrConCalle;
            txtYCalle.Text = empresaDireccion.StrYCalle;
            txtReferencia.Text = empresaDireccion.StrReferencia;
            txtNoExt.Text = empresaDireccion.StrNoExt;
            txtNoInt.Text = empresaDireccion.StrNoInt;

            if (EditingMode)
            {
                btnEditarDireccion.Enabled = true;
                btnEditarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnEliminarDireccion.Enabled = true;
                btnEliminarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            }

            int pos = -1;
            if (ViewState["DireccionPreviousRow"] != null)
            {
                pos = (int)ViewState["DireccionPreviousRow"];
                GridViewRow previousRow = dgvDirecciones.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["DireccionPreviousRow"] = dgvDirecciones.SelectedIndex;
            dgvDirecciones.SelectedRow.AddCssClass("success");

            panelDireccion.Visible = true;
            panelSucursal.Visible = false;

            btnCancelarDireccion.Visible = false;
            btnOkDireccion.Visible = false;
            btnCerrarDireccion.Visible = true;
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = true;
            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");
            uidDireccion.Text = string.Empty;
            ddPais.SelectedIndex = 0;
            ddEstado.SelectedIndex = 0;
            txtMunicipio.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtConCalle.Text = string.Empty;
            txtYCalle.Text = string.Empty;
            txtNoExt.Text = string.Empty;
            txtNoInt.Text = string.Empty;
            txtReferencia.Text = string.Empty;

            ActivarCamposDireccion(true);

            panelSucursal.Visible = false;
            panelDireccion.Visible = true;

            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
            btnCerrarDireccion.Visible = false;

            int pos = -1;
            if (ViewState["DireccionPreviousRow"] != null)
            {
                pos = (int)ViewState["DireccionPreviousRow"];
                GridViewRow previousRow = dgvDirecciones.Rows[pos];
                previousRow.RemoveCssClass("success");
            }
        }

        protected void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = true;
            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");
            ActivarCamposDireccion(true);

            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;

            panelSucursal.Visible = false;
            panelDireccion.Visible = true;
        }

        protected void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            lblAceptarEliminarDireccion.Visible = true;
            lblAceptarEliminarDireccion.Text = "¿Desea eliminar la direccion seleccionada?";
            btnAceptarEliminarDireccion.Visible = true;
            btnCancelarEliminarDireccion.Visible = true;

        }

        protected void btnAceptarEliminarDireccion_Click(object sender, EventArgs e)
        {
            ActivarCamposDireccion(false);
            Guid uid = new Guid(uidDireccion.Text);

            List<SucursalDireccion> direcciones = (List<SucursalDireccion>)ViewState["Direcciones"];
            SucursalDireccion direccion = direcciones.Select(x => x).Where(x => x.UidDireccion == uid).First();
            direcciones.Remove(direccion);
            DireccionRemoved.Add(direccion);
            dgvDirecciones.DataSource = direcciones;
            dgvDirecciones.DataBind();

            uidDireccion.Text = string.Empty;
            ddPais.SelectedIndex = 0;
            ddEstado.SelectedIndex = 0;
            txtMunicipio.Text = string.Empty;
            txtCiudad.Text = string.Empty;
            txtColonia.Text = string.Empty;
            txtCalle.Text = string.Empty;
            txtConCalle.Text = string.Empty;
            txtYCalle.Text = string.Empty;
            txtNoExt.Text = string.Empty;
            txtNoInt.Text = string.Empty;
            txtReferencia.Text = string.Empty;

            btnAgregarDireccion.RemoveCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");
            btnAceptarEliminarDireccion.Visible = false;
            btnCancelarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;

            if (direcciones.Count == 0)
            {
                btnAgregarDireccion.Visible = true;
                btnEditarDireccion.Visible = false;
                btnEliminarDireccion.Visible = false;
            }
            else
            {
                btnAgregarDireccion.Visible = false;
                btnEditarDireccion.Visible = true;
                btnEliminarDireccion.Visible = true;
            }

            ViewState["DireccionPreviousRow"] = null;
        }

        #endregion

        #region Panel derecho (Telefono)
        protected void dgvTelefonos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvTelefonos, "Select$" + e.Row.RowIndex);
            }
        }

        protected void dgvTelefonos_SelectedIndexChanged1(object sender, EventArgs e)
        {
            List<SucursalTelefono> telefonos = (List<SucursalTelefono>)ViewState["Telefonos"];
            SucursalTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono.ToString() == dgvTelefonos.SelectedDataKey.Value.ToString()).First();

            uidTelefono.Text = telefono.UidTelefono.ToString();
            txtTelefono.Text = telefono.StrTelefono;
            ddTipoTelefono.SelectedValue = telefono.UidTipoTelefono.ToString();

            if (EditingMode)
            {
                btnEditarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnEditarTelefono.Enabled = true;
                btnEliminarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnEliminarTelefono.Enabled = true;
                btnOKTelefono.AddCssClass("disabled").AddCssClass("hidden");
                btnOKTelefono.Enabled = false;
                btnCancelarTelefono.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarTelefono.Enabled = false;
            }

            int pos = -1;
            if (ViewState["TelefonoPreviousRow"] != null)
            {
                pos = (int)ViewState["TelefonoPreviousRow"];
                GridViewRow previousRow = dgvTelefonos.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["TelefonoPreviousRow"] = dgvTelefonos.SelectedIndex;
            dgvTelefonos.SelectedRow.AddCssClass("success");

        }

        protected void btnAgregarTelefono_Click(object sender, EventArgs e)
        {
            uidTelefono.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTelefono.Enabled = true;
            txtTelefono.RemoveCssClass("disabled");
            ddTipoTelefono.SelectedIndex = 0;
            ddTipoTelefono.RemoveCssClass("disabled");
            ddTipoTelefono.Enabled = true;

            btnOKTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnOKTelefono.Enabled = true;
            btnCancelarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnCancelarTelefono.Enabled = true;

            btnAgregarTelefono.Disable();
            btnEditarTelefono.Disable();
            btnEliminarTelefono.Disable();

            int pos = -1;
            if (ViewState["TelefonoPreviousRow"] != null)
            {
                pos = (int)ViewState["TelefonoPreviousRow"];
                GridViewRow previousRow = dgvTelefonos.Rows[pos];
                previousRow.RemoveCssClass("success");
            }
        }

        protected void btnOKTelefono_Click(object sender, EventArgs e)
        {
            lblErrorTelefono.Visible = true;
            frmGrpTelefono.RemoveCssClass("has-error");

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblErrorTelefono.Text = "El campo Telefono no debe estar vacío";
                txtTelefono.Focus();
                frmGrpTelefono.AddCssClass("has-error");
                return;
            }
            List<SucursalTelefono> telefonos = (List<SucursalTelefono>)ViewState["Telefonos"];
            SucursalTelefono telefono = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidTelefono.Text))
            {
                IEnumerable<SucursalTelefono> dir = from t in telefonos where t.UidTelefono.ToString() == uidTelefono.Text select t;
                telefono = dir.First();
                pos = telefonos.IndexOf(telefono);
                telefonos.Remove(telefono);
            }
            else
            {
                telefono = new SucursalTelefono();
                telefono.UidTelefono = Guid.NewGuid();
            }
            telefono.StrTelefono = txtTelefono.Text;
            telefono.UidTipoTelefono = new Guid(ddTipoTelefono.SelectedValue);
            telefono.StrTipoTelefono = ddTipoTelefono.SelectedItem.Text;

            if (pos < 0)
                telefonos.Add(telefono);
            else
                telefonos.Insert(pos, telefono);

            dgvTelefonos.DataSource = telefonos;
            dgvTelefonos.DataBind();

            uidTelefono.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTelefono.Enabled = false;
            txtTelefono.AddCssClass("disabled");
            ddTipoTelefono.SelectedIndex = 0;
            ddTipoTelefono.AddCssClass("disabled");
            ddTipoTelefono.Enabled = false;

            btnOKTelefono.AddCssClass("hidden").AddCssClass("disabled");
            btnOKTelefono.Enabled = false;
            btnCancelarTelefono.AddCssClass("hidden").AddCssClass("disabled");
            btnCancelarTelefono.Enabled = false;

            btnEditarTelefono.AddCssClass("disabled").AddCssClass("hidden");
            btnEditarTelefono.Enabled = false;
            btnEliminarTelefono.AddCssClass("disabled").AddCssClass("hidden");
            btnEliminarTelefono.Enabled = false;

            btnAgregarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.Enabled = true;
        }

        protected void btnCancelarTelefono_Click(object sender, EventArgs e)
        {
            frmGrpTelefono.RemoveCssClass("has-error");

            txtTelefono.Enabled = false;
            txtTelefono.AddCssClass("disabled");
            ddTipoTelefono.AddCssClass("disabled");
            ddTipoTelefono.Enabled = false;

            btnOKTelefono.AddCssClass("hidden").AddCssClass("disabled");
            btnOKTelefono.Enabled = false;
            btnCancelarTelefono.AddCssClass("hidden").AddCssClass("disabled");
            btnCancelarTelefono.Enabled = false;

            btnAgregarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.Enabled = true;


            if (uidTelefono.Text.Length == 0)
            {
                btnEditarTelefono.Disable();
                btnEliminarTelefono.Disable();

                ddTipoTelefono.SelectedIndex = 0;
                txtTelefono.Text = string.Empty;
            }
            else
            {
                btnEliminarTelefono.Enable();
                btnEditarTelefono.Enable();

                List<SucursalTelefono> telefonos = (List<SucursalTelefono>)ViewState["Telefonos"];
                SucursalTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono.ToString() == dgvTelefonos.SelectedDataKey.Value.ToString()).First();

                uidTelefono.Text = telefono.UidTelefono.ToString();
                txtTelefono.Text = telefono.StrTelefono;
                ddTipoTelefono.SelectedValue = telefono.UidTipoTelefono.ToString();
            }
        }

        protected void btnEditarTelefono_Click(object sender, EventArgs e)
        {
            txtTelefono.Enabled = true;
            txtTelefono.RemoveCssClass("disabled");

            ddTipoTelefono.Enabled = true;
            ddTipoTelefono.RemoveCssClass("disabled");

            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.AddCssClass("disabled");

            btnEditarTelefono.Enabled = false;
            btnEditarTelefono.AddCssClass("disabled");

            btnEliminarTelefono.Enabled = false;
            btnEliminarTelefono.AddCssClass("disabled");

            btnOKTelefono.Enabled = true;
            btnOKTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");

            btnCancelarTelefono.Enabled = true;
            btnCancelarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
        }

        protected void btnEliminarTelefono_Click(object sender, EventArgs e)
        {
            lblAceptarEliminarTelefono.Visible = true;
            lblAceptarEliminarTelefono.Text = "¿Desea Eliminar el telefono seleccionado?";
            btnAceptarEliminarTelefono.Visible = true;
            btnCancelarEliminarTelefono.Visible = true;
        }

        protected void btnAceptarEliminarTelefono_Click(object sender, EventArgs e)
        {
            btnAgregarTelefono.Enabled = true;
            btnAgregarTelefono.RemoveCssClass("disabled");

            btnOKTelefono.Enabled = false;
            btnOKTelefono.AddCssClass("hidden").AddCssClass("disabled");

            btnCancelarTelefono.Enabled = false;
            btnCancelarTelefono.AddCssClass("hidden").AddCssClass("disabled");

            Guid uid = new Guid(uidTelefono.Text);

            List<SucursalTelefono> telefonos = (List<SucursalTelefono>)ViewState["Telefonos"];
            SucursalTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono == uid).First();
            telefonos.Remove(telefono);
            TelefonoRemoved.Add(telefono);

            uidTelefono.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            ddTipoTelefono.SelectedIndex = 0;

            dgvTelefonos.DataSource = telefonos;
            dgvTelefonos.DataBind();

            btnCancelarEliminarTelefono.Visible = false;
            btnAceptarEliminarTelefono.Visible = false;
            lblAceptarEliminarTelefono.Visible = false;
            ViewState["TelefonoPreviousRow"] = null;
        }

        protected void btnCancelarEliminarTelefono_Click(object sender, EventArgs e)
        {
            btnCancelarEliminarTelefono.Visible = false;
            btnAceptarEliminarTelefono.Visible = false;
            lblAceptarEliminarTelefono.Visible = false;
        }

        #endregion

        #region otras funciones 
        protected void btnCancelarEliminarDireccion_Click(object sender, EventArgs e)
        {
            btnCancelarEliminarDireccion.Visible = false;
            btnAceptarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;
        }

        protected void btnCerrarDireccion_Click(object sender, EventArgs e)
        {
            panelDireccion.Visible = false;
            panelSucursal.Visible = true;
            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
        }

        protected void ddDireccionesEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = new Guid(ddDireccionesEmpresa.SelectedValue);
            VM.ObtenerEmpresaDireccion(uid);

            uidDireccion.Text = string.Empty;
            ddPais.SelectedValue = VM.Direccion.UidPais.ToString();
            ddEstado.SelectedValue = VM.Direccion.UidEstado.ToString();
            txtMunicipio.Text = VM.Direccion.StrMunicipio;
            txtCiudad.Text = VM.Direccion.StrCiudad;
            txtColonia.Text = VM.Direccion.StrColonia;
            txtCalle.Text = VM.Direccion.StrCalle;
            txtConCalle.Text = VM.Direccion.StrConCalle;
            txtYCalle.Text = VM.Direccion.StrYCalle;
            txtNoExt.Text = VM.Direccion.StrNoExt;
            txtNoInt.Text = VM.Direccion.StrNoInt;
            txtReferencia.Text = VM.Direccion.StrReferencia;

            panelDireccion.Visible = true;
            panelSucursal.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Enable();
            btnCancelarDireccion.Enable();

            List<SucursalDireccion> direcciones = (List<SucursalDireccion>)ViewState["Direcciones"];
            if (direcciones.Count > 0)
            {
                if (direcciones[0].ExistsInDatabase)
                    DireccionRemoved.Add(direcciones[0]);
                direcciones.Clear();
            }
            btnAgregarDireccion.Visible = true;
            btnEditarDireccion.Visible = false;
            btnEliminarDireccion.Visible = false;
            dgvDirecciones.DataSource = direcciones;
            dgvDirecciones.DataBind();
        }

        protected void btnObtenerDireccionEmpresa_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = true;
            VM.ObtenerEmpresaDirecciones(SesionActual.uidEmpresaActual.Value);

            ddDireccionesEmpresa.DataSource = VM.EmpresaDirecciones;
            ddDireccionesEmpresa.DataValueField = "UidDireccion";
            ddDireccionesEmpresa.DataTextField = "LongDirection";
            ddDireccionesEmpresa.DataBind();
            panelSeleccionDireccion.Visible = true;

            if (ddDireccionesEmpresa.Items.Count > 0)
                ddDireccionesEmpresa_SelectedIndexChanged(null, null);
        }

        protected void imagen(object sender, EventArgs e)
        {
            if (FUImagen.HasFile)
            {
                string extencion = Path.GetExtension(FUImagen.FileName).ToLower();
                string[] arreglo = { ".jpg", ".png", ".jpeg" };
                for (int i = 0; i < arreglo.Length; i++)
                {
                    if (extencion == arreglo[i])
                    {
                        string Nombrearchivo = Path.GetFileName(FUImagen.FileName);
                        int numero = new Random().Next(999999999);
                        string ruta = "~/Vista/Imagenes/Sucursales/" + uidSucursal.Text + '_' + numero + Nombrearchivo;


                        //guardar img
                        FUImagen.SaveAs(Server.MapPath(ruta));

                        string rutaimg = ruta + "?" + (numero - 1);

                        ViewState["rutaimg"] = ruta;

                        ImgSucursales.ImageUrl = rutaimg;

                    }
                }
            }
        }

        protected void btnEncargados_Click(object sender, EventArgs e)
        {
            Response.Redirect("Encargados.aspx");
        }
        #endregion otras funciones

        #region Panel derecho (Impresoras)

        //Listo esta funcion ya esta 12-10-17
        protected void dgvImpresoras_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvImpresoras, "Select$" + e.Row.RowIndex);
            }
        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnAgregarImpresora_Click(object sender, EventArgs e)
        {
            uidImpresora.Text = string.Empty;
            txtDescripcionImpresora.Text = string.Empty;
            txtDescripcionImpresora.Enabled = true;
            txtMarca.Text = string.Empty;
            txtMarca.Enabled = true;
            txtModelo.Text = string.Empty;
            txtModelo.Enabled = true;
            txtDescripcionImpresora.RemoveCssClass("disabled");
            txtMarca.RemoveCssClass("disabled");
            txtModelo.RemoveCssClass("disabled");
            ddTipoImpresora.SelectedIndex = 0;
            ddTipoImpresora.RemoveCssClass("disabled");
            ddTipoImpresora.Enabled = true;
            ddActivo.SelectedIndex = 0;
            ddActivo.RemoveCssClass("disabled");
            ddActivo.Enabled = true;

            btnOKImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnOKImpresora.Enabled = true;
            btnCancelarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnCancelarImpresora.Enabled = true;

            btnAgregarImpresora.Disable();
            btnEditarImpresora.Disable();
            btnEliminarImpresora.Disable();

            int pos = -1;
            if (ViewState["ImpresoraPreviousRow"] != null)
            {
                pos = (int)ViewState["ImpresoraPreviousRow"];
                GridViewRow previousRow = dgvImpresoras.Rows[pos];
                previousRow.RemoveCssClass("success");
            }
        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnEditarImpresora_Click(object sender, EventArgs e)
        {
            HabilitarFormularioImpresoras();

            btnAgregarImpresora.Enabled = false;
            btnAgregarImpresora.AddCssClass("disabled");

            btnEditarImpresora.Enabled = false;
            btnEditarImpresora.AddCssClass("disabled");

            btnEliminarImpresora.Enabled = false;
            btnEliminarImpresora.AddCssClass("disabled");

            btnOKImpresora.Enabled = true;
            btnOKImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");

            btnCancelarImpresora.Enabled = true;
            btnCancelarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnEliminarImpresora_Click(object sender, EventArgs e)
        {
            lblAceptarEliminarImpresora.Visible = true;
            lblAceptarEliminarImpresora.Text = "¿Desea Eliminar el telefono seleccionado?";
            btnAceptarEliminarImpresora.Visible = true;
            btnCancelarEliminarImpresora.Visible = true;
        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnOkImpresora_Click(object sender, EventArgs e)
        {
            lblErrorImpresora.Visible = true;
            frmGrpDescripcionImpresora.RemoveCssClass("has-error");
            frmGrpMarca.RemoveCssClass("has-error");
            frmGrpModelo.RemoveCssClass("has-error");
            //if (string.IsNullOrWhiteSpace(txtMarca.Text))
            //{
            //    lblErrorImpresora.Text = "El campo Marca no debe estar vacío";
            //    txtMarca.Focus();
            //    frmGrpTelefono.AddCssClass("has-error");
            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(txtModelo.Text))
            //{
            //    lblErrorImpresora.Text = "El campo Modelo no debe estar vacío";
            //    txtModelo.Focus();
            //    frmGrpModelo.AddCssClass("has-error");
            //    return;
            //}
            List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
            SucursalImpresora impresora = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidImpresora.Text))
            {
                IEnumerable<SucursalImpresora> dir = from i in impresoras where i.UidImpresora.ToString() == uidImpresora.Text select i;
                impresora = dir.First();
                pos = impresoras.IndexOf(impresora);
                impresoras.Remove(impresora);
            }
            else
            {
                impresora = new SucursalImpresora();
                impresora.UidImpresora = Guid.NewGuid();
            }
            impresora.StrDescripcion = txtDescripcionImpresora.Text;
            impresora.StrMarca = txtMarca.Text;
            impresora.StrModelo = txtModelo.Text;

            impresora.UidStatus = new Guid(ddActivo.SelectedValue);
            impresora.StrStatus = ddActivo.SelectedItem.Text;

            impresora.UidTipoImpresora = new Guid(ddTipoImpresora.SelectedValue);
            impresora.StrTipoImpresora = ddTipoImpresora.SelectedItem.Text;

            if (pos < 0)
                impresoras.Add(impresora);
            else
                impresoras.Insert(pos, impresora);

            dgvImpresoras.DataSource = impresoras;
            dgvImpresoras.DataBind();
            //--------------------------------------------------------------------------------------------
            //Actualizar Ddl Impresora en fotos
            if (impresoras.Count >= 1)
            {
                btnAgregarFoto.RemoveCssClass("disabled").RemoveCssClass("hidden");// Al agregar una impresora a ddl de las, entonces hablito el boton nuevo
                btnAgregarFoto.Enabled = true;

                ddImpresoraFoto.DataSource = impresoras;
                ddImpresoraFoto.DataValueField = "UidImpresora";
                ddImpresoraFoto.DataTextField = "StrDescripcion";
                ddImpresoraFoto.DataBind();
                ddImpresoraFoto.SelectedIndex = 0;
            }

            //--------------------------------------------------------------------------------------------
            LimpiarFormularioImpresoras();
            DesHabilitarFormularioImpresoras();

            btnOKImpresora.AddCssClass("hidden").AddCssClass("disabled");
            btnOKImpresora.Enabled = false;
            btnCancelarImpresora.AddCssClass("hidden").AddCssClass("disabled");
            btnCancelarImpresora.Enabled = false;

            btnEditarImpresora.AddCssClass("disabled").AddCssClass("hidden");
            btnEditarImpresora.Enabled = false;
            btnEliminarImpresora.AddCssClass("disabled").AddCssClass("hidden");
            btnEliminarImpresora.Enabled = false;

            btnAgregarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarImpresora.Enabled = true;


        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnCancelarImpresora_Click(object sender, EventArgs e)
        {
            frmGrpDescripcionImpresora.RemoveCssClass("has-error");
            frmGrpMarca.RemoveCssClass("has-error");
            frmGrpModelo.RemoveCssClass("has-error");


            DesHabilitarFormularioImpresoras();

            btnOKImpresora.AddCssClass("hidden").AddCssClass("disabled");
            btnOKImpresora.Enabled = false;
            btnCancelarImpresora.AddCssClass("hidden").AddCssClass("disabled");
            btnCancelarImpresora.Enabled = false;

            btnAgregarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarImpresora.Enabled = true;


            if (uidImpresora.Text.Length == 0)
            {
                btnEditarImpresora.Disable();
                btnEliminarImpresora.Disable();

                ddTipoImpresora.SelectedIndex = 0;
                ddActivo.SelectedIndex = 0;
                txtMarca.Text = string.Empty;
                txtModelo.Text = string.Empty;
                txtDescripcionImpresora.Text = string.Empty;
            }
            else
            {
                btnEliminarImpresora.Enable();
                btnEditarImpresora.Enable();

                List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
                SucursalImpresora impresora = impresoras.Select(x => x).Where(x => x.UidImpresora.ToString() == dgvImpresoras.SelectedDataKey.Value.ToString()).First();

                uidImpresora.Text = impresora.UidImpresora.ToString();
                txtMarca.Text = impresora.StrMarca;
                txtModelo.Text = impresora.StrModelo;
                ddTipoImpresora.SelectedValue = impresora.UidTipoImpresora.ToString();
                ddActivo.SelectedValue = impresora.UidStatus.ToString();
            }
        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnAceptarEliminarImpresora_Click(object sender, EventArgs e)
        {
            btnAgregarImpresora.Enabled = true;
            btnAgregarImpresora.RemoveCssClass("disabled");

            btnOKImpresora.Enabled = false;
            btnOKImpresora.AddCssClass("hidden").AddCssClass("disabled");

            btnCancelarImpresora.Enabled = false;
            btnCancelarImpresora.AddCssClass("hidden").AddCssClass("disabled");

            Guid uid = new Guid(uidImpresora.Text);

            List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
            SucursalImpresora impresora = impresoras.Select(x => x).Where(x => x.UidImpresora == uid).First();
            impresoras.Remove(impresora);
            ImpresoraRemoved.Add(impresora);

            uidImpresora.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            ddTipoImpresora.SelectedIndex = 0;
            ddActivo.SelectedIndex = 0;

            dgvImpresoras.DataSource = impresoras;
            dgvImpresoras.DataBind();

            btnCancelarEliminarImpresora.Visible = false;
            btnAceptarEliminarImpresora.Visible = false;
            lblAceptarEliminarImpresora.Visible = false;
            ViewState["ImpresoraPreviousRow"] = null;
        }

        //Listo esta funcion ya esta 12-10-17
        protected void btnCancelarEliminarImpresora_Click(object sender, EventArgs e)
        {  //esta funcion parece ser llamada por otras
            btnCancelarEliminarImpresora.Visible = false;
            btnAceptarEliminarImpresora.Visible = false;
            lblAceptarEliminarImpresora.Visible = false;
        }

        //Listo esta funcion ya esta 12-10-17
        protected void dgvImpresoras_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SucursalImpresora> impresoras = (List<SucursalImpresora>)ViewState["Impresoras"];
            SucursalImpresora impresora = impresoras.Select(x => x).Where(x => x.UidImpresora.ToString() == dgvImpresoras.SelectedDataKey.Value.ToString()).First();

            uidImpresora.Text = impresora.UidImpresora.ToString();
            txtDescripcionImpresora.Text = impresora.StrDescripcion;
            txtMarca.Text = impresora.StrMarca;
            txtModelo.Text = impresora.StrModelo;
            ddActivo.SelectedValue = impresora.UidStatus.ToString();
            ddTipoImpresora.SelectedValue = impresora.UidTipoImpresora.ToString();

            if (EditingMode)
            {
                btnEditarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnEditarImpresora.Enabled = true;
                btnEliminarImpresora.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnEliminarImpresora.Enabled = true;
                btnOKImpresora.AddCssClass("disabled").AddCssClass("hidden");
                btnOKImpresora.Enabled = false;
                btnCancelarImpresora.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarImpresora.Enabled = false;
            }

            int pos = -1;
            if (ViewState["ImpresoraPreviousRow"] != null)
            {
                pos = (int)ViewState["ImpresoraPreviousRow"];
                GridViewRow previousRow = dgvImpresoras.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["ImpresoraPreviousRow"] = dgvImpresoras.SelectedIndex;
            dgvImpresoras.SelectedRow.AddCssClass("success");

        }

        //Listo esta funcion ya esta 16-10-17 pendiente
        //void ActivarValidacionImpresoras()
        //{

        //    //RequiredFieldValidator_TbAdmDireccionDescripcion.Enabled = true;
        //    //RFV_txtDescripcionFoto.Enabled = true;
        //    //RFV_txtPrecioFoto.Enabled = true;
        //    //RFV_txtAltoFoto.Enabled = true;
        //    //RFV_txtAnchoFoto.Enabled = true;
        //}

        //Listo esta funcion ya esta 16-10-17 pendiente
        //void DesActivarValidacionImpresoras()
        //{
        //    //RFV_txtDescripcionFoto.Enabled = false;
        //    //RFV_txtPrecioFoto.Enabled = false;
        //    //RFV_txtAltoFoto.Enabled = false;
        //    //RFV_txtAnchoFoto.Enabled = false;

        //}

        //Listo esta funcion ya esta 16-10-17 pendiente
        void LimpiarFormularioImpresoras()
        {
            //solo son texbox
            uidImpresora.Text = string.Empty;
            txtDescripcionImpresora.Text = string.Empty;
            txtMarca.Text = string.Empty;
            txtModelo.Text = string.Empty;
            ddTipoImpresora.SelectedIndex = 0;
            ddActivo.SelectedIndex = 0;
        }

        //Listo esta funcion ya esta 28-10-17 pendiente
        void DesHabilitarFormularioImpresoras()
        {
            txtDescripcionImpresora.Enabled = false;
            txtDescripcionImpresora.AddCssClass("disabled");

            txtMarca.Enabled = false;
            txtMarca.AddCssClass("disabled");

            txtModelo.Enabled = false;
            txtModelo.AddCssClass("disabled");

            ddTipoImpresora.AddCssClass("disabled");
            ddTipoImpresora.Enabled = false;

            ddActivo.AddCssClass("disabled");
            ddActivo.Enabled = false;
        }

        //Listo esta funcion ya esta 28-10-17 pendiente
        void HabilitarFormularioImpresoras()
        {

            txtDescripcionImpresora.Enabled = true;
            txtDescripcionImpresora.RemoveCssClass("disabled");

            txtMarca.Enabled = true;
            txtMarca.RemoveCssClass("disabled");

            txtModelo.Enabled = true;
            txtModelo.RemoveCssClass("disabled");

            ddTipoImpresora.Enabled = true;
            ddTipoImpresora.RemoveCssClass("disabled");

            ddActivo.Enabled = true;
            ddActivo.RemoveCssClass("disabled");
        }

        #endregion Panel derecho (Impresoras)

        #region Panel derecho (Fotografias)
        protected void btnAgregarFoto_Click(object sender, EventArgs e)
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
        }
        protected void btnEditarFoto_Click(object sender, EventArgs e)
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
        }
        protected void btnEliminarFoto_Click(object sender, EventArgs e)
        {
            lblAceptarEliminarFoto.Visible = true;
            lblAceptarEliminarFoto.Text = "¿Desea eliminar La foto seleccionada?";
            btnAceptarEliminarFoto.Visible = true;
            btnCancelarEliminarFoto.Visible = true;
        }
        protected void btnOKFoto_Click(object sender, EventArgs e)
        {
            lblErrorFoto.Visible = true;
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
            foto.VchAlto = txtAltoFoto.Text;
            foto.VchAncho = txtAnchoFoto.Text;
            foto.UidStatus = new Guid(ddActivoFoto.SelectedValue);
            foto.StrStatus = ddActivoFoto.SelectedItem.Text;
            foto.BooRotarEnPapel =false;
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
        }
        protected void btnCancelarFoto_Click(object sender, EventArgs e)
        {
            //frmGrpDescripcionFoto.RemoveCssClass("has-error");
            //frmGrpPrecioFoto.RemoveCssClass("has-error");
            //frmGrpAltoFoto.RemoveCssClass("has-error");
            //frmGrpAnchoFoto.RemoveCssClass("has-error");

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
                txtAltoFoto.Text = foto.VchAlto.ToString();
                txtModelo.Text = foto.VchAncho.ToString();
                txtFxColumna.Text = foto.VchColumna;
                txtFxFila.Text = foto.VchFila;
                CbRotarImagenPapel.Checked = foto.BooRotarEnPapel;

                ddActivoFoto.SelectedValue = foto.UidStatus.ToString();
                ddMedidaFoto.SelectedValue = foto.UidMedida.ToString();
            }
        }
        protected void btnAceptarEliminarFoto_Click(object sender, EventArgs e)
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

            //uidFoto.Text = string.Empty;

            //txtDescripcionFoto.Text = string.Empty;
            //txtPrecioFoto.Text = string.Empty;
            //txtAltoFoto.Text = string.Empty;
            //txtAnchoFoto.Text = string.Empty;
            //txtFxFila.Text = string.Empty;
            //txtFxColumna.Text = string.Empty;
            //CbRotarImagenPapel.Checked = false;
            //ddActivoFoto.SelectedIndex = 0;
            //ddMedidaFoto.SelectedIndex = 0;

            LimpiarFormularioFotografias();

            //dgvFotos.DataSource = fotos;
            ViewState["Fotos"] = fotos;
            //dgvFotos.DataBind();
            DatabindFotografias();

            btnCancelarEliminarFoto.Visible = false;
            btnAceptarEliminarFoto.Visible = false;
            lblAceptarEliminarFoto.Visible = false;
            ViewState["FotoPreviousRow"] = null;
        }
        protected void btnCancelarEliminarFoto_Click(object sender, EventArgs e)
        {
            //esta funcion parece ser llamada por otras
            btnCancelarEliminarFoto.Visible = false;
            btnAceptarEliminarFoto.Visible = false;
            lblAceptarEliminarFoto.Visible = false;
        }
        protected void dgvFotos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvFotos, "Select$" + e.Row.RowIndex);
            }
        }
        protected void dgvFotos_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
            SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dgvFotos.SelectedDataKey.Value.ToString()).First();

            uidFoto.Text = foto.UidFoto.ToString();
            ddImpresoraFoto.SelectedValue = foto.UidImpresora.ToString();
            txtDescripcionFoto.Text = foto.StrDescripcion;
            txtPrecioFoto.Text = foto.StrPrecio;
            txtAltoFoto.Text = foto.VchAlto.ToString();
            txtAnchoFoto.Text = foto.VchAncho.ToString();
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
        }
        void LimpiarFormularioFotografias()
        {
            //solo son texbox
            uidFoto.Text = string.Empty;
            txtDescripcionFoto.Text = string.Empty;
            txtPrecioFoto.Text = string.Empty;
            txtAltoFoto.Text = string.Empty;
            txtAnchoFoto.Text = string.Empty;
            ddActivoFoto.SelectedIndex = 0;
            ddMedidaFoto.SelectedIndex = 0;
        }
        void DesHabilitarFormularioFotografias()
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

            txtAltoFoto.Enabled = false;
            txtAltoFoto.AddCssClass("disabled");

            txtAnchoFoto.Enabled = false;
            txtAnchoFoto.AddCssClass("disabled");

            ddActivoFoto.SelectedIndex = 0;
            ddActivoFoto.AddCssClass("disabled");
            ddActivoFoto.Enabled = false;

            ddMedidaFoto.SelectedIndex = 0;
            ddMedidaFoto.AddCssClass("disabled");
            ddMedidaFoto.Enabled = false;

           
        }
        void HabilitarFormularioFotografias()
        {
            ddImpresoraFoto.SelectedIndex = 0;
            ddImpresoraFoto.RemoveCssClass("disabled");
            ddImpresoraFoto.Enabled = true;

            txtDescripcionFoto.Enabled = true;
            txtDescripcionFoto.RemoveCssClass("disabled");

            txtPrecioFoto.Enabled = true;
            txtPrecioFoto.RemoveCssClass("disabled");

            txtAltoFoto.Enabled = true;
            txtAltoFoto.RemoveCssClass("disabled");

            txtAnchoFoto.Enabled = true;
            txtAnchoFoto.RemoveCssClass("disabled");

            ddActivoFoto.SelectedIndex = 0;
            ddActivoFoto.RemoveCssClass("disabled");
            ddActivoFoto.Enabled = true;

            ddMedidaFoto.SelectedIndex = 0;
            ddMedidaFoto.RemoveCssClass("disabled");
            ddMedidaFoto.Enabled = true;
            
        }
        void DatabindFotografias()
        {
            List<SucursalFoto> Fotos = (List<SucursalFoto>)ViewState["Fotos"];

            // txtCantMaqLicencia.Text ="EnuOrden: "+ Licencias[0].EnuOrden.ToString() + " StrOrdenaPor: " + Licencias[0].StrOrdenaPor.ToString();
            dgvFotos.DataSource = Fotos;
            dgvFotos.DataBind();
        }
        #endregion Panel derecho (Fotografias)

        #region Panel derecho (Licencias)
        protected void btnGenerarLicencia_Click(object sender, EventArgs e)//listo 23/11/17
        {
            try
            {
                int cant = int.Parse(txtCantMaqLicencia.Text);
                List<SucursalLicencia> LicenciasNuevas = new List<SucursalLicencia>();
                for (int i = 1; i <= cant; i++)
                {
                    SucursalLicencia Licencia = new SucursalLicencia();
                    Licencia.IntNo = i-1;
                    Licencia.UidLicencia = Guid.NewGuid();
                    Licencia.BooStatus = true;
                    Licencia.BooStatusLicencia = true;
                    LicenciasNuevas.Add(Licencia);
                }
                //dgvLicencias.DataSource = LicenciasNuevas;
                //dgvLicencias.DataBind();
                Session["Licencias"] = LicenciasNuevas;
                DatabindLicencias();
                txtCantMaqLicencia.Text = string.Empty;
                //LimpiarFormularioFotografias();
                //DesHabilitarFormularioFotografias();

                lblErrorLicencia.Visible = false;
                lblErrorLicencia.Text = "";
                //NOta: no necesita databindLicencias porque btnGenerarLicencia_Click es para generar unicamente licencias nuevas
                //y databindLicencias es para recuperar la lista de objetos ya configurada
            }
            catch (Exception x)
            {
                lblErrorLicencia.Visible = true;
                lblErrorLicencia.Text = x.ToString();
            }
        }
        protected void btnAgregarLicencia_Click(object sender, EventArgs e)
        {
            try
            {
                List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
                SucursalLicencia Licencia = new SucursalLicencia();
                    Licencia.IntNo =Licencias.Count;
                    Licencia.UidLicencia = Guid.NewGuid();
                    Licencia.BooStatus = true;
                    Licencia.BooStatusLicencia = true;
                Licencias.Add(Licencia);
                Session["Licencias"] = Licencias;
                DatabindLicencias();
                txtCantMaqLicencia.Text = string.Empty;
                //LimpiarFormularioFotografias();
                //DesHabilitarFormularioFotografias();

                lblErrorLicencia.Visible = false;
                lblErrorLicencia.Text = "";
                //NOta: no necesita databindLicencias porque btnGenerarLicencia_Click es para generar unicamente licencias nuevas
                //y databindLicencias es para recuperar la lista de objetos ya configurada
            }
            catch (Exception x)
            {
                lblErrorLicencia.Visible = true;
                lblErrorLicencia.Text = x.ToString();
            }
        }
        protected void dvgLicencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                SucursalLicencia Licencia = (SucursalLicencia)e.Row.DataItem;
                if (dgvLicencias.SelectedDataKey != null)
                {
                    //if (CVMEmpresa.CEmpresa.ObCListaDinamicaDireccion[(GVObjCListaDireccionEmpresa.SelectedIndex) - 1].GUIDDireccion == Direccion.GUIDDireccion)
                    //{
                    //    e.Row.CssClass = "table-hover success";
                    //}
                    //else
                    //{
                    //    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(GVObjCListaDireccionEmpresa, "Select$" + e.Row.RowIndex);
                    //}
                }
                else
                {
                    e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(dgvLicencias, "Select$" + e.Row.RowIndex);
                }
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                switch (VM.StrOrdenaPor)
                {
                    case "No":
                        if (VM.EnuOrden == Orden.ASC)
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoNo")).Attributes["class"] = Global.OrdenAscendente;
                        }
                        else
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoNo")).Attributes["class"] = Global.OrdenDescendente;
                        }
                        break;
                    case "Licencia":
                        if (VM.EnuOrden == Orden.ASC)
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoLicencia")).Attributes["class"] = Global.OrdenAscendente;
                        }
                        else
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoLicencia")).Attributes["class"] = Global.OrdenDescendente;
                        }
                        break;
                    case "StatusLicencia":
                        if (VM.EnuOrden == Orden.ASC)
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoStatusLicencia")).Attributes["class"] = Global.OrdenAscendente;
                        }
                        else
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoStatusLicencia")).Attributes["class"] = Global.OrdenDescendente;
                        }
                        break;
                    case "Status":
                        if (VM.EnuOrden == Orden.ASC)
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoStatus")).Attributes["class"] = Global.OrdenAscendente;
                        }
                        else
                        {
                            ((HtmlGenericControl)e.Row.FindControl("IcoStatus")).Attributes["class"] = Global.OrdenDescendente;
                        }
                        break;
                }
            }
        }
        protected void dgvLicencias_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            
            try
            {
                int index = int.Parse(e.CommandArgument.ToString());
                if (index >= 0) // aqui se necesitan de la varible index con un valor mayor -1 para saber la posicion de la licencia en el gridview
                {
                    if (e.CommandName.Equals("RegenerarLicencia"))
                    {
                        List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
                        Guid LicenciaNueva = Guid.NewGuid();
                        //dgvLicencias.DataSource = Licencias;
                        //((TextBox)GVObjCListaDireccionEmpresa.Rows[GVObjCListaDireccionEmpresa.EditIndex].FindControl("tbDirColonia")).Text = LicenciaNueva;
                        //((BoundField)dgvLicencias.Rows[dgvLicencias.SelectedIndex].FindControl("UidLicencia")).Text = LicenciaNueva;
                        dgvLicencias.Rows[index].Cells[2].Text = LicenciaNueva.ToString();
                        //SucursalLicencia Licencia = Licencias.Select(x => x).Where(x => x.UidLicencia.ToString() == dgvLicencias.SelectedDataKey.Value.ToString()).First();
                        Licencias[index].UidLicencia = LicenciaNueva;
                        Session["Licencias"] = Licencias;
                        DatabindLicencias();

                    }
                    if (e.CommandName.Equals("DesactivarLicencia"))
                    {
                        //SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dgvFotos.SelectedDataKey.Value.ToString()).First();
                        //VM.Licencias = dgvLicencias.DataSource;
                        
                        //SucursalLicencia Licencia = dgvLicencias.Rows[index].ToString();
                        //List<SucursalLicencia> Licencias = dgvLicencias.DataKeys;
                        //.SelectedDataKey.Value.ToString()).First();
                        List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
                        Licencias[index].BooStatus = false;
                        Session["Licencias"] = Licencias;
                        DatabindLicencias();
                    }
                    if (e.CommandName.Equals("ActivarLicencia"))
                    {
                        List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
                        Licencias[index].BooStatus = true;
                        Session["Licencias"] = Licencias;
                        DatabindLicencias();
                    }
                    if (e.CommandName.Equals("CopiarLicencia")) {
                        List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
                        txtCantMaqLicencia.Text= Licencias[index].UidLicencia.ToString();
                        
                    }
                    if (e.CommandName.Equals("EliminarLicencia"))
                    {
                        List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
                        Licencias.Remove(Licencias[index]);
                        int cant = Licencias.Count-1;
                        for (int i = 0; i <= cant; i++) // i comienza desde 0 porque recorre todo el gridview y el gridview comienza desde 0 igual que el Array aunque utilizando el count lo obtienes comenzando a partir del 1
                        {
                            if (i>=index) {
                                Licencias[i].IntNo = i ;
                            }
                        }
                        Session["Licencias"] = Licencias;
                        DatabindLicencias();
                    }
                }

            }
            catch (Exception ex)
            {
                lblErrorLicencia.Visible = true;
                lblErrorLicencia.Text = ex.ToString();
            }
        }
        void DatabindLicencias()
        {
            List<SucursalLicencia> Licencias = (List<SucursalLicencia>)Session["Licencias"];
           
           // txtCantMaqLicencia.Text ="EnuOrden: "+ Licencias[0].EnuOrden.ToString() + " StrOrdenaPor: " + Licencias[0].StrOrdenaPor.ToString();
            dgvLicencias.DataSource = Licencias;
            dgvLicencias.DataBind();
            int cant = dgvLicencias.Rows.Count - 1; //el menos 1 es debido porque en el gridview se maneja a partir del 0 y  Licencias.Count a partir del 1
            for (int i = 0; i <= cant; i++) // i comienza desde 0 porque recorre todo el gridview y el gridview comienza desde 0 igual que el Array aunque utilizando el count lo obtienes comenzando a partir del 1
            {
                if (Licencias[i].BooStatus == false)
                {
                    ((LinkButton)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusDesactivar")).Visible = false;
                    ((LinkButton)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusActivar")).Visible = true;

                    ((Label)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusDesactivar_icon")).Visible = true;
                    ((Label)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusActivar_icon")).Visible = false;

                }

                if (Licencias[i].BooStatus == true)
                {
                    ((LinkButton)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusDesactivar")).Visible = true;
                    ((LinkButton)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusActivar")).Visible = false;

                    ((Label)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusDesactivar_icon")).Visible = false;
                    ((Label)dgvLicencias.Rows[i].FindControl("btndvgLicenciasStatusActivar_icon")).Visible = true;
                }
                if (Licencias[i].BooStatusLicencia == true)
                {
                    ((Label)dgvLicencias.Rows[i].FindControl("LbDirStatusLicencia")).Text = "Disponible";
                }
                if (Licencias[i].BooStatusLicencia == false)
                {
                    ((Label)dgvLicencias.Rows[i].FindControl("LbDirStatusLicencia")).Text = "En uso";
                }
                if (((Label)dgvLicencias.Rows[i].FindControl("LbDirNo")).Text == "0" )
                {
                   // ((Label)dgvLicencias.Rows[i].FindControl("LbDirNo")).Text = "Servidor";
                    ((LinkButton)dgvLicencias.Rows[i].FindControl("btndvgLicenciasEliminar")).Visible = false;
                }

            }
        }
        //protected void dgvLicencias_Sorting(object sender, GridViewSortEventArgs e)
        //{
        //    //VM.Licencias= (List<SucursalLicencia>)Session["Licencias"];
        //    //DatabindLicencias();
        //   // txtCantMaqLicencia.Text = "EnuOrden: " + VM.EnuOrden.ToString() + " StrOrdenaPor: " + VM.StrOrdenaPor.ToString();
        //    VM.OrdenarListaLicencia(e.SortExpression);
        //   // txtCantMaqLicencia.Text = "EnuOrden: " + VM.EnuOrden.ToString() + " StrOrdenaPor: " + VM.StrOrdenaPor.ToString();
        //    DatabindLicencias();
        //    Session["Licencias"] = VM.Licencias;

        //}
        protected void dgvLicencias_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            dgvLicencias.PageIndex = e.NewPageIndex;
            DatabindLicencias();
        }


        protected void dgvLicencias_Sorting(object sender, GridViewSortEventArgs e)
        {
            //VM.Licencias= (List<SucursalLicencia>)Session["Licencias"];
            //DatabindLicencias();
            // txtCantMaqLicencia.Text = "EnuOrden: " + VM.EnuOrden.ToString() + " StrOrdenaPor: " + VM.StrOrdenaPor.ToString();
            VM.OrdenarListaLicencia(e.SortExpression);
            // txtCantMaqLicencia.Text = "EnuOrden: " + VM.EnuOrden.ToString() + " StrOrdenaPor: " + VM.StrOrdenaPor.ToString();
            DatabindLicencias();
            Session["Licencias"] = VM.Licencias;
        }




        #endregion Panel derecho (Licencias)

        #region Panel derecho (Papel)
        void DesHabilitarFormularioPapel() {
            txtNombrePapel.Enabled=false;
            txtAltoPapel.Enabled = false;
            txtAnchoPapel.Enabled = false;
            txtMargenSuperior.Enabled = false;
            txtMargenInferior.Enabled = false;
            txtMargenDerecho.Enabled = false;
            txtMargenIzquierdo.Enabled = false;
        }
        void DesHabilitarFormularioFotoPapel() {
            //if (DdlFoto.DataSource != null)
            //{
            //    DdlFoto.SelectedIndex = 0;
            //}
            //DdlFoto.AddCssClass("disabled");
            //DdlFoto.Enabled = false;

            CbRotarImagenPapel.AddCssClass("disabled");
            CbRotarImagenPapel.Enabled = false;

            txtFxFila.Enabled = false;
            txtFxFila.AddCssClass("disabled");

            txtFxColumna.Enabled = false;
            txtFxColumna.AddCssClass("disabled");
        }
        void HabilitarFormularioPapel() {
            txtNombrePapel.Enabled = true;
            txtAltoPapel.Enabled = true;
            txtAnchoPapel.Enabled = true;
            txtMargenSuperior.Enabled = true;
            txtMargenInferior.Enabled = true;
            txtMargenDerecho.Enabled = true;
            txtMargenIzquierdo.Enabled = true;

           
        }
        void HabilitarFormularioFotoPapel() {
            //if (DdlFoto.DataSource != null)
            //{
            //    DdlFoto.SelectedIndex = 0;
            //}
            //DdlFoto.RemoveCssClass("disabled");
            //DdlFoto.Enabled = true;

            CbRotarImagenPapel.RemoveCssClass("disabled");
            CbRotarImagenPapel.Enabled = true;
            txtFxFila.Enabled = true;
            txtFxFila.RemoveCssClass("disabled");
            txtFxColumna.Enabled = true;
            txtFxColumna.RemoveCssClass("disabled");
        }
        void LimpiarFormularioPapel() {
            txtNombrePapel.Text="";
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
            ToolMInferior.Visible = false;

            ToolAltoPapel.HRef = "";
            ToolAnchoPapel.HRef = "";
            ToolMSuperior.HRef = "";
            ToolMInferior.HRef = "";
            ToolMDerecho.HRef = "";
            ToolMInferior.HRef = "";
        }
        void LimpiarFormularioFotoPapel() {

            txtFxColumna.Text = string.Empty;
            txtFxFila.Text = string.Empty;
            CbRotarImagenPapel.Checked = false;

            lblErrorFotoPapel.Text = "";
            lblErrorFotoPapel.Visible = false;

        }
        public  bool ValidarCamposPapel() {
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
                return PapelBIen;
            }

            #endregion vacios
            lblErrorPapel.Text = "";//va despues que tab papel
            lblErrorPapel.Visible = false;
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
                return PapelBIen;
            }
            #endregion Es numero
            lblErrorPapel.Text = "";//va despues que tab papel
            lblErrorPapel.Visible = false;
            #region digitos
            if (txtAltoPapel.Text.Length<3) {
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
                return PapelBIen;
            }
            #endregion digitos
            lblErrorPapel.Text = "";//va despues que tab papel
            lblErrorPapel.Visible = false;
            return PapelBIen;
        }
        void DataBindFotografiasPapel() {
           
                List<SucursalFoto> Fotos = (List<SucursalFoto>)ViewState["Fotos"];
                List<SucursalFoto> FotosX = new List<SucursalFoto>();
                SucursalFoto fotox = new SucursalFoto(); fotox.StrDescripcion = "[Selecciona]"; FotosX.Add(fotox);

                // txtCantMaqLicencia.Text ="EnuOrden: "+ Licencias[0].EnuOrden.ToString() + " StrOrdenaPor: " + Licencias[0].StrOrdenaPor.ToString();
                dvgFotosPapel.DataSource = Fotos;
                dvgFotosPapel.DataBind();
                int cant = dvgFotosPapel.Rows.Count - 1; //el menos 1 es debido porque en el gridview se maneja a partir del 0 y  Licencias.Count a partir del 1
                for (int i = 0; i <= cant; i++) // i comienza desde 0 porque recorre todo el gridview y el gridview comienza desde 0 igual que el Array aunque utilizando el count lo obtienes comenzando a partir del 1
                {
                   if (Fotos[i].VchFila=="" && Fotos[i].VchColumna=="" && Fotos[i].BooRotarEnPapel == false) {
                         dvgFotosPapel.Rows[i].Visible = false;
                           FotosX.Add(Fotos[i]); 
                   } else {
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
            //ListItem li;
            //li = new ListItem("[Seleccionar]", "");
            //DdlFoto.Items.Add(li);
            //DdlFoto.SelectedItem.Text = li.Text;
        }
        protected void dvgFotosPapel_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dvgFotosPapel, "Select$" + e.Row.RowIndex);
            }
            if (e.Row.RowType == DataControlRowType.Header) {

                switch (lbOrdenFPPor.Text)
                {
                    case "Descripcion":
                        if (lbOrdenFP.Text =="ASC")
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
        }
        protected void dvgFotosPapel_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataBindFotografiasPapel();
            List<SucursalFoto> fotos = (List<SucursalFoto>)ViewState["Fotos"];
            SucursalFoto foto = fotos.Select(x => x).Where(x => x.UidFoto.ToString() == dvgFotosPapel.SelectedDataKey.Value.ToString()).First();
            UidFotoPapel.Text = foto.UidFoto.ToString();
            txtFxFila.Text = foto.VchFila.ToString();
            txtFxColumna.Text = foto.VchColumna.ToString();
            CbRotarImagenPapel.Checked = foto.BooRotarEnPapel;
            DdlFoto.Items.Insert(0, new ListItem(foto.StrDescripcion, foto.UidFoto.ToString() ));
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
        }
        protected void btnEditarFotoPapel_Click(object sender, EventArgs e)
        {
            HabilitarFormularioFotoPapel();
            btnEditarFotoPapel.AddCssClass("disabled");
            btnCancelarFotoPapel.Visible = true;
            btnOKFotoPapel.Visible = true;
            btnCancelarFotoPapel.RemoveCssClass("hidden").RemoveCssClass("disabled");
            btnOKFotoPapel.RemoveCssClass("hidden").RemoveCssClass("disabled");
            btnOKFotoPapel.Enabled = true;
            btnCancelarFotoPapel.Enabled = true;

        }
        protected void btnOKFotoPapel_Click(object sender, EventArgs e)
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
            double CEspdisponible = ConMMColumna(foto);
            double FEspdisponible = ConMMFila(foto);
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
            else {
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
        }
        protected void btnCancelarFotoPapel_Click(object sender, EventArgs e)
        {
            DesHabilitarFormularioFotoPapel();
            btnEditarFotoPapel.RemoveCssClass("disabled");
            btnCancelarFotoPapel.Visible = false;
            btnOKFotoPapel.Visible = false;
        }
        protected void btnEditarPapel_Click(object sender, EventArgs e)
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
            
            //DdlFoto.DataSource = ViewState["Fotos"];
            //DdlFoto.DataValueField = "UidFoto";
            //DdlFoto.DataTextField = "StrDescripcion";
            //DdlFoto.DataBind();

          
            //  ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "Pop", "$('#VConfimacionNuevoPapel').modal('hide');", true);
            //  ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "$('#VConfimacionNuevoPapel').modal('hide');", true);
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "#VConfimacionNuevoPapel", "$('body').removeClass('modal-open');$('.modal-backdrop').remove();$('#VConfimacionNuevoPapel').hide();", true);
        }
        protected void btnOkPapel_Click(object sender, EventArgs e)
        {
            DesHabilitarFormularioPapel();
            btnOkPapel.Visible = false;
            btnCancelarPapel.Visible = false;
        }
        protected void btnCancelarPapel_Click(object sender, EventArgs e)
        {

        }
        protected void DdlFoto_SelectedIndexChanged(object sender, EventArgs e)
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
        }
        public double ConMMColumna(SucursalFoto foto)
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
        public double ConMMFila(SucursalFoto foto)
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
        protected void dvgFotosPapel_Sorting(object sender, GridViewSortEventArgs e)
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
        }

        #endregion Panel derecho (Papel)


    }

}