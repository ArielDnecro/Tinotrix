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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TinoTriXxX.VistaModelo;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para PageTicketCliente.xaml
    /// </summary>
    public partial class PageTicketCliente : Page
    {
        VM_Escritorio VM = new VM_Escritorio();
        MainWindow parentWindow;
        public MainWindow ParentWindow
        {
            get { return parentWindow; }
            set { parentWindow = value; }
        }
        public PageTicketCliente(VM_Escritorio vm)
        {
            VM = vm;
            InitializeComponent();

            try
            {
                VM.ObtenerTicketCliente();
               TxtEncUnoLinea.Text= VM.ticketcliente.StrEnc1Linea;
               TxtEncDosLinea.Text = VM.ticketcliente.StrEnc2Linea;
                TxtEncTresLinea.Text = VM.ticketcliente.StrEnc3Linea;
                TxtEncCuatroLinea.Text = VM.ticketcliente.StrEnc4Linea;
                TxtEncCincoLinea.Text = VM.ticketcliente.StrEnc5Linea;
                TxtPieUnoLinea.Text = VM.ticketcliente.StrPie1Linea;
                TxtPieDosLinea.Text = VM.ticketcliente.StrPie2Linea;
                TxtPieTresLinea.Text = VM.ticketcliente.StrPie3Linea;
            }
            catch (FileNotFoundException u)
            {
                MessageBox.Show(u.Message);

            }
            catch (DirectoryNotFoundException i)
            {
                MessageBox.Show(i.Message);

            }
            catch (IOException p)
            {
                MessageBox.Show(p.Message);

            }
            catch (Exception d)
            {
                
            }

            VM.ObtenerSession();
                if (VM.Session.UidUsusario == Guid.Empty)
                {
                    //  MessageBox.Show("¡Inicia sesion primero!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                    TxtEncUnoLinea.IsEnabled = false;
                    TxtEncDosLinea.IsEnabled = false;
                    TxtEncTresLinea.IsEnabled = false;
                    TxtEncCuatroLinea.IsEnabled = false;
                    TxtEncCincoLinea.IsEnabled = false;
                    TxtPieUnoLinea.IsEnabled = false;
                    TxtPieDosLinea.IsEnabled = false;
                    TxtPieTresLinea.IsEnabled = false;
                }
                else
                {
                    TxtEncUnoLinea.IsEnabled = true;
                    TxtEncDosLinea.IsEnabled = true;
                    TxtEncTresLinea.IsEnabled = true;
                    TxtEncCuatroLinea.IsEnabled = true;
                    TxtEncCincoLinea.IsEnabled = true;
                    TxtPieUnoLinea.IsEnabled = true;
                    TxtPieDosLinea.IsEnabled = true;
                    TxtPieTresLinea.IsEnabled = true;
                }
            
        }

        private void BtnGuardarConfTicketCliente_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               
                VM.ActualizarEncPieTicketCliente(TxtEncUnoLinea.Text, TxtEncDosLinea.Text, 
                    TxtEncTresLinea.Text, TxtEncCuatroLinea.Text, TxtEncCincoLinea.Text,
                    TxtPieUnoLinea.Text, TxtPieDosLinea.Text, TxtPieTresLinea.Text);
               
            }
            catch (FileNotFoundException u)
            {
                MessageBox.Show(u.Message);

            }
            catch (DirectoryNotFoundException i)
            {
                MessageBox.Show(i.Message);

            }
            catch (IOException p)
            {
                MessageBox.Show(p.Message);

            }
            catch (Exception d)
            {
                MessageBox.Show("¡No se pudo guardar los cambios \n intente de nuevo! \n \n" + d, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                return;
            }

            //else
            //{
            //    MessageBox.Show("¡No valida!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //    return;
            //}
            MessageBox.Show("Se guardo correctamente los cambios", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            //PagePrincipal page = new PagePrincipal();
            //this.NavigationService.Navigate(page);
            parentWindow.MenuHome();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            parentWindow.txtEnc1Linea = TxtEncUnoLinea;
            parentWindow.txtEnc2Linea = TxtEncDosLinea;
            parentWindow.txtEnc3Linea = TxtEncTresLinea;
            parentWindow.txtEnc4Linea = TxtEncCuatroLinea;
            parentWindow.txtEnc5Linea = TxtEncCincoLinea;
            parentWindow.txtPie1Linea = TxtPieUnoLinea;
            parentWindow.txtPie2Linea = TxtPieDosLinea;
            parentWindow.txtPie3Linea = TxtPieTresLinea;
        }
    }
}
