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
using CodorniX.AndroidApp.ViewModel;
using CodorniX.AndroidApp.Adapters;
using CodorniX.Modelo;

namespace CodorniX.AndroidApp.Activities
{
    [Activity(Label = "Tareas")]
    public class Tareas : ListActivity
    {
        VMTareas VM;
        TareaAdapter adapter = null;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Tareas);
            Sesion sesion = ((CodorniXApplication)Application).GetSesion();

            VM = new VMTareas();
            VM.ObtenerTareas(sesion.uidUsuario, null, sesion.uidSucursalActual.Value, DateTime.Today);

            adapter = new TareaAdapter(this, VM.Tareas);

            ListAdapter = adapter;
        }

        protected override void OnListItemClick(ListView l, View v, int position, long id)
        {
            Tarea tarea = adapter.GetItem(position);
            Intent intent = new Intent(this, typeof(TareaInfo));
            Bundle bundle = new Bundle();
            bundle.PutString("UidTarea", tarea.UidTarea.ToString());
            bundle.PutString("UidDepartamento", tarea.UidDepartamento.ToString());
            bundle.PutString("UidTurno", tarea.UidTurno.ToString());
            intent.PutExtras(bundle);
            StartActivity(intent);
        }
    }
}