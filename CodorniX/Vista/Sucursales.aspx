<%@ Page Title="" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="Sucursales.aspx.cs" Inherits="CodorniX.Vista.Sucursales" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Sucursales</title>
    <link href="../Content/bootstrap-datepicker3.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
        <div class="col-md-5">
            <!-- Panel principal: Sucursales -->
            <asp:PlaceHolder ID="panelSucursal" runat="server">
                <div class="panel panel-primary">
                    <div class="panel-heading text-center">
                        Sucursales
                    </div>
                    <!-- Botones de busqueda --><!---Manuel se la come-->
                    <div class="btn-toolbar">
                        <div class="btn-group pull-right">
                            <asp:LinkButton ID="btnMostrarBusqueda" runat="server" Text="Mostrar" CssClass="btn btn-default btn-sm" OnClick="btnMostrarBusqueda_Click" />
                            <asp:LinkButton ID="btnBorrarBusqueda" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnBorrarBusqueda_Click">
                                Borrar
                                <span class="glyphicon glyphicon-refresh"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnBuscar_Click">
                                Buscar
                                <span class="glyphicon glyphicon-search"></span>
                            </asp:LinkButton>
                        </div>
                    </div>
                    <div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                                 <asp:Label ID="LbErrorSucursales" Text="" runat="server" Visible="false" ForeColor="Red"  />
                            </div>
                     </div>
                    <div class="panel-body">
                        <asp:PlaceHolder ID="seccionBusqueda" runat="server" Visible="true">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Nombre</h6>
                                    <asp:TextBox ID="txtBusquedaNombre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Nombre Comercial" />
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Fecha Inicio:</h6>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtBusquedaRegistradoDespues" runat="server" CssClass="form-control" placeholder="Día/Mes/Año" />
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Fecha Fin:</h6>
                                    <div class="input-group date">
                                        <asp:TextBox ID="txtBusquedaRegistradoAntes" runat="server" CssClass="form-control datepicker" placeholder="Día/Mes/Año" />
                                        <div class="input-group-addon">
                                            <span class="glyphicon glyphicon-calendar"></span>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Tipo</h6>
                                    <asp:ListBox ID="lbTipoSucursal" runat="server" CssClass="form-control" />
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Status</h6>
                                    <asp:ListBox ID="lbStatus" runat="server" CssClass="form-control" />
                                </div>
                            </div>
                        </asp:PlaceHolder>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-lg-12">
                                <asp:GridView ID="dgvSucursales" runat="server" PageSize="10" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="dgvSucursales_RowDataBound" OnSelectedIndexChanged="dgvSucursales_SelectedIndexChanged" DataKeyNames="UidSucursal" OnSorting="dgvSucursales_Sorting" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="dgvSucursales_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        No hay sucursales registradas
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="StrNombre" HeaderText="Nombre" SortExpression="Nombre" />
                                        <asp:BoundField DataField="StrTipoSucursal" HeaderText="Tipo de Sucursal" SortExpression="TipoSucursal" />
                                        <asp:BoundField DataField="DtFechaRegistro" HeaderText="Fecha de Registro" DataFormatString="{0:d}" HtmlEncode="false" SortExpression="FechaRegistro" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
            <!-- Panel secundario: Direccion -->
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
                         <div id="PnErrorDirIzSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorDirIz" Text="" runat="server"  ForeColor="Red"  />
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

        <div class="col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Sucursal
                </div>
                <div class="btn-toolbar">
                    <div class="btn-group pull-left">
                        <asp:LinkButton ID="btnNuevaSucursal" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnNuevaSucursal_Click">
                            <span class="glyphicon glyphicon-file"></span>
                            Nuevo
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnEditarSucursal" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarSucursal_Click">
                            <span class="glyphicon glyphicon-edit"></span>
                            Editar
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnOkSucursal" runat="server" CssClass="btn btn-success btn-sm disabled hidden" OnClick="btnOkSucursal_Click">
                            <span class="glyphicon glyphicon-ok"></span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancelarSucursal" runat="server" CssClass="btn btn-danger btn-sm disabled hidden" OnClick="btnCancelarSucursal_Click">
                            <span class="glyphicon glyphicon-remove"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="pull-right">
                        <asp:LinkButton ID="btnEncargados" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnEncargados_Click">
                                Encargados
                                <span class="glyphicon glyphicon-user"></span>
                            </asp:LinkButton>
                    </div>
                </div>
                <div id="PnErrorSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px; margin: 0px;" >
                    <div class="col-md-10 col-sm-10 col-xs-10">
                       <asp:Label ID="lblErrorSucursal" Text="" runat="server"  ForeColor="Red"  />
                    </div>
                 </div>
                <div class="panel-body">
                    <ul class="nav nav-tabs">
                        <li class="active" id="activeDatos" runat="server">
                            <asp:LinkButton ID="tabDatos" runat="server" Text="Datos" OnClick="tabDatos_Click" /></li>
                        <li id="activeDirecciones" runat="server">
                            <asp:LinkButton ID="tabDirecciones" runat="server" Text="Direcciones" OnClick="tabDirecciones_Click" /></li>
                        <li id="activeTelefonos" runat="server">
                            <asp:LinkButton ID="tabTelefonos" runat="server" Text="Teléfonos" OnClick="tabTelefonos_Click" /></li>
                        <li id="activeImpresoras" runat="server">
                            <asp:LinkButton ID="tabImpresoras" runat="server" Text="Impresoras" OnClick="tabImpresoras_Click" /></li>
                        <li id="activeFotografias" runat="server">
                            <asp:LinkButton ID="tabFotografias" runat="server" Text="Foto tramite" OnClick="tabFotografias_Click" /></li>
                         <li id="activePapel" runat="server">
                            <asp:LinkButton ID="tabPapel" runat="server" Text="Papel tramite" OnClick="tabPapel_Click" /></li>
                        <li id="activeFotografiasC" runat="server">
                            <asp:LinkButton ID="tabFotografiasC" runat="server" Text="Foto comercial" OnClick="tabFotografiasC_Click"/></li>
                         <li id="activePapelC" runat="server">
                            <asp:LinkButton ID="tabPapelC" runat="server" Text="Papel comercial" OnClick="tabPapelC_Click" /></li>
                        <li id="activeLicencias" runat="server">
                            <asp:LinkButton ID="tabLicencias" runat="server" Text="Licencias" OnClick="tabLicencias_Click" /></li>
                        <li id="activeServidor" runat="server">
                            <asp:LinkButton ID="tabServidor" runat="server" Text="Servidor" OnClick="tabServidor_Click" /></li>
                    </ul>

                    <asp:PlaceHolder ID="panelDatosSucursal" Visible="true" runat="server">
                        
                        <div class="row" style="padding-top: 10px;">
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <div class="col-md-4">
                                        <h6>Foto</h6>
                                        <asp:Image runat="server" CssClass="img img-thumbnail" ID="ImgSucursales" Width="200px" Height="160px" />
                                         <asp:TextBox ID="txtimagen" CssClass="form-control hide" runat="server"></asp:TextBox>
                                        <div>
                                            <label ID="lblFotoSucursal" class="btn btn-default btn-file form-control" runat="server">
                                                Escoger Foto
                                                <asp:FileUpload ID="FUImagen" CssClass="hide" runat="server" />
                                                <asp:Button ID="btnimagen" CssClass="hide" OnClick="imagen"  runat="server" />
                                            </label>
                                        </div>
                                    </div>
                                </ContentTemplate>
                                <Triggers>
                                    <asp:PostBackTrigger ControlID="btnimagen"  />
                                </Triggers>
                            </asp:UpdatePanel>
                            <asp:TextBox ID="uidSucursal" runat="server" CssClass="hidden disabled" />
                            <asp:Panel ID="frmGrpNombre" runat="server" CssClass="form-group" ></asp:Panel>
                            <div class="col-md-4 col-sm-4 col-xs-12">
                                
                                    <h6 class="control-label">Nombre</h6>
                                    <asp:TextBox ID="txtNombre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Nombre Comercial" />
                                
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12">
                                <h6>Tipo</h6>
                                <asp:DropDownList ID="ddTipoSucursal" runat="server" CssClass="form-control" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Fecha de registro</h6>
                                <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control datepicker" placeholder="Día/Mes/Año" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Status:</h6>
                                <asp:DropDownList ID="ddActivoSucursal" runat="server" CssClass="form-control" Enabled="false" >
                                </asp:DropDownList>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelDirecciones" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="btn-group text-left">
                                    <asp:LinkButton ID="btnAgregarDireccion" runat="server" CssClass="btn btn-default btn-sm disabled" Visible="true" OnClick="btnAgregarDireccion_Click">
                                        Nuevo
                                        <span class="glyphicon glyphicon-file"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEditarDireccion" runat="server" CssClass="btn btn-default btn-sm disabled" Visible="false" OnClick="btnEditarDireccion_Click">
                                        Editar
                                        <span class="glyphicon glyphicon-edit"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminarDireccion" runat="server" CssClass="btn btn-default btn-sm disabled" Visible="false" OnClick="btnEliminarDireccion_Click">
                                        Eliminar
                                        <span class="glyphicon glyphicon-trash"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnObtenerDireccionEmpresa" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnObtenerDireccionEmpresa_Click">
                                        Obtener de Empresa
                                    </asp:LinkButton>
                                </div>
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblAceptarEliminarDireccion" runat="server" />
                                    <asp:LinkButton ID="btnAceptarEliminarDireccion" OnClick="btnAceptarEliminarDireccion_Click" CssClass="btn btn-sm btn-success" runat="server">
                                        <asp:Label ID="Label2" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelarEliminarDireccion" OnClick="btnCancelarEliminarDireccion_Click" CssClass="btn btn-sm btn-danger" runat="server">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="PnErrorDirDeSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorDirDe" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                        </div>
                        <asp:PlaceHolder ID="panelSeleccionDireccion" runat="server" Visible="false">
                            <div class="row">
                                <div class="col-xs-12">
                                    Seleccione una dirección de la empresa.
                                </div>
                                <div class="col-xs-12">
                                    <asp:DropDownList ID="ddDireccionesEmpresa" runat="server" CssClass="form-control" OnSelectedIndexChanged="ddDireccionesEmpresa_SelectedIndexChanged" />
                                </div>
                            </div>
                        </asp:PlaceHolder>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:GridView ID="dgvDirecciones" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidDireccion" OnRowDataBound="dgvDirecciones_RowDataBound" OnSelectedIndexChanged="dgvDirecciones_SelectedIndexChanged">
                                    <EmptyDataTemplate>No hay direcciones asignadas a está sucursal</EmptyDataTemplate>
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
                                        <asp:Label ID="Label1" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarTelefono" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarTelefono_Click">
                            <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                         <div id="PnErrorTelefonoSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorTelefono" Text="" runat="server"  ForeColor="Red"  />
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
                                    <asp:TextBox ID="txtTelefono" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 9841233234" />
                                </asp:Panel>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:GridView ID="dgvTelefonos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidTelefono" OnRowDataBound="dgvTelefonos_RowDataBound" OnSelectedIndexChanged="dgvTelefonos_SelectedIndexChanged1">
                                    <EmptyDataTemplate>No hay teléfonos asignados a está sucursal</EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                        <asp:BoundField DataField="StrTipoTelefono" HeaderText="Tipo" />
                                        <asp:BoundField DataField="StrTelefono" HeaderText="Ciudad" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelImpresoras" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnAgregarImpresora" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="btnAgregarImpresora_Click">
                                        <span class="glyphicon glyphicon-plus"></span>
                                        Nuevo
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEditarImpresora" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarImpresora_Click">
                                        <span class="glyphicon glyphicon-edit"></span>
                                        Editar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEliminarImpresora" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="btnEliminarImpresora_Click">
                                        <span class="glyphicon glyphicon-trash"></span>
                                        Eliminar
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnOKImpresora" runat="server" Enabled="false" CssClass="btn btn-success btn-sm disabled hidden" OnClick="btnOkImpresora_Click">
                                        <span class="glyphicon glyphicon-ok"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelarImpresora" runat="server" Enabled="false" CssClass="btn btn-danger btn-sm disabled hidden" OnClick="btnCancelarImpresora_Click">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblAceptarEliminarImpresora" runat="server" />
                                    <asp:LinkButton ID="btnAceptarEliminarImpresora" CssClass="btn btn-sm btn-success" runat="server" OnClick="btnAceptarEliminarImpresora_Click">
                                        <asp:Label ID="Label4" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarImpresora" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarImpresora_Click">
                                          <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div id="PnErrorImpresoraSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorImpresora" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <asp:TextBox ID="uidImpresora" runat="server" CssClass="hidden disabled" />
                            <div class="col-xs-12 col-md-12">
                                <h6 class="control-label">Descripcion:</h6>
                                <%--<asp:Panel ID="frmGrpDescripcionImpresora" runat="server" CssClass="form-group">--%>
                                    <asp:TextBox ID="txtDescripcionImpresora" MaxLength="100" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: EPSON-420-K16" />
                                 <%--</asp:Panel>--%>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12 col-md-6">
                                <h6 class="control-label">Marca:</h6>
                                <%--<asp:Panel ID="frmGrpMarca" runat="server" CssClass="form-group">--%>
                                    <asp:TextBox ID="txtMarca" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: EPSON" />
                                <%-- </asp:Panel>--%>
                            </div>
                            <div class="col-xs-12 col-md-6">
                                <h6 class="control-label">Modelo:</h6>
                                <%--<asp:Panel ID="frmGrpModelo" runat="server" CssClass="form-group">--%>
                                   <asp:TextBox ID="txtModelo" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 420" />
                                <%--</asp:Panel>--%>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12 col-md-6">
                                <h6 class="control-label">Tipo de impresora:</h6>
                                <asp:DropDownList ID="ddTipoImpresora" runat="server" CssClass="form-control" Enabled="false">
                                    
                                </asp:DropDownList>
                                <asp:ListBox Visible="false" ID="lbTipoImpresora" runat="server" SelectionMode="Multiple" CssClass="form-control" />
                            </div>
                            <div class="col-xs-12 col-md-6">
                                <h6 class="control-label">Status:</h6>
                                <asp:DropDownList ID="ddActivo" runat="server" CssClass="form-control" Enabled="false" >
                                </asp:DropDownList>
                                <asp:ListBox Visible="false" ID="lbActivo" runat="server" SelectionMode="Multiple" CssClass="form-control" />
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:GridView ID="dgvImpresoras" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidImpresora" OnRowDataBound="dgvImpresoras_RowDataBound" OnSelectedIndexChanged="dgvImpresoras_SelectedIndexChanged">
                                    <EmptyDataTemplate>No hay Impresoras asignados a está sucursal</EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                        <asp:BoundField DataField="StrMarca" HeaderText="Marca" />
                                        <asp:BoundField DataField="StrModelo" HeaderText="Modelo" />
                                        <asp:BoundField DataField="StrTipoImpresora" HeaderText="Tipo" />
                                        <asp:BoundField DataField="StrStatus" HeaderText="Activo" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelFotos" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-xs-12">
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnAgregarFoto" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="ModoAgregacionFotos">
                                        <span class="glyphicon glyphicon-plus"></span>
                                        Nuevo
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEditarFoto" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" data-toggle="modal" data-target="#VConfimacionEditarFoto"  >
                                        <span class="glyphicon glyphicon-edit"></span>
                                        Editar
                                    </asp:LinkButton>
                                    
                                    <asp:LinkButton ID="btnOKFoto" runat="server" Enabled="false" CssClass="btn btn-success btn-sm disabled hidden" OnClick="OKActualizacionFotos">
                                        <span class="glyphicon glyphicon-ok"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelarFoto" runat="server" Enabled="false" CssClass="btn btn-danger btn-sm disabled hidden"  OnClick="CancelarActualizacionFotos">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblAceptarEliminarFoto" runat="server" />
                                    <asp:LinkButton ID="btnAceptarEliminarFoto" CssClass="btn btn-sm btn-success" runat="server" OnClick="btnAceptarEliminarFoto_Click">
                                        <asp:Label ID="Label5" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarFoto" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarFoto_Click">
                                      <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h4>Datos generales de las fotografias para tramite</h4>
                            </div>
                         </div>
                        
                        <div id="PnErrorFotoSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorFoto" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                        </div>
                        <div class="panel-body">

                            <div  class="row panel  col-md-15 col-sm-18 col-xs-36">
                                <asp:TextBox ID="uidFoto" runat="server" CssClass="hidden disabled" />
                                <div class="col-md-3 col-sm-4 col-xs-12" >
                                    <h6 class="control-label">Impresora:</h6>
                                    <asp:DropDownList ID="ddImpresoraFoto" runat="server" CssClass="form-control" Enabled="false" ></asp:DropDownList>
                                </div>
                                 <div class="col-md-3 col-sm-4 col-xs-12">
                                        <h6 class="control-label">Descripcion:</h6>
                                        <asp:TextBox ID="txtDescripcionFoto" CausesValidation="false" MaxLength="100" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: Infantil" />
                                </div>
                                <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio:</h6>
                                       <a id="ToolPrecioFoto" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                       <asp:TextBox ID="txtPrecioFoto" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 60" />
                                 </div>
                                 <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio ticket:</h6>
                                       <a id="ToolPrecioTicket" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                       <asp:TextBox ID="txtPrecioFotoTicket" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 80" />
                                 </div>
                                 <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio servidor:</h6>
                                        <a id="ToolPrecioServidor" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                       <asp:TextBox ID="txtPrecioFotoServidor" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 80" />
                                 </div>
                                <div class="col-md-3 col-sm-4 col-xs-12">
                                            <h6 >Status:</h6>
                                            <asp:DropDownList ID="ddActivoFoto" runat="server" CssClass="form-control" Enabled="false" >

                                            </asp:DropDownList>
                                 </div>
                                <div class="col-md-3 col-sm-4 col-xs-12">
                                            <h6 >Unidad de medida:</h6>
                                            <asp:DropDownList ID="ddMedidaFoto" runat="server" CssClass="form-control" Enabled="false" >
                                            </asp:DropDownList>
                                </div>
                            </div>
                      
                            <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                                  <div class="btn-primary text-center"  >
                                                      <label    style="padding-right:100px; padding-left:100px; font-weight:lighter; ">Medidas impresora</label>
                                                      <label   style="padding-left:100px; padding-right:100px; font-weight:lighter;">Medidas descripcion</label>
                                                  </div>
                                               
                                                  <div  class=" panel-body ">
                                                       <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <h6 >Alto:</h6>
                                                                <a id="ToolAltoFoto" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                                <asp:TextBox ID="txtAltoFoto" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.8" />
                                                        </div>
                                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                                               <h6 >Ancho:</h6>
                                                               <a id="ToolAnchoFoto" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                               <asp:TextBox ID="txtAnchoFoto" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.4" />
                                                        </div>
                                              
                                                       <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <h6 >Alto:</h6>
                                                                <a id="ToolAltoFotoDesc" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                                <asp:TextBox ID="txtAltoFotoDesc" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 3" />
                                                        </div>
                                                        <div class="col-md-3 col-sm-6 col-xs-12">
                                                               <h6 >Ancho:</h6>
                                                               <a id="ToolAnchoFotoDesc" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                               <asp:TextBox ID="txtAnchoFotoDesc" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.5" />
                                                        </div>
                                                  </div>
                            </div>
                       
                            <div class="row" style="padding-top: 10px;">
                                <div class="col-xs-12">
                                    <asp:GridView ID="dgvFotos" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidFoto" OnRowDataBound="dgvFotos_RowDataBound" OnSelectedIndexChanged="dgvFotos_SelectedIndexChanged">
                                        <EmptyDataTemplate>No hay fotografias para tramite asignados a está sucursal</EmptyDataTemplate>
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                            <asp:BoundField DataField="StrDescripcion" HeaderText="Descripcion" />
                                            <asp:BoundField DataField="StrPrecio" HeaderText="Precio" />

                                            <asp:BoundField DataField="VchAlto" HeaderText="Altura" />
                                            <asp:BoundField DataField="VchAncho" HeaderText="Ancho" />
                                            <asp:BoundField DataField="VchMedida" HeaderText="Unidad medida" />
                                            <asp:BoundField DataField="StrStatus" HeaderText="Activo" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                           
                        </div>
                    </asp:PlaceHolder>
                    
                    <asp:PlaceHolder ID="panelPapel" Visible="false" runat="server">
                         <asp:TextBox ID="UidPapel" runat="server" CssClass="hidden disabled" />

                         <div class="btn-group pull-left">
                            <asp:LinkButton ID="btnEditarPapel" runat="server" CssClass="btn btn-default btn-sm disabled" data-toggle="modal" data-target="#VConfimacionEditarPapel" >
                                <span class="glyphicon glyphicon-edit"></span>
                                Editar
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnOkPapel" runat="server" CssClass="btn btn-success btn-sm" Visible="false" OnClick="btnOkPapel_Click">
                                <span class="glyphicon glyphicon-ok"></span>
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancelarPapel" runat="server" CssClass="btn btn-danger btn-sm " Visible="false" OnClick="btnCancelarPapel_Click">
                                <span class="glyphicon glyphicon-remove"></span>
                            </asp:LinkButton>
                        </div>
                         
                         <div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h4>Datos generales del papel para tramite</h4>
                            </div>
                         </div>

                         <div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h6>* La unidad de medida obligatoriamente es en milimetros (mm) *</h6>
                            </div>
                         </div>

                         <div id="PnErrorPapelSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorPapel" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                         </div>

                         <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                             <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Nombre papel</h6>
                                <asp:TextBox ID="txtNombrePapel" runat="server"  CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: Carta" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Altura del papel</h6>
                                <a id="ToolAltoPapel" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtAltoPapel" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 270" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Anchura del papel</h6>
                                <a id="ToolAnchoPapel" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtAnchoPapel" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 210" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen superior</h6>
                                 <a id="ToolMSuperior" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenSuperior" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 30" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen inferior</h6>
                                 <a id="ToolMInferior" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenInferior" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 30" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen derecho</h6>
                                 <a id="ToolMDerecho" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenDerecho" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 25" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen izquierdo</h6>
                                 <a id="ToolMIzquierdo" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenIzquierdo" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 25" />
                            </div>
                         </div>

                        <div class="panel panel-primary panel-body ">
                            <asp:TextBox ID="UidFotoPapel" runat="server" CssClass="hidden disabled" />

                             <div class="row">
                                <div class="col-xs-12">
                                     <div class="btn-group">
                                        <%--<asp:LinkButton ID="btnAgregarFotoPapel" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnAgregarFotoPapel_Click">
                                            <span class="glyphicon glyphicon-plus"></span>
                                            Nuevo
                                        </asp:LinkButton>--%>
                                        <asp:LinkButton ID="btnEditarFotoPapel" runat="server"  CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarFotoPapel_Click">
                                            <span class="glyphicon glyphicon-edit"></span>
                                            Editar
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnOKFotoPapel" runat="server" Visible="false" CssClass="btn btn-success btn-sm " OnClick="btnOKFotoPapel_Click">
                                            <span class="glyphicon glyphicon-ok"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnCancelarFotoPapel" runat="server" Visible="false" CssClass="btn btn-danger btn-sm "  OnClick="btnCancelarFotoPapel_Click">
                                            <span class="glyphicon glyphicon-remove"></span>
                                        </asp:LinkButton>
                                    </div>
                                </div>
                             </div>

                             <div class="row" style="padding-top: 10px;">
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                 <h4>Fotografias sobre el papel</h4>
                                </div>
                             </div>

                             <div id="PnErrorFotoPapelSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorFotoPapel" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                             </div>

                             <div class="row" style="padding-top: 15px;">
                                 <div class="col-md-1 col-sm-1 col-xs-1">
                                    <asp:CheckBox ID="CbRotarImagenPapel" runat="server" Enabled="false" CssClass="form-control datepicker" Height="35" Width="35" />
                                 </div>
                                 <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Rotar fotografia sobre el papel</h6>
                                 </div>
                             </div>

                             <div class="row" style="padding-top: 15px;">
                            
                                 <div class="col-md-4 col-sm-6 col-xs-12" >
                                    <h6 class="control-label">Fotografia:</h6>
                                    <asp:DropDownList ID="DdlFoto" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DdlFoto_SelectedIndexChanged">
                                        <asp:ListItem Text="[Seleccionar]"> </asp:ListItem>
                                    </asp:DropDownList>
                                 </div>

                                 <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Cantidad de fotos X Fila</h6>
                                      <a id="ToolFxFila" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                    <asp:TextBox ID="txtFxFila" runat="server" Enabled="false" CssClass="form-control datepicker" placeholder="Ejemplo: 4"  />
                                 </div>

                                 <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Cantidad de fotos X Columna</h6>
                                      <a id="ToolFxColumna" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                    <asp:TextBox ID="txtFxColumna" runat="server" Enabled="false" CssClass="form-control datepicker" placeholder="Ejemplo: 6"  />
                                 </div>
                             </div>

                             <div class="row" style="padding-top: 10px;">
                                <div class="col-xs-12">
                                    <asp:Label ID="lbOrdenFPPor" runat="server" CssClass="hidden" Visible="false"></asp:Label>
                                    <asp:Label ID="lbOrdenFP" runat="server" CssClass="hidden" Visible="false"></asp:Label>
                                    <asp:GridView ID="dvgFotosPapel" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                        DataKeyNames="UidFoto" OnRowDataBound="dvgFotosPapel_RowDataBound"
                                        OnSelectedIndexChanged="dvgFotosPapel_SelectedIndexChanged"  AllowSorting="true"
                                         OnSorting="dvgFotosPapel_Sorting">
                                        <EmptyDataTemplate>No hay fotografias para tramite en papel para tramite asignados a está sucursal</EmptyDataTemplate>
                                        <Columns>
                                            <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                            <%--<asp:BoundField DataField="StrDescripcion" HeaderText="Descripcion" />--%>

                                            <asp:TemplateField>
                                                    <HeaderTemplate>
                                                           <asp:LinkButton ID="LBnOrdenaColonia" runat="server" CommandName="Sort" CommandArgument="Descripcion" >
                                                                    <span runat="server" >Descripcion  <i ID="IcoDescripcionFP" runat="server"  ></i> </span>
                                                           </asp:LinkButton>
                                                  </HeaderTemplate>
                                                  <ItemTemplate>
                                                             <asp:Label ID="LbDirDescripcionFP" runat="server" Text='<%# Bind("StrDescripcion") %>'></asp:Label>
                                                  </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="VchFila" HeaderText="# Foto X Fila" />
                                            <asp:BoundField DataField="VchColumna" HeaderText="# Foto X Columna" />
                                            
                                            <asp:TemplateField HeaderStyle-BackColor="#F3F3F3" ItemStyle-BackColor="#F3F3F3" ItemStyle-HorizontalAlign="Center">
                                                <HeaderTemplate>
                                                  <asp:LinkButton ID="LBnOrdenaStatus_icon" runat="server" CommandName="Sort" CommandArgument="Status">
                                                    <span> Rotar <i id="IcoRotar" runat="server"></i></span>
                                                  </asp:LinkButton>
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:Label ID="lbFotoRotadoPapel_icon" runat="server"><!--nombre id basado en los botones-->
                                                        <span class="glyphicon glyphicon glyphicon-ok"></span>
                                                    </asp:Label>
                                                    <asp:Label ID="lbFotoNoRotadoPapel_icon" runat="server"><!--nombre id basado en los botones-->
                                                        <span class="glyphicon glyphicon glyphicon-remove"></span>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>

                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                     </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelFotosC" Visible="false" runat="server">
                         <div class="row">
                            <div class="col-xs-12">
                                <div class="btn-group">
                                    <asp:LinkButton ID="btnAgregarFotoC" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" OnClick="ModoAgregacionFotosC">
                                        <span class="glyphicon glyphicon-plus"></span>
                                        Nuevo
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnEditarFotoC" runat="server" Enabled="false" CssClass="btn btn-default btn-sm disabled" data-toggle="modal" data-target="#VConfimacionEditarFotoC"  >
                                        <span class="glyphicon glyphicon-edit"></span>
                                        Editar
                                    </asp:LinkButton>
                                    
                                    <asp:LinkButton ID="btnOKFotoC" runat="server" Enabled="false" CssClass="btn btn-success btn-sm disabled hidden" OnClick="OKActualizacionFotosC">
                                        <span class="glyphicon glyphicon-ok"></span>
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelarFotoC" runat="server" Enabled="false" CssClass="btn btn-danger btn-sm disabled hidden"  OnClick="CancelarActualizacionFotosC">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                                <div style="padding-top: 10px;">
                                    <asp:Label ID="lblAceptarEliminarFotoC" runat="server" />
                                    <asp:LinkButton ID="btnAceptarEliminarFotoC" CssClass="btn btn-sm btn-success" runat="server" OnClick="btnAceptarEliminarFotoC_Click">
                                        <asp:Label ID="Label3" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>
                                    <asp:LinkButton ID="btnCancelarEliminarFotoC" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarFotoC_Click">
                                      <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                         <div class="row" style="padding-top: 10px;">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                     <h4>Datos generales de las fotografias comerciales</h4>
                                    </div>
                                 </div>

                         <div id="PnErrorFotoCSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorFotoC" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                         </div>

                         <div class="panel-body">
                             <div  class="row panel  col-md-15 col-sm-18 col-xs-36">
                               <asp:TextBox ID="uidFotoC" runat="server" CssClass="hidden disabled" />
                                  <div class="col-md-3 col-sm-4 col-xs-12" >
                                    <h6 class="control-label">Impresora:</h6>
                                    <asp:DropDownList ID="ddImpresoraFotoC" runat="server" CssClass="form-control" Enabled="false" ></asp:DropDownList>
                                   </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                        <h6 class="control-label">Descripcion:</h6>
                                        <asp:TextBox ID="txtDescripcionFotoC" CausesValidation="false" MaxLength="100" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: Admin1" />
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio:</h6>
                                       <a id="ToolPrecioFotoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                       <asp:TextBox ID="txtPrecioFotoC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 60" />
                                 </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio ticket:</h6>
                                       <a id="ToolPrecioTicketC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                       <asp:TextBox ID="txtPrecioFotoTicketC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 80" />
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio servidor:</h6>
                                        <a id="ToolPrecioServidorC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                       <asp:TextBox ID="txtPrecioFotoServidorC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 80" />
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                            <h6 >Status:</h6>
                                            <asp:DropDownList ID="ddActivoFotoC" runat="server" CssClass="form-control" Enabled="false" >

                                            </asp:DropDownList>
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                            <h6 >Unidad de medida:</h6>
                                            <asp:DropDownList ID="ddMedidaFotoC" runat="server" CssClass="form-control" Enabled="false" >
                                            </asp:DropDownList>
                                 </div>
                             </div>

                             <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                  <div class="btn-primary text-center"  >
                                       <label    style="padding-right:100px; padding-left:100px; font-weight:lighter; ">Medidas impresora</label>
                                       <label   style="padding-left:100px; padding-right:100px; font-weight:lighter;">Medidas descripcion</label>
                                  </div>

                                  <div  class="panel-body">
                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <h6 >Alto:</h6>
                                                                <a id="ToolAltoFotoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                                <asp:TextBox ID="txtAltoFotoC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.8" />
                                      </div>
                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                               <h6 >Ancho:</h6>
                                                               <a id="ToolAnchoFotoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                               <asp:TextBox ID="txtAnchoFotoC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.4" />
                                      </div>

                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <h6 >Alto:</h6>
                                                                <a id="ToolAltoFotoDescC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                                <asp:TextBox ID="txtAltoFotoDescC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 3" />
                                       </div>
                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                               <h6 >Ancho:</h6>
                                                               <a id="ToolAnchoFotoDescC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                                               <asp:TextBox ID="txtAnchoFotoDescC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.5" />
                                      </div>
                                  </div>
                             </div>

                             <div class="row" style="padding-top: 10px;">
                                  <div class="col-xs-12">
                                       <asp:GridView ID="dgvFotosC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidFoto" OnRowDataBound="dgvFotosC_RowDataBound" OnSelectedIndexChanged="dgvFotosC_SelectedIndexChanged">
                                           <EmptyDataTemplate>No hay fotografias comerciales asignados a está sucursal</EmptyDataTemplate>
                                            <Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                <asp:BoundField DataField="StrDescripcion" HeaderText="Descripcion" />
                                                <asp:BoundField DataField="StrPrecio" HeaderText="Precio" />

                                                <asp:BoundField DataField="VchAlto" HeaderText="Altura" />
                                                <asp:BoundField DataField="VchAncho" HeaderText="Ancho" />
                                                <asp:BoundField DataField="VchMedida" HeaderText="Unidad medida" />
                                                <asp:BoundField DataField="StrStatus" HeaderText="Activo" />
                                            </Columns>
                                       </asp:GridView>
                                  </div>
                             </div>
                         </div>
                    </asp:PlaceHolder>

                     <asp:PlaceHolder ID="panelPapelC" Visible="false" runat="server">
                         <asp:TextBox ID="UidPapelC" runat="server" CssClass="hidden disabled" />
                         <div class="btn-group pull-left">
                             <asp:LinkButton ID="btnEditarPapelC" runat="server" CssClass="btn btn-default btn-sm disabled" data-toggle="modal" data-target="#VConfimacionEditarPapelC" >
                                <span class="glyphicon glyphicon-edit"></span>
                                Editar
                             </asp:LinkButton>
                             <asp:LinkButton ID="btnOkPapelC" runat="server" CssClass="btn btn-success btn-sm" Visible="false" OnClick="btnOkPapelC_Click">
                                <span class="glyphicon glyphicon-ok"></span>
                             </asp:LinkButton>
                             <asp:LinkButton ID="btnCancelarPapelC" runat="server" CssClass="btn btn-danger btn-sm " Visible="false" OnClick="btnCancelarPapelC_Click">
                                <span class="glyphicon glyphicon-remove"></span>
                             </asp:LinkButton>
                         </div>

                         <div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h4>Datos generales del papel comercial</h4>
                            </div>
                         </div>

                         <div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h6>* La unidad de medida obligatoriamente es en milimetros (mm) *</h6>
                            </div>
                         </div>

                         <div id="PnErrorPapelCSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorPapelC" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                         </div>

                         <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Nombre papel</h6>
                                <asp:TextBox ID="txtNombrePapelC" runat="server"  CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: Oficio" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Altura del papel</h6>
                                <a id="ToolAltoPapelC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtAltoPapelC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 270" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Anchura del papel</h6>
                                <a id="ToolAnchoPapelC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtAnchoPapelC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 210" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen superior</h6>
                                 <a id="ToolMSuperiorC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenSuperiorC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 30" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen inferior</h6>
                                 <a id="ToolMInferiorC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenInferiorC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 30" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen derecho</h6>
                                 <a id="ToolMDerechoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenDerechoC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 25" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen izquierdo</h6>
                                 <a id="ToolMIzquierdoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                <asp:TextBox ID="txtMargenIzquierdoC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 25" />
                            </div>
                         </div>

                         <div class="panel panel-primary panel-body ">
                              <asp:TextBox ID="UidFotoPapelC" runat="server" CssClass="hidden disabled" />
                              
                               <div class="row">
                                     <div class="col-xs-12">
                                         <div class="btn-group">
                                             <asp:LinkButton ID="btnEditarFotoPapelC" runat="server"  CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarFotoPapelC_Click">
                                                <span class="glyphicon glyphicon-edit"></span>
                                                Editar
                                             </asp:LinkButton>
                                             <asp:LinkButton ID="btnOKFotoPapelC" runat="server" Visible="false" CssClass="btn btn-success btn-sm " OnClick="btnOKFotoPapelC_Click">
                                                <span class="glyphicon glyphicon-ok"></span>
                                             </asp:LinkButton>
                                             <asp:LinkButton ID="btnCancelarFotoPapelC" runat="server" Visible="false" CssClass="btn btn-danger btn-sm "  OnClick="btnCancelarFotoPapelC_Click">
                                                <span class="glyphicon glyphicon-remove"></span>
                                             </asp:LinkButton>
                                         </div>
                                     </div>
                               </div>

                               <div class="row" style="padding-top: 10px;">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <h4>Fotografias sobre el papel</h4>
                                    </div>
                               </div>

                               <div id="PnErrorFotoPapelCSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                         <asp:Label ID="lblErrorFotoPapelC" Text="" runat="server"  ForeColor="Red"  />
                                    </div>
                               </div>

                               <div class="row" style="padding-top: 15px;">
                                 <div class="col-md-1 col-sm-1 col-xs-1">
                                    <asp:CheckBox ID="CbRotarImagenPapelC" runat="server" Enabled="false" CssClass="form-control datepicker" Height="35" Width="35" />
                                 </div>
                                 <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Rotar fotografia sobre el papel</h6>
                                 </div>
                             </div>

                               <div class="row" style="padding-top: 15px;">
                                    <div class="col-md-4 col-sm-6 col-xs-12" >
                                        <h6 class="control-label">Fotografia:</h6>
                                        <asp:DropDownList ID="DdlFotoC" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DdlFotoC_SelectedIndexChanged">
                                            <asp:ListItem Text="[Seleccionar]"> </asp:ListItem>
                                        </asp:DropDownList>
                                     </div>
                                    
                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <h6>Cantidad de fotos X Fila</h6>
                                          <a id="ToolFxFilaC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                        <asp:TextBox ID="txtFxFilaC" runat="server" Enabled="false" CssClass="form-control datepicker" placeholder="Ejemplo: 4"  />
                                     </div>

                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <h6>Cantidad de fotos X Columna</h6>
                                          <a id="ToolFxColumnaC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a>
                                        <asp:TextBox ID="txtFxColumnaC" runat="server" Enabled="false" CssClass="form-control datepicker" placeholder="Ejemplo: 6"  />
                                    </div>
                               </div>

                               <div class="row" style="padding-top: 10px;">
                                    <div class="col-xs-12">
                                        <asp:Label ID="lbOrdenFPPorC" runat="server" CssClass="hidden" Visible="false"></asp:Label>
                                        <asp:Label ID="lbOrdenFPC" runat="server" CssClass="hidden" Visible="false"></asp:Label>
                                         <asp:GridView ID="dvgFotosPapelC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                        DataKeyNames="UidFoto" OnRowDataBound="dvgFotosPapelC_RowDataBound"
                                        OnSelectedIndexChanged="dvgFotosPapelC_SelectedIndexChanged"  AllowSorting="true"
                                         OnSorting="dvgFotosPapelC_Sorting">
                                            <EmptyDataTemplate>No hay fotografias comercial en papel comercial asignados a está sucursal</EmptyDataTemplate>
                                            <Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                <%--<asp:BoundField DataField="StrDescripcion" HeaderText="Descripcion" />--%>

                                                <asp:TemplateField>
                                                      <HeaderTemplate>
                                                               <asp:LinkButton ID="LBnOrdenaColonia" runat="server" CommandName="Sort" CommandArgument="Descripcion" >
                                                                        <span runat="server" >Descripcion  <i ID="IcoDescripcionFP" runat="server"  ></i> </span>
                                                               </asp:LinkButton>
                                                      </HeaderTemplate>
                                                      <ItemTemplate>
                                                                 <asp:Label ID="LbDirDescripcionFP" runat="server" Text='<%# Bind("StrDescripcion") %>'></asp:Label>
                                                      </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="VchFila" HeaderText="# Foto X Fila" />
                                                <asp:BoundField DataField="VchColumna" HeaderText="# Foto X Columna" />
                                            
                                                <asp:TemplateField HeaderStyle-BackColor="#F3F3F3" ItemStyle-BackColor="#F3F3F3" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                      <asp:LinkButton ID="LBnOrdenaStatus_icon" runat="server" CommandName="Sort" CommandArgument="Status">
                                                        <span> Rotar <i id="IcoRotar" runat="server"></i></span>
                                                      </asp:LinkButton>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:Label ID="lbFotoRotadoPapel_icon" runat="server"><!--nombre id basado en los botones-->
                                                            <span class="glyphicon glyphicon glyphicon-ok"></span>
                                                        </asp:Label>
                                                        <asp:Label ID="lbFotoNoRotadoPapel_icon" runat="server"><!--nombre id basado en los botones-->
                                                            <span class="glyphicon glyphicon glyphicon-remove"></span>
                                                        </asp:Label>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                            </Columns>
                                        </asp:GridView>
                                    </div>
                               </div>
                         </div>
                     </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelLicencias" Visible="false" runat="server">
                        <div class="row" style="padding-top: 40px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                            </div>
                            <div class="col-md-8 col-sm-12 col-xs-12">
                                <asp:Panel ID="frmGrpNoLicencias" runat="server" >
                                    <asp:TextBox ID="txtCantMaqLicencia" CausesValidation="false" MaxLength="3" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="No. PC's/ Licencia Seleccionada" />

                                </asp:Panel>
                            </div>

                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpGenerarLicencias" runat="server" >
                                    <asp:LinkButton ID="btnGenerarLicencia" runat="server" Enabled="false" BackColor="Green" ToolTip="Crear una nueva lista de Licencias" CssClass="btn btn-primary btn-sm disabled" OnClick="btnGenerarLicencia_Click">
                                                <span class="glyphicon glyphicon-plus"></span>
                                                <span class="glyphicon glyphicon-asterisk"></span>
                                     </asp:LinkButton>
                                    <asp:LinkButton ID="btnAgregarLicencia" runat="server" Enabled="false" BackColor="LightGreen" ToolTip="Agregar 1 Licencia a la lista" CssClass="btn btn-info btn-sm disabled" OnClick="btnAgregarLicencia_Click">
                                                <span class="glyphicon glyphicon-plus"></span>
                                                1
                                     </asp:LinkButton>
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <asp:Panel ID="frmGrpAgregarLicencia" runat="server" >
                                    
                                </asp:Panel>
                            </div>
                        </div>
                        <div id="PnErrorLicenciaSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorLicencia" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                        </div>
                        <div class="panel panel-info">
                        <asp:Panel ID="Panel1" runat="server" CssClass="panel-body table-responsive">
                        <div class="table-responsive" style="margin:auto auto auto">
                       
                            <asp:GridView 
                                ID="dgvLicencias" 
                                runat="server" 
                                AllowPaging="true"
                                AllowSorting="true"
                                AutoGenerateColumns="false" 
                                CssClass="table table-hover table-bordered table-condensed input-sm table-responsive" 
                                DataKeyNames="UidLicencia" 
                                 EnableViewState="true"
                                OnPageIndexChanging="dgvLicencias_PageIndexChanging"
                                OnRowCommand="dgvLicencias_RowCommand"
                                OnRowDataBound="dvgLicencias_RowDataBound"
                                OnSorting="dgvLicencias_Sorting"
                                 OnSelectedIndexChanged="dgvLicencias_SelectedIndexChanged"
                                >
                                    <EmptyDataTemplate>No hay Licencias asignados a está sucursal</EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" ItemStyle-CssClass="hidden" HeaderStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                        <%--<asp:BoundField DataField="IntNo" HeaderText="No." />
                                        <asp:BoundField DataField="UidLicencia" HeaderText="Licencia" />
                                        <asp:BoundField DataField="BooStatusLicencia" HeaderText="Disponible"/>--%>
                                        <asp:TemplateField>
                                                         <HeaderTemplate>
                                                             <asp:LinkButton ID="LBnOrdenaNo" runat="server" CommandName="Sort" CommandArgument="No">
                                                                    <span> No. <i id="IcoNo" runat="server"></i></span>
                                                             </asp:LinkButton>
                                                         </HeaderTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="LbDirNo" runat="server" Text='<%# Bind("IntNo") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <%--<FooterTemplate>
                                                           <asp:TextBox ID="tbNuevoFootDirColonia" runat="server" ReadOnly="true" CssClass="form-control input-sm"></asp:TextBox>
                                                         </FooterTemplate>--%>
                                        </asp:TemplateField>
                                        <asp:TemplateField >
                                                         <HeaderTemplate>
                                                             <asp:LinkButton ID="LBnOrdenaUidLicencia" runat="server" CommandName="Sort" CommandArgument="Licencia">
                                                                    <span> Licencia <i id="IcoLicencia" runat="server"></i></span>
                                                             </asp:LinkButton>
                                                         </HeaderTemplate>
                                                         <ItemTemplate>
                                                          <asp:LinkButton ID="BtnUidLicencia" OnClientClick="Copiar(this);" runat="server" Text='<%# Bind("UidLicencia") %>' CommandName="Select" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">
                                                             
                                                          </asp:LinkButton>
                                                         </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                                         <HeaderTemplate>
                                                             <asp:LinkButton ID="LBnOrdenaStatusLicencia" runat="server" CommandName="Sort" CommandArgument="StatusLicencia">
                                                                    <span> Disponibilidad <i id="IcoStatusLicencia" runat="server"></i></span>
                                                             </asp:LinkButton>
                                                         </HeaderTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="LbDirStatusLicencia" runat="server" Text='<%# Bind("BooStatusLicencia") %>'></asp:Label>
                                                         </ItemTemplate>
                                        </asp:TemplateField>
                                        
                                        <asp:TemplateField HeaderStyle-BackColor="#F3F3F3" ItemStyle-BackColor="#F3F3F3" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                              <asp:LinkButton ID="LBnOrdenaStatus_icon" runat="server" CommandName="Sort" CommandArgument="Status">
                                                <span> Status <i id="IcoStatus" runat="server"></i></span>
                                              </asp:LinkButton>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="btndvgLicenciasStatusActivar_icon" runat="server"><!--nombre id basado en los botones-->
                                                    <span class="glyphicon glyphicon glyphicon-ok"></span>
                                                </asp:Label>
                                                <asp:Label ID="btndvgLicenciasStatusDesactivar_icon" runat="server"><!--nombre id basado en los botones-->
                                                    <span class="glyphicon glyphicon glyphicon-remove"></span>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderStyle-BackColor="#F3F3F3" ItemStyle-BackColor="#F3F3F3" HeaderStyle-HorizontalAlign="Center" HeaderStyle-VerticalAlign="Top"  ItemStyle-VerticalAlign="Middle" ItemStyle-HorizontalAlign="Center">
                                            <HeaderTemplate>
                                                <asp:Label ID="LBnAccion" runat="server">
                                                    <span> Accion <i id="IcoStatus" runat="server"></i></span>
                                                </asp:Label>
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:LinkButton ID="btndvgLicenciasStatusDesactivar" runat="server" ToolTip="Desactivar Licencia" CssClass="btn btn-danger btn-sm" CommandName="DesactivarLicencia" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">
                                                <span class="glyphicon glyphicon-floppy-remove"></span>
                                                </asp:LinkButton>
                                                <asp:LinkButton ID="btndvgLicenciasStatusActivar" runat="server" ToolTip="Activar Licencia" CssClass="btn btn-primary btn-sm" Visible="false" CommandName="ActivarLicencia" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">
                                                <span class="glyphicon glyphicon-floppy-saved"></span>
                                                </asp:LinkButton>
                                                 <asp:LinkButton ID="btndvgLicenciasEliminar" runat="server" BackColor="Black" ToolTip="Eliminar Licencia"  CssClass="btn btn-primary btn-sm" CommandName="EliminarLicencia" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">
                                                 <span class="glyphicon glyphicon-trash"></span>
                                                 </asp:LinkButton>
                                                 <asp:LinkButton ID="btnRegenerarLicencia" runat="server" ToolTip="Cambiar solo el codigo de la Licencia" CssClass="btn btn-warning btn-sm" CommandName="RegenerarLicencia" CommandArgument="<%#((GridViewRow) Container).RowIndex %>">
                                                   <span class="glyphicon glyphicon-refresh"></span>
                                                 </asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                   
                                    <PagerStyle HorizontalAlign="Center"/>
                                </asp:GridView>
                            </div>
                            </asp:Panel>
                            </div>
                    </asp:PlaceHolder>

                    <asp:PlaceHolder ID="panelServidor" Visible="false" runat="server">
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorServer" Text="" runat="server" />
                            </div>
                        </div>

                        <div class="row"  style="padding-top: 10px;" >
                            <div class="col-md-3 col-sm-4 col-xs-12">
                                    <h6 class="control-label">Nombre ò direccion IP:</h6>
                                    <asp:TextBox ID="txtServidorIp" CausesValidation="false" MaxLength="100" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 192.168.0.1" />
                            </div>
                            <div class="col-md-3 col-sm-4 col-xs-12">
                                    <h6 class="control-label">Puerto:</h6>
                                    <asp:TextBox ID="txtPuerto" CausesValidation="false" MaxLength="100" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2378" />
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div> 
            </div>
        </div>
    </div>
    <div class="modal fade" id="VConfimacionEditarPapel"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabel">Editar papel</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                       Si escoge esta opcion se borrara las medidas de cada foto ¿Esta usted de acuerdo?
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="btnCancelarNuevoPapel" Cssclass="btn btn-secondary" Text="Cancelar"  data-dismiss="modal" />
                                        <asp:Button runat="server" ID="btnConfirmarNuevoPapel" Cssclass="btn btn-primary" Text="Confirmar"  OnClick="btnEditarPapel_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
    <div class="modal fade" id="VConfimacionEditarFoto"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Editar fotografia</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                       Si escoge esta opcion se borrara las medidas de esta foto ¿Esta usted de acuerdo?
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="btnCancelar" Cssclass="btn btn-secondary" Text="Cancelar"  data-dismiss="modal" />
                                        <asp:Button runat="server" ID="Button2" Cssclass="btn btn-primary" Text="Confirmar"  OnClick="btnEditarFoto_Click" />
                                    </div>
                                </div>
                            </div>
     </div>

    <div class="modal fade" id="VConfimacionEditarPapelC"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title" id="exampleModalLabelC">Editar papel</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                       Si escoge esta opcion se borrara las medidas de cada foto ¿Esta usted de acuerdo?
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="Button1" Cssclass="btn btn-secondary" Text="Cancelar"  data-dismiss="modal" />
                                        <asp:Button runat="server" ID="Button3" Cssclass="btn btn-primary" Text="Confirmar"  OnClick="btnEditarPapelC_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
    <div class="modal fade" id="VConfimacionEditarFotoC"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                            <div class="modal-dialog" role="document">
                                <div class="modal-content">
                                    <div class="modal-header">
                                        <h5 class="modal-title">Editar fotografia</h5>
                                        <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                            <span aria-hidden="true">&times;</span>
                                        </button>
                                    </div>
                                    <div class="modal-body">
                                       Si escoge esta opcion se borrara las medidas de esta foto ¿Esta usted de acuerdo?
                                    </div>
                                    <div class="modal-footer">
                                        <asp:Button runat="server" ID="Button4" Cssclass="btn btn-secondary" Text="Cancelar"  data-dismiss="modal" />
                                        <asp:Button runat="server" ID="Button5" Cssclass="btn btn-primary" Text="Confirmar"  OnClick="btnEditarFotoC_Click" />
                                    </div>
                                </div>
                            </div>
     </div>
    <style type="text/css">
        
        .tooltipDemo
        {           
            position: relative;
            display: inline;
            text-decoration: none;
            left: 5px;
            top: 0px;     
        }
        .tooltipDemo::before
        {
            border: solid;
            border-color: transparent #FF8F35;
            border-width: 6px 6px 6px 0px;
            bottom: 21px;
            content: "";
            left: 155px;
            top: 5px;
            position: absolute;
            z-index: 95;          
        }
        .tooltipDemo::after
        {
            background: #FF8F35;
            background: rgb(255, 143, 53);
            border-radius: 5px;
            color: #fff;
            width: 150px;
            left: 160px;
            top: -5px;           
            content:  attr(href) ;
            position: absolute;           
            padding: 5px 15px;          
            z-index: 95;           
        }       

    </style>

    <script  runat="server">

        void CancelarActualizacionFotos(Object sender, EventArgs e)
        {
            //txtDescripcionFoto.Text = "te lo acepto.jpg";
            //RFV_txtDescripcionFoto.Enabled = false;
            btnCancelarFoto_Click( sender,  e);
        }
        void OKActualizacionFotos(Object sender, EventArgs e) {
            //RFV_txtDescripcionFoto.Enabled = true;
            if (Page.IsValid)
            {
                btnOKFoto_Click(sender, e);
            }
        }
        //void ModoEdicionFotos(Object sender, EventArgs e) {


        //    btnEditarFoto_Click(sender, e);
        //    //  RFV_txtDescripcionFoto.Enabled = true;
        //}
        void ModoAgregacionFotos(Object sender, EventArgs e) {
            btnAgregarFoto_Click(sender, e);
        }

        void CancelarActualizacionFotosC(Object sender, EventArgs e)
        {
            btnCancelarFotoC_Click( sender,  e);
        }
        void OKActualizacionFotosC(Object sender, EventArgs e) {
            if (Page.IsValid)
            {
                btnOKFotoC_Click(sender, e);
            }
        }
        void ModoAgregacionFotosC(Object sender, EventArgs e) {
            btnAgregarFotoC_Click(sender, e);
        }
    </script>
    
     <!-- Copiar licencias -->
 <%--<script type="text/javascript" src="https://code.jquery.com/jquery-2.2.4.min.js"></script>--%>
