using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes
{
    public class Evaluator
    {
        public static IDictionary<string, object> MemDictionary = new Dictionary<string, object>(); 
        public static Stack<IDictionary<string, object>> DataStack = new Stack<IDictionary<string, object>>(); 
        
        public static object Evaluate(object value)
        {
            var expression = value as ISExpression;

            if (expression == null)
                return value;

            var result = expression.Eval();
            return result;
        }
    }
}
