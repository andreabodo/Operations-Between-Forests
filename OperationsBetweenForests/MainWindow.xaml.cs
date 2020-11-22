using OperationsBetweenForests.Core;
using System;
using System.Collections.Generic;
using System.Windows;

namespace OperationsBetweenForests
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public static Dictionary<String, Forest> Forests { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Forests = new Dictionary<string, Forest>();
            //SetBinding(GraphOutputTab.ForestListProperty, new Binding("Forests"));
        }
    }
}
