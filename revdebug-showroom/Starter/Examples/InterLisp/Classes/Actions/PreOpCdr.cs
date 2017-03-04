using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class PreOpCdr : PreOperationOneArg
    {
        public override object Execute(object argument)
        {
            if (argument == null)
            {
                return null;
            }

            if (!(argument is SeList))
            {
                throw new InvalidOperationException("Element is not s-expression type.");
            }

            return ((SeList)argument).Right;
        }
    }
}
