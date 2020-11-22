namespace OperationsBetweenForests.Core
{
    public class Edge
    {

        public string Father { get; set; }
        public string Child { get; set; }


        public Edge()
        {
            Father = null;
            Child = null;
        }

        public Edge(string father, string child) : this()
        {
            Father = father;
            Child = child;
        }

        public Edge(Node father, Node child) : this()
        {
            if(child is null)
            {
                Father = father.Value;
                Child = null;
            }
            else
            {
                Father = father.Value;
                Child = child.Value;
            }
        }

       /* public Edge(string father, Node child) : this()
        {
            Father = father;
            Child = child.Value;
        }

        public Edge(Node father, string child) : this()
        {
            Father = father.Value;
            Child = child;
        }*/
    }
}
