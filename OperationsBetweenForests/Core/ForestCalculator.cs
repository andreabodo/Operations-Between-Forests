using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class ForestCalculator
    {

        public static Forest ProductRec(Forest a, Forest b)
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
                foreach (Forest f in a.SubForest()){
                    result += ProductRec(f, b);
                }
                return result;
            }
        }

        public static Forest Product(Forest f, Forest g)
        {
            Forest resulForest;
            if (g.IsSingleNode())
            {
                //substitute label with new ones
                BuildLabels(f, g);
                return f;
            }
        }

        private static void BuildLabels(Forest f, Forest g)
        {
            List<Node> nodesToBeParsed = new List<Node>();
            if (g.IsSingleNode())
            {
                //case g single node
                Dictionary<String, Node> firstForestNodesMap = f.ForestNodesMap;
                //get g label
                String g_Label = g.Roots.First().Value;
                //modify f labels and f nodemap key
                nodesToBeParsed.AddRange(f.Roots);
                while (!(nodesToBeParsed.Count == 0))
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
            }
            else if (f.IsSingleNode()) { }//TODO
        }
    }
}
