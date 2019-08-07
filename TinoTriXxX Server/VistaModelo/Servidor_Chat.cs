﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;
using System.Net.Sockets;
using System.IO;
using System.Net;
using System.Windows;
using TinoTriXxX;

namespace Servidor
{
    class Servidor_Chat
    {
        /*        
            TcpListener--------> Espera la conexion del Cliente.        
            TcpClient----------> Proporciona la Conexion entre el Servidor y el Cliente.        
            NetworkStream------> Se encarga de enviar mensajes atravez de los sockets.        
        */

        private TcpListener server;
        private TcpClient client = new TcpClient();
        private IPEndPoint ipendpoint = new IPEndPoint(IPAddress.Any, 8000);
        private List<Connection> list = new List<Connection>();

        Connection con;


        private struct Connection
        {
            public NetworkStream stream;
            public StreamWriter streamw;
            public StreamReader streamr;
            public string nick;
        }

        public Servidor_Chat()
        {
            Inicio();
        }

        public void Inicio()
        {

           // Console.WriteLine("Servidor OK!");
            server = new TcpListener(ipendpoint);
            server.Start();

            while (true)
            {
                //client = server.AcceptTcpClient();

                //con = new Connection();
                //con.stream = client.GetStream();
                //con.streamr = new StreamReader(con.stream);
                //con.streamw = new StreamWriter(con.stream);

                //con.nick = con.streamr.ReadLine();

                //list.Add(con);
                ////Console.WriteLine(con.nick + " se a conectado.");
                
                //foreach (Window window in Application.Current.Windows)
                //{
                //    if (window.GetType() == typeof(MainWindow))
                //    {
                //        (window as MainWindow).lbFotos.Text = con.nick + " se a conectado.";
                //    }
                //}

                Thread t = new Thread(Escuchar_conexion);

                t.Start();
            }


        }

        void Escuchar_conexion()
        {
            Connection hcon = con;

            do
            {
                try
                {
                    string tmp = hcon.streamr.ReadLine();
                    //Console.WriteLine(hcon.nick + ": " + tmp);
                    
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).lbVenta.Text = hcon.nick + ": " + tmp;
                        }
                    }
                    foreach (Connection c in list)
                    {
                        try
                        {
                            c.streamw.WriteLine(hcon.nick + ": " + tmp);
                            c.streamw.Flush();
                        }
                        catch
                        {
                        }
                    }
                }
                catch
                {
                    list.Remove(hcon);
                    //Console.WriteLine(con.nick + " se a desconectado.");
                    foreach (Window window in Application.Current.Windows)
                    {
                        if (window.GetType() == typeof(MainWindow))
                        {
                            (window as MainWindow).lbFotos.Text = con.nick + " se a desconectado.";
                        }
                    }
                    break;
                }
            } while (true);
        }

    }
}
