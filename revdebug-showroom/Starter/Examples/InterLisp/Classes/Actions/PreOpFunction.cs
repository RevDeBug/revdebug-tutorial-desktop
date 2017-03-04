using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class PreOpFunction : PreOperation
    {
        private readonly object _arguments;
        private readonly SeList _body;
        
        public PreOpFunction(object arguments, SeList body)
        {
            _arguments = arguments;
            _body = body;
        }

        public override object Execute(SeList arguments)
        {
            //save data
            var functionParams = (SeList)this._arguments;
            var paramsValues = arguments;
            IDictionary<string, object> stepData = new Dictionary<string, object>();

            while ((functionParams != null) && (paramsValues != null))
            {
                var paramName = (KeyWord)functionParams.Left;
                var paramValue = paramsValues.Left;
                stepData.Add(paramName.Name, paramValue);
                functionParams = functionParams.RightAsList;
                paramsValues = paramsValues.RightAsList;
            }

            if (stepData.Count > 0)
                Evaluator.DataStack.Push(stepData);

            //evaluate
            var body2 = _body;
            object result = null;
            while (body2 != null)
            {
                result = Evaluator.Evaluate(body2.Left);
                body2 = body2.RightAsList;
                Evaluator.DataStack.Pop();
            }
            
            return result;
        }

        private object CastValue(object argument)
        {
            if (argument is double)
            {
                try
                {
                    var resultAsLong = Convert.ToInt64(argument);
                    return resultAsLong;
                }
                catch {}

            }
            
            return argument;
        }

    }
}
