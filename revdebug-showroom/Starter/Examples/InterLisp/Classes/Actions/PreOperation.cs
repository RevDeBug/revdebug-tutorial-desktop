using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    //PreOperation (prepare) - eval arguments before main evaluation
    public abstract class PreOperation : IOperation
    {
        public abstract object Execute(SeList arguments);

        public object Apply(SeList arguments)
        {
            var seList = EvalSubList(arguments);
            return this.Execute(seList);
        }

        private static SeList EvalSubList(SeList arguments)
        {
            if (arguments == null)
            {
                return null;
            }

            return new SeList(Evaluator.Evaluate(arguments.Left), EvalSubList(arguments.RightAsList));
        }
    }
}
