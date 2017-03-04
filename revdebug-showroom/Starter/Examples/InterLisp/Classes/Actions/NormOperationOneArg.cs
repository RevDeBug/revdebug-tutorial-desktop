using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    //Process normal one argument operation
    public abstract class NormOperationOneArg : NormOperation
    {
        public override object Execute(SeList arguments)
        {
            return Execute(arguments.Left);
        }

        public abstract object Execute(object argument);
    }
}
