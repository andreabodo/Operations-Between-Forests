using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core2
{
    class Tree : Operations
    {
        private string Name { get; }
        private List<Tree> Children { get; }

        public Tree(string name, List<Tree> children)
        {
            Name = name;
            Children = children;
        }

        public Tree(string name, params Tree[] children) : this(name, children.ToList()) { }

        //Nodo singolo
        public Tree(string name) : this(name, new List<Tree>()) { }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder(Name);
            foreach (Tree child in Children)
            {
                str.Append(" --> (").Append(child.ToString()).Append(")");
            }
            return str.ToString();
        }

        public List<Tree> GetTrees()
        {
            return Children;
        }

        public Operations Add(Operations other)
        {
            List<Tree> trees = new List<Tree>();
            trees.Add(this);
            trees.AddRange(other.GetTrees());
            return new Forest(trees);
        }

        public Operations Product(Operations other)
        {
            if (other is Tree)
            {
                return Product((Tree)other);
            }
            else
            {
                return Product((Forest)other);
            }
        }

        public Operations Product(Tree other)
        {
            Console.WriteLine("Tree-Tree " + this + " and " + other);
            if (Children.Count == 0)//nodo me
            {
                List<Tree> children = new List<Tree>();
                foreach (Tree child in other.Children)
                {
                    children.Add(new Tree(Name + child.Name));
                }
                return new Tree(this.Name + other.Name, children);
            }
            else if (other.Children.Count == 0)//nodo altro
            {
                List<Tree> children = new List<Tree>();
                foreach (Tree child in Children)
                {
                    children.Add(new Tree(child.Name + other.Name));
                }
                return new Tree(this.Name + other.Name, children);
            }
            else
            {
                Operations result = new Forest();
                Console.WriteLine("Product a_ e b " + Children.Count + " " + other.Children.Count + " " + result.GetTrees().Count);
                result = result.Add(removeRoot().Product(other));
                Console.WriteLine("Product a_ e b_ " + Children.Count + " " + other.Children.Count + " " + result.GetTrees().Count);
                result = result.Add(removeRoot().Product(other.removeRoot()));
                Console.WriteLine("Product a e b_ " + Children.Count + " " + other.Children.Count + " " + result.GetTrees().Count);
                result = result.Add(this.Product(other.removeRoot()));
                Console.WriteLine("Product " + this.Children.Count + " " + other.Children.Count + " " + result.GetTrees().Count);
                return new Tree(Name + other.Name, result.GetTrees());
            }
        }

        public Operations Product(Forest other)
        {
            Console.WriteLine("Tree-Forest " + Name + " " + other.GetType());
            Operations result = new Forest();
            foreach (Tree tree in other.Trees)
            {
                result = result.Add(this.Product(tree));
            }
            return result;
        }

        public Operations removeRoot()
        {
            if (Children.Count == 1)
            {
                return Children.First();
            }
            else
            {
                return new Forest(Children);
            }
        }
    }
}
