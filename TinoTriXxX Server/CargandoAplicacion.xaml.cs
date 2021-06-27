using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TinoTriXxX.Vista
{
    /// <summary>
    /// Lógica de interacción para CargandoAplicacion.xaml
    /// </summary>
    public partial class CargandoAplicacion : Window
    {
        public CargandoAplicacion()
        {
            try
            {
                InitializeComponent();
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
    }
}
