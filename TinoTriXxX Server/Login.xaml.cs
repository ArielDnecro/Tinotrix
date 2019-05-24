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
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        VM_Escritorio _CVMServer;
        public Login(VM_Escritorio vm)
        {
            InitializeComponent();
            _CVMServer = vm;
            txtEncargado.Focus();
        }

        private void txtEncargado_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                EncargadoEnter();
            }
        }
        void EncargadoEnter()
        {
            if (txtEncargado.Text != "" || txtEncargado.Text != string.Empty)
            {
                Guid idusuario = _CVMServer.ExisteEncargadoHost(txtEncargado.Text);
                if (idusuario == Guid.Empty)
                {
                    lbAvisoError.Content = "No existe Encargado";
                    lbAvisoError.Foreground = Brushes.Red;
                }
                else
                {
                    if (_CVMServer.usuariosucursal(idusuario, _CVMServer.Sucursal.UidSucursal) == false)
                    {
                        lbAvisoError.Content = "Este encargado no pertence \r\n a esta sucursal";
                        lbAvisoError.Foreground = Brushes.Red;
                    }
                    else
                    {
                        _CVMServer.ObteneEncargado(idusuario);
                        lbAvisoError.Content = "Bienvenido " + txtEncargado.Text.ToString().ToUpper() + " \r\n ingrese contraseña";
                        lbAvisoError.Foreground = Brushes.DarkSlateGray;

                        PnEncargado.Visibility = Visibility.Hidden;
                        PnPassword.Visibility = Visibility.Visible;
                        txtcontrasena.Focus();
                        btnRegresarUsuario.Visibility = Visibility.Visible;
                    }
                }
            }
            else {
                lbAvisoError.Content = "Ingrese Usuario";
                lbAvisoError.Foreground = Brushes.Red;
            }
        }

        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            _CVMServer.Usuario = null;
            this.Close();
        }
        private void btnRegresarUsuario_Click(object sender, RoutedEventArgs e)
        {
            //_CVMServer.Usuario = null;
            lbAvisoError.Content = "Ingrese su encargado";
            lbAvisoError.Foreground = Brushes.DarkSlateGray;
           PnEncargado.Visibility = Visibility.Visible;
            PnPassword.Visibility = Visibility.Hidden;
            txtEncargado.Focus();
            btnRegresarUsuario.Visibility = Visibility.Hidden;
        }

        private void txtcontrasena_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                contrasenaenter();
            }
        }
        void contrasenaenter()
        {
            if (txtcontrasena.Password.ToString() != string.Empty /*&& txtcontrasena.Password.ToString() != string.Empty*/)
            {

                if (_CVMServer.Encargado.STRPASSWORD != txtcontrasena.Password.ToString())
                {
                    lbAvisoError.Content = "Contraseña incorrecta";
                    lbAvisoError.Foreground = Brushes.Red;
                }
                else
                {
                    DateTime saveNow = DateTime.Now;
                    DateTime myDt;
                    myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
                    Guid uidfolio =  Guid.NewGuid();
                    
                    _CVMServer.IniciarTurno(_CVMServer.Sucursal.UidSucursal,uidfolio, _CVMServer.Encargado.UIDUSUARIO, myDt.ToString("T"), myDt.ToString("d"));
                    _CVMServer.ActualizarTurno(_CVMServer.Encargado.UIDUSUARIO);
                    this.Close();
                }
            }
        }
    }
}
