using Microsoft.Reporting.WinForms;
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

namespace TinoTriXxX.Informe
{
    /// <summary>
    /// Lógica de interacción para VInfFotosCliente.xaml
    /// </summary>
    public partial class VInfFotosCliente : Window
    {
        string path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        string ImagenVacia;
        Papel _papel;
        string _imageUrl;
        List<Imagen> _fotos;
        Foto _foto;
        Image _img;
        public VInfFotosCliente(string imageUrl, Image img ,Foto foto, Papel papel)
        {
            InitializeComponent();
            _imageUrl = imageUrl;
            _papel = papel;
            List<Imagen> fotos = new List<Imagen>();
            _fotos = fotos;
            _foto = foto;
            _img = img;
            ImagenVacia= path+ "\\Imagenes\\vacio.png";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (int.Parse(_foto.VchColumna) <= 10 && int.Parse(_foto.VchColumna) != 0)
            {
                //if (int.Parse(_foto.VchColumna) == 1)
                //{
                //    this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto1.rdlc";
                //}
                //else {
                //    if (_foto.StrDescripcion.Contains("Infantil")) { Infantil(); } else {
                //        if (_foto.StrDescripcion.Contains("Cartilla Militar")) { Militar(); } else {
                //            if (_foto.StrDescripcion.Contains("Titulo")) { Titulo(); } else {
                //                if (_foto.StrDescripcion.Contains("Credencial")) { Credencial(); } else {
                                    General();
                //                }
                //            }
                //        }
                //    }
                //}
               
                //FileInfo fi = new FileInfo(_imageUrl);
                // ReportFotosUsuario.SetPageSettings = 1000;
                System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
                System.Drawing.Printing.PaperSize size = new PaperSize();
                size.RawKind = (int)PaperKind.Custom;
                size.Height = int.Parse(_papel.StrAlto) * 4;
                size.Width = int.Parse(_papel.StrAncho) * 4;
                pg.PaperSize = size;
                pg.Margins.Top = int.Parse(_papel.StrMSuperior) * 4;
                pg.Margins.Bottom = int.Parse(_papel.StrMInferior) * 4;
                pg.Margins.Left = int.Parse(_papel.StrMDerecho) * 4;
                pg.Margins.Right = int.Parse(_papel.StrMIzquierdo) * 4;
                ReportFotosUsuario.SetPageSettings(pg);
                this.ReportFotosUsuario.LocalReport.EnableExternalImages = true;

                ReportParameter pImageUrl = new ReportParameter("url", new Uri(ImagenVacia).AbsoluteUri);
                #region
                //ReportParameter pImageUrl1 = new ReportParameter("pImageUrl1", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl2 = new ReportParameter("pImageUrl2", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl3 = new ReportParameter("pImageUrl3", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl4 = new ReportParameter("pImageUrl4", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl5 = new ReportParameter("pImageUrl5", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl6 = new ReportParameter("pImageUrl6", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl7 = new ReportParameter("pImageUrl7", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl8 = new ReportParameter("pImageUrl8", new Uri(ImagenVacia).AbsoluteUri);
                //ReportParameter pImageUrl9 = new ReportParameter("pImageUrl9", new Uri(ImagenVacia).AbsoluteUri);
                #endregion
                List<ReportParameter> rpl = new List<ReportParameter>();
                ReportParameter[] Rp = new ReportParameter[int.Parse(_foto.VchFila) * int.Parse(_foto.VchFila)];
                #region
                //for (int i = 0; i < int.Parse(_foto.VchColumna); i++)
                //{
                //    for (int a = 0; a < int.Parse(_foto.VchFila); a++)
                //    {
                //        //if (i == 0)
                //        //{
                //            pImageUrl = new ReportParameter("pImageUrl", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl);
                //        //}
                //        //if (i == 1)
                //        //{ pImageUrl1 = new ReportParameter("pImageUrl1", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl1); }
                //        //if (i == 2)
                //        //{ pImageUrl2 = new ReportParameter("pImageUrl2", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl2); }
                //        //if (i == 3)
                //        //{ pImageUrl3 = new ReportParameter("pImageUrl3", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl3); }
                //        //if (i == 4)
                //        //{ pImageUrl4 = new ReportParameter("pImageUrl4", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl4); }
                //        //if (i == 5)
                //        //{ pImageUrl5 = new ReportParameter("pImageUrl5", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl5); }
                //        //if (i == 6)
                //        //{ pImageUrl6 = new ReportParameter("pImageUrl6", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl6); }
                //        //if (i == 7)
                //        //{ pImageUrl7 = new ReportParameter("pImageUrl7", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl7); }
                //        //if (i == 8)
                //        //{ pImageUrl8 = new ReportParameter("pImageUrl8", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl8); }
                //        //if (i == 9)
                //        //{ pImageUrl9 = new ReportParameter("pImageUrl9", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl9); }
                //    }

                //}
                #endregion
                for (int a = 0; a < int.Parse(_foto.VchFila); a++)
                {
                    pImageUrl = new ReportParameter("url", new Uri(_imageUrl).AbsoluteUri); rpl.Add(pImageUrl);
                    Imagen imagen = new Imagen();
                    imagen.ruta = _imageUrl;
                    _fotos.Add(imagen);
                }
                #region
                //if (int.Parse(_foto.VchColumna)<6) {
                //    for (int o = int.Parse(_foto.VchColumna); o < 6; o++)
                //    {
                //        if (o == 0) { rpl.Add(pImageUrl); }
                //        if (o == 1) { rpl.Add(pImageUrl1); }
                //        if (o == 2) { rpl.Add(pImageUrl2); }
                //        if (o == 3) { rpl.Add(pImageUrl3); }
                //        if (o == 4) { rpl.Add(pImageUrl4); }
                //        if (o == 5) { rpl.Add(pImageUrl5); }
                //    }
                //}
                
                #endregion
                Rp = rpl.ToArray();
                ReportDataSource dat = new ReportDataSource("dsurl", _fotos);
                this.ReportFotosUsuario.LocalReport.SetParameters(Rp);
                //this.ReportFotosUsuario.ControlAdded += _foto;
                this.ReportFotosUsuario.LocalReport.DataSources.Add(dat);

                this.ReportFotosUsuario.RefreshReport();
            }
            else {
                
                if (int.Parse(_foto.VchColumna) > 10) {
                    MessageBoxResult result = MessageBox.Show("El numero de fotos es muy grande ", "Informe de impresion");
                }
                if (int.Parse(_foto.VchColumna) == 0) {
                    MessageBoxResult result = MessageBox.Show("La impresion NO contiene 'FOTOS' n/ favor de informar al administrador ", "Informe de impresion");
                }
            }
        }

