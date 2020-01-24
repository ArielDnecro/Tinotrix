using AForge.Video;
using AForge.Video.DirectShow;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Lógica de interacción para Camara.xaml
    /// </summary>
    public partial class Camara : Window
    {
        //String filePathElegidaImprimir = null;
        string path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        public System.Windows.Controls.Image _imagenfinal = null;
        System.Windows.Media.Color TemaAzulEstandar = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#FF3580BF");
        System.Windows.Media.Color TemaVerdeEstandar = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#00bb2d");
        System.Windows.Media.Color TemaRojoEstandar = (System.Windows.Media.Color)System.Windows.Media.ColorConverter.ConvertFromString("#f00800");
        bool Boo1eraVDispositivo = true;
        public Camara()
        {

            InitializeComponent();
            BtnCapturarFoto.Fill = new SolidColorBrush(TemaAzulEstandar);
            BtnConfirmar.Fill = new SolidColorBrush(TemaVerdeEstandar);
            BtnRegresar.Fill = new SolidColorBrush(TemaRojoEstandar);
            EIzquierdo.Fill = new SolidColorBrush(TemaRojoEstandar);
            EDerecho.Fill = new SolidColorBrush(TemaVerdeEstandar);
            AjustarVentana();
            BuscarDispositivos();
            IniciarCamara();
            _imagenfinal = new System.Windows.Controls.Image();
        }

        #region VisualInteraccion
        private void BtnCapturarFoto_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BtnCapturarFoto.Fill = System.Windows.Media.Brushes.Transparent;
        }

        private void BtnCapturarFoto_MouseMove(object sender, MouseEventArgs e)
        {
            BtnCapturarFoto.Cursor = Cursors.Hand;
            IcoCapturarFoto.Cursor = Cursors.Hand;
        }

        private void BtnCapturarFoto_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BtnCapturarFoto.Fill = new SolidColorBrush(TemaAzulEstandar);

            BtnCapturarFoto.Visibility = Visibility.Collapsed;
            IcoCapturarFoto.Visibility = Visibility.Collapsed;

            BtnRegresar.Visibility = Visibility.Visible;
            EIzquierdo.Visibility = Visibility.Visible;
            lbRegresar.Visibility = Visibility.Visible;
            IcoRegresar.Visibility = Visibility.Visible;

            BtnConfirmar.Visibility = Visibility.Visible;
            EDerecho.Visibility = Visibility.Visible;
            lbConfirm.Visibility = Visibility.Visible;
            IcoConfirm.Visibility = Visibility.Visible;

            BControladores.Visibility = Visibility.Collapsed;

            
            _imagenfinal.Source = ImgCamara.Source;
            TerminarFuenteDeVideo();
        }

        public System.Windows.Media.Imaging.BitmapImage ToImageSource(string path)
        {
            System.Windows.Media.Imaging.BitmapImage _bitmap = new System.Windows.Media.Imaging.BitmapImage();
            _bitmap.BeginInit();
            _bitmap.CacheOption = BitmapCacheOption.OnLoad;
            _bitmap.UriSource = new Uri(path);
            _bitmap.EndInit();
            return _bitmap;
        }

        private void BtnRegresar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BtnRegresar.Fill = System.Windows.Media.Brushes.Transparent;
            EIzquierdo.Fill = System.Windows.Media.Brushes.Transparent;
        }

        private void BtnRegresar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnRegresar.Cursor = Cursors.Hand;
            EIzquierdo.Cursor = Cursors.Hand;
            lbRegresar.Cursor = Cursors.Hand;
            IcoRegresar.Cursor = Cursors.Hand;
        }

        private void BtnRegresar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BtnRegresar.Fill = new SolidColorBrush(TemaRojoEstandar);
            EIzquierdo.Fill = new SolidColorBrush(TemaRojoEstandar);

            BtnCapturarFoto.Visibility = Visibility.Visible;
            IcoCapturarFoto.Visibility = Visibility.Visible;

            BtnRegresar.Visibility = Visibility.Collapsed;
            EIzquierdo.Visibility = Visibility.Collapsed;
            lbRegresar.Visibility = Visibility.Collapsed;
            IcoRegresar.Visibility = Visibility.Collapsed;

            BtnConfirmar.Visibility = Visibility.Collapsed;
            EDerecho.Visibility = Visibility.Collapsed;
            lbConfirm.Visibility = Visibility.Collapsed;
            IcoConfirm.Visibility = Visibility.Collapsed;

            BControladores.Visibility = Visibility.Visible;
        }

        private void BtnConfirmar_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            BtnConfirmar.Fill = System.Windows.Media.Brushes.Transparent;
            EDerecho.Fill = System.Windows.Media.Brushes.Transparent;
        }
        
        private void BtnConfirmar_MouseMove(object sender, MouseEventArgs e)
        {
            BtnConfirmar.Cursor = Cursors.Hand;
            EDerecho.Cursor = Cursors.Hand;
            lbConfirm.Cursor = Cursors.Hand;
            IcoConfirm.Cursor = Cursors.Hand;
        }

        private void BtnConfirmar_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            BtnConfirmar.Fill = new SolidColorBrush(TemaVerdeEstandar);
            EDerecho.Fill = new SolidColorBrush(TemaVerdeEstandar);

            
            this.Close();
        }
      
        private void BtnCerrarCamara_Click(object sender, RoutedEventArgs e)
        {
            TerminarFuenteDeVideo();
            this.Close();
        }
        #endregion visualinteraccion

        #region Camara Entrada
        void AjustarVentana()
        {
            WindowState = WindowState.Maximized;
        }
        private bool ExisteDispositivo = false;
        private FilterInfoCollection DispositivoDeVideo;
        private VideoCaptureDevice FuenteDeVideo = null;
        public void CargarDispositivos(FilterInfoCollection Dispositivos)
        {
            CbDispositivos.Items.Clear();
            for (int i = 0; i < Dispositivos.Count; i++)
            {
                CbDispositivos.Items.Add(Dispositivos[i].Name.ToString());
                
            }
            if (Boo1eraVDispositivo == true)
            {
                CbDispositivos.SelectedValue = Dispositivos[0].Name.ToString();
                CbDispositivos.SelectedIndex = 0;
                //cbxDispositivo.Text = VariablesGlobal.CamaraEntradaSeleccionada;
                Boo1eraVDispositivo = false;
            }
        }
        public void BuscarDispositivos()
        {
            DispositivoDeVideo = new FilterInfoCollection(FilterCategory.VideoInputDevice);

            if (DispositivoDeVideo.Count == 0)
            {
                ExisteDispositivo = false;
                TerminarFuenteDeVideo();
                //MessageBox.Show("¡No hay ninguna camara disponible!", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                
                this.Close();
            }
            else
            {
                ExisteDispositivo = true;
                CargarDispositivos(DispositivoDeVideo);
            }
        }
        //void SaveToBmp(FrameworkElement visual, string fileName)
        //{
        //    var encoder = new BmpBitmapEncoder();
        //    SaveUsingEncoder(visual, fileName, encoder);
        //}

        //void SaveUsingEncoder(FrameworkElement visual, string fileName, BitmapEncoder encoder)
        //{
        //    RenderTargetBitmap bitmap = new RenderTargetBitmap((int)visual.ActualWidth, (int)visual.ActualHeight, 96, 96, PixelFormats.Pbgra32);
        //    bitmap.Render(visual);
        //    BitmapFrame frame = BitmapFrame.Create(bitmap);
        //    encoder.Frames.Add(frame);

        //    using (var stream = File.Create(fileName))
        //    {
        //        encoder.Save(stream);
        //    }
        //}
        public void Video_NuevoFrame(object sender, NewFrameEventArgs eventArgs)
        {
            System.Drawing.Image imgforms = (Bitmap)eventArgs.Frame.Clone();

            BitmapImage bi = new BitmapImage();
            bi.BeginInit();

            MemoryStream ms = new MemoryStream();
            imgforms.Save(ms, ImageFormat.Bmp);
            ms.Seek(0, SeekOrigin.Begin);

            bi.StreamSource = ms;
            bi.EndInit();

            bi.Freeze();

            Dispatcher.BeginInvoke(new ThreadStart(delegate
            {
                ImgCamara.Source = bi;
            }));
            //AjustarVentana();
        }
        private void IniciarCamara()
        {
            IniciarFunenteVideo();
        }
        //private void btnCapturarFoto_Click(object sender, RoutedEventArgs e)
        //{
        //    string AccionCamara = btnCapturarFoto.Content.ToString();

        //    switch (AccionCamara)
        //    {
        //        case "Capturar Foto":
        //            btnCapturarFoto.Content = "Actualizar Foto";
        //            imgFotoEntrada.Source = imgCamara.Source;
        //            break;
        //        case "Actualizar Foto":
        //            imgFotoEntrada.Source = imgCamara.Source;
        //            break;
        //    }
        //}
        public void IniciarFunenteVideo()
        {
            if (CbDispositivos.SelectedIndex != -1)
            {
                FuenteDeVideo = new VideoCaptureDevice(DispositivoDeVideo[CbDispositivos.SelectedIndex].MonikerString);
                FuenteDeVideo.NewFrame += new NewFrameEventHandler(Video_NuevoFrame);
                FuenteDeVideo.Start();
                //btnCapturarFoto.Content = "Capturar Foto";
            }
        }
        public void TerminarFuenteDeVideo()
        {
            if (!(FuenteDeVideo == null))
                if (FuenteDeVideo.IsRunning)
                {
                    FuenteDeVideo.SignalToStop();
                    FuenteDeVideo = null;
                }
        }
        
        #endregion

        private void CbDispositivos_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cbChange();
        }
        void cbChange() {
            if (Boo1eraVDispositivo == false)
            {
                TerminarFuenteDeVideo();
                //CbDispositivos.SelectedValue = Dispositivos[0].Name.ToString();
                //CbDispositivos.SelectedIndex = 0;
                IniciarCamara();

            }
        }
        private void BtnActulizarDispositivos_Click(object sender, RoutedEventArgs e)
        {
            string f = CbDispositivos.SelectedValue.ToString(); 
            int n= CbDispositivos.SelectedIndex;
            BuscarDispositivos();
            if (n >= CbDispositivos.Items.Count-1)
            {
                CbDispositivos.SelectedIndex = n;
            }
            else {
                CbDispositivos.SelectedIndex = 0;
            }

        }
    }
}
