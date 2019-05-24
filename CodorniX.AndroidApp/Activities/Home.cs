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

namespace CodorniX.AndroidApp.Activities
{
    [Activity(Label = "CodorniX", MainLauncher = true, Icon = "@drawable/icon")]
    public class Home : Activity
    {
        VMHome VM;
        ImageButton Tareas;
        ImageButton Asignaciones;
        ImageButton Salir;
        ImageButton Acerca;
        TextView Bienvenido;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            VM = new VMHome();
            ISharedPreferences prefs = GetSharedPreferences(GetString(Resource.String.preference_key_name), FileCreationMode.Private);
            string username = prefs.GetString("username", null);
            string password = prefs.GetString("password", null);
            Sesion sesion = ((CodorniXApplication)Application).GetSesion();

            if (sesion == null && username != null && password != null)
            {
                // Fast call to login
                Intent intent = new Intent(this, typeof(Login));
                intent.SetFlags(ActivityFlags.ClearTop);
                StartActivity(intent);
                Finish();
                return;
            }

            if (username == null || password == null)
            {
                CerrarSesion(null, null);
                return;
            }

            VM.ObtenerEncargado(sesion.uidUsuario);


            // Create your application here
            SetContentView(Resource.Layout.Home);

            Tareas = FindViewById<ImageButton>(Resource.Id.btnTarea);
            Tareas.Click += OpenTareas;

            Asignaciones = FindViewById<ImageButton>(Resource.Id.btnAsignaciones);
            Asignaciones.Click += OpenAsignaciones;

            Salir = FindViewById<ImageButton>(Resource.Id.btnLogout);
            Salir.Click += CerrarSesion;

            Bienvenido = FindViewById<TextView>(Resource.Id.bienvenido);
            Bienvenido.Text = "Bienvenido " + VM.Encargado.STRNOMBRE + ' ' + VM.Encargado.STRAPELLIDOPATERNO;
        }

        private void OpenTareas(object sender, EventArgs e)
        {
            StartActivity(typeof(Tareas));
        }

        private void CerrarSesion(object sender, EventArgs e)
        {
            ISharedPreferences prefs = GetSharedPreferences(GetString(Resource.String.preference_key_name), FileCreationMode.Private);
            ISharedPreferencesEditor editor = prefs.Edit();
            editor.Remove("username");
            editor.Remove("password");
            editor.Commit();
            ((CodorniXApplication)Application).SetSesion(null);
            Intent intent = new Intent(this, typeof(Login));
            intent.SetFlags(ActivityFlags.ClearTop);
            StartActivity(intent);
            Finish();
        }

        private void OpenAsignaciones(object server, EventArgs e)
        {
            StartActivity(typeof(Asignaciones));
        }
    }
}