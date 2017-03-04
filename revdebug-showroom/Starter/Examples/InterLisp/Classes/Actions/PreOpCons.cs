using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class PreOpCons : PreOperationTwoArgs
    {
        public override object Execute(object argument1, object argument2)
        {
            return new SeList(argument1, argument2);
        }
    }
}
