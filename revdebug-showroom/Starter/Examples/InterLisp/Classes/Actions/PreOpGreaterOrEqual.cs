using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    class PreOpGreaterOrEqual : PreOperationTwoArgs
    {
        public override object Execute(object argument1, object argument2)
        {
            var isGreater = (bool)new PreOpGreater().Execute(argument1, argument2);
            var isEqual = (bool)new PreOpEqual().Execute(argument1, argument2);

            return (isEqual || isGreater);
        }
    }
}
