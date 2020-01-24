using CodorniX.Modelo;
using CodorniX.Util;
using CodorniX.VistaDelModelo;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Web.UI.HtmlControls;

namespace CodorniX.Vista
{
    public partial class Usuarios : System.Web.UI.Page
    {
        VMUsuarios VM = new VMUsuarios();
        VMLogin _CVMLogin = new VMLogin();
        #region propiedades

        private List<UsuarioDireccion> DireccionRemoved
        {
            get
            {
                if (ViewState["DireccionRemoved"] == null)
                    ViewState["DireccionRemoved"] = new List<UsuarioDireccion>();

                return (List<UsuarioDireccion>)ViewState["DireccionRemoved"];
            }
        }

        private List<UsuarioTelefono> TelefonoRemoved
        {
            get
            {
                if (ViewState["TelefonoRemoved"] == null)
                    ViewState["TelefonoRemoved"] = new List<UsuarioTelefono>();

                return (List<UsuarioTelefono>)ViewState["TelefonoRemoved"];
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

        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

        private List<Guid> ModulosBacksite
        {
            get
            {
                if (ViewState["ModulosBackside"] == null)
                    ViewState["ModulosBackside"] = new List<Guid>();

                return (List<Guid>)ViewState["ModulosBackside"];
            }
        }

        private List<Guid> ModulosBackend
        {
            get
            {
                if (ViewState["ModulosBackend"] == null)
                    ViewState["ModulosBackend"] = new List<Guid>();

                return (List<Guid>)ViewState["ModulosBackend"];
            }
        }

        private List<Guid> ModulosFrontend
        {
            get
            {
                if (ViewState["ModulosFrontend"] == null)
                    ViewState["ModulosFrontend"] = new List<Guid>();

                return (List<Guid>)ViewState["ModulosFrontend"];
            }
        }

        private bool Generated = false;

        #endregion

        #region Private methods

        private void GenerarCampos(bool regenerate, List<Modulo> modulos, List<Guid> uidModulos, List<Modulo> modulosDenegados, PlaceHolder holder)
        {
            if (regenerate)
                holder.Controls.Clear();

            uidModulos.Clear();

            foreach (Modulo modulo in modulos)
            {
                Panel panel = new Panel();
                panel.AddCssClass("col-xs-6");
                Panel div = new Panel();
                div.AddCssClass("checkbox");
                HtmlGenericControl label = new HtmlGenericControl("label");
                CheckBox checkBox = new CheckBox();
                checkBox.ID = modulo.UidModulo.ToString();
                checkBox.AutoPostBack = false;
                label.Controls.Add(checkBox);
                Label name = new Label();
                name.Text = modulo.StrModulo;
                label.Controls.Add(name);
                div.Controls.Add(label);
                panel.Controls.Add(div);
                holder.Controls.Add(panel);

                uidModulos.Add(modulo.UidModulo);
                if (regenerate)
                {
                    checkBox.Checked = true;
                    foreach (Modulo usuarioModulo in modulosDenegados)
                    {
                        if (usuarioModulo.UidModulo == modulo.UidModulo)
                        {
                            checkBox.Checked = false;
                        }
                    }
                }
            }
        }

        private void GenerarCampos(bool regenerate = false)
        {
            if (!regenerate && Generated)
                return;

            Guid perfil = new Guid(DdPerfil.SelectedValue);
            VM.ObtenerPerfil(perfil);
            Guid nivelAcceso = VM.CLASSPERFIL.UidNivelAcceso;
            VM.ObtenerModulosPerfil(new Guid(DdPerfil.SelectedValue), nivelAcceso);

            VM.ObtenerModulosPerfil(new Guid(DdPerfil.SelectedValue));
            if (!string.IsNullOrWhiteSpace(txtUidUsuario.Text))
                VM.ObtenerModulosUsuario(new Guid(txtUidUsuario.Text));
            else
                VM.ObtenerModulosUsuario(Guid.Empty);

            VM.ObtenerNivelesDeAcceso();
            VM.ObtenerNivelAcceso(nivelAcceso);

            IEnumerable<NivelAcceso> acceso;
            acceso = from e in VM.NivelAccesos where e.StrNivelAcceso == "Backsite" select e;
            VM.ObtenerModulosPerfil(perfil, acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.ModulosPerfil, ModulosBacksite, VM.ModulosUsuario, modulosBackside);

            acceso = from e in VM.NivelAccesos where e.StrNivelAcceso == "Backend" select e;
            VM.ObtenerModulosPerfil(perfil, acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.ModulosPerfil, ModulosBackend, VM.ModulosUsuario, modulosBackend);

            /*acceso = from e in VM.NivelAccesos where e.StrNivelAcceso == "Frontend" select e;
            VM.ObtenerModulosPerfil(perfil, acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.ModulosPerfil, ModulosFrontend, VM.ModulosUsuario, modulosFrontend);*/

            Generated = true;

            string nivel = VM.NivelAcceso.StrNivelAcceso;

            if (nivel == "Backsite")
            {
                accesoBackside.Visible = true;
                accesoBackend.Visible = false;
                accesoFrontend.Visible = false;

                activeBackside.Visible = true;
                activeBackend.Visible = true;
                activeFrontend.Visible = true;

                activeBackside.AddCssClass("active");
                activeBackend.RemoveCssClass("active");
                activeFrontend.RemoveCssClass("active");

                tabBackside.Disable();
                tabBackend.Enable();
                tabFrontend.Enable();
            }
            else if (nivel == "Backend")
            {
                ModulosBacksite.Clear();
                accesoBackside.Visible = false;
                accesoBackend.Visible = true;
                accesoFrontend.Visible = false;

                activeBackside.Visible = false;
                activeBackend.Visible = true;
                activeFrontend.Visible = true;

                activeBackside.RemoveCssClass("active");
                activeBackend.AddCssClass("active");
                activeFrontend.RemoveCssClass("active");

                tabBackside.Enable();
                tabBackend.Disable();
                tabFrontend.Enable();
            }
            else if (nivel == "Frontend")
            {
                ModulosBacksite.Clear();
                ModulosBackend.Clear();

                accesoBackside.Visible = false;
                accesoBackend.Visible = false;
                accesoFrontend.Visible = true;

                activeBackside.Visible = false;
                activeBackend.Visible = false;
                activeFrontend.Visible = true;

                activeBackside.RemoveCssClass("active");
                activeBackend.RemoveCssClass("active");
                activeFrontend.AddCssClass("active");

                tabBackside.Enable();
                tabBackend.Enable();
                tabFrontend.Disable();
            }
        }

        private void GuardarModulos(Guid uidUsuario)
        {
            List<Guid> permisos = new List<Guid>();
            List<Guid> modulos = ModulosBacksite;
            foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosBackside.FindControl(controlID);
                if (!box.Checked)
                    permisos.Add(modulo);
            }
            modulos = ModulosBackend;
            foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosBackend.FindControl(controlID);
                if (!box.Checked)
                    permisos.Add(modulo);
            }
            modulos = ModulosFrontend;
            foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosFrontend.FindControl(controlID);
                if (!box.Checked)
                    permisos.Add(modulo);
            }
            VM.ActualizarModulosUsuario(uidUsuario, permisos);
        }

