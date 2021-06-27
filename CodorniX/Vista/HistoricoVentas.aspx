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
                               <%--<li id="activeServidor" runat="server">
                            <asp:LinkButton ID="tabServidor" runat="server" Text="Servidor" OnClick="tabServidor_Click" /></li>--%>
                               <li  class="active" id="activeBusqueda" runat="server">
                                  <asp:LinkButton ID="tabBusqueda" runat="server" Text="Busqueda" /></li>
                              <%-- <li id="activeResumen" runat="server">
                                  <asp:LinkButton ID="tabResumen" runat="server" Text="Resumen" /></li>--%>
                          </ul>
                        
                          <asp:PlaceHolder ID="panelBusqueda"  runat="server">
                              <div class="btn-toolbar">
                                    <div class="btn-group pull-right">
                                        <asp:LinkButton ID="btnMostrarBusqueda" runat="server" CssClass="btn btn-default btn-sm"  >
                                            Mostrar
                                            <span class="glyphicon  glyphicon-eye-open"></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnBorrarBusqueda" runat="server" CssClass="btn btn-default btn-sm " >
                                            Borrar
                                            <span class="glyphicon  glyphicon-trash "></span>
                                        </asp:LinkButton>
                                        <asp:LinkButton ID="btnBuscar" runat="server" CssClass="btn btn-default btn-sm " >
                                            Buscar
                                            <span class="glyphicon glyphicon-search"></span>
                                        </asp:LinkButton>
                                    </div>
                              </div>
                              <div id="PnErrorHistoricos" class="row alert alert-danger"  runat="server" visible="false" style="padding-top: 10px;" >
                                <div class="col-md-12 col-sm-12 col-xs-12">
                                     <asp:Label ID="LbErrorHistoricos" Text="" runat="server"  ForeColor="Red"  />
                                </div>
                             </div>
                              <div class="panel-body">
                                  <asp:PlaceHolder ID="seccionBusqueda" runat="server" Visible="true">
                                      <div class="row panel col-md-15 col-sm-18 col-xs-36" >
                                          <div class="col-md-4 col-sm-6 col-xs-12">
                                                <h6>Sucursales:</h6>
                                               <asp:DropDownList ID="ddSucursales" runat="server" CssClass="form-control" >
                                                    <asp:ListItem>Todos</asp:ListItem>
                                               </asp:DropDownList>
                                           </div>
                                          <div class="col-md-4 col-sm-6 col-xs-12">
                                                <h6>Encargados:</h6>
                                               <asp:DropDownList ID="ddEncargados" runat="server" CssClass="form-control" >
                                                   <asp:ListItem>Todos</asp:ListItem>
                                               </asp:DropDownList>
                                           </div>
                                           <div class="col-md-4 col-sm-6 col-xs-12">
                                                <h6>Fotos:</h6>
                                               <asp:DropDownList ID="ddFotos" runat="server" CssClass="form-control" >
                                                   <asp:ListItem>Todos</asp:ListItem>
                                               </asp:DropDownList>
                                           </div>
                                      </div>
                                   
                                      <div  class="row panel panel-primary col-md-15 col-sm-18 col-xs-36">
                                              <div class="btn-primary text-center"  >
                                                  <h8>Numero de folio</h8>
                                              </div>
                                               
                                              <div  class=" panel-body ">
                                                   <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorNoFolioRangoMenor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem>(>)  Mayor que</asp:ListItem>
                                                           <asp:ListItem>(>=) Mayor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div  class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango minimo</h6>
                                                      <asp:TextBox ID="txtBusquedaNoFolioEntre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 2" />
                                                  </div>
                                                  <%--<div style="padding: 0px;" class="col-md-1 col-sm-12 col-xs-12" >--%>
                                                        <%--<label class="text-center" >Y</label>--%>
                                                  <%--</div>--%>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorNoFolioRangoMayor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem>(<) Menor que</asp:ListItem>
                                                           <asp:ListItem> (<=) Menor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango maximo</h6>
                                                     <asp:TextBox ID="txtBusquedaFolioY" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 23" />
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
                                                           <asp:ListItem>(>)  Mayor que</asp:ListItem>
                                                           <asp:ListItem>(>=) Mayor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div  class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango minimo</h6>
                                                      <asp:TextBox ID="txtBusquedaCantFotosEntre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 2" />
                                                  </div>
                                                  <%--<div style="padding: 0px;" class="col-md-1 col-sm-12 col-xs-12" >--%>
                                                        <%--<label class="text-center" >Y</label>--%>
                                                  <%--</div>--%>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorCantFotosRangoMayor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem>(<) Menor que</asp:ListItem>
                                                           <asp:ListItem> (<=) Menor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango maximo</h6>
                                                     <asp:TextBox ID="txtBusquedaCantFotosY" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 23" />
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
                                                           <asp:ListItem>(>)  Mayor que</asp:ListItem>
                                                           <asp:ListItem>(>=) Mayor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div  class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango minimo</h6>
                                                      <asp:TextBox ID="txtBusquedaImporteEntre" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 2" />
                                                  </div>
                                                  <%--<div style="padding: 0px;" class="col-md-1 col-sm-12 col-xs-12" >--%>
                                                        <%--<label class="text-center" >Y</label>--%>
                                                  <%--</div>--%>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorImporteRangoMayor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem>(<) Menor que</asp:ListItem>
                                                           <asp:ListItem> (<=) Menor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Rango maximo</h6>
                                                     <asp:TextBox ID="txtBusquedaImporteY" MaxLength="50" runat="server" CssClass="form-control" placeholder="Ej.: 23" />
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
                                                           <asp:ListItem>(>) Mayor que</asp:ListItem>
                                                           <asp:ListItem>(>=) Mayor o igual que</asp:ListItem>
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
                                                           <asp:ListItem>(<) Menor que</asp:ListItem>
                                                           <asp:ListItem> (<=) Menor o igual que</asp:ListItem>
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
                                                           <asp:ListItem>(>) Mayor que</asp:ListItem>
                                                           <asp:ListItem>(>=) Mayor o igual que</asp:ListItem>
                                                       </asp:DropDownList>
                                                  </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                    <h6 runat="server" >Rango minimo:</h6>
                                                    <div class="input-group date">
                                                        <asp:TextBox ID="txtBusquedaMinFechaCierre" runat="server" CssClass="form-control"  placeholder=" Ejemplo: 13/09/1994" />
                                                        <div class="input-group-addon">
                                                            <span class="glyphicon glyphicon-calendar"></span>
                                                        </div>
                                                    </div>
                                                 </div>
                                                  <div class="col-md-3 col-sm-6 col-xs-12">
                                                       <h6 runat="server" >Operador</h6>
                                                        <asp:DropDownList ID="ddOperadorFechaCierreRangoMayor" runat="server" CssClass="form-control" >
                                                           <asp:ListItem>(<) Menor que</asp:ListItem>
                                                           <asp:ListItem> (<=) Menor o igual que</asp:ListItem>
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

                                  </asp:PlaceHolder>

                                  <div class="row" style="padding-top: 10px;">
                                        <div class="col-lg-12">
                                            <asp:GridView ID="dgvHistoricos" runat="server" PageSize="10" CssClass="table table-bordered table-hover" AutoGenerateColumns="false" OnRowDataBound="dgvHistoricos_RowDataBound" OnSelectedIndexChanged="dgvHistoricos_SelectedIndexChanged" DataKeyNames="UidSucursal" OnSorting="dgvHistoricos_Sorting" AllowPaging="true" AllowSorting="true" OnPageIndexChanging="dgvHistoricos_PageIndexChanging">
                                                <PagerSettings Mode="NumericFirstLast" Position="Top" PageButtonCount="4" />
                                                <PagerStyle CssClass="pagination-ys" HorizontalAlign="Center" />
                                                <EmptyDataTemplate>
                                                    No hay historicos  de ventas registradas
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
                          </asp:PlaceHolder>
                            
                      </div>
                 </div>
             </asp:PlaceHolder>
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
             $(".input-group.time").clockpicker({
                 'default': 'now', 
                 autoclose: true,
                 //twelvehour: true,

                //donetext: 'Listo',
                 language: 'es',
                 
            });
        }
        //]]>
    </script>
</asp:Content>
