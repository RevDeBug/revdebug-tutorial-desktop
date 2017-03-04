using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterLispApp.Classes.Actions;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class PreOpMultiplication : PreOperation
    {
        public override object Execute(SeList arguments)
        {
            object result = 1;

            while (arguments != null)
            {
                object argument = arguments.Left;
                arguments = arguments.RightAsList;
                result = MathOperations.Multiply(result, argument);
            }

            return result;
        }
    }
}
