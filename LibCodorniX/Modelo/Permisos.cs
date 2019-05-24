using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.ConexionDB;

namespace CodorniX.Modelo
{
    public class Permisos
    {
        Conexion Conexion = new Conexion();
        private Guid _UidPerfil;
        public Guid UIDPERFIL
        {
            get { return _UidPerfil; }
            set { _UidPerfil = value; }
        }
        private Guid _UidModulo;
        public Guid UidModulo
        {
            get { return _UidModulo; }
            set { _UidModulo = value; }
        }
    }
}