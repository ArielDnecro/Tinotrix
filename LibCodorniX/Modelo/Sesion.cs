using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CodorniX.Modelo
{
    [Serializable]
    public class Sesion
    {
        public Guid uidUsuario;
        public List<Guid> uidEmpresasPerfiles;
        public List<Guid> uidSucursalesPerfiles;
        public Guid? uidEmpresaActual;
        public Guid? uidSucursalActual;
        public Guid? uidPerfilActual;
        public Guid? uidNivelAccesoActual;
        public Guid? UidPeriodo;
        public Guid? UidTurno;
        public string appWeb;
    }
}