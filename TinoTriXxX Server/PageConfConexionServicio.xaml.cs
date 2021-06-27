using System;
using System.Collections.Generic;
using System.IO;
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
    /// Lógica de interacción para PageConfConexionServicio.xaml
    /// </summary>
    public partial class PageConfConexionServicio : Page
    {
        VM_Escritorio VM = new VM_Escritorio();
        MainWindow parentWindow;
        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        public PageConfConexionServicio(VM_Escritorio vm)
        {
            VM = vm;
            InitializeComponent();
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

        private void BtnGuardarConfConexionServicio_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                bool Todobien = true;
                
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
                if (Todobien == true)
                {
                    if (VM.SignalR != null)//|| VM.Connection.State != ConnectionState.Disconnected
                    {
                        VM.SignalR.Dispose();
                    }
                    //VM.ObtenerIPServidor(); VM.ObtenerPuerto();
                    //VM.ServerURI = "http://" + VM.IpServidor + ":" + VM.PuertoConexion + "/signalr";
                    //VM.ConnectAsync();
                    Task.Run(() => ComienzoServicio());
                }
            }
            catch (Exception d)
            {
                MessageBox.Show("¡No se pudo guardar los cambios \n intente de nuevo!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            //else
            //{
            //    MessageBox.Show("¡No valida!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //    return;
            //}
            MessageBox.Show("Se guardo correctamente los cambios", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            parentWindow.MenuHome();

        }

        private void ComienzoServicio()
        {
            try
            {
                bool EstadoServicio = VM.StartServer();
                if (EstadoServicio == false)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                (window as MainWindow).LbEstadoServicioLocal.Text = "Desactivado";
                            }
                        }
                    }));
                }
                else
                {

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {

                        foreach (Window window in Application.Current.Windows)
                        {
                            if (window.GetType() == typeof(MainWindow))
                            {
                                (window as MainWindow).LbEstadoServicioLocal.Text = "Activado";
                            }
                        }
                    }));
                }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).LbNombreIPServidor.Text = VM.localIP;
                            (window as MainWindow).LbPuertoServidor.Text = VM.PuertoConexion;
                        }
                    }
                }));
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                //Application.Current.Shutdown();

            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show(e.Message);
                // Application.Current.Shutdown();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                // Application.Current.Shutdown();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //Application.Current.Shutdown();
            }
        }
    }
}
