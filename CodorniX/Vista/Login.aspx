﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CodorniX.Vista.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <%--<link href="../Content/bootstrap-theme.css" rel="stylesheet" />
    <link href="../Content/bootstrap.css" rel="stylesheet" />
    <script src="../Scripts/jquery-3.1.1.js"></script>
    <script src="../Scripts/bootstrap.js"></script>--%>

     <link rel="stylesheet" href="http://fonts.googleapis.com/css?family=Roboto:400,100,300,500" />
     <link rel="stylesheet" href="../Assets/Login/bootstrap.min.css"  />
     <link rel="stylesheet" href="../Assets/Login/font-awesome.min.css" />
     <link rel="stylesheet" href="../Assets/Login/form-elements.css" />
     <link rel="stylesheet" href="../Assets/Login/style.css" />
     <script src="../Assets/Login/jquery-1.11.1.min.js"></script>
    <script src="../Assets/Login/bootstrap.min.js"></script>
    <script src="../Assets/Login/jquery.backstretch.min.js"></script>
    <script src="../Assets/Login/scripts.js"></script>
</head>
<body>
    <form  id="idPag" runat="server">
        <!-- Top content -->
        <div class="top-content">
            <div class="inner-bg">
                <div class="container">
                    <div class="row">
                        <div class="col-sm-8 col-sm-offset-2 text">
                            <h1><strong>Tinotrix</strong></h1>
                            <div class="description">
                            	<p>
                                    Orden de servicio automatizado
                            	</p>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-6 col-sm-offset-3 form-box">
                        	<div class="form-top">
                        		<div class="form-top-left">
                        			<h3>¡Bienvenido!</h3>
                            		<p>Inicio de sesion en Tinotrix:</p>
                        		</div>
                        		<div class="form-top-right">
                        			<i class="fa fa-lock"></i>
                        		</div>
                            </div>
                            <div class="form-bottom">
			                   <!-- <form role="form" class="login-form">!-->
			                    	<div class="form-group">
                                        <asp:TextBox ID="txtUsuario" runat="server" CssClass="input-group-lg form-password form-control" placeholder="Correo o Usuario" type="text" >
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPassword" runat="server" CssClass="input-group-lg form-password form-control" placeholder="Contraseña" type="password" >
                                        </asp:TextBox>
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
			                        
                                    <asp:Button ID="btnLogin"  OnClick="btnLogin_Click" CssClass="btn btn-sm btn-default btn-primary form-control"   runat="server" Text="Iniciar Sesión" Font-Bold="True" />
                                
			                    <!--</form>!-->
		                    </div>
                        </div>
                    </div>
                </div>
            </div>
           
        </div>
        </form>
</body>
</html>
