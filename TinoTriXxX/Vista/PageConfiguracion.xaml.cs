using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TinoTriXxX.VistaModelo;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para PageConfiguracion.xaml
    /// </summary>
    public partial class PageConfiguracion : Page
    {
        VM_Escritorio VM = new VM_Escritorio();
        MainWindow parentWindow;
        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        public PageConfiguracion(VM_Escritorio vm)
        {
            InitializeComponent();
            VM = vm;

            if (VM.IpServidor == "" )
            {
                VM.ObtenerIPServidor();
                //if (VM.Connection == null || VM.Connection.State == ConnectionState.Disconnected)
                //{
                //    VM.ConnectAsync();
                //}
            }
            TxtIpServer.Text = VM.IpServidor;

            if (VM.PuertoConexion == "")
            {
                VM.ObtenerPuerto();
                //if (VM.Connection == null || VM.Connection.State == ConnectionState.Disconnected)
                //{
                //    VM.ConnectAsync();
                //}
            }
            TxtPuertoServer.Text = VM.PuertoConexion;

            VM.ObtenerSession();
            if (VM.Session.UidUsusario == Guid.Empty)
            {
                //  MessageBox.Show("¡Inicia sesion primero!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                TxtIpServer.IsEnabled = false;
                TxtPuertoServer.IsEnabled = false;
            }
            else
            {
                TxtIpServer.IsEnabled = true;
                TxtPuertoServer.IsEnabled = true ;
            }

           
         }
        private void BtnGuardarConfServidor_Click(object sender, RoutedEventArgs e)
        {
            try {
                bool Todobien = true;
                //IPAddress ipAddress;
                //if (IPAddress.TryParse(TxtIpServer.Text, out ipAddress))
                //{
                    VM.ActualizarIpServidor(TxtIpServer.Text);
                    VM.IpServidor = TxtIpServer.Text;
                //}
                //else
                //{
                //    Todobien = false;
                //}
                int integer;
                if (int.TryParse(TxtPuertoServer.Text, out integer))
                {
                    VM.ActualizarPuerto(TxtPuertoServer.Text);
                    VM.PuertoConexion = TxtPuertoServer.Text;
                }
                else
                {
                    Todobien = false;
                }
                if(Todobien==true)
                {
                    if (VM.Connection != null )//|| VM.Connection.State != ConnectionState.Disconnected
                    {
                       VM.Connection.Stop();
                       VM.Connection.Dispose();
                    }
                    VM.ObtenerIPServidor(); VM.ObtenerPuerto();
                    VM.ServerURI = "http://" + VM.IpServidor + ":" + VM.PuertoConexion + "/signalr";
                    VM.ConnectAsync();
                }
            }
            catch (FileNotFoundException u)
            {
                MessageBox.Show(u.Message);

            }
            catch (DirectoryNotFoundException i)
            {
                MessageBox.Show(i.Message);

            }
            catch (IOException p)
            {
                MessageBox.Show(p.Message);

            }
            catch (Exception d) {
                MessageBox.Show("¡No se pudo guardar los cambios \n intente de nuevo! \n \n" + d, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            
            //else
            //{
            //    MessageBox.Show("¡No valida!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //    return;
            //}
            MessageBox.Show("Se guardo correctamente los cambios", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //PagePrincipal page = new PagePrincipal();
            //this.NavigationService.Navigate(page);
            parentWindow.MenuHome();
        }

        #region servicio
        private void BtnPingPrueba_Click(object sender, RoutedEventArgs e)
        {
            bool cx = PingHost(TxtIpServer.Text);
            if (cx==false) {
                MessageBox.Show("¡Sin conexion!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            } else {
                MessageBox.Show("¡Hay comunicacion!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
        public static bool PingHost(string nameOrAddress)
        {
            bool pingable = false;
            Ping pinger = null;

            try
            {
                pinger = new Ping();
                PingReply reply = pinger.Send(nameOrAddress);
                pingable = reply.Status == IPStatus.Success;
            }
            catch (PingException)
            {
                // Discard PingExceptions and return false;
            }
            finally
            {
                if (pinger != null)
                {
                    pinger.Dispose();
                }
            }

            return pingable;
        }

        //void SaberIpRed() {
        //    // Ciclo por todas las interfaces de red en este dispositivo:
        //    foreach (var interfaces in NetworkInterface.GetAllNetworkInterfaces())
        //    {
        //        // Direcciones de unicast asignadas a la interfaz de red actual:
        //        foreach (var direccion in interfaces.GetIPProperties().UnicastAddresses)
        //        {
        //            // Valida que se trate de una IPv4:
        //            if (direccion.Address.AddressFamily == AddressFamily.InterNetwork)
        //            {
        //                Console.WriteLine("Dirección IP privada: {0}", direccion.Address.ToString());
        //            }
        //        }

        //    }
        //}
        #endregion Servicio

        private void BtnDescargarConf_Click(object sender, RoutedEventArgs e)
        {
            try {
                if (VM.DescargarConfiguracion() == true)
                {
                    TxtIpServer.Text = VM.Servidor.StrNombreIP;
                    TxtPuertoServer.Text = VM.Servidor.StrPuerto;
                }
                else {
                    MessageBox.Show("No hay configuracion que descargar", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
            }
            catch (FileNotFoundException f)
            {
                MessageBox.Show(f.Message);

            }
            catch (DirectoryNotFoundException f)
            {
                MessageBox.Show(f.Message);

            }
            catch (IOException f)
            {
                MessageBox.Show(f.Message);

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow.txtIpServer = TxtIpServer;
            parentWindow.txtPuertoServer = TxtPuertoServer;
        }
    }
}
