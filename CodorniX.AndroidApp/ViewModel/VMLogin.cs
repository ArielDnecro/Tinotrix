using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using CodorniX.Modelo;

namespace CodorniX.AndroidApp.ViewModel
{
    class VMLogin
    {
        Usuario.Repository usuarioRepository = new Usuario.Repository();
        Sucursal.Repository sucursalRepository = new Sucursal.Repository();
        UsuarioPerfilSucursal.Repository usuarioPerfilSucursalRepository = new UsuarioPerfilSucursal.Repository();

        private Usuario _Usuario;

        public Usuario Usuario
        {
            get { return _Usuario; }
            set { _Usuario = value; }
        }

        private List<UsuarioPerfilSucursal> _PerfilesSucursales;

        public List<UsuarioPerfilSucursal> PerfilesSucursales
        {
            get { return _PerfilesSucursales; }
            set { _PerfilesSucursales = value; }
        }

        public void ObtenerUsuario(string user)
        {
            _Usuario = usuarioRepository.FindByName(user);
        }

        public void ObtenerPerfilesSucursales(Guid uidUsuario)
        {
            _PerfilesSucursales = usuarioPerfilSucursalRepository.FindAll(uidUsuario);
        }
    }
}