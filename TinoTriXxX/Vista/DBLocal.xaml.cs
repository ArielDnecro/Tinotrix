using CodorniX.Modelo;
using Microsoft.Win32;
using System;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Windows;

namespace TinoTriXxX.Vista
{
    /// <summary>
    /// Lógica de interacción para DBLocal.xaml
    /// </summary>
    public partial class DBLocal : Window
    {
        int _Reintentos;

        public DBLocal()
        {
            try
            {
                InitializeComponent();
                _Reintentos = 0;

                CargarElementos();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void CargarElementos()
        {
            if (!ValidarEstatusServicio("SQLBROWSER").Equals("Running"))
            {
                ServiceController _serviceSQLBROWSER = new ServiceController("SQLBROWSER");
                ServiceHelper.ChangeStartMode(_serviceSQLBROWSER, System.ServiceProcess.ServiceStartMode.Automatic);
                Start(_serviceSQLBROWSER);
            }
            else if (ValidarEstatusServicio("SQLBROWSER").Equals("Running"))
            {
                string[] instancias = GetSQLServerList();

                for (int i = 0; i < instancias.Count(); i++)
                    cbInstancia.Items.Add(instancias[i].ToString());
            }

            if (cbInstancia.Items.Count == 0 && _Reintentos != 2)
            {
                _Reintentos++;
                CargarElementos();
            }

            if (cbInstancia.Items.Count > 0)
                cbInstancia.SelectedIndex = 0;
            else MessageBox.Show("No se dectaron instancias", "Instancias", MessageBoxButton.OK, MessageBoxImage.Information);

        }

        private static string[] GetSQLServerList()
        {
            SqlDataSourceEnumerator dse = SqlDataSourceEnumerator.Instance;
            DataTable dt = dse.GetDataSources();

            if (dt.Rows.Count == 0)
                return null;

            string[] SQLServers = new string[dt.Rows.Count];
            int f = -1;

            foreach (DataRow r in dt.Rows)
            {
                string SQLServer = r["ServerName"].ToString();
                string Instance = r["InstanceName"].ToString();
                if (Instance != null && !string.IsNullOrEmpty(Instance))
                {
                    SQLServer += "\\" + Instance;
                }
                SQLServers[System.Math.Max(System.Threading.Interlocked.Increment(ref f), f - 1)] = SQLServer;
            }

            Array.Sort(SQLServers);

            return SQLServers;
        }

        private string ValidarEstatusServicio(string NombreServicio)
        {
            ServiceController sc = new ServiceController(NombreServicio);

            switch (sc.Status)
            {
                case ServiceControllerStatus.Running:
                    return "Running";
                case ServiceControllerStatus.Stopped:
                    return "Stopped";
                case ServiceControllerStatus.Paused:
                    return "Paused";
                case ServiceControllerStatus.StopPending:
                    return "Stopping";
                case ServiceControllerStatus.StartPending:
                    return "Starting";
                default:
                    return "Status Changing";
            }
        }

        static void Start(ServiceController _service)
        {
            if (!(_service.Status == ServiceControllerStatus.Running || _service.Status == ServiceControllerStatus.StartPending))
                _service.Start();

            _service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 1, 0));
        }

        public static void StartService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;

                int millisec2 = Environment.TickCount;
                timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds - (millisec1));
                service.Start();
                service.WaitForStatus(ServiceControllerStatus.Running, timeout);

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public static void StopService(string serviceName, int timeoutMilliseconds)
        {
            ServiceController service = new ServiceController(serviceName);
            try
            {
                int millisec1 = 0;
                TimeSpan timeout;
                if (service.Status == ServiceControllerStatus.Running)
                {
                    millisec1 = Environment.TickCount;
                    timeout = TimeSpan.FromMilliseconds(timeoutMilliseconds);
                    service.Stop();
                    service.WaitForStatus(ServiceControllerStatus.Stopped, timeout);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        private void MontarBD_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    this.IsEnabled = false;

                    string _SQLConnectionString = @"Data Source = " + cbInstancia.Text + "; Initial Catalog = master; Integrated Security = True";
                    string path = @"BaseDeDatos\script.sql";
                    string script = string.Empty;

                    for (int i = 0; i < System.IO.Path.GetFullPath(path).Split(System.IO.Path.DirectorySeparatorChar).Length; i++)
                    {
                        try { script = File.ReadAllText(path); break; }
                        catch (Exception) { path = @"..\" + path; }
                    }

                    SqlConnection conn = new SqlConnection(_SQLConnectionString);
                    ExecuteBatchNonQuery(script, conn);

                    TinoTriXxX.Properties.Settings.Default["Source"] = cbInstancia.Text;
                    TinoTriXxX.Properties.Settings.Default.Save();

                    Microsoft.Win32.RegistryKey key;
                    key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(@"Tinotrix");
                    key.SetValue("Source", cbInstancia.Text);
                    key.Close();

                    StopService("SQLBROWSER", 1000);

                }
                finally
                {
                    this.IsEnabled = true;

                    MessageBox.Show("La base de datos fue creada y se ha configurado el equipo correctamente", "Configuración exitosa", MessageBoxButton.OK, MessageBoxImage.Information);

                    this.Close();
                }
            }
            catch (Exception a)
            {
                string cadena = "Error: " + a.Message;
                MessageBox.Show(cadena, "La base de datos no fue montada", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ExecuteBatchNonQuery(string sql, SqlConnection conn)
        {
            string sqlBatch = string.Empty;
            SqlCommand cmd = new SqlCommand(string.Empty, conn);
            conn.Open();
            sql += "\nGO";
            try
            {
                foreach (string line in sql.Split(new string[2] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    if (line.ToUpperInvariant().Trim() == "GO")
                    {
                        cmd.CommandText = sqlBatch;
                        cmd.ExecuteNonQuery();
                        sqlBatch = string.Empty;
                    }
                    else
                    {
                        sqlBatch += line + "\n";
                    }
                }
            }
            catch (Exception) { }
            finally
            {
                conn.Close();
            }
        }

        private void btnPruebaConexion_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                try
                {
                    this.IsEnabled = false;

                    string source = cbInstancia.Text;

                    SqlConnection _cadena = new SqlConnection(@"Data Source=" + source + ";Initial Catalog=master;Integrated Security=True;Connection Timeout=2");
                    _cadena.Open();
                    MessageBox.Show("Se estableció una conexión exitosa", "Conexión exitosa", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception r)
                {
                    MessageBox.Show("No se pudo establecer conexión" + r.Message, "Error de conexión", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            finally
            {
                this.IsEnabled = true;
            }
        }

    }
}
