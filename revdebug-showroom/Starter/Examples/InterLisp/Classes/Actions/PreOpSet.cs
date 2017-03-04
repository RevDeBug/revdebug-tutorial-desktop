using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InterLispApp.Classes.Actions
{
    public class PreOpSet : PreOperationTwoArgs
    {
        public override object Execute(object argument1, object argument2)
        {
            var keyWord = (KeyWord)argument1;
            if (!Evaluator.MemDictionary.ContainsKey(keyWord.Name))
                Evaluator.MemDictionary.Add(keyWord.Name, argument2);
            return argument2;
        }
    }
}
