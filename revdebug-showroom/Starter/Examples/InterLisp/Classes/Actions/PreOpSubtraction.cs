using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterLispApp.Classes.Actions;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class PreOpSubtraction : PreOperation
    {
        public override object Execute(SeList arguments)
        {
            var result = arguments.Left;
            arguments = arguments.RightAsList;

            while (arguments != null)
            {
                var argument = arguments.Left;
                arguments = arguments.RightAsList;
                result = MathOperations.Subst(result, argument);
            }
            return result;
        }
    }
}
