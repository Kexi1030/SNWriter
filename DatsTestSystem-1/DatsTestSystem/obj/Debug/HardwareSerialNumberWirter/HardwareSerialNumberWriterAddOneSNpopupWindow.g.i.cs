﻿#pragma checksum "..\..\..\HardwareSerialNumberWirter\HardwareSerialNumberWriterAddOneSNpopupWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "843BA3BCCC7C1FB864EA3DF045AC7BBCA1BD732B28BC6042EA4A462B1B4D153F"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using DatsTestSystem.HardwareSerialNumberWirter;
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


namespace DatsTestSystem.HardwareSerialNumberWirter {
    
    
    /// <summary>
    /// HardwareSerialNumberWriterAddOneSNpopupWindow
    /// </summary>
    public partial class HardwareSerialNumberWriterAddOneSNpopupWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\HardwareSerialNumberWirter\HardwareSerialNumberWriterAddOneSNpopupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SNInputBox;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\HardwareSerialNumberWirter\HardwareSerialNumberWriterAddOneSNpopupWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button DefineOneSNButton;
        
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
            System.Uri resourceLocater = new System.Uri("/DatsTestSystem;component/hardwareserialnumberwirter/hardwareserialnumberwriterad" +
                    "donesnpopupwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\HardwareSerialNumberWirter\HardwareSerialNumberWriterAddOneSNpopupWindow.xaml"
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
            this.SNInputBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 17 "..\..\..\HardwareSerialNumberWirter\HardwareSerialNumberWriterAddOneSNpopupWindow.xaml"
            this.SNInputBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SNInputBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.DefineOneSNButton = ((System.Windows.Controls.Button)(target));
            
            #line 18 "..\..\..\HardwareSerialNumberWirter\HardwareSerialNumberWriterAddOneSNpopupWindow.xaml"
            this.DefineOneSNButton.Click += new System.Windows.RoutedEventHandler(this.DefineOneSNButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

