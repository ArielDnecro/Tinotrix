using CodorniX.Common;
using CodorniX.Modelo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodorniX.VistaDelModelo
{
    public class VMSucursales
    {
        #region Propiedades
                private Sucursal.Repository repository = new Sucursal.Repository();
                private SucursalTelefono.Repository telefonoRepository = new SucursalTelefono.Repository();
                private SucursalDireccion.Repository direccionRepository = new SucursalDireccion.Repository();
        
                private SucursalImpresora.Repository impresoraRepository = new SucursalImpresora.Repository();
                private Status.Repository statusRepository = new Status.Repository();
                private SucursalFoto.Repository fotoRepository = new SucursalFoto.Repository();
                private SucursalFotoC.Repository fotoCRepository = new SucursalFotoC.Repository();
                private SucursalImpresoraTipo.Repository tipoimpresoraRepository = new SucursalImpresoraTipo.Repository();
                private SucursalLicencia.Repository LicenciaRepository = new SucursalLicencia.Repository();

                private EmpresaDireccion.Repository empresaDireccionRepository = new EmpresaDireccion.Repository();
                private Pais.Repository paisRepository = new Pais.Repository();
                private Estado.Repository estadoRepository = new Estado.Repository();
                private TipoTelefono.Repository tipoTelefonoRepository = new TipoTelefono.Repository();
                private TipoSucursal.Repository tipoSucursalRepository = new TipoSucursal.Repository();
                private UnidadMedida.Repository UnidadMedidaRepository = new UnidadMedida.Repository();
                private SucursalPapel.Repository PapelRepository = new SucursalPapel.Repository();
                private SucursalPapelC.Repository PapelCRepository = new SucursalPapelC.Repository();
                private SucursalServidor.Repository ServidorRepository = new SucursalServidor.Repository();

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
                //VMGlobalDireccion _CVMObjGlobalDireccion = new VMGlobalDireccion();
                //public VMGlobalDireccion CVMObjGlobalDireccion
                //{ get { return _CVMObjGlobalDireccion; } set { _CVMObjGlobalDireccion = value; } }
                #endregion sucursales

                #region direcciones
                private List<EmpresaDireccion> _EmpresaDirecciones;

                public List<EmpresaDireccion> EmpresaDirecciones
                {
                    get { return _EmpresaDirecciones; }
                    set { _EmpresaDirecciones = value; }
                }
                private List<SucursalDireccion> _direcciones;

                public List<SucursalDireccion> Direcciones
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
                #endregion direcciones

                #region telefonos
                private IList<TipoTelefono> _TipoTelefonos;

                public IList<TipoTelefono> TipoTelefonos
                {
                    get { return _TipoTelefonos; }
                    set { _TipoTelefonos = value; }
                }
                private List<SucursalTelefono> _telefonos;

                public List<SucursalTelefono> Telefonos
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
                #endregion telefonos

                #region impresoras
                private List<SucursalImpresora> _impresoras;

                public List<SucursalImpresora> Impresoras
                {
                    get { return _impresoras; }
                    set { _impresoras = value; }
                }

                private List<SucursalImpresoraTipo> _tipoimpresoras;

                public List<SucursalImpresoraTipo> TipoImpresoras
                {
                    get { return _tipoimpresoras; }
                    set { _tipoimpresoras = value; }
                }

                #endregion impresoras

                #region fotos
                private List<SucursalFoto> _fotos;

                public List<SucursalFoto> Fotos
                {
                    get { return _fotos; }
                    set { _fotos = value; }
                }

                private List< UnidadMedida > _Medidas;

                public List<UnidadMedida> Medidas
                {
                    get { return _Medidas; }
                    set { _Medidas = value; }
                }
        #endregion fotos

                #region fotos Comercial
                private List<SucursalFotoC> _fotosC;

                public List<SucursalFotoC> FotosC
                {
                    get { return _fotosC; }
                    set { _fotosC = value; }
                }

                //private List<UnidadMedida> _MedidasC;

                //public List<UnidadMedida> MedidasC
                //{
                //    get { return _MedidasC; }
                //    set { _MedidasC = value; }
                //}
                #endregion fotos Comercial

                #region Licencias
                private List<SucursalLicencia> _Licencias;
                        public List<SucursalLicencia> Licencias
                        {
                            get { return _Licencias; }
                            set { _Licencias = value; }
                        }

                        string _StrOrdenaPor = string.Empty;
                        public string StrOrdenaPor
                        {
                            get { return _StrOrdenaPor; }
                            set { _StrOrdenaPor = value; }
                        }
                        Orden _EnuOrden = Orden.ASC;
                        public Orden EnuOrden
                        {
                            get { return _EnuOrden; }
                            set { _EnuOrden = value; }
                        }
                        #endregion Licencias

                #region Papel
                private SucursalPapel _Papel;

                public SucursalPapel Papel
                {
                    get { return _Papel; }
                    set { _Papel = value; }
                }

        //string _StrOrdenaFPPor = string.Empty;
        //public string StrOrdenaFPPor
        //{
        //    get { return _StrOrdenaFPPor; }
        //    set { _StrOrdenaFPPor = value; }
        //}

        //Orden _EnuOrdenFP = Orden.ASC;
        //public Orden EnuOrdenFP
        //{
        //    get { return _EnuOrdenFP; }
        //    set { _EnuOrdenFP= value; }
        //}
        #endregion Papel

                #region Papel Comercial
                private SucursalPapelC _PapelC;
                public SucursalPapelC PapelC
                {
                    get { return _PapelC; }
                    set { _PapelC = value; }
                }
                #endregion Papel Comercial

                #region servidor
                private SucursalServidor _Servidor;
                        public SucursalServidor Servidor
                        {
                            get { return _Servidor; }
                            set { _Servidor = value; }
                        }
                        #endregion servidor

                #region varios
                private IList<Status> _ListaStatus;

                public IList<Status> ListaStatus
                {
                    get { return _ListaStatus; }
                    set { _ListaStatus = value; }
                }


                #endregion varios


        #endregion Propiedades
        #region Funciones

        #region sucursal
        public void ObtenerTipoSucursales()
        {
            _TipoSucursales = tipoSucursalRepository.FindAll();
        }
        public void ObtenerSucursales(Guid uidEmpresa)
        {
            _Sucursales = repository.FindAll(uidEmpresa);
        }

        public void ObtenerSucursal(Guid uid)
        {
            _Sucursal = repository.Find(uid);
        }
        public void BuscarSucursales(string nombre, DateTime? registradoDespues, DateTime? registradoAntes, string uids, Guid empresa, Guid status)
        {
            Sucursal.Criteria criteria = new Sucursal.Criteria()
            {
                Nombre = nombre,
                FechaRegistroDespues = registradoDespues,
                FechaRegistroAntes = registradoAntes,
                Tipos = uids,
                Empresa = empresa,
                Status = status
            };
            _Sucursales = repository.FindBy(criteria);
        }

        public void GuardarSucursal(Sucursal Sucursal)
        {
            repository.Save(Sucursal);
        }
        #endregion sucursal 

        #region direcciones
        public void GuardarDirecciones(List<SucursalDireccion> direcciones, Guid uidSucursal)
        {
            foreach (SucursalDireccion direccion in direcciones)
            {
                direccion.UidSucursal = uidSucursal;
                direccionRepository.Save(direccion);
            }
        }

        public void EliminarDirecciones(List<SucursalDireccion> direcciones)
        {
            foreach (SucursalDireccion direccion in direcciones)
            {
                direccionRepository.Remove(direccion);
            }
        }
        public void ObtenerDirecciones()
        {
            _direcciones = direccionRepository.FindAll(_Sucursal.UidSucursal);
        }

        public void ObtenerDireccion(Guid uid)
        {
            _direccion = direccionRepository.Find(uid);
        }

        public void ObtenerEmpresaDirecciones(Guid uid)
        {
            _EmpresaDirecciones = empresaDireccionRepository.FindAll(uid);
        }

        public void ObtenerEmpresaDireccion(Guid uid)
        {
            _direccion = empresaDireccionRepository.Find(uid);
        }
        public void ObtenerPaises()
        {
            _paises = paisRepository.FindAll();
        }

        public void ObtenerEstados(Guid uidPais)
        {
            _estados = estadoRepository.FindAll(uidPais);
        }

        #endregion direcciones

        #region telefonos
        public void ObtenerTelefonos()
        {
            _telefonos = telefonoRepository.FindAll(_Sucursal.UidSucursal);
        }

        public void ObtenerTelefono(Guid uid)
        {
            _telefono = telefonoRepository.Find(uid);
        }

        public void GuardarTelefonos(List<SucursalTelefono> telefonos, Guid uidSucursal)
        {
            foreach (SucursalTelefono telefono in telefonos)
            {
                telefono.UidSucursal = uidSucursal;
                telefonoRepository.Save(telefono);
            }
        }

        public void EliminarTelefonos(List<SucursalTelefono> telefonos)
        {
            foreach (SucursalTelefono telefono in telefonos)
            {
                telefonoRepository.Remove(telefono);
            }
        }

        public void ObtenerTipoTelefonos()
        {
            _TipoTelefonos = tipoTelefonoRepository.FindAll();
        }

        #endregion telefonos
        
        #region impresoras
        public void ObtenerImpresoras()
        {
            _impresoras = impresoraRepository.FindAll(_Sucursal.UidSucursal);
        }

        public void ObtenerTipoImpresoras()
        {
            _tipoimpresoras = tipoimpresoraRepository.FindAll();
        }
        public void GuardarImpresoras(List<SucursalImpresora> impresoras, Guid uidSucursal)
        {
            foreach (SucursalImpresora impresora in impresoras)
            {
                impresora.UidSucursal = uidSucursal;
                impresoraRepository.Save(impresora);
            }
        }

        public void EliminarImpresoras(List<SucursalImpresora> impresoras)
        {
            foreach (SucursalImpresora impresora in impresoras)
            {
                impresoraRepository.Remove(impresora);
            }
        }


        #endregion impresoras

        #region fotos

        public void ObtenerMedidas() {
            _Medidas = UnidadMedidaRepository.FindAll();
        }
        public void Obtenerfotos()
        {
            _fotos = fotoRepository.FindAll(_Sucursal.UidSucursal);
        }

        //public void ObtenerImpresora(Guid uid)
        //{
        //    _telefono = telefonoRepository.Find(uid);
        //}

        public void GuardarFotos(List<SucursalFoto> fotos, Guid uidSucursal)
        {
            foreach (SucursalFoto foto in fotos)
            {
                foto.UidSucursal = uidSucursal;
                fotoRepository.Save(foto);
            }
        }

        public void EliminarFotos(List<SucursalFoto> fotos)
        {
            foreach (SucursalFoto foto in fotos)
            {
                fotoRepository.Remove(foto);
            }
        }

        #endregion fotos

        #region fotos Comercial

        //public void ObtenerMedidas()
        //{
        //    _Medidas = UnidadMedidaRepository.FindAll();
        //}
        public void ObtenerfotosC()
        {
            _fotosC = fotoCRepository.FindAll(_Sucursal.UidSucursal);
        }
        
        public void GuardarFotosC(List<SucursalFotoC> fotosC, Guid uidSucursal)
        {
            foreach (SucursalFotoC foto in fotosC)
            {
                foto.UidSucursal = uidSucursal;
                fotoCRepository.Save(foto);
            }
        }

        public void EliminarFotosC(List<SucursalFotoC> fotosC)
        {
            foreach (SucursalFotoC foto in fotosC)
            {
                fotoCRepository.Remove(foto);
            }
        }

        #endregion fotos

        #region Licencias 
        public void ObtenerLicencias()
        {
            _Licencias = LicenciaRepository.FindAll(_Sucursal.UidSucursal);
        }
        public void GuardarLicencias(List<SucursalLicencia> Licencias, Guid uidSucursal)
        {
            LicenciaRepository.EliminarAllSucursal(uidSucursal);
            foreach (SucursalLicencia licencia in Licencias)
            {
                licencia.UidSucursal = uidSucursal;
                LicenciaRepository.Save(licencia);
            }
        }
        public void OrdenarListaLicencia(string ordenarpor)
        {
            try
            {
                if (ordenarpor == _StrOrdenaPor)
                {
                    if (_EnuOrden == Orden.ASC)
                    {
                        _EnuOrden = Orden.DESC;
                    }
                    else
                    {
                        _EnuOrden = Orden.ASC;
                    }
                }
                else
                {
                    _StrOrdenaPor = ordenarpor;
                    _EnuOrden = Orden.ASC;
                }
                OrdenarListaLicencia();
            }
            catch (Exception ex)
            {
                throw new VMSucursalesException("(OrdenarListaLicencia(string))" + ex.Message);
            }
        }
        public void OrdenarListaLicencia()
        {
            try
            {
                if (_EnuOrden == Orden.ASC)
                {
                    switch (_StrOrdenaPor)
                    {
                        case "No":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderBy(licencia => licencia.IntNo));
                            break;
                        case "Licencia":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderBy(licencia => licencia.UidLicencia));
                            break;
                        case "StatusLicencia":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderBy(licencia => licencia.BooStatusLicencia));
                            break;
                        case "Status":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderBy(licencia => licencia.BooStatus));
                            break;
                        default:
                            Licencias = new List<SucursalLicencia>(Licencias.OrderBy(licencia => licencia.IntNo));
                            break;
                    }
                }
                else
                {
                    switch (_StrOrdenaPor)
                    {
                        case "No":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderByDescending(licencia => licencia.IntNo));
                            break;
                        case "Licencia":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderByDescending(licencia => licencia.UidLicencia));
                            break;
                        case "StatusLicencia":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderByDescending(licencia => licencia.BooStatusLicencia));
                            break;
                        case "Status":
                            Licencias = new List<SucursalLicencia>(Licencias.OrderByDescending(licencia => licencia.BooStatus));
                            break;
                        default:
                            Licencias = new List<SucursalLicencia>(Licencias.OrderByDescending(licencia => licencia.IntNo));
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new VMSucursalesException("(OrdenarListaDirecciones)" + ex.Message);
            }
        }
        #endregion Licencias

        #region Papel
        public void GuardarPapel( SucursalPapel PapelNuevo)
        {
            //foreach (SucursalFoto foto in fotos)
            //{
                //PapelNuevo.UidPapel = Sucursal.UidSucursal;
                PapelRepository.Save(PapelNuevo);
            //}
        }

        public void ObtenerPapel(Guid uid)
        {
            _Papel = PapelRepository.Find(uid);
        }

        //public List<SucursalFoto> OrdenarListaFP(string ordenarpor, Orden OrdenFP, List<SucursalFoto> fotos )
        //{
        //    try
        //    {
        //        if (ordenarpor == _StrOrdenaFPPor)
        //        {
        //            if (_EnuOrdenFP == Orden.ASC)
        //            {
        //                _EnuOrdenFP = Orden.DESC;
        //            }
        //            else
        //            {
        //                _EnuOrdenFP = Orden.ASC;
        //            }
        //        }
        //        else
        //        {
        //            _StrOrdenaFPPor = ordenarpor;
        //            _EnuOrdenFP = Orden.ASC;
        //        }
        //       return OrdenarListaFP(fotos, ordenarpor);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new VMSucursalesException("(OrdenarLista de fotos papel(string))" + ex.Message);
        //    }
        //}
        public List<SucursalFoto> OrdenarListaFP(string ordenarpor, Orden OrdenFP, List<SucursalFoto> fotos)
        {
            try
            {
                if (OrdenFP == Orden.ASC)
                {
                    switch (ordenarpor)
                    {
                        case "Descripcion":
                            fotos = new List<SucursalFoto>(fotos.OrderBy(Descripcion => Descripcion.StrDescripcion));
                            break;
                        default:
                            fotos = new List<SucursalFoto>(fotos.OrderBy(Descripcion => Descripcion.StrDescripcion));
                            break;
                    }
                }
                else
                {
                    switch (ordenarpor)
                    {
                        case "Descripcion":
                            fotos = new List<SucursalFoto>(fotos.OrderByDescending(Descripcion => Descripcion.StrDescripcion));
                            break;
                        default:
                            fotos = new List<SucursalFoto>(fotos.OrderByDescending(Descripcion => Descripcion.StrDescripcion));
                            break;
                    }
                }
                return fotos;
            }
            catch (Exception ex)
            {
                throw new VMSucursalesException("(OrdenarListaFOTOSPAPEL)" + ex.Message);
            }
        }
        #endregion Papel

        #region Papel
        public void GuardarPapelC(SucursalPapelC PapelNuevo)
        {
            PapelCRepository.Save(PapelNuevo);
        }

        public void ObtenerPapelC(Guid uid)
        {
            _PapelC = PapelCRepository.Find(uid);
        }
        
        public List<SucursalFotoC> OrdenarListaFPC(string ordenarpor, Orden OrdenFP, List<SucursalFotoC> fotos)
        {
            try
            {
                if (OrdenFP == Orden.ASC)
                {
                    switch (ordenarpor)
                    {
                        case "Descripcion":
                            fotos = new List<SucursalFotoC>(fotos.OrderBy(Descripcion => Descripcion.StrDescripcion));
                            break;
                        default:
                            fotos = new List<SucursalFotoC>(fotos.OrderBy(Descripcion => Descripcion.StrDescripcion));
                            break;
                    }
                }
                else
                {
                    switch (ordenarpor)
                    {
                        case "Descripcion":
                            fotos = new List<SucursalFotoC>(fotos.OrderByDescending(Descripcion => Descripcion.StrDescripcion));
                            break;
                        default:
                            fotos = new List<SucursalFotoC>(fotos.OrderByDescending(Descripcion => Descripcion.StrDescripcion));
                            break;
                    }
                }
                return fotos;
            }
            catch (Exception ex)
            {
                throw new VMSucursalesException("(OrdenarLista FOTOS PAPEL COMERCIAL)" + ex.Message);
            }
        }
        #endregion Papel

        #region servidor
        public bool ObtenerServidor() {
            bool existe = false;
            if (_Sucursal.UidSucursal != Guid.Empty) {
                _Servidor = ServidorRepository.Obtener(_Sucursal.UidSucursal);
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
        public void EliminarServidor(Guid UidSucursal) {
            try
            {
                if (UidSucursal != Guid.Empty)
                {
                    if (UidSucursal != null)
                    {
                        ServidorRepository.Eliminar(_Sucursal.UidSucursal);
                    }
                }
            }
            catch
            {

            }
         }
        public void SalvarServidor(String StrServidorIP, String StrPuerto, Guid UidSucursal) {
            SucursalServidor server = new SucursalServidor();
            server.UidSucursal = UidSucursal;
            server.StrNombreIP = StrServidorIP;
            server.StrPuerto = StrPuerto;
            ServidorRepository.Salvar(server);
        }
        #endregion servidor
        //ObtenerStatus -> funcion que sirve en impresoras y fotografias.
        public void ObtenerStatus()
        {
            _ListaStatus = statusRepository.FindAll();
        }

        #endregion Funciones
        #region Excepciones

        public class VMSucursalesException : Exception
        {
            public VMSucursalesException(string mensaje) : base("(VMSucursalesException)" + mensaje) { }
        }

        #endregion Excepciones
    }

}