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
        #region Construccion
       
        #region Variables Historico generales
        VMHistoricoVentas VM = new VMHistoricoVentas();
        VMSucursales VMSucursal = new VMSucursales();
        VMEncargados VMEncargado = new VMEncargados();
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
                dgvHistoricos.Visible = false;
                dgvHistoricos.AddCssClass("hidden");
                dgvHistoricos.DataSource = null;
                dgvHistoricos.DataBind();

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


                #region Obtener listas de DropDownList (dd)
                //-----------------------------------------------------------------------------
                //VM.ObtenerMedidas();

                //ddMedidaFoto.DataSource = VM.Medidas;
                //ddMedidaFoto.DataValueField = "UidMedida";
                //ddMedidaFoto.DataTextField = "VchMedida";
                //ddMedidaFoto.DataBind();

                //ViewState["Sucursales"] = VM.Sucursales;
                //dgvSucursales.DataSource = VM.Sucursales;
                //dgvSucursales.DataBind();
                //dgvSucursales.Visible = true;
                //dgvSucursales.RemoveCssClass("hidden");

                //SUCURSALES ----------------------------------------------------------------------------------------------------
                try
                {
                   VMSucursal.BuscarSucursales("", null , null , "", SesionActual.uidEmpresaActual.Value, new Guid());
                }
                catch (Exception et)
                {
                    LbErrorHistoricos.Text = "Error al obtener la lista de SUCURSALES \n cargando ....\n" + et;
                   // Response.Redirect("HistoricoVentas.aspx");
                }
                ViewState["Sucursales"] = VMSucursal.Sucursales;

                ddSucursales.DataSource = VMSucursal.Sucursales;
              
                ddSucursales.DataValueField = "UidSucursal";
                ddSucursales.DataTextField = "StrNombre";
                
                ddSucursales.DataBind();
                ddSucursales.Items.Insert(0, new ListItem("Todos", "00000000-0000-0000-0000-000000000000"));
                ddSucursales.SelectedIndex = 0;
                //END SUCURSALES ----------------------------------------------------------------------------------------------------
                //ENCARGADOS ----------------------------------------------------------------------------------------------------
                try
                {
                    VMEncargado.BuscarUsuarios("", "", "", "",
                       "", "", "", "", "", "",
                       "", "", "", SesionActual.uidEmpresaActual.Value, "");
                }
                catch (Exception et)
                {
                    LbErrorHistoricos.Text = "Error al obtener la lista de ENCARGADOS \n cargando ....\n" + et;
                    // Response.Redirect("HistoricoVentas.aspx");
                }
                ViewState["Encargados"] = VMEncargado.LISTADEUSUARIOS;

                ddEncargados.DataSource = VMEncargado.LISTADEUSUARIOS;

                ddEncargados.DataValueField = "UIDUSUARIO";
                ddEncargados.DataTextField = "STRUSUARIO";

                ddEncargados.DataBind();
                ddEncargados.Items.Insert(0, new ListItem("Todos", "00000000-0000-0000-0000-000000000000"));
                ddEncargados.SelectedIndex = 0;
                //END ENCARGADOS ----------------------------------------------------------------------------------------------------
                //FOTOGRAFIAS ----------------------------------------------------------------------------------------------------

                //ddFotos.Items.Clear();
                //ViewState["Fotos"] = null;
                //List<SucursalFoto> listfoto = new List<SucursalFoto>();
                //try
                //{
                //    foreach (Sucursal pos in VMSucursal.Sucursales)
                //    {
                //        VMSucursal.Sucursal = pos;
                //        VMSucursal.Obtenerfotos();
                //        for (int i = 0; i < VMSucursal.Fotos.Count; i++)
                //        {
                //            VMSucursal.Fotos[i].StrDescripcion = VMSucursal.Sucursal.StrNombre + "(" + VMSucursal.Fotos[i].StrDescripcion + ")";
                //            listfoto.Add(VMSucursal.Fotos[i]);
                //        }

                //    }
                //}
                //catch (Exception et)
                //{
                //    LbErrorHistoricos.Text = "Error al obtener la lista de FOTOGRAFIAS \n cargando ....\n" + et;
                //    // Response.Redirect("HistoricoVentas.aspx");
                //}
                //ViewState["Fotos"] = listfoto;
                //ddFotos.DataSource = listfoto;

                //ddFotos.DataValueField = "UidFoto";
                //ddFotos.DataTextField = "StrDescripcion";

                //ddFotos.DataBind();
                //ddFotos.Items.Insert(0, new ListItem("Todos", ""));
                //ddFotos.SelectedIndex = 0;
                //END FOTOGRAFIAS ----------------------------------------------------------------------------------------------------

                #endregion Obtener listas de DropDownList (dd)

            }
            ScriptManager.RegisterStartupScript(this, typeof(Page), "initializeDatapicker", "enableDatapicker()", true);
        }
        #endregion Construccion

        #region panel busqueda

        protected void dgvHistoricos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvHistoricos, "Select$" + e.Row.RowIndex);
            }
        }
        private void SortHistorico(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            List<HTurno> HTurnos = (List<HTurno>)ViewState["HTurnos"];

            if (SortExpression == (string)ViewState["SortColumn"] && !same)
            {
                // We are resorting the same column, so flip the sort direction
                SortDirection =
                    ((SortDirection)ViewState["SortColumnDirection"] == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            if (SortExpression == "Sucursal")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.StrSucursal).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.StrSucursal).ToList();
                }
            }
            else if (SortExpression == "Encargado")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.StrEncargado).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.StrEncargado).ToList();
                }
            }
            else if (SortExpression == "Folio")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.IntFolio).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.IntFolio).ToList();
                }
            }
            else if (SortExpression == "NoFotos")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.IntNoFotos).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.IntNoFotos).ToList();
                }
            }
            else if (SortExpression == "Importe")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.DouImporte).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.DouImporte).ToList();
                }
            }
            else if (SortExpression == "FechaApertura")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.DtApertura).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.DtApertura).ToList();
                }
            }
            else
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurnos = HTurnos.OrderBy(x => x.DtCierre).ToList();
                }
                else
                {
                    HTurnos = HTurnos.OrderByDescending(x => x.DtCierre).ToList();
                }
            }
            dgvHistoricos.DataSource = HTurnos;
            ViewState["SortColumn"] = SortExpression;
            ViewState["SortColumnDirection"] = SortDirection;
        }
        protected void dgvHistoricos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try { 
            List<HTurno> turnos = (List<HTurno>)ViewState["HTurnos"];
            VM.HTurno= turnos.Select(x => x).Where(x => x.UidTurno.ToString() == dgvHistoricos.SelectedDataKey.Value.ToString()).First();
            UidHTurno.Text = VM.HTurno.UidTurno.ToString();
            txtSucursal.Text = VM.HTurno.StrSucursal;
            txtEncargado.Text = VM.HTurno.StrEncargado;
            txtFolio.Text = VM.HTurno.IntFolio.ToString();
            txtFApertura.Text = VM.HTurno.DtApertura.ToString("dd/MM/yyyy");
            txtFCierre.Text = VM.HTurno.DtCierre.ToString("dd/MM/yyyy");
            txtCantFotos.Text = VM.HTurno.IntNoFotos.ToString();
            txtImporte.Text = VM.HTurno.DouImporte.ToString();

            VM.BuscarHImpresion(VM.HTurno.UidTurno);
            ViewState["HTurno"] = VM.HTurno;
            dgvHImpresiones.DataSource = VM.HTurno.LHImpresiones;
            dgvHImpresiones.DataBind();

                LbErrorHistoricoDetalle.Visible = false;
                LbErrorHistoricoResumen.Visible = false;
                LbErrorHistoricoDetalle.Text = "";
                LbErrorHistoricoResumen.Text = "";
                PnErrorHistoricoDetalle.Visible = false;
                PnErrorHistoricoResumen.Visible = false;
            }
            catch (Exception et)
            {
                PnErrorHistoricoDetalle.Visible = true;
                PnErrorHistoricoResumen.Visible = true;
                LbErrorHistoricoDetalle.Visible = true;
                LbErrorHistoricoResumen.Visible = true;
                LbErrorHistoricoResumen.Text = "Error al obtener la lista de Historico venta \n cargando ....\n" + et;
                LbErrorHistoricoDetalle.Text = "Error al obtener la lista de Historico venta \n cargando ....\n" + et;
                // Response.Redirect("HistoricoVentas.aspx");
            }
}

        protected void dgvHistoricos_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Invalidate Last position
            ViewState["HTurnoPreviousRow"] = null;
            SortHistorico(e.SortExpression, e.SortDirection);
            dgvHistoricos.DataBind();
        }

        protected void dgvHistoricos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Invalidate Last Position
            ViewState["HTurnoPreviousRow"] = null;
            if (ViewState["SortColumn"] != null && ViewState["SortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["SortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["SortColumnDirection"];
                SortHistorico(SortExpression, SortDirection, true);
            }
            else
            {
                dgvHistoricos.DataSource = ViewState["HTurnos"];
            }
            dgvHistoricos.PageIndex = e.NewPageIndex;
            dgvHistoricos.DataBind();
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            btnMostrarBusqueda.Text = "Mostrar";
            seccionBusqueda.Visible = false;
            try
            {
               
             VM.BuscarHTurno(SesionActual.uidEmpresaActual.Value.ToString()
             ,ddSucursales.SelectedValue, ddEncargados.SelectedValue
             ,ddOperadorNoFolioRangoMenor.SelectedValue, txtBusquedaNoFolioEntre.Text.Trim(), ddOperadorNoFolioRangoMayor.SelectedValue, txtBusquedaFolioY.Text.Trim()
             ,ddOperadorCantFotosRangoMenor.SelectedValue, txtBusquedaCantFotosEntre.Text.Trim(),ddOperadorCantFotosRangoMayor.SelectedValue, txtBusquedaCantFotosY.Text.Trim()
             ,ddOperadorImporteRangoMenor.SelectedValue, txtBusquedaImporteEntre.Text.Trim(), ddOperadorImporteRangoMayor.SelectedValue, txtBusquedaImporteY.Text.Trim()
             ,ddOperadorFechaAperturaRangoMenor.SelectedValue, txtBusquedaMinFechaApertura.Text.Trim(), ddOperadorFechaAperturaRangoMayor.SelectedValue, txtBusquedaMaxFechaApertura.Text.Trim()
             ,ddOperadorFechaCierreRangoMenor.SelectedValue, txtBusquedaMinFechaCierre.Text.Trim(), ddOperadorFechaCierreRangoMayor.SelectedValue, txtBusquedaMaxFechaCierre.Text.Trim());


                ViewState["HTurnos"] = VM.LHTurnos;
                dgvHistoricos.DataSource = VM.LHTurnos;
                dgvHistoricos.DataBind();
                dgvHistoricos.Visible = true;
                dgvHistoricos.RemoveCssClass("hidden");

                //var groupedListCantFotos = VM.LHTurnos.GroupBy(d => d.IntNoFotos).Select(g => new { Value = g.Sum(s => s.IntNoFotos) });
                var groupedListCantFotos = VM.LHTurnos.Sum(item => item.IntNoFotos);
                txtBusReCantFotos.Text = groupedListCantFotos.ToString();
                var groupedListImporte = VM.LHTurnos.Sum(item => item.DouImporte);
                txtBusReImporte.Text = groupedListImporte.ToString();
                var groupedListTurnosEncontrados = VM.LHTurnos.Count();
                txtBusReTurnosEnc.Text = groupedListTurnosEncontrados.ToString();
                var groupedListFechasApertura = VM.LHTurnos.OrderBy(x => x.DtApertura).ToList();
                var groupedListFechasCierre = VM.LHTurnos.OrderBy(x => x.DtCierre).ToList();
                var DifFeCiLastFeApFirst = groupedListFechasCierre.Last().DtCierre - groupedListFechasApertura.First().DtApertura;
                //var ApDifFeApLastFeApFirst = groupedListFechasApertura.Last().DtApertura - groupedListFechasApertura.First().DtApertura;
                //var CiDifFeCiLastFeCiFirst = groupedListFechasCierre.Last().DtCierre - groupedListFechasApertura.First().DtCierre;
                txtBusReDiasEnc.Text = DifFeCiLastFeApFirst.Days.ToString();

                //DateTime startv1 = new DateTime(2019, 8, 11);
                //DateTime endv1 = new DateTime(2019, 12, 23);

                //TimeSpan differencev1 = endv1 - startv1; //create TimeSpan object

                //DateTime startv2 = new DateTime(11,8,2019);
                //DateTime endv2 = new DateTime(23, 12, 2019);

                //TimeSpan differencev2 = endv2 - startv2; //create TimeSpan object
                LbErrorHistoricos.Text = "";
                LbErrorResumenHistoricos.Text = "";
                LbErrorHistoricos.Visible=false;
                LbErrorResumenHistoricos.Visible = false;
                PnErrorBusResumenHistoricos.Visible = false;
                PnErrorBusHistoricos.Visible = false;
            }
            catch (Exception et)
            {
                PnErrorBusResumenHistoricos.Visible = true;
                PnErrorBusHistoricos.Visible = true;
                LbErrorResumenHistoricos.Text = "Error al obtener la lista de Historico turno \n cargando ....\n" + et;
                LbErrorHistoricos.Visible = true;
                LbErrorResumenHistoricos.Visible = true;
                LbErrorHistoricos.Text = "Error al obtener la lista de Historico turno \n cargando ....\n" + et;
                // Response.Redirect("HistoricoVentas.aspx");
            }

        }
        protected void btnBorrarBusqueda_Click(object sender, EventArgs e)
        {
            ddSucursales.SelectedIndex = 0;
            ddEncargados.SelectedIndex = 0;

            ddOperadorNoFolioRangoMenor.SelectedIndex = 0;
            txtBusquedaNoFolioEntre.Text = string.Empty;
            ddOperadorNoFolioRangoMayor.SelectedIndex = 0;
            txtBusquedaFolioY.Text = string.Empty;

            ddOperadorCantFotosRangoMenor.SelectedIndex = 0;
            txtBusquedaCantFotosEntre.Text = string.Empty;
            ddOperadorCantFotosRangoMayor.SelectedIndex = 0;
            txtBusquedaCantFotosY.Text = string.Empty;

            ddOperadorImporteRangoMenor.SelectedIndex = 0;
            txtBusquedaImporteEntre.Text = string.Empty;
            ddOperadorImporteRangoMayor.SelectedIndex = 0;
            txtBusquedaImporteY.Text = string.Empty;

            ddOperadorFechaAperturaRangoMenor.SelectedIndex = 0;
            txtBusquedaMinFechaApertura.Text = string.Empty;
            ddOperadorFechaAperturaRangoMayor.SelectedIndex = 0;
            txtBusquedaMaxFechaApertura.Text = string.Empty;

            ddOperadorFechaCierreRangoMenor.SelectedIndex = 0;
            txtBusquedaMinFechaCierre.Text = string.Empty;
            ddOperadorFechaCierreRangoMayor.SelectedIndex = 0;
            txtBusquedaMaxFechaCierre.Text = string.Empty;
        }

        protected void btnMostrarBusqueda_Click(object sender, EventArgs e)
        {
            if (btnMostrarBusqueda.Text == "Mostrar")
            {
                dgvHistoricos.Visible = false;
                dgvHistoricos.AddCssClass("hidden");

                seccionBusqueda.Visible = true;

                btnBorrarBusqueda.Visible = true;
                btnBuscar.Visible = true;

                btnMostrarBusqueda.Text = "Ocultar";
            }
            else
            {
                dgvHistoricos.Visible = true;
                dgvHistoricos.RemoveCssClass("hidden");

                seccionBusqueda.Visible = false;

                btnBorrarBusqueda.Visible = false;
                btnBuscar.Visible = false;

                btnMostrarBusqueda.Text = "Mostrar";
            }
        }
        protected void tabBusResumen_Click(object sender, EventArgs e)
        {
            //LbErrorHistoricos.Visible = false;
            //LbErrorResumenHistoricos.Visible = false;

            panelBusqueda.Visible = false;
            activeBusqueda.Attributes["class"] = "";

            panelBusResumen.Visible = true;
            activeBusResumen.Attributes["class"] = "active";
        }

        protected void tabBusqueda_Click(object sender, EventArgs e)
        {
            //LbErrorHistoricos.Visible = false;
            //LbErrorResumenHistoricos.Visible = false;

            panelBusqueda.Visible = true;
            activeBusqueda.Attributes["class"] = "active";

            panelBusResumen.Visible = false;
            activeBusResumen.Attributes["class"] = "";
        }
        #endregion panel busqueda

        #region Panel Derecho (1 Historico turno)
        protected void tabResumen_Click(object sender, EventArgs e)
        {

            //LbErrorHistoricoResumen.Visible = false;
            //LbErrorHistoricoDetalle.Visible = false;

            panelResumen.Visible = true;
            activeResumen.Attributes["class"] = "active";

            panelDetalle.Visible = false;
            activeDetalle.Attributes["class"] = "";
        }

        protected void tabDetalle_Click(object sender, EventArgs e)
        {


            //LbErrorHistoricoResumen.Visible = false;
            //LbErrorHistoricoDetalle.Visible = false;

            panelResumen.Visible = false;
            activeResumen.Attributes["class"] = "";

            panelDetalle.Visible = true;
            activeDetalle.Attributes["class"] = "active";
        }

        protected void dgvHImpresiones_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(dgvHImpresiones, "Select$" + e.Row.RowIndex);
            }
        }
        private void SortHImpresiones(string SortExpression, SortDirection SortDirection, bool same = false)
        {
            HTurno HTurno = (HTurno)ViewState["HTurno"];

            if (SortExpression == (string)ViewState["HISortColumn"] && !same)
            {
                // We are resorting the same column, so flip the sort direction
                SortDirection =
                    ((SortDirection)ViewState["HISortColumnDirection"] == SortDirection.Ascending) ?
                    SortDirection.Descending : SortDirection.Ascending;
            }

            if (SortExpression == "FechaHora")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.StrFechaHora).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.StrFechaHora).ToList();
                }
            }
            else if (SortExpression == "NoFolio")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.IntFolio).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.IntFolio).ToList();
                }
            }
            else if (SortExpression == "NoMaquina")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.IntMaquina).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.IntMaquina).ToList();
                }
            }
            else if (SortExpression == "NoCopias")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.StrCopiasXImpresion).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.StrCopiasXImpresion).ToList();
                }
            }
            else if (SortExpression == "NoFotos")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.StrFotosXCopiasXImpresion).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.StrFotosXCopiasXImpresion).ToList();
                }
            }
            else if (SortExpression == "FotoDesc")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.StrFotoDesc).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.StrFotoDesc).ToList();
                }
            }
            else if (SortExpression == "CostoTicket")
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.StrCostoTicket).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.StrCostoTicket).ToList();
                }
            }
            else
            {
                if (SortDirection == SortDirection.Ascending)
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderBy(x => x.StrCosto).ToList();
                }
                else
                {
                    HTurno.LHImpresiones = HTurno.LHImpresiones.OrderByDescending(x => x.StrCosto).ToList();
                }
            }
            dgvHImpresiones.DataSource = HTurno.LHImpresiones;
            ViewState["HISortColumn"] = SortExpression;
            ViewState["HISortColumnDirection"] = SortDirection;
        }
        protected void dgvHImpresiones_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void dgvHImpresiones_Sorting(object sender, GridViewSortEventArgs e)
        {
            // Invalidate Last position
            ViewState["HImpresionPreviousRow"] = null;
            SortHImpresiones(e.SortExpression, e.SortDirection);
            dgvHImpresiones.DataBind();
        }

        protected void dgvHImpresiones_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Invalidate Last Position
            ViewState["HImpresionPreviousRow"] = null;
            if (ViewState["HISortColumn"] != null && ViewState["HISortColumnDirection"] != null)
            {
                string SortExpression = (string)ViewState["HISortColumn"];
                SortDirection SortDirection = (SortDirection)ViewState["HISortColumnDirection"];
                SortHImpresiones(SortExpression, SortDirection, true);
            }
            else
            {
                HTurno HTurno = (HTurno)ViewState["HTurno"];
                dgvHImpresiones.DataSource = HTurno.LHImpresiones;
            }
            dgvHImpresiones.PageIndex = e.NewPageIndex;
            dgvHImpresiones.DataBind();
        }
        #endregion Panel Derecho (1 Historico turno)


    }
}