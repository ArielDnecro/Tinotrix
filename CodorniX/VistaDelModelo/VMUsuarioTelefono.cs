using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.ConexionDB;
using CodorniX.Modelo;

namespace CodorniX.VistaDelModelo
{
    public class VMUsuarioTelefono
    {
        DBUsuarioTelefono conexion = new DBUsuarioTelefono();
        private UsuarioTelefono _ObjUsuarioTelefono;
        public UsuarioTelefono ObjUsuarioTelefono
        {
            get { return _ObjUsuarioTelefono; }
            set { _ObjUsuarioTelefono = value; }
        }

        private Telefono _ObjTelefono;

        public Telefono ObjTelefono
        {
            get { return _ObjTelefono; }
            set { _ObjTelefono = value; }
        }

        private TipoTelefono _ObjTipoTelefono;

        public TipoTelefono ObjTipoTelefono
        {
            get { return _ObjTipoTelefono; }
            set { _ObjTipoTelefono = value; }
        }

        public List<Telefono> ltsTelefono = new List<Telefono>();
        public List<TipoTelefono> ltsTipoTelefono = new List<TipoTelefono>();
        public List<Usuario> ltsUsuario = new List<Usuario>();

    }
}