        private void ActivarCamposModulo(bool enable)
        {
            List<Guid> modulos = ModulosBacksite;
            foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosBackside.FindControl(controlID);
                if (enable)
                    box.Enable();
                else
                    box.Disable();
            }
            modulos = ModulosBackend;
            foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosBackend.FindControl(controlID);
                if (enable)
                    box.Enable();
                else
                    box.Disable();
            }
            modulos = ModulosFrontend;
            foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosFrontend.FindControl(controlID);
                if (enable)
                    box.Enable();
                else
                    box.Disable();
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

        protected void ddPais_SelectedIndexChanged(object sender, EventArgs e)
        {
            Guid uid = new Guid(ddPais.SelectedValue.ToString());
            VM.ObtenerEstados(uid);
            ddEstado.DataSource = VM.Estados;
            ddEstado.DataValueField = "UidEstado";
            ddEstado.DataTextField = "StrNombre";
            ddEstado.DataBind();
        }

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            FUImagen.Attributes["onchange"] = "upload(this)";
            if (SesionActual == null)
                return;

            if (!Acceso.TieneAccesoAModulo("Usuarios", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }

            if (!IsPostBack)
            {
                panelAccesos.Visible = false;

                Site1 master = (Site1)Master;
                master.ActivarAdministrador();

                #region Panel Izquierdo

                panelDireccion.Visible = false;
                panelDirecciones.Visible = false;
                panelTelefonos.Visible = false;
                PanelDatosGeneralesUsuario.Visible = false;

                lblFiltros.Text = "Ocultar";
                DVGUSUsuarios.Visible = false;
                DVGUSUsuarios.DataSource = null;
                DVGUSUsuarios.DataBind();

                VM.CargarPerfiles();
                lblPerfil.DataSource = VM.LISTADEPERFIL;
                lblPerfil.DataTextField = "strPerfil";
                lblPerfil.DataValueField = "UidPerfil";
                lblPerfil.DataBind();

                VM.CargarListaDeStatus();
                lblStatus.DataSource = VM.LISTADESTATUS;
                lblStatus.DataTextField = "strStatus";
                lblStatus.DataValueField = "UidStatus";
                lblStatus.DataBind();

                VM.ObtenerEmpresa();
                lbEmpresas.DataSource = VM.Empresas;
                lbEmpresas.DataValueField = "UidEmpresa";
                lbEmpresas.DataTextField = "StrNombreComercial";
                lbEmpresas.DataBind();

                #endregion

                #region panel derecho

                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
                txtFechaNacimiento.Enabled = false;
                //Cajas de texto
                txtNombre.Enabled = false;
                txtApellidoPaterno.Enabled = false;
                txtApellidoMaterno.Enabled = false;
                txtFechaNacimiento.Enabled = false;
                txtCorreo.Enabled = false;
                //txtCurp.Enabled = false;
                txtFechaInicio.Enabled = false;
                txtFechaFin.Enabled = false;
                txtUsuario.Enabled = false;
                txtPassword.Enabled = false;
                DdPerfil.Enabled = false;
                DdPerfil.CssClass = "form-control";

                txtRfc.Enabled = false;
                txtRazonSocial.Enabled = false;
                txtNombreComercial.Enabled = false;
                btnBuscarEmpresa.Enabled = false;
                txtUidEmpresa.Text = string.Empty;

                DdPerfil.DataSource = VM.LISTADEPERFIL;
                DdPerfil.DataTextField = "strPerfil";
                DdPerfil.DataValueField = "UidPerfil";
                DdPerfil.DataBind();

                DdStatus.Enabled = false;
                DdStatus.CssClass = "form-control";

                DdStatus.DataSource = VM.LISTADESTATUS;
                DdStatus.DataTextField = "strStatus";
                DdStatus.DataValueField = "UidStatus";
                DdStatus.DataBind();

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

                btnAceptarEliminarTelefono.Visible = false;
                btnCancelarEliminarTelefono.Visible = false;
                lblAceptarEliminarTelefono.Visible = false;
                FUImagen.Enabled = false;

                btnAceptarEliminarDireccion.Visible = false;
                lblAceptarEliminarDireccion.Visible = false;
                btnCancelarEliminarDireccion.Visible = false;
                if (Session["EmpresaActual"] != null)
                {
                    Empresa empresa = (Empresa)Session["EmpresaActual"];
                    txtEmpresa.Text = empresa.UidEmpresa.ToString();
                }
                ImgUsuario.ImageUrl = "Imagenes/logotipo2019.1.0.400x400.png";
                ImgUsuario.DataBind();
                #endregion

                GenerarCampos(true);
                ActivarCamposModulo(false);
            }
            else
            {
                GenerarCampos();
            }
        }

        #region Temporal

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

            bool error = false;

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");
            lblErrorDireccion.Text = string.Empty;

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
                lblErrorDireccion.Text = "Los campos marcados son obligatorios.";
                return;
            }

            List<UsuarioDireccion> direcciones = (List<UsuarioDireccion>)ViewState["Direcciones"];
            UsuarioDireccion direccion = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidDireccion.Text))
            {
                IEnumerable<UsuarioDireccion> dir = from d in direcciones where d.UidDireccion.ToString() == uidDireccion.Text select d;
                direccion = dir.First();
                pos = direcciones.IndexOf(direccion);
                direcciones.Remove(direccion);
            }
            else
            {
                direccion = new UsuarioDireccion();
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


            btnAgregarDireccion.Enable();
            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();

            PanelBusquedas.Visible = true;
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
            lblErrorDireccion.Text = string.Empty;

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

            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();




            PanelBusquedas.Visible = true;
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
            List<UsuarioDireccion> direcciones = (List<UsuarioDireccion>)ViewState["Direcciones"];
            UsuarioDireccion empresaDireccion = direcciones.Select(x => x).Where(x => x.UidDireccion.ToString() == dgvDirecciones.SelectedDataKey.Value.ToString()).First();
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

                btnEditarDireccion.Enable();
                btnEliminarDireccion.Enable();


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
            PanelBusquedas.Visible = false;

            btnCancelarDireccion.Visible = false;
            btnOkDireccion.Visible = false;
            btnCerrarDireccion.Visible = true;
        }

        protected void btnAgregarDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = true;
            btnAgregarDireccion.Disable();
            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();
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

            PanelBusquedas.Visible = false;
            panelDireccion.Visible = true;

            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;

            int pos = -1;
            if (ViewState["DireccionPreviousRow"] != null)
            {
                pos = (int)ViewState["DireccionPreviousRow"];
                if (pos < dgvDirecciones.Rows.Count)
                {
                    GridViewRow previousRow = dgvDirecciones.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }
            }
        }

        protected void btnEditarDireccion_Click(object sender, EventArgs e)
        {
            EditingModeDireccion = true;

            btnAgregarDireccion.Disable();
            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();
            ActivarCamposDireccion(true);

            PanelBusquedas.Visible = false;
            panelDireccion.Visible = true;

            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
            btnCerrarDireccion.Visible = false;
        }

        protected void btnEliminarDireccion_Click(object sender, EventArgs e)
        {
            btnAceptarEliminarDireccion.Visible = true;
            lblAceptarEliminarDireccion.Visible = true;
            lblAceptarEliminarDireccion.Text = "Desea Eliminar La Direccion Seleccionada. ";

            btnCancelarEliminarDireccion.Visible = true;
        }

        protected void btnAceptarEliminarDireccion_Click(object sender, EventArgs e)
        {
            ActivarCamposDireccion(false);
            Guid uid = new Guid(uidDireccion.Text);

            List<UsuarioDireccion> direcciones = (List<UsuarioDireccion>)ViewState["Direcciones"];
            UsuarioDireccion direccion = direcciones.Select(x => x).Where(x => x.UidDireccion == uid).First();
            direcciones.Remove(direccion);
            DireccionRemoved.Add(direccion);
            dgvDirecciones.DataSource = direcciones;
            dgvDirecciones.DataBind();

            btnAgregarDireccion.Enable();
            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();
            btnAceptarEliminarDireccion.Visible = false;
            btnCancelarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;
        }

        protected void btnCancelarEliminarDireccion_Click(object sender, EventArgs e)
        {
            btnAceptarEliminarDireccion.Visible = false;
            btnCancelarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;
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
            List<UsuarioTelefono> telefonos = (List<UsuarioTelefono>)ViewState["Telefonos"];
            UsuarioTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono.ToString() == dgvTelefonos.SelectedDataKey.Value.ToString()).First();

            uidTelefono.Text = telefono.UidTelefono.ToString();
            txtTelefono.Text = telefono.StrTelefono;
            ddTipoTelefono.SelectedValue = telefono.UidTipoTelefono.ToString();

            if (EditingMode)
            {
                btnEditarTelefono.Enable();
                btnEliminarTelefono.Enable();
            }
            btnOKTelefono.Disable(); ;
            btnCancelarTelefono.Disable();

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
            frmGrpTelefono.RemoveCssClass("has-error");
            lblErrorTelefono.Text = string.Empty;

            if (string.IsNullOrWhiteSpace(txtTelefono.Text))
            {
                lblErrorTelefono.Text = "El campo Telefono no debe estar vacío";
                txtTelefono.Focus();
                frmGrpTelefono.AddCssClass("has-error");

                return;
            }
            List<UsuarioTelefono> telefonos = (List<UsuarioTelefono>)ViewState["Telefonos"];
            UsuarioTelefono telefono = null;
            int pos = -1;
            if (!string.IsNullOrWhiteSpace(uidTelefono.Text))
            {
                IEnumerable<UsuarioTelefono> dir = from t in telefonos where t.UidTelefono.ToString() == uidTelefono.Text select t;
                telefono = dir.First();
                pos = telefonos.IndexOf(telefono);
                telefonos.Remove(telefono);
            }
            else
            {
                telefono = new UsuarioTelefono();
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

            if (uidTelefono.Text.Length == 0)
            {
                btnEditarTelefono.Disable();
                btnEliminarTelefono.Disable();
            }
            else
            {
                btnEditarTelefono.Disable();
                btnEliminarTelefono.Disable();
            }

            btnAgregarTelefono.RemoveCssClass("disabled");
            btnAgregarTelefono.Enabled = true;
        }

        protected void btnCancelarTelefono_Click(object sender, EventArgs e)
        {
            frmGrpTelefono.RemoveCssClass("has-error");
            lblErrorTelefono.Text = string.Empty;

            txtTelefono.Enabled = false;
            txtTelefono.AddCssClass("disabled");
            ddTipoTelefono.SelectedIndex = 0;
            ddTipoTelefono.AddCssClass("disabled");
            ddTipoTelefono.Enabled = false;

            btnOKTelefono.AddCssClass("hidden").AddCssClass("disabled");
            btnOKTelefono.Enabled = false;
            btnCancelarTelefono.AddCssClass("hidden").AddCssClass("disabled");
            btnCancelarTelefono.Enabled = false;

            if (uidTelefono.Text.Length == 0)
            {
                btnEditarTelefono.Disable();
                btnEliminarTelefono.Disable();
                uidTelefono.Text = string.Empty;
                txtTelefono.Text = string.Empty;
            }
            else
            {
                btnEditarTelefono.Enable();
                btnEliminarTelefono.Enable();

                List<UsuarioTelefono> telefonos = (List<UsuarioTelefono>)ViewState["Telefonos"];
                UsuarioTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono.ToString() == dgvTelefonos.SelectedDataKey.Value.ToString()).First();

                uidTelefono.Text = telefono.UidTelefono.ToString();
                txtTelefono.Text = telefono.StrTelefono;
                ddTipoTelefono.SelectedValue = telefono.UidTipoTelefono.ToString();
            }

            btnAgregarTelefono.RemoveCssClass("disabled");
            btnAgregarTelefono.Enabled = true;
        }

        protected void btnEditarTelefono_Click(object sender, EventArgs e)
        {
            txtTelefono.Enabled = true;
            txtTelefono.RemoveCssClass("disabled");

            ddTipoTelefono.Enabled = true;
            ddTipoTelefono.RemoveCssClass("disabled");

            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.AddCssClass("disabled");

            btnEditarTelefono.Disable();

            btnEliminarTelefono.Disable();

            btnOKTelefono.Enabled = true;
            btnOKTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");

            btnCancelarTelefono.Enabled = true;
            btnCancelarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
        }

        protected void btnEliminarTelefono_Click(object sender, EventArgs e)
        {
            lblAceptarEliminarTelefono.Visible = true;
            lblAceptarEliminarTelefono.Text = "¿Desea Eliminar El Telefono Seleccionado?";
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

            List<UsuarioTelefono> telefonos = (List<UsuarioTelefono>)ViewState["Telefonos"];
            UsuarioTelefono telefono = telefonos.Select(x => x).Where(x => x.UidTelefono == uid).First();
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
            btnEliminarTelefono.Disable();
            btnEditarTelefono.Disable();
        }

        protected void btnCancelarEliminarTelefono_Click(object sender, EventArgs e)
        {
            btnAceptarEliminarTelefono.Visible = false;
            btnCancelarEliminarTelefono.Visible = false;
            lblAceptarEliminarTelefono.Visible = false;
            if (uidTelefono.Text.Length > 0)
            {
                btnEliminarTelefono.Enable();
                btnEditarTelefono.Enable();
            }
            else
            {
                btnEliminarTelefono.Disable();
                btnEditarTelefono.Disable();
            }
        }

        #endregion

        #endregion

        #region Panel Izquierdo

        protected void LimpiarFiltros(object sender, EventArgs e)
        {
            FiltroNombre.Text = string.Empty;
            FiltroUsuario.Text = string.Empty;
            FiltroApellidoPaterno.Text = string.Empty;
            FiltroApellidoMaterno.Text = string.Empty;
            FiltroFechaNacimiento.Text = string.Empty;
            FiltroFechaNacimiento2.Text = string.Empty;
            FiltroFechaInicio.Text = string.Empty;
            FiltroFechaInicio2.Text = string.Empty;
            FiltroFechaFin.Text = string.Empty;
            FiltroFechaFin2.Text = string.Empty;
            FiltroCorreo.Text = string.Empty;
            lblPerfil.ClearSelection();
            lblStatus.ClearSelection();
            lbEmpresas.ClearSelection();
            DVGUSUsuarios.DataSource = VM.LISTADEUSUARIOS;
            DVGUSUsuarios.DataBind();
        }

        protected void btnVisibilidadPanelFiltros(object sender, EventArgs e)
        {
            string texto = lblFiltros.Text;
            DVGUSUsuarios.Visible = false;
            if (texto == "Mostrar")
            {

                //Activa botones y agrega clases
                btnBuscar.Enabled = true;
                btnBuscar.CssClass = "btn btn-sm btn-default";
                btnLimpiar.Enabled = true;
                btnLimpiar.CssClass = "btn btn-sm btn-default";
                lblFiltros.Text = "Ocultar";
                lblFiltros.CssClass = "glyphicon glyphicon-collapse-up";

                //Muestra panel de filtros
                PanelFiltros.Visible = true;

            }
            else if (texto == "Ocultar")
            {
                //Visivilidad de panel

                PanelFiltros.Visible = false;
                //Desactivar botones y cambia clase css
                btnBuscar.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnLimpiar.Enabled = false;
                btnLimpiar.CssClass = "btn btn-sm btn-default disabled";
                //cambia texto de label
                lblFiltros.Text = "Mostrar";
                lblFiltros.CssClass = "glyphicon glyphicon-collapse-down";
                DVGUSUsuarios.Visible = true;


            }
        }

        protected void DVGUSUsuarios_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtNombre.Enabled = false;
            txtApellidoPaterno.Enabled = false;
            txtApellidoMaterno.Enabled = false;
            txtFechaNacimiento.Enabled = false;
            txtCorreo.Enabled = false;
            txtFechaInicio.Enabled = false;
            txtFechaFin.Enabled = false;
            txtUsuario.Enabled = false;
            txtPassword.Enabled = false;
            DdPerfil.Enabled = false;
            DdStatus.Enabled = false;
            string UidUsuario = DVGUSUsuarios.SelectedDataKey.Value.ToString();
            VM.Obtenerusuario(new Guid(UidUsuario));
            txtUidUsuario.Text = VM.CLASSUSUARIO.UIDUSUARIO.ToString();
            txtNombre.Text = VM.CLASSUSUARIO.STRNOMBRE;
            txtApellidoPaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOPATERNO;
            txtApellidoMaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOMATERNO;
            txtFechaNacimiento.Text = VM.CLASSUSUARIO.DtFechaNacimiento.ToString("dd/MM/yyyy");
            txtCorreo.Text = VM.CLASSUSUARIO.STRCORREO;
            txtFechaInicio.Text = VM.CLASSUSUARIO.DtFechaInicio.ToString("dd/MM/yyyy");
            txtFechaFin.Text = VM.CLASSUSUARIO.DtFechaFin?.ToString("dd/MM/yyyy");
            txtUsuario.Text = VM.CLASSUSUARIO.STRUSUARIO;
            txtPassword.Text = VM.CLASSUSUARIO.STRPASSWORD;
            DdStatus.SelectedIndex = DdStatus.Items.IndexOf(DdStatus.Items.FindByValue(VM.CLASSUSUARIO.UidStatus.ToString()));
            ImgUsuario.ImageUrl = Page.ResolveUrl(VM.CLASSUSUARIO.RutaImagen);

            VM.ObtenerUsuarioPerfilEmpresa(new Guid(UidUsuario));
            DdPerfil.SelectedIndex = DdPerfil.Items.IndexOf(DdPerfil.Items.FindByValue(VM.UsuarioPerfilEmpresa.UidPerfil.ToString()));
            ViewState["Empresa"] = VM.UsuarioPerfilEmpresa;
            txtUidEmpresa.Text = VM.UsuarioPerfilEmpresa.UidEmpresa.ToString();
            VM.ObtenerEmpresaUsuario(txtUidEmpresa.Text);
            txtRfc.Text = VM.CEmpresa.StrRFC;
            txtRazonSocial.Text = VM.CEmpresa.StrRazonSocial;
            txtNombreComercial.Text = VM.CEmpresa.StrNombreComercial;

            VM.ObtenerTelefonos();
            ViewState["Telefonos"] = VM.Telefonos;
            dgvTelefonos.DataSource = VM.Telefonos;
            dgvTelefonos.DataBind();
            btnEditar.Enabled = true;
            btnEditar.CssClass = "btn btn-sm btn-default";

            VM.ObtenerDirecciones();
            ViewState["Direcciones"] = VM.Direcciones;
            dgvDirecciones.DataSource = VM.Direcciones;
            dgvDirecciones.DataBind();

            btnAgregarDireccion.Disable();
            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();

            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.AddCssClass("disabled");
            btnEditarTelefono.Disable();
            btnEliminarTelefono.Disable();

            if (EditingMode)
            {
                btnCancelar_Click(null, null);
            }

            int pos = -1;
            if (ViewState["EmpresaPreviousRow"] != null)
            {
                pos = (int)ViewState["EmpresaPreviousRow"];
                GridViewRow previousRow = DVGUSUsuarios.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["EmpresaPreviousRow"] = DVGUSUsuarios.SelectedIndex;
            DVGUSUsuarios.SelectedRow.AddCssClass("success");
            GenerarCampos(true);
            ActivarCamposModulo(false);
        }

        protected void DVGUSUsuarios_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DVGUSUsuarios, "Select$" + e.Row.RowIndex);

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                Label PERFIL = e.Row.FindControl("lblPerfil") as Label;
                if (e.Row.Cells[6].Text == "Administrador")
                {
                    //PERFIL.CssClass = "glyphicon glyphicon-user";
                    PERFIL.CssClass = "glyphicon glyphicon-font";
                    PERFIL.ToolTip = "Administrador";
                }
                if (e.Row.Cells[6].Text == "Superadministrador")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-user";
                    // PERFIL.CssClass = "glyphicon glyphicon-text-color";
                    PERFIL.ToolTip = "SuperAdministrador";
                }
                if (e.Row.Cells[6].Text == "Encargado")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-user";
                    PERFIL.ToolTip = "Encargado";
                }
                if (e.Row.Cells[6].Text == "Supervisor")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-user";
                    PERFIL.ToolTip = "Supervisor";
                }
                if (e.Row.Cells[6].Text == "Empleado")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-user";
                    PERFIL.ToolTip = "Empleado";
                }
                if (e.Row.Cells[8].Text == "Activo")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-ok";
                    ESTATUS.ToolTip = "Activo";
                }
                if (e.Row.Cells[8].Text == "Inactivo")
                {
                    ESTATUS.CssClass = "glyphicon glyphicon-remove";
                    ESTATUS.ToolTip = "Inactivo";
                }
            }
        }

        protected void btnCerrarDireccion_Click(object sender, EventArgs e)
        {
            panelDireccion.Visible = false;
            PanelBusquedas.Visible = true;
            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
        }

        private void SortUsuario(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            List<Modelo.Usuario> usuario = (List<Modelo.Usuario>)base.ViewState["Usuario"];

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
                    usuario = usuario.OrderBy(x => x.STRNOMBRE).ToList();
                }
                else
                {
                    usuario = usuario.OrderByDescending(x => x.STRNOMBRE).ToList();
                }
            }
            else if (SortExpression == "ApellidoPaterno")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    usuario = usuario.OrderBy(x => x.STRAPELLIDOPATERNO).ToList();
                }
                else
                {
                    usuario = usuario.OrderByDescending(x => x.STRAPELLIDOPATERNO).ToList();
                }
            }
            else if (SortExpression == "Usuario")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    usuario = usuario.OrderBy(x => x.STRUSUARIO).ToList();
                }
                else
                {
                    usuario = usuario.OrderByDescending(x => x.STRUSUARIO).ToList();
                }
            }
            else
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    usuario = usuario.OrderBy(x => x.DtFechaInicio).ToList();
                }
                else
                {
                    usuario = usuario.OrderByDescending(x => x.DtFechaInicio).ToList();
                }
            }
            DVGUSUsuarios.DataSource = usuario;
            ViewState["SortColumn"] = SortExpression;
            ViewState["SortColumnDirection"] = SortDirection;
        }

        protected void DVGUSUsuarios_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["EmpresaPreviousRow"] = null;
            SortUsuario(e.SortExpression, e.SortDirection);

            DVGUSUsuarios.DataBind();
        }

        protected void DVGUSUsuarios_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ViewState["EmpresaPreviousRow"] = null;
            if (ViewState["SortColumn"] != null && ViewState["SortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["SortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["SortColumnDirection"];
                SortUsuario(SortExpression, SortDirection, true);
            }
            else
            {
                DVGUSUsuarios.DataSource = ViewState["Usuario"];
            }
            DVGUSUsuarios.PageIndex = e.NewPageIndex;
            DVGUSUsuarios.DataBind();
        }
        #endregion

        #region Panel Derecho
        protected void btnNuevo_ActivarCampos(object sender, EventArgs e)
        {
            //Cajas de Texto
            ImgUsuario.ImageUrl = string.Empty;
            txtNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtFechaNacimiento.Enabled = true;
            txtCorreo.Enabled = true;
            // txtCurp.Enabled = true;
            txtFechaInicio.Enabled = true;
            txtFechaFin.Enabled = true;
            txtUsuario.Enabled = true;
            txtPassword.Enabled = true;
            FUImagen.Enabled = true;
            btnEditar.Disable();
            btnNuevo.Disable();
            DdPerfil.Enabled = true;
            DdPerfil.CssClass = "form-control";

            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;
            UidEmpresa.Value = string.Empty;
            txtUidEmpresa.Text = string.Empty;
            txtRazonSocial.Enabled = true;
            txtRfc.Enabled = true;
            txtNombreComercial.Enabled = true;
            txtNombreComercial.Text = string.Empty;
            txtRfc.Text = string.Empty;
            txtRazonSocial.Text = string.Empty;
            btnBuscarEmpresa.Enable();
            DdPerfil.SelectedIndex = 0;
            DdStatus.Enabled = true;
            if (txtFechaInicio.Text != string.Empty)
            {
                string fechainiciotexto = txtFechaInicio.Text;
                txtFechaFin.Text = fechainiciotexto;
            }


            //botones
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            txtUidUsuario.Text = string.Empty;
            //limpia cajas de texto
            txtNombre.Text = string.Empty;
            txtApellidoPaterno.Text = string.Empty;
            txtApellidoMaterno.Text = string.Empty;
            txtFechaNacimiento.Text = string.Empty;
            txtCorreo.Text = string.Empty;
            //txtCurp.Text = string.Empty;
            txtFechaInicio.Text = string.Empty;
            txtFechaFin.Text = string.Empty;
            txtUsuario.Text = string.Empty;
            txtPassword.Text = string.Empty;
            lblMensaje.Text = string.Empty;
            btnAgregarTelefono.Enabled = true;
            btnAgregarTelefono.CssClass = "btn btn-sm btn-default";
            btnAgregarDireccion.Enabled = true;
            btnAgregarDireccion.CssClass = "btn btn-sm btn-default";

            ViewState["Direcciones"] = new List<UsuarioDireccion>();
            DireccionRemoved.Clear();
            dgvDirecciones.DataSource = ViewState["Direcciones"];
            dgvDirecciones.DataBind();

            ViewState["Telefonos"] = new List<UsuarioTelefono>();
            TelefonoRemoved.Clear();
            dgvTelefonos.DataSource = ViewState["Telefonos"];
            dgvTelefonos.DataBind();

            GenerarCampos(true);
            ActivarCamposModulo(true);
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            EditingMode = false;

            lblErrorUsuario.Text = string.Empty;
            lblErrorDireccion.Text = string.Empty;
            lblErrorTelefono.Text = string.Empty;

            frmGrpNombre.RemoveCssClass("has-error");
            frmGrpApellidoPaterno.RemoveCssClass("has-error");
            frmGrpApellidoMaterno.RemoveCssClass("has-error");
            frmGrpFechaNacimiento.RemoveCssClass("has-error");
            frmGrpFechaInicio.RemoveCssClass("has-error");
            frmGrpFechaFin.RemoveCssClass("has-error");
            frmGrpUsuario.RemoveCssClass("has-error");
            frmGrpPassword.RemoveCssClass("has-error");

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            frmGrpTelefono.RemoveCssClass("has-error");

            //Activa cajas
            txtNombre.Enabled = false;
            txtApellidoPaterno.Enabled = false;
            txtApellidoMaterno.Enabled = false;
            txtFechaNacimiento.Enabled = false;
            txtCorreo.Enabled = false;
            //txtCurp.Enabled = false;
            txtFechaNacimiento.Enabled = false;
            txtFechaInicio.Enabled = false;
            txtFechaFin.Enabled = false;
            txtUsuario.Enabled = false;
            txtPassword.Enabled = false;
            DdPerfil.Enabled = false;
            DdStatus.Enabled = false;
            //Muestra botones de guardar
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            FUImagen.Enabled = false;

            btnBuscarEmpresa.Enabled = false;
            btnBuscarEmpresa.CssClass = "btn btn-sm btn-default disabled";
            btnEditar.CssClass = "btn btn-sm disabled btn-default";
            txtRfc.Enabled = false;
            txtRazonSocial.Enabled = false;
            txtNombreComercial.Enabled = false;

            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.CssClass = "btn btn-sm disabled btn-default";
            btnEditarTelefono.Disable();
            btnEliminarTelefono.Disable();

            btnAgregarDireccion.Disable();
            btnEditarDireccion.Disable();
            btnEliminarDireccion.Disable();

            btnNuevo.Enabled = true;
            btnNuevo.RemoveCssClass("disabled");
            btnEditar.Enabled = true;
            btnEditar.RemoveCssClass("disabled");

            btnCancelarEliminarDireccion_Click(sender, e);
            btnCancelarEliminarTelefono_Click(sender, e);

            ActivarCamposModulo(false);

            if (txtUidUsuario.Text.Length == 0)
            {
                btnEditar.Enabled = false;
                btnEditar.AddCssClass("disabled");
                //Limpia cajas de texto
                txtNombre.Text = string.Empty;
                txtApellidoPaterno.Text = string.Empty;
                txtApellidoMaterno.Text = string.Empty;
                txtCorreo.Text = string.Empty;
                //txtCurp.Text = string.Empty;
                txtFechaNacimiento.Text = string.Empty;
                txtFechaInicio.Text = string.Empty;
                txtFechaFin.Text = string.Empty;
                txtUsuario.Text = string.Empty;
                txtPassword.Text = string.Empty;
                txtRazonSocial.Text = string.Empty;
                txtRfc.Text = string.Empty;
                txtNombreComercial.Text = string.Empty;

                int pos;
                if (ViewState["DireccionPreviousRow"] != null)
                {
                    pos = (int)ViewState["DireccionPreviousRow"];
                    if (pos < dgvDirecciones.Rows.Count)
                    {
                        GridViewRow previousRow = dgvDirecciones.Rows[pos];
                        previousRow.RemoveCssClass("success");
                    }
                }

                if (ViewState["TelefonoPreviousRow"] != null)
                {
                    pos = (int)ViewState["TelefonoPreviousRow"];
                    GridViewRow previousRow = dgvTelefonos.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }

                if (ViewState["EmpresaPreviousRow"] != null)
                {
                    pos = (int)ViewState["EmpresaPreviousRow"];
                    GridViewRow previousRow = DVGUSUsuarios.Rows[pos];
                    previousRow.RemoveCssClass("success");
                }

                ViewState["Telefonos"] = null;
                ViewState["Direcciones"] = null;
                dgvDirecciones.DataSource = null;
                dgvDirecciones.DataBind();
                dgvTelefonos.DataSource = null;
                dgvTelefonos.DataBind();
                if (Session["RutaImagen"] != null)
                {
                    string Ruta = Session["RutaImagen"].ToString();

                    //Borra la imagen de la empresa
                    if (File.Exists(Server.MapPath(Ruta)))
                    {
                        File.Delete(Server.MapPath(Ruta));
                    }
                    //Recarga el controlador de la imagen con una imagen default
                    ImgUsuario.ImageUrl = "Img/Default.jpg";
                    ImgUsuario.DataBind();
                }

            }
            else
            {
                VM.Obtenerusuario(new Guid(txtUidUsuario.Text));
                txtUidUsuario.Text = VM.CLASSUSUARIO.UIDUSUARIO.ToString();
                txtNombre.Text = VM.CLASSUSUARIO.STRNOMBRE;
                txtApellidoPaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOPATERNO;
                txtApellidoMaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOMATERNO;
                txtFechaNacimiento.Text = VM.CLASSUSUARIO.DtFechaNacimiento.ToString("dd/MM/yyyy");
                txtCorreo.Text = VM.CLASSUSUARIO.STRCORREO;
                txtFechaInicio.Text = VM.CLASSUSUARIO.DtFechaInicio.ToString("dd/MM/yyyy");
                txtFechaFin.Text = VM.CLASSUSUARIO.DtFechaFin?.ToString("dd/MM/yyyy");
                txtUsuario.Text = VM.CLASSUSUARIO.STRUSUARIO;
                txtPassword.Text = VM.CLASSUSUARIO.STRPASSWORD;
                ImgUsuario.ImageUrl = Page.ResolveUrl(VM.CLASSUSUARIO.RutaImagen);

                VM.ObtenerUsuarioPerfilEmpresa(new Guid(txtUidUsuario.Text));
                DdPerfil.SelectedValue = VM.UsuarioPerfilEmpresa.UidPerfil.ToString();
                DdStatus.SelectedValue = VM.CLASSUSUARIO.UidStatus.ToString();

                txtUidEmpresa.Text = VM.UsuarioPerfilEmpresa.UidEmpresa.ToString();
                VM.ObtenerEmpresaUsuario(txtUidEmpresa.Text);
                txtRfc.Text = VM.CEmpresa.StrRFC;
                txtRazonSocial.Text = VM.CEmpresa.StrRazonSocial;
                txtNombreComercial.Text = VM.CEmpresa.StrNombreComercial;

                VM.ObtenerTelefonos();
                ViewState["Telefonos"] = VM.Telefonos;
                dgvTelefonos.DataSource = VM.Telefonos;
                dgvTelefonos.DataBind();
                btnEditar.Enabled = true;
                btnEditar.CssClass = "btn btn-sm btn-default";

                VM.ObtenerDirecciones();
                ViewState["Direcciones"] = VM.Direcciones;
                dgvDirecciones.DataSource = VM.Direcciones;
                dgvDirecciones.DataBind();
            }

            btnBuscarEmpresa.Text = "Buscar Empresa";
            userGrid.Visible = false;
            btnBuscarEmpresa.Disable();

            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            try { 
            //se hace esto primero para evitar que le den mas 1 vez, en caso de que no reaccione
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;

            EditingMode = false;
            bool error = false;
            string Correo = txtCorreo.Text;

            string FechaFin = txtFechaFin.Text;
            string UidPerfil = DdPerfil.SelectedValue;
            string UidStatus = DdStatus.SelectedValue;
                lblErrorUsuario.Visible = false;
                lblErrorUsuario.Text = string.Empty;
            lblErrorDireccion.Text = string.Empty;
            lblErrorTelefono.Text = string.Empty;

            frmGrpNombre.RemoveCssClass("has-error");
            frmGrpApellidoPaterno.RemoveCssClass("has-error");
            frmGrpApellidoMaterno.RemoveCssClass("has-error");
            frmGrpFechaNacimiento.RemoveCssClass("has-error");
            frmGrpFechaInicio.RemoveCssClass("has-error");
            frmGrpFechaFin.RemoveCssClass("has-error");
            frmGrpUsuario.RemoveCssClass("has-error");
            frmGrpPassword.RemoveCssClass("has-error");

            frmGrpMunicipio.RemoveCssClass("has-error");
            frmGrpCiudad.RemoveCssClass("has-error");
            frmGrpColonia.RemoveCssClass("has-error");
            frmGrpCalle.RemoveCssClass("has-error");
            frmGrpConCalle.RemoveCssClass("has-error");
            frmGrpYCalle.RemoveCssClass("has-error");
            frmGrpNoExt.RemoveCssClass("has-error");

            frmGrpTelefono.RemoveCssClass("has-error");

            if (txtNombre.Text.Trim() == string.Empty)
            {
                txtNombre.Focus();
                frmGrpNombre.AddCssClass("has-error");
                error = true;
            }
            string Nombre = txtNombre.Text;

            if (txtApellidoPaterno.Text.Trim() == string.Empty)
            {
                txtApellidoPaterno.Focus();
                frmGrpApellidoPaterno.AddCssClass("has-error");
                error = true;
            }
            string ApellidoPaterno = txtApellidoPaterno.Text;

            if (txtApellidoMaterno.Text.Trim() == string.Empty)
            {
                txtApellidoMaterno.Focus();
                frmGrpApellidoMaterno.AddCssClass("has-error");
                error = true;
            }
            string ApellidoMaterno = txtApellidoMaterno.Text;

            DateTime temp;
                if (txtFechaNacimiento.Text.Trim() == string.Empty)
                {
                    txtFechaNacimiento.Focus();
                    frmGrpFechaNacimiento.AddCssClass("has-error");
                    error = true;
                }
                else {
                    //if (!DateTime.TryParse(txtFechaNacimiento.Text, out temp))
                    //{
                    //    txtFechaNacimiento.Focus();
                    //    frmGrpFechaNacimiento.AddCssClass("has-error");
                    //    error = true;
                    //}
                    //else
                    //{
                        
                    //}
                }
            DateTime FechaNacimiento = DateTime.ParseExact(txtFechaNacimiento.Text, "dd/MM/yyyy", null);

                if (txtFechaInicio.Text.Trim() == string.Empty)
            {
                txtFechaInicio.Focus();
                frmGrpFechaInicio.AddCssClass("has-error");
                error = true;
                }
                else
                {
                    //if (!DateTime.TryParse(txtFechaInicio.Text, out temp))
                    //{
                    //    txtFechaNacimiento.Focus();
                    //    frmGrpFechaNacimiento.AddCssClass("has-error");
                    //    error = true;
                    //}
                    //else
                    //{

                    //}
                }
            DateTime FechaInicio = DateTime.ParseExact(txtFechaNacimiento.Text, "dd/MM/yyyy", null);

                if (txtUsuario.Text.Trim() == string.Empty)
            {
                txtUsuario.Focus();
                frmGrpUsuario.AddCssClass("has-error");
                error = true;
            }
            string Usuario = txtUsuario.Text;

            if (txtPassword.Text.Trim() == string.Empty)
            {
                txtPassword.Focus();
                frmGrpPassword.AddCssClass("has-error");
                error = true;
            }
            string Password = txtPassword.Text;

            if (txtUidEmpresa.Text.Trim() == string.Empty)
            {
                error = true;
                lblErrorUsuario.Text = "Debe seleccionar una empresa. ";
            }

            if (error)
            {
                lblErrorUsuario.Text += "Algunos campos en datos generales con obligatorios";
                return;
            }

            VM.ObtenerUsuarioPorNombre(txtUsuario.Text);

            if (VM.CLASSUSUARIO != null && VM.CLASSUSUARIO.UIDUSUARIO.ToString() != txtUidUsuario.Text)
            {
                txtUsuario.Focus();
                frmGrpUsuario.AddCssClass("has-error");
                lblErrorUsuario.Text = "El usuario ya existe";
                return;
            }

            if (txtUidUsuario.Text == string.Empty)
            {
                if (ViewState["rutaimg"] == null)
                    ViewState["rutaimg"] = "";

                if (VM.GuardarUsuario(Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento,
                Correo, FechaInicio, FechaFin, Usuario, Password, UidStatus, ViewState["rutaimg"].ToString()))
                {
                    lblMensaje.Text = "Guardado Correctamente";

                    //btnAceptar.Visible = false;
                    //btnCancelar.Visible = false;
                    //Deshabilitacion de cajas
                    txtNombre.Enabled = false;
                    txtApellidoPaterno.Enabled = false;
                    txtApellidoMaterno.Enabled = false;
                    txtFechaNacimiento.Enabled = false;
                    txtCorreo.Enabled = false;
                    //txtCurp.Enabled = false;
                    txtFechaInicio.Enabled = false;
                    txtFechaFin.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtPassword.Enabled = false;
                    DdPerfil.Enabled = false;
                    DdPerfil.CssClass = "form-control";
                    DVGUSUsuarios.Visible = true;
                    DdStatus.Enabled = false;
                    FUImagen.Enabled = false;

                    txtNombre.Text = string.Empty;
                    txtApellidoPaterno.Text = string.Empty;
                    txtApellidoMaterno.Text = string.Empty;
                    txtFechaNacimiento.Text = string.Empty;
                    txtFechaInicio.Text = string.Empty;
                    txtFechaFin.Text = string.Empty;
                    txtUsuario.Text = string.Empty;
                    txtPassword.Text = string.Empty;
                    txtCorreo.Text = string.Empty;

                }
                else
                {
                    lblMensaje.Text = "No Puedo Guardarse Correctamente";
                }

            }


            else
            {
                if (ViewState["rutaimg"] != null)
                {
                    string UidUsuario = DVGUSUsuarios.SelectedDataKey.Value.ToString();
                    VM.Obtenerusuario(new Guid(UidUsuario));
                    string Ruta = VM.CLASSUSUARIO.RutaImagen;

                    if (File.Exists(Server.MapPath(Ruta)))
                    {
                        File.Delete(Server.MapPath(Ruta));

                    }
                }
                else
                {
                    string UidUsuario = DVGUSUsuarios.SelectedDataKey.Value.ToString();
                    VM.Obtenerusuario(new Guid(UidUsuario));
                    ViewState["rutaimg"] = VM.CLASSUSUARIO.RutaImagen;
                }


                if (VM.ModificarUsuario(txtUidUsuario.Text, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento,
                    Correo, FechaInicio, FechaFin, Usuario, Password, UidStatus, ViewState["rutaimg"].ToString()))
                {
                    if (VM.ModificarUsuarioPerfilEmpresa(txtUidUsuario.Text, UidPerfil, txtUidEmpresa.Text))
                    {
                        lblMensaje.Text = "Se ha actualizado";

                    }
                    btnAceptar.Visible = false;
                    btnCancelar.Visible = false;
                    btnEditar.Enabled = false;
                    btnEditar.CssClass = "btn btn-sm disabled btn-default";
                    txtNombre.Enabled = false;
                    txtApellidoPaterno.Enabled = false;
                    txtApellidoMaterno.Enabled = false;
                    txtFechaNacimiento.Enabled = false;
                    txtCorreo.Enabled = false;
                    txtFechaInicio.Enabled = false;
                    txtFechaFin.Enabled = false;
                    txtUsuario.Enabled = false;
                    txtPassword.Enabled = false;
                    DdPerfil.Enabled = false;
                    DdPerfil.CssClass = "form-control";
                    DdStatus.Enabled = false;

                    DVGUSUsuarios.Visible = true;
                }
                else
                {
                    lblMensaje.Text = "No se ha actualizado";
                }
            }

            Guid uidUsuario = VM.CLASSUSUARIO.UIDUSUARIO;

            List<UsuarioTelefono> telefonos = (List<UsuarioTelefono>)ViewState["Telefonos"];
            VM.GuardarTelefonos(telefonos, uidUsuario);

            VM.EliminarTelefonos(TelefonoRemoved);

            List<UsuarioDireccion> direcciones = (List<UsuarioDireccion>)ViewState["Direcciones"];
            VM.GuardarDirecciones(direcciones, uidUsuario);
            VM.EliminarDirecciones(DireccionRemoved);
            if (DdPerfil.SelectedValue == "Superadministrador")
            {
                VM.GuardarUsuarioPerfilEmpresa(UidPerfil, uidUsuario, Guid.Empty.ToString());
            }
            else
            {
                if (txtUidUsuario.Text == string.Empty)
                {
                    VM.GuardarUsuarioPerfilEmpresa(UidPerfil, uidUsuario, txtUidEmpresa.Text);
                }

            }

            GuardarModulos(uidUsuario);

            VM.CargarListadeUsuarios2();
            DVGUSUsuarios.DataSource = VM.LISTADEUSUARIOS;
            DVGUSUsuarios.DataBind();

            PanelFiltros.Visible = false;
            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.CssClass = "btn btn-sm disabled btn-default";
            btnAgregarDireccion.Enabled = false;
            btnAgregarDireccion.CssClass = "btn btn-sm disabled btn-default";

            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataBind();

            dgvTelefonos.DataSource = null;
            dgvTelefonos.DataBind();

            btnNuevo.Enable();

            ActivarCamposModulo(false);

            txtRazonSocial.Enabled = false;
            txtRfc.Enabled = false;
            txtNombreComercial.Enabled = false;
            txtRazonSocial.Text = string.Empty;
            txtRfc.Text = string.Empty;
            txtNombreComercial.Text = string.Empty;

            ViewState["EmpresaPreviousRow"] = null;

            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;

            btnBuscarEmpresa.Text = "Buscar Empresa";
            userGrid.Visible = false;
            PanelEmpresa.Visible = true;
            btnBuscarEmpresa.Disable();

            }
            catch (Exception d) {

                lblErrorUsuario.Visible = true;
                lblErrorUsuario.Text = d.Message;
                btnNuevo.Enable();
            }
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            EditingMode = true;
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            btnNuevo.Disable();
            btnEditar.Disable();
            txtUidUsuario.Enabled = true;
            txtNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtFechaNacimiento.Enabled = true;
            FUImagen.Enabled = true;
            txtCorreo.Enabled = true;
            //txtCurp.Enabled = true;
            txtFechaInicio.Enabled = true;
            txtFechaFin.Enabled = true;
            txtUsuario.Enabled = true;
            txtPassword.Enabled = true;
            lblMensaje.Text = string.Empty;
            DdPerfil.Enabled = true;
            DdStatus.Enabled = true;

            txtRazonSocial.Enabled = true;
            txtRfc.Enabled = true;
            txtNombreComercial.Enabled = true;
            btnBuscarEmpresa.Enable();

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

            btnAgregarDireccion.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarDireccion.Enabled = true;
            btnAgregarTelefono.RemoveCssClass("disabled").RemoveCssClass("hidden");
            btnAgregarTelefono.Enabled = true;

            ActivarCamposModulo(true);
        }

        protected void FiltroFechaInicio_TextChanged(object sender, EventArgs e)
        {
            FiltroFechaInicio2.Text = FiltroFechaInicio.Text;

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            PanelFiltros.Visible = false;
            lblFiltros.Text = "Mostrar";
            lblFiltros.CssClass = "glyphicon glyphicon-collapse-down";
            btnLimpiar.Enabled = false;
            btnLimpiar.CssClass = "btn btn-sm btn-default disabled";

            string Nombre = FiltroNombre.Text.Trim();
            string ApellidoPaterno = FiltroApellidoPaterno.Text.Trim();
            string ApellidoMaterno = FiltroApellidoMaterno.Text.Trim();
            string FechaNacimiento = FiltroFechaNacimiento.Text.Trim();
            string FechaNacimiento2 = FiltroFechaNacimiento2.Text.Trim();
            string Correo = FiltroCorreo.Text.Trim();
            string FechaInicio = FiltroFechaInicio.Text.Trim();
            string FechaInicio2 = FiltroFechaInicio2.Text.Trim();
            string FechaFin = FiltroFechaFin.Text.Trim();
            string FechaFin2 = FiltroFechaFin2.Text.Trim();
            string Usuario = FiltroUsuario.Text.Trim();



            string perfiles = "";
            int[] i = lblPerfil.GetSelectedIndices();
            foreach (int j in i)
            {
                string value = lblPerfil.Items[j].Value;
                if (perfiles.Count() == 0)
                    perfiles += value;
                else
                    perfiles += "," + value;
            }
            string Perfil = perfiles;

            string Estatus = "";
            int[] es = lblStatus.GetSelectedIndices();
            foreach (int j in es)
            {
                string value = lblStatus.Items[j].Value;
                if (Estatus.Count() == 0)
                    Estatus += value;
                else
                    Estatus += "," + value;
            }
            string Status = Estatus;

            string Empresas = "";
            int[] l = lbEmpresas.GetSelectedIndices();
            foreach (int j in l)
            {
                string value = lbEmpresas.Items[j].Value;
                if (Empresas.Length == 0)
                    Empresas += value;
                else
                    Empresas += "," + value;
            }
            string empresas = Empresas;


            VM.BuscarUsuarios(Nombre, ApellidoPaterno, ApellidoMaterno,
                FechaNacimiento, FechaNacimiento2, Correo, FechaInicio, FechaInicio2,
                FechaFin, FechaFin2, Perfil, Usuario, Status, empresas);

            DVGUSUsuarios.Visible = true;
            DVGUSUsuarios.DataSource = VM.LISTADEUSUARIOS;
            DVGUSUsuarios.DataBind();
            ViewState["Usuario"] = VM.LISTADEUSUARIOS;

            btnBuscar.Enabled = false;
            btnBuscar.AddCssClass("disabled");

            ViewState["EmpresaPreviousRow"] = null;
        }

        protected void tabDatos_Click(object sender, EventArgs e)
        {
            PanelDatosGeneralesUsuario.Visible = true;
            panelDirecciones.Visible = false;
            panelTelefonos.Visible = false;
            panelAccesos.Visible = false;
            PanelEmpresa.Visible = false;

            activeDatosGenerales.Attributes["class"] = "active";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "";

            activeAccesos.RemoveCssClass("active");

            activeEmpresas.RemoveCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                PanelBusquedas.Visible = true;
            }
        }

        protected void tabDirecciones_Click(object sender, EventArgs e)
        {

            panelDirecciones.Visible = true;
            PanelDatosGeneralesUsuario.Visible = false;
            PanelEmpresa.Visible = false;
            panelTelefonos.Visible = false;
            panelAccesos.Visible = false;

            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "active";

            activeTelefonos.Attributes["class"] = "";

            activeAccesos.RemoveCssClass("active");
            activeEmpresas.RemoveCssClass("active");
        }

        protected void tabTelefonos_Click(object sender, EventArgs e)
        {
            panelTelefonos.Visible = true;
            PanelDatosGeneralesUsuario.Visible = false;
            PanelEmpresa.Visible = false;
            panelDirecciones.Visible = false;
            panelAccesos.Visible = false;

            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "active";

            activeAccesos.RemoveCssClass("active");
            activeEmpresas.RemoveCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                PanelBusquedas.Visible = true;
            }
        }

        protected void tabAccesos_Click(object sender, EventArgs e)
        {
            panelTelefonos.Visible = false;
            PanelDatosGeneralesUsuario.Visible = false;
            PanelEmpresa.Visible = false;
            panelDirecciones.Visible = false;
            panelAccesos.Visible = true;

            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "";

            activeEmpresas.RemoveCssClass("active");

            activeAccesos.AddCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                PanelBusquedas.Visible = true;
            }
        }

        protected void btnBuscarEmpresa_Click(object sender, EventArgs e)
        {
            if (btnBuscarEmpresa.Text.Contains("Buscar Empresa"))
            {
                string NombreComercial = txtNombreComercial.Text;
                string Rfc = txtRfc.Text;
                string RazonSocial = txtRazonSocial.Text;

                VM.BuscarEmpresa(NombreComercial, Rfc, RazonSocial);

                if (VM.Empresas.Count == 0)
                {
                    // FIXME: add message
                    return;
                }

                dgvEmpresa.DataSource = VM.Empresas;
                dgvEmpresa.DataBind();

                PanelEmpresa.Visible = false;
                userGrid.Visible = true;
            }
            else
            {
                txtNombre.Text = string.Empty;
                txtNombre.Enable();
                btnBuscarEmpresa.Text = "Buscar Empresa";
            }
        }

        #endregion

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
                        string ruta = "~/Vista/Imagenes/Encargados/" + txtUidUsuario.Text + '_' + numero + Nombrearchivo;


                        //guardar img
                        FUImagen.SaveAs(Server.MapPath(ruta));

                        string rutaimg = ruta + "?" + (numero - 1);

                        ViewState["rutaimg"] = ruta;

                        ImgUsuario.ImageUrl = rutaimg;

                    }
                }
            }
        }


        #endregion

        protected void tabBackside_Click(object sender, EventArgs e)
        {
            accesoBackside.Visible = true;
            accesoBackend.Visible = false;
            accesoFrontend.Visible = false;

            activeBackside.AddCssClass("active");
            activeBackend.RemoveCssClass("active");
            activeFrontend.RemoveCssClass("active");

            tabBackside.Disable();
            tabBackend.Enable();
            tabFrontend.Enable();
        }
        protected void tabBackend_Click(object sender, EventArgs e)
        {
            accesoBackside.Visible = false;
            accesoBackend.Visible = true;
            accesoFrontend.Visible = false;

            activeBackside.RemoveCssClass("active");
            activeBackend.AddCssClass("active");
            activeFrontend.RemoveCssClass("active");

            tabBackside.Enable();
            tabBackend.Disable();
            tabFrontend.Enable();
        }
        protected void tabFrontend_Click(object sender, EventArgs e)
        {
            accesoBackside.Visible = false;
            accesoBackend.Visible = false;
            accesoFrontend.Visible = true;

            activeBackside.RemoveCssClass("active");
            activeBackend.RemoveCssClass("active");
            activeFrontend.AddCssClass("active");

            tabBackside.Enable();
            tabBackend.Enable();
            tabFrontend.Disable();
        }
        protected void dgvEmpresa_SelectedIndexChanged(object sender, EventArgs e)
        {

            VM.ObtenerEmpresas(new Guid(dgvEmpresa.SelectedDataKey.Value.ToString()));
            txtUidEmpresa.Text = VM.CEmpresa.UidEmpresa.ToString();
            txtNombreComercial.Text = VM.CEmpresa.StrNombreComercial;
            txtRfc.Text = VM.CEmpresa.StrRFC;
            txtRazonSocial.Text = VM.CEmpresa.StrRazonSocial;
            txtNombreComercial.Disable();
            txtRazonSocial.Disable();
            txtRfc.Disable();

            PanelEmpresa.Visible = true;
            userGrid.Visible = false;
            btnBuscarEmpresa.Text = "Seleccionar otro";
            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;
        }
        protected void dgvEmpresa_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvEmpresa, "Select$" + e.Row.RowIndex);
            }

        }
        protected void tabEmpresas_Click(object sender, EventArgs e)
        {
            panelTelefonos.Visible = false;
            PanelDatosGeneralesUsuario.Visible = false;
            PanelEmpresa.Visible = true;
            panelDirecciones.Visible = false;
            panelAccesos.Visible = false;
            PanelEmpresa.Visible = true;

            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "";

            activeAccesos.Attributes["class"] = "";

            activeEmpresas.AddCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                PanelBusquedas.Visible = true;
            }
        }
        protected void DdPerfil_SelectedIndexChanged(object sender, EventArgs e)
        {
            GenerarCampos(true);

        }
    }
}