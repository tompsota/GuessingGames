﻿#pragma checksum "..\..\ChoosePlaySet.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "58075EF995F15BA2A38BEBD04788F7C558ABF986414AB51DEE817753EC135CDE"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using GuessingGame;
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


namespace GuessingGame {
    
    
    /// <summary>
    /// ChoosePlaySet
    /// </summary>
    public partial class ChoosePlaySet : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 23 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock PlayerWelcomeText;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock LeaderboardText;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock ChooseMessage;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PicSetComboBox;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OkButton;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid PlayersLeaderboardGrid;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\ChoosePlaySet.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ClearDBButton;
        
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
            System.Uri resourceLocater = new System.Uri("/GuessingGame;component/chooseplayset.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChoosePlaySet.xaml"
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
            this.PlayerWelcomeText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.LeaderboardText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.ChooseMessage = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.PicSetComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 5:
            this.OkButton = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\ChoosePlaySet.xaml"
            this.OkButton.Click += new System.Windows.RoutedEventHandler(this.OkButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.PlayersLeaderboardGrid = ((System.Windows.Controls.DataGrid)(target));
            
            #line 31 "..\..\ChoosePlaySet.xaml"
            this.PlayersLeaderboardGrid.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.DataGrid_LoadingRow);
            
            #line default
            #line hidden
            
            #line 31 "..\..\ChoosePlaySet.xaml"
            this.PlayersLeaderboardGrid.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.DataGrid_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ClearDBButton = ((System.Windows.Controls.Button)(target));
            
            #line 32 "..\..\ChoosePlaySet.xaml"
            this.ClearDBButton.Click += new System.Windows.RoutedEventHandler(this.ClearDBButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
