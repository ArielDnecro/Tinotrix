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
using System.ComponentModel;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Propiedades
        string path;
        int IntClientesConectados;
        public int _IntClientesConectados
        {
            get { return IntClientesConectados; }
            set { IntClientesConectados = value; }
        }
        private Thread threadServicio = null;
        CargandoAplicacion LoadApp;
        BackgroundWorker bg;
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
        MyHub hub;
        public MyHub Hub
        {
            get { return hub; }
            set { hub = value; }
        }
        #endregion propiedades

        #region Constructor
        public MainWindow()
        {
            bg = new BackgroundWorker();
            bg.DoWork += new DoWorkEventHandler(bg_DoWork);
            bg.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bg_RunWorkerCompleted);
            Ca();
            AplicarCultura();
            RedireccionarBasico();
            ConstructorPrincipal();
        }
        void ConstructorPrincipal()
        {
            InitializeComponent();
            //en esta primera seccion verifico que tengo internet
            System.Net.WebRequest Peticion = default(System.Net.WebRequest);
            var Respuesta = default(System.Net.WebResponse);
            try
            {

                Peticion = System.Net.WebRequest.Create("http://tinotrix.gearhostpreview.com/vista/Login.aspx");
                Respuesta = Peticion.GetResponse();
                //this.Close();
            }
            catch (FileNotFoundException r)
            {
                AppSinConexionInternet();
            }
            catch (DirectoryNotFoundException r)
            {
                AppSinConexionInternet();
            }
            catch (IOException r)
            {
                AppSinConexionInternet();
            }
            catch (Exception r)
            {
                AppSinConexionInternet();
            }
            //en esta seccion Inicializo mi ventana y la construyo con base a los datos que tenga en el host
            try
            {
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

                VariablesGlobal.FontName = "arial";
                VariablesGlobal.FontSize = 12;
                VariablesGlobal.FontSizeItemIndividual = 9;
                VariablesGlobal.FontSizePosicionLlave = 12F;
                VariablesGlobal.FontSizeCierreTurno = 9;
                VariablesGlobal.WidthImagen = 270;
                VariablesGlobal.HeightImagen = 50;
                VariablesGlobal.CentrarGoParkiX = "          ";

                ObtenerDirectorioRaiz();
                LimpiarApp();
                //SaberIpServidor();
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
        void ObtenerDirectorioRaiz()
        {
            path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
            if (!Directory.Exists(path + "\\Imagenes\\usuario\\"))
            {
                path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                // MessageBox.Show("sTRING PATH CHANGE");
            }

        }
        void LimpiarApp()
        {
            //var extensions = new List<string> { ".txt", ".xml" };
            //string[] files = Directory.GetFiles(sDir, "*.*", SearchOption.AllDirectories)
            //                    .Where(f => extensions.IndexOf(Path.GetExtension(f)) >= 0).ToArray();
            String Directorio = path + "\\Imagenes\\usuario\\";

            //string[] filePathsWebCam = Directory.GetFiles(Directorio, "*WebCamUsuario_*.png", SearchOption.AllDirectories);
            //foreach (string archivoWebCam in filePathsWebCam)
            //{
            //    File.Delete(archivoWebCam);
            //}
            //string[] filePathsOriginales = Directory.GetFiles(Directorio, "*FotoOriginalUsuario_*.*", SearchOption.AllDirectories);
            //foreach (string archivoOriginal in filePathsOriginales)
            //{
            //    File.Delete(archivoOriginal);
            //}
            //string[] filePathsFinales = Directory.GetFiles(Directorio, "*FotoFinalUsuario_*.*", SearchOption.AllDirectories);
            //foreach (string archivoFinal in filePathsFinales)
            //{
            //    File.Delete(archivoFinal);
            //}

            string[] filePathsFinales = Directory.GetFiles(Directorio, "*DescargaUsuario_*.*", SearchOption.AllDirectories);
            foreach (string archivoFinal in filePathsFinales)
            {
                File.Delete(archivoFinal);
            }
            //MessageBox.Show("DELETE ALL TEMP USER IMAGES");
        }
        #endregion Constructor

        #region Ventana Inicion
        void bg_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //LoadApp.Hide();
            LoadApp.Close();

            //this.Visibility = Visibility.Visible;
        }
        void bg_DoWork(object sender, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(20000);
            //this.Hide();
        }
        void Ca()
        {
            //CargandoAplicacion Load = new CargandoAplicacion();
            //Load.Show();
            //System.Windows.Threading.Dispatcher.Run();
            LoadApp = new CargandoAplicacion();
            LoadApp.Show();
            bg.RunWorkerAsync();
        }
        #endregion Ventana inicio

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
        void AppSinConexionInternet()
        {
            //AppTemaSinConexion();
            SinConexionInternet AppSinInternet = new SinConexionInternet();
            LoadApp.Close();
            AppSinInternet.ShowDialog();
            ConstructorPrincipal();
        }
        //void AppTemaSinConexion()
        //{
        //    GridMain.Visibility = Visibility.Hidden;
        //    //BtnOpenMenu.Background = Brushes.White;
        //    //IcoOpenMenu.Foreground = new SolidColorBrush(TemaAzulEstandar);
        //    BtnMenuFotografiasCliente.IsEnabled = false;
        //    BtnMenuHome.IsEnabled = false;
        //    BtnMenuLicencia.IsEnabled = false;
        //    // GBestado.Visibility = Visibility.Hidden;
        //    BtnMenuImpresoras.IsEnabled = false;
        //    BtnMenuConexionServidor.IsEnabled = false;
        //    LVIMenu.Background = Brushes.Transparent;
        //    LVILicencia.Background = Brushes.Transparent;
        //    LVICliente.Background = Brushes.Transparent;
        //    LVIConfiguracion.Background = Brushes.Transparent;
        //    btnInicioSession.Visibility = Visibility.Collapsed;
        //    btnCierreSession.Visibility = Visibility.Collapsed;
        //}
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //InicioConexionRedLocal();
            //this.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(delegate { InicioConexionRedLocal();  }));
            //Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate { InicioConexionRedLocal(); }));
            //Servidor_Chat chat = new Servidor_Chat();
            LoadApp.Close();
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
            popmenuicon.Foreground = new SolidColorBrush(TemaAzulEstandar);
            LVIMenu.IsEnabled = false;
            LVIMenu.Background = Brushes.Transparent;
            LVICliente.IsEnabled = false;
            LVICliente.Background = Brushes.Transparent;
            LVIConfiguracion.IsEnabled = false;
            LVIConfiguracion.Background = Brushes.Transparent;
            //Licencia no tiene desabilitacion porque es la unica opcion que hay para habilitar este producto
            LVILicencia.Background = Brushes.Transparent;
        }
        void TemaAppHabilitado()
        {
            GridMain.Visibility = Visibility.Visible;
            sPbtnsMenuPrincipal.Visibility = Visibility.Visible;
            GridBarraEstado.Visibility = Visibility.Visible;

            LVIMenu.IsEnabled = true;
            LVIMenu.Background = new SolidColorBrush(TemaDoradoEstandar); //esta parte se movera debido a que es llamado por varias funciones
            LVICliente.IsEnabled = true;
            LVIConfiguracion.IsEnabled = true;
            LVILicencia.IsEnabled = true;
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
            popmenuicon.Foreground = new SolidColorBrush(TemaAzulEstandar);

            
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
            popmenuicon.Foreground = Brushes.White;
        }
        private void btnCerrarApp_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.RevocarSession();
                SessionConf();
            }
            catch (FileNotFoundException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (DirectoryNotFoundException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (IOException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);

            }
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
            popmenuicon.Foreground = Brushes.White;
            //frame.NavigationService.Navigate(new PageLicencia());
            blicencia.Visibility = Visibility.Visible;
            bframe.Visibility = Visibility.Hidden;
            cerrarmenu();
            
        }
        private void BtnMenuHome_Click(object sender, RoutedEventArgs e)
        {
            MenuHome();
            cerrarmenu();
        }
        public void MenuHome() {
            LVIMenu.Background = new SolidColorBrush(TemaDoradoEstandar);
            LVILicencia.Background = Brushes.Transparent;
            LVICliente.Background = Brushes.Transparent;
            LVIConfiguracion.Background = Brushes.Transparent;
            frame.NavigationService.Navigate(new PagePrincipal());
            CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            popmenuicon.Foreground = Brushes.White;

            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
        }
        private void BtnMenuFotografiasCliente_Click(object sender, RoutedEventArgs e)
        {
           
            if (this.hub!=null && this.IntClientesConectados>=1 ) {
                try
                {
                    LVIMenu.Background = Brushes.Transparent;
                    LVILicencia.Background = Brushes.Transparent;
                    LVICliente.Background = new SolidColorBrush(TemaDoradoEstandar);
                    LVIConfiguracion.Background = Brushes.Transparent;
                    // frame.NavigationService.Navigate(new PagePrincipal());
                    CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
                    popmenuicon.Foreground = Brushes.White;

                    blicencia.Visibility = Visibility.Hidden;
                    bframe.Visibility = Visibility.Visible;

                    // frame.NavigationService.Navigate(new PageFotosCliente(VM));

                    PageFotosCliente pagecliente = new PageFotosCliente(VM);
                    //pagecliente.Hub = this.hub;
                    pagecliente.MainServer = this;
                    frame.NavigationService.Navigate(pagecliente);

                    cerrarmenu();
                }
                catch (Exception d) {
                    MessageBox.Show(d.Message, "Aviso de error TINOTRIX");
                }
            }
            else
            {
                MessageBox.Show("No hay una ninguna maquina a quien enviarle 1 foto: \n" , "Aviso de conexion TINOTRIX");
            }
        }
        private void BtnMenuImpresoras_Click(object sender, RoutedEventArgs e)
        {
            VM.ObtenerSession();
            if (VM.Session.UidUsusario == Guid.Empty)
            {
                MessageBox.Show("¡Inicia sesion primero!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                LVIMenu.Background = Brushes.Transparent;
                LVILicencia.Background = Brushes.Transparent;
                LVICliente.Background = Brushes.Transparent;
                LVIConfiguracion.Background = new SolidColorBrush(TemaDoradoEstandar);
                // frame.NavigationService.Navigate(new PagePrincipal());
                CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
                popmenuicon.Foreground = Brushes.White;

                blicencia.Visibility = Visibility.Hidden;
                bframe.Visibility = Visibility.Visible;

                PageImpresoras pageconfimpre = new PageImpresoras();
                pageconfimpre.ParentWindow = this;
                frame.NavigationService.Navigate(pageconfimpre);
                cerrarmenu();
            }
        }
        private void BtnMenuConexionServidor_Click(object sender, RoutedEventArgs e)
        {
            VM.ObtenerSession();
            if (VM.Session.UidUsusario == Guid.Empty)
            {
                MessageBox.Show("¡Inicia sesion primero!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            else
            {
                LVIMenu.Background = Brushes.Transparent;
                LVILicencia.Background = Brushes.Transparent;
                LVICliente.Background = Brushes.Transparent;
                LVIConfiguracion.Background = new SolidColorBrush(TemaDoradoEstandar);
                // frame.NavigationService.Navigate(new PagePrincipal());
                CMenu.Background = new SolidColorBrush(TemaAzulEstandar);
                popmenuicon.Foreground = Brushes.White;

                blicencia.Visibility = Visibility.Hidden;
                bframe.Visibility = Visibility.Visible;

                PageConfConexionServicio pagconser = new PageConfConexionServicio(VM);
                pagconser.ParentWindow = this;
                frame.NavigationService.Navigate(pagconser);
                cerrarmenu();
            }
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
            try
            {
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
                            else
                            {
                                if (VM.StatusSucursal.strStatus == "Inactivo")
                                {
                                    CumpleConTodoRequisito = false;
                                    //LbMarcoSubProSucursal.Background = Brushes.Red;
                                    LbLicenciaStatusSucursal.Foreground = Brushes.Red;

                                }
                                else
                                {
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
                                if (VM.SignalR == null)//|| VM.Connection.State != ConnectionState.Disconnected
                                {

                                    //VM.SignalR.Dispose();
                                    // Task.Run(() => ComienzoServicio());
                                    threadServicio = new Thread(new ThreadStart(ComienzoServicio));
                                    threadServicio.Start();
                                }
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
            }
            catch (Exception e)
            {
                MessageBox.Show("¡Error conexion y actualizacion de licencia! U.U: \n"+e, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                //Application.Current.Shutdown();
            }
            return CumpleConTodoRequisito;
        }
        public void ComprovarActulizacionLicencia() {
          
            try
            {
                Guid licenciaNueva = new Guid(txtLicenciaCodigo.Text);

              
                VM.IFExistsLicencia(licenciaNueva);
                if (VM.Licencia.UidLicencia == Guid.Empty)
                {
                   
                    MessageBox.Show("¡Licencia no existente! U.U", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    VM.FindLicencia(licenciaNueva, 1);
                    if (VM.Licencia.UidLicencia == Guid.Empty)
                    {
                        
                        MessageBox.Show("¡Licencia ocupada! U.U", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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

                            MessageBox.Show("¡Licencia desactivada! U.U", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        }
                        if (VM.StatusSucursal.strStatus == "Inactivo")
                        {
                            CumpleConTodoRequisito = false;

                            MessageBox.Show("¡Sucursal desactivada! U.U", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                            MessageBox.Show("¡Actualizacion de Licencia exitosa! \n \n 7W7", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                //VM.ObtenerSession();
                //if (VM.Session.UidUsusario != Guid.Empty)
                //{
                    MessageBoxResult result = MessageBox.Show("¿Seguro de revocar Licencia? 7x7 ",
                "Confirmacion", MessageBoxButton.YesNoCancel);
                if (result == MessageBoxResult.Yes)
                {
                    VM.ObtenerLicenciaLocal();
                    VM.HabilitarLicenciaAnteriorHost(VM.LicenciaLocal.UidLicencia);
                    RevocarLicencia();
                    MessageBox.Show("¡Licencia revocada satisfactoriamente! 7o7", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    ComprovarValidacionLicencia();
                }
                else if (result == MessageBoxResult.No)
                {
                    MessageBox.Show("No le saques jajjajajajanefivnsvosdvpsv 7u7", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    // Cancel code here  
                }
                //}
                //else
                //{
                //    MessageBox.Show("Solo con sesion se puede revocar la licencia", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                //}

            }
            catch (Exception ex) {
                MessageBox.Show(" POR GEI: La licencia no se pudo revocar XDXDXDXDXDXDX  7u7", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            VM.ObtenerImpresora();
            if (!string.IsNullOrEmpty(VM.StrDesImpresora))
            {
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

                CorteCaja.AddHeaderLine("****CIERRE DE TURNO TINOTRIX****");
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
                CorteCaja.AddHeaderLine("F/H INICIO:" + VM.Turno.DtHrInicio.ToString("dd/MM/yyyy").Replace(" ", "") + " " + VM.Turno.DtHrInicio.ToString("HH:mm:ss").Replace(" ", ""));
                CorteCaja.AddHeaderLine("   F/H FIN:" + VM.Turno.DtHrFin.ToString("dd/MM/yyyy").Replace(" ", "") + " " + VM.Turno.DtHrFin.ToString("HH:mm:ss").Replace(" ", ""));
                CorteCaja.AddHeaderLine(" ");
                //CorteCaja.AddHeaderLine(" ");
                // CorteCaja.AddHeaderLine("       FOTOS VENDIDAS: " + totalfotos.ToString());

                // CorteCaja.AddHeaderLine(" ");
                // CorteCaja.AddHeaderLine("   COSTO TOTAL DEL DIA: "+ costototalfotos.ToString());

                //CorteCaja.Cabecera("CANT", "FOTO", "IMPORTE");
                //CorteCaja.Cabecera("DESCRÌPCION", "CANT" + "  IMPORTE", "");
                if (VM.FotosVendidas.Count == 0)
                {
                    //CorteCaja.AddItem("-----------", "        0"+ "      $" + "0", "");
                }
                else
                {
                    CorteCaja.Cabecera("DESCRIPCION", "CANT" + "  IMPORTE", "");
                    foreach (var fot in VM.FotosVendidas)
                    {
                        CorteCaja.AddItem("[" + fot.StrDescripcion + "]", "        " + fot.StrCantidad+ "      $" + fot.StrCosto, "");
                    }
                    //CorteCaja.AddItem("Infantil", "        " + "12", "$" + "103");
                }
                //CorteCaja.AddItem(item.IntTotalBoletosCobrados.ToString(), "ROTACIÓN", "$" + item.DcmlimporteBoletosCobrados.ToString());
                //CorteCaja.AddItem(item.IntBoletosPerdidosCobrados.ToString(), "PERDIDO", "$" + item.DcmlimporteBoletosPerdidosCobrados.ToString());
                //CorteCaja.AddItem(item.IntTotalPensionadosCobrados.ToString(), "PENSIONADO", "$" + item.DcmlimportePensionadosCobrados.ToString());
                //CorteCaja.AddItem(item.IntBoletosToleranciaCobrados.ToString(), "TOLERANCIA", "$" + item.DcmlImporteBoletosToleranciaCobrados.ToString());
                CorteCaja.AddItem("===================================", "", "");
                // CorteCaja.AddItem("", "IMPORTE", "$" );
                // CorteCaja.AddItem("", "DESCUENTOS", "$" );
                if (VM.FotosVendidas.Count == 0)
                {
                    CorteCaja.AddItem("TOTAL", "        " +  "      $" + costototalfotos.ToString(), "");

                }
                else
                {
                    CorteCaja.AddItem("TOTAL", "        " + totalfotos.ToString() + "      $" + costototalfotos.ToString(), "");

                }
                CorteCaja.AddItem("===================================", "", "");
                //CorteCaja.AddItem();
                // CorteCaja.PrintTicket("Microsoft Print to PDF");//Nombre de la impresora de tickets para imprimir
                CorteCaja.PrintTicket(VM.StrDesImpresora);
                //CorteCaja.PrintTicket("EPSON UB-U03II");
            }
            else {
                MessageBox.Show("¡No hay impresora disponible o seleccionada! \n configure 'impresoras'" , "Configuracion Tinotrix");
            }
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
                            MessageBox.Show("Encargado no pertece a esta sucuersal", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            MessageBoxResult result = MessageBox.Show("¿Seguro de cerrar turno? 7x7",
             "Confirmacion", MessageBoxButton.YesNoCancel);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    VM.ObtenerTurno();
                    DateTime saveNow = DateTime.Now;
                    DateTime myDt;
                    myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
                    VM.Turno.DtHrFin = DateTime.Parse(myDt.ToString("HH:mm:ss"));
                    VM.Turno.DtFhInicio = DateTime.Parse(myDt.ToString("dd/MM/yyyy"));
                    bool TurnoCerrado = new bool();
                    TurnoCerrado= VM.RevocarTurno(VM.Turno.UidFolio, myDt.ToString("HH:mm:ss"), myDt.ToString("dd/MM/yyyy"), VM.Turno.IntTFotos, VM.Turno.IntTCosto);
                    if (TurnoCerrado == true) {
                        VM.ReporteVentaFotos(VM.Turno.UidFolio);
                        imprimirturno();
                    }
                    else
                    {
                        MessageBox.Show("¡Ya estaba cerrado el turno! \n", "ERROR Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    }
                    
                    VM.Turno = null;
                    VM.Encargado = null;
                    TurnoConf();

                   
                }
                catch (Exception y) {
                    VM.Turno = null;
                    VM.Encargado = null;
                    TurnoConf();
                    MessageBox.Show("¡Error, no se pudo ejecutar el siguiente proceso! \n"+ y, "ERROR Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
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
                            MessageBox.Show("Usuario no pertenece a esta empresa 7-7", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
            MessageBoxResult result = MessageBox.Show("¿Seguro de cerrar sesion? 7o7",
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

        #region Servicio
        public void ActualizarVentasClientes() {
            try
            {
                if (VM.Turno.UidFolio == Guid.Empty || VM.Turno.UidFolio== null  )
                {
                    MessageBox.Show("¡No haz iniciado Turno! \n " , "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else {
                    VM.ActualizarVentaGeneral();
                    lbFotos.Text = VM.Turno.IntTFotos.ToString();
                    lbVenta.Text = VM.Turno.IntTCosto.ToString();
                }
            }
            catch (Exception e) {
                MessageBox.Show("¡Error al actualizar las ventas! \n  " + e, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
        private void ComienzoServicio() {
            try
            {
                bool EstadoServicio = VM.StartServer();
                if (EstadoServicio == false)
                {
                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        LbEstadoServicioLocal.Text = "Desactivado";
                    }));
                }
                else
                {

                    this.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        LbEstadoServicioLocal.Text = "Activado";
                    }));
                }
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    LbNombreIPServidor.Text = VM.localIP;
                    LbPuertoServidor.Text = VM.PuertoConexion;
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
        private void BtnActualizarVentas_Click(object sender, RoutedEventArgs e)
        {
            ActualizarVentasClientes();
        }
        #endregion servicio

    }
    #region clases conexion red local
    class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            try
            {
               // GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
                app.UseCors(CorsOptions.AllowAll);
                app.MapSignalR();
               
            }
            catch (Exception e) { }
        }
       
        
    }
    public interface IUserIdProvider
    {
        string GetUserId(IRequest request);

    }
    public class MyHub : Hub
    {
        public readonly static ConnectionMapping<string> _connections =
           new ConnectionMapping<string>();
        
        public void Send(string name, string message)
        {
            // Clients.All.addMessage(message);
            //Clients.User(name).send("Mensaje existoso");

            //Application.Current.Dispatcher.Invoke(() =>
            //    ((MainWindow)Application.Current.MainWindow).lbFotos.Text= message);
            //Clients.Client(name).send(name, message);
            //Clients.User(name).SendAsync(message);
            //Clients.Client
            foreach (var connectionId in _connections.GetConnections(name))
            {
                Clients.Client(connectionId).SendAsync(message);
            }
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
            //MessageBox.Show("Un nuevo dispositivo se conecto: " + Context.User.ToString()
            //, "Aviso de conexion TINOTRIX"));


            // string name = Context.User.Identity.Name;
            string name = Context.Headers.Get("username");
            _connections.Add(name, Context.ConnectionId);
            // Clients.User(name).send("");
            Application.Current.Dispatcher.Invoke(() =>
               MessageBox.Show("Un nuevo dispositivo se conecto: \n" + name, "Aviso de conexion TINOTRIX"));

            Application.Current.Dispatcher.Invoke(() =>
               ((MainWindow)Application.Current.MainWindow)._IntClientesConectados = ((MainWindow)Application.Current.MainWindow)._IntClientesConectados +1);

            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).Hub = this);
            return base.OnConnected();
        }
        public void NuevaPCConectada(string name)
        {
            Application.Current.Dispatcher.Invoke(() =>
                MessageBox.Show("Un nuevo dispositivo se conecto: \n" + name, "Aviso de conexion TINOTRIX"));

        }
        public override Task OnDisconnected(bool stopCalled)
        {
            ////Use Application.Current.Dispatcher to access UI thread from outside the MainWindow class
            //Application.Current.Dispatcher.Invoke(() =>
            //    ((MainWindow)Application.Current.MainWindow).WriteToConsole("Client disconnected: " + Context.ConnectionId));
            //  MessageBox.Show("Se guardo la venta pero no se notifico al servidor", "Error de impresion");
            string name = Context.Headers.Get("username");

            _connections.Remove(name, Context.ConnectionId);
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).Hub = this);

            Application.Current.Dispatcher.Invoke(() =>
            ((MainWindow)Application.Current.MainWindow)._IntClientesConectados = ((MainWindow)Application.Current.MainWindow)._IntClientesConectados - 1);

            return base.OnDisconnected(stopCalled);
            //return base.OnDisconnected();
        }
        public override Task OnReconnected()
        {
            string name = Context.Headers.Get("username");

            if (!_connections.GetConnections(name).Contains(Context.ConnectionId))
            {
                _connections.Add(name, Context.ConnectionId);
            }
            Application.Current.Dispatcher.Invoke(() =>
                ((MainWindow)Application.Current.MainWindow).Hub = this);
            return base.OnReconnected();
        }
    }

    public class ConnectionMapping<T>
    {
        private readonly Dictionary<T, HashSet<string>> _connections =
            new Dictionary<T, HashSet<string>>();

        public int Count
        {
            get
            {
                return _connections.Count;
            }
        }
        public void Add(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    connections = new HashSet<string>();
                    _connections.Add(key, connections);
                }

                lock (connections)
                {
                    connections.Add(connectionId);
                }
            }
        }
        public IEnumerable<string> GetConnections(T key)
        {
            HashSet<string> connections;
            if (_connections.TryGetValue(key, out connections))
            {
                return connections;
            }

            return Enumerable.Empty<string>();
        }
        public void Remove(T key, string connectionId)
        {
            lock (_connections)
            {
                HashSet<string> connections;
                if (!_connections.TryGetValue(key, out connections))
                {
                    return;
                }

                lock (connections)
                {
                    connections.Remove(connectionId);

                    if (connections.Count == 0)
                    {
                        _connections.Remove(key);
                    }
                }
            }
        }
    }
    #endregion  clases conexion red local
}
