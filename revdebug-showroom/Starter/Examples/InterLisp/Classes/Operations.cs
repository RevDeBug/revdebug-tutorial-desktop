using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InterLispApp.Classes.Actions;
using Starter.Examples.InterLisp.Classes;

namespace InterLispApp.Classes
{
    public interface IOperation
    {
        object Apply(SeList arguments);
    }

    public static class Operations
    {
        public static object GetOperations(object value)
        {
            if (value.Equals("+"))
                return new PreOpAddition();
            if (value.Equals("-"))
                return new PreOpSubtraction();
            if (value.Equals("*"))
                return new PreOpMultiplication();
            if (value.Equals("/"))
                return new PreOpDivision();
            if (value.Equals("cons"))
                return new PreOpCons();
            if (value.Equals("car"))
                return new PreOpCar();
            if (value.Equals("cdr"))
                return new PreOpCdr();
            if (value.Equals("list"))
                return new PreOpList();
            if (value.Equals("nil"))
                return null;
            if (value.Equals("t"))
                return true;
            if (value.Equals("quote"))
                return new NormOpQuote();
            if (value.Equals("set"))
                return new PreOpSet();
            if (value.Equals("print"))
                return new PreOpPrint();
            if (value.Equals("if"))
                return new NormOpIf();
            if (value.Equals("defun"))
                return new NormOpDefun();
            if (value.Equals("="))
                return new PreOpEqual();
            if (value.Equals("<"))
                return new PreOpLess();
            if (value.Equals(">"))
                return new PreOpGreater();
            if (value.Equals(">="))
                return new PreOpGreaterOrEqual();
            if (value.Equals("<="))
                return new PreOpLessOrEqual();
            if (value.Equals("/="))
                return new PreOpNotEqual();

            string s;
            var word = value as KeyWord;
            if (word != null)
                s = word.Name;
            else
                s = value as string;
                
            if (s != null && Evaluator.MemDictionary.ContainsKey(s))
            {
                return Evaluator.MemDictionary[s];
            }

            return null;
        }
    }
}
