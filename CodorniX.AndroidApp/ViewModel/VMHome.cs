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
    class VMHome
    {
        private Encargado.Repository encargadoRepository = new Encargado.Repository();

        private Usuario _Encargado;

        public Usuario Encargado
        {
            get { return _Encargado; }
            set { _Encargado = value; }
        }

        public void ObtenerEncargado(Guid uid)
        {
            _Encargado = encargadoRepository.Find(uid);
        }

    }
}