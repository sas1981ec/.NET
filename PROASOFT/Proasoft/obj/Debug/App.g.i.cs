﻿#pragma checksum "..\..\App.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "8565D4D5D3EBD04C54F82AAE2CB277B6EA02AB66"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Proasoft;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
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


namespace Proasoft {
    
    
    /// <summary>
    /// App
    /// </summary>
    public partial class App : System.Windows.Application {
        
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
            
            #line 4 "..\..\App.xaml"
            this.Startup += new System.Windows.StartupEventHandler(this.ApplicationStartup);
            
            #line default
            #line hidden
            
            #line 5 "..\..\App.xaml"
            this.DispatcherUnhandledException += new System.Windows.Threading.DispatcherUnhandledExceptionEventHandler(this.ApplicationDispatcherUnhandledException);
            
            #line default
            #line hidden
            
            #line 5 "..\..\App.xaml"
            this.StartupUri = new System.Uri("/Views/VistaAutenticacion.xaml", System.UriKind.Relative);
            
            #line default
            #line hidden
            System.Uri resourceLocater = new System.Uri("/Proasoft;component/app.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\App.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        /// <summary>
        /// Application Entry Point.
        /// </summary>
        [System.STAThreadAttribute()]
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public static void Main() {
            Proasoft.App app = new Proasoft.App();
            app.InitializeComponent();
            app.Run();
        }
    }
}
