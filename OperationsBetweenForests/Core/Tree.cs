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
        private Dictionary<Node, List<Node>> structure;

        public bool IsSingleNode()
        {
            return root.children.Count == 0;
        }
    }
}
