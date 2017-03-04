using System;
using System.Collections;
using System.Text;
using InterLispApp.Classes;

namespace Starter.Examples.InterLisp.Classes
{
    //new 2.01
    public class SeList : ISExpression
    {
        private object _left;
        private object _right;

        public SeList(object left, object right)
        {
            this._left = left;
            this._right = right;
        }

        public SeList(object left)
        {
            this._left = left;
        }

        public object Left { get { return _left; } }

        public object Right { get { return _right; } }

        public SeList RightAsList { get { return (SeList)_right; } }

        public static SeList Create(IList elements)
        {
            if (elements == null || elements.Count == 0)
                return null;

            SeList list = null;

            for (int k = elements.Count; k > 0; k--)
                list = new SeList(elements[k - 1], list);

            return list;
        }

        public object Eval()
        {
            var function = (IOperation)Operations.GetOperations(_left);

            if (function == null)
                throw new InvalidOperationException(string.Format("Unknown operation '{0}'", Left));

            return function.Apply(RightAsList);
        }

        public override string ToString()
        {
            var txt = new StringBuilder();

            txt.Append("(" + Converter.Convert(_left));

            object rightObj = _right;

            while (rightObj is SeList)
            {
                var rightList = (SeList)rightObj;
                txt.Append(" " + Converter.Convert(rightList.Left));
                rightObj = rightList._right;
            }

            if (rightObj != null)
            {
                txt.Append(" . " + Converter.Convert(rightObj));
            }

            txt.Append(")");

            return txt.ToString();
        }
    }
}
