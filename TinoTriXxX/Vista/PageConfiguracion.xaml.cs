using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
        }

        private void BtnGuardarConfServidor_Click(object sender, RoutedEventArgs e)
        {
            try {
                bool Todobien = true;
                IPAddress ipAddress;
                if (IPAddress.TryParse(TxtIpServer.Text, out ipAddress))
                {
                    VM.ActualizarIpServidor(TxtIpServer.Text);
                    VM.IpServidor = TxtIpServer.Text;
                }
                else
                {
                    Todobien = false;
                }
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
                       VM. Connection.Dispose();
                    }
                    VM.ObtenerIPServidor(); VM.ObtenerPuerto();
                    VM.ServerURI = "http://" + VM.IpServidor + ":" + VM.PuertoConexion + "/signalr";
                    VM.ConnectAsync();
                }
            } catch (Exception d) {
                MessageBox.Show("¡No se pudo guardar los cambios \n intente de nuevo!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            
            //else
            //{
            //    MessageBox.Show("¡No valida!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //    return;
            //}
            MessageBox.Show("Se guardo correctamente los cambios", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
        }
    }
}
