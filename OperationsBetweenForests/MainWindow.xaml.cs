using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using Microsoft.Win32;
using OperationsBetweenForests.Core;
using OperationsBetweenForests.Models;
using OperationsBetweenForests.Output;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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
