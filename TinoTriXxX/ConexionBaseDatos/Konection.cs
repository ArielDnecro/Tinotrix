using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TinoTriXxX.ConexionBaseDatos
{
    public static class Config
    {
        //public static readonly string ConnectionString = "Data Source=.;Initial Catalog=CodorniX;User ID=sa;Password=12345678";
        //Data Source = (LocalDB)\MSSQLLocalDB;AttachDbFilename="C:\Users\Iudex\Documents\Visual Studio 2017\Projects\CodorniX\TinoTrix Escritorio\BdTinotrixLocal.mdf";Integrated Security = True
        //return " Data Source = AMON; User ID = sa; Password = 123; Initial Catalog = CodorniXBase; Connection Timeout = 0; Persist Security Info = True; Connect Timeout = 0";


        /// <summary>
        /// Cadena de conexión para <see cref="Connection"/>
        /// </summary>
        /// <value>
        /// La cadena de conexión por defecto tiene un valor fijo, sin embargo, en caso de registrar
        /// algun proveedor de configuración, este es invocado con el fin de obtener la cadena de conexión
        /// adecuada. Esto permite una reutilización de este código como una librería independiente.
        /// </value>
        public static string ConnectionString
        {
            get
            {
                string conn = ConfigProviderManager.GetConfigProviderManager().GetConnectionString();

                if (string.IsNullOrEmpty(conn))
                    return " Data Source = (LocalDB)/MSSQLLocalDB;AttachDbFilename=" + "BdTinotrix.mdf" + "; Integrated Security = True;Connect Timeout=30";

                return conn;
            }
        }

    }
    public class ConfigProviderManager
    {
        List<IConfigProvider> providers = new List<IConfigProvider>();
        private static ConfigProviderManager self;

        /// <summary>
        /// Get a shared instance of <see cref="ConfigProviderManager"/>.
        /// </summary>
        /// <returns>The shared instance</returns>
        public static ConfigProviderManager GetConfigProviderManager()
        {
            if (self == null)
                self = new ConfigProviderManager();

            return self;
        }

        /// <summary>
        /// Register a new <see cref="IConfigProvider"/>.
        /// Only accepts one object per type.
        /// </summary>
        /// <param name="obj">A instance of a IConfigProvider implementation class</param>
        public void RegisterConfigProvider(IConfigProvider obj)
        {
            Type providerType = obj.GetType();
            bool contains = providers.Where(x => x.GetType() == providerType).Count() > 0;
            if (!contains)
                providers.Add(obj);
        }

        /// <summary>
        /// Get the connection string 
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            foreach (IConfigProvider provider in providers)
            {
                string conn = provider.GetConnectionString();

                if (conn == null)
                    continue;

                return conn;
            }
            return null;
        }
    }
    public interface IConfigProvider
    {
        /// <summary>
        /// Obtiene la cadena de conexión.
        /// </summary>
        /// <returns>una cadena de texto con la cadena de configuración.</returns>
        string GetConnectionString();
    }
    public class Konection
    {

        //private SqlConnection _Connection = new SqlConnection(Config.ConnectionString);

        private SqlConnection _Connection = new SqlConnection();
        private SqlTransaction CurrentTransaction = null;
        public void Dispose()
        {
            _Connection = new SqlConnection(@"Data Source=" + TinoTriXxX.Properties.Settings.Default["Source"].ToString() + ";Initial Catalog=TinotrixCliente;Integrated Security=True");
            _Connection.Dispose();
            if (CurrentTransaction != null)
                CurrentTransaction.Dispose();
        }
        public DataTable ExecuteQuery(SqlCommand command)
        {
            string p = TinoTriXxX.Properties.Settings.Default["Source"].ToString();
            _Connection = new SqlConnection(@"Data Source=" + TinoTriXxX.Properties.Settings.Default["Source"].ToString() + ";Initial Catalog=TinotrixCliente;Integrated Security=True");
            DataTable table = new DataTable();
            //SqlDataReader reader;

            try
            {
                _Connection.Open();
                command.Connection = _Connection;
                //reader = command.ExecuteReader();
                //table.Load(reader);
                SqlDataAdapter a = new SqlDataAdapter(command);
                a.Fill(table);

            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                _Connection.Close();
            }
            return table;
        }
        public bool ExecuteCommand(SqlCommand command, bool disponse = true)
        {
            _Connection = new SqlConnection(@"Data Source=" + TinoTriXxX.Properties.Settings.Default["Source"].ToString() + ";Initial Catalog=TinotrixCliente;Integrated Security=True");
            int result = 0;
            try
            {
                if (CurrentTransaction == null)
                    _Connection.Open();

                command.Connection = _Connection;
                if (CurrentTransaction != null)
                    command.Transaction = CurrentTransaction;
                result = command.ExecuteNonQuery();
                if (disponse)
                    command.Dispose();
            }
            catch (SqlException e)
            {
                throw e;
            }
            finally
            {
                if (CurrentTransaction == null)
                    _Connection.Close();
            }

            return true;
        }
    }
}
