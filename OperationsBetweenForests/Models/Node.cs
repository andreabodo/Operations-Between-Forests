using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Models
{
    class Node
    {
       public String padre { get; set; }
       public String value { get; set; }
       public List<Node> children { get; set; }

    public List<Node> hoSonno()
        {
            List<Node> nodiFigli = new List<Node>();
            if (children != null && children.Count != 0)
            { 
                foreach (Node n in children)
                {
                    nodiFigli.AddRange(n.hoSonno());
                }
                return nodiFigli;
            }
            else
            {
                return nodiFigli;
            }
        }

    }
}
