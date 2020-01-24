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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TinoTriXxX.VistaModelo;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para PagePrincipal.xaml
    /// </summary>
    public partial class PagePrincipal : Page
    {
        VM_Escritorio VM = new VM_Escritorio();
        public PagePrincipal(VM_Escritorio vm)
        {
            VM = vm;
            InitializeComponent();
        }

        private void BtnSeccionVentas_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (VM.Turno.UidFolio == Guid.Empty || VM.Turno.UidFolio == null)
                {
                    MessageBox.Show("¡No haz iniciado Turno! \n ", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                }
                else
                {
                    SeccionVentas ventas = new SeccionVentas(VM);
                    ventas.ShowDialog();
                }
            }
            catch (Exception t)
            {
                MessageBox.Show("¡Error al mostrar las ventas! \n  " + t, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }
    }
}
