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

        internal bool IsSingleTree()
        {
            return Roots.Count == 1;
        }

        internal Node[] GetAllRoots()
        {
            throw new NotImplementedException();
        }

        internal List<Node> getRootList()
        {
            throw new NotImplementedException();
        }

        internal List<Forest> SubForest()
        {
            List<Forest> subForests = new List<Forest>(trees.Count);
            foreach(Tree t in trees)
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
