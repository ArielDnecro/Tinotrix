using CodorniX.ConexionDB;
using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        #endregion Propiedades


        #region funciones
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
                _SucursalDireccion = SucursalDirecciones[0];
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
            _Turno = TurnoRepository.Find();
        }
        public void RevocarTurno( Guid UidTurno, String HrSalida, String FhSalida, int TFotos, int TCosto)// local y host
        {
            TurnoRepository.RevocarEncargado();
            TurnoRepository.Revocar();
            TurnoRepository.RevocarHost(UidTurno, HrSalida,  FhSalida,  TFotos,  TCosto);
            Turno = null;
            Encargado = null;
        }
        public void IniciarTurno(Guid uidsucursal, Guid uidfolio, Guid uidusuario, String hrentrada, String fhentrada)//host y local
        {
           _Turno.IntNoFolio = TurnoRepository.IniciarTurnoHost( uidsucursal, uidfolio, uidusuario, hrentrada, fhentrada);
            TurnoRepository.IniciarTurnoLocal(uidfolio,Turno.IntNoFolio, hrentrada, fhentrada);
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

        #endregion funciones 
        #region Excepciones
        public class VM_EscritorioException : Exception
        {
            public VM_EscritorioException(string mensaje) : base("(VM_EscritorioException):  " + mensaje) { }
        }
        #endregion Excepciones
    }
}
