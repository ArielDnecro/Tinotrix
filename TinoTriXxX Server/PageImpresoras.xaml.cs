using System;
using System.Collections.Generic;
using System.Drawing.Printing;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TinoTriXxX.VistaModelo;
using System.Management;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para PageImpresoras.xaml
    /// </summary>
    public partial class PageImpresoras : Page
    {
        VM_Escritorio VM = new VM_Escritorio();
        MainWindow parentWindow;
        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
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
           
        }

         void cargarImpresoras()
        {
            try
            {
                //CbDispositivos.Items.Clear();
                VM.ObtenerImpresora();
                if (PrinterSettings.InstalledPrinters.Count >= 1)
                {
                    //foreach (String strPrinter in PrinterSettings.InstalledPrinters)
                    //{

                    //    CbDispositivos.Items.Add(strPrinter);

                    //}
                    CbDispositivos.ItemsSource = PrinterSettings.InstalledPrinters;

                    if (!string.IsNullOrEmpty(VM.StrDesImpresora))
                    {
                        try
                        {
                            CbDispositivos.Text = VM.StrDesImpresora;
                            //foreach (ComboBoxItem item in CbDispositivos.Items)
                            //    if (item.Content.ToString() == VM.StrDesImpresora)
                            //    {
                            //        CbDispositivos.SelectedItem = item;
                            //        //break;
                            //    }
                        }
                        catch (Exception r)
                        {
                            CbDispositivos.Text = CbDispositivos.Items[0].ToString();
                        }
                    }
                    else
                    {
                        CbDispositivos.Text = CbDispositivos.Items[0].ToString();
                    }
                }
                else
                {
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

        private void BtnConfirmarCambios_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                VM.ActualizarImpresora(CbDispositivos.SelectedValue.ToString());
            }
            catch (FileNotFoundException f)
            {
                MessageBox.Show(f.Message);

            }
            catch (DirectoryNotFoundException f)
            {
                MessageBox.Show(f.Message);

            }
            catch (IOException f)
            {
                MessageBox.Show(f.Message);

            }
            catch (Exception f)
            {
                MessageBox.Show(f.Message);

            }
            parentWindow.MenuHome();
        }
    }
}
