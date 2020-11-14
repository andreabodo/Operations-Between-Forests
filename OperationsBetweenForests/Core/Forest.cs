using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    public class Forest
    {

        public String Name { get; set; }
        public List<Node> Roots { get; set; }//radici
        public Dictionary<string, Node> ForestNodesMap { get; set; }
        public List<Edge> EdgeList { get; set; }
        public int NodeCount { get; set; }

        public Forest()
        {
            Roots = new List<Node>();
            ForestNodesMap = new Dictionary<String, Node>();
            EdgeList = new List<Edge>();
        }

        public Forest(List<Node> roots) : this() 
        {
            Roots = roots;
        }

        public Forest(HashSet<Edge> edges) : this()
        {
            EdgeList = edges.ToList();
        }

        public Forest(string name, HashSet<Edge> edges, List<Node> roots, Dictionary<string, Node> nodes) : this()
        {
            Name = name;
            EdgeList = edges.ToList();
            Roots = roots;
            ForestNodesMap = nodes;
        }
        

        public Forest(params Node[] node) : this()
        {
            foreach(Node n in node)
            {
                Roots.Add(n);
            }
        }

        internal bool IsSingleNode()
        {
            if(Roots.Count == 1)
            {
                Node n = Roots.First();
                if(n.Children.Count == 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        internal void GeneratesRelationships()
        {
            foreach(Edge e in EdgeList)
            {
                e.Father.Children.Add(e.Child);
                e.Child.Parent = e.Father;
            }
        }

        internal bool IsSingleTree()
        {
            return Roots.Count == 1;
        }

        internal List<Forest> SubForest()
        {
            List<Forest> subForests = new List<Forest>(Roots.Count);
            foreach(Node t in Roots)
            {
                subForests.Add(new Forest(t));
            }
            return subForests;
        }

        internal Forest Add(Forest f2)
        {
            throw new NotImplementedException();
        }

        public static Forest operator +(Forest f1, Forest f2)
        {
            return f1.Add(f2);
        }
    }
}
