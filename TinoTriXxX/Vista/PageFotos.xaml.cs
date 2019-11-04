
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
using System.Drawing.Imaging;
using ImageMagick;
using System.Threading;
using System.Windows.Threading;

namespace TinoTriXxX
{
    /// <summary>
    /// Lógica de interacción para PageFotos.xaml
    /// </summary>
    public partial class PageFotos : Page
    {
        #region Propiedades
        System.Windows.Point? lastCenterPositionOnTarget;
        System.Windows.Point? lastMousePositionOnTarget;
        System.Windows.Point? lastDragPoint;
        CroppingAdorner _clp;
        FrameworkElement _felCur = null;
        System.Windows.Media.Brush _brOriginal;
        VM_Escritorio VM;
        string path;
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
        double DouTmPuntoExt;//con  esto especifica el tamaño de los cuadritos en el que se extiende o se achica la imagen
        //int IntZoom;
        int CropVH;
        int IntRotation;
        Foto foto = null;
        string sourceFileOriginal = null;
        String filePathElegidaImprimir = null;
        bool ProcesoComienza;
        string StrFotoOriginalPath;
        //System.Drawing.Image imagenfinal = null;
        #region PilaImagenesDescargadas
             List<string> LFDesFotosOrdenada;
        int FotoPMax;
        int FotoPA;
        #endregion PilaImagenesDescargadas
        #endregion Propiedades

        #region Constructor
       
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
            //scrollViewer.MouseMove += OnMouseMove;

            slider.ValueChanged += OnSliderValueChanged;

            //Cropimage();
            CropVH = 0;
            IntRotation = 0;
            ObtenerDirectorioRaiz();
            VM.FotosDescargadas=this.btnDescargarFoto;
            
