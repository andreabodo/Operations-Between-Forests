using GraphX.Common.Enums;
using GraphX.Controls;
using GraphX.Logic.Algorithms.LayoutAlgorithms;
using GraphX.Logic.Algorithms.OverlapRemoval;
using OperationsBetweenForests.Models;
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

        private void ShowGraphButton_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine("Generate graph clicked begin");
            Random Rand = new Random();

            //Create data graph object
            var graph = new MyGraph();

            //Create and add vertices using some DataSource for ID's
            int[] DataSource = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 };
            foreach (var item in DataSource)
            {
                var ver = new DataVertex() { ID = item, Text = item.ToString()};
                graph.AddVertex(ver);
                Console.WriteLine(ver.ID + " " + ver.Text);
            }


            

            var vlist = graph.Vertices.ToList();

            graph.AddEdge(new DataEdge(vlist[0], vlist[2]));
            graph.AddEdge(new DataEdge(vlist[0], vlist[1]));
            graph.AddEdge(new DataEdge(vlist[3], vlist[5]));
            graph.AddEdge(new DataEdge(vlist[10], vlist[11]));

            //Generate random edges for the vertices
            /*foreach (var item in vlist)
            {
                if (Rand.Next(1, 15) < 15) continue;
                var vertex2 = vlist[Rand.Next(0, graph.VertexCount - 1)];
                graph.AddEdge(new DataEdge(item, vertex2, Rand.Next(1, 14))
                { Text = string.Format("{0} -> {1}", item, vertex2) });
            }*/
            var LogicCore = new MyGXLogicCore(graph);
            //This property sets layout algorithm that will be used to calculate vertices positions
            //Different algorithms uses different values and some of them uses edge Weight property.
            LogicCore.DefaultLayoutAlgorithm = LayoutAlgorithmTypeEnum.KK;
            //Now we can set optional parameters using AlgorithmFactory
            //NOTE: default parameters can be automatically created each time you change Default algorithms
            LogicCore.DefaultLayoutAlgorithmParams =
                               LogicCore.AlgorithmFactory.CreateLayoutParameters(LayoutAlgorithmTypeEnum.KK);
            //Unfortunately to change algo parameters you need to specify params type which is different for every algorithm.
            ((KKLayoutParameters)LogicCore.DefaultLayoutAlgorithmParams).MaxIterations = 100;

            //This property sets vertex overlap removal algorithm.
            //Such algorithms help to arrange vertices in the layout so no one overlaps each other.
            LogicCore.DefaultOverlapRemovalAlgorithm = OverlapRemovalAlgorithmTypeEnum.FSA;
            //Setup optional params
            LogicCore.DefaultOverlapRemovalAlgorithmParams =
                              LogicCore.AlgorithmFactory.CreateOverlapRemovalParameters(OverlapRemovalAlgorithmTypeEnum.FSA);
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).HorizontalGap = 50;
            ((OverlapRemovalParameters)LogicCore.DefaultOverlapRemovalAlgorithmParams).VerticalGap = 50;

            //This property sets edge routing algorithm that is used to build route paths according to algorithm logic.
            //For ex., SimpleER algorithm will try to set edge paths around vertices so no edge will intersect any vertex.
            LogicCore.DefaultEdgeRoutingAlgorithm = EdgeRoutingAlgorithmTypeEnum.SimpleER;

            //This property sets async algorithms computation so methods like: Area.RelayoutGraph() and Area.GenerateGraph()
            //will run async with the UI thread. Completion of the specified methods can be catched by corresponding events:
            //Area.RelayoutFinished and Area.GenerateGraphFinished.
            LogicCore.AsyncAlgorithmCompute = false;

            //Finally assign logic core to GraphArea object
            graphArea.LogicCore = LogicCore;
            graphArea.GenerateGraph(true);
           
            Console.WriteLine("Generate graph clicked end");
        }
    }
}
