<%@ Page Title="Empresas" Language="C#" MasterPageFile="Site1.Master" AutoEventWireup="true" CodeBehind="Empresas.aspx.cs" Inherits="CodorniX.Vista.Empresas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Empresas</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
        <div class="col-md-6">
            <!-- Panel principal: Empresas -->
            <asp:PlaceHolder ID="panelEmpresa" runat="server">
                <div class="panel panel-primary">
                    <div class="panel-heading text-center">
                        Empresa
                    </div>
                    <!-- Botones de busqueda -->
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
                    <div class="panel-body">
                        <asp:PlaceHolder ID="seccionBusqueda" runat="server" Visible="true">
                            <div class="row">
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>RFC</h6>
                                    <asp:TextBox ID="txtBusquedaRFC" MaxLength="14" runat="server" CssClass="form-control" placeholder="RFC" />
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Nombre Comercial</h6>
                                    <asp:TextBox ID="txtBusquedaNombreComercial" MaxLength="50" runat="server" CssClass="form-control" placeholder="Nombre Comercial" />
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Razón Social</h6>
                                    <asp:TextBox ID="txtBusquedaRazonSocial" MaxLength="60" runat="server" CssClass="form-control" placeholder="Razón Social" />
                                </div>
                                <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Giro Comercial</h6>
                                    <asp:TextBox ID="txtBusquedaGiro" MaxLength="40" runat="server" CssClass="form-control" placeholder="Giro Comercial" />
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
                            </div>
                        </asp:PlaceHolder>
                        <div class="row" style="padding-top: 10px;">
                            <div class="col-lg-12">
                                <asp:GridView ID="dgvEmpresas" runat="server" PageSize="10" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="dgvEmpresas_RowDataBound" OnSelectedIndexChanged="dgvEmpresas_SelectedIndexChanged" DataKeyNames="UidEmpresa" OnSorting="dgvEmpresas_Sorting" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="dgvEmpresas_PageIndexChanging">
                                    <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                    <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                    <EmptyDataTemplate>
                                        No hay empresas registradas
                                    </EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                        <asp:BoundField DataField="StrRFC" HeaderText="RFC" SortExpression="RFC" />
                                        <asp:BoundField DataField="StrNombreComercial" HeaderText="Nombre Comercial" SortExpression="NombreComercial" />
                                        <asp:BoundField DataField="StrRazonSocial" HeaderText="Razón Social" SortExpression="RazonSocial" />
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
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorDireccion" Text="" runat="server" CssClass="text-danger" />
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
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Empresa
                </div>
                <div class="btn-toolbar">
                    <div class="btn-group pull-left">
                        <asp:LinkButton ID="btnNuevaEmpresa" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnNuevaEmpresa_Click">
                            <span class="glyphicon glyphicon-file"></span>
                            Nuevo
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnEditarEmpresa" runat="server" CssClass="btn btn-default btn-sm disabled" OnClick="btnEditarEmpresa_Click">
                            <span class="glyphicon glyphicon-edit"></span>
                            Editar
                        </asp:LinkButton>

                        <asp:LinkButton ID="btnOkEmpresa" runat="server" CssClass="btn btn-success btn-sm disabled hidden" OnClick="btnOkEmpresa_Click">
                            <span class="glyphicon glyphicon-ok"></span>
                        </asp:LinkButton>
                        <asp:LinkButton ID="btnCancelarEmpresa" runat="server" CssClass="btn btn-danger btn-sm disabled hidden" OnClick="btnCancelarEmpresa_Click">
                            <span class="glyphicon glyphicon-remove"></span>
                        </asp:LinkButton>
                    </div>
                    <div class="pull-right">
                        <asp:LinkButton ID="btnUsuario" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnUsuario_Click">
                            Usuario
                            <span class="glyphicon glyphicon-user"></span>
                        </asp:LinkButton>
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
                    </ul>
                    <asp:PlaceHolder ID="panelDatosEmpresa" Visible="true" runat="server">
                        <div class="row" style="color: red; padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorEmpresa" Text="" runat="server" CssClass="text-danger" />
                            </div>
                        </div>
                        <div class="row" style="padding-top: 10px;">
                            <asp:TextBox ID="uidEmpresa" runat="server" CssClass="hidden disabled" />
                            <asp:UpdatePanel runat="server">

                                <ContentTemplate>

                                    <div class="col-md-4">
                                        <h6>Foto</h6>
                                        <asp:Image runat="server" CssClass="img img-thumbnail" ID="ImgEmpresas" Width="200px" Height="160px" />
                                        <asp:TextBox ID="txtimagen" CssClass="form-control hide" runat="server"></asp:TextBox>
                                        <div>
                                            <label id="lblFotoEncargado" class="btn btn-default btn-file form-control" runat="server">
                                                Escoger Foto
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

                            <div class="col-md-4 col-sm-4 col-xs-12">
                                <asp:Panel ID="frmGrpRFC" runat="server" CssClass="form-group">
                                    <h6 class="control-label">RFC</h6>
                                    <asp:TextBox ID="txtRFC" MaxLength="13" runat="server" CssClass="form-control" placeholder="RFC" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12">
                                <asp:Panel ID="frmGrpNombreComercial" runat="server" CssClass="form-group">
                                    <h6 class="control-label">Nombre Comercial</h6>
                                    <asp:TextBox ID="txtNombreComercial" MaxLength="50" runat="server" CssClass="form-control" placeholder="Nombre Comercial" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-8 col-sm-8 col-xs-12">
                                <asp:Panel ID="frmGrpRazonSocial" runat="server" CssClass="form-group">
                                    <h6 class="control-label">Razón Social</h6>
                                    <asp:TextBox ID="txtRazonSocial" MaxLength="60" runat="server" CssClass="form-control" placeholder="Razón Social" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-4 col-xs-12">
                                <asp:Panel ID="frmGrpGiro" runat="server" CssClass="form-group">
                                    <h6>Giro Comercial</h6>
                                    <asp:TextBox ID="txtGiro" MaxLength="40" runat="server" CssClass="form-control" placeholder="Giro Comercial" />
                                </asp:Panel>
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Fecha de registro</h6>
                                <asp:TextBox ID="txtFechaRegistro" runat="server" CssClass="form-control datepicker" placeholder="Día/Mes/Año" />
                            </div>
                        </div>
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
                                    <asp:LinkButton ID="btnAceptarEliminarDireccion" OnClick="btnAceptarEliminarDireccion_Click" CssClass="btn btn-sm btn-success" runat="server">
                                        <asp:Label ID="Label2" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarDireccion" OnClick="btnCancelarEliminarDireccion_Click" CssClass="btn btn-sm btn-danger" runat="server">
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
                                        <asp:Label ID="Label1" CssClass="glyphicon glyphicon-ok" runat="server" />
                                    </asp:LinkButton>

                                    <asp:LinkButton ID="btnCancelarEliminarTelefono" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarTelefono_Click">
                                        <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorTelefono" Text="" runat="server" CssClass="text-danger" />
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
                                    <EmptyDataTemplate>No hay teléfonos asignados a está empresa</EmptyDataTemplate>
                                    <Columns>
                                        <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                        <asp:BoundField DataField="StrTipoTelefono" HeaderText="Tipo" />
                                        <asp:BoundField DataField="StrTelefono" HeaderText="Ciudad" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
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

        function openModal() {
            $('#mdlError').modal({ show: true });
        }
        
        function upload(FileUpload1) {
            if (FileUpload1.value != '') {
                document.getElementById("<% = btnimagen.ClientID  %>").click();
            }
        }
        //]]>
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
