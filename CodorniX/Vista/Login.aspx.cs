using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CodorniX.VistaDelModelo;
using System.Data.SqlClient;
using CodorniX.Modelo;

namespace CodorniX.Vista
{
    public partial class Login : System.Web.UI.Page
    {
        VMLogin _CVMLogin = new VMLogin();
        string strperfil = "";
        GoogleReCaptcha.GoogleReCaptcha ctrlGoogleReCaptcha = new GoogleReCaptcha.GoogleReCaptcha();

        protected override void CreateChildControls()
        {
            ctrlGoogleReCaptcha.Width = 10;
            base.CreateChildControls();
            ctrlGoogleReCaptcha.PublicKey = "6LfVDgMTAAAAAJnH9GV0i7r_Pl3FfC_hyfh2Cgnq";
            ctrlGoogleReCaptcha.PrivateKey = "6LfVDgMTAAAAAPfTlH1n7z72CvS46c2C_qkTmFsZ";
            this.Panel1.Controls.Add(ctrlGoogleReCaptcha);
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Sesion"] != null)
            {
                var SesionActual = (Sesion)Session["Sesion"];
                Response.Redirect(Acceso.ObtenerHomePerfil(SesionActual.uidPerfilActual.Value), false);
                return;
            }
        }
        public bool loguear = false;

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Sesion sesion = new Sesion();
            if (txtUsuario.Text != string.Empty && txtPassword.Text != string.Empty)
            {
                Guid idusuario = _CVMLogin.IniciarSesion(txtUsuario.Text);

                if (idusuario != Guid.Empty)
                {
                    Usuario usuario = new Usuario.Repository().Find(idusuario);

                    if (usuario.STRPASSWORD != txtPassword.Text)
                    {
                        lblMensaje.Text = "La contraseña es incorrecta";
                        return;
                    }

                    Status.Repository statusRepository = new Status.Repository();
                    Perfil.Repositorio perfilRepository = new Perfil.Repositorio();

                    Status status = statusRepository.Find(usuario.UidStatus);

                    if (status.strStatus != "Activo")
                    {
                        lblMensaje.Text = "El usuario está inactivo";
                        return;
                    }

                    sesion.uidUsuario = idusuario;

                    List<UsuarioPerfilEmpresa> ep = new UsuarioPerfilEmpresa.Repository().FindAll(idusuario);
                    if (ep.Count > 0)
                    {
                        List<Guid> uidEmpresas = (from em in ep select em.UidEmpresa).ToList();
                        List<Guid> uidPerfiles = (from pf in ep select pf.UidPerfil).ToList();
                        sesion.uidEmpresasPerfiles = uidEmpresas;
                        sesion.uidEmpresaActual = uidEmpresas[0];
                        sesion.uidPerfilActual = uidPerfiles[0];
                        sesion.uidNivelAccesoActual = perfilRepository.CargarDatos(sesion.uidPerfilActual.Value).UidNivelAcceso;
                    }
                    else
                    {
                        List<UsuarioPerfilSucursal> sp = new UsuarioPerfilSucursal.Repository().FindAll(idusuario);
                        if (sp.Count > 0)
                        {
                            List<Guid> uidSucursales = (from su in sp select su.UidSucursal).ToList();
                            List<Guid> uidPerfiles = (from pf in sp select pf.UidPerfil).ToList();
                            sesion.uidSucursalesPerfiles = uidSucursales;
                            sesion.uidSucursalActual = uidSucursales[0];
                            sesion.uidEmpresaActual = new Sucursal.Repository().Find(uidSucursales[0]).UidEmpresa;
                            sesion.uidPerfilActual = uidPerfiles[0];
                        }
                        else
                        {
                            lblMensaje.Text = "El usuario no tiene empresa ni sucursal asignados";
                            return;
                        }
                    }

                    strperfil = Acceso.ObtenerAppWeb(sesion.uidPerfilActual.Value);
                    Session["Sesion"] = sesion;

                    if (strperfil == "Backsite")
                    {
                        Response.Redirect("HomeBS.aspx", false);
                        return;
                    }
                    else if (strperfil == "Backend")
                    {
                        Response.Redirect("Home.aspx", false);
                        return;
                    }
                    else if (strperfil == "Frontend")
                    {
                        Response.Redirect("Bienvenido.aspx", false);
                        return;
                    }
                }
                else
                {
                    lblMensaje.Text = "No estás registrado.";
                }
            }

        }

        protected void btnDescargarServidor_Click(object sender, EventArgs e)
        {
            if (ctrlGoogleReCaptcha.Validate())
            {
                Response.Redirect("http://www.compuandsoft.com/Tinotrix/setup_Tinotrix_servidor.rar", false);
                return;
            }
            else
            {

            }
        }

        protected void btnDescargarCliente_Click(object sender, EventArgs e)
        {
            if (ctrlGoogleReCaptcha.Validate())
            {
                Response.Redirect("http://www.compuandsoft.com/Tinotrix/setup_Tinotrix_cliente.rar", false);
                return;
            }
            else
            {
               
            }
        }
    }
}