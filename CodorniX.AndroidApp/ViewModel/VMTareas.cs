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
    class VMTareas
    {
        Tarea.Repositorio tareaRepository = new Tarea.Repositorio();

        private List<Tarea> _Tareas;

        public List<Tarea> Tareas
        {
            get { return _Tareas; }
            set { _Tareas = value; }
        }

        public void ObtenerTareas(Guid uidUsuario, Guid? uidTurno, Guid uidSucursal, DateTime fecha)
        {
            _Tareas = tareaRepository.FindByUser(uidUsuario, uidTurno, uidSucursal, fecha);
        }


    }
}