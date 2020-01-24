<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="CodorniX.Vista.Login" %>

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
    

</head>
<body>
   
        <!-- Top content  Orden de servicio automatizado-->
        <div class="top-content">
            <div ><%--inner-bg--%>
                <div class="container">
                    <div class="row">
                        <div class="col-sm-8 col-sm-offset-2 text">
                            <h1><strong>TINOTRIX</strong></h1>
                            <div class="description">
                            	<p>
                                    GESTION DE SERVICIO FOTOGRAFICO
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
			                    <form role="form"  method="post" class="login-form" runat="server">
			                    	<div class="form-group">
                                        <asp:TextBox ID="txtUsuario" runat="server" Font-Size="18" CssClass="input-group-lg form-password form-control" placeholder="Correo o Usuario" type="text" >
                                        </asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <asp:TextBox ID="txtPassword" runat="server" Font-Size="18" CssClass="input-group-lg form-password form-control" placeholder="Contraseña" type="password" >
                                        </asp:TextBox>
                                        <asp:Label ID="lblMensaje" runat="server" ForeColor="Red"></asp:Label>
                                    </div>
			                        <%--<div class="form-group">--%>
                                        <asp:Button ID="btnLogin"  OnClick="btnLogin_Click" CssClass="btn btn-primary navbar   form-control "    runat="server" Text="Iniciar"  Font-Size="X-Large" />
                                    <%--</div>--%>
                                    <div  class="row col-md-15 col-sm-36 col-xs-36 "  style="  margin-bottom:0px; margin-left:-50px; margin-right:-50px; margin-top:0px; ">
                                        <div class="col-md-6 col-sm-12 col-xs-12"  style="margin-bottom:0px; margin-left:0px; margin-right:0px; margin-top:0px; padding:0px;">
                                                <asp:Panel ID="Panel1"     runat="server">
                                        
                                                </asp:Panel>
                                        </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12   " style="margin:0px; padding:0px;" >
                                              <asp:LinkButton  ID="btnDescargarServidor" runat="server"   CssClass="btn btn-primary   form-control "  Height="76"  Font-Size="20"  BorderColor="Gold" BorderWidth="1"     ToolTip="Descargar tinotrix servidor"   ForeColor="White" OnClick="btnDescargarServidor_Click"   >
                                                 <div runat="server" aria-orientation="horizontal" style=" vertical-align:central;">
                                                      <img src="Imagenes/logotiposerver2019.3.3.150x150.png" height="64" width="64" style="" />
                                                      <span class="glyphicon glyphicon-download " runat="server"  ></span>
                                                 </div>
                                             </asp:LinkButton>
                                         </div>
                                         <div class="col-md-3 col-sm-12 col-xs-12"   style=" padding:0px; " >
                                           <asp:LinkButton  ID="btnDescargarCliente" runat="server"   style="margin:0px; "   BorderColor="LightGreen" BorderWidth="1" Height="76"   CssClass=" btn btn-success form-control"  Font-Size="20"       ToolTip="Descargar tinotrix cliente" ForeColor="White"  OnClick="btnDescargarCliente_Click"   >
                                                 <div runat="server" aria-orientation="horizontal" style="padding:0px; vertical-align:central;">
                                                      <img src="Imagenes/logotipo2019.3.0.150x150.png" height="64" width="64" style="padding:0px;" />
                                                      <span class="glyphicon glyphicon-download " runat="server"  ></span>
                                                 </div>
                                           </asp:LinkButton>
                                         </div>
                                    </div>
                                   
                                    <%--<div class="form-group" runat="server">
                                       <asp:LinkButton runat="server" CssClass="form-control text-center  " BackColor="Transparent" Font-Size="12" ForeColor="White"  Text="Descargar tinotrix server" PostBackUrl="http://www.compuandsoft.com/Tinotrix/setup_Tinotrix_servidor.rar" ></asp:LinkButton>
                                      
                                    </div>
                                     <div class="form-group" runat="server">
                                          <asp:LinkButton runat="server" CssClass="form-control text-center" BackColor="Transparent" Font-Size="12" ForeColor="White"  Text="Descargar tinotrix cliente" PostBackUrl="http://www.compuandsoft.com/Tinotrix/setup_Tinotrix_cliente.rar" ></asp:LinkButton>
                                     </div>--%>
                                     
			                    </form>
		                    </div>
                        </div>
                    </div>
                </div>
            </div>
           
        </div>
    <script src="../Assets/Login/jquery-1.11.1.min.js"></script>
    <script src="../Assets/Login/bootstrap.min.js"></script>
    <script src="../Assets/Login/jquery.backstretch.min.js"></script>
    <script src="../Assets/Login/scripts.js"></script>
</body>
</html>
