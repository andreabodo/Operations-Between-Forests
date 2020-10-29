using Microsoft.Win32;
using OperationsBetweenForests.Models;
using OperationsBetweenForests.Serialization;
using System;
using System.Collections.Generic;

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

namespace OperationsBetweenForests.Input
{
    /// <summary>
    /// Logica di interazione per GraphInputTab.xaml
    /// </summary>
    public partial class GraphInputTab : UserControl
    {
        int Index { get; set; }

        public GraphInputTab()
        {
            InitializeComponent();
            Index = 0;
        }

        private void ResetIndex()
        {
            Index = 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //Creazione text boxes
            TextBox txt1 = new TextBox();
            txt1.Text = "Nodo";
            txt1.Name = "NodeTextBox" + (Index);
            TextBox txt2 = new TextBox();
            txt2.Text = "Figli";
            txt1.Name = "ChildrenTextBox" + (Index);
            //Creazione griglia
            Grid grid = new Grid();
            grid.Name = "Couple" + Index;
            grid.Height = 55;
            grid.Width = 437;
            grid.Margin = new Thickness(0, 0, 0, 2);
            grid.Children.Add(txt1);
            grid.Children.Add(txt2);
            var thickness = new Thickness(0, 0, 317, 32);
            txt1.Margin = (thickness);
            var thickness2 = new Thickness(55, 28, 10, 4);
            txt2.Margin = (thickness2);
            //Aggiunta alla listview
            InputListView.Items.Insert(Index++, grid);
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            if (InputListView.Items.Count > 1)
            {
                InputListView.Items.RemoveAt(Index - 1);
                Index--;
            }
        }

        /// <summary>
        /// Generates graph and save it in a JSON file.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GenerateGraphButton_Click(object sender, RoutedEventArgs e)
        {
            //ListView to Dictionary
            MyGraph graph = new MyGraph(GraphNameTextBox.Text.Trim());
            ItemCollection itemList = InputListView.Items; //prendo gli oggetti textbox contenuti nella listview
            Dictionary<String, String[]> graphDictionary = new Dictionary<string, string[]>();
            foreach (var element in itemList)
            {
                if (element.GetType() == typeof(Grid))
                {
                    Grid currentElement = (Grid)element; //se è una grid allora contiene le due text box padre e figli
                    UIElementCollection gridChildrenList = currentElement.Children;
                    if (gridChildrenList.Count == 2)
                    {
                        TextBox node = (TextBox)gridChildrenList[0];
                        TextBox children = (TextBox)gridChildrenList[1];
                        String[] childrenArray = children.Text.Split(',');
                        try
                        {
                            graphDictionary.Add(node.Text, childrenArray);
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Il grafo contiene due nodi con lo stesso nome nel campo \"padre\"", "Errore");
                        }
                    }
                }
                    //Graph creation: key node - values[] children nodes
                    foreach (String key in graphDictionary.Keys)
                    {
                        DataVertex node = new DataVertex(key);
                        graph.AddVertex(node);
                        String[] childrens = graphDictionary[key];
                        foreach (String children in childrens)
                        {
                            DataVertex childrenNode = new DataVertex(children);
                            graph.AddVertex(childrenNode);
                            DataEdge edge = new DataEdge(node, childrenNode);
                            graph.AddEdge(edge);
                        }
                    }
            }
            //Generate area to use data in serialization
            MyGXLogicCore logicCore = LogicCoreTreeProvider.DefaultTreeLogicCore(graph);
            MyGraphArea area = new MyGraphArea() { LogicCore = logicCore };
            area.GenerateGraph(graph, true);
            //Save graph
            /* SaveFileDialog saveD = new SaveFileDialog() { Filter = "TreeFile | *.xml", Title = "Seleziona il nome del file", FileName = "DefaultTree.xml" };
             if (saveD.ShowDialog() == true)
             {
                 FileManager.SerializeDataToFile(saveD.FileName, area.ExtractSerializationData());
             }*/

            FileManager.SaveToJsonFile(graph);
        }

        private void GetInput(object sender, RoutedEventArgs e)
        {

        }

    }
}
