using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
    /// Lógica de interacción para PageImpresoras.xaml
    /// </summary>
    public partial class PageImpresoras : Page
    {
        VM_Escritorio VM = new VM_Escritorio();
        public PageImpresoras()
        {
            InitializeComponent();
            cargarImpresoras();
        }

        private void BtnActulizarDispositivos_Click(object sender, RoutedEventArgs e)
        {
            cargarImpresoras();
        }

        private void CbDispositivos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            VM.ActualizarImpresora(CbDispositivos.SelectedValue.ToString());
        }

        private void cargarImpresoras()
        {
            CbDispositivos.Items.Clear();
            VM.ObtenerImpresora();
            if (PrinterSettings.InstalledPrinters.Count>=1) {
                foreach (String strPrinter in PrinterSettings.InstalledPrinters)
                {

                    CbDispositivos.Items.Add(strPrinter);

                }
                if (!string.IsNullOrEmpty(VM.StrDesImpresora))
                {
                    try {
                        CbDispositivos.Text = VM.StrDesImpresora;
                    } catch (Exception r) {
                        CbDispositivos.Text = CbDispositivos.Items[0].ToString();
                    }
                }
                else
                {
                    CbDispositivos.Text = CbDispositivos.Items[0].ToString();
                }
            } else {

                MessageBox.Show("¡NO hay impresoras!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            //if (vmBoletoConfiguracion.LsBoletoConfigurar.Count != 0)
            //{
            //    foreach (var item in vmBoletoConfiguracion.LsBoletoConfigurar)
            //    {
            //        if (item.VchTipoCamara == "Impresora")
            //        {
            //            UidImpresora = item.UidBoletoConfiguracion.ToString();
            //            cbxImpresoras.Text = item.VchNombreImpresora;
            //        }
            //    }
            //}
            //else
            //{
            //    CbDispositivos.Text = CbDispositivos.Items[0].ToString();
            //}

        }

    }
}
