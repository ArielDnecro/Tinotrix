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

namespace CodorniX.AndroidApp
{
    class AndroidConfigProvider : IConfigProvider
    {
        BroadcastDatabaseProvider provider = new BroadcastDatabaseProvider();

        public string GetConnectionString()
        {
            string conn = provider.GetConnectionString();
            if (conn != null)
                return conn;
            else
                return "Data Source=192.168.137.1;Initial Catalog=CodorniX;User ID=sa;Password=Sakura10";
        }
    }
}