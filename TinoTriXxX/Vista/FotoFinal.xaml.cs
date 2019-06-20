using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using TinoTriXxX.Informe;
using TinoTriXxX.Modelo;

namespace TinoTriXxX.Vista
{
    /// <summary>
    /// Lógica de interacción para FotoFinal.xaml
    /// </summary>
    public partial class FotoFinal : Window
    {
        string _imageUrl; Image _img; Foto _foto; Papel _papel;

        RowDefinition rowDef1;
        ColumnDefinition colDef1;
        public FotoFinal(string imageUrl, Image img, Foto foto, Papel papel)
        {
            InitializeComponent();
           
            ////this.Width= (double.Parse(papel.StrAncho) * 0.1) * 37.7952755905512;
            ////this.Height= (double.Parse(papel.StrAlto) * 0.1) * 37.7952755905512;
            _imageUrl = imageUrl; _img = img;  _foto = foto; _papel = papel;
            ////Gcuerpo.Height = ( double.Parse(papel.StrAlto)*4)+30;
            ////Gcuerpo.Width = double.Parse(papel.StrAncho)*4;
            this.SizeToContent = SizeToContent.Width;
            //double CEspDisponibleMilimetros = (double.Parse(papel.StrAncho) - (double.Parse(papel.StrMDerecho) + double.Parse(papel.StrMIzquierdo)));
            //double CEspDisponible= CEspDisponibleMilimetros * 0.1;
            //double CEspDisponibleReal = CEspDisponible * 37.7952755905512;
            //BCuerpo.Width = CEspDisponibleReal;
            //this.SizeToContent = SizeToContent.Height;
            //double FEspDisponibleMilimetros = (double.Parse(papel.StrAlto) - (double.Parse(papel.StrMInferior) + double.Parse(papel.StrMSuperior)));
            //double FEspDisponible = FEspDisponibleMilimetros * 0.1;
            //double FEspDisponibleReal = FEspDisponible * 37.7952755905512;
            //BCuerpo.Height = FEspDisponibleReal;
            //Bsuperior.Height = (double.Parse(papel.StrMSuperior)*0.1)* 37.7952755905512;
            //BInferior.Height = (double.Parse(papel.StrMInferior) * 0.1) * 37.7952755905512;
            //BDerecho.Width = (double.Parse(papel.StrMDerecho) * 0.1) * 37.7952755905512;
            //BIzquierdo.Width = (double.Parse(papel.StrMIzquierdo) * 0.1) * 37.7952755905512;

            for (int i = 0; i < int.Parse(_foto.VchColumna); i++)
            {
                colDef1 = new ColumnDefinition();
                GEspacioDisponible.ColumnDefinitions.Add(colDef1);
                
            }
            for (int a = 0; a < int.Parse(_foto.VchFila); a++)
            {
                rowDef1 = new RowDefinition();
                GEspacioDisponible.RowDefinitions.Add(rowDef1);
            }

            //var directorio = new System.IO.DirectoryInfo(_imageUrl);

            //foreach (var file in directorio.GetFiles())
            //{
            //    PictureBox pic = new PictureBox();
            //    pic.Location = new Point(50, 50);
            //    pic.Name = "pic" + i;
            //    pic.Size = new Size(300, 75);
            //    pic.ImageLocation = file;

            //    this.Controls.Add(pic);
            //}
            //Image[] ArrayImg = new Image[int.Parse(_foto.VchFila)*];
            //for (int o = 0; o < int.Parse(_foto.VchColumna); o++)
            //{
            //    for (int u = 0; u < int.Parse(_foto.VchFila); u++)
            //    {
            //        Image userimage = new Image();
            //        userimage = img;
            //        Grid.SetRow(userimage, u);
            //        Grid.SetColumn(userimage, o);
            //        GEspacioDisponible.Children.Add(userimage);
            //    }

            //}


            //int pos = 0;

            //foreach (RowDefinition item in GEspacioDisponible.RowDefinitions)
            //{
            //    GEspacioDisponible.ColumnDefinitions.Add(new ColumnDefinition() { Width = GridLength.Auto });
            //    _img.SetValue(Grid.ColumnProperty, pos);
            //    GEspacioDisponible.Children.Add(_img);
            //    pos++;
            //}
        }
        private void BtnCerrar_Click(object sender, RoutedEventArgs e)
        {
            // this.WindowState = WindowState.Minimized;
            this.Close();
        }
        private void BtnImprimirInforme_Click(object sender, RoutedEventArgs e)
        {
            VInfFotosCliente frm = new VInfFotosCliente( _imageUrl, _img, _foto, _papel);
            frm.ShowDialog();
            this.Close();
        }
    }
}
