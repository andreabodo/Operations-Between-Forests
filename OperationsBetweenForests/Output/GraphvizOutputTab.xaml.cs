using DotNetGraph;
using OperationsBetweenForests.Core;
using OperationsBetweenForests.DOT;
using OperationsBetweenForests.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace OperationsBetweenForests.Output
{
    /// <summary>
    /// Logica di interazione per GraphvizOutputTab.xaml
    /// </summary>
    public partial class GraphvizOutputTab : UserControl
    {


        public string ImageFilePath
        {
            get { return (string)GetValue(ImageFilePathProperty); }
            set { SetValue(ImageFilePathProperty, value); }
        }
        public static readonly DependencyProperty ImageFilePathProperty =
            DependencyProperty.Register("ImageFilePath", typeof(string), typeof(GraphvizOutputTab), new PropertyMetadata(""));

       /* private static void OnImageFilePathChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GraphvizOutputTab outTab = (GraphvizOutputTab)d;
            BitmapImage btmpimg = new BitmapImage();
            btmpimg.BeginInit();
            btmpimg.UriSource = new Uri((string) e.NewValue, UriKind.Relative);
            btmpimg.EndInit();
            outTab.GraphImage.Source = btmpimg;
        }*/

        public GraphvizOutputTab()
        {
            InitializeComponent();
            ImageFilePath = "";
        }

        private void ShowGraphButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.Forests.Count > 0)
            {
                String selected = GraphListComboBox.SelectedItem.ToString();//estrazione foresta da foreste in memoria
                DotGraph graph = DOTCompiler.ToDotGraph(MainWindow.Forests[selected]);
                String fileName = graph.Identifier;
                FileManager.SaveDotFile(fileName, DOTCompiler.DotCompile(graph));
                DOTEngine.Run(@"DOTGraphs/" + fileName + ".dot");
                if (File.Exists(@"DOTGraphs/" + fileName + ".dot.png"))
                {
                    BitmapImage btpimg = new BitmapImage();
                    btpimg.BeginInit();
                    btpimg.UriSource = new Uri(System.IO.Path.GetFullPath(@"DOTGraphs/" + fileName + ".dot.png"), UriKind.Absolute);
                    btpimg.EndInit();
                    GraphImage.Source = btpimg;
                }
            }
        }

        private void ReloadButton_Click(object sender, RoutedEventArgs e)
        {
            this.GraphListComboBox.ItemsSource = new ObservableCollection<string>(MainWindow.Forests.Keys);//TODO N.B. richiede una ObservableCollection se no non aggiorna la dimensione di drop down
        }

        private void LoadButton_Click(object sender, RoutedEventArgs e)
        {
            Forest f = (Forest)FileManager.DeserializeFromJsonFile();
            if (!(f is null))//se l'utente decide di annullare il caricamento
            {
                MainWindow.Forests.Add(f.Name, f);
                ReloadButton_Click(this, new RoutedEventArgs(MouseUpEvent));
            }
        }
    }
}
