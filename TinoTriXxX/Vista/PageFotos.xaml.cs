
using DAP.Adorners;
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
using TinoTriXxX.Modelo;
using TinoTriXxX.VistaModelo;
using System.Drawing;
using TinoTriXxX.Vista;
using TinoTriXxX.Informe;
using System.Drawing.Drawing2D;
using System.Drawing.Printing;
using ImageMagick;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para PageFotos.xaml
    /// </summary>
    public partial class PageFotos : Page
    {
        System.Windows.Point? lastCenterPositionOnTarget;
        System.Windows.Point? lastMousePositionOnTarget;
        System.Windows.Point? lastDragPoint;
        CroppingAdorner _clp;
        FrameworkElement _felCur = null;
        System.Windows.Media.Brush _brOriginal;
        VM_Escritorio VM;
        string path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        //reportDocument.Load(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,"Reporte_Peliculas.rpt"));
        string ImageName;
        double IntCropChanged;
        double Alto;
        double Ancho;
        double AltoRecorte;
        double AnchoRecorte;
        double AltoReal;
        double AnchoReal;
        double X;
        double Y;
        //int IntZoom;
        int CropVH;
        int IntRotation;
        Foto foto = null;
        string sourceFileOriginal = null;
        String filePathElegidaImprimir = null;
        //System.Drawing.Image imagenfinal = null;
        public PageFotos(VM_Escritorio vm)
        {
            InitializeComponent();
            VM = vm;
            cargarfotos();
            cargarPapel();
            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            //scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            //scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;

            //scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            scrollViewer.MouseMove += OnMouseMove;

            slider.ValueChanged += OnSliderValueChanged;

            //Cropimage();
            CropVH = 0;
            IntRotation = 0;
            }
      
        #region Crop
        
        private void SetClipColorRed()
        {
            if (_clp != null)
            {
                _clp.Fill = _brOriginal;
            }
        }
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RefreshCropImage();
        }
        private void RefreshCropImage()
        {
            if (_clp != null)
            {

                Rect rc = _clp.ClippingRectangle;
                tblkClippingRectangle.Text = string.Format(
                    "Clipping Rectangle: ({0:N1}, {1:N1}, {2:N1}, {3:N1})",
                    rc.Left,
                    rc.Top,
                    rc.Right,
                    rc.Bottom);
                imgCrop.Source = _clp.BpsCrop();
            }
        } 
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            WinLoaded();
        }
        void WinLoaded() {
            AddCropToElement(ImgFotoUsuario);
            _brOriginal = _clp.Fill;
            RefreshCropImage();
        }
        private void AddCropToElement(FrameworkElement fel)
        {
            if (_felCur != null)
            {
                RemoveCropFromCur();
            }
            Rect rcInterior = new Rect(0, 0, 0, 0);
          
                rcInterior = new Rect(
                            X - (AnchoRecorte / 2),
                            Y - (AltoRecorte / 2),
                           AnchoRecorte,
                            AltoRecorte);
            
            AdornerLayer aly = AdornerLayer.GetAdornerLayer(fel);
            _clp = new CroppingAdorner(fel, rcInterior);
            aly.Add(_clp);
            imgCrop.Source = _clp.BpsCrop();
            _clp.CropChanged += CropChanged;
            _felCur = fel;
            //if (rbRed.IsChecked != true)
            //{
            //    SetClipColorGrey();
            //}
        }
        private void RemoveCropFromCur()
        {
            AdornerLayer aly = AdornerLayer.GetAdornerLayer(_felCur);
            aly.Remove(_clp);
        }
        private void CropChanged(Object sender, RoutedEventArgs rea)
        {
            IntCropChanged = 1;
            RefreshCropImage();
            //if (rea.) { }
            //AddCropToElement(ImgFotoUsuario);
            //RefreshCropImage();
            
        }
        private void SetClipColorGrey()
        {
            if (_clp != null)
            {
                System.Windows.Media.Color clr = Colors.Black;
                clr.A = 110;
                _clp.Fill = new SolidColorBrush(clr);
            }
        }
        private void ImgFotoUsuario_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            UbicarMarcoSobreImagen();
        }
        private void ImgFotoUsuario_MouseMove(object sender, MouseEventArgs e)
        {
            //Point position = Mouse.GetPosition(ImgFotoUsuario);
            //txthandposition.Text = "Ubicacion mouse X: " + position.X + ", Y: " + position.Y;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Mouse.OverrideCursor = Cursors.Hand;
                UbicarMarcoSobreImagen();
            }

            if (IntCropChanged == 1)//Indicardor de que se editando el tamaño de recorte
            {

                double EmpAncho = Ancho / Alto;
                double EmpAlto = Alto / Ancho;
                if (CropVH == 0)
                {
                    EmpAncho = Ancho / Alto;
                    EmpAlto = Alto / Ancho;
                }
                else
                {
                    EmpAncho = Alto / Ancho;
                    EmpAlto = Ancho / Alto;
                }
                if (_clp.IntHandle == 1 || _clp.IntHandle == 4)
                {
                    AnchoRecorte = EmpAncho * _clp.BpsCrop().Height;
                    AltoRecorte = _clp.BpsCrop().Height;
                }
                if (_clp.IntHandle == 2 || _clp.IntHandle == 3)
                {
                    AltoRecorte = EmpAlto * _clp.BpsCrop().Width;
                    AnchoRecorte = _clp.BpsCrop().Width;
                }
                UbicarMarcoSobreImagen();
                _clp.IntHandle = 0;
                IntCropChanged = 0;

            }

        }
        void UbicarMarcoSobreImagen()
        {

            System.Windows.Point position = Mouse.GetPosition(ImgFotoUsuario);
            //p.Text = "Ubicacion de la imagen X: " + position.X + ", Y: " + position.Y;
            X = position.X;
            Y = position.Y;
            if (position.X < (AnchoRecorte / 2))
            {
                X = AnchoRecorte / 2;
            }
            if (position.X > ImgFotoUsuario.ActualWidth - (AnchoRecorte / 2))
            {
                X = ImgFotoUsuario.ActualWidth - (AnchoRecorte / 2);
            }
            if (position.Y < (AltoRecorte / 2))
            {
                Y = AltoRecorte / 2;
            }
            if (position.Y > ImgFotoUsuario.ActualHeight - (AltoRecorte / 2))
            {
                Y = ImgFotoUsuario.ActualHeight - (AltoRecorte / 2);
            }
            AddCropToElement(ImgFotoUsuario);
            RefreshCropImage();
            BtnAfirmarElegirFoto.Visibility = Visibility.Visible;
            BtnCancelarElegirFoto.Visibility = Visibility.Visible;

        }
        #endregion crop

        #region zoom
        void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (lastDragPoint.HasValue)
            {
                System.Windows.Point posNow = e.GetPosition(scrollViewer);

                double dX = posNow.X - lastDragPoint.Value.X;
                double dY = posNow.Y - lastDragPoint.Value.Y;

                lastDragPoint = posNow;

                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - dX);
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - dY);
            }
        }
        void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            var mousePos = e.GetPosition(scrollViewer);
            if (mousePos.X <= scrollViewer.ViewportWidth && mousePos.Y < scrollViewer.ViewportHeight) //make sure we still can use the scrollbars
            {
                scrollViewer.Cursor = Cursors.SizeAll;
                lastDragPoint = mousePos;
                Mouse.Capture(scrollViewer);
            }
        }
        void OnPreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            lastMousePositionOnTarget = Mouse.GetPosition(grid);

            if (e.Delta > 0)
            {
                slider.Value +=1;
                
            }
            if (e.Delta < 0)
            {
                slider.Value -= 1;

            }
            //IntZoom = e.Delta;
            e.Handled = true;
        }
        void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            scrollViewer.Cursor = Cursors.Arrow;
            scrollViewer.ReleaseMouseCapture();
            lastDragPoint = null;
        }
        void OnSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            scaleTransform.ScaleX = e.NewValue;
            scaleTransform.ScaleY = e.NewValue;

            var centerOfViewport = new System.Windows.Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
            lastCenterPositionOnTarget = scrollViewer.TranslatePoint(centerOfViewport, grid);
            //ScaleTransform scaleTransform1 = new ScaleTransform(0, 0, 0, 0);
            //myTransformGroup.Children.Add(scaleTransform1);
            //ImgFotoUsuario.RenderTransform = myTransformGroup;

        }
        void OnScrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange != 0 || e.ExtentWidthChange != 0)
            {
                System.Windows.Point? targetBefore = null;
                System.Windows.Point? targetNow = null;

                if (!lastMousePositionOnTarget.HasValue)
                {
                    if (lastCenterPositionOnTarget.HasValue)
                    {
                        var centerOfViewport = new System.Windows.Point(scrollViewer.ViewportWidth / 2, scrollViewer.ViewportHeight / 2);
                        System.Windows.Point centerOfTargetNow = scrollViewer.TranslatePoint(centerOfViewport, grid);

                        targetBefore = lastCenterPositionOnTarget;
                        targetNow = centerOfTargetNow;
                    }
                }
                else
                {
                    targetBefore = lastMousePositionOnTarget;
                    targetNow = Mouse.GetPosition(grid);

                    lastMousePositionOnTarget = null;
                }

                if (targetBefore.HasValue)
                {
                    double dXInTargetPixels = targetNow.Value.X - targetBefore.Value.X;
                    double dYInTargetPixels = targetNow.Value.Y - targetBefore.Value.Y;

                    double multiplicatorX = e.ExtentWidth / grid.Width;
                    double multiplicatorY = e.ExtentHeight / grid.Height;

                    double newOffsetX = scrollViewer.HorizontalOffset - dXInTargetPixels * multiplicatorX;
                    double newOffsetY = scrollViewer.VerticalOffset - dYInTargetPixels * multiplicatorY;

                    if (double.IsNaN(newOffsetX) || double.IsNaN(newOffsetY))
                    {
                        return;
                    }

                    scrollViewer.ScrollToHorizontalOffset(newOffsetX);
                    scrollViewer.ScrollToVerticalOffset(newOffsetY);
                }
            }
        }
        #endregion zoom

        #region fotos
        private void cargarfotos() {
            VM.CargarlistaFotos(VM.Sucursal.UidSucursal);
            
            LbFotos.ItemsSource = VM.ListaFotos;
        }
        private void btnRecargarListaFotos_Click(object sender, RoutedEventArgs e)
        {
            cargarfotos();
        }
        private void btnTomarFoto_Click(object sender, RoutedEventArgs e)
        {


            Button bt = (Button)sender;
            Grid parent = (Grid)bt.Parent;
             foto = (Foto)parent.DataContext;
            if (foto != null) {
                LbFotos.Visibility = Visibility.Hidden;
                lbTituloFotografias.Visibility = Visibility.Hidden;
                btnRecargarListaFotos.Visibility = Visibility.Hidden;

                GFotoSeleccionada.Visibility = Visibility.Visible;
                btnRegresarListaFotos.Visibility = Visibility.Visible;
                btnCapturarFoto.Visibility = Visibility.Visible;
                btnCargarFoto.Visibility = Visibility.Visible;
                BFotoUsuario.Visibility = Visibility.Visible;
                ImgFondoEditor1.Visibility = Visibility.Visible;
                ImgFondoEditor2.Visibility = Visibility.Visible;
                ImgFondoEditor3.Visibility = Visibility.Visible;
                // SvFotoUsuario.Visibility = Visibility.Visible;
                lbDescripcionFoto.Content = foto.StrDescripcion;
                lbStatusFoto.Content = foto.StrStatus;
                lbDescripcionDetalleFoto.Content = foto.StrDescripcionDetalle;

                ImgFotoUsuario.Visibility = Visibility.Collapsed;

                double PM = VM.MedidafotoConversor(foto.StrMedida);
                AltoRecorte = foto.IntAlto * PM ;
                AnchoRecorte = foto.IntAncho * PM;
                AltoReal = foto.IntAlto * PM;
                AnchoReal = foto.IntAncho * PM;

                Alto = foto.IntAlto;
                Ancho = foto.IntAncho;
                GridMenu.Visibility = Visibility.Visible;
                //-imgCrop.Visibility = Visibility.Collapsed;
                //RecSombraSeleccionadora.Visibility = Visibility.Collapsed;
                //btnRotacionMenos90.Visibility = Visibility.Visible;
                //btnRotacion90.Visibility = Visibility.Visible;
            }
        }
        private void btnRegresarListaFotos_Click(object sender, RoutedEventArgs e)
        {
            cargarfotos();
            LbFotos.Visibility = Visibility.Visible;
            btnRecargarListaFotos.Visibility = Visibility.Visible;
            lbTituloFotografias.Visibility = Visibility.Visible;

            btnCapturarFoto.Visibility = Visibility.Hidden;
            btnCargarFoto.Visibility = Visibility.Hidden;
            GFotoSeleccionada.Visibility = Visibility.Hidden;
            btnRegresarListaFotos.Visibility = Visibility.Hidden;
            BFotoUsuario.Visibility = Visibility.Hidden;
            ImgFondoEditor1.Visibility = Visibility.Hidden;
            ImgFondoEditor2.Visibility = Visibility.Hidden;
            ImgFondoEditor3.Visibility = Visibility.Hidden;
            // SvFotoUsuario.Visibility = Visibility.Hidden;
            //ImgFotoUsuario.Source = null;
            lbNombreFotoUsuario.Visibility = Visibility.Hidden;
            lbNombreFotoUsuario.Content = "";
            tblkClippingRectangle.Visibility = Visibility.Hidden;
            //-imgCrop.Visibility = Visibility.Hidden;
            btnRotacionMenos90.Visibility = Visibility.Hidden;
            btnRotacion90.Visibility = Visibility.Hidden;

            BtnCancelarElegirFoto.Visibility = Visibility.Hidden;
            BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
            //btnSeleccionFoto.Visibility = Visibility.Hidden;
            GridMenu.Visibility = Visibility.Hidden;
            GridMenu.IsEnabled = false;
            //btnRegresarEscogerFoto.Visibility = Visibility.Hidden;
            //BtnImprimir.Visibility = Visibility.Hidden;
            //RecSombraSeleccionadora.Visibility = Visibility.Collapsed;
        }
        private void btnCargarFoto_Click(object sender, RoutedEventArgs e)
        {
            cargarfoto();
           
        }
        void cargarfoto() {
            // Create OpenFileDialog 
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            // Set filter for file extension and default file extension 
            dlg.DefaultExt = "JPG Files (*.jpg)|*.jpg";
            dlg.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            dlg.Multiselect = false;
            // Display OpenFileDialog by calling ShowDialog method 
            Nullable<bool> result = dlg.ShowDialog();
            // Get the selected file name and display in a TextBox 
            if (result == true)
            {
                //BitmapImage bi = new BitmapImage();
                ImageName = dlg.SafeFileName;
                 sourceFileOriginal = dlg.FileName;
                lbNombreFotoUsuario.Visibility = Visibility.Visible;
                lbNombreFotoUsuario.Content = sourceFileOriginal;
                //System.Windows.Controls.Image  _image = new System.Windows.Controls.Image();
                //bi.BeginInit();
                //bi.UriSource = new System.Uri(sourceFile);
                //bi.EndInit();
                //_image.Source = bi;
                //ImgFotoUsuario.Source = _image.Source;
                string archivoOriginal = System.IO.Path.GetFileName(dlg.FileName);
               string ExtencionOriginalArchivo = System.IO.Path.GetExtension(dlg.FileName);
                string archivoNuevo = "FotoOriginalUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ExtencionOriginalArchivo;
                String DirectorioDestino = path + "\\Imagenes\\usuario\\";
                string ArchivoDescargado = DirectorioDestino + archivoNuevo;
                File.Copy(dlg.FileName, ArchivoDescargado,true);

                ImgFotoUsuario.Source = ToImageSource(ArchivoDescargado);
                ImgFotoUsuario.Visibility = Visibility.Visible;
                tblkClippingRectangle.Visibility = Visibility.Visible;
                btnRotacionMenos90.Visibility = Visibility.Visible;
                btnRotacion90.Visibility = Visibility.Visible;
            }

        }
        public System.Windows.Media.Imaging.BitmapImage ToImageSource(string path)
        {
            System.Windows.Media.Imaging.BitmapImage _bitmap = new System.Windows.Media.Imaging.BitmapImage();
            _bitmap.BeginInit();
           // _bitmap.CacheOption = BitmapCacheOption.OnLoad;
            _bitmap.UriSource = new Uri(path);
            _bitmap.EndInit();
            //using (var stream = File.OpenRead(path))
            //{
            //    _bitmap.BeginInit();
            //    //_bitmap.CacheOption = BitmapCacheOption.OnLoad;
            //    _bitmap.StreamSource = stream;
            //    _bitmap.EndInit();
            //}
            return _bitmap;
        }
        private void btnCapturarFoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Camara c = new Camara();
                //AplicarEfecto( this, 5);
                //c.ShowDialog();
                //AplicarEfecto(this, 0);
                Nullable<bool> result  = c.ShowDialog();
                //if (result == true) {
                if (c._imagenfinal.Source!=null) {
                    ImgFotoUsuario.Source = c._imagenfinal.Source;
                    ImgFotoUsuario.Visibility = Visibility.Visible;
                    //}
                    tblkClippingRectangle.Visibility = Visibility.Visible;
                    btnRotacionMenos90.Visibility = Visibility.Visible;
                    btnRotacion90.Visibility = Visibility.Visible;
                    
                    String Directorio = path + "\\Imagenes\\usuario\\";
                    string archivoWebCam = Directorio + "WebCamUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ".png";

                    //if (File.Exists(archivoWebCam))
                    //{
                    //    File.SetAttributes(archivoWebCam, FileAttributes.Normal);
                    //    File.Delete(archivoWebCam);//elimina la foto de otras sesiones
                    //}

                    var encoder = new PngBitmapEncoder();
                    //"JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImgFotoUsuario.Source));
                    using (FileStream stream = new FileStream(archivoWebCam, FileMode.Create)) encoder.Save(stream);
                    ImgFotoUsuario.Source = ToImageSource(archivoWebCam);
                    // File.Delete(archivoWebCam);
                }
            } catch (Exception et) {
                MessageBox.Show("¡No hay ninguna camara disponible! \r\n \r\n" + et.Message , "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }


        }
        private void AplicarEfecto(Window win, int NivelDegradado)
        {
            var objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = NivelDegradado;
            win.Effect = objBlur;
        }
        private void BtnCancelarElegirFoto_Click(object sender, RoutedEventArgs e)
        {
            //btnSeleccionFoto.Visibility = Visibility.Hidden;
            btnCargarFoto.Visibility = Visibility.Visible;
            BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
            BtnCancelarElegirFoto.Visibility = Visibility.Hidden;
            //imgCrop.Visibility = Visibility.Hidden;
            GridMenu.IsEnabled = false;
        }
        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }
        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //UserControl usc = null;
            //GridMain.Children.Clear();

            //switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            //{
            //    case "ItemHome":
            //        usc = new UserControlHome();
            //        GridMain.Children.Add(usc);
            //        break;
            //    case "ItemCreate":
            //        usc = new UserControlCreate();
            //        GridMain.Children.Add(usc);
            //        break;
            //    default:
            //        break;
            //}
        }
        private void BtnAfirmarElegirFoto_Click(object sender, RoutedEventArgs e)
        {
            //GridMenu.IsEnabled = true;
            ////-imgCrop.Visibility = Visibility.Visible;
            //btnRotacionMenos90.Visibility = Visibility.Hidden;
            //btnRotacion90.Visibility = Visibility.Hidden;
            //BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
            //BtnCancelarElegirFoto.Visibility = Visibility.Hidden;
            ////RecSombraSeleccionadora.Visibility = Visibility.Hidden;
            //ImgFotoUsuario.Visibility = Visibility.Collapsed;
            //tblkClippingRectangle.Visibility = Visibility.Collapsed;
            ////btnSeleccionFoto.Visibility = Visibility.Hidden;
            //btnCargarFoto.Visibility = Visibility.Hidden;
            //btnCapturarFoto.Visibility = Visibility.Hidden;
            //btnRegresarEscogerFoto.Visibility = Visibility.Visible;
            //BtnImprimir.Visibility = Visibility.Visible;



            String Directorio = path + "\\Imagenes\\usuario\\"; //"C:\\Users\\Iudex\\Documents\\TinoTrix\\Clone\\Tinotrix\\TinoTriXxX"
                                                                // C: \Users\Iudex\Documents\TinoTrix\Clone\Tinotrix\TinoTriXxX\Imagenes\usuario
            String Extencion = System.IO.Path.GetExtension(sourceFileOriginal);
            String Archivo = "FotoFinalUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ".png";
            filePathElegidaImprimir = Directorio + Archivo ;
            System.Windows.Controls.Image UserImage = imgCrop;

            //System.Drawing.Image imgd = System.Drawing.Image.FromFile(@"C:\Users\...\Pictures\book.jpg");

            //Bitmap bmp = (Bitmap)Bitmap.FromFile(@"C:\testimage.bmp");
            //Bitmap newImage = ResizeBitmap(bmp, Ancho, Alto);
            //Bitmap bmp = (Bitmap)Bitmap.FromFile(@"C:\testimage.bmp");
            //bmp += new System.Windows.Forms.PaintEventHandler(ResizeImagen);

            // System.Windows.Controls.Image image = System.Windows.Controls.Image.filw("foo.png");
            //Bitmap bitmap = new Bitmap(imgCrop, new System.Windows.Size(320, 480)); // or some math to resize it to 1/2
            //bitmap.Save("foo2.png", System.Drawing.Imaging.ImageFormat.Png);

            var encoder = new PngBitmapEncoder();
            //"JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));
            using (FileStream stream = new FileStream(filePathElegidaImprimir, FileMode.Create)) encoder.Save(stream);

            RedimencionarImagenElegida();

            VInfFotosCliente frm = new VInfFotosCliente(filePathElegidaImprimir, imgCrop, foto, VM.Papel);
            frm.ShowDialog();
            //FotoFinal ff = new FotoFinal(filePathElegidaImprimir, imgCrop, foto, VM.Papel);
            //ff.ShowDialog();
            //File.Delete(filePathElegidaImprimir);
        }

        void RedimencionarImagenElegida()
        {
            MagickImage OImage = new MagickImage(filePathElegidaImprimir);
            // OImage.
            OImage.Resize(0, (int)AltoReal);
            File.Delete(filePathElegidaImprimir);
            OImage.Write(filePathElegidaImprimir);
        }
        //private void btnRegresarEscogerFoto_Click(object sender, RoutedEventArgs e)
        //{
        //    GridMenu.IsEnabled = false;
        //    //-imgCrop.Visibility = Visibility.Hidden;
        //    btnRotacionMenos90.Visibility = Visibility.Visible;
        //    btnRotacion90.Visibility = Visibility.Visible;
        //    BtnAfirmarElegirFoto.Visibility = Visibility.Visible;
        //    BtnCancelarElegirFoto.Visibility = Visibility.Visible;
        //    //RecSombraSeleccionadora.Visibility = Visibility.Visible;
        //    ImgFotoUsuario.Visibility = Visibility.Visible;
        //    tblkClippingRectangle.Visibility = Visibility.Visible;
        //    //btnSeleccionFoto.Visibility = Visibility.Visible;
        //    btnCargarFoto.Visibility = Visibility.Visible;
        //    btnCapturarFoto.Visibility = Visibility.Visible;
        //    btnRegresarEscogerFoto.Visibility = Visibility.Hidden;
        //    BtnImprimir.Visibility = Visibility.Hidden;
        //}

        #endregion fotos

        #region Rotacion
        //TransformGroup myTransformGroup = new TransformGroup();
        private void btnRotacionMenos90_Click(object sender, RoutedEventArgs e)
        {
            //RotateTransform rotateTransform = ImgFotoUsuario.LayoutTransform as RotateTransform;
            //rotateTransform = new RotateTransform(-90);
            //rotateTransform.CenterX = ImgFotoUsuario.ActualWidth / 2;
            //rotateTransform.CenterY = ImgFotoUsuario.ActualHeight / 2;
            //myTransformGroup.Children.Add(rotateTransform);
            //ImgFotoUsuario.LayoutTransform = myTransformGroup;
            ////ImgFotoUsuario.SizeChanged=ImgFotoUsuario.ActualWidth;
            ////System.Windows.Controls.Image _image = new System.Windows.Controls.Image();
            //double AnchoC = AnchoReal;
            //double AltoC = AltoReal;

            //if (CropVH==0) {
            //    CropVH = 1;

            //} else {
            //    CropVH = 0;
            //}
            //AnchoReal = AltoC;
            //AltoReal = AnchoC;
            //UbicarMarcoSobreImagen();

            //intRotation marca la rotacion actual de la foto


            if (IntRotation == 3)
            { RotarImagen(ImgFotoUsuario, Rotation.Rotate180); IntRotation = 2; }
            else
            {
                if (IntRotation == 2)
                { RotarImagen(ImgFotoUsuario, Rotation.Rotate90); IntRotation = 1; }
                else
                {
                    if (IntRotation == 1)
                    { RotarImagen(ImgFotoUsuario, Rotation.Rotate0); IntRotation = 0; }
                    else
                    {
                        if (IntRotation == 0)
                        { RotarImagen(ImgFotoUsuario, Rotation.Rotate270); IntRotation = 3; }
                    }

                }

            }

        }
        private void RotarImagen(System.Windows.Controls.Image control, Rotation value)
        {
            var biOriginal = (BitmapImage)control.Source;

            var biRotated = new BitmapImage();
            biRotated.BeginInit();
            biRotated.UriSource = biOriginal.UriSource;
            biRotated.Rotation = value;
            biRotated.EndInit();

            control.Source = biRotated;
            
        }
        private void btnRotacion90_Click(object sender, RoutedEventArgs e)
        {
            if (IntRotation == 3)
            { RotarImagen(ImgFotoUsuario, Rotation.Rotate0); IntRotation = 0; }
            else
            {
                if (IntRotation == 2)
                { RotarImagen(ImgFotoUsuario, Rotation.Rotate270); IntRotation = 3; }
                else
                {
                    if (IntRotation == 1)
                    { RotarImagen(ImgFotoUsuario, Rotation.Rotate180); IntRotation = 2; }
                    else
                    {
                        if (IntRotation == 0)
                        { RotarImagen(ImgFotoUsuario, Rotation.Rotate90); IntRotation = 1; }
                    }

                }

            }
        }
        
        #endregion Rotacion

        #region papel
        private void cargarPapel()
        {
            VM.CargarPapel(VM.Sucursal.UidSucursal);
            //LbFotos.ItemsSource = VM.ListaFotos;
        }

        #endregion

        //private void BtnImprimir_Click(object sender, RoutedEventArgs e)
        //{ 


        //}
        // public System.Windows.Forms.PictureBox pictureBox1 = new System.Windows.Forms.PictureBox();

        //pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
        //private void GetPixel_Example(System.Windows.Forms.PaintEventArgs e)
        //{
        //    System.Drawing.Image image = new Bitmap("Apple.gif");
        //    // Draw the image unaltered with its upper-left corner at (0, 0).
        //    e.Graphics.DrawImage(image, 0, 0);
        //    // Make the destination rectangle 30 percent wider and
        //    // 30 percent taller than the original image.
        //    // Put the upper-left corner of the destination
        //    // rectangle at (150, 20).
        //    int width = image.Width;
        //    int height = image.Height;
        //    RectangleF destinationRect = new RectangleF(150, 20, 1.3f * width, 1.3f * height);
        //    // Draw a portion of the image. Scale that portion of the image
        //    // so that it fills the destination rectangle.
        //    RectangleF sourceRect = new RectangleF(0, 0, .75f * width, .75f * height);
        //    e.Graphics.DrawImage(image, destinationRect, sourceRect, GraphicsUnit.Pixel);
        //}
        //private void pictureBox1_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        //{
        //    GetPixel_Example(e);
        //}

        //public Bitmap ResizeBitmap(Bitmap bmp, int width, int height)
        //{
        //    Bitmap result = new Bitmap(width, height);
        //    using (Graphics g = Graphics.FromImage(result))
        //    {
        //        g.DrawImage(bmp, 0, 0, width, height);
        //    }

        //    return result;
        //}

        //public static Bitmap ResizeImage(System.Drawing.Image image, int width, int height)
        //{
        //    //Image imgPhoto = Image.FromFile(@"C:\Users\...\Pictures\book.jpg");
        //    //Bitmap image = ResizeImage(imgPhoto, 100, 150);
        //    //image.Save(@"C:\Users\...\Pictures\books.jpg");
        //    var destRect = new System.Drawing.Rectangle(0, 0, width, height);
        //    var destImage = new Bitmap(width, height);

        //    destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

        //    using (var graphics = Graphics.FromImage(destImage))
        //    {
        //        graphics.CompositingMode = CompositingMode.SourceCopy;
        //        graphics.CompositingQuality = CompositingQuality.HighQuality;
        //        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
        //        graphics.SmoothingMode = SmoothingMode.HighQuality;
        //        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

        //        using (var wrapMode = new System.Drawing.Imaging.ImageAttributes())
        //        {
        //            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
        //            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
        //        }
        //    }
        //    return destImage;
        //}
        //PrintPageEventArgs
        //private void ResizeImagen(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        //{
        //    Bitmap bmp = new Bitmap(@"c:\miqui\tomate.png");

        //    float anchoImagen = 25.4F * bmp.Width / bmp.HorizontalResolution; // en milímetros
        //    float alturaImagen = 25.4F * bmp.Height / bmp.VerticalResolution;

        //    e.Graphics.DrawImage(bmp, 0, 0);
        //    e.Graphics.PageUnit = GraphicsUnit.Millimeter;

        //    // pinto el texto un cm debajo de la imagen
        //   // string texto = "Dimensiones de la imagen en milímetros: " + anchoImagen + "x" + alturaImagen;
        //    e.Graphics.TranslateTransform(0, alturaImagen + 10);
        //   // e.Graphics.DrawString(texto, Font, System.Drawing.Brushes.Black, 0, 0);
        //}

        //public static Bitmap Resize(string filePath)
        //{
        //    Bitmap image = (Bitmap)Bitmap.FromFile(filePath);
        //    int destinoWidth = (int)(image.Width);
        //    int destinoHeight = (int)(image.Height);

        //    Bitmap Imagen2 = new Bitmap(destinoWidth, destinoHeight);
        //    using (Graphics g = Graphics.FromImage((System.Drawing.Image)Imagen2)) { g.DrawImage(image, 0, 0, destinoWidth, destinoHeight); }
        //    image.Dispose();
        //    return (Imagen2);
        //}

        

        // FrameWithinGrid.Navigate(new System.Uri("Page1.xaml",UriKind.RelativeOrAbsolute));
        //Process.start();
        // System.Diagnostics.Process.Start("http://mydomain.com/Default.aspx");

        //     NavigationWindow window = new NavigationWindow();

        // Uri source = new Uri("http://www.c-sharpcorner.com/Default.aspx", UriKind.Absolute);

        // window.Source = source; window.Show();
    }
}
