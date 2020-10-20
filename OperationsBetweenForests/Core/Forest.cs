using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class Forest
    {

        private String name { get; set; }
        private List<Tree> trees { get; set; }

        internal bool IsSingleNode()
        {
            throw new NotImplementedException();
        }

        internal Forest RemoveRoot()
        {
            throw new NotImplementedException();
        }

        internal bool IsSingleTree()
        {
            throw new NotImplementedException();
        }

        internal Node[] GetAllRoots()
        {
            throw new NotImplementedException();
        }

        internal Forest SubForest()
        {
            throw new NotImplementedException();
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
