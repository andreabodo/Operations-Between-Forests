using System;
using System.Collections.Generic;

namespace OperationsBetweenForests.Core
{
    public class Node
    {
        public Node Parent { get; set; }
        public String Value { get; set; }
        public List<Node> Children { get; set; }

        public Node()
        {
            Value = null;
            Children = null;
        }

        public Node(String value, Node parent = null) : this()
        {
            this.Value = value;
            this.Parent = parent;
            Children = new List<Node>();
        }

        public void RemoveParent()
        {
            Parent = null;
        }

        public override bool Equals(object obj)
        {
            if(obj is Node)
            {
                return this.Value.Equals(((Node)obj).Value);
            }
            else
            {
                return base.Equals(obj);
            }
        }

        //? public boolean isParent(Node node) ?
    }
}
