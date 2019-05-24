using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System;

using CodorniX.Modelo;
using CodorniX.AndroidApp.ViewModel;
using Android.Content;

namespace CodorniX.AndroidApp.Activities
{
    [Activity(Label = "Login")]
    public class Login : Activity
    {
        private VMLogin VM;

        private Button LoginB;
        private EditText Username;
        private EditText Password;
        private TextView Error;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Register custom configuration for Common library 
            ConfigProviderManager.GetConfigProviderManager().RegisterConfigProvider(new AndroidConfigProvider());
            // Initialize the ViewModel
            VM = new VMLogin();

            // Set our view from the "main" layout resource
            SetContentView (Resource.Layout.Main);

            ISharedPreferences prefs = GetSharedPreferences(GetString(Resource.String.preference_key_name), FileCreationMode.Private);
            string username = prefs.GetString("username", null);
            string password = prefs.GetString("password", null);

            bool result = false;

            if (username != null && password != null)
                result = InicioSesion(username, password);

            if (result)
            {
                Intent intent = new Intent(this, typeof(Home));
                intent.SetFlags(ActivityFlags.ClearTop);
                StartActivity(intent);
                Finish();
                return;
            }
            else if (!result || username == null || password == null)
            {

                LoginB = FindViewById<Button>(Resource.Id.login);
                Username = FindViewById<EditText>(Resource.Id.username);
                Password = FindViewById<EditText>(Resource.Id.password);
                Error = FindViewById<TextView>(Resource.Id.error);

                LoginB.Click += (sender, e) =>
                {
                    string user, pass;
                    user = Username.Text;
                    pass = Password.Text;

                    if (InicioSesion(user, pass))
                    {
                        Intent intent = new Intent(this, typeof(Home));
                        intent.SetFlags(ActivityFlags.ClearTop);
                        StartActivity(intent);
                        Finish();
                    }
                };
            }
        }
        

        private bool InicioSesion(string user, string pass)
        {
            VM.ObtenerUsuario(user);
            if (VM.Usuario == null)
            {
                Error.Text = GetString(Resource.String.errorUsername);
                return false;
            }
            else
            {
                if (VM.Usuario.STRPASSWORD != pass)
                {
                    Error.Text = GetString(Resource.String.errorPassword);
                    return false;
                }

                VM.ObtenerPerfilesSucursales(VM.Usuario.UIDUSUARIO);

                if (VM.PerfilesSucursales.Count == 0)
                {
                    Error.Text = GetString(Resource.String.errorSucursal);
                    return false;
                }

                ISharedPreferences prefs = GetSharedPreferences(GetString(Resource.String.preference_key_name), FileCreationMode.Private);
                ISharedPreferencesEditor editor = prefs.Edit();
                editor.PutString("username", user);
                editor.PutString("password", pass);
                editor.Commit();

                UsuarioPerfilSucursal ups = VM.PerfilesSucursales[0];
                Sesion sesion = new Sesion();
                sesion.uidUsuario = VM.Usuario.UIDUSUARIO;
                sesion.uidSucursalActual = ups.UidSucursal;
                sesion.uidPerfilActual = ups.UidPerfil;
                ((CodorniXApplication)Application).SetSesion(sesion);

                return true;
            }
        }
    }
}

