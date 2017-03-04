using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    public class PreOpLessOrEqual : PreOperationTwoArgs
    {
        public override object Execute(object argument1, object argument2)
        {
            var isLess = (bool)new PreOpLess().Execute(argument1, argument2);
            var isEqual = (bool)new PreOpEqual().Execute(argument1, argument2);

            return isEqual || isLess;
        }
    }
}
