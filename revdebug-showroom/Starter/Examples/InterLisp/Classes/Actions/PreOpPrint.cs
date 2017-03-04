using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    class PreOpPrint : PreOperationOneArg
    {
        public override object Execute(object argument)
        {
            //Console.WriteLine(argument.ToString());
            return argument;
        }
    }
}
