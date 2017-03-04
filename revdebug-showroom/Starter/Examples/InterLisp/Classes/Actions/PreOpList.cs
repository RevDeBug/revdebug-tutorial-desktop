using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    public class PreOpList : PreOperation
    {
        public override object Execute(SeList arguments)
        {
            return arguments;
        }
    }
}
