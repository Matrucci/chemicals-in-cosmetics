﻿#pragma checksum "..\..\client-products.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "CE6CE558A3327BEC9E9D7DEF0B8477C295F669FBFC24FA11232A5696F6A1F88B"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Chemicals_and_cosmetics;
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


namespace Chemicals_and_cosmetics {
    
    
    /// <summary>
    /// client_products
    /// </summary>
    public partial class client_products : System.Windows.Controls.Page, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\client-products.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox primary_categoty_cb;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\client-products.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox sub_category_cb;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\client-products.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox chemical_lb;
        
        #line default
        #line hidden
        
        
        #line 77 "..\..\client-products.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button findProductsBtn;
        
        #line default
        #line hidden
        
        
        #line 78 "..\..\client-products.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button backToMenu;
        
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
            System.Uri resourceLocater = new System.Uri("/Chemicals and cosmetics;component/client-products.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\client-products.xaml"
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
            this.primary_categoty_cb = ((System.Windows.Controls.ComboBox)(target));
            
            #line 25 "..\..\client-products.xaml"
            this.primary_categoty_cb.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.primary_categoty_cb_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.sub_category_cb = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 3:
            this.chemical_lb = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.findProductsBtn = ((System.Windows.Controls.Button)(target));
            
            #line 77 "..\..\client-products.xaml"
            this.findProductsBtn.Click += new System.Windows.RoutedEventHandler(this.findProductsBtn_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.backToMenu = ((System.Windows.Controls.Button)(target));
            
            #line 78 "..\..\client-products.xaml"
            this.backToMenu.Click += new System.Windows.RoutedEventHandler(this.backToMenu_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

