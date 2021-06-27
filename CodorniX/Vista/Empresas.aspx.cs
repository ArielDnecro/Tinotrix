using CodorniX.Modelo;
using CodorniX.Util;
using CodorniX.VistaDelModelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
namespace CodorniX.Vista
{
    public partial class Empresas : System.Web.UI.Page
    {
        VMEmpresas VM = new VMEmpresas();

        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        private List<EmpresaDireccion> DireccionRemoved
        {
            get
            {
                if (ViewState["DireccionRemoved"] == null)
                    ViewState["DireccionRemoved"] = new List<EmpresaDireccion>();

                return (List<EmpresaDireccion>)ViewState["DireccionRemoved"];
            }
        }

        private List<EmpresaTelefono> TelefonoRemoved
        {
            get
            {
                if (ViewState["TelefonoRemoved"] == null)
                    ViewState["TelefonoRemoved"] = new List<EmpresaTelefono>();

                return (List<EmpresaTelefono>)ViewState["TelefonoRemoved"];
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

        #region Private methods

        private void ActivarCamposDatos(bool enable)
        {
            if (enable)
            {
                btnNuevaEmpresa.AddCssClass("disabled");
                btnNuevaEmpresa.Enabled = false;

                btnEditarEmpresa.AddCssClass("disabled");
                btnEditarEmpresa.Enabled = false;

                txtNombreComercial.RemoveCssClass("disabled");
                txtNombreComercial.Enabled = true;

                txtRazonSocial.RemoveCssClass("disabled");
                txtRazonSocial.Enabled = true;

                txtGiro.RemoveCssClass("disabled");
                txtGiro.Enabled = true;

                txtRFC.RemoveCssClass("disabled");
                txtRFC.Enabled = true;

                btnOkEmpresa.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnOkEmpresa.Enabled = true;

                btnCancelarEmpresa.RemoveCssClass("disabled").RemoveCssClass("hidden");
                btnCancelarEmpresa.Enabled = true;

                dgvDirecciones.Enabled = true;

                dgvTelefonos.Enabled = true;

                EditingMode = true;
            }
            else
            {
                btnNuevaEmpresa.RemoveCssClass("disabled");
                btnNuevaEmpresa.Enabled = true;

                btnEditarEmpresa.RemoveCssClass("disabled");
                btnEditarEmpresa.Enabled = true;

                txtNombreComercial.AddCssClass("disabled");
                txtNombreComercial.Enabled = false;

                txtRazonSocial.AddCssClass("disabled");
                txtRazonSocial.Enabled = false;

                txtGiro.AddCssClass("disabled");
                txtGiro.Enabled = false;

                txtRFC.AddCssClass("disabled");
                txtRFC.Enabled = false;

                txtFechaRegistro.AddCssClass("disabled");
                txtFechaRegistro.Enabled = false;

                btnOkEmpresa.AddCssClass("disabled").AddCssClass("hidden");
                btnOkEmpresa.Enabled = false;

                btnCancelarEmpresa.AddCssClass("disabled").AddCssClass("hidden");
                btnCancelarEmpresa.Enabled = false;

                dgvDirecciones.Enabled = false;

                dgvTelefonos.Enabled = false;

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            FUImagen.Attributes["onchange"] = "upload(this)";
            if (SesionActual == null)
                return;

            if (!Acceso.TieneAccesoAModulo("Empresas", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }

            if (!IsPostBack)
            {
                lblErrorTelefono.Visible = false;
                lblErrorDireccion.Visible = false;
                lblErrorEmpresa.Visible = false;
                btnUsuario.Enabled = false;
                btnUsuario.CssClass = "btn btn-default disabled btn-sm";
                //Botones de aceptar eliminar direccion
                lblAceptarEliminarDireccion.Visible = false;
                btnCancelarEliminarDireccion.Visible = false;
                btnAceptarEliminarDireccion.Visible = false;

                btnAceptarEliminarTelefono.Visible = false;
                btnCancelarEliminarTelefono.Visible = false;
                lblAceptarEliminarTelefono.Visible = false;
                Site1 master = (Site1)Master;
                master.ActivarEmpresa();

                dgvEmpresas.Visible = false;
                dgvEmpresas.AddCssClass("hidden");
                dgvEmpresas.DataSource = null;
                dgvEmpresas.DataBind();

                FUImagen.Enabled = false;
                btnMostrarBusqueda.Text = "Ocultar";
                btnBorrarBusqueda.Visible = true;
                btnBorrarBusqueda.RemoveCssClass("hidden").RemoveCssClass("disabled");
                btnBuscar.Visible = true;
                btnBuscar.RemoveCssClass("hidden").RemoveCssClass("disabled");

                dgvDirecciones.DataSource = null;
                dgvDirecciones.DataBind();

                dgvTelefonos.DataSource = null;
                dgvTelefonos.DataBind();

                VM.ObtenerPaises();
                ddPais.DataSource = VM.Paises;
                ddPais.DataValueField = "UidPais";
                ddPais.DataTextField = "StrNombre";
                ddPais.DataBind();

                ddPais_SelectedIndexChanged(null, null);

                VM.ObtenerTipoTelefonos();
                ddTipoTelefono.DataSource = VM.TipoTelefonos;
                ddTipoTelefono.DataValueField = "UidTipoTelefono";
                ddTipoTelefono.DataTextField = "StrTipoTelefono";
                ddTipoTelefono.DataBind();

                ActivarCamposDatos(false);
                ActivarCamposDireccion(false);

                btnAgregarDireccion.AddCssClass("disabled");
                btnAgregarTelefono.AddCssClass("disabled");
                btnEditarEmpresa.Enabled = false;
                btnEditarEmpresa.AddCssClass("disabled");
                lbTipoTelefono.DataSource = VM.TipoTelefonos;
                lbTipoTelefono.DataValueField = "UidTipoTelefono";
                lbTipoTelefono.DataTextField = "StrTipoTelefono";
                lbTipoTelefono.DataBind();

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
                ImgEmpresas.ImageUrl = "Imgenes/Default.jpg";
                ImgEmpresas.DataBind();
            }
           

            ScriptManager.RegisterStartupScript(this, typeof(Page), "initializeDatapicker", "enableDatapicker()", true);
        }

        #region Panel izquierdo

        protected void dgvEmpresas_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvEmpresas, "Select$" + e.Row.RowIndex);
            }
        }

        protected void dgvEmpresas_SelectedIndexChanged(object sender, EventArgs e)
        {
            FUImagen.Enabled = false;
            btnUsuario.Enabled = true;
            btnUsuario.CssClass = "btn btn-default btn-sm";
            Guid uid = new Guid(dgvEmpresas.SelectedDataKey.Value.ToString());
            VM.ObtenerEmpresa(uid);
            if (SesionActual.uidEmpresaActual == null || !SesionActual.uidEmpresaActual.Equals(VM.Empresa.UidEmpresa))
            {
                SesionActual.uidEmpresaActual = VM.Empresa.UidEmpresa;
                SesionActual.uidSucursalActual = null;
                Site1 master = (Site1)Page.Master;
                master.UpdateNavbar();
            }
            uidEmpresa.Text = VM.Empresa.UidEmpresa.ToString();
            txtNombreComercial.Text = VM.Empresa.StrNombreComercial;
            txtRazonSocial.Text = VM.Empresa.StrRazonSocial;
            txtGiro.Text = VM.Empresa.StrGiro;
            txtRFC.Text = VM.Empresa.StrRFC;
            txtFechaRegistro.Text = VM.Empresa.DtFechaRegistro.ToString("dd/MM/yyyy");
            ImgEmpresas.ImageUrl = Page.ResolveUrl(VM.Empresa.RutaImagen);
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

            ActivarCamposDireccion(false);
            ActivarCamposDatos(false);
            btnEditarEmpresa.RemoveCssClass("disabled");
            btnEditarEmpresa.Enabled = true;

            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");

            btnAgregarTelefono.AddCssClass("disabled");
            btnAgregarTelefono.Enabled = false;
            txtTelefono.Enabled = false;


            int pos = -1;
            if (ViewState["EmpresaPreviousRow"] != null)
            {
                pos = (int)ViewState["EmpresaPreviousRow"];
                GridViewRow previousRow = dgvEmpresas.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["EmpresaPreviousRow"] = dgvEmpresas.SelectedIndex;
            dgvEmpresas.SelectedRow.AddCssClass("success");
        }

        protected void ddPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = new Guid(ddPais.SelectedValue.ToString());
            VM.ObtenerEstados(uid);
            ddEstado.DataSource = VM.Estados;
            ddEstado.DataValueField = "UidEstado";
            ddEstado.DataTextField = "StrNombre";
            ddEstado.DataBind();
        }

        private void SortEmpresa(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            List<Empresa> empresas = (List<Empresa>)ViewState["Empresas"];

            if (SortExpression == (string)ViewState["SortColumn"] && !same)
            {
                // We are resorting the same column, so flip the sort direction
                SortDirection =
                    ((SortDirection)ViewState["SortColumnDirection"] == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            if (SortExpression == "RFC")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    empresas = empresas.OrderBy(x => x.StrRFC).ToList();
                }
                else
                {
                    empresas = empresas.OrderByDescending(x => x.StrRFC).ToList();
                }
            }
            else if (SortExpression == "NombreComercial")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    empresas = empresas.OrderBy(x => x.StrNombreComercial).ToList();
                }
                else
                {
                    empresas = empresas.OrderByDescending(x => x.StrNombreComercial).ToList();
                }
            }
            else
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    empresas = empresas.OrderBy(x => x.StrRazonSocial).ToList();
                }
                else
                {
                    empresas = empresas.OrderByDescending(x => x.StrRazonSocial).ToList();
                }
            }
            dgvEmpresas.DataSource = empresas;
            ViewState["SortColumn"] = SortExpression;
            ViewState["SortColumnDirection"] = SortDirection;
        }

        protected void dgvEmpresas_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Invalidate Last position
            ViewState["EmpresaPreviousRow"] = null;
            SortEmpresa(e.SortExpression, e.SortDirection);

            dgvEmpresas.DataBind();
        }


        protected void dgvEmpresas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Invalidate Last Position
            ViewState["EmpresaPreviousRow"] = null;
            if (ViewState["SortColumn"] != null && ViewState["SortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["SortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["SortColumnDirection"];
                SortEmpresa(SortExpression, SortDirection, true);
            }
            else
            {
                dgvEmpresas.DataSource = ViewState["Empresas"];
            }
            dgvEmpresas.PageIndex = e.NewPageIndex;
            dgvEmpresas.DataBind();
        }

        protected void btnBorrarBusqueda_Click(object sender, EventArgs e)
        {
            txtBusquedaNombreComercial.Text = string.Empty;
            txtBusquedaRazonSocial.Text = string.Empty;
            txtBusquedaGiro.Text = string.Empty;
            txtBusquedaRFC.Text = string.Empty;
            txtBusquedaRegistradoAntes.Text = string.Empty;
            txtBusquedaRegistradoDespues.Text = string.Empty;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            seccionBusqueda.Visible = false;

            string nombreComercial = txtBusquedaNombreComercial.Text;
            string razonSocial = txtBusquedaRazonSocial.Text;
            string giro = txtBusquedaGiro.Text;
            string rfc = txtBusquedaRFC.Text;

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
            VM.BuscarEmpresas(nombreComercial, razonSocial, giro, rfc, registradoDespues, registradoAntes);
            ViewState["Empresas"] = VM.Empresas;
            dgvEmpresas.DataSource = VM.Empresas;
            dgvEmpresas.DataBind();
            dgvEmpresas.Visible = true;
            dgvEmpresas.RemoveCssClass("hidden");

            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataBind();

            dgvTelefonos.DataSource = null;
            dgvTelefonos.DataBind();

            btnMostrarBusqueda.Text = "Mostrar";
            btnBorrarBusqueda.Visible = false;
            btnBuscar.Visible = false;
        }

        protected void btnMostrarBusqueda_Click(object sender, EventArgs e)
        {
            if (btnMostrarBusqueda.Text == "Mostrar")
            {
                dgvEmpresas.Visible = false;
                dgvEmpresas.AddCssClass("hidden");

                seccionBusqueda.Visible = true;

                btnBorrarBusqueda.Visible = true;
                btnBuscar.Visible = true;

                btnMostrarBusqueda.Text = "Ocultar";
            }
            else
            {
                dgvEmpresas.Visible = true;
                dgvEmpresas.RemoveCssClass("hidden");

                seccionBusqueda.Visible = false;

                btnBorrarBusqueda.Visible = false;
                btnBuscar.Visible = false;

                btnMostrarBusqueda.Text = "Mostrar";
            }
        }

        #endregion

        #region Panel derecho (edicion)


        protected void btnNuevaEmpresa_Click(object sender, EventArgs e)
        {
            ActivarCamposDatos(true);
            uidEmpresa.Text = string.Empty;
            txtNombreComercial.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtGiro.Text = string.Empty;
            txtRFC.Text = string.Empty;
            txtFechaRegistro.Text = DateTime.Now.ToString("yyyy/MM/dd");
            FUImagen.Enabled = true;
            ImgEmpresas.ImageUrl = string.Empty;

            ViewState["Direcciones"] = new List<EmpresaDireccion>();
            DireccionRemoved.Clear();
            dgvDirecciones.DataSource = ViewState["Direcciones"];
            dgvDirecciones.DataBind();

            ViewState["Telefonos"] = new List<EmpresaTelefono>();
            TelefonoRemoved.Clear();
            dgvTelefonos.DataSource = ViewState["Telefonos"];
            dgvTelefonos.DataBind();


            btnAgregarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.Enabled = true;
        }

        protected void btnEditarEmpresa_Click(object sender, EventArgs e)
        {
            ActivarCamposDatos(true);
            btnAgregarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarDireccion.Enabled = true;
            btnAgregarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.Enabled = true;
            FUImagen.Enabled = true;
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
        }

        protected void btnOkEmpresa_Click(object sender, EventArgs e)
        {
            lblErrorEmpresa.Visible = true;
            Empresa empresa = null;
            bool error = false;
            bool rfcError = false;

            if (!string.IsNullOrWhiteSpace(uidEmpresa.Text))
            {
                VM.ObtenerEmpresa(new Guid(uidEmpresa.Text));
                empresa = VM.Empresa;
            }
            else
            {
                empresa = new Empresa();
            }

            // Eliminar marcas de error
            frmGrpRFC.RemoveCssClass("has-error");
            frmGrpNombreComercial.RemoveCssClass("has-error");
            frmGrpRazonSocial.RemoveCssClass("has-error");
            frmGrpGiro.RemoveCssClass("has-error");

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            frmGrpTelefono.RemoveCssClass("has-error");

            // Validación
            if (string.IsNullOrWhiteSpace(txtRFC.Text) || txtRFC.Text.Length < 12)
            {
                txtRFC.Focus();
                frmGrpRFC.AddCssClass("has-error");
                error = true;
                rfcError = true;
            }

            empresa.StrRFC = txtRFC.Text;

            if (string.IsNullOrWhiteSpace(txtNombreComercial.Text))
            {
                txtNombreComercial.Focus();
                frmGrpNombreComercial.AddCssClass("has-error");
                error = true;
            }

            empresa.StrNombreComercial = txtNombreComercial.Text;

            if (string.IsNullOrWhiteSpace(txtRazonSocial.Text))
            {
                txtRazonSocial.Focus();
                frmGrpRazonSocial.AddCssClass("has-error");
                error = true;
            }

            empresa.StrRazonSocial = txtRazonSocial.Text;

            if (string.IsNullOrWhiteSpace(txtGiro.Text))
            {
                txtGiro.Focus();
                frmGrpGiro.AddCssClass("has-error");
                error = true;
            }

            if (error)
            {
                lblErrorDireccion.Text = "Los campos marcados son obligatorios.";
                if (rfcError)
                    lblErrorDireccion.Text += " El campo RFC debe ser entre 12 y 13 caracteres.";
                return;
            }

            empresa.StrGiro = txtGiro.Text;
            if (ViewState["rutaimg"] != null)
            {
                string UidEmpresa = dgvEmpresas.SelectedDataKey.Value.ToString();
                VM.ObtenerEmpresa (new Guid(UidEmpresa));
                string Ruta = VM.Empresa.RutaImagen;

                if (File.Exists(Server.MapPath(Ruta)))
                {
                    File.Delete(Server.MapPath(Ruta));

                }
            }

            if (ViewState["rutaimg"] != null)
                empresa.RutaImagen = ViewState["rutaimg"].ToString();

            VM.GuardarEmpresa(empresa);
            txtRFC.Text = string.Empty;
            txtNombreComercial.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            txtGiro.Text = string.Empty;

            List<EmpresaDireccion> direcciones = (List<EmpresaDireccion>)ViewState["Direcciones"];
            VM.GuardarDirecciones(direcciones, empresa.UidEmpresa);

            VM.EliminarDirecciones(DireccionRemoved);

            List<EmpresaTelefono> telefonos = (List<EmpresaTelefono>)ViewState["Telefonos"];
            VM.GuardarTelefonos(telefonos, empresa.UidEmpresa);

            VM.EliminarTelefonos(TelefonoRemoved);

            ActivarCamposDatos(false);
            ActivarCamposDireccion(false);

            VM.ObtenerEmpresas();
            dgvEmpresas.DataSource = VM.Empresas;
            dgvEmpresas.DataBind();

            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataBind();

            dgvTelefonos.DataSource = null;
            dgvTelefonos.DataBind();

            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");

            btnOkDireccion.AddCssClass("disabled");
            btnCancelarDireccion.AddCssClass("disabled");

            ActivarCamposDireccion(false);

            btnAgregarTelefono.AddCssClass("disabled");
            btnEditarTelefono.AddCssClass("disabled");
            btnEliminarTelefono.AddCssClass("disabled");

            btnOKTelefono.AddCssClass("disabled").AddCssClass("hidden");
            btnCancelarTelefono.AddCssClass("disabled").AddCssClass("hidden");

            txtTelefono.Text = string.Empty;
            uidTelefono.Text = string.Empty;
            ddTipoTelefono.SelectedIndex = 0;
            FUImagen.Enabled = false;

            
        }

        protected void btnCancelarEmpresa_Click(object sender, EventArgs e)
        {
            ActivarCamposDatos(false);
            lblErrorEmpresa.Visible = false;
            lblErrorEmpresa.Text = "";
            lblErrorDireccion.Visible = false;
            lblErrorDireccion.Text = "";
            lblErrorTelefono.Visible = false;
            lblErrorTelefono.Text = "";
            FUImagen.Enabled = false;
            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");

            btnOkDireccion.AddCssClass("disabled");
            btnCancelarDireccion.AddCssClass("disabled");

            ActivarCamposDireccion(false);

            btnAgregarTelefono.AddCssClass("disabled");
            btnEditarTelefono.AddCssClass("disabled");
            btnEliminarTelefono.AddCssClass("disabled");

            btnOKTelefono.AddCssClass("disabled").AddCssClass("hidden");
            btnCancelarTelefono.AddCssClass("disabled").AddCssClass("hidden");

            txtTelefono.Text = string.Empty;
            uidTelefono.Text = string.Empty;
            ddTipoTelefono.SelectedIndex = 0;

            // Eliminar marcas de error
            frmGrpRFC.RemoveCssClass("has-error");
            frmGrpNombreComercial.RemoveCssClass("has-error");
            frmGrpRazonSocial.RemoveCssClass("has-error");
            frmGrpGiro.RemoveCssClass("has-error");

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            frmGrpTelefono.RemoveCssClass("has-error");

            btnCancelarEliminarDireccion_Click(sender, e);
            btnCancelarEliminarTelefono_Click(sender, e);

            if (uidEmpresa.Text.Length == 0)
            {
                uidEmpresa.Text = string.Empty;
                txtNombreComercial.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                txtGiro.Text = string.Empty;
                txtRFC.Text = string.Empty;
                txtFechaRegistro.Text = string.Empty;

                if (ViewState["Direcciones"] != null)
                {
                    dgvDirecciones.DataSource = ViewState["Direcciones"];
                    dgvDirecciones.DataBind();
                }
                if (ViewState["Telefonos"] != null)
                {
                    dgvTelefonos.DataSource = ViewState["Telefonos"];
                    dgvTelefonos.DataBind();
                }
                if (Session["RutaImagen"] != null)
                {
                    string Ruta = Session["RutaImagen"].ToString();

                    //Borra la imagen de la empresa
                    if (File.Exists(Server.MapPath(Ruta)))
                    {
                        File.Delete(Server.MapPath(Ruta));
                    }
                    //Recarga el controlador de la imagen con una imagen default
                    ImgEmpresas.ImageUrl = "Img/Default.jpg";
                    ImgEmpresas.DataBind();
                }
            }
            else
            {
                VM.ObtenerEmpresa(new Guid(uidEmpresa.Text));
                Session["EmpresaActual"] = VM.Empresa;
                Label lblEmpresa = (Label)Page.Master.FindControl("lblEmpresa");
                lblEmpresa.Text = VM.Empresa.StrNombreComercial;
                uidEmpresa.Text = VM.Empresa.UidEmpresa.ToString();
                txtNombreComercial.Text = VM.Empresa.StrNombreComercial;
                txtRazonSocial.Text = VM.Empresa.StrRazonSocial;
                txtGiro.Text = VM.Empresa.StrGiro;
                txtRFC.Text = VM.Empresa.StrRFC;
                txtFechaRegistro.Text = VM.Empresa.DtFechaRegistro.ToString("dd/MM/yyyy");
                ImgEmpresas.ImageUrl = Page.ResolveUrl(VM.Empresa.RutaImagen);

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
                btnEditarEmpresa.AddCssClass("disabled");
                btnEditarEmpresa.Enabled = false;
            }
        }

        #endregion

        #region Panel derecho (tabs)

        protected void tabDatos_Click(object sender, EventArgs e)
        {
            lblErrorTelefono.Visible = false;
            lblErrorEmpresa.Visible = false;
            lblErrorDireccion.Visible = false;
            panelDatosEmpresa.Visible = true;
            activeDatos.Attributes["class"] = "active";
            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";
            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelEmpresa.Visible = true;
            }
        }

        protected void tabDirecciones_Click(object sender, EventArgs e)
        {

            lblErrorTelefono.Visible = false;
            lblErrorEmpresa.Visible = false;
            lblErrorDireccion.Visible = false;
            panelDatosEmpresa.Visible = false;
            activeDatos.Attributes["class"] = "";
            panelDirecciones.Visible = true;
            activeDirecciones.Attributes["class"] = "active";
            panelTelefonos.Visible = false;
            activeTelefonos.Attributes["class"] = "";
        }

        protected void tabTelefonos_Click(object sender, EventArgs e)
        {
            lblErrorTelefono.Visible = false;
            lblErrorEmpresa.Visible = false;
            lblErrorDireccion.Visible = false;
            panelDatosEmpresa.Visible = false;
            activeDatos.Attributes["class"] = "";
            panelDirecciones.Visible = false;
            activeDirecciones.Attributes["class"] = "";
            panelTelefonos.Visible = true;
            activeTelefonos.Attributes["class"] = "active";

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                panelEmpresa.Visible = true;
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
            bool error = false;
            EditingModeDireccion = false;
            lblErrorDireccion.Visible = true;
            lblErrorDireccion.Text = string.Empty;
            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            if (string.IsNullOrWhiteSpace(txtMunicipio.Text))
            {
                txtMunicipio.Focus();
                frmGrpMunicipio.AddCssClass("has-error");
                error = true;
            }

            if (string.IsNullOrWhiteSpace(txtCiudad.Text))
            {
                txtCiudad.Focus();
                frmGrpCiudad.AddCssClass("has-error");
                error = true;
            }

            if (string.IsNullOrWhiteSpace(txtColonia.Text))
            {
                txtColonia.Focus();
                frmGrpColonia.AddCssClass("has-error");
                error = true;
            }

            if (string.IsNullOrWhiteSpace(txtCalle.Text))
            {
                txtCalle.Focus();
                frmGrpCalle.AddCssClass("has-error");
                error = true;
            }

            if (string.IsNullOrWhiteSpace(txtConCalle.Text))
            {
                txtConCalle.Focus();
                frmGrpConCalle.AddCssClass("has-error");
                error = true;
            }

            if (string.IsNullOrWhiteSpace(txtYCalle.Text))
            {
                txtYCalle.Focus();
                frmGrpYCalle.AddCssClass("has-error");
                error = true;
            }

            if (string.IsNullOrWhiteSpace(txtNoExt.Text))
            {
                txtNoExt.Focus();
                frmGrpNoExt.AddCssClass("has-error");
                error = true;
            }

            if (error)
            {
                lblErrorDireccion.Text = "Los campos marcados son obligatorios";
                return;
            }

            List<EmpresaDireccion> direcciones = (List<EmpresaDireccion>)ViewState["Direcciones"];
            EmpresaDireccion direccion = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidDireccion.Text))
            {
                IEnumerable<EmpresaDireccion> dir = from d in direcciones where d.UidDireccion.ToString() == uidDireccion.Text select d;
                direccion = dir.First();
                pos = direcciones.IndexOf(direccion);
                direcciones.Remove(direccion);
            }
            else
            {
                direccion = new EmpresaDireccion();
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


            btnAgregarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnEditarDireccion.AddCssClass("disabled").AddCssClass("hidden");
            btnEliminarDireccion.AddCssClass("disabled").AddCssClass("hidden");

            panelEmpresa.Visible = true;
            panelDireccion.Visible = false;
        }

        protected void btnCancelarDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = false;
            lblErrorDireccion.Text = string.Empty;
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

            panelEmpresa.Visible = true;
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
            List<EmpresaDireccion> direcciones = (List<EmpresaDireccion>)ViewState["Direcciones"];
            EmpresaDireccion empresaDireccion = direcciones.Select(x => x).Where(x => x.UidDireccion.ToString() == dgvDirecciones.SelectedDataKey.Value.ToString()).First();
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
            panelEmpresa.Visible = false;

            btnCancelarDireccion.Visible = false;
            btnOkDireccion.Visible = false;
            btnCerrarDireccion.Visible = true;
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
            btnCerrarDireccion.Visible = false;
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

            panelEmpresa.Visible = false;
            panelDireccion.Visible = true;

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

            panelEmpresa.Visible = false;
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

            List<EmpresaDireccion> direcciones = (List<EmpresaDireccion>)ViewState["Direcciones"];
            EmpresaDireccion direccion = direcciones.Select(x => x).Where(x => x.UidDireccion == uid).First();
            direcciones.Remove(direccion);
            DireccionRemoved.Add(direccion);
            dgvDirecciones.DataSource = direcciones;
            dgvDirecciones.DataBind();

            btnAgregarDireccion.RemoveCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");
            btnAceptarEliminarDireccion.Visible = false;
            btnCancelarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;
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

        protected void dgvTelefonos_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<EmpresaTelefono> telefonos = (List<EmpresaTelefono>)ViewState["Telefonos"];
            EmpresaTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono.ToString() == dgvTelefonos.SelectedDataKey.Value.ToString()).First();

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
            List<EmpresaTelefono> telefonos = (List<EmpresaTelefono>)ViewState["Telefonos"];
            EmpresaTelefono telefono = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidTelefono.Text))
            {
                IEnumerable<EmpresaTelefono> dir = from t in telefonos where t.UidTelefono.ToString() == uidTelefono.Text select t;
                telefono = dir.First();
                pos = telefonos.IndexOf(telefono);
                telefonos.Remove(telefono);
            }
            else
            {
                telefono = new EmpresaTelefono();
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

                List<EmpresaTelefono> telefonos = (List<EmpresaTelefono>)ViewState["Telefonos"];
                EmpresaTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono.ToString() == dgvTelefonos.SelectedDataKey.Value.ToString()).First();

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

            List<EmpresaTelefono> telefonos = (List<EmpresaTelefono>)ViewState["Telefonos"];
            EmpresaTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono == uid).First();
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

        protected void btnCancelarEliminarDireccion_Click(object sender, EventArgs e)
        {
            btnCancelarEliminarDireccion.Visible = false;
            btnAceptarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;
        }

        protected void btnCerrarDireccion_Click(object sender, EventArgs e)
        {
            panelDireccion.Visible = false;
            panelEmpresa.Visible = true;
            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
        }

        protected void btnUsuario_Click(object sender, EventArgs e)
        {
            Response.Redirect("Usuarios.aspx");
        }

        #region imagen

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
                        string ruta = "~/Vista/Imagenes/Empresas/"+ uidEmpresa.Text +'_'+ numero + Nombrearchivo;


                        //guardar img
                        FUImagen.SaveAs(Server.MapPath(ruta));

                        string rutaimg = ruta + "?" + (numero - 1);

                        ViewState["rutaimg"] = ruta;

                        ImgEmpresas.ImageUrl = rutaimg;

                    }
                }
            }
        }
        #endregion
    }
}