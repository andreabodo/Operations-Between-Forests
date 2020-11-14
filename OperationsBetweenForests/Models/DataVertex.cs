using GraphX.Common.Models;
using GraphX.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Models
{
    public class DataVertex : VertexBase
    {
        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        public DataVertex() : this(string.Empty)
        {
        }

        public DataVertex(string text = "")
        {
            Text = text;
        }
    }
}
