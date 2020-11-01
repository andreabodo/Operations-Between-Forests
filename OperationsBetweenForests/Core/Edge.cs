using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    public class Edge
    {

        public Node Father { get; set; }
        public Node Child { get; set; }


        public Edge()
        {
            Father = new Node();
            Child = new Node();
        }

        public Edge(Node father, Node child) : this()
        {
            Father = father;
            Child = child;
        }
    }
}
