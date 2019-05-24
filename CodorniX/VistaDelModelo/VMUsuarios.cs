using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using CodorniX.Modelo;
using CodorniX.ConexionDB;
using CodorniX.Util;

namespace CodorniX.VistaDelModelo
{
    public class VMUsuarios
    {
        #region Propiedades
        DBUsuarios Conexion = new DBUsuarios();
        UsuarioTelefono usuariotelefono = new UsuarioTelefono();
        UsuarioDireccion.Repository direccionRepository = new UsuarioDireccion.Repository();
        TipoTelefono.Repository tipoTelefonoRepository = new TipoTelefono.Repository();
        private Pais.Repository paisRepository = new Pais.Repository();
        private Estado.Repository estadoRepository = new Estado.Repository();
        private UsuarioTelefono.Repository telefonoRepository = new UsuarioTelefono.Repository();
        private Empresa.Repository empresaRepository = new Empresa.Repository();
        private Perfil.Repositorio repositorioPerfiles = new Perfil.Repositorio();
        private NivelAcceso.Repositorio repositorioNiveles = new NivelAcceso.Repositorio();
        private Usuario.Repository repositoriousuario = new Usuario.Repository();

        private List<TipoTelefono> _tipoTelefonos;

        public List<TipoTelefono> TipoTelefonos
        {
            get { return _tipoTelefonos; }
            set { _tipoTelefonos = value; }
        }

        private IList<Perfil> _ObjPerfil;

        public IList<Perfil> ObjPerfil
        {
            get { return _ObjPerfil; }
            set { _ObjPerfil = value; }
        }

        private Usuario _Usuario;

        public Usuario CLASSUSUARIO
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        private Perfil _Perfil;

        public Perfil CLASSPERFIL
        {
            get { return _Perfil; }
            set { _Perfil = value; }
        }

        private Status _Status;

        public Status CLASSSTATUS
        {
            get { return _Status; }
            set { _Status = value; }
        }

        private Telefono _ObjTelefono;

        public Telefono ObjTelefono
        {
            get { return _ObjTelefono; }
            set { _ObjTelefono = value; }
        }

        private UsuarioTelefono _ObjUsuarioTelefono;

        public UsuarioTelefono ObjUsuarioTelefono
        {
            get { return _ObjUsuarioTelefono; }
            set { _ObjUsuarioTelefono = value; }
        }
        private List<UsuarioTelefono> _telefonos;

        public List<UsuarioTelefono> Telefonos
        {
            get { return _telefonos; }
            set { _telefonos = value; }
        }

        private Direccion _Direccion;

        public Direccion Direccion
        {
            get { return _Direccion; }
            set { _Direccion = value; }
        }

        private List<Pais> _paises;

        public List<Pais> Paises
        {
            get { return _paises; }
            set { _paises = value; }
        }

        private List<Estado> _estados;

        public List<Estado> Estados
        {
            get { return _estados; }
            set { _estados = value; }
        }

        private List<UsuarioDireccion> _Direcciones;

        public List<UsuarioDireccion> Direcciones
        {
            get { return _Direcciones; }
            set { _Direcciones = value; }
        }

        private Telefono _telefono;

        public Telefono Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
        }

        private UsuarioPerfilEmpresa _UsuarioPerfilEmpresa;

        public UsuarioPerfilEmpresa UsuarioPerfilEmpresa
        {
            get { return _UsuarioPerfilEmpresa; }
            set { _UsuarioPerfilEmpresa = value; }
        }

        private List<Empresa> _empresas;

        public List<Empresa> Empresas
        {
            get { return _empresas; }
            set { _empresas = value; }
        }

        private List<Modulo> _modulosDelPerfil;

        public List<Modulo> ModulosPerfil
        {
            get { return _modulosDelPerfil; }
            set { _modulosDelPerfil = value; }
        }

        private List<Modulo> _modulosDelUsuario;

        public List<Modulo> ModulosUsuario
        {
            get { return _modulosDelUsuario; }
            set { _modulosDelUsuario = value; }
        }

