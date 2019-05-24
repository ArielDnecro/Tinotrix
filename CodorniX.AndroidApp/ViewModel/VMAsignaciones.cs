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
    class VMAsignaciones
    {
        Periodo.Repository periodoRepository = new Periodo.Repository();

        private List<Periodo> _Periodos;

        public List<Periodo> Periodos
        {
            get { return _Periodos; }
            set { _Periodos = value; }
        }

        public void ObtenerPeriodos(Guid uidUsuario, DateTime? fecha, Guid uidSucursal)
        {
            Periodo.Criteria criteria = new Periodo.Criteria()
            {
                Sucursal = uidSucursal,
                Usuario = uidUsuario,
                FechaFinDespuesDe = fecha,
                FechaInicioAntesDe = fecha,
            };
            _Periodos = periodoRepository.FindBy(criteria, true);
        }
    }
}