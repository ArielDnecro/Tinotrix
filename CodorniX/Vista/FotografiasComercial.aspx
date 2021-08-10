<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="FotografiasComercial.aspx.cs" Inherits="CodorniX.Vista.FotografiasComercial" %>

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
        </div>

        <div class="col-md-7">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Sucursal
                </div>
                <div class="btn-toolbar">
                    <div class="btn-group pull-left">
                        
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
                        
                        <li class="active" id="activeFotografiasC" runat="server">
                            <asp:LinkButton ID="tabFotografiasC" runat="server" Text="Foto comercial" OnClick="tabFotografiasC_Click"/></li>
                         <li id="activePapelC" runat="server">
                            <asp:LinkButton ID="tabPapelC" runat="server" Text="Papel comercial" OnClick="tabPapelC_Click" /></li>
                        
                    </ul>

                    <asp:TextBox ID="uidSucursal" runat="server" CssClass="hidden disabled" />

                    <asp:PlaceHolder ID="panelFotosC" runat="server">
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
                                    </asp:LinkButton><asp:LinkButton ID="btnCancelarEliminarFotoC" CssClass="btn btn-sm btn-danger" runat="server" OnClick="btnCancelarEliminarFotoC_Click">
                                      <span class="glyphicon glyphicon-remove"></span>
                                    </asp:LinkButton></div></div></div><div class="row" style="padding-top: 10px;">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                     <h4>Datos generales de las fotografias comerciales</h4></div></div><div id="PnErrorFotoCSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorFotoC" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                         </div>

                         <div class="panel-body">
                             <div  class="row panel  col-md-15 col-sm-18 col-xs-36">
                               <asp:TextBox ID="uidFotoC" runat="server" CssClass="hidden disabled" />
                                  <div class="col-md-3 col-sm-4 col-xs-12" >
                                    <h6 class="control-label">Impresora:</h6><asp:DropDownList ID="ddImpresoraFotoC" runat="server" CssClass="form-control" Enabled="false" ></asp:DropDownList>
                                   </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                        <h6 class="control-label">Descripcion:</h6><asp:TextBox ID="txtDescripcionFotoC" CausesValidation="false" MaxLength="100" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: Admin1" />
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio:</h6><a id="ToolPrecioFotoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtPrecioFotoC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 60" />
                                 </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio ticket:</h6><a id="ToolPrecioTicketC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtPrecioFotoTicketC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 80" />
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                       <h6 class="control-label">Precio servidor:</h6><a id="ToolPrecioServidorC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtPrecioFotoServidorC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 80" />
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                            <h6 >Status:</h6><asp:DropDownList ID="ddActivoFotoC" runat="server" CssClass="form-control" Enabled="false" >

                                            </asp:DropDownList>
                                  </div>
                                  <div class="col-md-3 col-sm-4 col-xs-12">
                                            <h6 >Unidad de medida:</h6><asp:DropDownList ID="ddMedidaFotoC" runat="server" CssClass="form-control" Enabled="false" >
                                            </asp:DropDownList>
                                 </div>
                             </div>

                             <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                  <div class="btn-primary text-center"  >
                                       <label    style="padding-right:100px; padding-left:100px; font-weight:lighter; ">Medidas impresora</label> <label   style="padding-left:100px; padding-right:100px; font-weight:lighter;">Medidas descripcion</label> </div><div  class="panel-body">
                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <h6 >Alto:</h6><a id="ToolAltoFotoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtAltoFotoC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.8" />
                                      </div>
                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                               <h6 >Ancho:</h6><a id="ToolAnchoFotoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtAnchoFotoC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.4" />
                                      </div>

                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                                <h6 >Alto:</h6><a id="ToolAltoFotoDescC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtAltoFotoDescC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 3" />
                                       </div>
                                      <div class="col-md-3 col-sm-6 col-xs-12">
                                                               <h6 >Ancho:</h6><a id="ToolAnchoFotoDescC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtAnchoFotoDescC" MaxLength="15" runat="server" Enabled="false" CssClass="form-control disabled" placeholder="Ejemplo: 2.5" />
                                      </div>
                                  </div>
                             </div>

                             <div class="row" style="padding-top: 10px;">
                                  <div class="col-xs-12">
                                       <asp:GridView ID="dgvFotosC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false" DataKeyNames="UidFoto" OnRowDataBound="dgvFotosC_RowDataBound" OnSelectedIndexChanged="dgvFotosC_SelectedIndexChanged">
                                           <EmptyDataTemplate>No hay fotografias comerciales asignados a está sucursal</EmptyDataTemplate><Columns>
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
                             </asp:LinkButton><asp:LinkButton ID="btnOkPapelC" runat="server" CssClass="btn btn-success btn-sm" Visible="false" OnClick="btnOkPapelC_Click">
                                <span class="glyphicon glyphicon-ok"></span>
                             </asp:LinkButton><asp:LinkButton ID="btnCancelarPapelC" runat="server" CssClass="btn btn-danger btn-sm " Visible="false" OnClick="btnCancelarPapelC_Click">
                                <span class="glyphicon glyphicon-remove"></span>
                             </asp:LinkButton></div><div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h4>Datos generales del papel comercial</h4></div></div><div class="row" style="padding-top: 10px;">
                            <div class="col-md-12 col-sm-12 col-xs-12">
                             <h6>* La unidad de medida obligatoriamente es en milimetros (mm) *</h6></div></div><div id="PnErrorPapelCSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="lblErrorPapelC" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                         </div>

                         <div class="row" style="padding-top: 10px; padding-bottom: 10px;">
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Nombre papel</h6><asp:TextBox ID="txtNombrePapelC" runat="server"  CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: Oficio" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Altura del papel</h6><a id="ToolAltoPapelC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtAltoPapelC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 270" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Anchura del papel</h6><a id="ToolAnchoPapelC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtAnchoPapelC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 210" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen superior</h6><a id="ToolMSuperiorC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtMargenSuperiorC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 30" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen inferior</h6><a id="ToolMInferiorC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtMargenInferiorC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 30" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen derecho</h6><a id="ToolMDerechoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtMargenDerechoC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 25" />
                            </div>
                            <div class="col-md-4 col-sm-6 col-xs-12">
                                <h6>Margen izquierdo</h6><a id="ToolMIzquierdoC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtMargenIzquierdoC" runat="server" CssClass="form-control datepicker" Enabled="false" placeholder="Ejemplo: 25" />
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
                                             </asp:LinkButton><asp:LinkButton ID="btnOKFotoPapelC" runat="server" Visible="false" CssClass="btn btn-success btn-sm " OnClick="btnOKFotoPapelC_Click">
                                                <span class="glyphicon glyphicon-ok"></span>
                                             </asp:LinkButton><asp:LinkButton ID="btnCancelarFotoPapelC" runat="server" Visible="false" CssClass="btn btn-danger btn-sm "  OnClick="btnCancelarFotoPapelC_Click">
                                                <span class="glyphicon glyphicon-remove"></span>
                                             </asp:LinkButton></div></div></div><div class="row" style="padding-top: 10px;">
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                        <h4>Fotografias sobre el papel</h4></div></div><div id="PnErrorFotoPapelCSucursal" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                    <div class="col-md-12 col-sm-12 col-xs-12">
                                         <asp:Label ID="lblErrorFotoPapelC" Text="" runat="server"  ForeColor="Red"  />
                                    </div>
                               </div>

                               <div class="row" style="padding-top: 15px;">
                                 <div class="col-md-1 col-sm-1 col-xs-1">
                                    <asp:CheckBox ID="CbRotarImagenPapelC" runat="server" Enabled="false" CssClass="form-control datepicker" Height="35" Width="35" />
                                 </div>
                                 <div class="col-md-4 col-sm-6 col-xs-12">
                                    <h6>Rotar fotografia sobre el papel</h6></div></div><div class="row" style="padding-top: 15px;">
                                    <div class="col-md-4 col-sm-6 col-xs-12" >
                                        <h6 class="control-label">Fotografia:</h6><asp:DropDownList ID="DdlFotoC" runat="server" AutoPostBack="True" CssClass="form-control" OnSelectedIndexChanged="DdlFotoC_SelectedIndexChanged">
                                            <asp:ListItem Text="[Seleccionar]"> </asp:ListItem></asp:DropDownList></div><div class="col-md-4 col-sm-6 col-xs-12">
                                        <h6>Cantidad de fotos X Fila</h6><a id="ToolFxFilaC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtFxFilaC" runat="server" Enabled="false" CssClass="form-control datepicker" placeholder="Ejemplo: 4"  />
                                     </div>

                                    <div class="col-md-4 col-sm-6 col-xs-12">
                                        <h6>Cantidad de fotos X Columna</h6><a id="ToolFxColumnaC" href="#"   class="tooltipDemo" runat="server"  visible="false" ></a><asp:TextBox ID="txtFxColumnaC" runat="server" Enabled="false" CssClass="form-control datepicker" placeholder="Ejemplo: 6"  />
                                    </div>
                               </div>

                               <div class="row" style="padding-top: 10px;">
                                    <div class="col-xs-12">
                                        <asp:Label ID="lbOrdenFPPorC" runat="server" CssClass="hidden" Visible="false"></asp:Label><asp:Label ID="lbOrdenFPC" runat="server" CssClass="hidden" Visible="false"></asp:Label><asp:GridView ID="dvgFotosPapelC" runat="server" CssClass="table table-bordered" AutoGenerateColumns="false"
                                        DataKeyNames="UidFoto" OnRowDataBound="dvgFotosPapelC_RowDataBound"
                                        OnSelectedIndexChanged="dvgFotosPapelC_SelectedIndexChanged"  AllowSorting="true"
                                         OnSorting="dvgFotosPapelC_Sorting">
                                            <EmptyDataTemplate>No hay fotografias comercial en papel comercial asignados a está sucursal</EmptyDataTemplate><Columns>
                                                <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hide" FooterStyle-CssClass="hide" ItemStyle-CssClass="hide" />
                                                <%--<asp:BoundField DataField="StrDescripcion" HeaderText="Descripcion" />--%>

                                                <asp:TemplateField>
                                                      <HeaderTemplate>
                                                               <asp:LinkButton ID="LBnOrdenaColonia" runat="server" CommandName="Sort" CommandArgument="Descripcion" >
                                                                        <span runat="server" >Descripcion  <i ID="IcoDescripcionFP" runat="server"  ></i> </span>
                                                               </asp:LinkButton></HeaderTemplate><ItemTemplate>
                                                                 <asp:Label ID="LbDirDescripcionFP" runat="server" Text='<%# Bind("StrDescripcion") %>'></asp:Label></ItemTemplate></asp:TemplateField><asp:BoundField DataField="VchFila" HeaderText="# Foto X Fila" />
                                                <asp:BoundField DataField="VchColumna" HeaderText="# Foto X Columna" />
                                            
                                                <asp:TemplateField HeaderStyle-BackColor="#F3F3F3" ItemStyle-BackColor="#F3F3F3" ItemStyle-HorizontalAlign="Center">
                                                    <HeaderTemplate>
                                                      <asp:LinkButton ID="LBnOrdenaStatus_icon" runat="server" CommandName="Sort" CommandArgument="Status">
                                                        <span> Rotar <i id="IcoRotar" runat="server"></i></span>
                                                      </asp:LinkButton></HeaderTemplate><ItemTemplate>
                                                        <asp:Label ID="lbFotoRotadoPapel_icon" runat="server"><!--nombre id basado en los botones-->
                                                            <span class="glyphicon glyphicon glyphicon-ok"></span>
                                                        </asp:Label><asp:Label ID="lbFotoNoRotadoPapel_icon" runat="server"><!--nombre id basado en los botones-->
                                                            <span class="glyphicon glyphicon glyphicon-remove"></span>
                                                        </asp:Label></ItemTemplate></asp:TemplateField></Columns></asp:GridView></div></div></div></asp:PlaceHolder>

                </div>
            </div>
        </div>

    </div> 
    
    <%-- procedimientos de confirmaciones y acceso al lado servidor--%>
                <div class="modal fade" id="VConfimacionEditarPapelC"  tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                                <div class="modal-dialog" role="document">
                                    <div class="modal-content">
                                        <div class="modal-header">
                                            <h5 class="modal-title" id="exampleModalLabelC">Editar papel</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button></div><div class="modal-body">
                                           Si escoge esta opcion se borrara las medidas de cada foto ¿Esta usted de acuerdo? </div><div class="modal-footer">
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
                                            <h5 class="modal-title">Editar fotografia</h5><button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                                <span aria-hidden="true">&times;</span></button></div><div class="modal-body">
                                           Si escoge esta opcion se borrara las medidas de esta foto ¿Esta usted de acuerdo? </div><div class="modal-footer">
                                            <asp:Button runat="server" ID="Button4" Cssclass="btn btn-secondary" Text="Cancelar"  data-dismiss="modal" />
                                            <asp:Button runat="server" ID="Button5" Cssclass="btn btn-primary" Text="Confirmar"  OnClick="btnEditarFotoC_Click" />
                                        </div>
                                    </div>
                                </div>
         </div>

    <script  runat="server">
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

    <!-- Datapicker -->
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
                    <button type="button" class="close" data-dismiss="modal">&times;</button><h4 class="modal-title">Error al guardar los cambios</h4></div><div class="modal-body">
                    <p>
                        <asp:Label ID="lblError" Text="" runat="server" />
                    </p>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button></div></div></div></div>

    <script>
        //<![CDATA[
        function openModal() {
            $('#mdlError').modal({ show: true });
        }
        //]]>
    </script>

</asp:Content>