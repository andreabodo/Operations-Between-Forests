using GraphX.Logic.Models;
using QuickGraph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Models
{
    public class MyGXLogicCore : GXLogicCore<DataVertex, DataEdge, BidirectionalGraph<DataVertex, DataEdge>>
    {
        public MyGXLogicCore() : base() { } //TODO verificare che funzioni

        public MyGXLogicCore(BidirectionalGraph<DataVertex, DataEdge> graph) : base(graph)
        {
        }
    }
}
