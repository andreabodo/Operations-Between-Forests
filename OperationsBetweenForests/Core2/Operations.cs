using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OperationsBetweenForests.Core2
{
    interface Operations
    {
        List<Tree> GetTrees();
        Operations Add(Operations other);
        Operations Product(Operations other);
    }
}
