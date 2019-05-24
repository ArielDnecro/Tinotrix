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

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       

        VM_Escritorio VM = new VM_Escritorio();

        //var converter = new System.Windows.Media.BrushConverter();
        //var brush = (Brush)converter.ConvertFromString("#FFFFFF90");
        Color TemaAzulEstandar = (Color)ColorConverter.ConvertFromString("#FF3580BF");
        Boolean CumpleConTodoRequisito;
        public MainWindow()
        {
          
            InitializeComponent();
            ComprovarValidacionLicencia();
            //DispatcherTimer time = new DispatcherTimer();  
            //time.Interval = TimeSpan.FromSeconds(150);
            //time.Tick += Time_Tick;
            //time.Start();
            frame.NavigationService.Navigate(new PagePrincipal());
            bframe.Visibility = Visibility.Visible;
            blicencia.Visibility = Visibility.Hidden;



        }

      
        private void Time_Tick(object sender, EventArgs e)
        {
            //comprovar Licencia en el hodting
            ComprovarValidacionLicencia();


        }
        #region Eventos de la vista

        void TemaAppDesabilitado()
        {
            GridMain.Visibility = Visibility.Hidden;
            BtnOpenMenu.Background = Brushes.White;
            IcoOpenMenu.Foreground = new SolidColorBrush(TemaAzulEstandar);
            BtnMenuFotos.IsEnabled = false;
            BtnMenuHome.IsEnabled = false;
            GBestado.Visibility = Visibility.Hidden;
            BtnMenuConfiguracion.IsEnabled = false;
        }
        void TemaAppHabilitado()
        {
            GridMain.Visibility = Visibility.Visible;
            BtnMenuFotos.IsEnabled = true;
            BtnMenuHome.IsEnabled = true;
            BtnMenuConfiguracion.IsEnabled = true;
            GBestado.Visibility = Visibility.Visible;
        }
        private void BtnSesion_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void BtnOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            OpenMenuclick();
        }

        private void BtnCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            CloseMenuClick();
        }

        private void BtnMenuLicencia_Click(object sender, RoutedEventArgs e)
        {
            GridMain.Visibility = Visibility.Visible;
            blicencia.Visibility = Visibility.Visible;
            bframe.Visibility = Visibility.Hidden;
            cerrarmenu();
        }

        private void BtnMenuHome_Click(object sender, RoutedEventArgs e)
        {
            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            frame.NavigationService.Navigate(new PagePrincipal());
            cerrarmenu();
        }

        private void BtnMenuFotos_Click(object sender, RoutedEventArgs e)
        {
            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            frame.NavigationService.Navigate(new PageFotos(VM));
            cerrarmenu();
        }

        private void BtnMenuConfiguracion_Click(object sender, RoutedEventArgs e)
        {
            blicencia.Visibility = Visibility.Hidden;
            bframe.Visibility = Visibility.Visible;
            frame.NavigationService.Navigate(new PageConfiguracion());
            cerrarmenu();
        }
        void cerrarmenu()
        {
            Storyboard closemenu = this.Resources["CloseMenu"] as Storyboard;
            closemenu.Begin();
            CloseMenuClick();
        }
        void CloseMenuClick()
        {
            BtnOpenMenu.Visibility = Visibility.Visible;
            BtnCloseMenu.Visibility = Visibility.Collapsed;

            BtnOpenMenu.Background = new SolidColorBrush(TemaAzulEstandar);
            IcoOpenMenu.Foreground = Brushes.White;
        }
        void OpenMenuclick()
        {
            BtnOpenMenu.Visibility = Visibility.Collapsed;
            BtnCloseMenu.Visibility = Visibility.Visible;

            BtnCloseMenu.Background = Brushes.White;
            IcoCloseMenu.Foreground = new SolidColorBrush(TemaAzulEstandar);

        }
        
        private void AplicarEfecto(Window win, int NivelDegradado)
        {
            var objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = NivelDegradado;
            win.Effect = objBlur;
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
            ComprovarValidacionLicencia();
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
            CumpleConTodoRequisito = true;
            Brush azul = new SolidColorBrush(TemaAzulEstandar);
            VM.ObtenerLicenciaLocal();
            if (VM.LicenciaLocal.UidLicencia == Guid.Empty)
            {

                LbLicenciaCodigo.Text = "No disponible";
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
                BtnActualizarLicencia.IsEnabled = false;
                TemaAppDesabilitado();
                CumpleConTodoRequisito = false;
            }
            else
            {
                VM.IFExistsLicencia(VM.LicenciaLocal.UidLicencia);
                if (VM.Licencia.UidLicencia == Guid.Empty)
                {
                    RevocarLicencia();
                    ComprovarValidacionLicencia();
                    CumpleConTodoRequisito = false;
                }
                else
                {
                    VM.FindLicencia(VM.LicenciaLocal.UidLicencia, 0);
                    if (VM.Licencia.UidLicencia == Guid.Empty)
                    {
                        LbLicenciaCodigo.Text = VM.Licencia.UidLicencia.ToString();
                        LbLicenciaNoMaquinas.Text = "No disponible";
                        LbLicenciaStatus.Text = "No disponible";
                        LbLicenciaNoPc.Text = "No disponible";
                        //LbPcEncabezado.Content = "No disponible";

                        //LbSucursalEncabezado.Content = "No disponible";
                        LbLicenciaNombreSucursal.Text = "No disponible";
                        LbLicenciaStatusSucursal.Text = "No disponible";
                        LbLicenciaDireccionSucursal.Text = "No disponible";
                        LbLicenciaNombreEmpresa.Text = "No disponible";
                        CumpleConTodoRequisito = false;
                    }
                    else
                    {
                        VM.ObtenerDatosSesionHosting();
                        VM.ActualizarEmpresaLocal(VM.Sucursal.UidEmpresa);
                        //Seccion de llenado de labels subProducto
                        //licencia
                        LbLicenciaCodigo.Text = VM.Licencia.UidLicencia.ToString();
                        LbLicenciaNoMaquinas.Text = VM.Licencia.IntNoTotal.ToString();

                        if (VM.Licencia.BooStatus== false)
                        {
                            // LbLicenciaStatus.Text = VM.Licencia.BooStatus.ToString();
                            CumpleConTodoRequisito =false;
                            LbLicenciaStatus.Text = "Inactivo";
                        }
                        else {
                            LbLicenciaStatus.Text = "Activo";
                            
                        }
                        
                        LbLicenciaNoPc.Text = VM.Licencia.IntNo.ToString();
                        BtnActualizarLicencia.IsEnabled = true;

                        //Sucursal
                        LbLicenciaNombreSucursal.Text = VM.Sucursal.StrNombre.ToString();
                        if (VM.StatusSucursal.strStatus.ToString().Trim() == "false")
                        {
                            CumpleConTodoRequisito = false;
                            LbLicenciaStatusSucursal.Text = "Inactivo";
                        }
                        else
                        {
                            LbLicenciaStatusSucursal.Text = "Activo";
                        }

                        LbLicenciaDireccionSucursal.Text = "Calle " + VM.SucursalDireccion.StrCalle + " entre calle "
                        + VM.SucursalDireccion.StrConCalle + " y calle " + VM.SucursalDireccion.StrYCalle + " colonia "
                        + VM.SucursalDireccion.StrColonia + ", " + VM.SucursalDireccion.StrCiudad
                        //+ ", " + VM.SucursalDireccion.e
                        ;

                        //Empresa
                        LbLicenciaNombreEmpresa.Text = VM.Empresa.StrNombreComercial;

                        if (VM.Licencia.BooStatus == false)
                        {
                            CumpleConTodoRequisito = false;
                            //LbMarcoSubProLicencia.Background = Brushes.Red;
                            LbLicenciaStatus.Foreground = Brushes.Red;
                        }
                        if (VM.StatusSucursal.strStatus == "Inactivo")
                        {
                            CumpleConTodoRequisito = false;
                            //LbMarcoSubProSucursal.Background = Brushes.Red;
                            LbLicenciaStatusSucursal.Foreground = Brushes.Red;
                        }

                        if (CumpleConTodoRequisito == false)
                        {
                            //TemaProductoDesactivado();
                            TemaAppDesabilitado();
                        }
                        else
                        {
                            //TemaProductoElejido();
                            CumpleConTodoRequisito = true;
                            LbLicenciaStatusSucursal.Foreground = azul;
                            LbLicenciaStatus.Foreground = azul;
                            TemaAppHabilitado();
                        }
                    }
                }
            }
            SessionConf();
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
        }
        private void BtnRevocarLicencia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ObtenerSession();
                if (VM.Session.UidUsusario != Guid.Empty)
                {
                    MessageBoxResult result = MessageBox.Show("¿Seguro de revocar Licencia?","Confirmacion", MessageBoxButton.YesNoCancel);
                    if (result == MessageBoxResult.Yes)
                    {
                        VM.ObtenerLicenciaLocal();
                        VM.HabilitarLicenciaAnteriorHost(VM.LicenciaLocal.UidLicencia);
                        RevocarLicencia();
                        MessageBox.Show("¡Licencia revocda satisfactoriamente!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
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
                MessageBox.Show("La licencia no se pudo revocar ", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
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

        #region Sesion
        private void btnInicioSession_Click(object sender, RoutedEventArgs e)
        {
            Autentificacion au = new Autentificacion(VM);
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
                //lbUsuario.Text = "No disponible";
                //lbNombreUsuario.Text = "No disponible";
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
                    //lbNombreUsuario.Text = "No disponible";
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
                        //lbNombreUsuario.Text = VM.Usuario.STRNOMBRE;
                    }

                }
            }
            //VM.Session = null;
        }


        #endregion Sesion

        private void btnCierreSession_Click(object sender, RoutedEventArgs e)
        {
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

        
    }
}
