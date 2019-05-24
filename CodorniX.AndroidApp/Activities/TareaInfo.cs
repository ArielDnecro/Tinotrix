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

namespace CodorniX.AndroidApp.Activities
{
    [Activity(Label = "Información de la Tarea")]
    public class TareaInfo : Activity
    {
        VMTareaInfo VM;
        TextView Tarea;
        TextView Descripcion;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.TareaInfo);

            if (Intent.Extras == null)
            {
                Finish();
                return;
            }

            string uid = Intent.Extras.GetString("UidTarea");
            Guid uidTarea = new Guid(uid);
            uid = Intent.Extras.GetString("UidDepartamento");
            Guid uidDepartamento = new Guid(uid);
            uid = Intent.Extras.GetString("UidTurno");
            Guid uidTurno = new Guid(uid);

            VM = new VMTareaInfo();
            VM.ObtenerTarea(uidTarea);
            VM.ObtenerDepartamento(uidDepartamento);
            VM.ObtenerTurno(uidTurno);

            Tarea = FindViewById<TextView>(Resource.Id.tarea);
            Descripcion = FindViewById<TextView>(Resource.Id.descripcion);

            Tarea.Text = VM.Tarea.StrNombre;
            Descripcion.Text = VM.Tarea.StrDescripcion;
        }
    }
}