<script>

    //$(document).ready(function () {
    //    AgregarClaseCeldaGrid('dgvLicencias');
    //    $('.tdCopy').click(function () {
    //        CopiarTexto($(this).text());
    //    });
    //});

    //function AgregarClaseCeldaGrid(GridId) {
    //    $('[id*=' + GridId + '] td').each(function () {
    //        $(this).addClass('tdCopy');
    //    });
    //}

    //function CopiarTexto(text) {
    //    var textArea = document.createElement("textarea");
    //    // Si se renderiza por alguna razón
    //    textArea.style.background = 'transparent';
    //    textArea.value = text;
    //    document.body.appendChild(textArea);
    //    textArea.select();
    //    try {
    //        var ok = document.execCommand('copy');
    //        //console.log('La copia del texto es correcta ' + ok);
    //    } catch (err) {
    //        //console.log('No se pudo copiar');
    //    }
    //    document.body.removeChild(textArea);
    //}
    function Copiar(element) {
        //creamos un input que nos ayudara a guardar el texto temporalmente
        var $temp = $("<input>");
        //lo agregamos a nuestro body
        $("body").append($temp);
        //agregamos en el atributo value del input el contenido html encontrado
        //en el td que se dio click
        //y seleccionamos el input temporal
        $temp.val($(element).html()).select();
        //ejecutamos la funcion de copiado
        document.execCommand("copy");
        //eliminamos el input temporal
        $temp.remove();
    }
</script>

    <script>
        //<![CDATA[
        function enableDatapicker() {
            $(".input-group.date").datepicker({
                todayBtn: true,
                clearBtn: true,
                autoclose: true,
                todayHighlight: true,
                language: 'es',
            });
        }
        //]]>
    </script>
    <!-- Modal -->
    <div id="mdlError" class="modal fade" role="dialog">
        <div class="modal-dialog modal-sm">
            <!-- Modal content-->
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal">&times;</button>
                    <h4 class="modal-title">Error al guardar los cambios</h4>
                </div>
                <div class="modal-body">
                    <p>
                        <asp:Label ID="lblError" Text="" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
    <script>
        //<![CDATA[
        function openModal() {
            $('#mdlError').modal({ show: true });
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
</asp:Content>
