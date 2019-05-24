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
using CodorniX.AndroidApp.ViewModel;
using CodorniX.AndroidApp.Adapters;

namespace CodorniX.AndroidApp.Activities
{
    [Activity(Label = "Asignaciones")]
    public class Asignaciones : ListActivity
    {
        VMAsignaciones VM;
        List<Periodo> periodos = new List<Periodo>();
        AsignacionAdapter adapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Asignaciones);

            Sesion sesion = ((CodorniXApplication)Application).GetSesion();
            VM = new VMAsignaciones();
            VM.ObtenerPeriodos(sesion.uidUsuario, DateTime.Today, sesion.uidSucursalActual.Value);

            adapter = new AsignacionAdapter(this, VM.Periodos);

            ListAdapter = adapter;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Periodo periodo = adapter.GetItem(position);

            Toast.MakeText(this, periodo.StrNombreDepto, ToastLength.Short).Show();
        }
    }
}