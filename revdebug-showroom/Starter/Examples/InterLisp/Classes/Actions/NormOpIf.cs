using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class NormOpIf : NormOperation
    {
        public override object Execute(SeList arguments)
        {
            var firstArgument = Evaluator.Evaluate(arguments.Left);
            if (firstArgument == null || (firstArgument is bool && ((bool) firstArgument) == false))
            {
                var elseexpr = arguments.RightAsList.RightAsList;

                object result = null;
                while (elseexpr != null)
                {
                    result = Evaluator.Evaluate(elseexpr.Left);
                    elseexpr = elseexpr.RightAsList;
                }
                return result;
            }

            return Evaluator.Evaluate(arguments.RightAsList.Left);
        }
    }
}
