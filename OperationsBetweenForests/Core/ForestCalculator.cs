using OperationsBetweenForests.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core
{
    class ForestCalculator
    {

        public static Forest ProductRec(Forest a, Forest b)
        {
            //a neutro
            if (a.IsSingleNode()) { return b; }
            //b neutro
            else if (b.IsSingleNode()) { return a; }
            else if (a.IsSingleTree() && b.IsSingleTree())
            {
               return ProductRec(a.RemoveRoot(), b) + ProductRec(a.RemoveRoot(), b.RemoveRoot()) + ProductRec(a, b.RemoveRoot());
            }
            else
            {
                Forest result = new Forest();
                foreach (Forest f in a.SubForest()){
                    result += ProductRec(f, b);
                }
                return result;
            }
        }

    }
}
