using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class Tree
    {

        private String name;
        private Node root;

        public Tree(Node root)
        {
            this.root = root;
        }

        public bool IsSingleNode()
        {
            return root.Children.Count == 0;
        }

        internal List<Tree> RemoveRoot()
        {
            List<Tree> nextTrees = new List<Tree>(root.Children.Count);
            foreach(Node n in root.Children)
            {
                n.RemoveParent();
                nextTrees.Add(new Tree(n));
            }
            return nextTrees;
        }
    }
}
