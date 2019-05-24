<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PerfilEmpresa.aspx.cs" MasterPageFile="Site1.Master" Inherits="CodorniX.Vista.PerfilEmpresa" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
        <!-- panel izquierdo -->
        <div class="col-md-6">
            <asp:PlaceHolder ID="panelbusquedas" runat="server">
                <div class="panel panel-primary">
                    <div class="panel-heading text-center">
                        BuscarPerfiles
                    </div>

                    <div class="pull-right">
                        <asp:LinkButton ID="btnMostrar" OnClick="btnMostrar_Click" CssClass="btn btn-sm btn-default" runat="server">
                            <asp:Label ID="lblFiltros" class="glyphicon glyphicon-collapse-up" runat="server" />
                        </asp:LinkButton><asp:LinkButton ID="btnLimpiar" OnClick="btnLimpiar_Click" CssClass="btn btn-sm btn-default" runat="server">
                            <span class="glyphicon glyphicon-trash"></span>
                            Limpiar
                        </asp:LinkButton><asp:LinkButton ID="btnBuscar" OnClick="btnBuscar_Click" CssClass="btn btn-sm btn-default" runat="server">
                            <span class="glyphicon glyphicon-search"></span>
                            Buscar
                        </asp:LinkButton>
                    </div>

                    <div class="panel-body">
                        <asp:Panel ID="PanelFiltros" runat="server">
                            <div class="col-md-12">
                                <div class="col-md-4">
                                    <h6>Perfiles</h6>
                                    <asp:TextBox ID="FiltroPerfiles" CssClass="form-control" placeholder="Perfil" runat="server"></asp:TextBox>
                                </div>
                            </div>
                            <div class="col-md-12">
                                <br />
                            </div>
                        </asp:Panel>

                        <div style="padding-top: 25px">
                            <asp:GridView ID="DVGPerfiles" AllowPaging="true" PageSize="10" OnPageIndexChanging="DVGPerfiles_PageIndexChanging" OnSelectedIndexChanged="DVGPerfiles_SelectedIndexChanged" OnSorting="DVGPerfiles_Sorting" OnRowDataBound="DVGPerfiles_RowDataBound" AutoGenerateColumns="false" DataKeyNames="UidPerfil" CssClass="table table-bordered table-condensed table-striped input-sm" AllowSorting="true" runat="server">
                                <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                <EmptyDataTemplate>
                                    No hay Datos
                                </EmptyDataTemplate>
                                <Columns>
                                     <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hidden" />
                                    <asp:BoundField DataField="StrPerfil" HeaderText="Perfil" SortExpression="Perfil" />
                                   
                                   </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
            </asp:PlaceHolder>
        </div>

        <!-- panel Derecho-->
        <div class="col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading">
                    Gestion de Perfiles
                </div>
                <div class="text-left">
                    <asp:LinkButton ID="btnNuevo" OnClick="btnNuevo_Click" CssClass="btn btn-sm btn-default" runat="server">
                            <span class="glyphicon glyphicon-file"></span>
                            Nuevo
                    </asp:LinkButton><asp:LinkButton ID="btnEditar" OnClick="btnEditar_Click" CssClass="btn btn-sm disabled btn-default" runat="server">
                            <span class="glyphicon glyphicon-cog"></span>
                            Editar
                    </asp:LinkButton><asp:LinkButton ID="btnAceptar" CssClass="btn btn-sm btn-success" OnClick="btnAceptar_Click" runat="server">
                        <asp:Label ID="lblAccion" CssClass="glyphicon glyphicon-ok" runat="server" />
                    </asp:LinkButton><asp:LinkButton ID="btnCancelar" OnClick="btnCancelar_Click" CssClass="btn btn-sm btn-danger" runat="server">
                            <span class="glyphicon glyphicon-remove"></span>
                    </asp:LinkButton>
                    <asp:Label ID="lblMensaje" runat="server" />
                    <asp:TextBox CssClass="hide" ID="txtUidPerfil" runat="server" />
                    <asp:TextBox CssClass="hide" ID="txtUidNivelAcceso" runat="server" />
                </div>

                <div class="panel-body">
                     <ul class="nav nav-tabs">
                        <li class="active" id="activeDatosGenerales" runat="server">
                            <asp:LinkButton ID="tabDatos" runat="server" OnClick="tabDatos_Click" Text="Datos Generales" /></li>
                       <li id="activeAccesos" runat="server">
                            <asp:LinkButton ID="tabAccesos" OnClick="tabAccesos_Click" runat="server" Text="Accesos" />
                        </li>
                    </ul>
                    <asp:PlaceHolder ID="panelGestiondePerfiles" runat="server">
                        <div class="row" style="color: red; padding-top: 10px;">
                            <div class="col-xs-12">
                                <asp:Label ID="lblErrorPerfil" Text="" runat="server" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-4">
                                <asp:Panel ID="frmGrpPerfiles" runat="server" CssClass="form-group">
                                    <h6>Perfil</h6>
                                    <asp:TextBox ID="txtPerfil" CssClass="form-control" placeholder="Perfil" runat="server"></asp:TextBox>
                                </asp:Panel>
                            </div>

                            <div class="col-md-4">
                                <h6>Home</h6>
                                <asp:DropDownList ID="DdHome" CssClass="form-control" runat="server">
                                </asp:DropDownList>
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
</asp:Content>

