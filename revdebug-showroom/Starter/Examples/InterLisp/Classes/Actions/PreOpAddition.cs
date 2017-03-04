using System;
using Starter.Examples.InterLisp.Classes;


namespace InterLispApp.Classes.Actions
{
    public class PreOpAddition : PreOperation
    {
        public override object Execute(SeList arguments)
        {
            object result = 0;

            while (arguments != null)
            {
                var argument = arguments.Left;
                arguments = arguments.RightAsList;
                result = MathOperations.Add(result, argument);
            }

            return result;
        }
    }
}
