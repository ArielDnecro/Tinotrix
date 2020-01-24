using System;
using System.Collections.Generic;
using System.Globalization;
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
using TestLib;
using TinoTriXxX.Modelo;
using TinoTriXxX.VistaModelo;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para SeccionVentas.xaml
    /// </summary>
    public partial class SeccionVentas : Window
    {
        VM_Escritorio VM = new VM_Escritorio();
     List< HistoricoImpresionesXTurno> ImpresionesXImprimir = null;
        double total = 0;
        double totalDescuento = 0;
        public SeccionVentas(VM_Escritorio vm)
        {
            VM = vm;
            InitializeComponent();
            cargarHVentas();
            ImpresionesXImprimir = new List<HistoricoImpresionesXTurno>();
            if (ImpresionesXImprimir.Count == 0)
            {
                btnImpresionVenta.IsEnabled = false;
                btnImpresionDescuento.IsEnabled = false;
            }
            else
            {
                btnImpresionVenta.IsEnabled = true;
                btnImpresionDescuento.IsEnabled = true;
            }
            //VariablesGlobal.FontName = "arial";
            //VariablesGlobal.FontSize = 12;
            //VariablesGlobal.FontSizeItemIndividual = 9;
            //VariablesGlobal.FontSizePosicionLlave = 12F;
            //VariablesGlobal.FontSizeCierreTurno = 9;
            //VariablesGlobal.WidthImagen = 270;
            //VariablesGlobal.HeightImagen = 50;
            //VariablesGlobal.CentrarGoParkiX = "          ";
        }
        
        private void cargarHVentas()
        {
            try
            {

                VM.ObtenerListaHistoricoVentasXTurno(VM.Turno.UidFolio);
                
                LbHVentas.ItemsSource = VM.HVentas;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show(e.Message);
                //Application.Current.Shutdown();

            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show(e.Message);
                // Application.Current.Shutdown();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
                // Application.Current.Shutdown();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                //Application.Current.Shutdown();
            }
        }
        private void BtnMinimizarVentana_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.WindowState = WindowState.Minimized;
            }
            catch (FileNotFoundException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (DirectoryNotFoundException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (IOException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);

            }
        }

        private void BtnCerrarVentana_Click(object sender, RoutedEventArgs e)
        {
            try
            {
               this.Close();
            }
            catch (FileNotFoundException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (DirectoryNotFoundException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (IOException t)
            {
                MessageBox.Show(t.Message);

            }
            catch (Exception t)
            {
                MessageBox.Show(t.Message);

            }
        }

        private void BtnImpresionVenta_Click(object sender, RoutedEventArgs e)
        {
            Impresion(1);
        }
        private void BtnImpresionDescuento_Click(object sender, RoutedEventArgs e)
        {
            Impresion(2);
        }
        void Impresion(int TList)
        {
            try
            {
                VM.ObtenerTicketCliente();
                VM.ObtenerImpresora();
                if (!string.IsNullOrEmpty(VM.StrDesImpresora))
                {
                   
                      var groupedList = ImpresionesXImprimir.GroupBy(u => u.StrFotoDesc)
                                        .Select(grp => new { Cant = grp.Count(), FotoDesc = grp.Key, Costo = grp.Sum(x => double.Parse(x.StrCosto)), ImpresionesXImprimir = grp.ToList() })
                                        .ToList();
                    if(TList==2)
                    {
                          groupedList = ImpresionesXImprimir.GroupBy(u => u.StrFotoDesc)
                                        .Select(grp => new { Cant = grp.Count(), FotoDesc = grp.Key, Costo = grp.Sum(x => double.Parse(x.StrCostoTicket)), ImpresionesXImprimir = grp.ToList() })
                                        .ToList();
                    }
                     

                    Ticket2 CorteCaja = new Ticket2();

                    CorteCaja.FontName = VariablesGlobal.FontName;
                    CorteCaja.FontSize = VariablesGlobal.FontSizeCierreTurno;

                    CorteCaja.AddHeaderLine(VM.ticketcliente.StrEnc1Linea);
                    CorteCaja.AddHeaderLine(VM.ticketcliente.StrEnc2Linea);
                    CorteCaja.AddHeaderLine(VM.ticketcliente.StrEnc3Linea);
                    CorteCaja.AddHeaderLine(VM.ticketcliente.StrEnc4Linea);
                    CorteCaja.AddHeaderLine(VM.ticketcliente.StrEnc5Linea);
                    CorteCaja.AddHeaderLine(" ");
                    CorteCaja.AddHeaderLine(" ENCARGADO:" + VM.Encargado.STRNOMBRE);
                    CorteCaja.AddHeaderLine("FECHA/HORA:" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    CorteCaja.AddHeaderLine("===================================");
                    if (ImpresionesXImprimir.Count == 0)
                    {
                        CorteCaja.AddItem("-----------", "        0" + "      $" + "0", "");
                    }
                    else
                    {
                        //CorteCaja.Cabecera("FOLIO  DESC.", "FECHA/HORA" + "  IMPORTE", "");
                        //foreach (var fot in ImpresionesXImprimir)
                        //{
                        //    DateTime date = DateTime.Parse(fot.StrFechaHora);
                        //    CorteCaja.AddItem("[" + fot.IntFolio.ToString("D4") + "] [" + fot.StrFotoDesc.Substring(0, 5) + ".] " + date.ToString("dd/MM/yyyy HH:mm") + " $" + fot.StrCosto, "", "");

                        //}
                        //CorteCaja.AddItem("Infantil", "        " + "12", "$" + "103");
                        if (TList == 1)
                        {
                            CorteCaja.Cabecera("CANT. DESCRIPCION", "", "IMPORTE");
                        }
                            if (TList == 2)
                        {
                            CorteCaja.Cabecera("CANT. DESCRIPCION", "", "DESCUENTO");
                        }
                            
                        foreach (var fot in groupedList)
                        {
                            //  CorteCaja.AddItem("[" + fot.Cant.ToString("D4")+ "]   [" + fot.FotoDesc.Substring(0, 7) + ".] " , "                $" + fot.Costo, "");
                            var resultString = fot.FotoDesc.PadRight(22);
                            CorteCaja.AddItem(fot.Cant.ToString("D3").Replace("0", " ") + "   " + resultString, "", "$" + fot.Costo);
                        }
                    }
                    CorteCaja.AddItem("===================================", "", "");
                    if (total == 0)
                    {
                        CorteCaja.AddItem("TOTAL", "        " + "      $" + "0", "");

                    }
                    else
                    {
                        if (TList == 1)
                        {
                            CorteCaja.AddItem("", "        TOTAL:", "$" + total.ToString());
                        }
                        if (TList == 2)
                        {
                            CorteCaja.AddItem("", "        TOTAL:", "$" + totalDescuento.ToString());
                        }
                        
                    }
                    CorteCaja.AddItem("===================================", "", "");
                    CorteCaja.AddItem(VM.ticketcliente.StrPie1Linea, "", "");
                    CorteCaja.AddItem(VM.ticketcliente.StrPie2Linea, "", "");
                    CorteCaja.AddItem(VM.ticketcliente.StrPie3Linea, "", "");
                    CorteCaja.PrintTicket(VM.StrDesImpresora);
                    // CorteCaja.PrintTicket("Microsoft Print to PDF");
                }
                else
                {
                    MessageBox.Show("¡No hay impresora disponible o seleccionada! \n configure 'impresoras'", "Configuracion Tinotrix");
                }
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
        }
        private void ChSumaVENTAaTICKET_Checked(object sender, RoutedEventArgs e)
        {
            try
            {
                //var cb = (CheckBox)sender;
                //var parentDataGrid = (CheckBox)cb.Tag;
                //HistoricoVentasXTurno ImpresionSeleccionada = (HistoricoVentasXTurno)parentDataGrid.DataContext;
                //ImpresionesXImprimir.Add(ImpresionSeleccionada);
                object border = ((FrameworkElement)sender).Parent;
                Grid grid = (Grid)((FrameworkElement)border).Parent;

                HistoricoImpresionesXTurno ImpresionSeleccionada = (HistoricoImpresionesXTurno)grid.DataContext;
                ImpresionesXImprimir.Add(ImpresionSeleccionada);

                NumberFormatInfo ni = new NumberFormatInfo();
                ni.NumberDecimalSeparator = ".";
                 total = double.Parse(tbTotalPagar.Text,ni)+ double.Parse(ImpresionSeleccionada.StrCosto, ni);
                tbTotalPagar.Text = total.ToString();

                totalDescuento = double.Parse(tbTotalDescuento.Text, ni) + double.Parse(ImpresionSeleccionada.StrCostoTicket, ni);
                tbTotalDescuento.Text = totalDescuento.ToString();

                if (ImpresionesXImprimir.Count ==0)
                {
                    btnImpresionVenta.IsEnabled = false;
                    btnImpresionDescuento.IsEnabled = false;
                }
                else
                {
                    btnImpresionVenta.IsEnabled = true;
                    btnImpresionDescuento.IsEnabled = true;
                }
            }
            catch (Exception d) { MessageBox.Show(d.Message); }
        }

        private void ChSumaVENTAaTICKET_Unchecked(object sender, RoutedEventArgs e)
        {
            try
            {
                object border = ((FrameworkElement)sender).Parent;
            Grid grid = (Grid)((FrameworkElement)border).Parent;
            HistoricoImpresionesXTurno ImpresionSeleccionada = (HistoricoImpresionesXTurno)grid.DataContext;
            ImpresionesXImprimir.RemoveAll(x => x.IntFolio == ImpresionSeleccionada.IntFolio);

                NumberFormatInfo ni = new NumberFormatInfo();
                ni.NumberDecimalSeparator = ".";
                 total = double.Parse(tbTotalPagar.Text, ni) - double.Parse(ImpresionSeleccionada.StrCosto, ni);
                tbTotalPagar.Text = total.ToString();

                totalDescuento = double.Parse(tbTotalDescuento.Text, ni) - double.Parse(ImpresionSeleccionada.StrCostoTicket, ni);
                tbTotalDescuento.Text = totalDescuento.ToString();

                if (ImpresionesXImprimir.Count ==0)
                {
                    btnImpresionVenta.IsEnabled = false;
                    btnImpresionDescuento.IsEnabled = false;
                }
                else
                {
                    btnImpresionVenta.IsEnabled = true;
                    btnImpresionDescuento.IsEnabled = true;
                }
            }
            catch (Exception d) { MessageBox.Show(d.Message); }
        }

       
        //private string GetParents(Object element, int parentLevel)
        //{
        //    string returnValue = String.Format("[{0}] {1}", parentLevel, element.GetType());
        //    if (element is FrameworkElement)
        //    {
        //        if (((FrameworkElement)element).Parent != null)
        //            returnValue += String.Format("{0}{1}",
        //                Environment.NewLine, GetParents(((FrameworkElement)element).Parent, parentLevel + 1));
        //    }
        //    return returnValue;
        //}
    }
}
