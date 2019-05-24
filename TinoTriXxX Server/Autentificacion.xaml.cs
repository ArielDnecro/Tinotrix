using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TinoTriXxX.VistaModelo;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para Autentificacion.xaml
    /// </summary>
    public partial class Autentificacion : Window
    {
        
        VM_Escritorio _CVMServer;
        //string strperfil = "";

        public Autentificacion(VM_Escritorio vm)
        {
            InitializeComponent();
            _CVMServer = vm;
            txtusuario.Focus();
        }
        
        private void btnEntrar_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void txtusuario_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                usuarioenter();
            }
        }
        void usuarioenter() {

            if (txtusuario.Text != string.Empty /*&& txtcontrasena.Password.ToString() != string.Empty*/)
            {

                Guid idusuario = _CVMServer.IniciarSesion(txtusuario.Text);
                 _CVMServer.ObtenerEmpresaLocal();
                if (idusuario == Guid.Empty)
                {
                    lbAvisoError.Content = "No existe usuario";
                    lbAvisoError.Foreground = Brushes.Red;
                }
                else
                {
                    //_CVMServer.asignarIDUsuario(idusuario);

                    if (_CVMServer.usuarioempresa(idusuario, _CVMServer.Empresa.UidEmpresa) == false )
                    {
                        lbAvisoError.Content = "Este usuario no pertenece \r\n a esta empresa";
                        lbAvisoError.Foreground = Brushes.Red;
                    }
                    else
                    {
                            _CVMServer.ObteneUsuario(idusuario);
                            lbAvisoError.Content = "Bienvenido " + txtusuario.Text.ToString().ToUpper() + " \r\n ingrese contraseña";
                            lbAvisoError.Foreground = Brushes.DarkSlateGray;
                            PnUser.Visibility = Visibility.Hidden;
                            PnPassword.Visibility = Visibility.Visible;
                            txtcontrasena.Focus();
                            btnRegresarUsuario.Visibility = Visibility.Visible;
                    }
                }
                _CVMServer.EmpresaLocal = null;
            }
        }
        private void txtcontrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                contrasenaenter();
            }
        }
        void contrasenaenter() {
            if (txtcontrasena.Password.ToString() != string.Empty /*&& txtcontrasena.Password.ToString() != string.Empty*/)
            {

                if (_CVMServer.Usuario.STRPASSWORD != txtcontrasena.Password.ToString())
                {
                    lbAvisoError.Content = "Contraseña incorrecta";
                    lbAvisoError.Foreground = Brushes.Red;
                }
                else
                {
                    _CVMServer.ActualizarSession(_CVMServer.Usuario.UIDUSUARIO);
                    this.Close();
                }
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            _CVMServer.Usuario = null;
            this.Close();
        }

        private void btnRegresarUsuario_Click(object sender, RoutedEventArgs e)
        {
            _CVMServer.Usuario= null;
            lbAvisoError.Content = "Ingrese su usuario";
            lbAvisoError.Foreground = Brushes.DarkSlateGray;
            PnUser.Visibility = Visibility.Visible;
            PnPassword.Visibility = Visibility.Hidden;
            txtusuario.Focus();
            btnRegresarUsuario.Visibility = Visibility.Hidden;
        }
    }
}
