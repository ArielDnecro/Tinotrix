<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Encargados.aspx.cs" Inherits="CodorniX.Vista.Encargados" MasterPageFile="Site1.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
        <!--panel izquierdo-->
        <div class="col-md-6">

            <asp:PlaceHolder ID="PanelBusquedas" runat="server">
                <div class="panel panel-primary">
                    <div class="panel-heading text-center">
                        Buscar Encargados
                    </div>
                    <div class="text-right">
                        <asp:Label ID="lblMensajeIzquierdo" runat="server" />
                        <asp:LinkButton ID="btnMostrar" OnClick="btnVisibilidadPanelFiltros" CssClass="btn btn-sm btn-default" runat="server">
                            <asp:Label ID="lblFiltros" class="glyphicon glyphicon-collapse-up" runat="server" />
                        </asp:LinkButton><asp:LinkButton ID="btnLimpiar" OnClick="LimpiarFiltros" CssClass="btn btn-sm btn-default" runat="server">
                            <span class="glyphicon glyphicon-trash"></span>
                            Limpiar
                        </asp:LinkButton><asp:LinkButton ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-sm btn-default" runat="server">
                            <span class="glyphicon glyphicon-search"></span>
                            Buscar
                        </asp:LinkButton>
                    </div>
                    <div class="panel-body">
                        <asp:Panel ID="PanelFiltros" runat="server">
                            <div class="row">
                                <div class="col-md-6">
                                    <h6>Nombre</h6>
                                    <asp:TextBox CssClass="form-control" ID="FiltroNombre" runat="server" />
                                </div>
                                <div class="col-md-6">
                                    <h6>Apellido Paterno</h6>
                                    <asp:TextBox CssClass="form-control" ID="FiltroApellidoPaterno" runat="server" />
                                </div>
                                <div class="col-md-4">
                                    <h6>Apellido Materno</h6>
                                    <asp:TextBox CssClass="form-control" ID="FiltroApellidoMaterno" runat="server" />
                                </div>
                                <div class="col-md-4">
                                    <h6>Correo</h6>
                                    <asp:TextBox CssClass="form-control" ID="FiltroCorreo" runat="server" />
                                </div>
                                <div class="col-md-4">
                                    <h6>Usuario</h6>
                                    <asp:TextBox CssClass="form-control" ID="FiltroUsuario" runat="server" />
                                </div>
                                <div class="col-md-6">
                                    <h6>Fecha Nacimiento</h6>
                                    <div class="input-group date" id="fechaN">
                                        <asp:TextBox ID="FiltroFechaNacimiento" placeholder="Fecha Nacimiento" CssClass="form-control fechaN" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <h6>Fecha Nacimiento 2</h6>
                                    <div class="input-group date" id="fechaN2">
                                        <asp:TextBox ID="FiltroFechaNacimiento2" placeholder="Fecha Nacimiento" CssClass="form-control fechaN2" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <h6>Fecha Inicio</h6>
                                    <div class="input-group date" id="fechaI">
                                        <asp:TextBox ID="FiltroFechaInicio" placeholder="Fecha Nacimiento" CssClass="form-control fechaI" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <h6>Fecha Inicio 2</h6>
                                    <div class="input-group date" id="fechaI2">
                                        <asp:TextBox ID="FiltroFechaInicio2" placeholder="Fecha Nacimiento" CssClass="form-control fechaI2" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <h6>Fecha de terminación</h6>
                                    <div class="input-group date" id="fechaF">
                                        <asp:TextBox ID="FiltroFechaFin" placeholder="Fecha Nacimiento" CssClass="form-control fechaF" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <h6>Fecha de terminación 2</h6>
                                    <div class="input-group date" id="fechaF2">
                                        <asp:TextBox ID="FiltroFechaFin2" placeholder="Fecha Nacimiento" CssClass="form-control fechaF2" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <h6>Perfil</h6>
                                    <!-- <asp:DropDownList ID="DdBuscarPerfil" runat="server" CssClass="form-control" Enabled="false" />-->
                                    <asp:ListBox ID="lblPerfil" runat="server" SelectionMode="Multiple" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <h6>Status</h6>
                                    <asp:ListBox ID="lblStatus" runat="server" SelectionMode="Multiple" CssClass="form-control" />
                                </div>
                                <div class="col-md-6">
                                    <h6>Sucursal</h6>
                                    <asp:ListBox ID="ListSucursal" runat="server" SelectionMode="Multiple" CssClass="form-control" />
                                </div>
                            </div>
                        </asp:Panel>
                        <div style="padding-top: 25px">
                            <asp:GridView ID="DVGEncargados" AllowPaging="true" PageSize="10" OnPageIndexChanging="DVGEncargados_PageIndexChanging" OnSelectedIndexChanged="DVGEncargados_SelectedIndexChanged" OnSorting="DVGEncargados_Sorting" OnRowDataBound="DVGEncargados_RowDataBound" AutoGenerateColumns="false" DataKeyNames="UIDUSUARIO" CssClass="table table-bordered table-condensed table-striped input-sm" AllowSorting="true" runat="server">
                                <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    No hay Datos
                                </EmptyDataTemplate>
                                <Columns>
                                    <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="STRNOMBRE" HeaderText="Nombre" SortExpression="Nombre" />
                                    <asp:BoundField DataField="STRAPELLIDOPATERNO" HeaderText="Apellido Paterno" SortExpression="ApellidoPaterno" />
                                    <asp:BoundField DataField="STRUSUARIO" HeaderText="Usuario" SortExpression="Usuario" />
                                    <asp:BoundField DataField="DtFechaInicio" DataFormatString="{0:d}" HtmlEncode="false" HeaderText="Fecha de Inicio" SortExpression="FechaInicio" />
                                    <asp:TemplateField HeaderText="Perfil" SortExpression="StrPerfil">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPerfil" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StrPerfil" HeaderText="Perfil" HeaderStyle-CssClass="hidden" FooterStyle-CssClass="hidden" ItemStyle-CssClass="hidden" />
                                    <asp:TemplateField HeaderText="Estatus" SortExpression="StrStatus">
                                        <ItemTemplate>
                                            <asp:Label ID="lblEstatus" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="StrStatus" HeaderStyle-CssClass="hidden" FooterStyle-CssClass="hidden" ItemStyle-CssClass="hidden" HeaderText="Status" />
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <asp:PlaceHolder ID="panelDireccion" Visible="false" runat="server">
                <div class="panel panel-primary">
                    <div class="panel-heading">
                        Dirección
                    </div>
                    <div class="btn-toolbar">
                        <div class="btn-group pull-right">
                            <asp:LinkButton ID="btnOkDireccion" runat="server" CssClass="btn btn-success btn-sm disabled" OnClick="btnOkDireccion_Click">
                                <span class="glyphicon glyphicon-ok"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancelarDireccion" runat="server" CssClass="btn btn-danger btn-sm disabled" OnClick="btnCancelarDireccion_Click">
                                <span class="glyphicon glyphicon-remove"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCerrarDireccion" runat="server" CssClass="btn btn-default btn-sm" Visible="false" OnClick="btnCerrarDireccion_Click">
                                Cerrar
                                <span class="glyphicon glyphicon-remove"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorDireccion" Text="" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <asp:TextBox ID="uidDireccion" runat="server" CssClass="hidden" />
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>País</h6>
                                <asp:DropDownList ID="ddPais" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddPais_SelectedIndexChanged" placeholder="País" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Estado</h6>
                                <asp:DropDownList ID="ddEstado" runat="server" CssClass="form-control" placeholder="Estado" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpMunicipio" runat="server" CssClass="form-group">
                                    <h6>Municipio</h6>
                                    <asp:TextBox ID="txtMunicipio" MaxLength="30" runat="server" CssClass="form-control" placeholder="Municipio" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpCiudad" runat="server" CssClass="form-group">
                                    <h6>Ciudad</h6>
                                    <asp:TextBox ID="txtCiudad" MaxLength="30" runat="server" CssClass="form-control" placeholder="Ciudad" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpColonia" runat="server" CssClass="form-group">
                                    <h6>Colonia</h6>
                                    <asp:TextBox ID="txtColonia" MaxLength="30" runat="server" CssClass="form-control" placeholder="Colonia" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpCalle" runat="server" CssClass="form-group">
                                    <h6>Calle</h6>
                                    <asp:TextBox ID="txtCalle" MaxLength="20" runat="server" CssClass="form-control" placeholder="Calle" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpConCalle" runat="server" CssClass="form-group">
                                    <h6>Con Calle</h6>
                                    <asp:TextBox ID="txtConCalle" MaxLength="20" runat="server" CssClass="form-control" placeholder="Con Calle" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpYCalle" runat="server" CssClass="form-group">
                                    <h6>Y Calle</h6>
                                    <asp:TextBox ID="txtYCalle" MaxLength="20" runat="server" CssClass="form-control" placeholder="Y Calle" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpNoExt" runat="server" CssClass="form-group">
                                    <h6>No. Exterior</h6>
                                    <asp:TextBox ID="txtNoExt" MaxLength="15" runat="server" CssClass="form-control" placeholder="No. Exterior" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>No. Interior</h6>
                                <asp:TextBox ID="txtNoInt" MaxLength="15" runat="server" CssClass="form-control" placeholder="No. Interior" />
                            </div>
                            <div class="col-md-8 col-sm-12 col-xs-12">
                                <h6>Referencia</h6>
                                <asp:TextBox ID="txtReferencia" runat="server" CssClass="form-control" placeholder="Referencia" />
                            </div>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>
        <!-- panel derecho-->
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Gestion de Encargados
                </div>
                <div class="text-left">
                    <asp:LinkButton ID="btnNuevo" OnClick="btnNuevo_ActivarCampos" CssClass="btn btn-sm btn-default" runat="server">
                        <span class="glyphicon glyphicon-file"></span>
                        Nuevo
                    </asp:LinkButton><asp:LinkButton ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-sm disabled btn-default" runat="server">
                        <span class="glyphicon glyphicon-cog"></span>
                        Editar
                    </asp:LinkButton><asp:LinkButton ID="btnEmpresa" OnClick="btnEmpresa_Click" CssClass="btn btn-sm btn-default" Visible="false" runat="server">
                        Empresa
                    </asp:LinkButton><asp:LinkButton ID="btnAceptar" CssClass="btn btn-sm btn-success" OnClick="btnAceptar_Click" runat="server">
                        <asp:Label ID="lblAccion" CssClass="glyphicon glyphicon-ok" runat="server" />
                    </asp:LinkButton><asp:LinkButton ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-danger" runat="server">
                        <span class="glyphicon glyphicon-remove"></span>
                    </asp:LinkButton>
                    <asp:Label ID="lblMensaje" runat="server" />
                    <asp:Label Style="color: red;" ID="lblErrorUsuario" Text="" runat="server" />
                    <asp:TextBox CssClass="hide" ID="txtUidEncargado" runat="server" />
                    <asp:TextBox CssClass="hide btn btn-sm btn-default" ID="txtEmpresa" runat="server" />
                    <div class="pull-right hide">
                        <asp:Label ID="lblEmpresa" runat="server" Text="(ninguna)" />
                    </div>
                </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs">
                        <li class="active" id="activeDatosGenerales" runat="server">
                            <asp:LinkButton ID="tabDatos" runat="server" OnClick="tabDatos_Click" Text="Datos Generales" /></li>
                        <li id="activeSucursales" runat="server">
                            <asp:LinkButton ID="tabSucursales" runat="server" OnClick="tabSucursales_Click" Text="Sucursal" /></li>
                        <li id="activeDirecciones" runat="server">
                            <asp:LinkButton ID="tabDirecciones" OnClick="tabDirecciones_Click" runat="server" Text="Direcciones" /></li>
                        <li id="activeTelefonos" runat="server">
                            <asp:LinkButton ID="tabTelefonos" OnClick="tabTelefonos_Click" runat="server" Text="Teléfonos" /></li>
                        <li id="activeAccesos" runat="server">
                            <asp:LinkButton ID="tabAccesos" OnClick="tabAccesos_Click" runat="server" Text="Accesos" />
                        </li>
                    </ul>
                    <asp:PlaceHolder ID="PanelDatosGeneralesUsuario" runat="server">

                        <div class="row">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <h6>Foto</h6>
                                        <asp:Image runat="server" CssClass="img img-thumbnail" ID="ImgEncargado" Width="200px" Height="160px" />
                                        <asp:TextBox ID="txtimagen" CssClass="form-control hide" runat="server"></asp:TextBox>
                                        <div>
                                            <label id="lblFotoEncargado" class="btn btn-default btn-file form-control" runat="server">
                                                Seleccionar Foto
                                                <asp:FileUpload ID="FUImagen" CssClass="hide" runat="server" />
                                                <asp:Button ID="btnimagen" CssClass="hide" OnClick="imagen" runat="server" />
                                            </label>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnimagen" />
                                </Triggers>
                            </asp:UpdatePanel>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpNombre" runat="server" CssClass="form-group">
                                    <h6>Nombre</h6>
                                    <asp:TextBox ID="txtNombre" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpApellidoPaterno" runat="server" CssClass="form-group">
                                    <h6>Apellido Paterno</h6>
                                    <asp:TextBox ID="txtApellidoPaterno" placeholder="Apellido Paterno" CssClass="form-control" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpApellidoMaterno" runat="server" CssClass="form-group">
                                    <h6>Apellido Materno</h6>
                                    <asp:TextBox ID="txtApellidoMaterno" placeholder="Apellido Materno" CssClass="form-control" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpFechaNacimiento" runat="server" CssClass="form-group">
                                    <h6>Fecha Nacimiento</h6>
                                    <div class="input-group date extra">
                                        <asp:TextBox ID="txtFechaNacimiento" placeholder="Fecha Nacimiento" CssClass="form-control" runat="server" />
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpFechaInicio" runat="server" CssClass="form-group">
                                    <h6>Fecha Inicio</h6>
                                    <div class="input-group date extra">
                                        <asp:TextBox ID="txtFechaInicio" placeholder="Fecha Inicio" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpFechaFin" runat="server" CssClass="form-group">
                                    <h6>Fecha de terminación</h6>
                                    <div class="input-group date extra">
                                        <asp:TextBox ID="txtFechaFin" placeholder="Fecha de terminación" CssClass="form-control" runat="server"></asp:TextBox>
                                        <span class="input-group-addon input-sm ">
                                            <i class="glyphicon glyphicon-calendar"></i>
                                        </span>
                                    </div>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpUsuario" runat="server" CssClass="form-group">
                                    <h6>Usuario</h6>
                                    <asp:TextBox ID="txtUsuario" placeholder="Usuario" CssClass="form-control" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpPassword" runat="server" CssClass="form-group">
                                    <h6>Contraseña</h6>
                                    <asp:TextBox ID="txtPassword" placeholder="Contraseña" CssClass="form-control" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpCorreo" runat="server" CssClass="form-group">
                                    <h6>Correo</h6>
                                    <asp:TextBox ID="txtCorreo" placeholder="Correo Electronico" CssClass="form-control" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4">
                                <h6>Perfil</h6>
                                <asp:DropDownList ID="DdPerfil" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4">
                                <h6>Status</h6>
                                <asp:DropDownList ID="DdStatus" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <%-- <div class="col-md-4">
                                <h6>Sucursal</h6>
                                <asp:DropDownList ID="DdSucursal" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="col-md-4 hide">
                                <asp:Panel ID="Panel1" runat="server" CssClass="form-group">
                                    <h6>Empresa</h6>
                                    <asp:TextBox ID="txtEncargadoEmpresa" CssClass="form-control" placeholder="Nombre" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>--%>
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="PanelSucursales" runat="server" Visible="false">
                        <div class="row">
                            <div class="col-xs-12 text-right">
                                <asp:LinkButton ID="btnBuscarSucursal" runat="server" CssClass="btn btn-sm disabled btn-default" OnClick="btnBuscarSucursal_Click">
                                    Buscar Sucursal
                                </asp:LinkButton>
                            </div>
                            <asp:TextBox CssClass="hide" ID="txtUidSucursal" runat="server" />
                            <asp:HiddenField ID="HiddenField1" runat="server" />
                            <div class="col-xs-12">
                                <asp:Label ID="lblsucursal" runat="server"></asp:Label>
                            </div>
                            <div class="col-xs-4">
                                <h6>Nombre</h6>
                                <asp:TextBox ID="txtNombreSucursal" runat="server" CssClass="form-control disabled" />
                            </div>
                            <div class="col-xs-4">
                                <h6>Tipo</h6>
                                <asp:TextBox ID="txtTipoSucursal" runat="server" CssClass="form-control disabled" />
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder Visible="false" ID="userGrid" runat="server">
                        <asp:GridView ID="dgvSucursales" runat="server" AllowPaging="true" CssClass="table table-bordered table-responsive" OnSelectedIndexChanged="dgvSucursales_SelectedIndexChanged" OnRowDataBound="dgvSucursales_RowDataBound" DataKeyNames="UidSucursal" AutoGenerateColumns="false">
                            <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                            <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                            <EmptyDataTemplate>
                                No existen encargados que coincidan con la búsqueda.
                            </EmptyDataTemplate>
                            <Columns>
                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                <asp:BoundField DataField="StrNombre" HeaderText="Nombre" />
                                <asp:BoundField DataField="StrTipoSucursal" HeaderText="Tipo de sucursal" />
                            </Columns>
                        </asp:GridView>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder ID="panelDirecciones" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnAgregarDireccion" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnAgregarDireccion_Click">
                                        Nuevo
                                        <span class="glyphicon glyphicon-file"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEditarDireccion" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarDireccion_Click">
                                        Editar
                                        <span class="glyphicon glyphicon-edit"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminarDireccion" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnEliminarDireccion_Click">
                                        Eliminar
                                        <span class="glyphicon glyphicon-trash"></span>
                                    </asp:LinkButton>


                                </div>
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblAceptarEliminarDireccion" runat="server" />
                                    <asp:LinkButton ID="btnAceptarEliminarDireccion" CssClass="btn btn-sm btn-success" OnClick="btnAceptarEliminarDireccion_Click" runat="server">
                                        <asp:Label ID="Label2" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarDireccion" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarDireccion_Click">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:GridView ID="dgvDirecciones" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidDireccion" OnRowDataBound="dgvDirecciones_RowDataBound" OnSelectedIndexChanged="dgvDirecciones_SelectedIndexChanged">
                                    <EmptyDataTemplate>No hay direcciones asignadas a está empresa</EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="StrCiudad" HeaderText="Ciudad" />
                                        <asp:BoundField DataField="StrCalle" HeaderText="En Calle" />
                                        <asp:BoundField DataField="StrConCalle" HeaderText="Con Calle" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelTelefonos" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnAgregarTelefono" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="btnAgregarTelefono_Click">
                                        <span class="glyphicon glyphicon-plus"></span>
                                        Nuevo
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEditarTelefono" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarTelefono_Click">
                                        <span class="glyphicon glyphicon-edit"></span>
                                        Editar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminarTelefono" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="btnEliminarTelefono_Click">
                                        <span class="glyphicon glyphicon-trash"></span>
                                        Eliminar
                                    </asp:LinkButton>



                                    <asp:LinkButton ID="btnOKTelefono" runat="server" Enabled="false" CssClass="btn btn-success btn-sm disabled hidden" OnClick="btnOKTelefono_Click">
                                        <span class="glyphicon glyphicon-ok"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelarTelefono" runat="server" Enabled="false" CssClass="btn btn-danger btn-sm disabled hidden" OnClick="btnCancelarTelefono_Click">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblAceptarEliminarTelefono" runat="server" />
                                    <asp:LinkButton ID="btnAceptarEliminarTelefono" CssClass="btn btn-sm btn-success" runat="server" OnClick="btnAceptarEliminarTelefono_Click">
                                        <asp:Label ID="Label3" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarTelefono" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarTelefono_Click">
                            <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorTelefono" Text="" runat="server" />
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <asp:TextBox ID="uidTelefono" runat="server" CssClass="hidden disabled" />
                            <div class="col-xs-12 col-md-6">
                                <asp:DropDownList ID="ddTipoTelefono" runat="server" CssClass="form-control" Enabled="false" />
                                <asp:ListBox Visible="false" ID="lbTipoTelefono" runat="server" SelectionMode="Multiple" CssClass="form-control" />
                            </div>
                            <div class="col-xs-12 col-md-6">
                                <asp:Panel ID="frmGrpTelefono" runat="server" CssClass="form-group">
                                    <asp:TextBox ID="txtTelefono" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Teléfono" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:GridView ID="dgvTelefonos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidTelefono" OnRowDataBound="dgvTelefonos_RowDataBound" OnSelectedIndexChanged="dgvTelefonos_SelectedIndexChanged">
                                    <EmptyDataTemplate>No hay teléfonos asignados a este encargado</EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                        <asp:BoundField DataField="StrTipoTelefono" HeaderText="Tipo" />
                                        <asp:BoundField DataField="StrTelefono" HeaderText="Ciudad" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                    <asp:PlaceHolder runat="server" ID="panelAccesos" Visible="false">
                        <div class="row">
                            <div class="col-xs-12">
                                <h4>Lista de módulos autorizados</h4>
                            </div>
                        </div>
                        <ul class="nav nav-tabs">
                            <li class="active" id="activeBackside" runat="server">
                                <asp:LinkButton ID="tabBackside" runat="server" OnClick="tabBackside_Click" Text="Backsite" /></li>
                            <li id="activeBackend" runat="server">
                                <asp:LinkButton ID="tabBackend" runat="server" OnClick="tabBackend_Click" Text="Backend" /></li>
                            <li id="activeFrontend" runat="server">
                                <asp:LinkButton ID="tabFrontend" OnClick="tabFrontend_Click" runat="server" Text="Frontend" /></li>
                        </ul>
                        <asp:PlaceHolder runat="server" ID="accesoBackside" Visible="true">
                            <div class="row">
                                <asp:PlaceHolder runat="server" ID="modulosBackside" EnableViewState="true" />
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="accesoBackend" Visible="false">
                            <div class="row">
                                <asp:PlaceHolder runat="server" ID="modulosBackend" EnableViewState="true" />
                            </div>
                        </asp:PlaceHolder>
                        <asp:PlaceHolder runat="server" ID="accesoFrontend" Visible="false">
                            <div class="row">
                                <asp:PlaceHolder runat="server" ID="modulosFrontend" EnableViewState="true" />
                            </div>
                        </asp:PlaceHolder>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaAll() {
            $('.input-group.date.extra').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            });
        }
        //]]>
    </script>
    <script>

        function upload(FileUpload1) {
            if (FileUpload1.value != '') {
                document.getElementById("<% = btnimagen.ClientID  %>").click();
            }
        }
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaF2() {
            $('#fechaF2').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            });
        }
        //]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaF() {
            $('#fechaF').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            }).on('changeDate', function (ev) {
                var fecha2 = $('.fechaF2');
                if (fecha2.val().length == 0)
                    fecha2.val($(".fechaF").val());
            });
        }
        //]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaI2() {
            $('#fechaI2').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            });
        }//]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaI() {
            $('#fechaI').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            }).on('changeDate', function (ev) {
                var fecha2 = $('.fechaI2');
                if (fecha2.val().length == 0)
                    fecha2.val($(".fechaI").val());
            });
        }//]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaN2() {
            $('#fechaN2').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            });
        }//]]>
    </script>
    <script type="text/javascript">
        //<![CDATA[
        function prepareFechaN() {
            $('#fechaN').datepicker({
                autoclose: true,
                todayHighlight: true,
                language: 'es',
                format: 'dd/mm/yyyy',
                clearBtn: true,
                todayBtn: true,
            }).on('changeDate', function (ev) {
                var fecha2 = $('.fechaN2');
                if (fecha2.val().length == 0)
                    fecha2.val($(".fechaN").val());
            });
        }//]]>
    </script>
    <script>
        //<![CDATA[
        function pageLoad() {
            prepareFechaN();
            prepareFechaN2();
            prepareFechaI();
            prepareFechaI2();
            prepareFechaF();
            prepareFechaF2();

            prepareFechaAll();
        }//]]>

    </script>
    <style>
        .btn-file {
            position: relative;
            overflow: hidden;
        }

            .btn-file input[type=file] {
                position: absolute;
                top: 0;
                right: 0;
                min-width: 100%;
                min-height: 100%;
                font-size: 100px;
                text-align: right;
                filter: alpha(opacity=0);
                opacity: 0;
                outline: none;
                background: white;
                cursor: inherit;
                display: block;
            }
    </style>
</asp:Content>

