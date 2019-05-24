using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.Modelo;
using System.Data;
using System.Data.SqlClient;
using CodorniX.ConexionDB;

namespace CodorniX.VistaDelModelo
{
    public class VMPerfiles
    {
        #region Propiedades
        
        private Perfil.Repositorio repositorioPerfil = new Perfil.Repositorio();
        private NivelAcceso.Repositorio repositorioNivelAcceso = new NivelAcceso.Repositorio();
        private Modulo.Repositorio repositorioModulo = new Modulo.Repositorio();
        private Perfil.Repositorio repositorioPerfiles = new Perfil.Repositorio();
        private NivelAcceso.Repositorio repositorioNiveles = new NivelAcceso.Repositorio();

        private Perfil _CPerfil;

        public Perfil CPerfil
        {
            get { return _CPerfil; }
            set { _CPerfil = value; }
        }

        private Modulo _CModulo;

        public Modulo CModulo
        {
            get { return _CModulo; }
            set { _CModulo = value; }
        }


        private List<Perfil> _ltsPerfil;

        public List<Perfil> ltsPerfil
        {
            get { return _ltsPerfil; }
            set { _ltsPerfil = value; }
        }

        private IList<NivelAcceso> _ltsNivelAcceso;

        public IList<NivelAcceso> ltsNivelAcceso
        {
            get { return _ltsNivelAcceso; }
            set { _ltsNivelAcceso = value; }
        }

        private IList<Modulo> _ltsModulo;

        public IList<Modulo> ltsModulo
        {
            get { return _ltsModulo; }
            set { _ltsModulo = value; }
        }

        private List<Modulo> _listModulo;

        public List<Modulo> listModulo
        {
            get { return _listModulo; }
            set { _listModulo = value; }
        }

        private List<Modulo> _listModulo2;

        public List<Modulo> listModulo2
        {
            get { return _listModulo2; }
            set { _listModulo2 = value; }
        }

        private List<Modulo> _listModuloBackend;

        public List<Modulo> listModuloBackend
        {
            get { return _listModuloBackend; }
            set { _listModuloBackend = value; }
        }

        private List<Modulo> _listModulo2Backend;

        public List<Modulo> listModulo2Backend
        {
            get { return _listModulo2Backend; }
            set { _listModulo2Backend = value; }
        }


        private List<NivelAcceso> _listNivelAcceso;

        public List<NivelAcceso> listNivelAcceso
        {
            get { return _listNivelAcceso; }
            set { _listNivelAcceso = value; }
        }


        private NivelAcceso _CNivelAcceso;

        public NivelAcceso CNivelAcceso
        {
            get { return _CNivelAcceso; }
            set { _CNivelAcceso = value; }
        }

        private List<Modulo> _modulosDelPerfil;

        public List<Modulo> ModulosPerfil
        {
            get { return _modulosDelPerfil; }
            set { _modulosDelPerfil = value; }
        }

        private List<Modulo> _modulos;

        public List<Modulo> Modulos
        {
            get { return _modulos; }
            set { _modulos = value; }
        }

        private NivelAcceso _NivelAcceso;

        public NivelAcceso NivelAcceso
        {
            get { return _NivelAcceso; }
            set { _NivelAcceso = value; }
        }

        public void InicializarLista()
        {
            ltsPerfil = new List<Perfil>();
        }


        #endregion
        public bool GuardarPerfil(string perfil, string UidNivelAcceso, string UidEmpresa)
        {
            CPerfil = new Perfil()
            {
                strPerfil = perfil,
                UidNivelAcceso = new Guid(UidNivelAcceso),
                UidEmpresa =new Guid( UidEmpresa)

            };
            bool Resultado = false;
            try
            {
                Resultado = CPerfil.GuardarDatos();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public bool GuardarPerfilsinempresa(string perfil, string UidNivelAcceso,string UidHome)
        {
            CPerfil = new Perfil()
            {
                strPerfil = perfil,
                UidNivelAcceso = new Guid(UidNivelAcceso),
                UidHome = new Guid(UidHome)

            };
            bool Resultado = false;
            try
            {
                Resultado = CPerfil.GuardarDatossinempresa();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public bool ModificarPerfil(string UidPerfil, string perfil, string UidNivelAcceso, string UidHome)
        {

            CPerfil = new Perfil()
            {
                UidPerfil = new Guid(UidPerfil),

                strPerfil = perfil,
                UidNivelAcceso = new Guid( UidNivelAcceso), 
                UidHome= new Guid(UidHome)
            };
            bool Resultado = false;
            try
            {
                Resultado = CPerfil.ModificarDatos();
            }
            catch (Exception)
            {

                throw;
            }
            return Resultado;
        }

        public void ObtenerPerfiles(Guid UidPerfil)
        {
            _CPerfil = repositorioPerfil.CargarDatos(UidPerfil);
        }

        public void CargarPerfiles()
        {
            ltsPerfil = repositorioPerfil .CargarPerfilPorNivel();
        }

        public void Buscar(string perfil, string NivelAcceso)
        {
            Perfil.Criterio criterio = new Perfil.Criterio()
            {
                perfil = perfil,
                nivelacceso=NivelAcceso,
                
            };
            ltsPerfil = repositorioPerfil.buscar(criterio);
        }

        public void CargarModulos(Guid uidperfil)
        {
            _listModulo2 = repositorioModulo.CargarModulos(uidperfil);
        }

        public void CargarModulosBackend(Guid uidperfil)
        {
            _listModulo2Backend = repositorioModulo.CargarModulosBackend(uidperfil);
        }

        public void CargarTodoslosModulos()
        {
            _listModulo = repositorioModulo.CargarTodoslosModulos();
        }

        public void CargarTodoslosModulosBakcend()
        {
            _listModuloBackend = repositorioModulo.CargarTodoslosModulosBackend();
        }

        public void ObtenerModulos(string Acceso)
        {
            _listModulo = repositorioModulo.CargarTodosLosModulos(Acceso);
        }

        public void ActualizarModulos(Guid UidPerfil, List<Guid> modulos)
        {
            Modulo.ActualizarModulos(UidPerfil, modulos);
        }
        
        public void ObtenerNivelAcceso()
        {
            _listNivelAcceso = repositorioNivelAcceso.CargarNivel();

        }

        public void ObtenerHome()
        {
            _listModulo = repositorioModulo.CargarHome();
        }

        public void ObtenerModulosPerfil(Guid uidPerfil, Guid? uidNivelAcceso = null)
        {
            _modulosDelPerfil = Acceso.ObtenerModulosPorPerfil(uidPerfil, uidNivelAcceso);
        }

        public void ObtenerModulosNivel(Guid uidNivelAcceso)
        {
            _modulos = Acceso.ObtenerModulosPorNivel(uidNivelAcceso);
        }

        public void ObtenerNivelAcceso(Guid uid)
        {
            _NivelAcceso = repositorioNiveles.Find(uid);
        }
        
        public void ObtenerNivelesDeAcceso()
        {
            _listNivelAcceso = repositorioNivelAcceso.FindAll();

        }
    }
}