            String Directorio = path + "\\Imagenes\\usuario\\";
            string[] filePathsdescarga = Directory.GetFiles(Directorio, "*FotoFinalDescarga_*.*", SearchOption.AllDirectories);
            btnDescargarFoto.Badge= filePathsdescarga.Count();
            LFDesFotosOrdenada = new List<string>();
            FotoPMax = -1;
            FotoPA = -1;
        }
        void ObtenerDirectorioRaiz()
            {
                path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName);
                if (!Directory.Exists(path + "\\Imagenes\\usuario\\"))
                {
                    path = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                   // MessageBox.Show("sTRING PATH CHANGE");
                }

            }
       #endregion Constructor
      
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
                    "Area de recorte: ({0:N1}, {1:N1}, {2:N1}, {3:N1})",
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
            _clp = new CroppingAdorner(fel, rcInterior, DouTmPuntoExt);
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
            ChkFotoOvalada.IsChecked = false;
            UbicarMarcoSobreImagen();
            //if (ChkFotoOvalada.IsChecked == true)
            //{
            //    FotoOvalada();
            //}
        }
        private void ImgFotoUsuario_MouseMove(object sender, MouseEventArgs e)
        {
            
            //Point position = Mouse.GetPosition(ImgFotoUsuario);
            //txthandposition.Text = "Ubicacion mouse X: " + position.X + ", Y: " + position.Y;
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                ChkFotoOvalada.IsChecked = false;
                //Mouse.OverrideCursor = Cursors.Hand;
                UbicarMarcoSobreImagen();
                //if (ChkFotoOvalada.IsChecked == true)
                //{
                //    FotoOvalada();
                //}
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
                DouTmPuntoExt = (int)AltoRecorte * 0.05;
                UbicarMarcoSobreImagen();
                _clp.IntHandle = 0;
                IntCropChanged = 0;
                //if (ChkFotoOvalada.IsChecked == true)
                //{
                //    FotoOvalada();
                //}
                
               
            }

            //if (e.LeftButton == MouseButtonState.Pressed)
            //{
            //    if (ChkFotoOvalada.IsChecked==true) {
            //        FotoOvalada();
            //    }
            //}
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
            //BtnCancelarElegirFoto.Visibility = Visibility.Visible;
            imgCrop.Visibility = Visibility.Visible;
        }
        public static System.Drawing.Image CropToCircle(System.Drawing.Image srcImage, System.Drawing.Color backGround)
        {
                System.Drawing.Image dstImage = new Bitmap(srcImage.Width, srcImage.Height, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                Graphics g = Graphics.FromImage(dstImage);
                using (System.Drawing.Brush br = new SolidBrush(backGround)) {
                    g.FillRectangle(br, 0, 0, dstImage.Width, dstImage.Height);
                }
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(0, 0, dstImage.Width, dstImage.Height);
                g.SetClip(path);
                g.DrawImage(srcImage, 0, 0);

                return dstImage;
         }
        private void ChkFotoOvalada_Checked(object sender, RoutedEventArgs e)
        {
            FotoOvalada();
        }
        void FotoOvalada()
        {
            String Directorio = path + "\\Imagenes\\usuario\\"; //"C:\\Users\\Iudex\\Documents\\TinoTrix\\Clone\\Tinotrix\\TinoTriXxX"
                                                                // C: \Users\Iudex\Documents\TinoTrix\Clone\Tinotrix\TinoTriXxX\Imagenes\usuario
            string[] filePaths = Directory.GetFiles(Directorio, "*FotoOvaladaUsuario_*.png", SearchOption.AllDirectories);
            try {
                foreach (string archivo in filePaths)
                {
                    File.Delete(archivo);
                }
            }
            catch (Exception d)
            {
            }
            string filePathElegida = "";
            // if (filePaths.Count() == 0)
            // {
            //     String Archivo = "FotoPreFinalUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ".png";
            //     filePathElegida = Directorio + Archivo;
            //     System.Windows.Controls.Image UserImage = imgCrop;
            //     var encoder = new PngBitmapEncoder();
            //     encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));
            //     using (FileStream stream = new FileStream(filePathElegida, FileMode.Create)) encoder.Save(stream);
            //     filePaths = Directory.GetFiles(Directorio, "*FotoPreFinalUsuario_*.png", SearchOption.AllDirectories);
            // }

            // System.Windows.Media.Imaging.BmpBitmapEncoder bbe = new BmpBitmapEncoder();
            // if (filePaths.Count() == 1)
            // {
            //     filePathElegida = filePaths[0];
            // }
            //// byte[] imageData = DownloadData(filePathElegida);
            // MemoryStream ms = new MemoryStream();
            // bbe.Frames.Add(BitmapFrame.Create((BitmapSource)imgCrop.Source));
            // using (FileStream stream = new FileStream(filePathElegida, FileMode.Create))
            //     ms.CopyTo(stream);
            //     //bbe.Frames.Add(BitmapFrame.Create(new Uri(filePathElegida, UriKind.RelativeOrAbsolute)));
            //     bbe.Save(ms);
            String Archivo = "FotoOvaladaUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss-fff") + ".png";
            filePathElegida = Directorio + Archivo;
            System.Windows.Controls.Image UserImage = imgCrop;
            var encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));
            using (FileStream stream = new FileStream(filePathElegida, FileMode.Create))
            encoder.Save(stream);
            filePaths = Directory.GetFiles(Directorio, "*FotoOvaladaUsuario_*.png", SearchOption.AllDirectories);

            MemoryStream ms = new MemoryStream();
            System.Windows.Media.Imaging.BmpBitmapEncoder bbe = new BmpBitmapEncoder();
            bbe.Frames.Add(BitmapFrame.Create(new Uri(filePathElegida, UriKind.RelativeOrAbsolute)));
            bbe.Save(ms);

            System.Drawing.Image img2 = System.Drawing.Image.FromStream(ms);
            img2 = CropToCircle(img2, System.Drawing.Color.Transparent);
            imgCrop.Source = ConvertImageControl(img2);
        }
        public static System.Windows.Media.ImageSource ConvertImageControl(System.Drawing.Image image)
        {
            try
            {
                if (image != null)
                {
                    var bitmap = new System.Windows.Media.Imaging.BitmapImage();
                    bitmap.BeginInit();
                    System.IO.MemoryStream memoryStream = new System.IO.MemoryStream();
                    image.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Png);
                    memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
                    bitmap.StreamSource = memoryStream;
                    bitmap.EndInit();
                    return bitmap;
                }
            }
            catch { }
            return null;
        }
        private void ChkFotoOvalada_Unchecked(object sender, RoutedEventArgs e)
        {
            AddCropToElement(ImgFotoUsuario);
            RefreshCropImage();
            BtnAfirmarElegirFoto.Visibility = Visibility.Visible;
            //BtnCancelarElegirFoto.Visibility = Visibility.Visible;
            imgCrop.Visibility = Visibility.Visible;
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

            btnAnterior.Visibility = Visibility.Hidden;
            btnSiguiente.Visibility = Visibility.Hidden;

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
                btnDescargarFoto.Visibility = Visibility.Visible;
                BFotoUsuario.Visibility = Visibility.Visible;
                ImgFondoEditor1.Visibility = Visibility.Visible;
                ImgFondoEditor2.Visibility = Visibility.Visible;
                BimgCrop.Visibility = Visibility.Visible;
                //ImgFondoEditor3.Visibility = Visibility.Visible;
                ChkFotoOvalada.Visibility = Visibility.Visible;
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
                ////GridMenu.Visibility = Visibility.Visible;

                //-imgCrop.Visibility = Visibility.Collapsed;
                //RecSombraSeleccionadora.Visibility = Visibility.Collapsed;
                //btnRotacionMenos90.Visibility = Visibility.Visible;
                //btnRotacion90.Visibility = Visibility.Visible;
            }
        }
        private void btnRegresarListaFotos_Click(object sender, RoutedEventArgs e)
        {
            cargarfotos();
            btnAnterior.Visibility = Visibility.Hidden;
            btnSiguiente.Visibility = Visibility.Hidden;

            LbFotos.Visibility = Visibility.Visible;
            btnRecargarListaFotos.Visibility = Visibility.Visible;
            lbTituloFotografias.Visibility = Visibility.Visible;

            btnCapturarFoto.Visibility = Visibility.Hidden;
            btnCargarFoto.Visibility = Visibility.Hidden;
            btnDescargarFoto.Visibility = Visibility.Hidden;
            GFotoSeleccionada.Visibility = Visibility.Hidden;
            btnRegresarListaFotos.Visibility = Visibility.Hidden;
            BFotoUsuario.Visibility = Visibility.Hidden;
            ImgFondoEditor1.Visibility = Visibility.Hidden;
            ImgFondoEditor2.Visibility = Visibility.Hidden;
            BimgCrop.Visibility = Visibility.Hidden;
            ChkFotoOvalada.Visibility = Visibility.Hidden;
            // SvFotoUsuario.Visibility = Visibility.Hidden;
            //ImgFotoUsuario.Source = null;
            lbNombreFotoUsuario.Visibility = Visibility.Hidden;
            lbNombreFotoUsuario.Text = "";
            tblkClippingRectangle.Visibility = Visibility.Hidden;
            imgCrop.Visibility = Visibility.Hidden;
            btnRotacionMenos90.Visibility = Visibility.Hidden;
            btnRotacion90.Visibility = Visibility.Hidden;

            BtnCancelarElegirFoto.Visibility = Visibility.Hidden;
            BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
            //btnSeleccionFoto.Visibility = Visibility.Hidden;
            ChkFotoOvalada.IsEnabled = false;
            ////GridMenu.Visibility = Visibility.Hidden;

            // GridMenu.IsEnabled = false;
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
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
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
                lbNombreFotoUsuario.Text = sourceFileOriginal;
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
                StrFotoOriginalPath = ArchivoDescargado;
                ImgFotoUsuario.Source = ToImageSource(ArchivoDescargado);
                ImgFotoUsuario.Visibility = Visibility.Visible;
                tblkClippingRectangle.Visibility = Visibility.Visible;
                btnRotacionMenos90.Visibility = Visibility.Visible;
                btnRotacion90.Visibility = Visibility.Visible;
                btnAnterior.Visibility = Visibility.Hidden;
                btnSiguiente.Visibility = Visibility.Hidden;
                ChkFotoOvalada.IsEnabled = true;
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
                    btnAnterior.Visibility = Visibility.Hidden;
                    btnSiguiente.Visibility = Visibility.Hidden;
                    ChkFotoOvalada.IsEnabled = true;
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
            btnDescargarFoto.Visibility = Visibility.Visible;
            BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
            BtnCancelarElegirFoto.Visibility = Visibility.Hidden;
            //imgCrop.Visibility = Visibility.Hidden;
            //GridMenu.IsEnabled = false;
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
        private async void BtnAfirmarElegirFoto_Click(object sender, RoutedEventArgs e)
        {
            if (VM.TurnoServidorAbierto(VM.Sucursal.UidSucursal) == true)
            {

                //CancellationTokenSource tokenSource = new CancellationTokenSource();
                //Dispatcher.Invoke(DispatcherPriority.Send, new ThreadStart(delegate { ShowLoadingText(tokenSource.Token); }));
                //////await Task.Run(() => reportPreview.ShowDialog());
                ////await this.Dispatcher.BeginInvoke((Action)(() => { AfimarFotoElegida(); }));

                //this.Dispatcher.Invoke(DispatcherPriority.Normal, new ThreadStart(delegate { AfimarFotoElegida(); }));
                //tokenSource.Cancel();
                AfimarFotoElegida();

               
            }
            else {
                MessageBox.Show("No hay un encargado en turno que le pueda atender");
            }
        }
        private void AfimarFotoElegida() 
        {
            try
            {
                BtnAfirmarElegirFoto.Visibility = Visibility.Hidden;
               //string StrIniciando = string.Empty;
               //for (int i = 0; i < 100000; i++) { StrIniciando += i.ToString(); }
               String Directorio = path + "\\Imagenes\\usuario\\"; //"C:\\Users\\Iudex\\Documents\\TinoTrix\\Clone\\Tinotrix\\TinoTriXxX"
                                                                    // C: \Users\Iudex\Documents\TinoTrix\Clone\Tinotrix\TinoTriXxX\Imagenes\usuario
                //String Extencion = System.IO.Path.GetExtension(sourceFileOriginal);
                String Archivo = "FotoFinalUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ".png";
                filePathElegidaImprimir = Directorio + Archivo;
                System.Windows.Controls.Image UserImage = imgCrop;
                var encoder = new PngBitmapEncoder();
                //"JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                //MessageBox.Show("encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)UserImage.Source));

               // MessageBox.Show(" using (FileStream stream = new FileStream(filePathElegidaImprimir, FileMode.Create)) encoder.Save(stream);", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                using (FileStream stream = new FileStream(filePathElegidaImprimir, FileMode.Create)) encoder.Save(stream);

                       // MessageBox.Show("RedimencionarImagenElegida();", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

           //  RedimencionarImagenElegida();
                        //VInfFotosCliente frm = new VInfFotosCliente(filePathElegidaImprimir, imgCrop, foto, VM.Papel);
                        //frm.ShowDialog();
                       // MessageBox.Show("ReportPreview reportPreview = new ReportPreview(VM, filePathElegidaImprimir, foto, VM.Papel);", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                ReportPreview reportPreview = new ReportPreview(VM, filePathElegidaImprimir, foto);

                //MessageBox.Show("reportPreview.ShowDialog();", "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);

                reportPreview.ShowDialog();
                //FotoFinal ff = new FotoFinal(filePathElegidaImprimir, imgCrop, foto, VM.Papel);
                //ff.ShowDialog();
                //File.Delete(filePathElegidaImprimir);
                BtnAfirmarElegirFoto.Visibility = Visibility.Visible;
            }
            catch (FileNotFoundException e)
            {
                MessageBox.Show("FileNotFoundException: " + e.Message, "Tinotrix: Error informe", MessageBoxButton.OK, MessageBoxImage.Asterisk);
               // MessageBox.Show(e.Message);
                //Application.Current.Shutdown();

            }
            catch (DirectoryNotFoundException e)
            {
                MessageBox.Show("DirectoryNotFoundException: " + e.Message, "Tinotrix: Error informe", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                // Application.Current.Shutdown();
            }
            catch (IOException e)
            {
                MessageBox.Show("IOException: " + e.Message, "Tinotrix: Error informe", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                // Application.Current.Shutdown();
            }
            catch (Exception e)
            {
                MessageBox.Show("Exception: " + e.Message, "Tinotrix: Error informe", MessageBoxButton.OK, MessageBoxImage.Asterisk);
                //Application.Current.Shutdown();
            }
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
        private void BtnDescargarFotografia_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string DirectorioDescarga = path + "\\Imagenes\\usuario\\";
                string[] filePaths = Directory.GetFiles(DirectorioDescarga, "*FotoFinalDescarga_*.png", SearchOption.AllDirectories);


                if (filePaths.Count() >= 1)
                {
                    
                    List<DateTime> ArchivosSoloFechaString = new List<DateTime>();
                    foreach (string archivo in filePaths)
                    {
                        string NombreArchivo = System.IO.Path.GetFileName(archivo);
                        //string ExtencionOriginalArchivo = System.IO.Path.GetExtension(archivo);
                        //Ejemplo: FotoFinalDescarga_ 09-25-2019 13-41-11 =>termina texto 18, comienza fecha comienza 19 y termina 37
                        string resultado = extraerValor(NombreArchivo, "FotoFinalDescarga_ ", ".png");
                        ArchivosSoloFechaString.Add( DateTime.ParseExact(resultado, "MM-dd-yyyy HH-mm-ss", null));
                    }
                    var Listafechaordenada = ArchivosSoloFechaString.OrderByDescending(x => x.DayOfYear).ThenByDescending(x => x.TimeOfDay).ToList();

                    LFDesFotosOrdenada = new List<string>();
                    List<DateTime> ListaFechaOrdenada = Listafechaordenada.ToList();
                    List<string> ListaFotosOrdenada = new List<string>();
                    foreach (DateTime fechita in ListaFechaOrdenada) {
                        ListaFotosOrdenada.Add(fechita.ToString("MM-dd-yyyy HH-mm-ss"));
                    }

                    foreach (string archivoOrdenado in ListaFotosOrdenada)
                    {
                        foreach (string archivo in filePaths)
                        {
                            if (archivo.Contains(archivoOrdenado)) {
                                LFDesFotosOrdenada.Add(archivo);
                            }
                        }
                    }
                    ImgFotoUsuario.Source = ToImageSource(LFDesFotosOrdenada[0]);
                    ImgFotoUsuario.Visibility = Visibility.Visible;
                    tblkClippingRectangle.Visibility = Visibility.Visible;
                    btnRotacionMenos90.Visibility = Visibility.Visible;
                    btnRotacion90.Visibility = Visibility.Visible;
                    ChkFotoOvalada.IsEnabled = true;
                    if (filePaths.Count() > 1)
                    {
                       FotoPMax = filePaths.Count()-1;
                        FotoPA = 0;
                       btnAnterior.Visibility = Visibility.Visible;
                        btnSiguiente.Visibility = Visibility.Hidden;
                    }
                }
                else {
                    MessageBox.Show("No hay ninguna foto para visualizar \n" , " TINOTRIX");
                }
            }
            catch (FileNotFoundException p)
            {
                MessageBox.Show(p.Message);

            }
            catch (DirectoryNotFoundException p)
            {
                MessageBox.Show(p.Message);

            }
            catch (IOException p)
            {
                MessageBox.Show(p.Message);

            }
            catch (Exception p)
            {
                MessageBox.Show(p.Message);

            }
           
        }
        private String extraerValor(String cadena, String stringInicial, String stringFinal)
        {
            int terminaString = cadena.LastIndexOf(stringFinal);
            String nuevoString = cadena.Substring(0, terminaString);
            int offset = stringInicial.Length;
            int iniciaString = nuevoString.LastIndexOf(stringInicial) + offset;
            int cortar = nuevoString.Length - iniciaString;
            nuevoString = nuevoString.Substring(iniciaString, cortar);
            return nuevoString;
        }
        private void BtnAnterior_Click(object sender, RoutedEventArgs e)
        {
            FotoPA = FotoPA + 1;
            ImgFotoUsuario.Source = ToImageSource(LFDesFotosOrdenada[FotoPA]);
            if (FotoPA + 1 > FotoPMax)
            {
                btnAnterior.Visibility = Visibility.Hidden;
            }
            else {
                btnAnterior.Visibility = Visibility.Visible;
            }
            if (FotoPA - 1 == -1)
            {
                btnSiguiente.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSiguiente.Visibility = Visibility.Visible;
            }
        }
        private void BtnSiguiente_Click(object sender, RoutedEventArgs e)
        {
            FotoPA = FotoPA - 1;
            ImgFotoUsuario.Source = ToImageSource(LFDesFotosOrdenada[FotoPA]);
            if (FotoPA + 1 > FotoPMax)
            {
                btnAnterior.Visibility = Visibility.Hidden;
            }
            else
            {
                btnAnterior.Visibility = Visibility.Visible;
            }
            if (FotoPA - 1 == -1)
            {
                btnSiguiente.Visibility = Visibility.Hidden;
            }
            else
            {
                btnSiguiente.Visibility = Visibility.Visible;
            }
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
        
        #endregion Rotacion

        #region papel
        private void cargarPapel()
        {
            VM.CargarPapel(VM.Sucursal.UidSucursal);
            //LbFotos.ItemsSource = VM.ListaFotos;
        }
        #endregion

        #region otras funciones
        private async void ShowLoadingText(CancellationToken token)
        {
            BCargando.Visibility = Visibility.Visible;

            try
            {
                while (!token.IsCancellationRequested)
                {
                    txtLoading.Text = "Cargando";
                    await Task.Delay(500, token);
                    txtLoading.Text = "Cargando.";
                    await Task.Delay(500, token);
                    txtLoading.Text = "Cargando..";
                    await Task.Delay(500, token);
                    txtLoading.Text = "Cargando...";
                    await Task.Delay(500, token);
                }
            }
            catch (TaskCanceledException)
            {
                BCargando.Visibility = Visibility.Hidden;
            }
        }
        private void BtnBrillo_Click(object sender, RoutedEventArgs e)
        {
            //MagickImage image = new MagickImage("filepath.jpg");
            //image.Crop(new MagickGeometry(424, 448, 224, 224));
            //image.Resize(123, 234);
            //image.Write("output.jpg");

            //MagickImage image = new MagickImage(StrFotoOriginalPath);
            //image.
        }

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
       
        #endregion otras funciones


    }
}
