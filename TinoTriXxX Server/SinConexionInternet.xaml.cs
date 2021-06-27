using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TinoTriXxX.Vista
{
    /// <summary>
    /// Lógica de interacción para SinConexionInternet.xaml
    /// </summary>
    /// 

    internal enum AccentState
    {
        ACCENT_DISABLED = 0,
        ACCENT_ENABLE_GRADIENT = 1,
        ACCENT_ENABLE_TRANSPARENTGRADIENT = 2,
        ACCENT_ENABLE_BLURBEHIND = 3,
        ACCENT_INVALID_STATE = 4
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct AccentPolicy
    {
        public AccentState AccentState;
        public int AccentFlags;
        public int GradientColor;
        public int AnimationId;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct WindowCompositionAttributeData
    {
        public WindowCompositionAttribute Attribute;
        public IntPtr Data;
        public int SizeOfData;
    }

    internal enum WindowCompositionAttribute
    {
        // ...
        WCA_ACCENT_POLICY = 19
        // ...
    }
    public partial class SinConexionInternet : Window
    {
        [DllImport("user32.dll")]
        internal static extern int SetWindowCompositionAttribute(IntPtr hwnd, ref WindowCompositionAttributeData data);
        public int xClick = 0, yClick = 0;
        public SinConexionInternet()
        {
            InitializeComponent();
            //WindowBlur.SetIsEnabled(this, true);
            MouseDown += Window_MouseDown;
        }
        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                DragMove();
        }
        private void AplicarEfecto(Window g, int NivelDegradado)
        {
            var objBlur = new System.Windows.Media.Effects.BlurEffect();
            objBlur.Radius = NivelDegradado;
            g.Effect = objBlur;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            EnableBlur();
        }
      
        private void BtnProbarConexion_Click(object sender, RoutedEventArgs e)
        {
            System.Net.WebRequest Peticion = default(System.Net.WebRequest);
            var Respuesta = default(System.Net.WebResponse);
            try {

                Peticion = System.Net.WebRequest.Create("http://tinotrix.gearhostpreview.com/vista/Login.aspx");
                Respuesta = Peticion.GetResponse();
                this.Close();
            }
            catch (FileNotFoundException r)
            {
                
            }
            catch (DirectoryNotFoundException r)
            {
               
            }
            catch (IOException r)
            {
                
            }
            catch (Exception r)
            {

            }
        }
        private void BtnCerrarApp_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
        private void BtnMinizarApp_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void BtnProbarConexion_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                //Mouse.OverrideCursor = Cursors.Hand;
                DragMove();
            }
        }

        internal void EnableBlur()
        {
            var windowHelper = new WindowInteropHelper(this);

            var accent = new AccentPolicy();
            accent.AccentState = AccentState.ACCENT_ENABLE_BLURBEHIND;

            var accentStructSize = Marshal.SizeOf(accent);

            var accentPtr = Marshal.AllocHGlobal(accentStructSize);
            Marshal.StructureToPtr(accent, accentPtr, false);

            var data = new WindowCompositionAttributeData();
            data.Attribute = WindowCompositionAttribute.WCA_ACCENT_POLICY;
            data.SizeOfData = accentStructSize;
            data.Data = accentPtr;

            SetWindowCompositionAttribute(windowHelper.Handle, ref data);

            Marshal.FreeHGlobal(accentPtr);
        }
    }
}
