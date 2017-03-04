using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class NormOpDefun : NormOperation
    {
        public override object Execute(SeList arguments)
        {
            var function = (KeyWord) arguments.Left;
            var functionParameters = arguments.RightAsList.Left;
            var functionBody = arguments.RightAsList.RightAsList;

            if (functionBody == null)
            {
                var result = Evaluator.Evaluate(functionParameters);
                Evaluator.MemDictionary.Add(function.Name, result);
                return result;
            }

            var userFunction = new PreOpFunction(functionParameters, functionBody);
            Evaluator.MemDictionary.Add(function.Name, userFunction);

            return "Saved user function: " + function.Name;
        }
    }
}
