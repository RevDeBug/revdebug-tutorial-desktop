using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterLispApp.Classes.Actions;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public abstract class PreOperationTwoArgs : PreOperation
    {
        public abstract object Execute(object argument1, object argument2);

        public override object Execute(SeList arguments)
        {
            return Execute(arguments.Left, arguments.RightAsList.Left);
        }
    }
}
