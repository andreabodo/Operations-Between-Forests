using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class Node
    {
        private bool isRoot;
        private Node parent;
        private int level;
        public String Value { get; set; }
        public List<Node> Children { get; set; }

        public Node(String value, Node parent = null)
        {
            this.Value = value;
            this.parent = parent;
        }

        internal void RemoveParent()
        {
            parent = null;
        }
    }
}
