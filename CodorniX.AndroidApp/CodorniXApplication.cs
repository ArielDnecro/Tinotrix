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

namespace CodorniX.AndroidApp
{
    [Application(Label = "CodorniX", Theme = "@android:style/Theme.DeviceDefault.Light")]
    class CodorniXApplication : Application
    {
        private Sesion sesion;

        public CodorniXApplication(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
        }

        public override void OnCreate()
        {
            base.OnCreate();
            //app init ...
        }

        public Sesion GetSesion()
        {
            return sesion;
        }

        public void SetSesion(Sesion sesion)
        {
            this.sesion = sesion;
        }
    }
}