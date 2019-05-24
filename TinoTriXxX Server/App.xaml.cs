using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows; 

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        // public static System.Windows CurrentMain { get { return Current.MainWindow; } }
        public static Window CurrentMainWindow
        {
            get { return Current.MainWindow; }
        }
    }
}
