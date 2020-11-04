using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using Microsoft.Win32;
using OperationsBetweenForests.Models;
using OperationsBetweenForests.Serialization;
using QuickGraph;
using QuickGraph.Algorithms.MaximumFlow;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using OperationsBetweenForests.Input;
using OperationsBetweenForests.Core;

namespace OperationsBetweenForests.Output
{
    /// <summary>
    /// Logica di interazione per GraphOutputTab.xaml
    /// </summary>
    public partial class GraphOutputTab : UserControl
    {
        public GraphOutputTab()
        {
            InitializeComponent();
            ZoomControl.SetViewFinderVisibility(zoomCtrl, Visibility.Visible);
        }

        public void LoadGraph(MyGraph graph)
        {
            var LogicCore = new MyGXLogicCore(); //   C'ERANO PROBLEMI PERCHè IL LOGIC CORE COME NELLA DOC NON PRENDEVA IL GRAFO!!!
            //TODO capire come visualizzare il grafo da loading anzichè da creazione via codice. Il problema sembra essere sempre il fatto che
            //il logic core richiede un grafo
            LogicCore.Graph = graph;//TODO provare altrimenti a ricostruire il grafo ogni volta che viene caricato prendendo le strutture da quelle
            //caricate in graphArea

            //This property sets layout algorithm that will be used to calculate vertices positions
            //Different algorithms uses different values and some of them uses edge Weight property.
            LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
            //Now we can set optional parameters using AlgorithmFactory
            //NOTE: default parameters can be automatically created each time you change Default algorithms
            LogicCore.DefaultLayoutAlgorithmParams =
                               LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.Tree);
            //Unfortunately to change algo parameters you need to specify params type which is different for every algorithm.
            ((SimpleTreeLayoutParameters)LogicCore.DefaultLayoutAlgorithmParams).Direction = LayoutDirection.TopToBottom;

            //This property sets vertex overlap removal algorithm.
            //Such algorithms help to arrange vertices in the layout so no one overlaps each other.
            LogicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            //Setup optional params
            LogicCore.DefaultOverlapRemovalAlgorithmParams =
                              LogicCore.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 300;

            //This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
            //For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
            LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            //This property sets async algorithms computation so methods like: Area.RelayoutGraph() and Area.GenerateGraph()
            //will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
            //Area.RelayoutFinished and Area.GenerateGraphFinished.
            LogicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to GraphArea object
            graphArea.LogicCore = LogicCore;

            //Generate graph
            graphArea.GenerateGraph(true);
            graphArea.ShowAllEdgesLabels(false);
            graphArea.ShowAllEdgesArrows(false);
            graphArea.SetVerticesMathShape(VertexShape.Circle);
        }

        //TODO caricare l'albero struttura dati e creare l'albero visualizzazione
        /*private void ShowGraphButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Generate graph clicked begin");
            MyGraph graph = FileManager.DeserializeFromJsonFile();
            Console.WriteLine(graph.ToString());
            Console.WriteLine("Vertici?" + graph.Vertices.Count());
            var LogicCore = new MyGXLogicCore(); //   C'ERANO PROBLEMI PERCHè IL LOGIC CORE COME NELLA DOC NON PRENDEVA IL GRAFO!!!
            //TODO capire come visualizzare il grafo da loading anzichè da creazione via codice. Il problema sembra essere sempre il fatto che
            //il logic core richiede un grafo
            LogicCore.Graph = graph;//TODO provare altrimenti a ricostruire il grafo ogni volta che viene caricato prendendo le strutture da quelle
            //caricate in graphArea

            //This property sets layout algorithm that will be used to calculate vertices positions
            //Different algorithms uses different values and some of them uses edge Weight property.
            LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.Tree;
            //Now we can set optional parameters using AlgorithmFactory
            //NOTE: default parameters can be automatically created each time you change Default algorithms
            LogicCore.DefaultLayoutAlgorithmParams =
                               LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.Tree);
            //Unfortunately to change algo parameters you need to specify params type which is different for every algorithm.
            ((SimpleTreeLayoutParameters)LogicCore.DefaultLayoutAlgorithmParams).Direction = LayoutDirection.TopToBottom;

            //This property sets vertex overlap removal algorithm.
            //Such algorithms help to arrange vertices in the layout so no one overlaps each other.
            LogicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            //Setup optional params
            LogicCore.DefaultOverlapRemovalAlgorithmParams =
                              LogicCore.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 300;

            //This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
            //For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
            LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            //This property sets async algorithms computation so methods like: Area.RelayoutGraph() and Area.GenerateGraph()
            //will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
            //Area.RelayoutFinished and Area.GenerateGraphFinished.
            LogicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to GraphArea object
            graphArea.LogicCore = LogicCore;

            //Generate graph
            graphArea.GenerateGraph(true);
            graphArea.ShowAllEdgesLabels(true);
            graphArea.ShowAllEdgesArrows(true);
            graphArea.SetVerticesMathShape(VertexShape.Circle);

            Console.WriteLine("Generate graph clicked end");
        }*/

