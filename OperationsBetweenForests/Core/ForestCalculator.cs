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
            String resultName = f.Name + g.Name;
            List<Node> firstInputRoots = new List<Node>(f.Roots);
            Forest firstInputForest = new Forest(f.Name, f.EdgeList, f.Roots, f.ForestNodesMap);
            List<Node> secondInputRoots = new List<Node>(g.Roots);
            Forest secondInputForest = new Forest(g.Name, g.EdgeList, g.Roots, g.ForestNodesMap);
            Forest resultForest = new Forest() { Name = f.Name + "," + g.Name };//TODO inizializzare? (no)
            if (secondInputForest.IsSingleNode())
            {
                //substitute label with new ones
                //BuildLabels(firstInputForest, secondInputForest);
                return firstInputForest;
            }
            else if (firstInputForest.IsSingleNode())
            {
                //substitute label with new ones
               //BuildLabels(firstInputForest, secondInputForest);
                return secondInputForest;
            }
            //multiple trees in f applies distributive property
            else if(firstInputForest.Roots.Count>1 && !(firstInputForest is null))
            {
                //get list of trees
                List<Forest> treeList = new List<Forest>();
                foreach(Node root in firstInputForest.Roots)
                {
                    Forest tree = new Forest(root);
                    treeList.Add(tree);
                }
                //calculate product formula
                List<Forest> resultList = new List<Forest>();
                foreach(Forest tree in treeList)
                {
                    resultList.Add(Product(tree, secondInputForest));
                }
                //sum the forest in result list
                Forest result = resultList.First();
                resultList.RemoveAt(0);
                while (resultList.Any())
                {
                    result = Sum(result, resultList.First());
                    resultList.RemoveAt(0);
                }
                result.Name = resultName;
                return result;
            }
            //multiple trees in g (same as previous but for g)
            else if (secondInputForest.Roots.Count>1 && !(secondInputForest is null))
            {
                //get list of trees
                List<Forest> treeList = new List<Forest>();
                foreach(Node root in secondInputForest.Roots)
                {
                    Forest tree = new Forest(root);
                    treeList.Add(tree);
                }
                //calculate product formula
                List<Forest> resultList = new List<Forest>();
                foreach(Forest tree in treeList)
                {
                    resultList.Add(Product(tree, firstInputForest));
                }
                //sum the forest in output list
                Forest result = resultList.First();
                resultList.RemoveAt(0);
                while (resultList.Any())
                {
                    result = Sum(result, resultList.First());
                    resultList.RemoveAt(0);
                }
                result.Name = resultName;
                return result;
            }
            //if here, calculates F and G trees product: at this point applies forest product formula F_xG_=((F_xG)+(FxG)+(FxG_))_
            Forest f_Bottomless = Bottomless(firstInputForest);
            Forest g_Bottomless = Bottomless(secondInputForest);
            List<Forest> outputList = new List<Forest>();
            //F_ x G
            outputList.Add(Product(firstInputForest, g_Bottomless));
            //F x G
            outputList.Add(Product(f_Bottomless, g_Bottomless));
            //F x G_
            outputList.Add(Product(f_Bottomless, secondInputForest));
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
            resultForest = Bottom(output);
            PolishProductEdgeList(resultForest.EdgeList);
            resultForest.Name = resultName;
            return resultForest;
        }

        private static Forest Bottom(Forest f)
        {
            Forest result = new Forest(f.Name, f.EdgeList, f.Roots, f.ForestNodesMap);
            String rootData = GetValidData("R", f.ForestNodesMap);
            Node root = new Node(rootData);
            //updates edge list
            List<Edge> edgeList = new List<Edge>();
            foreach(Node oldRoot in result.Roots)
            {
                //new node as parent
                oldRoot.Parent = root;
                //add child to new root
                root.Children.Add(oldRoot);
                Edge e = new Edge(root, oldRoot);
                edgeList.Add(e);
            }
            result.EdgeList.AddRange(edgeList);
            //updates roots list
            List<Node> updatedRoots = new List<Node>();
            updatedRoots.Add(root);
            result.Roots = updatedRoots;
            //updates nodes map
            result.ForestNodesMap.Add(root.Value, root);
            //updates nodes count
            int oldNodesCount = f.NodeCount;
            result.NodeCount = result.ForestNodesMap.Count;
            return result;
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
            List<Node> nodesList = new List<Node>(f.Roots);
            foreach(Node root in nodesList)
            {
                f.Roots.Remove(root);
                f.ForestNodesMap.Remove(root.Value);
                foreach(Node child in root.Children)
                {
                    f.Roots.Add(child);
                    child.Parent = null;
                    //Node newRoot = new Node(child.Value);
                    //newRoot.Children.AddRange(child.Children);
                    //nodesList.Add(newRoot);
                }
                f.EdgeList.RemoveAll(x => x.Father == root.Value);
            }
            f.NodeCount = f.ForestNodesMap.Count;
            return f; //new Forest(nodesList);
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
                        if (e.Father.CompareTo(oldData) == 0)
                        {
                            e.Father = validData;
                        }
                        else if (e.Child != null && e.Child.CompareTo(oldData) == 0)
                        {
                            e.Child = validData;
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
                    tempData = rootData + "_" + count;
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
            List<Edge> inputEdgeList = new List<Edge>(edgeList);
            foreach(Edge e in inputEdgeList)
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
                            if (e.Child.CompareTo(oldLabel) == 0)
                            {
                                e.Child = newLabel;
                            }
                        }
                        if (e.Father.CompareTo(oldLabel) == 0)
                        {
                            e.Father = newLabel;
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
                            if(e.Child.CompareTo(oldLabel) == 0)
                            {
                                e.Child = newLabel;
                            }
                        }
                        if(e.Father.CompareTo(oldLabel) == 0)
                        {
                            e.Father = newLabel;
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