        private void ReportFotosUsuario_Load(object sender, EventArgs e)
        {

        }

        void Infantil() {
           
            if (int.Parse(_foto.VchColumna) == 2)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Infantil.Infantil2.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 3)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Infantil.Infantil3.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 4)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Infantil.Infantil4.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 5)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Infantil.Infantil5.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 6)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Infantil.Infantil6.rdlc";
            }
            
        }
        void Militar() {
            if (int.Parse(_foto.VchColumna) == 2)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Militar.Militar2.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 3)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Militar.Militar3.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 4)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Militar.Militar4.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 5)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Militar.Militar5.rdlc";
            }
        }
        void Credencial() {
            if (int.Parse(_foto.VchColumna) == 2)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Credencial.Credencial2.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 3)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Credencial.Credencial3.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 4)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Credencial.Credencial4.rdlc";
            }
            if (int.Parse(_foto.VchColumna) == 5)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Credencial.Credencial5.rdlc";
            }
        }
        void Titulo() {
            if (int.Parse(_foto.VchColumna) == 2)
            {
                this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Titulo.Titulo2.rdlc";
            }
        }
        void General() {
            if (int.Parse(_foto.VchColumna) == 1){ this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto1.rdlc"; }
            else { if (int.Parse(_foto.VchColumna) == 2) { this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto2.rdlc"; }
                else { if (int.Parse(_foto.VchColumna) == 3) { this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto3.rdlc"; }
                    else { if (int.Parse(_foto.VchColumna) == 4) { this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto4.rdlc"; }
                        else { if (int.Parse(_foto.VchColumna) == 5) { this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto5.rdlc"; }
                            else { if (int.Parse(_foto.VchColumna) == 6) { this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto6.rdlc"; }
                                else { if (int.Parse(_foto.VchColumna) == 7) { this.ReportFotosUsuario.LocalReport.ReportEmbeddedResource = "TinoTriXxX.Informe.Foto7.rdlc"; } } } } } } }
        }
    }
}
