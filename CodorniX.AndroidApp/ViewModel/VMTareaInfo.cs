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
    class VMTareaInfo
    {
        Tarea.Repositorio tareaRepository = new Tarea.Repositorio();
        Departamento.Repository departamentoRepository = new Departamento.Repository();
        Turno.Repository turnoRepository = new Turno.Repository();

        private Tarea _Tarea;

        public Tarea Tarea
        {
            get { return _Tarea; }
            set { _Tarea = value; }
        }

        private Departamento _Departamento;

        public Departamento Departamento
        {
            get { return _Departamento; }
            set { _Departamento = value; }
        }

        private Turno _Turno;

        public Turno Turno
        {
            get { return _Turno; }
            set { _Turno = value; }
        }

        public void ObtenerTarea(Guid uid)
        {
            _Tarea = tareaRepository.Encontrar(uid);
        }

        public void ObtenerDepartamento(Guid uid)
        {
            _Departamento = departamentoRepository.Encontrar(uid);
        }

        public void ObtenerTurno(Guid uid)
        {
            _Turno = turnoRepository.Find(uid);
        }
    }
}