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
    /// Lógica de interacción para PageFotosCliente.xaml
    /// </summary>
    public partial class PageFotosCliente : Page
    {
        #region Propiedades
        System.Windows.Point? lastCenterPositionOnTarget;
        System.Windows.Point? lastMousePositionOnTarget;
        System.Windows.Point? lastDragPoint;
        VM_Escritorio VM;
        string path;
        //MyHub hub;
        //public MyHub Hub
        //{
        //    get { return hub; }
        //    set { hub = value; }
        //}
        MainWindow mainserver;
        public MainWindow MainServer {
            get { return mainserver; }
            set { mainserver = value; }
        }
        #endregion Propiedades

        #region Constructor
        public PageFotosCliente(VM_Escritorio vm)
        {
            InitializeComponent();
            VM = vm;
            scrollViewer.ScrollChanged += OnScrollViewerScrollChanged;
            //scrollViewer.MouseLeftButtonUp += OnMouseLeftButtonUp;
            //scrollViewer.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
            scrollViewer.PreviewMouseWheel += OnPreviewMouseWheel;
            //scrollViewer.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
            scrollViewer.MouseMove += OnMouseMove;
            slider.ValueChanged += OnSliderValueChanged;
            //Cropimage();
            ObtenerDirectorioRaiz();
            
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
                slider.Value += 1;

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

        #endregion Constructor

        private void BtnCapturarFoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Camara c = new Camara();
                //AplicarEfecto( this, 5);
                //c.ShowDialog();
                //AplicarEfecto(this, 0);
                Nullable<bool> result = c.ShowDialog();
                //if (result == true) {
                if (c._imagenfinal.Source != null)
                {
                    ImgFotoUsuario.Source = c._imagenfinal.Source;
                    ImgFotoUsuario.Visibility = Visibility.Visible;
                    //}
                    

                    String Directorio = path + "\\Imagenes\\usuario\\";
                    string archivoDescarga = Directorio + "DescargaUsuario_" + DateTime.Now.ToString(" MM-dd-yyyy HH-mm-ss") + ".png";

                    //if (File.Exists(archivoWebCam))
                    //{
                    //    File.SetAttributes(archivoWebCam, FileAttributes.Normal);
                    //    File.Delete(archivoWebCam);//elimina la foto de otras sesiones
                    //}

                    var encoder = new PngBitmapEncoder();
                    //"JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
                    encoder.Frames.Add(BitmapFrame.Create((BitmapSource)ImgFotoUsuario.Source));
                    using (FileStream stream = new FileStream(archivoDescarga, FileMode.Create)) encoder.Save(stream);
                    ImgFotoUsuario.Source = ToImageSource(archivoDescarga);
                    // File.Delete(archivoWebCam);
                }
            }
            catch (Exception et)
            {
                MessageBox.Show("¡No hay ninguna camara disponible! \r\n \r\n" + et.Message, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            AgregarMaquinas();
            BdEnviarMaquinas.IsEnabled=true;
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
        void AgregarMaquinas() {
            try {
                CbMaquinas.Items.Clear();
                for (int i = 1; i < VM.Licencia.IntNoTotal+1; i++)
                {
                    CbMaquinas.Items.Add("Maquina "+i);
                }
               CbMaquinas.Text= CbMaquinas.Items[0].ToString();
            }
            catch (Exception et)
            {
                MessageBox.Show("¡NO se pudo agregar las maquinas a la lista! \r\n \r\n" + et.Message, "Tinotrix", MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
        }

        private void BtnEnviarCliente_Click(object sender, RoutedEventArgs e)
        {
            if (mainserver.Hub != null && mainserver._IntClientesConectados >= 1)
            {
                try
                {
                    //Application.Current.Dispatcher.Invoke(() =>
                    //    ((MainWindow)Application.Current.MainWindow).SendImage(string name, Image message));
                    String Usuer = CbMaquinas.Text;
                    //Convertirmos control.image => Drawing.imagen
                    MemoryStream ms = new MemoryStream();
                    System.Windows.Media.Imaging.BmpBitmapEncoder bbe = new BmpBitmapEncoder();
                    bbe.Frames.Add(BitmapFrame.Create(new Uri(ImgFotoUsuario.Source.ToString(), UriKind.RelativeOrAbsolute)));
                    bbe.Save(ms);
                    //lo convertirmos Drawing.imagen => string y lo enviamos al cliente
                    System.Drawing.Image img2 = System.Drawing.Image.FromStream(ms);
                    try
                    {
                        mainserver.Hub.Send(Usuer, ImageToString(img2));
                    }
                    catch (Exception gh)
                    {
                        MessageBox.Show("No se pudo enviar la foto: \n" + gh.Message, "Aviso de error TINOTRIX");
                    }
                }
                catch (Exception d)
                {
                    MessageBox.Show("No se puede procesar la foto: \n" + d.Message, "Aviso de error TINOTRIX");
                }
            }
            else {
                MessageBox.Show("No hay ninguna maquina conectada" , "Aviso de conexion TINOTRIX");
            }
        }
        public static string ImageToString( System.Drawing.Image image)
        {
            if (image == null)
                return String.Empty;

            var stream = new MemoryStream();
            image.Save(stream, image.RawFormat);
            var bytes = stream.ToArray();

            return Convert.ToBase64String(bytes);
        }
    }
}