        private Empresa _CEmpresa;

        public Empresa CEmpresa
        {
            get { return _CEmpresa; }
            set { _CEmpresa = value; }
        }


        private List<NivelAcceso> _NivelAccesos;

        public List<NivelAcceso> NivelAccesos
        {
            get { return _NivelAccesos; }
            set { _NivelAccesos = value; }
        }

        private NivelAcceso _NivelAcceso;

        public NivelAcceso NivelAcceso
        {
            get { return _NivelAcceso; }
            set { _NivelAcceso = value; }
        }


        public List<Usuario> LISTADEUSUARIOS = new List<Usuario>();
        public List<Perfil> LISTADEPERFIL = new List<Perfil>();
        public List<Status> LISTADESTATUS = new List<Status>();
        public List<TipoTelefono> ltsTipoTelefono = new List<TipoTelefono>();

        #endregion
        #region Metodos
        public void CargarListaDeUsuarios()
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "sp_BuscarUsuario";

            foreach (DataRow item in Conexion.Busquedas(comando).Rows)
            {
                CLASSUSUARIO = new Usuario()
                {
                    UIDUSUARIO = new Guid(item["UidUsuario"].ToString()),
                    STRNOMBRE = item["VchNombre"].ToString(),
                    STRAPELLIDOPATERNO = item["VchApellidoPaterno"].ToString(),
                    STRAPELLIDOMATERNO = item["VchApellidoMaterno"].ToString(),
                    DtFechaNacimiento = Convert.ToDateTime(item["DtFechaNacimiento"].ToString()),
                    STRCORREO = item["VchCorreo"].ToString(),
                    DtFechaInicio = Convert.ToDateTime(item["DtFechaInicio"].ToString()),
                    DtFechaFin = item.IsNull("DtFechaFin") ? (DateTime?)null : Convert.ToDateTime(item["DtFechaFin"].ToString()),
                    STRUSUARIO = item["VchUsuario"].ToString(),
                    STRPASSWORD = item["VchPassword"].ToString(),
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    UidStatus = new Guid(item["UidStatus"].ToString()),
                    StrPerfil = item["VchPerfil"].ToString(),
                    StrStatus = item["VchStatus"].ToString(),

                };
                LISTADEUSUARIOS.Add(CLASSUSUARIO);
            }
            
        }

        public void CargarListadeUsuarios2()
        {
            LISTADEUSUARIOS = repositoriousuario.CargarUsuarios();
        }


        public void CargarListaDeUsuariosPerfil(string strperfil)
        {
            foreach (DataRow item in Conexion.obtenerUsuarios().Rows)
            {
                CLASSUSUARIO = new Usuario()
                {
                    UIDUSUARIO = new Guid(item["UidUsuario"].ToString()),
                    STRNOMBRE = item["VchNombre"].ToString(),
                    STRAPELLIDOPATERNO = item["VchApellidoPaterno"].ToString(),
                    STRAPELLIDOMATERNO = item["VchApellidoMaterno"].ToString(),
                    DtFechaNacimiento = Convert.ToDateTime(item["DtFechaNacimiento"].ToString()),
                    STRCORREO = item["VchCorreo"].ToString(),
                    //STRCURP = item["VchCurp"].ToString(),
                    DtFechaInicio = Convert.ToDateTime(item["DtFechaInicio"].ToString()),
                    DtFechaFin = Convert.ToDateTime(item["DtFechaFin"].ToString()),
                    STRUSUARIO = item["VchUsuario"].ToString(),
                    STRPASSWORD = item["VchPassword"].ToString(),
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    UidStatus = new Guid(item["UidStatus"].ToString()),
                    StrPerfil = item["VchPerfil"].ToString(),
                    StrStatus = item["VchStatus"].ToString(),
                };
                LISTADEUSUARIOS.Add(CLASSUSUARIO);
            }
        }

        public void CargarListaDePerfil()
        {
            foreach (DataRow item in Conexion.obtenerPerfiles().Rows)
            {
                CLASSPERFIL = new Perfil()
                {
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    strPerfil = item["VchPerfil"].ToString()

                };
                LISTADEPERFIL.Add(CLASSPERFIL);
            }
        }

        public void CargarPerfiles()
        {
            LISTADEPERFIL = repositorioPerfiles.CargarPerfilPorNivel();
        }

        public void CargarListaDePerfilTodos()
        {
            foreach (DataRow item in Conexion.obtenerTodosPerfiles().Rows)
            {
                CLASSPERFIL = new Perfil()
                {
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    strPerfil = item["VchPerfil"].ToString()

                };
                LISTADEPERFIL.Add(CLASSPERFIL);
            }
        }

        public void ListaPerfilEncargado ()
        {
            foreach (DataRow item in Conexion.obtenerPerfileEncargado().Rows)
            {
                CLASSPERFIL = new Perfil()
                {
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    strPerfil = item["VchPerfil"].ToString()

                };
                LISTADEPERFIL.Add(CLASSPERFIL);
            }
        }

        public void CargarListaDePerfilBuscar()
        {
            foreach (DataRow item in Conexion.obtenerPerfiles().Rows)
            {
                CLASSPERFIL = new Perfil()
                {
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    strPerfil = item["VchPerfil"].ToString()

                };
                LISTADEPERFIL.Add(CLASSPERFIL);
            }
        }

        

        public void CargarListaDeStatus()
        {
            foreach (DataRow item in Conexion.obtenerStatus().Rows)
            {
                CLASSSTATUS = new Status()
                {
                    UidStatus = new Guid(item["UidStatus"].ToString()),
                    strStatus = item["VchStatus"].ToString()

                };
                LISTADESTATUS.Add(CLASSSTATUS);
            }
        }


        public void Obtenerusuario(Guid UIDUSUARIO)
        {
            foreach (DataRow item in Conexion.obtenerUsuarioseleccionado(UIDUSUARIO).Rows)
            {
                CLASSUSUARIO = new Usuario()
                {
                    UIDUSUARIO = new Guid(item["UidUsuario"].ToString()),
                    STRNOMBRE = item["VchNombre"].ToString(),
                    STRAPELLIDOPATERNO = item["VchApellidoPaterno"].ToString(),
                    STRAPELLIDOMATERNO = item["VchApellidoMaterno"].ToString(),
                    DtFechaNacimiento = Convert.ToDateTime(item["DtFechaNacimiento"].ToString()),
                    STRCORREO = item["VchCorreo"].ToString(),
                    //STRCURP = item["VchCurp"].ToString(),
                    DtFechaInicio = Convert.ToDateTime(item["DtFechaInicio"].ToString()),
                    DtFechaFin = item.IsNull("DtFechaFin") ? (DateTime?)null : Convert.ToDateTime(item["DtFechaFin"].ToString()),
                    STRUSUARIO = item["VchUsuario"].ToString(),
                    STRPASSWORD = item["VchPassword"].ToString(),
                    //UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    UidStatus = new Guid(item["UidStatus"].ToString()),
                    RutaImagen = item["VchRutaImagen"].ToString(),
                };

            }
        }

        public void ObtenerUsuarioPerfilEmpresa(Guid UIDUSUARIO)
        {
            foreach (DataRow item in Conexion.obtenerUsuarioPerfilEmpresaseleccionado(UIDUSUARIO).Rows)
            {
                UsuarioPerfilEmpresa = new UsuarioPerfilEmpresa()
                {
                    UidUsuario = new Guid(item["UidUsuario"].ToString()),
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    UidEmpresa = new Guid(item["UidEmpresa"].ToString())
                };

            }
        }

        public void ObtenerEmpresaUsuario(string uidempresa)
        {
            _CEmpresa = empresaRepository.Find(new Guid(uidempresa));
        }



        public void ObtenerPerfil(Guid UiPerfil)
        {
            _Perfil = repositorioPerfiles.CargarDatos(UiPerfil);
        }

        public void ObtenerStatus(Guid UidStatus)
        {
            foreach (DataRow item in Conexion.obtenerStatusSeleccionado(UidStatus).Rows)
            {
                CLASSSTATUS = new Status()
                {
                    UidStatus = new Guid(item["UidStatus"].ToString()),
                    strStatus = item["VchStatus"].ToString(),
                };
            }
        }
        public void InicializarLista()
        {
            LISTADEUSUARIOS = new List<Usuario>();
        }

        public bool GuardarUsuario(string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            string FechaNacimiento, string Correo, string FechaInicio,
            string FechaFin, string Usuario, string Password, string UidStatus, string rutaimagen)
        {
            CLASSUSUARIO = new Usuario()
            {
                STRNOMBRE = Nombre,
                STRAPELLIDOPATERNO = ApellidoPaterno,
                STRAPELLIDOMATERNO = ApellidoMaterno,
                DtFechaNacimiento = Convert.ToDateTime(FechaNacimiento),
                STRCORREO = Correo,
                //STRCURP = Curp,

                DtFechaInicio = Convert.ToDateTime(FechaInicio),
                DtFechaFin = FechaFin.Length == 0 ? (DateTime?)null : Convert.ToDateTime(FechaFin),
                STRUSUARIO = Usuario,
                STRPASSWORD = Password,
                UidStatus = new Guid(UidStatus),
                RutaImagen= rutaimagen
            };
            bool Resultado = false;
            try
            {
                Resultado = CLASSUSUARIO.GuardarDatos();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public bool ModificarUsuario(string UidUsuario, string Nombre, string ApellidoPaterno, string ApellidoMaterno,
            string FechaNacimiento, string Correo, string FechaInicio, string FechaFin,
            string Usuario, string Password,  string UidStatus, string rutaimagen)
        {

            CLASSUSUARIO = new Usuario()
            {
                UIDUSUARIO = new Guid(UidUsuario),

                STRNOMBRE = Nombre,
                STRAPELLIDOPATERNO = ApellidoPaterno,
                STRAPELLIDOMATERNO = ApellidoMaterno,
                DtFechaNacimiento = Convert.ToDateTime(FechaNacimiento),
                STRCORREO = Correo,
                // STRCURP = Curp,
                DtFechaInicio = Convert.ToDateTime(FechaInicio),
                DtFechaFin = FechaFin.Length == 0 ? (DateTime?)null : Convert.ToDateTime(FechaFin),
                STRUSUARIO = Usuario,
                STRPASSWORD = Password,
                UidStatus = new Guid(UidStatus),
                RutaImagen = rutaimagen
            };
            bool Resultado = false;
            try
            {
                Resultado = CLASSUSUARIO.ModificarDatos();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }


        public void BuscarUsuarios(string nombre, string ApellidoPaterno, string ApellidoMaterno,
            string FechaNacimiento, string FechaNacimiento2, string Correo, string FechaInicio, string FechaInicio2, string FechaFin, string FechaFin2, string perfil,
            string usuario,  string Status, string empresas)
        {
            SqlCommand comando = new SqlCommand();
            comando.CommandType = CommandType.StoredProcedure;
            comando.CommandText = "sp_BuscarUsuario";

            if (nombre != string.Empty)
            {
                comando.Parameters.Add("@VchNombre", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchNombre"].Value = nombre;
            }

            if (ApellidoPaterno != string.Empty)
            {
                comando.Parameters.Add("@VchApellidoPaterno", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchApellidoPaterno"].Value = ApellidoPaterno;
            }
            if (ApellidoMaterno != string.Empty)
            {
                comando.Parameters.Add("@VchApellidoMaterno", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchApellidoMaterno"].Value = ApellidoMaterno;
            }
            if (FechaNacimiento != string.Empty)
            {
                comando.Parameters.Add("@DtFechaNacimiento", SqlDbType.Date);
                comando.Parameters["@DtFechaNacimiento"].Value = Convert.ToDateTime(FechaNacimiento);
            }
            if (FechaNacimiento2 != string.Empty)
            {
                comando.Parameters.Add("@DtFechaNacimiento2", SqlDbType.Date);
                comando.Parameters["@DtFechaNacimiento2"].Value = Convert.ToDateTime(FechaNacimiento2);
            }
            if (Correo != string.Empty)
            {
                comando.Parameters.Add("@VchCorreo", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchCorreo"].Value = Correo;
            }

            if (FechaInicio != string.Empty)
            {
                comando.Parameters.Add("@DtFechaInicio", SqlDbType.Date);
                comando.Parameters["@DtFechaInicio"].Value = Convert.ToDateTime(FechaInicio);
            }

            if (FechaInicio2 != string.Empty)
            {
                comando.Parameters.Add("@DtFechaInicio2", SqlDbType.Date);
                comando.Parameters["@DtFechaInicio2"].Value = Convert.ToDateTime(FechaInicio2);
            }

            if (FechaFin != string.Empty)
            {
                comando.Parameters.Add("@DtFechaFin", SqlDbType.Date);
                comando.Parameters["@DtFechaFin"].Value = Convert.ToDateTime(FechaFin);
            }

            if (FechaFin2 != string.Empty)
            {
                comando.Parameters.Add("@DtFechaFin2", SqlDbType.Date);
                comando.Parameters["@DtFechaFin2"].Value = Convert.ToDateTime(FechaFin2);
            }

            if (usuario != string.Empty)
            {
                comando.Parameters.Add("@VchUsuario", SqlDbType.NVarChar, 50);
                comando.Parameters["@VchUsuario"].Value = usuario;
            }
            if (perfil != string.Empty)
            {
                comando.Parameters.Add("@UidPerfil", SqlDbType.NVarChar,4000);
                comando.Parameters["@UidPerfil"].Value = perfil;
            }
            if (Status != string.Empty)
            {
                comando.Parameters.Add("@UidStatus", SqlDbType.NVarChar, 4000);
                comando.Parameters["@UidStatus"].Value = Status;
            }
            if (empresas != string.Empty)
            {
                comando.AddParameter("@UidEmpresas", empresas, SqlDbType.NVarChar, 4000);
            }

            foreach (DataRow item in Conexion.Busquedas(comando).Rows)
            {
                CLASSUSUARIO = new Usuario()
                {
                    UIDUSUARIO = new Guid(item["UidUsuario"].ToString()),
                    STRNOMBRE = item["VchNombre"].ToString(),
                    STRAPELLIDOPATERNO = item["VchApellidoPaterno"].ToString(),
                    STRAPELLIDOMATERNO = item["VchApellidoMaterno"].ToString(),
                    DtFechaNacimiento = Convert.ToDateTime(item["DtFechaNacimiento"].ToString()),
                    STRCORREO = item["VchCorreo"].ToString(),
                    DtFechaInicio = Convert.ToDateTime(item["DtFechaInicio"].ToString()),
                    DtFechaFin = item.IsNull("DtFechaFin") ? (DateTime?)null : Convert.ToDateTime(item["DtFechaFin"].ToString()),
                    STRUSUARIO = item["VchUsuario"].ToString(),
                    STRPASSWORD=item["VchPassword"].ToString(),
                    UidPerfil = new Guid(item["UidPerfil"].ToString()),
                    UidStatus = new Guid(item["UidStatus"].ToString()),
                    StrPerfil = item["VchPerfil"].ToString(),
                    StrStatus = item["VchStatus"].ToString(),

                };
                LISTADEUSUARIOS.Add(CLASSUSUARIO);
            }

        }

        public void GuardarTelefonos(List<UsuarioTelefono> telefonos, Guid uidUsuario)
        {
            foreach (UsuarioTelefono telefono in telefonos)
            {
                telefono.UidUsuario = uidUsuario;
                telefonoRepository.Save(telefono);
            }
        }

        public void ObtenerDireccion(Guid uid)
        {
            _Direccion = direccionRepository.Find(uid);
        }

        public void ObtenerTelefono(Guid uid)
        {
            _telefono = telefonoRepository.Find(uid);
        }

        public void ObtenerPaises()
        {
            _paises = paisRepository.FindAll();
        }

        public void ObtenerEstados(Guid uidPais)
        {
            _estados = estadoRepository.FindAll(uidPais);
        }

        public void ObtenerDirecciones()
        {
            _Direcciones = direccionRepository.FindAll(_Usuario.UIDUSUARIO);
        }

        public void ObtenerTelefonos()
        {
            _telefonos = telefonoRepository.FindAll(_Usuario.UIDUSUARIO);
        }

        public void GuardarDirecciones(List<UsuarioDireccion> direcciones, Guid uidUsuario)
        {
            foreach (UsuarioDireccion direccion in direcciones)
            {
                direccion.UidUsuario = uidUsuario;
                direccionRepository.Save(direccion);
            }
        }

        public void EliminarDirecciones(List<UsuarioDireccion> direcciones)
        {
            foreach (UsuarioDireccion direccion in direcciones)
            {
                direccionRepository.Remove(direccion);
            }
        }
        

        public void EliminarTelefonos(List<UsuarioTelefono> telefonos)
        {
            foreach (UsuarioTelefono telefono in telefonos)
            {
                telefonoRepository.Remove(telefono);
            }
        }

        public void ObtenerTipoTelefonos()
        {
            _tipoTelefonos = (List<TipoTelefono>)tipoTelefonoRepository.FindAll();
        }


        public bool GuardarUsuarioPerfilEmpresa(string Perfil, Guid uidUsuario, string Empresa)
        {
            UsuarioPerfilEmpresa = new UsuarioPerfilEmpresa()
            {
                UidUsuario = uidUsuario,
                UidPerfil = new Guid(Perfil),
                UidEmpresa = new Guid(Empresa)

            };
            bool Resultado = false;
            try
            {
                Resultado = UsuarioPerfilEmpresa.GuardarUsuarioPerfilEmpresa();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;

        }

        public bool ModificarUsuarioPerfilEmpresa(string UidUsuario, string UidPerfil, string UidEmpresa)
        {

            UsuarioPerfilEmpresa = new UsuarioPerfilEmpresa()
            {
                UidUsuario = new Guid(UidUsuario),
                UidPerfil = new Guid(UidPerfil),
                UidEmpresa = new Guid(UidEmpresa)
            };
            bool Resultado = false;
            try
            {
                Resultado = UsuarioPerfilEmpresa.ModificarUsuarioPerfilEmpresa();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public void ObtenerEmpresa()
        {
            _empresas = empresaRepository.FindAll();
        }

        public void ObtenerEmpresas(Guid UidEMpresa)
        {
            _CEmpresa = empresaRepository.Find(UidEMpresa);
        }

        public void ActualizarModulosUsuario(Guid uidUsuario, List<Guid> modulos)
        {
            Acceso.ActualizarModulosDelUsuario(uidUsuario, modulos);
        }

        public void ObtenerModulosUsuario(Guid uidUsuario)
        {
            _modulosDelUsuario = Acceso.ObtenerModulosPorUsuario(uidUsuario);
        }

        public void ObtenerModulosPerfil(Guid uidPerfil, Guid? uidNivelAcceso = null)
        {
            _modulosDelPerfil = Acceso.ObtenerModulosPorPerfil(uidPerfil, uidNivelAcceso);
        }

        public void ObtenerNivelAccesos()
        {
            _NivelAccesos = repositorioNiveles.CargarNivel();
        }

        public void ObtenerNivelAcceso(Guid uid)
        {
            _NivelAcceso = repositorioNiveles.Find(uid);
        }

        public void BuscarEmpresa(string NombreComercial, string RFC, string RazonSocial)
        {
            _empresas = empresaRepository.BuscarEmpresa(NombreComercial,RFC, RazonSocial);
        }

        public void ObtenerNivelesDeAcceso()
        {
            _NivelAccesos = repositorioNiveles.FindAll();
        }

        public void ObtenerUsuarioPorNombre(string username)
        {
            _Usuario = repositoriousuario.FindByName(username);
        }
        #endregion
    }
}