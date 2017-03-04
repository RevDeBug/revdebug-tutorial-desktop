using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes.Actions
{
    //NormOperation (normal operation) - simply eval arguments
    public abstract class NormOperation : IOperation 
    {
        public object Apply(SeList arguments)
        {
            return Execute(arguments);
        }

        public abstract object Execute(SeList arguments);

    }
}
