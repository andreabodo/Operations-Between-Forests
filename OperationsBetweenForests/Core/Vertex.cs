using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class Vertex
    {
        private bool isRoot;
        private Node father;
        private int level;
        private String value;
        private List<Vertex> children;
    }
}
