using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetGraph;
using DotNetGraph.Edge;
using DotNetGraph.Extensions;
using DotNetGraph.Node;
using OperationsBetweenForests.Core;

namespace OperationsBetweenForests.DOT
{
    public static class DOTCompiler
    {

        /// <summary>
        /// Converts Core.Forest representation to DotNetCore.Graph representation.
        /// </summary>
        /// <param name="f"> The forest to convert.</param>
        /// <returns>DotNetCore format graph.</returns>
        public static DotGraph ToDotGraph(Forest f)
        {
            DotGraph graph = new DotGraph(f.Name, false);
            Dictionary<String, DotNode> existingNodes = new Dictionary<string, DotNode>();//dict di supporto
            foreach (Edge sourceEdge in f.EdgeList)
            {
                if (!(existingNodes.ContainsKey(sourceEdge.Father)))//padre non esiste ancora
                {
                    DotNode fath = new DotNode(sourceEdge.Father)
                    {
                        Shape = DotNodeShape.Ellipse,
                        Label = sourceEdge.Father,
                        FillColor = Color.LightGray,
                        FontColor = Color.Black,
                        Style = DotNodeStyle.Solid,
                        Height = 0.5f
                    };
                    graph.Elements.Add(fath);//aggiunta al grafo
                    existingNodes.Add(fath.Identifier, fath);//aggiornamento dict supporto
                    if (sourceEdge.Child != null && !(existingNodes.ContainsKey(sourceEdge.Child)))//figlio non esiste ancora
                    {
                        DotNode chil = new DotNode(sourceEdge.Child)
                        {
                            Shape = DotNodeShape.Ellipse,
                            Label = sourceEdge.Child,
                            FillColor = Color.LightGray,
                            FontColor = Color.Black,
                            Style = DotNodeStyle.Solid,
                            Height = 0.5f
                        };
                        graph.Elements.Add(chil);//aggiunta al grafo
                        existingNodes.Add(chil.Identifier, chil);//aggiornamento dict supporto
                        DotEdge edge = new DotEdge(fath, chil)
                        {
                            ArrowHead = DotEdgeArrowType.None,
                            ArrowTail = DotEdgeArrowType.None,
                            Color = Color.Black,
                            FontColor = Color.Black,
                            Label = "",
                            Style = DotEdgeStyle.Solid,
                            PenWidth = 1.0f
                        };
                        graph.Elements.Add(edge);
                    }
                    else //figlio esiste
                    {
                        DotEdge edge = new DotEdge(fath, existingNodes[sourceEdge.Child])
                        {
                            ArrowHead = DotEdgeArrowType.None,
                            ArrowTail = DotEdgeArrowType.None,
                            Color = Color.Black,
                            FontColor = Color.Black,
                            Label = "",
                            Style = DotEdgeStyle.Solid,
                            PenWidth = 1.0f
                        };
                        graph.Elements.Add(edge);
                    }
                }
                else//padre esiste
                {
                    if(sourceEdge.Child != null && !(existingNodes.ContainsKey(sourceEdge.Child)))//figlio non esiste ancora
                    {
                        DotNode chil = new DotNode(sourceEdge.Child)
                        {
                            Shape = DotNodeShape.Ellipse,
                            Label = sourceEdge.Child,
                            FillColor = Color.LightGray,
                            FontColor = Color.Black,
                            Style = DotNodeStyle.Solid,
                            Height = 0.5f
                        };
                        graph.Elements.Add(chil);//aggiunta al grafo
                        existingNodes.Add(chil.Identifier, chil);//aggiornamento dict supporto
                        DotEdge edge = new DotEdge(existingNodes[sourceEdge.Father], chil)
                        {
                            ArrowHead = DotEdgeArrowType.None,
                            ArrowTail = DotEdgeArrowType.None,
                            Color = Color.Black,
                            FontColor = Color.Black,
                            Label = "",
                            Style = DotEdgeStyle.Solid,
                            PenWidth = 1.0f
                        };
                        graph.Elements.Add(edge);
                    }
                    else
                    {
                        DotEdge edge = new DotEdge(existingNodes[sourceEdge.Father], existingNodes[sourceEdge.Child])
                        {
                            ArrowHead = DotEdgeArrowType.None,
                            ArrowTail = DotEdgeArrowType.None,
                            Color = Color.Black,
                            FontColor = Color.Black,
                            Label = "",
                            Style = DotEdgeStyle.Solid,
                            PenWidth = 1.0f
                        };
                        graph.Elements.Add(edge);
                    }
                }
            }
            return graph;
        }

        /// <summary>
        /// Compile to DOT format.
        /// </summary>
        /// <param name="graph"></param>
        /// <returns>DOT string to use as DOT file content.</returns>
        public static String DotCompile(DotGraph graph)
        {
            return graph.Compile();
        }


    }
}