        //TODO test
        public MyGraph DemoGraph()
        {
            //Random Rand = new Random();

            //Create data graph object
            MyGraph graph = new MyGraph();
            
            //Create and add vertices using some DataSource for ID's
            int[] DataSource = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (var item in DataSource)
            {
                var ver = new DataVertex() { ID = item, Text = item.ToString() + "Mario" };
                graph.AddVertex(ver);
                Console.WriteLine(ver.ID + " " + ver.Text);
            }
            var vlist = graph.Vertices.ToList();

            graph.AddEdge(new DataEdge(vlist[0], vlist[2]));
            graph.AddEdge(new DataEdge(vlist[0], vlist[1]));
            graph.AddEdge(new DataEdge(vlist[3], vlist[5]));
            graph.AddEdge(new DataEdge(vlist[10], vlist[11]));
            graph.AddEdge(new DataEdge(vlist[11], vlist[12]));
            graph.AddEdge(new DataEdge(vlist[6], vlist[0]));
            graph.AddEdge(new DataEdge(vlist[6], vlist[10]));
            DataEdge a = new DataEdge(vlist[0], vlist[2]);
            
            return graph;
        }

        private void ShowGraphButton_Click(object sender, RoutedEventArgs e)
        {
            if (MainWindow.Forests.Count > 0)
            {
                Forest f = MainWindow.Forests.First().Value;
                MyGraph graph = new MyGraph();
                Dictionary<String, DataVertex> existingNodes = new Dictionary<String, DataVertex>(); //struttura dati di appoggio per evitare i nodi duplicati
                foreach(Edge sourceEdge in f.EdgeList)
                {
                    if (!(existingNodes.ContainsKey(sourceEdge.Father.Value)))//padre non esiste
                    {
                        DataVertex fath = new DataVertex(sourceEdge.Father.Value);
                        graph.AddVertex(fath);
                        existingNodes.Add(fath.Text, fath);
                        if (!(existingNodes.ContainsKey(sourceEdge.Child.Value)))
                        {
                            DataVertex chil = new DataVertex(sourceEdge.Child.Value);
                            graph.AddVertex(chil);
                            existingNodes.Add(chil.Text, chil);
                            graph.AddEdge(new DataEdge(fath, chil));
                        }
                        else
                        {
                            DataVertex chil = new DataVertex();
                            existingNodes.TryGetValue(sourceEdge.Child.Value, out chil);
                            graph.AddEdge(new DataEdge(fath, chil));
                        }
                    }
                    else//padre esiste
                    {
                        DataVertex fath = new DataVertex();
                        existingNodes.TryGetValue(sourceEdge.Father.Value, out fath);
                        if (!(existingNodes.ContainsKey(sourceEdge.Child.Value)))
                        {
                            DataVertex chil = new DataVertex(sourceEdge.Child.Value);
                            graph.AddVertex(chil);
                            existingNodes.Add(chil.Text, chil);
                            graph.AddEdge(new DataEdge(fath, chil));
                        }
                        else
                        {
                            DataVertex chil = new DataVertex();
                            existingNodes.TryGetValue(sourceEdge.Child.Value, out chil);
                            graph.AddEdge(new DataEdge(fath, chil));
                        }
                    }
                }
                LoadGraph(graph);
            }
        }
    }
}
