using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    public class PreOpNotEqual : PreOperationTwoArgs
    {
        public override object Execute(object argument1, object argument2)
        {
            double arg1;
            double arg2;

            if (argument1 is long)
                double.TryParse(argument1.ToString(), out arg1);
            else
                arg1 = (double)argument1;

            if (argument2 is long)
                double.TryParse(argument2.ToString(), out arg2);
            else
                arg2 = (double)argument2;

            return ((IComparable)arg1).CompareTo(arg2) != 0;
        }
    }
}
