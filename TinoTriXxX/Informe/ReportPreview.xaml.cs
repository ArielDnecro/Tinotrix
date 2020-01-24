using Microsoft.AspNet.SignalR.Client;
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
using System.Windows.Shapes;
using TinoTriXxX.Modelo;
using TinoTriXxX.Util;
using TinoTriXxX.VistaModelo;

namespace TinoTriXxX.Informe
{
    /// <summary>
    /// Lógica de interacción para ReportPreview.xaml
    /// </summary>
    public partial class ReportPreview : Window
    {
        protected RdlcGenerator _rdlcGenerator;

        string currentPath = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        string emptyImage;

        private int columns;
        private int rows;

        private double contentWidth;
        private double contentHeight;
        VM_Escritorio VM;
        Foto foto = null;
        public ReportPreview(VM_Escritorio vm, string imgUrl, Foto photo )
        {
            try
            {
                Papel paper = new Papel();
                paper = vm.Papel;
               // MessageBox.Show("REPORTE", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                InitializeComponent();
                VM = vm;
                foto = photo;
                this.columns = int.Parse(photo.VchColumna);
                this.rows = int.Parse(photo.VchFila);

                if (this.columns > 10)
                {
                    MessageBox.Show("El numero de columnas execede el permitido", "Error de impresion");
                    return;
                }

                if (this.columns == 0)
                {
                    MessageBox.Show("El numero de columnas es invalido, se detecto 0.", "Error de impresion");
                    return;
                }

                // new instance
                this._rdlcGenerator = new RdlcGenerator();

                // set page size
                //this._rdlcGenerator.PageWidth = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrAncho));
                //this._rdlcGenerator.PageHeight = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrAlto));
                //System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                //System.Drawing.Printing.PaperSize size = new PaperSize();
                //size.RawKind = (int)VerticalContentAlignment;
                ////size.RawKind += (int)PaperKind.Custom;
                //pg.PaperSize = size;
                //this.rvReportPreview.SetPageSettings(pg);
                

                this._rdlcGenerator.PageWidth = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrAncho));
                this._rdlcGenerator.PageHeight = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrAlto));

                // set page margin
                this._rdlcGenerator.LeftMarginPage = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrMIzquierdo));
                this._rdlcGenerator.TopMarginPage = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrMSuperior));
                this._rdlcGenerator.RightMarginPage = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrMDerecho));
                this._rdlcGenerator.BottomMarginPage = RdlcComponentGenerator.MillimitersToCentimeters(double.Parse(paper.StrMInferior));

                // calculate content size
                this.contentWidth = Math.Round(this._rdlcGenerator.PageWidth - (this._rdlcGenerator.LeftMarginPage + this._rdlcGenerator.RightMarginPage), 1);
                this.contentHeight = Math.Round(this._rdlcGenerator.PageHeight - (this._rdlcGenerator.TopMarginPage + this._rdlcGenerator.BottomMarginPage), 1);

                
                this._rdlcGenerator.rdlcBodyWidth = this.contentWidth  - 0.05;
                this._rdlcGenerator.rdlcBodyHeight = this.contentHeight - 0.05;

                string contentRows = this.generatePictureColumns(photo, imgUrl);

                this._rdlcGenerator.AddContentToBody(contentRows);
                //this.rvReportPreview.VerticalScroll = 1;

                //System.Drawing.Printing.PageSettings ps = new System.Drawing.Printing.PageSettings();
                //ps.Landscape = false;
                //this.rvReportPreview.SetPageSettings(ps);
                //this.rvReportPreview.Padding = new System.Windows.Forms.Padding() { All = -12 };

                this.rvReportPreview.LocalReport.LoadReportDefinition(this._rdlcGenerator.GenerateReport());
                this.rvReportPreview.LocalReport.EnableExternalImages = true;
                this.rvReportPreview.RefreshReport();

                // MessageBox.Show("fIN REPORTE", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                if (VM.Connection == null || VM.Connection.State == ConnectionState.Disconnected)
                {
                    VM.ConnectAsync();
                }
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
        private string generatePictureColumns(Foto photo, string imagePath)
        {
            double photoSpacingLeft = (this.columns == 1 ? 0 : (this._rdlcGenerator.rdlcBodyWidth - (photo.IntAncho * this.columns)) / (int.Parse(photo.VchColumna) - 1));
            double photoSpacingTop = (this.rows == 1 ? 0 : (this._rdlcGenerator.rdlcBodyHeight - (photo.IntAlto * this.rows)) / (int.Parse(photo.VchFila) - 1));
            string columns = string.Empty;

            double left = 0;
            double top = 0;
            double width = 0;
            double height = 0;

            int currentColumnPosition = 0;
            int currentRowPosition = 0;
            int totalPhotos = int.Parse(photo.VchColumna) * int.Parse(photo.VchFila);

            for (int i = 0; i < totalPhotos; i++)
            {
                width = photo.StrMedida.Equals("Centimetro") ? photo.IntAncho : RdlcComponentGenerator.InchToCm(photo.IntAncho);
                height = photo.StrMedida.Equals("Centimetro") ? photo.IntAlto : RdlcComponentGenerator.InchToCm(photo.IntAlto);

                columns += RdlcComponentGenerator.GenerateImage(
                    name: "Image" + i,
                    value: "file:\\" + imagePath,
                    width: width,
                    height: height,
                    leftPadding: 0,
                    topPadding: 0,
                    rightPadding: 0,
                    bottomPadding: 0,
                    left: left,
                    top: top);

                left = (width + photoSpacingLeft) * (currentColumnPosition + 1);

                if (currentColumnPosition == (this.columns - 1))
                {
                    top = (height + photoSpacingTop) * (currentRowPosition + 1);
                    currentColumnPosition = -1;
                    currentRowPosition++;
                    left = 0;
                }

                currentColumnPosition++;
            }
            return columns;
        }
        private string generatePictureRows(Foto photo)
        {
            string rows = "";
            return rows;
        }
        private void RvReportPreview_Print(object sender, Microsoft.Reporting.WinForms.ReportPrintEventArgs e)
        {

        }
        private void RvReportPreview_PrintingBegin(object sender, Microsoft.Reporting.WinForms.ReportPrintEventArgs e)
        {

           // MessageBox.Show("test", "Error de impresion");
            DateTime saveNow = DateTime.Now;
            DateTime myDt = DateTime.SpecifyKind(saveNow, DateTimeKind.Utc);
            int NumFotos = int.Parse(foto.VchColumna) * int.Parse(foto.VchFila);
            //VM.NuevaImpresion(VM.Sucursal.UidSucursal, foto.UidFoto, myDt.ToString("dd/MM/yyyy HH:mm:ss"),
            //NumFotos, int.Parse(foto.StrPrecio) * NumFotos);

           
            try
            {

                VM.NuevaImpresion(VM.Sucursal.UidSucursal,
                   foto.UidFoto, myDt.ToString("dd/MM/yyyy HH:mm:ss"),
                   int.Parse(e.PrinterSettings.Copies.ToString()),
                   int.Parse(e.PrinterSettings.Copies.ToString()) * NumFotos,
                   (double.Parse(e.PrinterSettings.Copies.ToString()) * double.Parse(foto.StrPrecio)).ToString(),
                   (double.Parse(e.PrinterSettings.Copies.ToString()) * double.Parse(foto.StrPrecioTicket)).ToString(),
                   VM.Licencia.IntNo);

            } catch (Exception b) {
                MessageBox.Show("NO se puedo guardar impresion \n\n detalle del error: \n"+b, "Tinotrix Error de registro de impresion");
            } 
            if (VM.Connection == null || VM.Connection.State == ConnectionState.Disconnected)
            {
                try { VM.ConnectAsync(); } catch (Exception b) { }
            }
            try { VM.HubProxy.Invoke("NuevaImpresionVenta"); } catch (Exception u) { MessageBox.Show("Se guardo la venta pero no se notifico al servidor", "Error de impresion"); }
           
        }

        
    }
}
