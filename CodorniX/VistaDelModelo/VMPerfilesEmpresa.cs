using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.Modelo;
using CodorniX.ConexionDB;
using System.Data;

namespace CodorniX.VistaDelModelo
{
    public class VMPerfilesEmpresa
    {
        DBPerfiles perfil = new DBPerfiles();
        private Perfil.Repositorio repositorioPerfil = new Perfil.Repositorio();
        private NivelAcceso.Repositorio repositorioNivelAcceso = new NivelAcceso.Repositorio();
        private NivelAcceso.Repositorio repositorioNiveles = new NivelAcceso.Repositorio();
        private Modulo.Repositorio repositorioModulo = new Modulo.Repositorio();
        private Perfil _CPerfil;

        public Perfil CPerfil
        {
            get { return _CPerfil; }
            set { _CPerfil = value; }
        }

        private Empresa _CEmpresa;

        public Empresa CEmpresa
        {
            get { return _CEmpresa; }
            set { _CEmpresa = value; }
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
        public bool GuardarPerfil(string perfil, string UidNivelAcceso, Guid UidEmpresa,string home)
        {
            CPerfil = new Perfil()
            {
                strPerfil = perfil,
                UidNivelAcceso = new Guid(UidNivelAcceso),
                UidEmpresa=  UidEmpresa,
                UidHome= new Guid(home)

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

        public bool ModificarPerfil(string UidPerfil, string perfil, string UidNivelAcceso, Guid UidEmpresa, string home)
        {

            CPerfil = new Perfil()
            {
                UidPerfil = new Guid(UidPerfil),

                strPerfil = perfil,
                UidNivelAcceso = new Guid(UidNivelAcceso),
                UidEmpresa= UidEmpresa,
                UidHome= new Guid(home)
            };
            bool Resultado = false;
            try
            {
                Resultado = CPerfil.ModificarDatosconempresa();
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

        public void CargarPerfilPorEmpresa(Guid UidEmpresa)
        {
            ltsPerfil = repositorioPerfil.CargarTodosLosPerfilesporempresa(UidEmpresa);
        }

        public void Buscar(string perfil, Guid UidEmpresa)
        {
            Perfil.Criterio criterio = new Perfil.Criterio()
            {
                perfil = perfil,
                UidEmpesa= UidEmpresa
            };
            ltsPerfil = repositorioPerfil.buscarporempresa(criterio);
        }

        public void CargarModulos(string uidperfil)
        {
            _listModulo = repositorioModulo.CargarModulos(new Guid(uidperfil));
            _listModulo2 = repositorioModulo.CargarModulos(new Guid(uidperfil));
        }

        public void CargarTodoslosModulos()
        {
            _listModulo = repositorioModulo.CargarTodoslosModulos();
        }
        public void ObtenerModulos(string Acceso)
        {
            _listModulo = repositorioModulo.CargarTodosLosModulos(Acceso);
        }

        public void ActualizarModulos(Guid UidPerfil, List<Guid> modulos)
        {
            Modulo.ActualizarModulos(UidPerfil, modulos);
        }


        public Guid ObtenerNivelAcceso(string NivelAcceso)
        {
            Guid id = new Guid();
            foreach (DataRow item in perfil.obtenerNivelAcceso(NivelAcceso).Rows)
            {
                id = new Guid(item["UidNivelAcceso"].ToString());
            }
            return id;
        }

        public void ObtenerNivelAcceso()
        {
            _listNivelAcceso = repositorioNivelAcceso.CargarNivel();

        }

        public void ObtenerNivelesDeAcceso()
        {
            _listNivelAcceso = repositorioNivelAcceso.FindAll();

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

        public void ObtenerNivelAccesoPorNombre(string nombre)
        {
            _NivelAcceso = repositorioNiveles.FindByName(nombre);
        }

        public void ObtenerHome()
        {
            _listModulo = repositorioModulo.CargarHome();
        }
    }
}