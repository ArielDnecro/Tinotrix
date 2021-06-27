using System;
using System.Collections.Generic;
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
    /// Lógica de interacción para Acerca_de_Tinotrix.xaml
    /// </summary>
    public partial class Acerca_de_Tinotrix : Window
    {
        public Acerca_de_Tinotrix()
        {
            InitializeComponent();
        }

        private void BtnAceptarAcercade_Click(object sender, RoutedEventArgs e)
        {
            cerrar();
        }

        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            cerrar();
        }
        void cerrar() {
            this.Close();
        }
    }
}
