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
using System.Net.Sockets;
using System.Net;

namespace CodorniX.AndroidApp
{
    /// <summary>
    /// Broadcast configuration provider for local testing
    /// </summary>
    class BroadcastDatabaseProvider
    {
        /// <summary>
        /// Socket client
        /// </summary>
        UdpClient client;

        /// <summary>
        /// Cached connection string
        /// </summary>
        string connectionString;

        public BroadcastDatabaseProvider()
        {
            client = new UdpClient();
            client.EnableBroadcast = true;
            client.Client.ReceiveTimeout = 20;
        }

        public string GetConnectionString()
        {
            // Return the cached connection if it exists
            if (connectionString != null)
                return connectionString;

            // Connect to broadcast address to port 12000
            IPEndPoint dest = new IPEndPoint(IPAddress.Broadcast, 12000);
            IPEndPoint endpoint = new IPEndPoint(IPAddress.Any, 0);
            try
            {
                client.Send(Encoding.UTF8.GetBytes("HELLO"), 5, dest);
                byte[] result = client.Receive(ref endpoint);
                string response = Encoding.UTF8.GetString(result);
                if (response.StartsWith("OK:"))
                {
                    // Split message OK:USER:PASS:DB to get the connection string.
                    string[] info = response.Split(':');
                    connectionString = string.Format("Data Source={0};Initial Catalog={3};User ID={1};Password={2}", endpoint.Address.ToString(), info[1], info[2], info[3]);
                }
            }
            catch (Exception)
            {
                // pass
            }
            return connectionString;
        }
    }
}