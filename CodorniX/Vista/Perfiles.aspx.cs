using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodorniX.Modelo;
using CodorniX.Util;
using CodorniX.VistaDelModelo;
using System.Drawing;
using System.Web.UI.HtmlControls;


namespace CodorniX.Vista
{
    public partial class Perfiles : System.Web.UI.Page
    {
        VMPerfiles VM = new VMPerfiles();


        private Sesion SesionActual
        {
            get { return (Sesion)Session["Sesion"]; }
        }
        private bool Generated = false;


        private List<Guid> ModulosBacksite
        {
            get
            {
                if (ViewState["ModulosBacksite"] == null)
                    ViewState["ModulosBacksite"] = new List<Guid>();

                return (List<Guid>)ViewState["ModulosBacksite"];
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
                    checkBox.Checked = false;
                    foreach (Modulo usuarioModulo in modulosDenegados)
                    {
                        if (usuarioModulo.UidModulo == modulo.UidModulo)
                        {
                            checkBox.Checked = true;
                        }
                    }
                }
            }
        }

        private void GenerarCampos(bool regenerate = false)
        {
            if (!regenerate && Generated)
                return;

            Guid nivelAcceso = new Guid(DdNivel.SelectedValue);
            
            if (!string.IsNullOrWhiteSpace(txtUidPerfil.Text))
                VM.ObtenerModulosPerfil(new Guid(txtUidPerfil.Text));
            else
                VM.ObtenerModulosPerfil(Guid.Empty);

            VM.ObtenerNivelesDeAcceso();

            IEnumerable<NivelAcceso> acceso;
            acceso = from e in VM.listNivelAcceso where e.StrNivelAcceso == "Backsite" select e;
            VM.ObtenerModulosNivel(acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.Modulos, ModulosBacksite, VM.ModulosPerfil, modulosBackside);

            acceso = from e in VM.listNivelAcceso where e.StrNivelAcceso == "Backend" select e;
            VM.ObtenerModulosNivel(acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.Modulos, ModulosBackend, VM.ModulosPerfil, modulosBackend);

            /*acceso = from e in VM.listNivelAcceso where e.StrNivelAcceso == "Frontend" select e;
            VM.ObtenerModulosNivel(acceso.FirstOrDefault().UidNivelAcceso);
            GenerarCampos(regenerate, VM.Modulos, ModulosFrontend, VM.ModulosPerfil, modulosFrontend);*/

            Generated = true;

            VM.ObtenerNivelAcceso(nivelAcceso);
            string nivel = VM.NivelAcceso.StrNivelAcceso;

            if (nivel == "Backsite")
            {
                accesoBackside.Visible = true;
                accesoBackend.Visible = false;
                //accesoFrontend.Visible = false;

                activeBackside.Visible = true;
                activeBackend.Visible = true;
                //activeFrontend.Visible = true;

                activeBackside.AddCssClass("active");
                activeBackend.RemoveCssClass("active");
                //activeFrontend.RemoveCssClass("active");

                tabBackside.Disable();
                tabBackend.Enable();
                //tabFrontend.Enable();
            }
            else if (nivel == "Backend")
            {
                ModulosBacksite.Clear();
                accesoBackside.Visible = false;
                accesoBackend.Visible = true;
                //accesoFrontend.Visible = false;

                activeBackside.Visible = false;
                activeBackend.Visible = true;
                //activeFrontend.Visible = true;

                activeBackside.RemoveCssClass("active");
                activeBackend.AddCssClass("active");
                //activeFrontend.RemoveCssClass("active");

                tabBackside.Enable();
                tabBackend.Disable();
                //tabFrontend.Enable();
            }
            else if (nivel == "Frontend")
            {
                ModulosBacksite.Clear();
                ModulosBackend.Clear();

                accesoBackside.Visible = false;
                accesoBackend.Visible = false;
                //accesoFrontend.Visible = true;

                activeBackside.Visible = false;
                activeBackend.Visible = false;
                //activeFrontend.Visible = true;

                activeBackside.RemoveCssClass("active");
                activeBackend.RemoveCssClass("active");
                //activeFrontend.AddCssClass("active");

                tabBackside.Enable();
                tabBackend.Enable();
                //tabFrontend.Disable();
            }
        }

