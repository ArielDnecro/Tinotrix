using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CodorniX.Modelo;

namespace CodorniX
{
    /// <summary>
    /// Global configuration class for CodorniX Common Library, this class specifies some configurations that can change.
    /// </summary>
    public static class Config
    {
        //public static readonly string ConnectionString = "Data Source=.;Initial Catalog=CodorniX;User ID=sa;Password=12345678";
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
                    //return "Data Source=den1.mssql7.gear.host;Initial Catalog=tinotrix;User ID=tinotrix;Password=Oe2CSy-uA1_1";
                    return "Data Source=den1.mssql7.gear.host;Initial Catalog=tinotrix2;User ID=tinotrix2;Password=Ua6n4gu2C~_1";
                //return "Data Source=den1.mssql7.gear.host;Initial Catalog=tinotrix1;User ID=tinotrix1;Password=Ua6n4gu2C~_1";
                //return " Data Source = AMON; User ID = sa; Password = 123; Initial Catalog = CodorniXBase; Connection Timeout = 0; Persist Security Info = True; Connect Timeout = 0";
                //return " Data Source =.; User ID = sa; Password = 123; Initial Catalog = tinotrix; Connection Timeout = 0; Persist Security Info = True; Connect Timeout = 0";

                return conn;
            }
        }

    }

    /// <summary>
    /// Configuration provider for <see cref="Config"/> class.
    /// </summary>
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

    /// <summary>
    /// Interfaz de proveedor de configuración
    /// </summary>
    public interface IConfigProvider
    {
        /// <summary>
        /// Obtiene la cadena de conexión.
        /// </summary>
        /// <returns>una cadena de texto con la cadena de configuración.</returns>
        string GetConnectionString();
    }
}