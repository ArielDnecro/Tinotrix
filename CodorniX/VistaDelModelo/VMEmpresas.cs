using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodorniX.VistaDelModelo
{
    public class VMEmpresas
    {
        private Empresa.Repository repository = new Empresa.Repository();
        private EmpresaTelefono.Repository telefonoRepository = new EmpresaTelefono.Repository();
        private EmpresaDireccion.Repository direccionRepository = new EmpresaDireccion.Repository();
        private Pais.Repository paisRepository = new Pais.Repository();
        private Estado.Repository estadoRepository = new Estado.Repository();
        private TipoTelefono.Repository tipoTelefonoRepository = new TipoTelefono.Repository();

        private List<Empresa> _empresas;

        public List<Empresa> Empresas
        {
            get { return _empresas; }
            set { _empresas = value; }
        }

        private Empresa _empresa;

        public Empresa Empresa
        {
            get { return _empresa; }
            set { _empresa = value; }
        }

        private List<EmpresaDireccion> _direcciones;

        public List<EmpresaDireccion> Direcciones
        {
            get { return _direcciones; }
            set { _direcciones = value; }
        }

        private Direccion _direccion;

        public Direccion Direccion
        {
            get { return _direccion; }
            set { _direccion = value; }
        }

        private List<EmpresaTelefono> _telefonos;

        public List<EmpresaTelefono> Telefonos
        {
            get { return _telefonos; }
            set { _telefonos = value; }
        }

        private Telefono _telefono;

        public Telefono Telefono
        {
            get { return _telefono; }
            set { _telefono = value; }
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

        private IList<TipoTelefono> _TipoTelefonos;

        public IList<TipoTelefono> TipoTelefonos
        {
            get { return _TipoTelefonos; }
            set { _TipoTelefonos = value; }
        }


        public void ObtenerEmpresas()
        {
            _empresas = repository.FindAll();
        }

        public void ObtenerEmpresa(Guid uid)
        {
            _empresa = repository.Find(uid);
        }

        public void ObtenerDirecciones()
        {
            _direcciones = direccionRepository.FindAll(_empresa.UidEmpresa);
        }

        public void ObtenerDireccion(Guid uid)
        {
            _direccion = direccionRepository.Find(uid);
        }

        public void ObtenerTelefonos()
        {
            _telefonos = telefonoRepository.FindAll(_empresa.UidEmpresa);
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

        public void BuscarEmpresas(string nombreComercial, string razonSocial, string giro, string rfc, DateTime? registradoDespues, DateTime? registradoAntes)
        {
            Empresa.Criteria criteria = new Empresa.Criteria()
            {
                NombreComercial = nombreComercial,
                RazonSocial = razonSocial,
                Giro = giro,
                RFC = rfc,
                FechaRegistroDespues = registradoDespues,
                FechaRegistroAntes = registradoAntes,
            };
            _empresas = repository.FindBy(criteria);
        }

        public void GuardarEmpresa(Empresa empresa)
        {
            repository.Save(empresa);
        }

        public void GuardarDirecciones(List<EmpresaDireccion> direcciones, Guid uidEmpresa)
        {
            foreach (EmpresaDireccion direccion in direcciones)
            {
                direccion.UidEmpresa = uidEmpresa;
                direccionRepository.Save(direccion);
            }
        }

        public void EliminarDirecciones(List<EmpresaDireccion> direcciones)
        {
            foreach (EmpresaDireccion direccion in direcciones)
            {
                direccionRepository.Remove(direccion);
            }
        }

        public void GuardarTelefonos(List<EmpresaTelefono> telefonos, Guid uidEmpresa)
        {
            foreach (EmpresaTelefono telefono in telefonos)
            {
                telefono.UidEmpresa = uidEmpresa;
                telefonoRepository.Save(telefono);
            }
        }

        public void EliminarTelefonos(List<EmpresaTelefono> telefonos)
        {
            foreach (EmpresaTelefono telefono in telefonos)
            {
                telefonoRepository.Remove(telefono);
            }
        }

        public void ObtenerTipoTelefonos()
        {
            _TipoTelefonos = tipoTelefonoRepository.FindAll();
        }
    }
}