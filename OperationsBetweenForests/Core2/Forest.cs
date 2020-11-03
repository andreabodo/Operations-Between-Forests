using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core2
{
    class Forest : Operations
    {
        public List<Tree> Trees { get; set; }

        public Forest(List<Tree> trees)
        {
            this.Trees = trees;
        }

        public Forest(params Tree[] trees) : this(trees.ToList()) { }


        public List<Tree> GetTrees()
        {
            return Trees;
        }

        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            foreach (Tree tree in Trees)
            {
                str.Append(tree.ToString()).Append("\n");
            }
            return str.ToString();
        }

        public Operations Add(Operations other)
        {
            List<Tree> trees = new List<Tree>(this.Trees);
            if (other is Tree)
            {
                trees.Add((Tree)other);
            }
            else
            {
                trees.AddRange(other.GetTrees());
            }
            Console.WriteLine("Total: " + trees.Count);
            return new Forest(trees);
        }

        public Operations Product(Operations other)
        {
            Console.WriteLine("Forest-Forest " + this.Trees.Count + " and " + other.GetTrees().Count);
            Forest result = new Forest();
            foreach (Tree tree in Trees)
            {
                result = (Forest)result.Add(tree.Product(other));
            }
            return result;
        }

        private static void test1()
        {
            Tree node1 = new Tree("a");
            Tree node2 = new Tree("b");
            Console.WriteLine(node1.Product(node2));
        }

        public static void test2()
        {
            List<Tree> children = new List<Tree>();
            Tree node3 = new Tree("b");
            children.Add(node3);
            Tree node1 = new Tree("a");
            Tree node2 = new Tree("c", children);
        }

        public static void test6()
        {
            List<Tree> children1 = new List<Tree>();
            List<Tree> children2 = new List<Tree>();
            List<Tree> children3 = new List<Tree>();
            List<Tree> children4 = new List<Tree>();

            Tree leaf2 = new Tree("b");
            Tree leafd = new Tree("d");
            children4.Add(leafd);
            Tree tree3 = new Tree("c", children4);
            children1.Add(tree3);
            children1.Add(leaf2);
            Tree root = new Tree("a", children1);
            List<Tree> forest1 = new List<Tree>();
            forest1.Add(root);
            Forest f1 = new Forest(forest1);
            Forest f2 = f1;
            Console.WriteLine("Risultato: " + f1.Product(f2).ToString());
        }

    }
}
