using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class Forest
    {

        private String name { get; set; }
        private List<Tree> trees { get; set; }
        public List<Node> Roots { get; set; }
        public Dictionary<string, Node> ForestNodesMap { get; internal set; }
        public IEnumerable<Edge> EdgeList { get; internal set; }

        public Forest(params Tree[] trees)
        {
            foreach(Tree t in trees)
            {
                this.trees.Add(t);
            }
        }

        internal bool IsSingleNode()
        {
            if(trees.Count==1)
            {
                return trees.First().IsSingleNode();
            }
            else
            {
                return false;
            }
        }

        internal Forest RemoveRoot()
        {
            if(trees.Count == 1)
            {
                trees = trees.First().RemoveRoot();
            }
            return this;
        }

        internal bool IsSingleTree()
        {
            return trees.Count == 1;
        }

        internal Node[] GetAllRoots()
        {
            throw new NotImplementedException();
        }

        internal int getRootList()
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
