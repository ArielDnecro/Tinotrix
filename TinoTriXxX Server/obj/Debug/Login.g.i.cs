﻿#pragma checksum "..\..\Login.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D3AC77D0F749D78BFCB3349DFAC40B3A6F9539013E9CEB9F26B141D56CDAEFEA"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace TinoTriXxX {
    
    
    /// <summary>
    /// Login
    /// </summary>
    public partial class Login : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 31 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel PnEncargado;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtEncargado;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel PnPassword;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox txtcontrasena;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbAvisoError;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRegresarUsuario;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSiguiente;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEntrar;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\Login.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BtnSalir;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TinoTriXxX;component/login.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Login.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.PnEncargado = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 2:
            this.txtEncargado = ((System.Windows.Controls.TextBox)(target));
            
            #line 32 "..\..\Login.xaml"
            this.txtEncargado.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtEncargado_KeyDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.PnPassword = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 4:
            this.txtcontrasena = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 36 "..\..\Login.xaml"
            this.txtcontrasena.KeyDown += new System.Windows.Input.KeyEventHandler(this.txtcontrasena_KeyDown);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lbAvisoError = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.btnRegresarUsuario = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\Login.xaml"
            this.btnRegresarUsuario.Click += new System.Windows.RoutedEventHandler(this.btnRegresarUsuario_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnSiguiente = ((System.Windows.Controls.Button)(target));
            
            #line 43 "..\..\Login.xaml"
            this.btnSiguiente.Click += new System.Windows.RoutedEventHandler(this.BtnSiguiente_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnEntrar = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\Login.xaml"
            this.btnEntrar.Click += new System.Windows.RoutedEventHandler(this.BtnEntrar_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.BtnSalir = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\Login.xaml"
            this.BtnSalir.Click += new System.Windows.RoutedEventHandler(this.BtnSalir_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

