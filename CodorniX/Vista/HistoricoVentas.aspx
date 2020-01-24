<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="HistoricoVentas.aspx.cs" Inherits="CodorniX.Vista.HistoricoVentas" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <title>Historico ventas</title>
    <link href="../Content/bootstrap-datepicker3.css" rel="stylesheet" />
    <link href="../Content/jquery-clockpicker.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
         <div class="col-md-7">
             <asp:PlaceHolder ID="PanelBusquedas" runat="server">
                 <div class="panel panel-primary">
                      <div class="panel-heading text-center">
                         <h5>BUSQUEDA DE HISTORICO VENTAS</h5>
                      </div>
                      <div class="panel-body">
                        <ul class="nav nav-tabs">
                               <li  class="active" id="Li1" runat="server">
                                  <asp:LinkButton  runat="server" Text="Busqueda"  /></li>
                        </ul>
                        <div class="btn-toolbar">
                                    <div class="btn-group pull-right">
                                        <asp:LinkButton ID="btnMostrarBusqueda" runat="server" CssClass="btn btn-default btn-sm" OnClick="btnMostrarBusqueda_Click"  >
                                            Mostrar
                                            <span class="glyphicon  glyphicon-eye-open"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnBorrarBusqueda" runat="server" CssClass="btn btn-default btn-sm " OnClick="btnBorrarBusqueda_Click" >
                                            Borrar
                                            <span class="glyphicon  glyphicon-trash "></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm " OnClick="btnBuscar_Click" >
                                            Buscar
                                            <span class="glyphicon glyphicon-search"></span>
                                        </asp:LinkButton>
                                    </div>
                        </div>
                        <asp:PlaceHolder ID="PlaceHolder1"  runat="server">
                          <ul class="nav nav-tabs">
                               <%--<li id="activeServidor" runat="server">
                               <asp:LinkButton ID="tabServidor" runat="server" Text="Servidor" OnClick="tabServidor_Click" /></li>--%>
                               <li  class="active" id="activeBusqueda" runat="server">
                                  <asp:LinkButton ID="tabBusqueda" runat="server" Text="Lista de turnos" OnClick="tabBusqueda_Click" /></li>
                               <li id="activeBusResumen" runat="server">
                                  <asp:LinkButton ID="tabBusResumen" runat="server" Text="Resumen"  OnClick="tabBusResumen_Click" /></li>
                          </ul>
                        
                          <asp:PlaceHolder ID="panelBusqueda"  runat="server">
                             
                              <div id="PnErrorBusHistoricos" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="LbErrorHistoricos" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                             </div>
                              <div class="panel-body">
                                  <asp:PlaceHolder ID="seccionBusqueda" runat="server" Visible="true">
                                      <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Sucursales:</h6>
                                               <asp:DropDownList ID="ddSucursales" runat="server" CssClass="form-control" ></asp:DropDownList>
                                           </div>
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Encargados:</h6>
                                               <asp:DropDownList ID="ddEncargados" runat="server" CssClass="form-control" ></asp:DropDownList>
                                           </div>
                                          
                                      </div>

                                      <%--<div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                           <div class="col-md-12 col-sm-6 col-xs-12">
                                                <h6>Fotografias:</h6>
                                               <asp:DropDownList ID="ddFotos" runat="server" CssClass="form-control" >
                                                   <asp:ListItem>Todos</asp:ListItem>
                                               </asp:DropDownList>
                                           </div>
                                      </div>--%>

                                      <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                              <div class="btn-primary text-center"  >
                                                  <h8>Numero de folio</h8>
                                              </div>
                                               
                                              <div  class=" panel-body ">
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorNoFolioRangoMenor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem Value=">=">(>=) Mayor o igual que</asp:ListItem>
                                                           <asp:ListItem Value=">" >(>)  Mayor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div  class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango minimo</h6>
                                                      <asp:TextBox ID="txtBusquedaNoFolioEntre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 2" />
                                                      <%--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"    ControlToValidate="txtBusquedaNoFolioEntre" ErrorMessage="Este campo debe de ser un numero" />--%>
                                                  </div>
                                                  <%--<div style="padding: 0px;" class="col-md-1 col-sm-12 col-xs-12" >--%>
                                                        <%--<label class="text-center" >Y</label>--%>
                                                  <%--</div>--%>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorNoFolioRangoMayor" runat="server" CssClass="form-control" >
                                                          <asp:ListItem Value="<="> (<=) Menor o igual que</asp:ListItem>
                                                           <asp:ListItem Value="<">(<) Menor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango maximo</h6>
                                                     <asp:TextBox ID="txtBusquedaFolioY" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 23" />
                                                       <%--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"    ControlToValidate="txtBusquedaFolioY" ErrorMessage="Este campo debe de ser un numero" />--%>
                                                  </div>
                                              </div>
                                      </div>
                                    
                                      <div  class="row panel panel-primary col-md-18 col-sm-18 col-xs-36">
                                              <div class="btn-primary text-center"  >
                                                  <h8>Fecha de apertura</h8>
                                              </div>
                                               
                                              <div  class=" panel-body ">
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorFechaAperturaRangoMenor" runat="server" CssClass="form-control" >
                                                          <asp:ListItem Value=">=">(>=) Mayor o igual que</asp:ListItem>
                                                          <asp:ListItem Value=">">(>) Mayor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                        <h6>Rango minimo:</h6>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtBusquedaMinFechaApertura" runat="server" CssClass="form-control"  placeholder=" Ej.: 27/01/1993" />
                                                            <div class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </div>
                                                        </div>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorFechaAperturaRangoMayor" runat="server" CssClass="form-control" >
                                                          <asp:ListItem Value="<=">(<=) Menor o igual que</asp:ListItem>
                                                          <asp:ListItem Value="<">(<) Menor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                        <h6>Rango maximo:</h6>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtBusquedaMaxFechaApertura" runat="server" CssClass="form-control"  placeholder=" Ej.: 27/01/2019" />
                                                            <div class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </div>
                                                        </div>
                                                  </div>
                                              </div>
                                      </div>

                                      <div  class="row panel panel-primary col-md-18 col-sm-18 col-xs-36">
                                              <div class="btn-primary text-center"  >
                                                  <h8>Fecha de cierre</h8>
                                              </div>
                                               
                                              <div  class=" panel-body ">
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorFechaCierreRangoMenor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem Value=">=">(>=) Mayor o igual que</asp:ListItem>
                                                           <asp:ListItem Value=">">(>) Mayor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                    <h6 runat="server" >Rango minimo:</h6>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtBusquedaMinFechaCierre" runat="server" CssClass="form-control"  placeholder=" Ej.: 13/09/1994" />
                                                        <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </div>
                                                    </div>
                                                 </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorFechaCierreRangoMayor" runat="server" CssClass="form-control" >
                                                          <asp:ListItem Value="<=">(<=) Menor o igual que</asp:ListItem> 
                                                          <asp:ListItem Value="<">(<) Menor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                        <h6>Rango maximo:</h6>
                                                        <div class="input-group date">
                                                            <asp:TextBox ID="txtBusquedaMaxFechaCierre" runat="server" CssClass="form-control"  placeholder=" Ej.: 13/09/2019" />
                                                            <div class="input-group-addon">
                                                                <span class="glyphicon glyphicon-calendar"></span>
                                                            </div>
                                                        </div>
                                                  </div>
                                              </div>
                                      </div>

                                      <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                              <div class="btn-primary text-center"  >
                                                  <h8>Cantidad de fotos</h8>
                                              </div>
                                               
                                              <div  class=" panel-body ">
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorCantFotosRangoMenor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem Value=">=">(>=) Mayor o igual que</asp:ListItem>
                                                           <asp:ListItem Value=">">(>)  Mayor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div  class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango minimo</h6>
                                                      <asp:TextBox ID="txtBusquedaCantFotosEntre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 2" />
                                                      <%--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"    ControlToValidate="txtBusquedaCantFotosEntre" ErrorMessage="Este campo debe de ser un numero" />--%>
                                                  
                                                  </div>
                                                  <%--<div style="padding: 0px;" class="col-md-1 col-sm-12 col-xs-12" >--%>
                                                        <%--<label class="text-center" >Y</label>--%>
                                                  <%--</div>--%>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorCantFotosRangoMayor" runat="server" CssClass="form-control" >
                                                          <asp:ListItem Value="<=">(<=) Menor o igual que</asp:ListItem>
                                                          <asp:ListItem Value="<">(<) Menor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango maximo</h6>
                                                     <asp:TextBox ID="txtBusquedaCantFotosY" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 23" />
                                                       <%--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Integer" ForeColor="Red"    ControlToValidate="txtBusquedaCantFotosY" ErrorMessage="Este campo debe de ser un numero" />--%>
                                                  
                                                  </div>
                                              </div>
                                      </div>
                                   
                                      <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                              <div class="btn-primary text-center"  >
                                                  <h8>Importe</h8>
                                              </div>
                                               
                                              <div  class=" panel-body ">
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorImporteRangoMenor" runat="server" CssClass="form-control" >
                                                          <asp:ListItem Value=">=">(>=) Mayor o igual que</asp:ListItem>
                                                          <asp:ListItem Value=">">(>)  Mayor que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div  class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango minimo</h6>
                                                      <asp:TextBox ID="txtBusquedaImporteEntre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 2" />
                                                      <%--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Double" ForeColor="Red"    ControlToValidate="txtBusquedaImporteEntre" ErrorMessage="Este campo debe de ser un numero" />--%>
                                                  
                                                  </div>
                                                  <%--<div style="padding: 0px;" class="col-md-1 col-sm-12 col-xs-12" >--%>
                                                        <%--<label class="text-center" >Y</label>--%>
                                                  <%--</div>--%>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorImporteRangoMayor" runat="server" CssClass="form-control" >
                                                             <asp:ListItem Value="<=">(<=) Menor o igual que</asp:ListItem>
                                                             <asp:ListItem Value="<">(<) Menor que</asp:ListItem>
                                                        </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango maximo</h6>
                                                     <asp:TextBox ID="txtBusquedaImporteY" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 23" />
                                                      <%--<asp:CompareValidator runat="server" Operator="DataTypeCheck" Type="Double" ForeColor="Red"    ControlToValidate="txtBusquedaImporteY" ErrorMessage="Este campo debe de ser un numero" />--%>
                                                  
                                                  </div>
                                              </div>
                                      </div>
                                   
                                     

                                  </asp:PlaceHolder>

                                  <div class="row table-responsive" style="padding-top: 10px;">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="dgvHistoricos" runat="server" PageSize="10" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="dgvHistoricos_RowDataBound" OnSelectedIndexChanged="dgvHistoricos_SelectedIndexChanged" DataKeyNames="UidTurno" OnSorting="dgvHistoricos_Sorting" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="dgvHistoricos_PageIndexChanging">
                                                <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    No hay historicos de turnos registradas
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="StrSucursal" HeaderText="Sucursal" SortExpression="Sucursal" />
                                                    <asp:BoundField DataField="StrEncargado" HeaderText="Encargado" SortExpression="Encargado" />
                                                    <asp:BoundField DataField="UidSucursal" HeaderText="Guid Sucursal" SortExpression="UidSucursal"  Visible="false"/>
                                                    <asp:BoundField DataField="UidEncargado" HeaderText="Guid Encargado" SortExpression="UidEncargado" Visible ="false" />
                                                    <asp:BoundField DataField="IntFolio" HeaderText="Folio" SortExpression="Folio" />
                                                    <asp:BoundField DataField="IntNoFotos" HeaderText="No. Copias" SortExpression="NoFotos" />
                                                    <asp:BoundField DataField="DouImporte" HeaderText="Importe" SortExpression="Importe" />
                                                    <asp:BoundField DataField="DtApertura" HeaderText="Fecha de apertura" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="FechaApertura" />
                                                    <asp:BoundField DataField="DtCierre" HeaderText="Fecha de cierre" DataFormatString="{0:dd/MM/yyyy}" HtmlEncode="false" SortExpression="FechaCierre" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                  </div>
                              </div>
                          </asp:PlaceHolder>
                          <asp:PlaceHolder ID="panelBusResumen" Visible="false"  runat="server">
                             <div id="PnErrorBusResumenHistoricos" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="LbErrorResumenHistoricos" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                             </div>
                             <div class="panel-body">
                                 <asp:PlaceHolder ID="SeccionBusResumen" runat="server" Visible="true">
                                     <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Cantidad de turnos encontrados:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-list-alt"></span>
                                               </div>
                                               <asp:TextBox ID="txtBusReTurnosEnc" MaxLength="50" runat="server" CssClass="form-control" Enabled="false"  />
                                               
                                           </div>
                                     </div>
                                     <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Rango de dias encontrados:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon  glyphicon-time"></span>
                                               </div>
                                               <asp:TextBox ID="txtBusReDiasEnc" MaxLength="50" runat="server" CssClass="form-control" Enabled="false"  />
                                           </div>
                                     </div>
                                     <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Cantidad de fotos:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon  glyphicon-camera"></span>
                                               </div>
                                               <asp:TextBox ID="txtBusReCantFotos" MaxLength="50" runat="server" CssClass="form-control" Enabled="false"  />
                                           </div>
                                      </div>
                                     <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Importe:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-tags"></span>
                                               </div>
                                               <asp:TextBox ID="txtBusReImporte" MaxLength="50" runat="server" CssClass="form-control" Enabled="false" />
                                           </div>
                                      </div>
                                 </asp:PlaceHolder>
                             </div>
                           </asp:PlaceHolder>
                        </asp:PlaceHolder>
                      </div>
                 </div>
             </asp:PlaceHolder>
         </div>
   
         <div class="col-md-5">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    <h5>HISTORICO VENTA</h5>
                </div>
                <div class="panel-body">
                     <ul class="nav nav-tabs">
                        <li class="active" id="activeResumen" runat="server">
                            <asp:LinkButton ID="tabResumen" runat="server" Text="Resumen" OnClick="tabResumen_Click" /></li>
                        <li id="activeDetalle" runat="server">
                            <asp:LinkButton ID="tabDetalle" runat="server" Text="Detalle" OnClick="tabDetalle_Click" /></li>
                     </ul>
                     <asp:PlaceHolder ID="panelResumen" Visible="true" runat="server">
                         <div id="PnErrorHistoricoResumen" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="LbErrorHistoricoResumen" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                          </div>
                         <asp:TextBox ID="UidHTurno" runat="server" CssClass="hidden disabled" />
                         <div class="panel-body">
                              <asp:PlaceHolder ID="seccionResumen" runat="server" Visible="true">
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Sucursal:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                               <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-home"></span>
                                               </div>
                                               <asp:TextBox ID="txtSucursal" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Encargado:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-user"></span>
                                               </div>
                                               <asp:TextBox ID="txtEncargado" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Numero de Folio:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon  glyphicon-folder-open"></span>
                                               </div>
                                               <asp:TextBox ID="txtFolio" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Fecha de apertura:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                               <div class="input-group-addon">
                                                            <span class="glyphicon  glyphicon-calendar"></span>
                                               </div>
                                               <asp:TextBox ID="txtFApertura" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Fecha de cierre:</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon  glyphicon-calendar"></span>
                                               </div>
                                               <asp:TextBox ID="txtFCierre" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Cantidad total de fotos (copias):</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-camera"></span>
                                               </div>
                                              <asp:TextBox ID="txtCantFotos" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                  <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-6 col-sm-6 col-xs-12">
                                                <h6>Importe total (fotos):</h6>
                                           </div>
                                          <div class="input-group col-md-6 col-sm-6 col-xs-12">
                                              <div class="input-group-addon">
                                                            <span class="glyphicon  glyphicon-tags"></span>
                                               </div>
                                              <asp:TextBox ID="txtImporte" runat="server"  CssClass="form-control datepicker" Enabled="false" />
                                           </div>
                                    </div>
                                 
                                   
                              </asp:PlaceHolder>
                         </div>
                     </asp:PlaceHolder>
                     <asp:PlaceHolder ID="panelDetalle" Visible="false" runat="server">
                         <div id="PnErrorHistoricoDetalle" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="LbErrorHistoricoDetalle" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                          </div>
                           <div class="panel-body">
                                <div class="row table-responsive " style="padding-top: 10px; ">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="dgvHImpresiones" runat="server" PageSize="10" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="dgvHImpresiones_RowDataBound" OnSelectedIndexChanged="dgvHImpresiones_SelectedIndexChanged"  OnSorting="dgvHImpresiones_Sorting" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="dgvHImpresiones_PageIndexChanging">
                                                <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    No hay historicos  de ventas registradas
                                                </EmptyDataTemplate>
                                                <Columns>
                                                    <asp:ButtonField CommandName="Select" HeaderStyle-CssClass="hidden" ItemStyle-CssClass="hidden" FooterStyle-CssClass="hidden" />
                                                    <asp:BoundField DataField="StrFechaHora" HeaderText="Fecha y hora de venta" DataFormatString="{0:dd/MM/yyyy HH:mm}" HtmlEncode="false" SortExpression="FechaHora"  ItemStyle-Width="150px"/>
                                                    <asp:BoundField DataField="IntFolio" HeaderText="No. folio" SortExpression="NoFolio" />
                                                    <asp:BoundField DataField="IntMaquina" HeaderText="No. PC" SortExpression="NoMaquina" />
                                                    <asp:BoundField DataField="StrCopiasXImpresion" HeaderText="No. copias" SortExpression="NoCopias" />
                                                    <asp:BoundField DataField="StrFotosXCopiasXImpresion" HeaderText="No. fotos" SortExpression="NoFotos" />
                                                    <asp:BoundField DataField="StrFotoDesc" HeaderText="Descripcion foto" SortExpression="FotoDesc" />
                                                    <asp:BoundField DataField="StrCostoTicket" HeaderText="Costo ticket" SortExpression="CostoTicket" />
                                                    <asp:BoundField DataField="StrCosto" HeaderText="Costo foto" SortExpression="Costo" />
                                                    
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                  </div>
                           </div>
                     </asp:PlaceHolder>
                </div>
            </div>
        </div>
    </div>
 <%--</div>--%>

    <script>
        function enableDatapicker() {
            $(".input-group.date").datepicker({
                todayBtn: true,
                clearBtn: true,
                autoclose: true,
                todayHighlight: true,
                language: 'es',
            });
             $(".input-group.time").clockpicker({
                 'default': 'now', 
                 autoclose: true,
                 //twelvehour: true,

                //donetext: 'Listo',
                 language: 'es',
                 
            });
        }
    </script>
</asp:Content>
