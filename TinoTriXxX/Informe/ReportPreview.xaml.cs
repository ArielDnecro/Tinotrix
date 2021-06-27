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
using TinoTriXxX.Modelo;
using TinoTriXxX.Util;

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

        public ReportPreview(string imgUrl, Foto photo, Papel paper)
        {
            InitializeComponent();

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

            this._rdlcGenerator.rdlcBodyWidth = this.contentWidth - 0.05;
            this._rdlcGenerator.rdlcBodyHeight = this.contentHeight;

            string contentRows = this.generatePictureColumns(photo, imgUrl);

            this._rdlcGenerator.AddContentToBody(contentRows);

            this.rvReportPreview.LocalReport.LoadReportDefinition(this._rdlcGenerator.GenerateReport());
            this.rvReportPreview.LocalReport.EnableExternalImages = true;
            this.rvReportPreview.RefreshReport();
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
    }
}
