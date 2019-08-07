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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TinoTriXxX.VistaModelo;
using System.Windows.Threading;
using System.Windows.Media.Animation;
using System.Threading;
using Microsoft.Win32;
using System.Data.SqlClient;
using TinoTriXxX.Vista;
using TestLib;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.IO;
using Servidor;
using System.Reflection;
using Microsoft.Owin.Hosting;
using Owin;
using Microsoft.Owin.Cors;
using Microsoft.AspNet.SignalR;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        VM_Escritorio VM = new VM_Escritorio();
        Color TemaAzulEstandar = (Color)ColorConverter.ConvertFromString("#FF3580BF");
        Color TemaDoradoEstandar = (Color)ColorConverter.ConvertFromString("#ffc107");
        Boolean CumpleConTodoRequisito;
        bool arranquesesionapp = false;//con esta variable controlo la forma en que se cierra la ventana de inicio de sesion, se cierra solo la ventana o la app completa

        //private TcpListener server;
        //private TcpClient client = new TcpClient();
        //private IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 8000);
        //private List<Connection> list = new List<Connection>();

        //Connection con;
        //private struct Connection
        //{
        //    public NetworkStream stream;
        //    public StreamWriter streamw;
        //    public StreamReader streamr;
        //    public string nick;
        //}

        public IDisposable SignalR { get; set; }
        const string ServerURI = "http://127.0.0.1:8000";
        string localIP = "";
        public MainWindow()
        {
            AplicarCultura();
            RedireccionarBasico();


            InitializeComponent();
            //arranquesesionapp = true;
            frame.NavigationService.Navigate(new PagePrincipal());
            acceso();
            
            bframe.Visibility = Visibility.Visible;
            blicencia.Visibility = Visibility.Hidden;

            ////VariablesGlobal.FontName = "Arial";
            //VariablesGlobal.FontSize = 6.5F;
            //VariablesGlobal.FontSizeItemIndividual = 8;
            //VariablesGlobal.FontSizePosicionLlave = 10;
            ////VariablesGlobal.FontSizeCierreTurno = 7.5F;
            //VariablesGlobal.WidthImagen = 180;
            //VariablesGlobal.HeightImagen = 40;
            //VariablesGlobal.CentrarGoParkiX = "        ";

            VariablesGlobal.FontName = "Lucida Console";
            VariablesGlobal.FontSize = 9F;
            VariablesGlobal.FontSizeItemIndividual = 9;
            VariablesGlobal.FontSizePosicionLlave = 12F;
            VariablesGlobal.FontSizeCierreTurno = 9;
            VariablesGlobal.WidthImagen = 270;
            VariablesGlobal.HeightImagen = 50;
            VariablesGlobal.CentrarGoParkiX = "          ";

            //SaberIpServidor();

            Task.Run(() => StartServer());
        }

        #region interaccion en la red
        private void AplicarCultura()
        {
            System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("ES-Mx");
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
        }
        public void RedireccionarBasico()
        {
            string sourceRegistro = string.Empty;
            try
            {
                sourceRegistro = Registry.GetValue(@"HKEY_CURRENT_USER\TinotrixServer", "Source", "NULL").ToString();
            }
            catch (Exception) { sourceRegistro = string.Empty; }

            //Validar si el registro no existe o tiene un valor nulo 
            if (!string.IsNullOrEmpty(sourceRegistro))
            {
                //Prueba la conexión con el source guardado en el registro de windows 
                if (PruebaConexionRegistro(sourceRegistro))
                {
                    TinoTriXxX.Properties.Settings.Default["Source"] = sourceRegistro;
                }
                else
                {
                    DBLocal wBDLocal = new DBLocal();
                    wBDLocal.ShowDialog();
                }
            }
            //Validar si el registro existe o tiene un valor nulo 
            else
            {
                DBLocal wBDLocal = new DBLocal();
                wBDLocal.ShowDialog();
            }
        }
        public bool PruebaConexionRegistro(string source)
        {
            int intentos = 3;
            bool aux = false;
            SqlConnection _sqlConeccion;
            string stringConnection = string.Empty;

            stringConnection = @"Data Source=" + source + ";Initial Catalog=TinotrixServer;Integrated Security=True;Connection Timeout=1";

            for (int i = 0; i < intentos; i++)
                try
                {
                    _sqlConeccion = new SqlConnection(stringConnection);
                    _sqlConeccion.Open();
                    aux = true;
                    _sqlConeccion.Close();
                    break;
                }
                catch (Exception) { }

            return aux;
        }
        void acceso()
        {
            Boolean valido = ComprovarValidacionLicencia();

            #region error 71218
            //if (valido == true)
            //{

            //    if (VM.Usuario == null)
            //    {
            //        time.Interval = TimeSpan.FromSeconds(2);
            //        time.Tick += Time_Tick;
            //        time.Start();
            //    }
            //    else
            //    {
            //        time.Stop();
            //        AplicarEfecto(this, 0);

            //    }
            //}
            //else
            //{

            //}
            #endregion
        }
        #endregion interaccion en la red

        #region Eventos de la vista
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //InicioConexionRedLocal();
            //this.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(delegate { InicioConexionRedLocal();  }));
            //Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate { InicioConexionRedLocal(); }));
            //Servidor_Chat chat = new Servidor_Chat();

        }
        private void AplicarEfecto(Window win, int NivelDegradado)
        {
            var objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = NivelDegradado;
            win.Effect = objBlur;
        }
        //private void BtnSesion_Click(object sender, RoutedEventArgs e)
        //{
        //    Application.Current.Shutdown();
        //}
        void TemaAppDesabilitado()
        {
            GridMain.Visibility = Visibility.Hidden;
            //sPbtnsMenuPrincipal.Visibility = Visibility.Hidden;
            //OpenMenuclick();
            GridBarraEstado.Visibility = Visibility.Hidden;
            BtnOpenMenu.Background = Brushes.White;
            IcoOpenMenu.Foreground = new SolidColorBrush(TemaAzulEstandar);
            CMenu.Background = Brushes.White;
            popmenu.Foreground = new SolidColorBrush(TemaAzulEstandar);
            LVIMenu.IsEnabled = false;
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = Brushes.Transparent;
        }
        void TemaAppHabilitado()
        {
            GridMain.Visibility = Visibility.Visible;
            sPbtnsMenuPrincipal.Visibility = Visibility.Visible;
            GridBarraEstado.Visibility = Visibility.Visible;

            LVIMenu.IsEnabled = true;
            LVIMenu.Background = new SolidColorBrush(TemaDoradoEstandar); //esta parte se movera debido a que es llamado por varias funciones
            //sPMenu.Children.Add(btniniciarturno);
            //VM.ObtenerSession();
            //if (VM.Session.UidUsusario == Guid.Empty)
            //{
            //    Autentificacion au = new Autentificacion(VM, arranquesesionapp);
            //    au.ShowDialog();
            //    //SessionConf();
            //}
        }
        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuclick();

        }
        void OpenMenuclick()
        {
            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;

            BtnCloseMenu.Background = Brushes.White;
            IcoCloseMenu.Foreground = new SolidColorBrush(TemaAzulEstandar);

            CMenu.Background = Brushes.White;
            popmenu.Foreground = new SolidColorBrush(TemaAzulEstandar);

            
        }
        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenuClick();
        }
        void CloseMenuClick() {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;

            BtnOpenMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            IcoOpenMenu.Foreground = Brushes.White;

            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenu.Foreground = Brushes.White;
        }
        private void btnCerrarApp_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            Application.Current.Shutdown();
        }
        private void btnMinimizarApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void BtnMenuLicencia_Click(object sender, RoutedEventArgs e)
        {
            //imprimirturno();
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = new SolidColorBrush(TemaDoradoEstandar);
            LVICliente.Background = Brushes.Transparent;
            LVIConfiguracion.Background = Brushes.Transparent;

            GridMain.Visibility = Visibility.Visible;
            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenu.Foreground = Brushes.White;
            //frame.NavigationService.Navigate(new PageLicencia());
            blicencia.Visibility = Visibility.Visible;
            bframe.Visibility = Visibility.Hidden;
            cerrarmenu();
            
        }
        private void BtnMenuHome_Click(object sender, RoutedEventArgs e)
        {
            LVIMenu.Background = new SolidColorBrush(TemaDoradoEstandar);
            LVILicencia.Background = Brushes.Transparent;
            LVICliente.Background = Brushes.Transparent;
            LVIConfiguracion.Background = Brushes.Transparent;
            frame.NavigationService.Navigate(new PagePrincipal());
            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenu.Foreground = Brushes.White;

            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            cerrarmenu();
            
        }
        private void BtnMenuFotografiasCliente_Click(object sender, RoutedEventArgs e)
        {
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = Brushes.Transparent;
            LVICliente.Background = new SolidColorBrush(TemaDoradoEstandar);
            LVIConfiguracion.Background = Brushes.Transparent;
            // frame.NavigationService.Navigate(new PagePrincipal());
            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenu.Foreground = Brushes.White;

            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            frame.NavigationService.Navigate(new PageFotosCliente());
            cerrarmenu();
        }
        private void BtnMenuImpresoras_Click(object sender, RoutedEventArgs e)
        {
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = Brushes.Transparent;
            LVICliente.Background = Brushes.Transparent;
            LVIConfiguracion.Background = new SolidColorBrush(TemaDoradoEstandar);
            // frame.NavigationService.Navigate(new PagePrincipal());
            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenu.Foreground = Brushes.White;

            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            frame.NavigationService.Navigate(new PageImpresoras());
            cerrarmenu();
        }
        private void BtnMenuConexionServidor_Click(object sender, RoutedEventArgs e)
        {
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = Brushes.Transparent;
            LVICliente.Background = Brushes.Transparent;
            LVIConfiguracion.Background = new SolidColorBrush(TemaDoradoEstandar);
            // frame.NavigationService.Navigate(new PagePrincipal());
            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenu.Foreground = Brushes.White;

            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            frame.NavigationService.Navigate(new PageConfConexionServicio());
            cerrarmenu();
        }
        void cerrarmenu()
        {
            Storyboard closemenu = this.Resources["CloseMenu"] as Storyboard;
            closemenu.Begin();
            CloseMenuClick();
        }
        #endregion Eventos de la vista

        #region Actualizar Licencia
        
        private void BtnFinalizarCancelarActualizacionLicencia_Click(object sender, RoutedEventArgs e)
        {
            CancelarActulizacionLicencia();
        }

        private void BtnComprovarLicencia_Click(object sender, RoutedEventArgs e)
        {
            ComprovarActulizacionLicencia();
            CancelarActulizacionLicencia();

        }
        private void BtnActualizarLicencia_Click(object sender, RoutedEventArgs e)
        {
            //HabilitarActualizacionLicencia();//06 oct 18
            VM.ObtenerLicenciaLocal();
            if (VM.LicenciaLocal.UidLicencia != Guid.Empty)
            {
                ComprovarValidacionLicencia();
            }
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = new SolidColorBrush(TemaDoradoEstandar);
        }
        void CancelarActulizacionLicencia()
        {
            LbLicenciaCodigo.Visibility = Visibility.Visible;
            LbTitleCodigo.Visibility = Visibility.Visible;
            txtLicenciaCodigo.Visibility = Visibility.Hidden;
            //txtLicenciaCodigo.Focus();
            //txtLicenciaCodigo.SelectionStart = 0;
            //txtLicenciaCodigo.SelectionLength = txtLicenciaCodigo.Text.Length;
            BtnActualizarLicencia.Visibility = Visibility.Visible;
            BtnComprovarLicencia.Visibility = Visibility.Hidden;

            VM.ObtenerLicenciaLocal();
            if (VM.LicenciaLocal.UidLicencia == Guid.Empty)
            {
                BtnRevocarLicencia.Visibility = Visibility.Hidden;
                //BtnActualizarLicencia.Content = "Introducir"; 06 de oct 18 
            }
            else {
                BtnRevocarLicencia.Visibility = Visibility.Visible;
                //BtnActualizarLicencia.Content = "Actualizar";
                BtnAgregarLicencia.Visibility = Visibility.Hidden;
            }
            


            BtnFinalizarCancelarActualizacionLicencia.Visibility = Visibility.Hidden;
            BtnFinalizarCancelarActualizacionLicencia.Content = "Cancelar";
        }
        void HabilitarActualizacionLicencia()
        {
            //materialDesign: HintAssist.Hint = "Actualiza tu Licencia aqui"--codigo de la vista para poner un anuncio arriba del textbox....es para no olvidarlo y utilizarlo mas adelante
            LbLicenciaCodigo.Visibility = Visibility.Hidden;
            //LbTitleCodigo.Visibility = Visibility.Hidden;
            txtLicenciaCodigo.Visibility = Visibility.Visible;
            txtLicenciaCodigo.Focus();
            txtLicenciaCodigo.SelectionStart = 0;
            txtLicenciaCodigo.SelectionLength = txtLicenciaCodigo.Text.Length;
            BtnActualizarLicencia.Visibility = Visibility.Hidden;
            BtnComprovarLicencia.Visibility = Visibility.Visible;
            BtnRevocarLicencia.Visibility = Visibility.Hidden;
            BtnFinalizarCancelarActualizacionLicencia.Visibility = Visibility.Visible;
        }

        public Boolean ComprovarValidacionLicencia()
        {
            CumpleConTodoRequisito = false;
            Brush azul = new SolidColorBrush(TemaAzulEstandar);
            VM.ObtenerLicenciaLocal();
            if (VM.LicenciaLocal.UidLicencia == Guid.Empty)
            {

                LbLicenciaCodigo.Text = "00000000-0000-0000-0000-000000000000";
                LbLicSta.Text = "No diponible";
                LbLicenciaNoMaquinas.Text = "No disponible";
                LbLicenciaStatus.Text = "No disponible";
                LbLicenciaNoPc.Text = "No disponible";
                LbLicenciaNombreSucursal.Text = "No disponible";
                LbLicenciaStatusSucursal.Text = "No disponible";
                LbLicenciaDireccionSucursal.Text = "No disponible";
                LbLicenciaNombreEmpresa.Text = "No disponible";

                BtnRevocarLicencia.Visibility = Visibility.Hidden;
               // BtnActualizarLicencia.Content = "Introducir";//06 oct 18
                BtnAgregarLicencia.Visibility = Visibility.Visible;
                LbLicenciaStatusSucursal.Foreground = azul;
                LbLicenciaStatus.Foreground = azul;

                TemaAppDesabilitado();
            }
            else
            {
                VM.IFExistsLicencia(VM.LicenciaLocal.UidLicencia);
                if (VM.Licencia.UidLicencia == Guid.Empty)
                {
                    RevocarLicencia();
                    ComprovarValidacionLicencia();
                }
                else
                {
                    VM.FindLicencia(VM.LicenciaLocal.UidLicencia, 0);
                    if (VM.Licencia.UidLicencia == Guid.Empty)
                    {
                        LbLicenciaCodigo.Text = VM.Licencia.UidLicencia.ToString();
                        LbLicSta.Text = "No disponible";
                        LbLicenciaNoMaquinas.Text = "No disponible";
                        LbLicenciaStatus.Text = "No disponible";
                        LbLicenciaNoPc.Text = "No disponible";
                        LbLicenciaNombreSucursal.Text = "No disponible";
                        LbLicenciaStatusSucursal.Text = "No disponible";
                        LbLicenciaDireccionSucursal.Text = "No disponible";
                        LbLicenciaNombreEmpresa.Text = "No disponible";
                    }
                    else
                    {
                        VM.ObtenerDatosSesionHosting();
                        VM.ActualizarEmpresaLocal(VM.Sucursal.UidEmpresa);

                        //Seccion de llenado de labels subProducto
                        //licencia
                        LbLicenciaCodigo.Text = VM.Licencia.UidLicencia.ToString();
                        LbLicenciaNoMaquinas.Text = VM.Licencia.IntNoTotal.ToString();
                        LbLicenciaNoPc.Text = VM.Licencia.IntNo.ToString();
                        LbLicenciaNombreSucursal.Text = VM.Sucursal.StrNombre.ToString();
                        LbLicenciaStatusSucursal.Text = VM.StatusSucursal.strStatus;
                        LbLicenciaDireccionSucursal.Text = "Calle " + VM.SucursalDireccion.StrCalle + " entre calle "
                        + VM.SucursalDireccion.StrConCalle + " y calle " + VM.SucursalDireccion.StrYCalle + " colonia "
                        + VM.SucursalDireccion.StrColonia + ", " + VM.SucursalDireccion.StrCiudad
                        ;

                        //Empresa
                        LbLicenciaNombreEmpresa.Text = VM.Empresa.StrNombreComercial;

                        if (VM.Licencia.BooStatus == false)
                        {
                            CumpleConTodoRequisito = false;
                            //LbMarcoSubProLicencia.Background = Brushes.Red;
                            LbLicenciaStatus.Foreground = Brushes.Red;
                            LbLicenciaStatus.Text = "Inactivo";
                            LbLicSta.Foreground = Brushes.Red;
                            LbLicSta.Text = "Inactivo";
                        }
                        else {
                            if (VM.StatusSucursal.strStatus == "Inactivo")
                            {
                                CumpleConTodoRequisito = false;
                                //LbMarcoSubProSucursal.Background = Brushes.Red;
                                LbLicenciaStatusSucursal.Foreground = Brushes.Red;
                                
                            }
                            else {
                                CumpleConTodoRequisito = true;
                            }
                        }
                        

                        if (CumpleConTodoRequisito == false)
                        {
                            TemaAppDesabilitado();
                        }
                        else
                        {
                          //////  DateTime saveNow = DateTime.Now;
                          //////  DateTime myDt;
                          //////  myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
                          //////lbFechaInicio.Text = myDt.ToString("d");
                          ////// lbHoraInicio.Text= myDt.ToString("T");

                            LbLicenciaStatusSucursal.Foreground = azul;
                            LbLicenciaStatus.Foreground = azul;
                            LbLicenciaStatus.Text = "Activo";
                            LbLicSta.Foreground = azul;
                            LbLicSta.Text = "Activo";
                            TemaAppHabilitado();
                        }

                    }
                }
            }
             SessionConf();//este paso no se hace porque ya esta en temaapphabilitado();
            TurnoConf();
            //prueba aun
            //VM.LicenciaLocal = null;
            //VM.Licencia = null;
            //VM.Sucursal = null;
            //VM.StatusSucursal = null;
            //VM.SucursalDirecciones = null;
            //VM.SucursalDireccion = null;
            //VM.Empresa = null;
            //VM.EmpresaLocal = null;
            return CumpleConTodoRequisito;
        }
        public void ComprovarActulizacionLicencia() {
          
            try
            {
                Guid licenciaNueva = new Guid(txtLicenciaCodigo.Text);

              
                VM.IFExistsLicencia(licenciaNueva);
                if (VM.Licencia.UidLicencia == Guid.Empty)
                {
                   
                    MessageBox.Show("¡Licencia no existente!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    VM.FindLicencia(licenciaNueva, 1);
                    if (VM.Licencia.UidLicencia == Guid.Empty)
                    {
                        
                        MessageBox.Show("¡Licencia ocupada!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    else
                    {
                        VM.ObtenerLicenciaLocal();
                        VM.HabilitarLicenciaAnteriorHost(VM.LicenciaLocal.UidLicencia);
                        VM.ActualizarLicenciaLocal(licenciaNueva);
                        VM.ObtenerLicenciaLocal();
                        Boolean CumpleConTodoRequisito = true;
                        VM.ObtenerDatosSesionHosting();

                        if (VM.Licencia.BooStatus == false)
                        {
                            CumpleConTodoRequisito = false;

                            MessageBox.Show("¡Licencia desactivada!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        if (VM.StatusSucursal.strStatus == "Inactivo")
                        {
                            CumpleConTodoRequisito = false;

                            MessageBox.Show("¡Sucursal desactivada!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }

                        if (CumpleConTodoRequisito == false)
                        {
                            //ActualizacionExitosaYNoFuncional();
                            //LbLicenciaResultado.Content = "Algunas de sus propiedades estan Desactivadas";
                        }
                        else
                        {
                            //ActualizacionExitosaYFuncional();
                            arranquesesionapp = true;
                            MessageBox.Show("¡Actualizacion de Licencia exitosa!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                    }
                    //}
                }
            }
            catch (Exception ex)
            {
                //ActualizacionNoExitosa();
                //LbLicenciaErrorDetalle.Content = ex.Message;
            }

            ComprovarValidacionLicencia();
            LVIMenu.Background = Brushes.Transparent;
            LVILicencia.Background = new SolidColorBrush(TemaDoradoEstandar);
        }
        private void BtnRevocarLicencia_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                VM.ObtenerSession();
                if (VM.Session.UidUsusario != Guid.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("¿Seguro de revocar Licencia?",
                "Confirmacion", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    VM.ObtenerLicenciaLocal();
                    VM.HabilitarLicenciaAnteriorHost(VM.LicenciaLocal.UidLicencia);
                    RevocarLicencia();
                    MessageBox.Show("¡Licencia revocada satisfactoriamente!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ComprovarValidacionLicencia();
                }
                else if (result == MessageBoxResult.No)
                {
                    MessageBox.Show("No le saques jajjajajajanefivnsvosdvpsv", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    // Cancel code here  
                }
                }
                else
                {
                    MessageBox.Show("Solo con sesion se puede revocar la licencia", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }

            }
            catch (Exception ex) {
                MessageBox.Show(" POR GEI: La licencia no se pudo revocar XDXDXDXDXDXDX", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            VM.LicenciaLocal = null;
        }
        public void RevocarLicencia() {
                VM.RevocarLicenciaLocal();
                ComprovarValidacionLicencia();
        }
        private void BtnAgregarLicencia_Click(object sender, RoutedEventArgs e)
        {
            HabilitarActualizacionLicencia();
        }

        #endregion Actualizar Licencia

        #region Turno
        void imprimirturno()
        {
            #region codigo original
            ////Ticket CorteCaja = new Ticket();
            //Ticket2 CorteCaja = new Ticket2();

            //CorteCaja.FontName = VariablesGlobal.FontName;
            //CorteCaja.FontSize = VariablesGlobal.FontSizeCierreTurno;

            //CorteCaja.AddHeaderLine("*****CIERRE DE TURNO*****");
            //CorteCaja.AddHeaderLine(" ");
            //CorteCaja.AddHeaderLine("REIMPRESO");
            //CorteCaja.AddHeaderLine(txbEmpresa.Text);
            //CorteCaja.AddHeaderLine(txbSucursal.Text);
            //CorteCaja.AddHeaderLine("         CAJA: " + txbCaja.Text);

            //CorteCaja.AddHeaderLine("        FOLIO: " + it.StrFolio);
            //CorteCaja.AddHeaderLine("     OPERADOR: " + it.StrUsuario);
            //CorteCaja.AddHeaderLine("   F/H INICIO: " + Convert.ToDateTime(it.DTFHoraInicio).ToString("dd/MM/yyyy HH:mm:ss"));
            //CorteCaja.AddHeaderLine("      F/H FIN: " + Convert.ToDateTime(it.DTFHoraCierre).ToString("dd/MM/yyyy HH:mm:ss"));
            //CorteCaja.AddHeaderLine(" ");
            //CorteCaja.AddHeaderLine("   BOLETOS EMITIDOS: " + item.IntBoletosEmitidosTC);
            //CorteCaja.AddHeaderLine("   BOLETOS COBRADOS: " + item.IntBoletosCobrados);

            //CorteCaja.Cabecera("CANT", "DESC", "       IMPORTE");
            //CorteCaja.AddItem(item.IntTotalBoletosCobrados.ToString(), "ROTACIÓN", "$" + item.DcmlimporteBoletosCobrados.ToString());
            //CorteCaja.AddItem(item.IntBoletosPerdidosCobrados.ToString(), "PERDIDO", "$" + item.DcmlimporteBoletosPerdidosCobrados.ToString());
            //CorteCaja.AddItem(item.IntTotalPensionadosCobrados.ToString(), "PENSIONADO", "$" + item.DcmlimportePensionadosCobrados.ToString());
            //CorteCaja.AddItem(item.IntBoletosToleranciaCobrados.ToString(), "TOLERANCIA", "$" + item.DcmlImporteBoletosToleranciaCobrados.ToString());
            //CorteCaja.AddItem("===================================", "", "");
            //CorteCaja.AddItem("", "IMPORTE", "$" + it.DcmlImporteTotalTurno.ToString());
            //CorteCaja.AddItem("", "DESCUENTOS", "$" + it.DcmDescuento.ToString());
            //CorteCaja.AddItem("", "TOTAL", "$" + it.DcmTotalConDescuento.ToString());

            //CorteCaja.PrintTicket(VariablesGlobal.ImpresoraSeleccionada);//Nombre de la impresora de tickets para imprimir

            #endregion codigo original

            double costototalfotos = 0;
            double totalfotos = 0;
            foreach (var fot in VM.FotosVendidas)
            {
                costototalfotos = costototalfotos + double.Parse(fot.StrCosto);
                totalfotos = totalfotos + double.Parse(fot.StrCantidad);
            }

            //Ticket CorteCaja = new Ticket();
            Ticket2 CorteCaja = new Ticket2();

            CorteCaja.FontName = VariablesGlobal.FontName;
            CorteCaja.FontSize = VariablesGlobal.FontSizeCierreTurno;

            CorteCaja.AddHeaderLine("**********CIERRE DE TURNO**********");
            CorteCaja.AddHeaderLine(" ");
            CorteCaja.AddHeaderLine(VM.Empresa.StrNombreComercial.ToUpper());
            CorteCaja.AddHeaderLine(VM.Sucursal.StrNombre.ToUpper());
            CorteCaja.AddHeaderLine(" ");
            // CorteCaja.AddHeaderLine("         CAJA: " + txbCaja.Text);

            //CorteCaja.AddHeaderLine("             FOLIO: " + VM.Turno.IntNoFolio);
            //CorteCaja.AddHeaderLine(" ENCARGADO: " + VM.Encargado.STRNOMBRE);
            //CorteCaja.AddHeaderLine("      F/H INICIO: " + VM.Turno.DtHrInicio);
            //CorteCaja.AddHeaderLine("           F/H FIN: " + VM.Turno.DtHrFin);
            //CorteCaja.AddHeaderLine(" ");
            //CorteCaja.AddHeaderLine("     FOTOS VENDIDAS: " + totalfotos.ToString());

            CorteCaja.AddHeaderLine("     FOLIO:" + VM.Turno.IntNoFolio);
            CorteCaja.AddHeaderLine(" ENCARGADO:" + VM.Encargado.STRNOMBRE);
            CorteCaja.AddHeaderLine("F/H INICIO:" + VM.Turno.DtHrInicio.ToString("dd/MM/yyyy").Replace(" ","")+" "+ VM.Turno.DtHrInicio.ToString("HH:mm:ss").Replace(" ", ""));
            CorteCaja.AddHeaderLine("   F/H FIN:" + VM.Turno.DtHrFin.ToString("dd/MM/yyyy").Replace(" ", "")+" "+ VM.Turno.DtHrFin.ToString("HH:mm:ss").Replace(" ", ""));
            CorteCaja.AddHeaderLine(" ");
            CorteCaja.AddHeaderLine("       FOTOS VENDIDAS: " + totalfotos.ToString());

            CorteCaja.AddHeaderLine(" ");
            // CorteCaja.AddHeaderLine("   COSTO TOTAL DEL DIA: "+ costototalfotos.ToString());

            CorteCaja.Cabecera("CANT", "FOTO", "IMPORTE");

            foreach (var fot in VM.FotosVendidas)
            {
                CorteCaja.AddItem("[" + fot.StrCantidad + "]", "" + fot.StrDescripcion, "$:" + fot.StrCosto);
            }
            //CorteCaja.AddItem(item.IntTotalBoletosCobrados.ToString(), "ROTACIÓN", "$" + item.DcmlimporteBoletosCobrados.ToString());
            //CorteCaja.AddItem(item.IntBoletosPerdidosCobrados.ToString(), "PERDIDO", "$" + item.DcmlimporteBoletosPerdidosCobrados.ToString());
            //CorteCaja.AddItem(item.IntTotalPensionadosCobrados.ToString(), "PENSIONADO", "$" + item.DcmlimportePensionadosCobrados.ToString());
            //CorteCaja.AddItem(item.IntBoletosToleranciaCobrados.ToString(), "TOLERANCIA", "$" + item.DcmlImporteBoletosToleranciaCobrados.ToString());
            CorteCaja.AddItem("===================================", "", "");
            // CorteCaja.AddItem("", "IMPORTE", "$" );
            // CorteCaja.AddItem("", "DESCUENTOS", "$" );

            CorteCaja.AddItem("", "TOTAL", "$" + costototalfotos.ToString());

            CorteCaja.PrintTicket("Microsoft Print to PDF");//Nombre de la impresora de tickets para imprimir

        }
        private void btnInicioTurno_Click(object sender, RoutedEventArgs e)
            {
                Login lo = new Login(VM);
                lo.Owner = this; AplicarEfecto(this, 5);
                lo.ShowDialog();
                lo.Owner = this; AplicarEfecto(this, 0);
                TurnoConf();
            }
            void TurnoConf()
            {
                if (CumpleConTodoRequisito == false)
                {
                    if (btnInicioTurno.Visibility != Visibility.Collapsed)
                    {
                        btnInicioTurno.Visibility = Visibility.Collapsed;
                    }
                    if (btnCierreTurno.Visibility != Visibility.Collapsed)
                    {
                        btnCierreTurno.Visibility = Visibility.Collapsed;
                    }
                    lbEncagado.Text = "No disponible";
                    lbTurno.Text = "Cerrada";
                    icoCheckstatusturno.Visibility = Visibility.Hidden;
                }
                else
                {
                    VM.ObtenerTurno();
                    if (VM.Turno.UidUsusario == Guid.Empty)
                    {
                        if (btnCierreTurno.Visibility != Visibility.Collapsed)
                        {
                            btnCierreTurno.Visibility = Visibility.Collapsed;
                        }
                        if (btnInicioTurno.Visibility == Visibility.Collapsed)
                        {
                            btnInicioTurno.Visibility = Visibility.Visible;
                        }
                        lbEncagado.Text = "No disponible";
                        lbTurno.Text = "Cerrada";
                        icoCheckstatusturno.Visibility = Visibility.Hidden;
                         lbHoraInicio.Text ="00:00:00";
                         lbFechaInicio.Text = "00/00/0000";
                         lbFolio.Text = "000000000000";
                         lbFotos.Text = "0";
                         lbVenta.Text = "0";
                }
                    else
                    {
                        if (VM.usuariosucursal(VM.Turno.UidUsusario, VM.Sucursal.UidSucursal) == false)
                        {
                            MessageBox.Show("ENCARGADO NO PERTENECE A ESTA SUCURSAL", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                           // VM.RevocarTurno();
                           // TurnoConf();
                        }
                        
                            if (VM.Encargado == null)
                            {
                                VM.ObteneEncargado(VM.Turno.UidUsusario);
                            }


                            if (btnCierreTurno.Visibility == Visibility.Collapsed)
                            {

                                //NameScope.GetNameScope(yourContainer).UnregisterName("name of your control");
                                //sPMenu.RegisterName("btnCierreSession", btncerrarsession);
                                //sPMenu.Children.Add(btncerrarsession);
                                btnCierreTurno.Visibility = Visibility.Visible;
                                //NameScope.GetNameScope(sPMenu).RegisterName("btnCierreSession", btncerrarsession);
                            }
                            if (btnInicioTurno.Visibility != Visibility.Collapsed)
                            {
                                btnInicioTurno.Visibility = Visibility.Collapsed;
                            }
                        
                            lbEncagado.Text = VM.Encargado.STRUSUARIO;
                            lbTurno.Text = "Abierto";
                            icoCheckstatusturno.Visibility = Visibility.Visible;
                            lbHoraInicio.Text = VM.Turno.DtHrInicio.ToString("T");
                            lbFechaInicio.Text = VM.Turno.DtFhInicio.ToString("d");
                            lbFolio.Text = VM.Turno.IntNoFolio.ToString("D12");
                            lbFotos.Text = VM.Turno.IntTFotos.ToString();
                            lbVenta.Text = VM.Turno.IntTCosto.ToString();
                            ActualizarVentasClientes();

                }
                     //VM.Turno = null;

                }
            }
            private void btnCierreTurno_Click(object sender, RoutedEventArgs e)
            {
            MessageBoxResult result = MessageBox.Show("¿Seguro de cerrar turno?",
             "Confirmacion", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                VM.ObtenerTurno();
                DateTime saveNow = DateTime.Now;
                DateTime myDt;
                myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
                VM.Turno.DtHrFin = DateTime.Parse( myDt.ToString("HH:mm:ss"));
                VM.Turno.DtFhInicio = DateTime.Parse(myDt.ToString("dd/MM/yyyy"));
                VM.RevocarTurno(VM.Turno.UidFolio,  myDt.ToString("HH:mm:ss"), myDt.ToString("dd/MM/yyyy"), VM.Turno.IntTFotos, VM.Turno.IntTCosto);
                VM.ReporteVentaFotos(VM.Turno.UidFolio);
                imprimirturno();
                VM.Turno = null;
                VM.Encargado = null;
                TurnoConf();
            }
            else if (result == MessageBoxResult.No)
            {

            }
            else
            {
                // Cancel code here  
            }
        }
        #endregion TUrno

        #region Session
            private void btnInicioSesion_Click(object sender, RoutedEventArgs e)
            {
                Autentificacion au = new Autentificacion(VM, true);
                au.Owner = this; AplicarEfecto(this, 5);
                au.ShowDialog();
                au.Owner = this; AplicarEfecto(this, 0);
                SessionConf();
            }
            void SessionConf()
            {
                if (CumpleConTodoRequisito == false)
                {
                    if (btnInicioSession.Visibility != Visibility.Collapsed)
                    {
                        btnInicioSession.Visibility = Visibility.Collapsed;
                    }
                    if (btnCierreSession.Visibility != Visibility.Collapsed)
                    {
                        btnCierreSession.Visibility = Visibility.Collapsed;
                    }
                    lbUsuario.Text = "No disponible";
                    lbNombreUsuario.Text = "No disponible";
                }
                else
                {
                    VM.ObtenerSession();
                    if (VM.Session.UidUsusario == Guid.Empty)
                    {
                        if (btnCierreSession.Visibility != Visibility.Collapsed)
                        {
                            btnCierreSession.Visibility = Visibility.Collapsed;
                        }
                        if (btnInicioSession.Visibility == Visibility.Collapsed)
                        {
                            btnInicioSession.Visibility = Visibility.Visible;
                        }
                        lbUsuario.Text = "No disponible";
                        lbNombreUsuario.Text = "No disponible";
                    }
                    else
                    {

                        if (VM.usuarioempresa(VM.Session.UidUsusario, VM.Empresa.UidEmpresa) == false)
                        {
                            MessageBox.Show("USUARIO NO PERTENECE A ESTA EMPRESA", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                            VM.RevocarSession();
                            SessionConf();
                        }
                        else
                        {
                            if (VM.Usuario == null)
                            {
                                VM.ObteneUsuario(VM.Session.UidUsusario);
                            }


                            if (btnCierreSession.Visibility == Visibility.Collapsed)
                            {

                                //NameScope.GetNameScope(yourContainer).UnregisterName("name of your control");
                                //sPMenu.RegisterName("btnCierreSession", btncerrarsession);
                                //sPMenu.Children.Add(btncerrarsession);
                                btnCierreSession.Visibility = Visibility.Visible;
                                //NameScope.GetNameScope(sPMenu).RegisterName("btnCierreSession", btncerrarsession);
                            }
                            if (btnInicioSession.Visibility != Visibility.Collapsed)
                            {
                                btnInicioSession.Visibility = Visibility.Collapsed;
                            }
                            lbUsuario.Text = VM.Usuario.STRUSUARIO;
                            lbNombreUsuario.Text = VM.Usuario.STRNOMBRE;
                        }

                    }
                }
              //VM.Session = null;
            }
            private void btnCierreSession_Click(object sender, RoutedEventArgs e) {
            MessageBoxResult result = MessageBox.Show("¿Seguro de cerrar sesion?",
              "Confirmacion", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                VM.RevocarSession();
                SessionConf();
            }
            else if (result == MessageBoxResult.No)
            {

            }
            else
            {
                // Cancel code here  
            }
        }



        #endregion Session


        #region codigo prototipo
        //void SaberIpServidor() {
        //    IPHostEntry host;

        //    host = Dns.GetHostEntry(Dns.GetHostName());
        //    foreach (IPAddress ip in host.AddressList)
        //    {
        //        if (ip.AddressFamily.ToString() == "InterNetwork")
        //        {
        //            localIP = ip.ToString();
        //        }
        //    }
        //    MessageBox.Show("Tú IP Local Es: " + localIP);
        //}

        //public void InicioConexionRedLocal()
        //{

        //   // Console.WriteLine("Servidor OK!");
        //    server = new TcpListener(ipendpoint);
        //    server.Start();

        //    while (true)
        //    {
        //        client = server.AcceptTcpClient();

        //        con = new Connection();
        //        con.stream = client.GetStream();
        //        con.streamr = new StreamReader(con.stream);
        //        con.streamw = new StreamWriter(con.stream);

        //        con.nick = con.streamr.ReadLine();

        //        list.Add(con);
        //        //Console.WriteLine(con.nick + " se a conectado.");
        //        lbFotos.Text = con.nick + " se a conectado.";


        //        Thread t = new Thread(Escuchar_conexion);

        //        t.Start();
        //    }


        //}

        //void Escuchar_conexion()
        //{
        //    Connection hcon = con;

        //    do
        //    {
        //        try
        //        {
        //            string tmp = hcon.streamr.ReadLine();
        //            //Console.WriteLine(hcon.nick + ": " + tmp);
        //            lbVenta.Text = hcon.nick + ": " + tmp;
        //            foreach (Connection c in list)
        //            {
        //                try
        //                {
        //                    c.streamw.WriteLine(hcon.nick + ": " + tmp);
        //                    c.streamw.Flush();
        //                }
        //                catch
        //                {
        //                }
        //            }
        //        }
        //        catch
        //        {
        //            list.Remove(hcon);
        //            //Console.WriteLine(con.nick + " se a desconectado.");
        //            lbFotos.Text = con.nick + " se a desconectado.";
        //            break;
        //        }
        //    } while (true);
        //}

        #endregion codigo prototipo

        private void StartServer()
        {
            try
            {
                SignalR = WebApp.Start(ServerURI);
            }
            catch (TargetInvocationException)
            {
                //WriteToConsole("A server is already running at " + ServerURI);
                //this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                MessageBox.Show("¡Error al conectar en la red, un servidor ya esta conectado!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }
            //MessageBox.Show("¡Servidor comenzo a conectarse!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //this.Dispatcher.Invoke(() => ButtonStop.IsEnabled = true);
            //WriteToConsole("Server started at " + ServerURI);
        }

        public void ActualizarVentasClientes() {
            try
            {
                VM.ActualizarVentaGeneral();
                lbFotos.Text = VM.Turno.IntTFotos.ToString();
                lbVenta.Text = VM.Turno.IntTCosto.ToString();
            }
            catch (Exception e) {
                MessageBox.Show("¡Error al actualizar las ventas!"+e, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

       
    }
    #region clases conexion red local
    
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCors(CorsOptions.AllowAll);
            app.MapSignalR();
        }
    }
    public class MyHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addMessage(name, message);
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).lbFotos.Text= message);
            
        }
        public void NuevaImpresionVenta() {
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).ActualizarVentasClientes());
           // Clients.All.addMessage(name, "Se confirmo actualizacion de venta");
        }
        public override Task OnConnected()
        {
            ////Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            //Application.Current.Dispatcher.Invoke(() =>
            //    ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client connected: " + Context.ConnectionId));

            return base.OnConnected();
        }
        public override Task OnDisconnected()
        {
            ////Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            //Application.Current.Dispatcher.Invoke(() =>
            //    ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));
           
            return base.OnDisconnected();
        }

    }
    #endregion  clases conexion red local
}
