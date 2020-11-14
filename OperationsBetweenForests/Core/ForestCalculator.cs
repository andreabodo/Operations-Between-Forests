using OperationsBetweenForests.Models;
using QuickGraph.Collections;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class ForestCalculator
    {

        /*public static Forest ProductRec(Forest a, Forest b)
        {
            //a neutro
            if (a.IsSingleNode()) { return b; }
            //b neutro
            else if (b.IsSingleNode()) { return a; }
            else if (a.IsSingleTree() && b.IsSingleTree())
            {
                return ProductRec(a.RemoveRoot(), b) + ProductRec(a.RemoveRoot(), b.RemoveRoot()) + ProductRec(a, b.RemoveRoot());
            }
            else
            {
                Forest result = new Forest();
                foreach (Forest f in a.SubForest())
                {
                    result += ProductRec(f, b);
                }
                return result;
            }
        }*/

        //Tra parentesi le scelte del codice originale

        public static Forest Product(Forest f, Forest g)
        {
            Forest resultForest = new Forest() { Name = f.Name + "," + g.Name };//TODO inizializzare? (no)
            if (g.IsSingleNode())
            {
                //substitute label with new ones
                BuildLabels(f, g);
                return f;
            }
            else if (f.IsSingleNode())
            {
                //substitute label with new ones
                BuildLabels(f, g);
                return g;
            }
            //multiple trees in F
            else if(!f.IsSingleNode() && !(f is null))
            {
                //get list of trees
                List<Forest> treeList = new List<Forest>();
                foreach(Node root in f.Roots)
                {
                    Forest tree = new Forest(root);
                    treeList.Add(tree);
                }
                //calculate product formula
                List<Forest> resultList = new List<Forest>();
                foreach(Forest tree in treeList)
                {
                    resultList.Add(Product(tree, g));
                }
                //sum the forest in result list
                Forest result = resultList.First();
                resultList.RemoveAt(0);
                while (resultList.Any())
                {
                    result = Sum(result, resultList.First());
                    resultList.RemoveAt(0);
                }
                return result;
            }
            //multiple trees in g (same as previous but for g)
            else if (!g.IsSingleNode() && !(g is null))
            {
                //get list of trees
                List<Forest> treeList = new List<Forest>();
                foreach(Node root in g.Roots)
                {
                    Forest tree = new Forest(root);
                    treeList.Add(tree);
                }
                //calculate product formula
                List<Forest> resultList = new List<Forest>();
                foreach(Forest tree in treeList)
                {
                    resultList.Add(Product(tree, f));
                }
                //sum the forest in output list
                Forest result = resultList.First();
                resultList.RemoveAt(0);
                while (resultList.Any())
                {
                    result = Sum(result, resultList.First());
                    resultList.RemoveAt(0);
                }
                return result;
            }
            //calculate F and G trees product
            Forest f_Bottomless = Bottomless(f);
            Forest g_Bottomless = Bottomless(g);
            List<Forest> outputList = new List<Forest>();
            //F_ x G
            outputList.Add(Product(f, g_Bottomless));
            //F x G
            outputList.Add(Product(f_Bottomless, g_Bottomless));
            //F x G_
            outputList.Add(Product(f_Bottomless, g));
            //sum partial result
            Forest output = outputList.First();
            outputList.RemoveAt(0);
            while (outputList.Any())
            {
                output = Sum(output, outputList.First());
                outputList.RemoveAt(0);
            }
            //add the bottom
            String f_RootLabel = f.Roots.First().Value;
            String g_RootLabel = g.Roots.First().Value;
            String rootLabel = "(" + f_RootLabel + "," + g_RootLabel + ")";
            resultForest = Bottom(output, rootLabel);
            PolishProductEdgeList(resultForest.EdgeList);
            return resultForest;
        }

       /* public static Forest Product2(Forest f, Forest g)
        {
            List<Node> firstInputRootsList = new List<Node>(f.Roots);
            Forest firstInputForest = new Forest(firstInputRootsList);
            List<Node> secondInputRootsList = new List<Node>(g.Roots);
            //Forest secondInputForest

        }*/

        private static Forest Bottom(Forest output, string rootLabel)
        {
            Node n = new Node(rootLabel);
            n.Children.AddRange(output.Roots);
            output.Roots.Clear();
            output.Roots.Add(n);
            List<Edge> newEdges = new List<Edge>();
            foreach(Node c in n.Children)
            {
                newEdges.Add(new Edge(n, c));
            }
            output.ForestNodesMap.Add(n.Value, n);
            return output;
        }

        /*
        /// <summary>
        /// Add a common root
        /// </summary>
        /// <param name="f"></param>
        /// <returns></returns>
        private static Forest Bottom2(Forest f)
        {
            Node root = f.Roots.First();
            //update edge list
            List<Edge> edgeList = new List<Edge>();
            foreach(Node oldRoot in f.Roots)
            {
                //set new node as parent
                oldRoot.Parent = root;
                //add child to new root
                root.Children.Add(oldRoot);
                Edge e = new Edge(root, oldRoot);
            }
        }*/

        private static Forest Bottomless(Forest f)
        {
            Node n = f.Roots.First();
            f.Roots.Remove(n);
            f.Roots.AddRange(n.Children);
            List<Edge> edgeList = f.EdgeList;
            foreach(Edge e in edgeList)
            {
                if (e.Father.Equals(n))
                {
                    e.Child = null;
                }
            }
            f.ForestNodesMap.Remove(n.Value);
            return f;
        }

        private static Forest Sum(Forest f, Forest g)
        {
            Forest output = new Forest(f.Roots);
            Forest secondForest = new Forest(g.Roots);
            foreach(Node node in secondForest.ForestNodesMap.Values)
            {
                String validData = GetValidData(node.Value, output.ForestNodesMap);

                //add node to the ForestNodesMap
                if (validData.CompareTo(node.Value) != 0)
                {
                    //check the presence of a node with same data in second forest
                    validData = GetValidData(validData, secondForest.ForestNodesMap);
                    String oldData = node.Value;
                    //update g root list
                    if(node.Parent is null)
                    {
                        foreach(Node root in secondForest.Roots)
                        {
                            if (root.Value.CompareTo(oldData) == 0)
                            {
                                root.Value = validData;
                            }
                        }
                    }

                    //update g edge list
                    foreach(Edge e in secondForest.EdgeList)
                    {
                        if (e.Father.Value.CompareTo(oldData) == 0)
                        {
                            e.Father = secondForest.ForestNodesMap[validData];//TODO fine funzione somma
                        }
                        else if(e.Child!=null && e.Child.Value.CompareTo(oldData)==0)
                        {
                            e.Child = secondForest.ForestNodesMap[validData];
                        }
                    }
                    node.Value = validData;
                }
                output.ForestNodesMap.Add(validData, node);
            }
            //merge edge lists
            output.EdgeList.AddRange(secondForest.EdgeList);
            //merge root lists
            output.Roots.AddRange(secondForest.Roots);
            //update node count
            output.NodeCount = f.NodeCount + g.NodeCount;
            return output;
        }

        private static string GetValidData(string value, Dictionary<string, Node> forestNodesMap)
        {
            int count = 1;
            Boolean isDataValid = false;
            String tempData = value;
            String rootData = value;
            while (!isDataValid)
            {
                if (forestNodesMap.ContainsKey(tempData))
                {
                    tempData = rootData + count;
                    count++;
                }
                else
                {
                    isDataValid = true;
                }
            }
            return tempData;
        }

        private static void PolishProductEdgeList(List<Edge> edgeList)
        {
            foreach(Edge e in edgeList)
            {
                if(e.Child == null)
                {
                    edgeList.Remove(e);
                }
            }
        }

        private static void BuildLabels(Forest f, Forest g)
        {
            List<Node> nodesToBeParsed = new List<Node>();
            if (g.IsSingleNode())
            {
                #region case g single node
                Dictionary<String, Node> firstForestNodesMap = f.ForestNodesMap;
                //get g label
                String g_Label = g.Roots.First().Value;
                //modify f labels and f nodemap key
                nodesToBeParsed.AddRange(f.Roots);
                while (nodesToBeParsed.Any())
                {
                    Node n = nodesToBeParsed.First();

                    //get old n label
                    String oldLabel = n.Value;
                    //update n label
                    String newLabel = "(" + oldLabel + ", " + g_Label + ")";
                    n.Value = newLabel;
                    //update nodemap
                    firstForestNodesMap.Remove(oldLabel);
                    firstForestNodesMap.Add(n.Value, n);
                    //update edges
                    foreach (Edge e in f.EdgeList)
                    {
                        if (e.Child != null)
                        {
                            if (e.Child.Value.CompareTo(oldLabel) == 0)
                            {
                                e.Child.Value = newLabel;
                            }
                        }
                        if (e.Father.Value.CompareTo(oldLabel) == 0)
                        {
                            e.Father.Value = newLabel;
                        }
                    }
                    //update nodesToBeParsed list
                    nodesToBeParsed.AddRange(n.Children);
                    nodesToBeParsed.Remove(n);
                }
                #endregion
            }
            else if (f.IsSingleNode())
            {
                #region case f single node
                Dictionary<String, Node> secondForestNodesMap = g.ForestNodesMap;
                //get f label
                String f_Label = f.Roots.First().Value;
                //modify g labels and g node map key
                nodesToBeParsed.AddRange(g.Roots);
                while (nodesToBeParsed.Any())
                {
                    Node n = nodesToBeParsed.First();
                    //get old n label
                    String oldLabel = n.Value;
                    //update n label
                    String newLabel = "(" + oldLabel + ',' + f_Label + ")";
                    n.Value = newLabel;
                    //update node map
                    secondForestNodesMap.Remove(oldLabel);
                    secondForestNodesMap.Add(newLabel, n);
                    //update edges
                    foreach(Edge e in g.EdgeList)
                    {
                        if(!(e.Child is null))
                        {
                            if(e.Child.Value.CompareTo(oldLabel) == 0)
                            {
                                e.Child.Value = newLabel;
                            }
                        }
                        if(e.Father.Value.CompareTo(oldLabel) == 0)
                        {
                            e.Father.Value = newLabel;
                        }
                    }
                    //update nodesToBeParsed list
                    nodesToBeParsed.AddRange(n.Children);
                    nodesToBeParsed.Remove(n);

                }
                #endregion
            }
        }
    }
}
