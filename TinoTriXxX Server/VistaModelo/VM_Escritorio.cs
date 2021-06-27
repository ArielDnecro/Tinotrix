using CodorniX.ConexionDB;
using CodorniX.Modelo;
using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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
        private SucursalLicencia.Repository LicenciaRepository = new SucursalLicencia.Repository();
        private Empresa.Repository EmpresaRepository = new Empresa.Repository();
        private LicenciaLocal.Repository LicenciaLocalRepository = new LicenciaLocal.Repository();
        private EmpresaLocal.Repository EmpresaLocalRepository = new EmpresaLocal.Repository();
        private Session.Repository SessionLocalRepository = new Session.Repository();
        private Usuario.Repository UsuarioRepositiry = new Usuario.Repository();
        private Turno.Repository TurnoRepository = new Turno.Repository();
        DBLogin login = new DBLogin();
        private Impresora.Repository ImpresoraRepository = new Impresora.Repository();
        private _Foto.Repository FotoRepository = new _Foto.Repository();
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

        #region Empresa
        private EmpresaLocal _EmpresaLocal;
        public EmpresaLocal EmpresaLocal
        {
            get { return EmpresaLocal; }
            set { _EmpresaLocal = value; }
        }

        #endregion Empresa

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
        private Usuario _Encargado;
        public Usuario Encargado
        {
            get { return _Encargado; }
            set { _Encargado = value; }
        }

        private Turno _Turno;
        public Turno Turno
        {
            get { return _Turno; }
            set { _Turno = value; }
        }
        #endregion Usuario

        #region Impresora
        private string _StrDescImpresora;
        public string StrDesImpresora
        {
            get { return _StrDescImpresora; }
            set { _StrDescImpresora = value; }
        }
        #endregion Impresora

        #region foto
        private List<_Foto> _FotosVendidas;
        public List<_Foto> FotosVendidas
        {
            get { return _FotosVendidas; }
            set { _FotosVendidas = value; }
        }
        #endregion foto

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

        #region red
        public String UserName { get; set; }
       // public string ServerURI = "";
        // http://localhost:8000/signalr
        public string IpServidor = "";
        public string PuertoConexion = "0";
        //public IHubProxy HubProxy { get; set; }
        //public HubConnection Connection { get; set; }
        public IDisposable SignalR { get; set; }
        public string ServerURI = "http://127.0.0.1:8000";
        public string localIP = "";
        #endregion red

        #endregion Propiedades
        #region funciones

        #region Licencia
        public void ObtenerLicenciaLocal()
        {
            _LicenciaLocal = LicenciaLocalRepository.Find();
        }
        public void IFExistsLicencia(Guid UIDLicencia )
        {
                _Licencia = LicenciaRepository.IfExistsServer(UIDLicencia);
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
                 if (SucursalDirecciones.Count>=1) { _SucursalDireccion = SucursalDirecciones[0]; }
                else { _SucursalDireccion = new SucursalDireccion(); }
                _Empresa = EmpresaRepository.Find(Sucursal.UidEmpresa);

            //21-8-19
            Turno turnito = new Turno();
            turnito = TurnoRepository.Findhost(Sucursal.UidSucursal);
            if (turnito != null || turnito != new Turno())
            {
                TurnoRepository.IniciarTurnoLocal(turnito.UidFolio, turnito.IntNoFolio, turnito.DtHrInicio.ToString("HH:mm:ss"), turnito.DtFhInicio.ToString("dd/MM/yyyy"));
                ActualizarTurno(turnito.UidUsusario);
            }
            else
            {
                TurnoRepository.RevocarEncargado();
                TurnoRepository.Revocar();
            }
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
        public void ObtenerEmpresaLocal()
        {
            _EmpresaLocal = EmpresaLocalRepository.Find();
        }
        public void ActualizarEmpresaLocal(Guid UIDEmpresa)
        {
            try
            {
                EmpresaLocalRepository.ActualizarEmpresa(UIDEmpresa);
            }
            catch (Exception e)
            {
                throw new VM_EscritorioException("(ConvertirCriterioAEmpresa)" + e.Message);
            }
        }
        public void RevocarEmpresaLocal()
        {
            LicenciaLocalRepository.Revocar();
        }
        #endregion Licencia
     
        #region Turno
        public Guid ExisteEncargadoHost(string usuario)
        {
            Guid id = new Guid();
            foreach (DataRow item in login.obtenerUsuarioLogin(usuario).Rows)
            {
                id = new Guid(item["UidUsuario"].ToString());
            }
            return id;
        }
        public void ObteneEncargado(Guid idusuario)
        {
            _Encargado = UsuarioRepositiry.Find(idusuario);
        }
        public bool usuariosucursal(Guid user, Guid sucursal)
        {
            return UsuarioRepositiry.wpffindsucursal(user, sucursal);
        }
        public void ActualizarTurno(Guid UIDEncargado)//local
        {
            try
            {
                TurnoRepository.ActualizarUsuario(UIDEncargado);
            }
            catch (Exception e)
            {
                throw new VM_EscritorioException("(wpf vm encargado local )" + e.Message);
            }
        }
        public void ObtenerTurno()//Local
        {
            //_Turno = 
            _Turno = TurnoRepository.Find();
            //if (Turno.UidUsusario == Guid.Empty)
            //{

            //}
      }
        public bool RevocarTurno( Guid UidTurno, String HrSalida, String FhSalida, int TFotos, int TCosto)// local y host
        {
            TurnoRepository.RevocarEncargado();
            TurnoRepository.Revocar();
            return  TurnoRepository.RevocarHost(UidTurno, HrSalida,  FhSalida,  TFotos,  TCosto);//
            //Turno = null;
            //Encargado = null;
        }
        public void IniciarTurno(Guid uidsucursal, Guid uidfolio, Guid uidusuario, String hrentrada, String fhentrada)//host y local
        {
           _Turno.IntNoFolio = TurnoRepository.IniciarTurnoHost( uidsucursal, uidfolio, uidusuario, hrentrada, fhentrada);
            TurnoRepository.IniciarTurnoLocal(uidfolio,Turno.IntNoFolio, hrentrada, fhentrada);
        }
        public void ActualizarVentaGeneral()
        {
            Turno TurnitoX3 = new Turno();
            TurnitoX3= TurnoRepository.ActualizarVentaGeneral(Turno.UidFolio);
            _Turno.IntTCosto = TurnitoX3.IntTCosto;
            _Turno.IntTFotos = TurnitoX3.IntTFotos;
        }
        #endregion turno

        #region Session
        //La session es el usuario que esta validado, guardado y cargada en la aplicacion local
        public void ObtenerSession()
        {
            _Session = SessionLocalRepository.Find();
        }
        public void ActualizarSession(Guid UIDSession)
        {
            try
            {
                SessionLocalRepository.ActualizarUsuario(UIDSession);
            }
            catch (Exception e)
            {
                throw new VM_EscritorioException("(ConvertirCriterioAEmpresa)" + e.Message);
            }
        }
        public void RevocarSession()
        {
            SessionLocalRepository.Revocar();
            Session = null;
            Usuario = null;
        }
        #endregion SESSION

        #region usuario 
        //El usuario es el que se esta validando en el hosting para luego comprovar si es guardado en el app local o no
        //recordemos que el usuario y session tiene variables diferentes, session solo guarda el id del usuario.
        public Guid IniciarSesion(string usuario)
        {
            Guid id = new Guid();
            foreach (DataRow item in login.obtenerUsuarioLogin(usuario).Rows)
            {
                id = new Guid(item["UidUsuario"].ToString());
            }
            return id;
        }
        public void asignarIDUsuario(Guid Id) {
            _Usuario.UIDUSUARIO = Id;
        }
        public bool usuarioempresa(Guid user,Guid empresa) {
          
                return UsuarioRepositiry.wpffindempresa(user, empresa);
        }
        public void ObteneUsuario(Guid idusuario) {
           _Usuario = UsuarioRepositiry.Find(idusuario);
        }
        #endregion usuario

        #region Impresora
        public void ObtenerImpresora()
        {
            _StrDescImpresora= ImpresoraRepository.Find().StrDescripcion;
        }

        public void ActualizarImpresora(string StrDescripcion)
        {
            try
            {
                ImpresoraRepository.ActualizarImpresora(StrDescripcion);
            }
            catch (Exception e)
            {
                throw new VM_EscritorioException("(ConvertirCriterioAImpresora)" + e.Message);
            }
        }
        #endregion Impresora

        #region Foto
        public void ReporteVentaFotos(Guid UIDTURNO) {
            _FotosVendidas = FotoRepository.CargarFotos(UIDTURNO);
        }
        #endregion Foto

        #region Servicios
        public string ObtenerPuerto()
        {
            //string StrPuerto= "";
            if (ServicioRepository.VerificarExistenciaPuerto() == true)
            {
                PuertoConexion = ServicioRepository.FindPuerto();
            }
            else
            {
            }
            return PuertoConexion;
        }
        //public string ObtenerIPServidor()
        //{
        //    // string StrIP = "";
        //    if (ServicioRepository.VerificarExistenciaIPServidor() == true)
        //    {
        //        IpServidor = ServicioRepository.FindIPServidor();
        //    }
        //    else
        //    {

        //    }
        //    return IpServidor;
        //}
        //public void ActualizarIpServidor(string StrIpServer)
        //{
        //    ServicioRepository.ActualizarIPServidor(StrIpServer);
        //}
        public void ActualizarPuerto(string StrPuerto)
        {
            ServicioRepository.ActualizarPuerto(StrPuerto);
        }
        void SaberIpServidor()
        {
            IPHostEntry host;

            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily.ToString() == "InterNetwork")
                {
                    localIP = ip.ToString();
                }
            }
            // MessageBox.Show("Tú IP Local Es: " + localIP);
            ServerURI = "http://" + localIP + ":" + PuertoConexion;
        }
        public bool StartServer()
        {
            bool value = false;
            try
            {
                if (ObtenerPuerto() != "0" )
                {
                    SaberIpServidor();
                    try
                    {
                        SignalR = WebApp.Start(ServerURI);
                        
                    }
                    catch (TargetInvocationException)
                    {

                        //WriteToConsole("A server is already running at " + ServerURI);
                        //this.Dispatcher.Invoke(() => ButtonStart.IsEnabled = true);
                        MessageBox.Show("¡Error al conectar en la red, un servidor ya esta conectado!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                        value= false;
                    }
                    // MessageBox.Show("¡Servidor comenzo a conectarse! \n " + ServerURI, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    value = true;
                }
                else
                {
                    MessageBox.Show("¡No tiene un puerto definido \n servicio no inicializado !", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    value = false;
                }
            }
            catch (FileNotFoundException e)
            {
                value = false;
                MessageBox.Show(e.Message);
                //Application.Current.Shutdown();

            }
            catch (DirectoryNotFoundException e)
            {
                value = false;
                MessageBox.Show(e.Message);
                // Application.Current.Shutdown();
            }
            catch (IOException e)
            {
                value = false;
                MessageBox.Show(e.Message);
                // Application.Current.Shutdown();
            }
            catch (Exception e)
            {
                value = false;
                MessageBox.Show(e.Message);
                //Application.Current.Shutdown();
            }
            return value;
        }
        #endregion Servicios
        #endregion funciones 
        #region Excepciones
        public class VM_EscritorioException : Exception
        {
            public VM_EscritorioException(string mensaje) : base("(VM_EscritorioException):  " + mensaje) { }
        }
        #endregion Excepciones
    }
}