        private void GuardarModulos(Guid uidPerfil)
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
            /*foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosFrontend.FindControl(controlID);
                if (!box.Checked)
                    permisos.Add(modulo);
            }*/
            VM.ActualizarModulos(uidPerfil, permisos);
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
            /*foreach (Guid modulo in modulos)
            {
                string controlID = modulo.ToString();
                CheckBox box = (CheckBox)modulosFrontend.FindControl(controlID);
                if (enable)
                    box.Enable();
                else
                    box.Disable();
            }*/
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (SesionActual == null)
                return;

            if (!Acceso.TieneAccesoAModulo("Perfiles", SesionActual.uidUsuario, SesionActual.uidPerfilActual.Value))
            {
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }

            if (!IsPostBack)
            {
                btnAceptar.Visible = false;
                btnCancelar.Visible = false;
                txtPerfil.Enabled = false;
                DdHome.Enabled = false;
                DdNivel.Enabled = false;
                lblFiltros.Text = "Ocultar";
                DVGPerfiles.Visible = false;
                DVGPerfiles.DataSource = null;
                DVGPerfiles.DataBind();
                VM.ObtenerNivelAcceso();
                DdNivel.DataSource = VM.listNivelAcceso;
                DdNivel.DataTextField = "StrNivelAcceso";
                DdNivel.DataValueField = "UidNivelAcceso";
                DdNivel.DataBind();

                lblNivelAcceso.DataSource = VM.listNivelAcceso;
                lblNivelAcceso.DataTextField = "StrNivelAcceso";
                lblNivelAcceso.DataValueField = "UidNivelAcceso";
                lblNivelAcceso.DataBind();

                VM.ObtenerHome();
                DdHome.DataSource = VM.listModulo;
                DdHome.DataTextField = "StrUrl";
                DdHome.DataValueField = "UidModulo";
                DdHome.DataBind();
                GenerarCampos();
                ActivarCamposModulo(false);
            }
            else
            {
                GenerarCampos();
            }
        }

        protected void DVGPerfiles_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ViewState["EmpresaPreviousRow"] = null;
            if (ViewState["SortColumn"] != null && ViewState["SortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["SortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["SortColumnDirection"];
                SortPerfiles(SortExpression, SortDirection, true);
            }
            else
            {
                DVGPerfiles.DataSource = ViewState["Perfil"];
            }
            DVGPerfiles.PageIndex = e.NewPageIndex;
            DVGPerfiles.DataBind();
        }

        protected void DVGPerfiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPerfil.Enabled = false;
            DdHome.Enabled = false;
            DdNivel.Enabled = false;
            txtPerfil.Enabled = false;
            btnEditar.Enabled = true;
            btnEditar.RemoveCssClass("disabled");
            string UidPerfil = DVGPerfiles.SelectedDataKey.Value.ToString();
            VM.ObtenerPerfiles(new Guid( UidPerfil));
            txtUidPerfil.Text = VM.CPerfil.UidPerfil.ToString();
            txtPerfil.Text = VM.CPerfil.strPerfil;
            DdHome.SelectedIndex = DdHome.Items.IndexOf(DdHome.Items.FindByValue(VM.CPerfil.UidHome.ToString()));
            DdNivel.SelectedIndex = DdNivel.Items.IndexOf(DdNivel.Items.FindByValue(VM.CPerfil.UidNivelAcceso.ToString()));
            int pos = -1;
            if (ViewState["EmpresaPreviousRow"] != null)
            {
                pos = (int)ViewState["EmpresaPreviousRow"];
                GridViewRow previousRow = DVGPerfiles.Rows[pos];
                previousRow.RemoveCssClass("success");
            }

            ViewState["EmpresaPreviousRow"] = DVGPerfiles.SelectedIndex;
            DVGPerfiles.SelectedRow.AddCssClass("success");
            GenerarCampos(true);
            ActivarCamposModulo(false);
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            btnNuevo.Enabled = true;
            btnNuevo.RemoveCssClass("disabled");

        }

        protected void DVGPerfiles_Sorting(object sender, GridViewSortEventArgs e)
        {
            ViewState["EmpresaPreviousRow"] = null;
            SortPerfiles(e.SortExpression, e.SortDirection);

            DVGPerfiles.DataBind();
        }
        private void SortPerfiles(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            List<Perfil> perfil = (List<Perfil>)ViewState["Perfil"];

            if (SortExpression == (string)ViewState["SortColumn"] && !same)
            {
                SortDirection =
                    ((SortDirection)ViewState["SortColumnDirection"] == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            if (SortExpression == "Perfil")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                perfil = perfil.OrderBy(x => x.strPerfil).ToList();
                }
                else
                {
                    perfil = perfil.OrderByDescending(x => x.strPerfil).ToList();
                }
            }
            else 
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    perfil = perfil.OrderBy(x => x.StrNivelAcceso).ToList();
                }
                else
                {
                    perfil = perfil.OrderByDescending(x => x.StrNivelAcceso).ToList();
                }
            }

            DVGPerfiles.DataSource = perfil;
            ViewState["SortColumn"] = SortExpression;
            ViewState["SortColumnDirection"] = SortDirection;
        }

        protected void DVGPerfiles_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(DVGPerfiles, "Select$" + e.Row.RowIndex);
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            btnNuevo.Disable();
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            txtUidPerfil.Text = string.Empty;
            txtPerfil.Enabled = true;
            txtPerfil.Text = string.Empty;
            btnEditar.Enabled = false;
            btnEditar.CssClass = "btn btn-sm disabled btn-default";
            GenerarCampos(true);
            ActivarCamposModulo(true);
            DdHome.Enabled = true;
            DdNivel.Enabled = true;
        }

        protected void btnEditar_Click(object sender, EventArgs e)
        {
            btnAceptar.Visible = true;
            btnCancelar.Visible = true;
            btnNuevo.Enabled = false;
            btnNuevo.AddCssClass("disabled");
            btnEditar.Enabled = false;
            btnEditar.AddCssClass("disabled");
            txtPerfil.Enabled = true;
            GenerarCampos();
            ActivarCamposModulo(true);
            DdHome.Enabled = true;
            DdNivel.Enabled = true;

        }

        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            string home= DdHome.SelectedValue;
            string nivel = DdNivel.SelectedValue;
            if (txtPerfil.Text==string.Empty)
            {
                lblErrorPerfil.Text = "El campo Perfil tiene que ser llenado necesariamente.";
                txtPerfil.Focus();
                frmGrpPerfiles.AddCssClass("has-error");
                return;
            }
            string perfil = txtPerfil.Text;
            if (txtUidPerfil.Text == string.Empty)
            {
                if (VM.GuardarPerfilsinempresa(perfil, nivel,home))
                {


                    lblMensaje.Text = "Guardado Correctamente";
                    btnAceptar.Visible = false;
                    btnCancelar.Visible = false;
                    txtPerfil.Enabled = false;
                    txtPerfil.Text = string.Empty;
                    btnAceptar.Visible = false;
                    btnCancelar.Visible = false;
                }
                else
                {
                    lblMensaje.Text = "Error al Guardarse";
                }
            }
            else
            {
                if (VM.ModificarPerfil(txtUidPerfil.Text, perfil, nivel, home))
                {
                    lblMensaje.Text = "Modificado Correctamente";
                    txtPerfil.Text = string.Empty;
                    txtPerfil.Enabled = false;
                    btnAceptar.Visible = false;
                    btnCancelar.Visible  = false;
                }
                else
                {
                    lblMensaje.Text = "Error al Modificar";
                }
            }
            Guid UidPerfil = VM.CPerfil.UidPerfil;
            GuardarModulos(UidPerfil);
            DVGPerfiles.Visible = true;
            VM.CargarPerfiles();
            DVGPerfiles.DataSource = VM.ltsPerfil;
            DVGPerfiles.DataBind();
            ActivarCamposModulo(false);
            btnEditar.Enabled = false;
            btnEditar.AddCssClass("disabled");
            DdHome.Enabled = false;
            DdNivel.Enabled = false;
            PanelFiltros.Visible = false;
            btnNuevo.Enable();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            btnAceptar.Visible = false;
            btnCancelar.Visible = false;
            txtPerfil.Enabled = false;
            DdHome.Enabled = false;
            DdNivel.Enabled = false;
            lblErrorPerfil.Visible = false;
            ActivarCamposModulo(false);
            if (txtUidPerfil.Text.Length == 0)
            {
                txtPerfil.Text = string.Empty;
                btnEditar.Enabled = false;
                btnEditar.CssClass = "btn btn-sm disabled btn-default";
            }
            else
            {
                VM.ObtenerPerfiles(new Guid(txtUidPerfil.Text));
                txtUidPerfil.Text = VM.CPerfil.UidPerfil.ToString();
                txtPerfil.Text = VM.CPerfil.strPerfil;
                DdHome.SelectedIndex = DdHome.Items.IndexOf(DdHome.Items.FindByValue(VM.CPerfil.UidHome.ToString()));
                DdNivel.SelectedIndex = DdNivel.Items.IndexOf(DdNivel.Items.FindByValue(VM.CPerfil.UidNivelAcceso.ToString()));
                btnEditar.Enabled = true;
                btnEditar.CssClass = "btn btn-sm btn-default";
            }
            btnNuevo.Enable();
        }

        protected void btnMostrar_Click(object sender, EventArgs e)
        {
            string texto = lblFiltros.Text;
            DVGPerfiles.Visible = false;
            if (texto=="Mostrar")
            {
                btnBuscar.Enabled = true;
                btnBuscar.CssClass = "btn btn-sm btn-default";
                btnLimpiar.Enabled = true;
                btnLimpiar.CssClass = "btn btn-sm btn-default";
                lblFiltros.Text = "Ocultar";
                lblFiltros.CssClass = "glyphicon glyphicon-collapse-up";
                PanelFiltros.Visible = true;
            }
            else if (texto == "Ocultar")
            {
                PanelFiltros.Visible = false;
                btnBuscar.Enabled = false;
                btnBuscar.CssClass = "btn btn-sm btn-default disabled";
                btnLimpiar.Enabled = false;
                btnLimpiar.CssClass = "btn btn-sm btn-default disabled";
                lblFiltros.Text = "Mostrar";
                lblFiltros.CssClass = "glyphicon glyphicon-collapse-down";
                DVGPerfiles.Visible = true;
            }
        }

        protected void btnLimpiar_Click(object sender, EventArgs e)
        {
            FiltroPerfiles.Text = string.Empty;
            lblNivelAcceso.ClearSelection();
            DVGPerfiles.DataSource = VM.ltsPerfil;
            DVGPerfiles.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            string nivelacceso = "";
            int[] i = lblNivelAcceso.GetSelectedIndices();
            foreach (int j in i)
            {
                string value = lblNivelAcceso.Items[j].Value;
                if (nivelacceso.Count() == 0)
                    nivelacceso += value;
                else
                    nivelacceso += "," + value;
            }
            string NivelAcceso = nivelacceso;


            PanelFiltros.Visible = false;
            lblFiltros.Text = "Mostrar";
            lblFiltros.CssClass = "glyphicon glyphicon-collapse-down";
            btnLimpiar.Enabled = false;
            btnLimpiar.CssClass = "btn btn-sm btn-default disabled";

            btnBuscar.Enabled = false;
            btnBuscar.CssClass = "btn btn-sm btn-default disabled";

            string perfil = FiltroPerfiles.Text.Trim();

            VM.Buscar(perfil, NivelAcceso);

            DVGPerfiles.Visible = true;
            DVGPerfiles.DataSource = VM.ltsPerfil;
           DVGPerfiles.DataBind();
            ViewState["Perfil"] = VM.ltsPerfil;
        }

        protected void tabDatos_Click(object sender, EventArgs e)
        {
            panelGestiondePerfiles.Visible = true;
            panelAccesos.Visible = false;

            activeDatosGenerales.Attributes["class"] = "active";
            
            activeAccesos.RemoveCssClass("active");
            
        }

        protected void tabAccesos_Click(object sender, EventArgs e)
        {
            panelGestiondePerfiles.Visible = false;
            panelAccesos.Visible = true;

            activeDatosGenerales.Attributes["class"] = "";

            activeAccesos.AddCssClass("active");
        }

        protected void tabBackside_Click(object sender, EventArgs e)
        {
            accesoBackside.Visible = true;
            accesoBackend.Visible = false;
            //accesoFrontend.Visible = false;

            activeBackside.AddCssClass("active");
            activeBackend.RemoveCssClass("active");
            //activeFrontend.RemoveCssClass("active");

            tabBackside.Disable();
            tabBackend.Enable();
            //tabFrontend.Enable();
        }

        protected void tabBackend_Click(object sender, EventArgs e)
        {
            accesoBackside.Visible = false;
            accesoBackend.Visible = true;
            //accesoFrontend.Visible = false;

            activeBackside.RemoveCssClass("active");
            activeBackend.AddCssClass("active");
            //activeFrontend.RemoveCssClass("active");

            tabBackside.Enable();
            tabBackend.Disable();
            //tabFrontend.Enable();
        }

        protected void tabFrontend_Click(object sender, EventArgs e)
        {
            accesoBackside.Visible = false;
            accesoBackend.Visible = false;
            //accesoFrontend.Visible = true;

            activeBackside.RemoveCssClass("active");
            activeBackend.RemoveCssClass("active");
            //activeFrontend.AddCssClass("active");

            tabBackside.Enable();
            tabBackend.Enable();
            //tabFrontend.Disable();
        }
    }
}