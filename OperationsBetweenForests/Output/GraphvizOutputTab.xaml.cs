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

        //Zoom values
        double scale = 1.0;
        readonly double minScale = 0.5;
        readonly double maxScale = 3.0;

        public GraphvizOutputTab()
        {
            InitializeComponent();
        }

        private void Image_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            // back to normal (maybe this isn't needed since we're making a new one below anyway)
            GraphImage.RenderTransform = null;

            var position = e.MouseDevice.GetPosition(GraphImage);

            if (e.Delta > 0)
                scale += 0.1;
            else
                scale -= 0.1;

            if (scale > maxScale)
                scale = maxScale;
            if (scale < minScale)
                scale = minScale;

            GraphImage.RenderTransform = new ScaleTransform(scale, scale, position.X, position.Y);


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
