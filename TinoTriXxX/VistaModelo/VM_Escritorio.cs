using CodorniX.ConexionDB;
using CodorniX.Modelo;
using MaterialDesignThemes.Wpf;
using Microsoft.AspNet.SignalR.Client;
using Microsoft.AspNet.SignalR.Client.Http;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;
using TinoTriXxX.Modelo;

namespace TinoTriXxX.VistaModelo
{
    public class VM_Escritorio
    {
        #region Propiedades
        private Sucursal.Repository Sucursalrepository = new Sucursal.Repository();
        //private SucursalTelefono.Repository telefonoRepository = new SucursalTelefono.Repository();
        private SucursalDireccion.Repository SucursaldireccionRepository = new SucursalDireccion.Repository();

        //private SucursalImpresora.Repository impresoraRepository = new SucursalImpresora.Repository();
        private Status.Repository statusRepository = new Status.Repository();
        //private SucursalFoto.Repository fotoRepository = new SucursalFoto.Repository();
        //private SucursalImpresoraTipo.Repository tipoimpresoraRepository = new SucursalImpresoraTipo.Repository();
        private SucursalLicencia.Repository LicenciaRepository = new SucursalLicencia.Repository();
        private Empresa.Repository EmpresaRepository = new Empresa.Repository();
        private LicenciaLocal.Repository LicenciaLocalRepository = new LicenciaLocal.Repository();
        private Usuario.Repository UsuarioRepositiry = new Usuario.Repository();
        private Session.Repository SessionLocalRepository = new Session.Repository();
        private EmpresaLocal.Repository EmpresaLocalRepository = new EmpresaLocal.Repository();
        private Foto.Repository FotoRepository = new Foto.Repository();
        private Papel.Repository PapelRepository = new Papel.Repository();
        private Turno.Repository TurnoRepository = new Turno.Repository();
        DBLogin login = new DBLogin();
        private Servicio.Repository ServicioRepository = new Servicio.Repository();
        #region empresa
        private Empresa _Empresa;
        public Empresa Empresa
        {
            get { return _Empresa; }
            set { _Empresa = value; }
        }
        #endregion empresa

        #region sucursales
        private List<Sucursal> _Sucursales;

        public List<Sucursal> Sucursales
        {
            get { return _Sucursales; }
            set { _Sucursales = value; }
        }

        private Sucursal _Sucursal;
        public Sucursal Sucursal
        {
            get { return _Sucursal; }
            set { _Sucursal = value; }
        }

        private List<TipoSucursal> _TipoSucursales;
        public List<TipoSucursal> TipoSucursales
        {
            get { return _TipoSucursales; }
            set { _TipoSucursales = value; }
        }
        #endregion sucursales

        #region direcciones
        private List<EmpresaDireccion> _EmpresaDirecciones;
        public List<EmpresaDireccion> EmpresaDirecciones
        {
            get { return _EmpresaDirecciones; }
            set { _EmpresaDirecciones = value; }
        }

        private List<SucursalDireccion> _SucursalDirecciones;
        public List<SucursalDireccion> SucursalDirecciones
        {
            get { return _SucursalDirecciones; }
            set { _SucursalDirecciones = value; }
        }

        private SucursalDireccion _SucursalDireccion;
        public SucursalDireccion SucursalDireccion
        {
            get { return _SucursalDireccion; }
            set { _SucursalDireccion = value; }
        }

        //private List<Pais> _paises;
        //public List<Pais> Paises
        //{
        //    get { return _paises; }
        //    set { _paises = value; }
        //}

        //private List<Estado> _estados;
        //public List<Estado> Estados
        //{
        //    get { return _estados; }
        //    set { _estados = value; }
        //}
        #endregion direcciones

        #region Usuario
        private Usuario _Usuario;
        public Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }
        private Session _Session;
        public Session Session
        {
            get { return _Session; }
            set { _Session = value; }
        }
        
        #endregion Usuario
     

        #region Licencias
        private SucursalLicencia _Licencia;
        public SucursalLicencia Licencia
        {
            get { return _Licencia; }
            set { _Licencia = value; }
        }

        private LicenciaLocal _LicenciaLocal;
        public LicenciaLocal LicenciaLocal
        {
            get { return _LicenciaLocal; }
            set { _LicenciaLocal = value; }
        }

        #endregion Licencias

        #region varios
        private IList<Status> _ListaStatus;
        public IList<Status> ListaStatus
        {
            get { return _ListaStatus; }
            set { _ListaStatus = value; }
        }


        private Status _StatusSucursal;
        public Status StatusSucursal
        {
            get { return _StatusSucursal; }
            set { _StatusSucursal = value; }
        }
        #endregion varios

        #region foto
        public List<Foto> ListaFotos = new List<Foto>();
        #endregion foto

