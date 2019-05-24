
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
        //BitmapImage bi = new BitmapImage();
        
        string path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
        //reportDocument.Load(Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName,"Reporte_Peliculas.rpt"));
        string ImageName;
        double IntCropChanged;
        double Alto;
        double Ancho;
        double AltoReal;
        double AnchoReal;
        double X;
        double Y;
        int IntZoom;
        int CropVH;
        int IntRotation;
        public PageFotos(VM_Escritorio vm)
        {
            InitializeComponent();
            VM = vm;
            cargarfotos();

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
        //private void CropImage_Checked(object sender, RoutedEventArgs e)
        //{
        //    //if (dckControls != null && imgChurch != null)
        //    Cropimage();
        //}

        //void Cropimage() {
           
        //    if (ImgFotoUsuario != null)
        //    {
        //        //dckControls.Visibility = Visibility.Hidden;
        //        SetClipColorRed();
        //        AddCropToElement(ImgFotoUsuario);
        //        RefreshCropImage();
        //    }
        //}
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
                            X - (AnchoReal / 2),
                            Y - (AltoReal / 2),
                           AnchoReal,
                            AltoReal);
            
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
                    AnchoReal = EmpAncho * _clp.BpsCrop().Height;
                    AltoReal = _clp.BpsCrop().Height;
                }
                if (_clp.IntHandle == 2 || _clp.IntHandle == 3)
                {
                    AltoReal = EmpAlto * _clp.BpsCrop().Width;
                    AnchoReal = _clp.BpsCrop().Width;
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
            if (position.X < (AnchoReal / 2))
            {
                X = AnchoReal / 2;
            }
            if (position.X > ImgFotoUsuario.ActualWidth - (AnchoReal / 2))
            {
                X = ImgFotoUsuario.ActualWidth - (AnchoReal / 2);
            }
            if (position.Y < (AltoReal / 2))
            {
                Y = AltoReal / 2;
            }
            if (position.Y > ImgFotoUsuario.ActualHeight - (AltoReal / 2))
            {
                Y = ImgFotoUsuario.ActualHeight - (AltoReal / 2);
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
            Foto foto = (Foto)parent.DataContext;
            if (foto != null) {
                LbFotos.Visibility = Visibility.Hidden;
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
                AltoReal = foto.IntAlto * PM ;
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
            btnRegresarEscogerFoto.Visibility = Visibility.Hidden;
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
                string sourceFile = dlg.FileName;
                lbNombreFotoUsuario.Visibility = Visibility.Visible;
                lbNombreFotoUsuario.Content = sourceFile;
                //System.Windows.Controls.Image  _image = new System.Windows.Controls.Image();
                //bi.BeginInit();
                //bi.UriSource = new System.Uri(sourceFile);
                //bi.EndInit();
                //_image.Source = bi;
                //ImgFotoUsuario.Source = _image.Source;
                ImgFotoUsuario.Source = ToImageSource(dlg.FileName);
                ImgFotoUsuario.Visibility = Visibility.Visible;
                tblkClippingRectangle.Visibility = Visibility.Visible;
            }

        }
        public System.Windows.Media.Imaging.BitmapImage ToImageSource(string path)
        {
            System.Windows.Media.Imaging.BitmapImage _bitmap = new System.Windows.Media.Imaging.BitmapImage();
            _bitmap.BeginInit();
            _bitmap.UriSource = new Uri(path);
            _bitmap.EndInit();
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
                    ImgFotoUsuario.Source = c._imagenfinal.Source;
                ImgFotoUsuario.Visibility = Visibility.Visible;
                //}

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
        //private void btnSeleccionFoto_Click(object sender, RoutedEventArgs e)
        //{
        //    AddCropToElement(ImgFotoUsuario);
        //    RefreshCropImage();
        //    BtnAfirmarElegirFoto.Visibility = Visibility.Visible;
        //    BtnCancelarElegirFoto.Visibility = Visibility.Visible;

        //}

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
            GridMenu.IsEnabled = true;
            //-imgCrop.Visibility = Visibility.Visible;
            btnRotacionMenos90.Visibility = Visibility.Hidden;
            btnRotacion90.Visibility = Visibility.Hidden;
            BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
            BtnCancelarElegirFoto.Visibility = Visibility.Hidden;
            //RecSombraSeleccionadora.Visibility = Visibility.Hidden;
            ImgFotoUsuario.Visibility = Visibility.Collapsed;
            tblkClippingRectangle.Visibility = Visibility.Collapsed;
            //btnSeleccionFoto.Visibility = Visibility.Hidden;
            btnCargarFoto.Visibility = Visibility.Hidden;
            btnCapturarFoto.Visibility = Visibility.Hidden;
            btnRegresarEscogerFoto.Visibility = Visibility.Visible;
        }

        private void btnRegresarEscogerFoto_Click(object sender, RoutedEventArgs e)
        {
            GridMenu.IsEnabled = false;
            //-imgCrop.Visibility = Visibility.Hidden;
            btnRotacionMenos90.Visibility = Visibility.Visible;
            btnRotacion90.Visibility = Visibility.Visible;
            BtnAfirmarElegirFoto.Visibility = Visibility.Visible;
            BtnCancelarElegirFoto.Visibility = Visibility.Visible;
            //RecSombraSeleccionadora.Visibility = Visibility.Visible;
            ImgFotoUsuario.Visibility = Visibility.Visible;
            tblkClippingRectangle.Visibility = Visibility.Visible;
            //btnSeleccionFoto.Visibility = Visibility.Visible;
            btnCargarFoto.Visibility = Visibility.Visible;
            btnCapturarFoto.Visibility = Visibility.Visible;
            btnRegresarEscogerFoto.Visibility = Visibility.Hidden;
        }

       



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
        // rotacion

        //double LeftC = rc.Left;
        //double RightC = rc.Right;
        //double TopC=rc.Top;
        //double BottomC=rc.Bottom;


        //WinLoaded();
        //X = YC;
        //Y = XC;
        //var bi = ImgFotoUsuario.Source as BitmapImage;
        //bi.BeginInit();
        //bi.Rotation = Rotation.Rotate90;
        //bi.EndInit();
        //set image source
        //ImgFotoUsuario.Source = bi;




        //string j = new Uri( @"pack://application:,,").ToString();

        //BitmapImage bi = new BitmapImage(bit.UriSource);
        //BitmapImage properties must be in a BeginInit/EndInit block
        //BitmapImage _bit = new BitmapImage(bi.UriSource);
        //_bit = bi;
        //bi.CreateOptions = BitmapCreateOptions.IgnoreImageCache;
        //bi.CacheOption = BitmapCacheOption.OnLoad;
        //bi.StreamSource.Position = 0;


        //string DirectorioImage = path + "Assets\UserImage\"";
        //_bit.UriSource = new Uri(path+ "/Assets/UserImage/"+ ImageName);
        //Set image rotation

        //EliminarImagenUsuario();
        //bi.DownloadCompleted += ImageDownloadCompleted;
        //bi=_bit;
        //private static byte[] ReadImageMemory()
        //{
        //    //ImageSource img = ImgFotoUsuario.Source;
        //    //BitmapSource bmp = (BitmapSource)img;


        //    BitmapSource bitmapSource = BitmapConversion.ToBitmapSource(Clipboard.GetImage());
        //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //    MemoryStream memoryStream = new MemoryStream();
        //    encoder.Frames.Add(BitmapFrame.Create(bitmapSource));
        //    encoder.Save(memoryStream);
        //    return memoryStream.GetBuffer();
        //}

        //public BitmapImage ImageFromBuffer(Byte[] bytes)
        //{
        //    MemoryStream stream = new MemoryStream(bytes);
        //    BitmapImage image = new BitmapImage();
        //    image.BeginInit();
        //    image.StreamSource = stream;
        //    image.EndInit();
        //    return image;
        //}
        //public BitmapImage Convert(Image img)
        //{
        //    using (var memory = new MemoryStream())
        //    {

        //        img.Save(memory, ImageFormat.Png);
        //        memory.Position = 0;

        //        var bitmapImage = new BitmapImage();
        //        bitmapImage.BeginInit();
        //        bitmapImage.StreamSource = memory;
        //        bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
        //        bitmapImage.EndInit();

        //        return bitmapImage;
        //    }
        //}

        //private void ImageDownloadCompleted(object sender,EventArgs e)
        //{
        //    JpegBitmapEncoder encoder = new JpegBitmapEncoder();
        //    Guid photoID = System.Guid.NewGuid();
        //    String photolocation = photoID.ToString() + ".jpg";  //file name 

        //    encoder.Frames.Add(BitmapFrame.Create((BitmapImage)sender));

        //    using (var filestream = new FileStream(photolocation, FileMode.Create))
        //        encoder.Save(filestream);
        //}
        //void EliminarImagenUsuario()
        //{
        //    List<string> strFiles = Directory.GetFiles(@"..\..\Assets\UserImage\", "*", SearchOption.AllDirectories).ToList();

        //    foreach (string fichero in strFiles)
        //    {
        //        File.Delete(fichero);
        //    }
        //}
        #endregion Rotacion

    }
}
