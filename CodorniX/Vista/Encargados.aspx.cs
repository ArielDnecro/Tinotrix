using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodorniX.VistaDelModelo;
using CodorniX.Modelo;
using CodorniX.Util;
using System.IO;
using System.Web.UI.HtmlControls;

namespace CodorniX.Vista
{
    public partial class Encargados : System.Web.UI.Page
    {
        #region propiedades


        VMEncargados VM = new VMEncargados();
        VMLogin _CVMLogin = new VMLogin();
        VMEmpresas VME = new VMEmpresas();

        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }

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
            if (!string.IsNullOrWhiteSpace(txtUidEncargado.Text))
                VM.ObtenerModulosUsuario(new Guid(txtUidEncargado.Text));
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

            acceso = from e in VM.NivelAccesos where e.StrNivelAcceso == "Frontend" select e;
            VM.ObtenerModulosPerfil(perfil, acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.ModulosPerfil, ModulosFrontend, VM.ModulosUsuario, modulosFrontend);

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

            if (!Acceso.TieneAccesoAModulo("Encargados", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }

            if (!IsPostBack)
            {
                txtNombreSucursal.Disable();
                txtTipoSucursal.Disable();

                #region Panel Izquierdo
                btnBuscarSucursal.Enabled = false;
                txtUidSucursal.Text = string.Empty;
                panelDireccion.Visible = false;
                panelDirecciones.Visible = false;
                panelTelefonos.Visible = false;
                //PanelFiltros.Visible = false;
                lblFiltros.Text = "Ocultar";
                DVGEncargados.Visible = false;
                DVGEncargados.DataSource = null;
                DVGEncargados.DataBind();

                VM.CargarPerfiles(SesionActual.uidEmpresaActual.Value);
                lblPerfil.DataSource = VM.LISTADEPERFIL;
                lblPerfil.DataTextField = "strPerfil";
                lblPerfil.DataValueField = "UidPerfil";
                lblPerfil.DataBind();

                VM.CargarListaDeStatus();
                lblStatus.DataSource = VM.LISTADESTATUS;
                lblStatus.DataTextField = "strStatus";
                lblStatus.DataValueField = "UidStatus";
                lblStatus.DataBind();

                VM.ObtenerSucursales(SesionActual.uidEmpresaActual.Value);
                ListSucursal.DataSource = VM.Sucursales;
                ListSucursal.DataTextField = "StrNombre";
                ListSucursal.DataValueField = "UidSucursal";
                ListSucursal.DataBind();


                #endregion

                #region panel derecho
                //botones
                //btnEditar.Enabled = true;
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
                //btnEmpresa.Enabled = true;
                if (SesionActual.uidEmpresaActual != null)
                {
                    txtEmpresa.Text = SesionActual.uidEmpresaActual.ToString();
                }
                ImgEncargado.ImageUrl = "Imgenes/Default.jpg";
                ImgEncargado.DataBind();
                #endregion

                GenerarCampos();
                ActivarCamposModulo(false);
            }
            else
            {
                GenerarCampos();
            }
        }

        #region Panel busquedas

