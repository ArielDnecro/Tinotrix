<%@ Page Title="" Language="C#" MasterPageFile="~/Vista/Site1.Master" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="CodorniX.Vista.Home" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Home</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContenidoDelSitio" runat="server">
    <div class="row">
        <div class="col-xs-12 col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Inicio
                </div>
                <div class="panel-body">
                    <p>Bienvenido a CodorniX</p>
                </div>
            </div>
        </div>
        <div class="col-xs-12 col-md-6">
            <div class="panel panel-primary">
                <div class="panel-heading text-center">
                    Inicio
                </div>
                <div class="panel-body">
                    <div class="row">
                        <div class="col-xs-4 col-md-3 text-center">
                            <a href="Sucursales.aspx">
                                <img src="../Images/mod_empresas.png" class="menu-icon" />
                                <br />
                                <strong>Sucursales</strong>
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
