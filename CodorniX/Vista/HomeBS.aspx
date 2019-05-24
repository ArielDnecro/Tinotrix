<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="HomeBS.aspx.cs" Inherits="CodorniX.Vista.HomeBS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Página Inicio</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
        <div class="col-xs-12 col-md-6"  hidden="hidden" >
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Inicio
                </div>
                <div class="panel-body">
                    <p>Bienvenido a TinoTriX</p>
                </div>
            </div>
        </div>

        <div class="col-xs-12 col-md-6  form-box col-sm-offset-3" >
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Bienvenido al inicio de tinotrix
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4 col-md-3  text-left">
                            <a href="Usuarios.aspx">
                                <img src="../Images/users.png" class="menu-icon" />
                                <br />
                                <strong>Usuarios</strong>
                            </a>
                        </div>
                        <div class="col-xs-4 col-md-3 text-left">
                            <a href="Empresas.aspx">
                                <img src="../Images/building.png" class="menu-icon" />
                                <br />
                                <strong>Empresas</strong>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
