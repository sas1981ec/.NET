﻿#pragma checksum "..\..\..\Views\CambioContrasena.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0C8F12B39A42D47A3C5800398351AD1E085A69C7"
//------------------------------------------------------------------------------
// <auto-generated>
//     Este código fue generado por una herramienta.
//     Versión de runtime:4.0.30319.42000
//
//     Los cambios en este archivo podrían causar un comportamiento incorrecto y se perderán si
//     se vuelve a generar el código.
// </auto-generated>
//------------------------------------------------------------------------------

using Proasoft.InfraestructuraVM;
using Proasoft.Views;
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


namespace Proasoft.Views {
    
    
    /// <summary>
    /// CambioContrasena
    /// </summary>
    public partial class CambioContrasena : Proasoft.Views.VentanaBase, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\Views\CambioContrasena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PswContrasenaAntigua;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\Views\CambioContrasena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtContrasenaAntigua;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\Views\CambioContrasena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PswContrasenaNueva;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\Views\CambioContrasena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtContrasenaNueva;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\Views\CambioContrasena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox PswContrasenaNueva2;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\Views\CambioContrasena.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TxtContrasenaNueva2;
        
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
            System.Uri resourceLocater = new System.Uri("/Proasoft;component/views/cambiocontrasena.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\CambioContrasena.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.PswContrasenaAntigua = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 17 "..\..\..\Views\CambioContrasena.xaml"
            this.PswContrasenaAntigua.PasswordChanged += new System.Windows.RoutedEventHandler(this.ActualizarTextoContrasenaAntigua);
            
            #line default
            #line hidden
            return;
            case 2:
            this.TxtContrasenaAntigua = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.PswContrasenaNueva = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 22 "..\..\..\Views\CambioContrasena.xaml"
            this.PswContrasenaNueva.PasswordChanged += new System.Windows.RoutedEventHandler(this.ActualizarTextoContrasenaNueva);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TxtContrasenaNueva = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.PswContrasenaNueva2 = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 27 "..\..\..\Views\CambioContrasena.xaml"
            this.PswContrasenaNueva2.PasswordChanged += new System.Windows.RoutedEventHandler(this.ActualizarTextoContrasenaNueva2);
            
            #line default
            #line hidden
            return;
            case 6:
            this.TxtContrasenaNueva2 = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
