using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    public class Node
    {
        private Node parent;
        public String Value { get; set; }
        public List<Node> Children { get; set; }

        public Node()
        {
            Value = null;
            Children = null;
        }

        public Node(String value, Node parent = null)
        {
            this.Value = value;
            this.parent = parent;
            Children = new List<Node>();
        }

        internal void RemoveParent()
        {
            parent = null;
        }
    }
}
