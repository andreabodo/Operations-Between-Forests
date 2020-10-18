using QuickGraph;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;

namespace OperationsBetweenForests.Models
{
    public class MyGraph : BidirectionalGraph<DataVertex, DataEdge>
    {

        //TODO My extension
        private String Name { get; set; }

        public MyGraph() : base()
        {
        }

        //TODO My extension
        public MyGraph(String name): base()
        {
            Name = name;
        }

        public override string ToString()
        {
            var vertices = this.Vertices;
            String s = this.Name + ":" + "\n";
            s += "Vertices: \n";
            foreach(var v in vertices)
            {
                s += v.ID + "\n";
            }
            s += "Edges: \n";
            var edges = this.Edges;
            foreach (var e in edges)
            {
                s += e.ID + "\n";
            }
            return s;
        }
    }
}
