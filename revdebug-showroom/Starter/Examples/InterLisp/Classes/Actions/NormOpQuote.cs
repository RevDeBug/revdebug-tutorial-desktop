using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    public class NormOpQuote : NormOperationOneArg
    {
        public override object Execute(object argument)
        {
            return argument;
        }
    }
}