        #region Papel
        private Papel _Papel;
        public Papel Papel
        {
            get { return _Papel; }
            set { _Papel = value; }
        }
        #endregion Papel
<<<<<<< HEAD
=======

        #region red
        public String UserName { get; set; }
        public string ServerURI = "";
        // http://localhost:8000/signalr
        public string IpServidor = "";
        public string PuertoConexion = "";
        public IHubProxy HubProxy { get; set; }
        public HubConnection Connection { get; set; }

        private SucursalServidor _Servidor;
        public SucursalServidor Servidor
        {
            get { return _Servidor; }
            set { _Servidor = value; }
        }

        private Badged _FotosDescargadas;
        public Badged FotosDescargadas
        {
            get { return _FotosDescargadas; }
            set { _FotosDescargadas = value; }
        }
        
        #endregion red

>>>>>>> dbac16a27acfe069ffa037d12af6bbd626ce7b37
        #endregion Propiedades
        #region funciones
        #region Conexion fotos area local
        public async void ConnectAsync()
        {
            if (Connection == null || Connection.State == Microsoft.AspNet.SignalR.Client.ConnectionState.Disconnected)
            {
                try
                {
                    //GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => new MyIdProvider());
                    Connection = new HubConnection(ServerURI);
                
                    Connection.Headers.Add("username", UserName);
                    //Connection.TraceLevel = TraceLevels.All;
                    //Connection.TraceWriter = Console.Out;
                    //VM.Connection.Closed += Connection_Closed;//FUNCION ORIGINAL
                    HubProxy = Connection.CreateHubProxy("MyHub");

                    //Connection.ConnectionToken= { "username" : UserName };

                    //Handle incoming event from server: use Invoke to write to console from SignalR's thread
                    //HubProxy.On<string, string>("AddMessage", (name, message) =>
                    //    this.Dispatcher.Invoke(() =>
                    //        RichTextBoxConsole.AppendText(String.Format("{0}: {1}\r", name, message))
                    //    )
                    //);
                    //HubProxy.On<string, System.Windows.Controls.Image>("AddMessage", (name, message) =>
                    //    Application.Current.Dispatcher.Invoke(() =>
                    //        GuardarImagenDescarga(message)
                    //    )
                    //);
                    HubProxy.On< string>("SendAsync", (message) =>
                        Application.Current.Dispatcher.Invoke(() =>
                            GuardarImagenDescarga(message)
                        )
                    );
                    //HubProxy.On<string>("AddMessage", (message) =>
                    //   Application.Current.Dispatcher.Invoke(() =>
                    //       GuardarImagenDescarga(message)
                    //   )
                    //);
                    //HubProxy.On<string, string>("AddMessage", (name, message) =>
                    //    Application.Current.Dispatcher.Invoke(() =>
                    //        prueba(message)
                    //    )
                    //);
                    await Connection.Start();
           

                        //Show chat UI; hide login UI
                        //SignInPanel.Visibility = Visibility.Collapsed;
                        //ChatPanel.Visibility = Visibility.Visible;
                        //ButtonSend.IsEnabled = true;
                        //TextBoxMessage.Focus();
                        //RichTextBoxConsole.AppendText("Connected to server at " + ServerURI + "\r");
                //HubProxy.Invoke("NuevaPCConectada", UserName);
                }
                catch (HttpRequestException e)
                {
                    //StatusText.Content = "Unable to connect to server: Start server before connecting clients.";
                    //No connection: Don't enable Send button or show chat UI

                
                    //MessageBox.Show("HttpRequestException: " + e.Message, "Tinotrix: Error conexion asincrona", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Connection.Stop();
                    Connection.Dispose();

                }
                catch (FileNotFoundException e)
                {
                    //MessageBox.Show("FileNotFoundException: " + e.Message, "Tinotrix: Error conexion asincrona", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Connection.Stop();
                    Connection.Dispose();

                }
                catch (DirectoryNotFoundException e)
                {
                    //MessageBox.Show("DirectoryNotFoundException: " + e.Message, "Tinotrix: Error conexion asincrona", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Connection.Stop();
                    Connection.Dispose();

                }
                catch (IOException e)
                {
                    //MessageBox.Show("IOException: " + e.Message, "Tinotrix: Error conexion asincrona", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Connection.Stop();
                    Connection.Dispose();

                }
                catch (Exception e)
                {
                    //MessageBox.Show("Exception: " + e.Message, "Tinotrix: Error conexion asincrona", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    Connection.Stop();
                    Connection.Dispose();

                }
                
            }
        }
        void GuardarImagenDescarga(string strimagen)
        {
            try
            {
                if (!string.IsNullOrEmpty(strimagen))
                {
                    //la imagen en string se convierte en imagen drawing
                    System.Drawing.Image ImguserDrawing = StringToImageDrawing(strimagen);
                    //se crea una nueva imagen control
                    System.Windows.Controls.Image Imuser = new System.Windows.Controls.Image();
                    //se convierte imagen drawing en source control imagen y se le asigna a control imagen
                    Imuser.Source = ConvertImageControl(ImguserDrawing);
                    ///PROCESO DE GUARDADO FISICO A LA IMAGEN DESCARGADA
                    string path;

                    path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
                    if (!Directory.Exists(path + "\\Imagenes\\usuario\\"))
                    {
                        path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                        // MessageBox.Show("sTRING PATH CHANGE");
                    }
                    String Directorio = path + "\\Imagenes\\usuario\\"; //"C:\\Users\\Iudex\\Documents\\TinoTrix\\Clone\\Tinotrix\\TinoTriXxX"
                                                                        // C: \Users\Iudex\Documents\TinoTrix\Clone\Tinotrix\TinoTriXxX\Imagenes\usuario
                                                                        //String Extencion = System.IO.Path.GetExtension(sourceFileOriginal);
                    String Archivo = "FotoFinalDescarga_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ".png";
                    string filePathElegidaImprimir = Directorio + Archivo;
                    System.Windows.Controls.Image UserImage = Imuser;
                    var encoder = new PngBitmapEncoder();
                    //"JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    //MessageBox.Show("encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));

                    // MessageBox.Show(" using (FileStream stream = new FileStream(filePathElegidaImprimir, FileMode.Create)) encoder.Save(stream);", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                    using (FileStream stream = new FileStream(filePathElegidaImprimir, FileMode.Create)) encoder.Save(stream);
                    MessageBox.Show("¡Llego una nueva fotografia!");

                   string pageactual=  Application.Current.Dispatcher.Invoke(() =>
                             ((MainWindow)Application.Current.MainWindow).frame.Content.ToString());
                   string s2 = "PageFotos";
                   bool b = pageactual.Contains(s2);
                    if (b)
                    {
                        string[] filePathsdescarga = Directory.GetFiles(Directorio, "*FotoFinalDescarga_*.*", SearchOption.AllDirectories);
                        //var mw = Application.Current.Windows.Cast<Window>().FirstOrDefault(window => window is MainWindow) as MainWindow;
                        _FotosDescargadas.Badge = filePathsdescarga.Count();
                    }
                    else
                    {

                    }
                }
            }
            catch (HttpRequestException e)
            {
                MessageBox.Show("Llego una imagen del servidor pero hubo un problema: \n" + e.Message);
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("Llego una imagen del servidor pero hubo un problema: \n" + e.Message);

            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("Llego una imagen del servidor pero hubo un problema: \n" + e.Message);

            }
            catch (IOException e)
            {
                MessageBox.Show("Llego una imagen del servidor pero hubo un problema: \n" + e.Message);

            }
            catch (Exception e)
            {
                MessageBox.Show("Llego una imagen del servidor pero hubo un problema: \n" + e.Message);

            }
        }
        public static Image StringToImageDrawing( string base64String)
        {
            if (String.IsNullOrWhiteSpace(base64String))
                return null;

            var bytes = Convert.FromBase64String(base64String);
            var stream = new MemoryStream(bytes);
            return Image.FromStream(stream);
        }
        public static System.Windows.Media.ImageSource ConvertImageControl(System.Drawing.Image image)
        {
            try
            {
                if (image != null)
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch { }
            return null;
        }
        #endregion Conexion fotos area local
        #region Empresa
        public void ActualizarEmpresaLocal(Guid UIDEmpresa)
        {
            try
            {
                EmpresaLocalRepository.ActualizarEmpresa(UIDEmpresa);
            }
            catch (Exception e)
            {
                throw new VM_EscritorioException("(actualizar empresa local)" + e.Message);
            }
        }
        #endregion empresa
        #region Licencia
        public void ObtenerLicenciaLocal()
            {
                _LicenciaLocal = LicenciaLocalRepository.Find();
            }
            public void IFExistsLicencia(Guid UIDLicencia )
            {
                    _Licencia = LicenciaRepository.IfExists(UIDLicencia);
            }
            public void FindLicencia(Guid UIDLicencia, int IntPrimeraVez)
            {
                   _Licencia = LicenciaRepository.Find(UIDLicencia, IntPrimeraVez);
            }
            public void ObtenerDatosSesionHosting()
            {
            
                    _Sucursal = Sucursalrepository.Find(Licencia.UidSucursal);
                    _StatusSucursal = statusRepository.Find(Sucursal.UidStatus);
                    _SucursalDirecciones = SucursaldireccionRepository.FindAll(Licencia.UidSucursal);
                   // _SucursalDireccion = SucursalDirecciones[0];
                    if (SucursalDirecciones.Count >= 1) { _SucursalDireccion = SucursalDirecciones[0]; }
                    else { _SucursalDireccion = new SucursalDireccion(); }
                    _Empresa = EmpresaRepository.Find(Sucursal.UidEmpresa);
            }
            public void HabilitarLicenciaAnteriorHost(Guid UIDLicenciaAnterior)
            {
                try
                {
                    LicenciaRepository.HabilitarLicenciaAnterior(UIDLicenciaAnterior);
                }
                catch (Exception e)
                {
                    throw new VM_EscritorioException("(Habilitacion de la Licencia anterior en el Host)" + e.Message);
                }
            }
            public void ActualizarLicenciaLocal(Guid UIDLicencia)
            {
                try
                {
                    LicenciaLocalRepository.ActualizarLicencia(UIDLicencia);
                }
                catch (Exception e) {
                    throw new VM_EscritorioException("(ConvertirCriterioAEmpresa)" + e.Message);
                }
            }
            public void RevocarLicenciaLocal()
            {
                LicenciaLocalRepository.Revocar();
            }
            #endregion Licencia
        #region Session
                public Guid IniciarSesion(string usuario)
                {
                    Guid id = new Guid();
                    foreach (DataRow item in login.obtenerUsuarioLogin(usuario).Rows)
                    {
                        id = new Guid(item["UidUsuario"].ToString());
                    }
                    return id;
                }
                public bool usuarioempresa(Guid user, Guid empresa)
                {
                    return UsuarioRepositiry.wpffindempresa(user, empresa);
                }
                public void ObteneUsuario(Guid idusuario)
                {
                    _Usuario = UsuarioRepositiry.Find(idusuario);
                }
                public void ActualizarSession(Guid UIDSession)
                {
                    try
                    {
                        SessionLocalRepository.ActualizarUsuario(UIDSession);
                    }
                    catch (Exception e)
                    {
                        throw new VM_EscritorioException("(Actualizar sesion local)" + e.Message);
                    }
                }
                public void ObtenerSession()
                {
                    _Session = SessionLocalRepository.Find();
                }
                public void RevocarSession()
                {
                    SessionLocalRepository.Revocar();
                    Session = null;
                    Usuario = null;
                }
        #endregion Session
        #region FOTO
        public void CargarlistaFotos(Guid UIDSucursal) {
            ListaFotos = FotoRepository.CargarFotos(UIDSucursal);
        }
        public double MedidafotoConversor(string StrMedida) {
          return FotoRepository.CargarMedidaLocal(StrMedida);
        }
        public void NuevaImpresion(Guid UidSucursal, Guid UidFoto, string DtTmVenta, int IntFotos, int IntCosto) {
            FotoRepository.NuevaImpresion( UidSucursal,  UidFoto,  DtTmVenta,  IntFotos, IntCosto);
        }
        #endregion FOTO
        #region Papel
        public void CargarPapel(Guid UIDSucursal) {
            _Papel = PapelRepository.Find(UIDSucursal);
        }
        #endregion Papel
        #region turno
        public bool TurnoServidorAbierto(Guid sucursal)
        {
            return TurnoRepository.TurnoServidorAbierto(sucursal);
        }
<<<<<<< HEAD
        #endregion turno
=======


        #endregion turno
        #region Servicios
        public string ObtenerPuerto()
        {
            //string StrPuerto= "";
            if (ServicioRepository.VerificarExistenciaPuerto()==true)
            {
                PuertoConexion = ServicioRepository.FindPuerto();
            }
            else
            {
            }
            return PuertoConexion;
        }
        public string ObtenerIPServidor()
        {
           // string StrIP = "";
            if (ServicioRepository.VerificarExistenciaIPServidor()==true)
            {
                IpServidor = ServicioRepository.FindIPServidor();
            }
            else
            {

            }
            return IpServidor;
        }
        public void ActualizarIpServidor(string StrIpServer) {
            ServicioRepository.ActualizarIPServidor(StrIpServer);
        }
        public void ActualizarPuerto(string StrPuerto)
        {
            ServicioRepository.ActualizarPuerto(StrPuerto);
        }
       
        public bool DescargarConfiguracion()
        {
            bool existe = false;
            if (_Sucursal.UidSucursal != Guid.Empty)
            {
                _Servidor = ServicioRepository.ObtenerConfServerHost(_Sucursal.UidSucursal);
                if (!String.IsNullOrEmpty(_Servidor.StrNombreIP))
                {
                    if (_Servidor.UidSucursal != Guid.Empty)
                    {
                        existe = true;
                    }
                }
            }
            return existe;
        }
        #endregion Servicios
>>>>>>> dbac16a27acfe069ffa037d12af6bbd626ce7b37
        #endregion funciones 
        #region Excepciones
        public class VM_EscritorioException : Exception
        {
            public VM_EscritorioException(string mensaje) : base("(VM_EscritorioException):  " + mensaje) { }
        }
        #endregion Excepciones
    }
}
