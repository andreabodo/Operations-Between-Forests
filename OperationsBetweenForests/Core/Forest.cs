using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    public class Forest
    {

        public String Name { get; set; }
        public List<Node> Roots { get; set; }//radici
        public Dictionary<string, Node> ForestNodesMap { get; set; }
        public List<Edge> EdgeList { get; set; }
        public int NodeCount { get; set; }

        #region Constructors
        public Forest()
        {
            Name = null;
            Roots = new List<Node>();
            ForestNodesMap = new Dictionary<String, Node>();
            EdgeList = new List<Edge>();
            NodeCount = 0;
        }

        public Forest(List<Node> inputRoots) : this() 
        {
            List<Node> nodeList = new List<Node>();
            foreach(Node root in inputRoots)
            {
                if(root.Children.Count == 0)
                {
                    Node treeRoot = new Node(root.Value);
                    Roots.Add(treeRoot);
                    ForestNodesMap.Add(treeRoot.Value, treeRoot);
                    EdgeList.Add(new Edge(treeRoot, null));
                }
                else
                {
                    nodeList.Add(root);
                }
            }
            while (nodeList.Count > 0)
            {
                Node node = nodeList.First();
                Node treeNode = new Node(node.Value);
                if(node.Parent is null)
                {
                    Roots.Add(treeNode);
                }
                else
                {
                    Edge e = new Edge(node.Parent, treeNode);
                    EdgeList.Add(e);
                    Node fatherNode = ForestNodesMap[node.Parent.Value];
                    fatherNode.Children.Add(treeNode);
                    treeNode.Parent = fatherNode;
                }
                List<Node> childrenList = node.Children;
                ForestNodesMap.Add(treeNode.Value, treeNode);
                nodeList.AddRange(childrenList);
                nodeList.RemoveAt(0);
            }
            NodeCount = ForestNodesMap.Count;
        }

        public Forest(HashSet<Edge> edges) : this()
        {
            EdgeList = edges.ToList();
            NodeCount = ForestNodesMap.Count;
        }

        public Forest(string name, List<Edge> edges, List<Node> roots, Dictionary<string, Node> nodes) : this()
        {
            Name = name;
            EdgeList = edges.ToList();
            Roots = roots;
            ForestNodesMap = nodes;
            NodeCount = ForestNodesMap.Count;
        }
        

        public Forest(Node root) : this()
        {
            List<Node> nodeList = new List<Node>();
            if (root.Children.Count == 0)
            {
                Node treeRoot = new Node(root.Value);
                Roots.Add(treeRoot);
                ForestNodesMap.Add(treeRoot.Value, treeRoot);
            }
            else
            {
                nodeList.Add(root);
            }
            while (nodeList.Any())
            {
                Node node = nodeList.First();
                Node treeNode = new Node(node.Value);
                if(node.Parent is null)
                {
                    Roots.Add(treeNode);
                }
                else
                {
                    Edge e = new Edge(node.Parent, treeNode);
                    EdgeList.Add(e);
                    Node fatherNode = ForestNodesMap[node.Value];
                    fatherNode.Children.Add(treeNode);
                    treeNode.Parent = fatherNode;
                }
                List<Node> childrenList = node.Children;
                ForestNodesMap.Add(treeNode.Value, treeNode);
                nodeList.AddRange(childrenList);
                nodeList.RemoveAt(0);
            }
            NodeCount = ForestNodesMap.Count;
        }
        #endregion

        public bool IsSingleNode()
        {
          if(NodeCount == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public void GeneratesChildrenRelationships()
        {
            Forest result = new Forest(Name, EdgeList, Roots, ForestNodesMap);
            foreach(Edge e in result.EdgeList)
            {
                result.ForestNodesMap[e.Father].Children.Add(result.ForestNodesMap[e.Child]);
            }
            UpdateRoots();
        }

        private void UpdateRoots()
        {
            List<Node> roots = new List<Node>(1);
            foreach (Node n in ForestNodesMap.Values)
            {
                if (n.Parent is null)
                {
                    roots.Add(n);
                }
            }
            Roots = roots;
        }

        [Obsolete]
        public void GeneratesParentRelationships()
        {
            foreach(Edge e in EdgeList)
            {
                ForestNodesMap[e.Child].Parent = ForestNodesMap[e.Father];
            }
        }

        public bool IsSingleTree()
        {
            return Roots.Count == 1;
        }

        internal List<Forest> SubForest()
        {
            List<Forest> subForests = new List<Forest>(Roots.Count);
            foreach(Node t in Roots)
            {
                subForests.Add(new Forest(t));
            }
            return subForests;
        }

        public void DestroyChildrenRelationships()
        {
            Forest result = new Forest(Name, EdgeList, Roots, ForestNodesMap);
            foreach (Edge e in result.EdgeList)
            {
                result.ForestNodesMap[e.Father].Children.Remove(result.ForestNodesMap[e.Child]);
            }
            UpdateRoots();
        }

        internal Forest Add(Forest f2)
        {
            throw new NotImplementedException();
        }

        public static Forest operator +(Forest f1, Forest f2)
        {
            return f1.Add(f2);
        }
    }
}