        protected void btnVisibilidadPanelFiltros(object sender, EventArgs e)
        {
            string texto = lblFiltros.Text;
            DVGEncargados.Visible = false;
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
                DVGEncargados.Visible = true;


            }
        }

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
            ListSucursal.ClearSelection();
            DVGEncargados.DataSource = VM.LISTADEUSUARIOS;
            DVGEncargados.DataBind();
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


            string Sucursales = "";
            int[] su = ListSucursal.GetSelectedIndices();
            foreach (int j in su)
            {
                string value = ListSucursal.Items[j].Value;
                if (Sucursales.Count() == 0)
                    Sucursales += value;
                else
                    Sucursales += "," + value;
            }
            string Sucursal = Sucursales;
           

            VM.BuscarUsuarios(Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento,
                FechaNacimiento2, Correo, FechaInicio, FechaInicio2, FechaFin, FechaFin2,
                Usuario, Perfil, Status, SesionActual.uidEmpresaActual.Value,Sucursal);

            DVGEncargados.Visible = true;
            DVGEncargados.DataSource = VM.LISTADEUSUARIOS;
            DVGEncargados.DataBind();
            ViewState["Encargado"] = VM.LISTADEUSUARIOS;

            btnBuscar.Enabled = false;
            btnBuscar.AddCssClass("disabled");

            ViewState["EmpresaPreviousRow"] = null;
        }

        #region Grid Encargados

        private void SortEncargado(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            List<Usuario> usuario = (List<Usuario>)ViewState["Encargado"];

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
            else if (SortExpression == "Encargado")
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
            DVGEncargados.DataSource = usuario;
            ViewState["SortColumn"] = SortExpression;
            ViewState["SortColumnDirection"] = SortDirection;
        }

        protected void DVGEncargados_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ViewState["EmpresaPreviousRow"] = null;
            if (ViewState["SortColumn"] != null && ViewState["SortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["SortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["SortColumnDirection"];
                SortEncargado(SortExpression, SortDirection, true);
            }
            else
            {
                DVGEncargados.DataSource = ViewState["Encargado"];
            }
            DVGEncargados.PageIndex = e.NewPageIndex;
            DVGEncargados.DataBind();
        }

        protected void DVGEncargados_SelectedIndexChanged(object sender, EventArgs e)
        {
            FUImagen.Enabled = false;
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
            string UidUsuario = DVGEncargados.SelectedDataKey.Value.ToString();
            VM.Obtenerusuario(new Guid(UidUsuario));
            txtUidEncargado.Text = VM.CLASSUSUARIO.UIDUSUARIO.ToString();
            txtNombre.Text = VM.CLASSUSUARIO.STRNOMBRE;
            txtApellidoPaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOPATERNO;
            txtApellidoMaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOMATERNO;
            txtFechaNacimiento.Text = VM.CLASSUSUARIO.DtFechaNacimiento.ToString("dd/MM/yyyy");
            txtCorreo.Text = VM.CLASSUSUARIO.STRCORREO;
            txtFechaInicio.Text = VM.CLASSUSUARIO.DtFechaInicio.ToString("dd/MM/yyyy");
            txtFechaFin.Text = VM.CLASSUSUARIO.DtFechaFin?.ToString("dd/MM/yyyy");
            txtUsuario.Text = VM.CLASSUSUARIO.STRUSUARIO;
            txtPassword.Text = VM.CLASSUSUARIO.STRPASSWORD;
            //txtimagen.Text = VM.CLASSUSUARIO.RutaImagen;
            ImgEncargado.ImageUrl=Page.ResolveUrl(VM.CLASSUSUARIO.RutaImagen);
            DdStatus.SelectedIndex = DdStatus.Items.IndexOf(DdStatus.Items.FindByValue(VM.CLASSUSUARIO.UidStatus.ToString()));
            
            VM.ObtenerUsuarioPerfilSucursal(new Guid(UidUsuario));
            DdPerfil.SelectedIndex = DdPerfil.Items.IndexOf(DdPerfil.Items.FindByValue(VM.UsuarioPerfilSucursal.UidPerfil.ToString()));

            txtUidSucursal.Text = VM.UsuarioPerfilSucursal.UidSucursal.ToString();
            VM.ObtenerSucursalUsuario(txtUidSucursal.Text);
            txtNombreSucursal.Text = VM.CSucursal.StrNombre;
            txtTipoSucursal.Text = VM.CSucursal.StrTipoSucursal;


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

            btnAgregarDireccion.Enabled = false;
            btnAgregarDireccion.AddCssClass("disabled");
            btnEditarDireccion.Enabled = false;
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.Enabled = false;
            btnEliminarDireccion.AddCssClass("disabled");

            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.AddCssClass("disabled");
            btnEditarTelefono.Enabled = false;
            btnEditarTelefono.AddCssClass("disabled");
            btnEliminarTelefono.Enabled = false;
            btnEliminarTelefono.AddCssClass("disabled");

            if (EditingMode)
            {
                btnCancelar_Click(null, null);
            }

            int pos = -1;
            if (ViewState["EmpresaPreviousRow"] != null)
            {
                pos = (int)ViewState["EmpresaPreviousRow"];
                GridViewRow previousRow = DVGEncargados.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["EmpresaPreviousRow"] = DVGEncargados.SelectedIndex;
            DVGEncargados.SelectedRow.AddCssClass("success");
            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;

            GenerarCampos(true);
            ActivarCamposModulo(false);
        }

        protected void DVGEncargados_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["EmpresaPreviousRow"] = null;
            SortEncargado(e.SortExpression, e.SortDirection);

            DVGEncargados.DataBind();
        }

        protected void DVGEncargados_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = Page.ClientScript.GetPostBackClientHyperlink(DVGEncargados, "Select$" + e.Row.RowIndex);

                Label ESTATUS = e.Row.FindControl("lblEstatus") as Label;
                Label PERFIL = e.Row.FindControl("lblPerfil") as Label;
                if (e.Row.Cells[6].Text == "Encargado")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-cog";
                    PERFIL.ToolTip = "Encargado";
                }
                if (e.Row.Cells[6].Text == "Supervisor")
                {
                    PERFIL.CssClass = "glyphicon glyphicon-eye-open";
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

        #endregion

        #endregion

        #region Panel Direccion

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


            btnAgregarDireccion.RemoveCssClass("disabled");
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.AddCssClass("disabled");

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

            PanelBusquedas.Visible = true;
            panelDireccion.Visible = false;
        }

        protected void btnCerrarDireccion_Click(object sender, EventArgs e)
        {
            panelDireccion.Visible = false;
            PanelBusquedas.Visible = true;
            btnCerrarDireccion.Visible = false;
            btnOkDireccion.Visible = true;
            btnCancelarDireccion.Visible = true;
        }

        #endregion

        #region Panel Derecho
        protected void btnNuevo_ActivarCampos(object sender, EventArgs e)
        {
            //Cajas de Texto
            ImgEncargado.ImageUrl = string.Empty;
            txtNombreSucursal.Text = string.Empty;
            txtTipoSucursal.Text = string.Empty;
            txtUidSucursal.Text = string.Empty;
            txtNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtFechaNacimiento.Enabled = true;
            txtCorreo.Enabled = true;
            btnBuscarSucursal.Enabled = true;
            btnBuscarSucursal.CssClass = "btn btn-sm btn-default";
            // txtCurp.Enabled = true;
            txtFechaInicio.Enabled = true;
            txtFechaFin.Enabled = true;
            txtUsuario.Enabled = true;
            txtPassword.Enabled = true;
            btnEditar.Disable();
            btnNuevo.Disable();
            btnEmpresa.Enabled = true;
            FUImagen.Enabled = true;
            btnEditar.CssClass = "btn btn-sm disabled btn-default";
            DdPerfil.Enabled = true;
            DdPerfil.CssClass = "form-control";

            DdStatus.Enabled = true;
            if (txtFechaInicio.Text != string.Empty)
            {
                string fechainiciotexto = txtFechaInicio.Text;
                txtFechaFin.Text = fechainiciotexto;
            }


            //botones
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            txtUidEncargado.Text = string.Empty;
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


            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;

            ActivarCamposModulo(true);
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            EditingMode = true;
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            btnNuevo.Disable();
            btnEditar.Disable();
            txtUidEncargado.Enabled = true;
            txtNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            FUImagen.Enabled = true;
            txtFechaNacimiento.Enabled = true;
            txtCorreo.Enabled = true;
            //txtCurp.Enabled = true;
            txtFechaInicio.Enabled = true;
            txtFechaFin.Enabled = true;
            txtUsuario.Enabled = true;
            txtPassword.Enabled = true;
            lblMensaje.Text = string.Empty;
            DdPerfil.Enabled = true;
            DdStatus.Enabled = true;
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

            btnBuscarSucursal.Enabled = true;
            btnBuscarSucursal.CssClass = "btn btn-sm btn-default";
        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            EditingMode = false;
            bool error = false;

            string Correo = txtCorreo.Text;

            string FechaFin = txtFechaFin.Text;
            string UidPerfil = DdPerfil.SelectedValue;
            string UidStatus = DdStatus.SelectedValue;

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

            activeSucursales.RemoveCssClass("has-error");

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

            if (txtFechaNacimiento.Text.Trim() == string.Empty)
            {
                txtFechaNacimiento.Focus();
                frmGrpFechaNacimiento.AddCssClass("has-error");
                error = true;
            }
            string FechaNacimiento = txtFechaNacimiento.Text;

            if (txtFechaInicio.Text.Trim() == string.Empty)
            {
                txtFechaInicio.Focus();
                frmGrpFechaInicio.AddCssClass("has-error");
                error = true;
            }
            string FechaInicio = txtFechaInicio.Text;
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
            if (error)
            {
                lblErrorUsuario.Text = "Algunos campos en datos generales son obligatorios.";

                return;
            }
            if (txtUidSucursal.Text.Trim() == string.Empty)
            {
                lblErrorUsuario.Text = "Debe seleccionar una sucursal. ";
                return;
            }

            

            VM.ObtenerUsuarioPorNombre(txtUsuario.Text);

            if (VM.CLASSUSUARIO != null && VM.CLASSUSUARIO.UIDUSUARIO.ToString() != txtUidEncargado.Text)
            {
                txtUsuario.Focus();
                frmGrpUsuario.AddCssClass("has-error");
                lblErrorUsuario.Text = "El usuario ya existe en el sistema";
                return;
            }

            if (txtUidEncargado.Text == string.Empty)
            {

                if (ViewState["rutaimg"] == null)
                    ViewState["rutaimg"] = "";
                if (VM.GuardarUsuario(Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento,
                Correo, FechaInicio, FechaFin, Usuario, Password, UidStatus, ViewState["rutaimg"].ToString()))
                {

                    lblMensaje.Text = "Guardado Correctamente";

                    btnAceptar.Visible = false;
                    btnCancelar.Visible = false;
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
                    FUImagen.Enabled = false;
                    txtPassword.Enabled = false;
                    DdPerfil.Enabled = false;
                    DdPerfil.CssClass = "form-control";
                    DVGEncargados.Visible = true;
                    DdStatus.Enabled = false;
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

                //string UidStatus = DdStatus.SelectedValue;
                if (ViewState["rutaimg"]!=null)
                {
                    string UidUsuario = DVGEncargados.SelectedDataKey.Value.ToString();
                    VM.Obtenerusuario(new Guid(UidUsuario));
                    string Ruta = VM.CLASSUSUARIO.RutaImagen;
                    
                    if (File.Exists(Server.MapPath(Ruta)))
                    {
                        File.Delete(Server.MapPath(Ruta));
                        
                    }
                }
                else
                {
                    string UidUsuario = DVGEncargados.SelectedDataKey.Value.ToString();
                    VM.Obtenerusuario(new Guid(UidUsuario));
                    ViewState["rutaimg"] = VM.CLASSUSUARIO.RutaImagen;
                }

                if (VM.ModificarUsuario(txtUidEncargado.Text, Nombre, ApellidoPaterno, ApellidoMaterno, FechaNacimiento,
                    Correo, FechaInicio, FechaFin, Usuario, Password, UidStatus,ViewState["rutaimg"].ToString()))
                {
                   

                    if (VM.ModificarUsuarioPerfilSucursal(txtUidEncargado.Text, UidPerfil, txtUidSucursal.Text))
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
                    DVGEncargados.Visible = true;

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

            GuardarModulos(uidUsuario);

            VM.EliminarDirecciones(DireccionRemoved);
            if (txtUidEncargado.Text == string.Empty)
            {
                VM.GuardarUsuarioPerfilSucursal(UidPerfil, txtUidSucursal.Text, uidUsuario);
            }
            VM.BuscarUsuarios(null, null, null, null, null, null, null, null, null, null, null, null, null, SesionActual.uidEmpresaActual.Value,null);
            
            DVGEncargados.DataSource = VM.LISTADEUSUARIOS;
            DVGEncargados.DataBind();

            PanelFiltros.Visible = false;
            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.CssClass = "btn btn-sm disabled btn-default";
            btnAgregarDireccion.Enabled = false;
            btnAgregarDireccion.CssClass = "btn btn-sm disabled btn-default";


            dgvDirecciones.DataSource = null;
            dgvDirecciones.DataBind();

            dgvTelefonos.DataSource = null;
            dgvTelefonos.DataBind();

            btnEditar.Disable();
            btnNuevo.Enable();

            ActivarCamposModulo(false);

            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;

            btnBuscarSucursal.Enabled = false;
            btnBuscarSucursal.CssClass = "btn btn-sm disabled btn-default";
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            EditingMode = false;

            lblErrorUsuario.Text = string.Empty;
            lblErrorDireccion.Text = string.Empty;
            lblErrorTelefono.Text = string.Empty;

            btnBuscarSucursal.Enabled = false;
            btnBuscarSucursal.CssClass = "btn btn-sm btn-default disabled";

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
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            FUImagen.Enabled = false;



            btnAgregarTelefono.Enabled = false;
            btnAgregarTelefono.CssClass = "btn btn-sm disabled btn-default";
            btnEditarTelefono.Enabled = false;
            btnEditarTelefono.AddCssClass("disabled");
            btnEliminarTelefono.Enabled = false;
            btnEliminarTelefono.AddCssClass("disabled");
            btnEmpresa.Enabled = false;
            btnEmpresa.CssClass = "btn btn-sm disabled btn-default";

            btnAgregarDireccion.Enabled = false;
            btnAgregarDireccion.CssClass = "btn btn-sm disabled btn-default";
            btnEditarDireccion.Enabled = false;
            btnEditarDireccion.AddCssClass("disabled");
            btnEliminarDireccion.Enabled = false;
            btnEliminarDireccion.AddCssClass("disabled");

            btnNuevo.Enable();

            btnCancelarEliminarDireccion_Click(sender, e);
            btnCancelarEliminarTelefono_Click(sender, e);

            if (txtUidEncargado.Text.Length == 0)
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
                txtNombreSucursal.Text = string.Empty;
                txtTipoSucursal.Text = string.Empty;
                txtNombreSucursal.Enabled = false;
                txtTipoSucursal.Enabled = false;
                txtUidSucursal.Text = string.Empty;
                userGrid.Visible = false;
                PanelSucursales.Visible = true;
                int pos;
                if (ViewState["DireccionPreviousRow"] != null)
                {
                    pos = (int)ViewState["DireccionPreviousRow"];
                    GridViewRow previousRow = dgvDirecciones.Rows[pos];
                    previousRow.RemoveCssClass("success");
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
                    GridViewRow previousRow = DVGEncargados.Rows[pos];
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
                    ImgEncargado.ImageUrl = "Img/Default.jpg";
                    ImgEncargado.DataBind();
                }
            }
            else
            {
                VM.Obtenerusuario(new Guid(txtUidEncargado.Text));
                VM.ObtenerUsuarioPerfilSucursal(new Guid(txtUidEncargado.Text));
                txtUidEncargado.Text = VM.CLASSUSUARIO.UIDUSUARIO.ToString();
                txtNombre.Text = VM.CLASSUSUARIO.STRNOMBRE;
                txtApellidoPaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOPATERNO;
                txtApellidoMaterno.Text = VM.CLASSUSUARIO.STRAPELLIDOMATERNO;
                txtFechaNacimiento.Text = VM.CLASSUSUARIO.DtFechaNacimiento.ToString("dd/MM/yyyy");
                txtCorreo.Text = VM.CLASSUSUARIO.STRCORREO;
                txtFechaInicio.Text = VM.CLASSUSUARIO.DtFechaInicio.ToString("dd/MM/yyyy");
                txtFechaFin.Text = VM.CLASSUSUARIO.DtFechaFin?.ToString("dd/MM/yyyy");
                txtUsuario.Text = VM.CLASSUSUARIO.STRUSUARIO;
                txtPassword.Text = VM.CLASSUSUARIO.STRPASSWORD;
                ImgEncargado.ImageUrl = Page.ResolveUrl(VM.CLASSUSUARIO.RutaImagen);
                DdPerfil.SelectedIndex = DdPerfil.Items.IndexOf(DdPerfil.Items.FindByValue(VM.UsuarioPerfilSucursal.UidPerfil.ToString()));
                DdStatus.SelectedIndex = DdStatus.Items.IndexOf(DdStatus.Items.FindByValue(VM.CLASSUSUARIO.UidStatus.ToString()));

                userGrid.Visible = false;
                PanelSucursales.Visible = true;
                txtNombreSucursal.Enabled = false;
                txtTipoSucursal.Enabled = false;

                txtUidSucursal.Text = VM.UsuarioPerfilSucursal.UidSucursal.ToString();
                VM.ObtenerSucursalUsuario(txtUidSucursal.Text);
                txtNombreSucursal.Text = VM.CSucursal.StrNombre;
                txtTipoSucursal.Text = VM.CSucursal.StrTipoSucursal;

                VM.ObtenerTelefonos();
                ViewState["Telefonos"] = VM.Telefonos;
                dgvTelefonos.DataSource = VM.Telefonos;
                dgvTelefonos.DataBind();
                btnEditar.Enable();

                VM.ObtenerDirecciones();
                ViewState["Direcciones"] = VM.Direcciones;
                dgvDirecciones.DataSource = VM.Direcciones;
                dgvDirecciones.DataBind();

                ActivarCamposModulo(false);

                ViewState["DireccionPreviousRow"] = null;
                ViewState["TelefonoPreviousRow"] = null;
            }
        }

        protected void tabDatos_Click(object sender, EventArgs e)
        {
            PanelDatosGeneralesUsuario.Visible = true;
            panelDirecciones.Visible = false;
            panelTelefonos.Visible = false;
            panelAccesos.Visible = false;
            PanelSucursales.Visible = false;

            activeDatosGenerales.Attributes["class"] = "active";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "";

            activeAccesos.RemoveCssClass("active");

            activeSucursales.RemoveCssClass("active");

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
            panelTelefonos.Visible = false;
            panelAccesos.Visible = false;
            PanelSucursales.Visible = false;
            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "active";

            activeTelefonos.Attributes["class"] = "";

            activeSucursales.RemoveCssClass("active");

            activeAccesos.RemoveCssClass("active");
        }

        protected void tabTelefonos_Click(object sender, EventArgs e)
        {
            panelTelefonos.Visible = true;
            PanelDatosGeneralesUsuario.Visible = false;
            panelDirecciones.Visible = false;
            panelAccesos.Visible = false;
            PanelSucursales.Visible = false;

            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "active";

            activeAccesos.RemoveCssClass("active");

            activeSucursales.RemoveCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                PanelBusquedas.Visible = true;
            }
        }

        protected void btnEmpresa_Click(object sender, EventArgs e)
        {
            Response.Redirect("Empresas.aspx");
            txtNombre.Enabled = true;
            txtApellidoPaterno.Enabled = true;
            txtApellidoMaterno.Enabled = true;
            txtFechaNacimiento.Enabled = true;
            txtFechaInicio.Enabled = true;
            txtFechaFin.Enabled = true;
            txtUsuario.Enabled = true;
            txtPassword.Enabled = true;
            txtCorreo.Enabled = true;
        }

        protected void tabAccesos_Click(object sender, EventArgs e)
        {
            panelTelefonos.Visible = false;
            PanelDatosGeneralesUsuario.Visible = false;
            panelDirecciones.Visible = false;
            panelAccesos.Visible = true;
            PanelSucursales.Visible = false;

            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "";

            activeAccesos.AddCssClass("active");

            activeSucursales.RemoveCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);
            else
            {
                panelDireccion.Visible = false;
                PanelBusquedas.Visible = true;
            }
        }

        protected void tabSucursales_Click(object sender, EventArgs e)
        {
            panelTelefonos.Visible = false;
            PanelDatosGeneralesUsuario.Visible = false;
            panelDirecciones.Visible = false;
            PanelSucursales.Visible = true;
            panelAccesos.Visible = false;

            activeAccesos.Attributes["class"] = "";
            activeDatosGenerales.Attributes["class"] = "";

            activeDirecciones.Attributes["class"] = "";

            activeTelefonos.Attributes["class"] = "";

            activeSucursales.AddCssClass("active");

            if (EditingModeDireccion)
                btnCancelarDireccion_Click(null, null);

        }

        protected void btnBuscarSucursal_Click(object sender, EventArgs e)
        {
            if (btnBuscarSucursal.Text.Contains("Buscar Sucursal"))
            {
                string Nombresucursal = txtNombreSucursal.Text;

                VM.Buscarsucursal(SesionActual.uidEmpresaActual.ToString(), Nombresucursal);
                
                if (VM.Sucursales.Count == 0)
                {
                    // FIXME: add message
                    return;
                }

                dgvSucursales.DataSource = VM.Sucursales;
                dgvSucursales.DataBind();

                PanelSucursales.Visible = false;
                userGrid.Visible = true;
            }
            else
            {
                txtNombre.Text = string.Empty;
                txtNombre.Enable();
                btnBuscarSucursal.Text = "Buscar Sucursal";
            }
        }

        #endregion

        #region Temporal

        #region Panel Direcciones 

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
                GridViewRow previousRow = dgvDirecciones.Rows[pos];
                previousRow.RemoveCssClass("success");
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
            ViewState["DireccionPreviousRow"] = null;
        }

        protected void btnCancelarEliminarDireccion_Click(object sender, EventArgs e)
        {
            btnAceptarEliminarDireccion.Visible = false;
            btnCancelarEliminarDireccion.Visible = false;
            lblAceptarEliminarDireccion.Visible = false;
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
        #endregion

        #region Panel Telefonos

        protected void btnAgregarTelefono_Click(object sender, EventArgs e)
        {
            uidTelefono.Text = string.Empty;
            txtTelefono.Text = string.Empty;
            txtTelefono.Enable();
            ddTipoTelefono.SelectedIndex = 0;
            ddTipoTelefono.Enable();
            
            btnOKTelefono.Visible = true;
            btnCancelarTelefono.Visible = true;

            btnAgregarTelefono.Enable();
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

        protected void btnEditarTelefono_Click(object sender, EventArgs e)
        {
            txtTelefono.Enable();

            ddTipoTelefono.Enable();

            btnAgregarTelefono.Disable();

            btnEditarTelefono.Disable();

            btnEliminarTelefono.Disable();

            btnOKTelefono.Visible=true;

            btnCancelarTelefono.Visible=true;
        }

        protected void btnEliminarTelefono_Click(object sender, EventArgs e)
        {
            lblAceptarEliminarTelefono.Visible = true;
            lblAceptarEliminarTelefono.Text = "¿Desea Eliminar El Telefono Seleccionado?";
            btnAceptarEliminarTelefono.Visible = true;
            btnCancelarEliminarTelefono.Visible = true;
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
            txtTelefono.Disable();
            ddTipoTelefono.SelectedIndex = 0;
            ddTipoTelefono.Disable();

            btnOKTelefono.Visible=false;
            btnCancelarTelefono.Visible=false;

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

            btnAgregarTelefono.Enable();
        }

        protected void btnCancelarTelefono_Click(object sender, EventArgs e)
        {
            frmGrpTelefono.RemoveCssClass("has-error");
            lblErrorTelefono.Text = string.Empty;

            txtTelefono.Disable();
            ddTipoTelefono.SelectedIndex = 0;
            ddTipoTelefono.Disable();

            btnOKTelefono.Visible=false;
            btnCancelarTelefono.Visible=false;

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

            btnAgregarTelefono.Enable();
        }

        protected void btnAceptarEliminarTelefono_Click(object sender, EventArgs e)
        {

            btnAgregarTelefono.Enable();
            btnEditarTelefono.Disable();
            btnEliminarTelefono.Disable();

            btnOKTelefono.Visible = false; ;

            btnCancelarTelefono.Visible=false;

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
        }

        protected void btnCancelarEliminarTelefono_Click(object sender, EventArgs e)
        {
            btnAceptarEliminarTelefono.Visible = false;
            btnCancelarEliminarTelefono.Visible = false;
            lblAceptarEliminarTelefono.Visible = false;
        }

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

        #endregion

        #endregion

        #region imagen

        protected void imagen(object sender, EventArgs e)
        {
            if (FUImagen.HasFile)
            {
                string extencion = Path.GetExtension(FUImagen.FileName).ToLower();
                string[] arreglo = {".jpg",".png",".jpeg" };
                for (int i = 0; i < arreglo.Length; i++)
                {
                    if (extencion == arreglo[i])
                    {
                        string Nombrearchivo = Path.GetFileName(FUImagen.FileName);
                        int numero = new Random().Next(999999999);
                        string ruta = "~/Vista/Imagenes/Encargados/" + txtUidEncargado.Text+ '_' + numero + Nombrearchivo;


                        //guardar img
                        FUImagen.SaveAs(Server.MapPath(ruta));

                        string rutaimg = ruta + "?" + (numero - 1);

                        ViewState["rutaimg"] = ruta;

                        ImgEncargado.ImageUrl = rutaimg;

                    }
                }
            }
        }

        #endregion

        #region Paneles

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

        #endregion
        protected void dgvSucursales_SelectedIndexChanged(object sender, EventArgs e)
        {
            VM.Obtenersucursal(new Guid(dgvSucursales.SelectedDataKey.Value.ToString()));
            txtUidSucursal.Text = VM.CSucursal.UidSucursal.ToString();
            txtNombreSucursal.Text = VM.CSucursal.StrNombre;
            txtTipoSucursal.Text = VM.CSucursal.StrTipoSucursal;
            txtNombreSucursal.Disable();
            txtTipoSucursal.Disable();
            
            userGrid.Visible = false;
            btnBuscarSucursal.Text = "Seleccionar otro";
            ViewState["DireccionPreviousRow"] = null;
            ViewState["TelefonoPreviousRow"] = null;
            PanelSucursales.Visible = true;
        }

        protected void dgvSucursales_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvSucursales, "Select$" + e.Row.RowIndex);
            }
        }
    }
}